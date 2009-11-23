// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using AVCodec;
using WaveOut;

namespace videocut
{
    public class VideoController
    {
        /// <summary>
        /// VideoController の状態を表す
        /// </summary>
        public enum StateKind { NotOpened, Prepared, Waiting, Playing, Stopping };

        private volatile StateKind state_ = StateKind.NotOpened;

        private WaveOutWrapper waveout_ = new WaveOutWrapper();
        private IDecoder avcodec_manager_ = new AVCodecManager();

        private int start_tick_time_ = 0;
        private int start_frame_ = 0;
        private int end_frame_ = 1;
        private volatile int current_frame_ = 0;
        private double frame_per_sec_ = 24.0;

        private volatile bool is_wave_playing_ = false;

        private long current_wave_pos_;

        private List<int> stopping_point_list_ = new List<int>();

        private VideoControllerUser user_;

        private ManualResetEvent thread_event_ = new ManualResetEvent(true);
        private ManualResetEvent thread_end_event_ = new ManualResetEvent(false);

        private DrawingThread drawing_thread_ = null;
        private IntPtr picture_box_handle;

        private volatile List<Command> command_queue_ = new List<Command>();

        public VideoController(VideoControllerUser user)
        {
            user_ = user;
        }

        public string Info
        {
            get
            {
                return "start_tick_time_ = " + start_tick_time_ + "\r\n" +
                    "start_frame_ = " + start_frame_ + "\r\n" +
                    "end_frame_ = " + end_frame_ + "\r\n" +
                    "current_frame_ = " + current_frame_ + "\r\n" +
                    "frame_per_sec_ = " + frame_per_sec_ + "\r\n" +
                    "current_wave_pos_ = " + current_wave_pos_ + "\r\n" +
                    "is_wave_playing_ = " + is_wave_playing_ + "\r\n" +
                    "state_ = " + state_ + "\r\n" +
                    "waveout.state = " + waveout_.State + "\r\n";
            }
        }

        public StateKind State
        {
            get { return state_; }
        }

        public int FrameLength
        {
            get { return end_frame_; }
        }

        public double FramePerSec
        {
            get { return frame_per_sec_; }
        }

        public IntPtr PictureBoxHandle
        {
            set { picture_box_handle = value; }
        }

        public int[] KeyFrameList
        {
            get { return avcodec_manager_.KeyFrameList; }
        }

        public bool[] FrameExistsList
        {
            get { return avcodec_manager_.FrameExistsList; }
        }

        public void OpenFiles(string filename)
        {
            if (state_ != StateKind.NotOpened)
            {
                Close();
            }
            System.Diagnostics.Debug.Write("VideoController.OpenFiles");

            avcodec_manager_.Open(filename);

            if (avcodec_manager_.HasAudio)
            {
                waveout_.AudioCallback = AudioCallback;
                waveout_.TimeOut = TimeOutCallback;
                waveout_.Stopped = StoppedCallback;
                waveout_.Open(avcodec_manager_.AudioSampleRate, avcodec_manager_.AudioChannel,
                    avcodec_manager_.AudioBytesPerSample * 8);
            }

            frame_per_sec_ = (double)avcodec_manager_.Rate / avcodec_manager_.Scale;
            end_frame_ = avcodec_manager_.FrameLength;
            user_.InformChangingFrame(0);
            if (avcodec_manager_.HasVideo)
            {
                drawing_thread_ = new DrawingThread(avcodec_manager_.Width, avcodec_manager_.Height, picture_box_handle);
                drawing_thread_.Start();
                user_.SetPictureBoxSize(new Size(avcodec_manager_.Width, avcodec_manager_.Height));
            }

            Thread t = new Thread(new ThreadStart(Run));
            t.IsBackground = true;
            t.Start();

            state_ = StateKind.Prepared;

            thread_event_.Set();
            DrawWhileGetting(0); // 最初のフレームを描画
        }

        public void Close()
        {
            if (avcodec_manager_.HasVideo)
            {
                drawing_thread_.Close();
            }
            if (avcodec_manager_.HasAudio)
            {
                waveout_.Close();
            }
            thread_end_event_.Reset();
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.EndThread));
            }
            thread_end_event_.WaitOne(3000, false);

            avcodec_manager_.Close();

            Clear();

            state_ = StateKind.NotOpened;
        }

        public WaveOutWrapper.CallbackKind AudioCallback(IntPtr data, int size, object user_object)
        {
            if (!avcodec_manager_.IsPreparedAudio(current_wave_pos_, size))
            {
                return WaveOutWrapper.CallbackKind.Wait;
            }
            else if (avcodec_manager_.SupplyAudioData(data, ref current_wave_pos_, size))
            {
                return WaveOutWrapper.CallbackKind.Play;
            }
            else
            {
                return WaveOutWrapper.CallbackKind.Stop;
            }
        }

        public void StoppedCallback()
        {

        }

        public void TimeOutCallback()
        {
            if (state_ == StateKind.Playing)
            {
                is_wave_playing_ = false;
                state_ = StateKind.Waiting;
                user_.DisplayLoad(true);
            }
        }

        public void Start(int start_frame)
        {
            System.Diagnostics.Debug.Write("VideoController.Start");
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.Start, start_frame));
            }
        }

        public void Stop()
        {
            System.Diagnostics.Debug.Write("VideoController.Stop");
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.Stop));
            }
        }

        public void SeekAndDraw(int frame)
        {
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.Seek, frame));
            }
        }

        public void StopSeek(int frame)
        {
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.Stop));
                command_queue_.Add(new Command(Command.Kind.Seek, frame));
            }
        }
        
        public void DrawCurrentFrame()
        {
            SeekAndDraw(current_frame_);
        }

        public void SetStoppingPoint(int point_frame)
        {
            stopping_point_list_.Clear();
            if (point_frame >= 0)
            {
                stopping_point_list_.Add(point_frame);
            }
        }

        private void Run()
        {
            System.Diagnostics.Debug.Write("VideoCtrl Thread Start");

            while (true)
            {
                Command command = GetCommand();

                if (command != null)
                {
                    if (command.kind_ == Command.Kind.EndThread)
                    {
                        break;
                    }
                    else
                    {
                        DoCommand(command);
                    }
                }

                switch (state_)
                {
                    case StateKind.Waiting:
                        if ((!avcodec_manager_.HasVideo ||
                                avcodec_manager_.IsPreparedVideo(current_frame_) &&
                                    avcodec_manager_.IsPreparedVideo(Math.Min(current_frame_ + 2, FrameLength - 1))) &&
                            (!avcodec_manager_.HasAudio ||
                                avcodec_manager_.IsPreparedAudio(current_wave_pos_, waveout_.BufferLength * 2)))
                        {
                            start_tick_time_ = Environment.TickCount;
                            start_frame_ = current_frame_;
                            if (avcodec_manager_.HasVideo)
                            {
                                user_.DisplayLoad(false);
                                BufferContainer buffer = avcodec_manager_.GetFrame(current_frame_);
                                if (buffer != null)
                                {
                                    drawing_thread_.Draw(buffer.Buffer);
                                }
                            }
                            if (avcodec_manager_.HasAudio)
                            {
                                waveout_.Play();
                                is_wave_playing_ = true;
                            }
                            state_ = StateKind.Playing;
                        }
                        break;
                    case StateKind.Playing:
                        int update_frame = (int)((Environment.TickCount - start_tick_time_) * frame_per_sec_ / 1000) + start_frame_;

                        if (update_frame > end_frame_ || JudgeStoppingPoint(update_frame))
                        {
                            lock (command_queue_)
                            {
                                command_queue_.Add(new Command(Command.Kind.Stop));
                            }
                        }

                        if (update_frame != current_frame_)
                        {
                            current_frame_ = update_frame;
                            if (avcodec_manager_.HasVideo)
                            {
                                if (avcodec_manager_.IsPreparedVideo(update_frame))
                                {
                                    RequestDraw(current_frame_);
                                }
                                else
                                {
                                    avcodec_manager_.RequireSeeking(current_frame_);
                                    state_ = StateKind.Waiting;
                                    user_.DisplayLoad(true);
                                    if (avcodec_manager_.HasAudio)
                                    {
                                        waveout_.Stop();
                                        is_wave_playing_ = false;
                                    }
                                }
                            }
                            user_.InformChangingFrame(current_frame_);
                        }
                        break;
                }
                Thread.Sleep(1);
            }
            thread_end_event_.Set();
        }

        private void DoCommand(Command command)
        {
            if (command.kind_ == Command.Kind.Start)
            {
                current_frame_ = command.frame_;
                user_.InformChangingFrame(current_frame_);
                if (avcodec_manager_.HasVideo)
                {
                    DrawWhileGetting(current_frame_);
                }
                if (avcodec_manager_.HasAudio)
                {
                    if (is_wave_playing_)
                    {
                        System.Diagnostics.Debug.Write("waveout_.Stop()");
                        System.Diagnostics.Debug.Write(waveout_.State.ToString());
                        waveout_.Stop();
                        is_wave_playing_ = false;
                    }
                    current_wave_pos_ = GetWavePos(current_frame_);
                }
                state_ = StateKind.Waiting;
            }
            else if (command.kind_ == Command.Kind.Seek)
            {
                current_frame_ = command.frame_;
                user_.InformChangingFrame(current_frame_);
                if (avcodec_manager_.HasVideo)
                {
                    DrawWhileGetting(current_frame_);
                }
                if (avcodec_manager_.HasAudio)
                {
                    current_wave_pos_ = GetWavePos(current_frame_);
                }
            }
            else if (command.kind_ == Command.Kind.Stop)
            {
                if (avcodec_manager_.HasAudio)
                {
                    if (is_wave_playing_)
                    {
                        waveout_.Stop();
                        is_wave_playing_ = false;
                    }
                }
                if (command.frame_ >= 0) // シークする
                {
                    current_frame_ = command.frame_;
                    if (avcodec_manager_.HasVideo)
                    {
                        DrawWhileGetting(current_frame_);
                    }
                    if (avcodec_manager_.HasAudio)
                    {
                        current_wave_pos_ = GetWavePos(current_frame_);
                    }
                }
                user_.InformStop();
                state_ = StateKind.Prepared;
            }
            else if (command.kind_ == Command.Kind.Draw)
            {
                if (avcodec_manager_.HasVideo)
                {
                    DrawWhileGetting(current_frame_);
                }
            }
        }

        private long GetWavePos(int frame)
        {
            long pos = (long)((double)frame * avcodec_manager_.AudioSampleRate *
                avcodec_manager_.AudioChannel * avcodec_manager_.AudioBytesPerSample / frame_per_sec_);
            if (pos % 4 != 0)
            {
                pos += 4 - pos % 4;
            }
            if (pos < 0)
            {
                pos += 0;
            }
            return pos;
        }

        private void Clear()
        {
            start_tick_time_ = 0;
            start_frame_ = 0;
            end_frame_ = 0;
            current_frame_ = 0;
            frame_per_sec_ = 24.0;

            is_wave_playing_ = false;

            current_wave_pos_ = 0;
            thread_event_.Set();
            thread_end_event_.Reset();

            stopping_point_list_.Clear();

            drawing_thread_ = null;

            command_queue_.Clear();
        }

        // 取得できるまで待ってDraw
        private void DrawWhileGetting(int frame)
        {
            if (avcodec_manager_.IsPreparedVideo(frame))
            {
                RequestDraw(frame);
            }
            else
            {
                avcodec_manager_.RequireSeeking(frame);
                lock (command_queue_)
                {
                    command_queue_.Add(new Command(Command.Kind.Draw));
                }
            }
        }

        private Command GetCommand()
        {
            Command command = null;
            lock (command_queue_)
            {
                if (command_queue_.Count >= 100)
                {
                    throw new Exception();
                }
                for (int i = 0; i < command_queue_.Count; ++i) // EndThread だけは最優先
                {
                    if (command_queue_[i].kind_ == Command.Kind.EndThread)
                    {
                        command = command_queue_[i];
                        command_queue_.Clear();
                        return command;
                    }
                }
                int seek_frame = -1;
                for (int i = command_queue_.Count - 1; i >= 0; --i)
                {
                    switch (command_queue_[i].kind_)
                    {
                        case Command.Kind.Start:
                            if (seek_frame >= 0)
                            {
                                command_queue_.Clear();
                                return new Command(Command.Kind.Start, seek_frame);
                            }
                            else
                            {
                                command = command_queue_[i];
                                command_queue_.Clear();
                                return command;
                            }
                        case Command.Kind.Stop:
                            command = command_queue_[i];
                            command_queue_.Clear();
                            if (seek_frame >= 0)
                            {
                                return new Command(Command.Kind.Stop, seek_frame);
                            }
                            else
                            {
                                return command;
                            }
                        case Command.Kind.Seek:
                            if (seek_frame < 0)
                            {
                                seek_frame = command_queue_[i].frame_;
                            }
                            break;
                    }
                }
                if (seek_frame >= 0)
                {
                    command_queue_.Clear();
                    return new Command(Command.Kind.Seek, seek_frame);
                }
                for (int i = command_queue_.Count - 1; i >= 0; --i)
                {
                    if (command_queue_[i].kind_ == Command.Kind.Draw)
                    {
                        command = command_queue_[i];
                        command_queue_.Clear();
                        return command;
                    }
                }
                return null;
            }
        }

        private void RequestDraw(int frame)
        {
            BufferContainer buffer = avcodec_manager_.GetFrame(frame);
            if (buffer != null)
            {
                drawing_thread_.Draw(buffer.Buffer);
            }
        }

        private bool JudgeStoppingPoint(int update_frame)
        {
            for (int i = 0; i < stopping_point_list_.Count; ++i)
            {
                if (stopping_point_list_[i] < update_frame && update_frame < stopping_point_list_[i] + 10)
                {
                    stopping_point_list_.Clear();
                    waveout_.Stop();
                    is_wave_playing_ = false;
                    user_.InformStop();
                    return true;
                }
            }
            return false;
        }
    }

    class Command
    {
        public enum Kind { Start, Stop, Seek, Draw, EndThread };
        public Kind kind_;
        public int frame_;

        public Command(Kind kind)
        {
            kind_ = kind;
            frame_ = -1;
        }

        public Command(Kind kind, int frame)
        {
            kind_ = kind;
            frame_ = frame;
        }

        public override string ToString()
        {
            return kind_.ToString() + (frame_ >= 0 ? ", " + frame_ : "");
        }
    }

    class DrawingThread
    {
        [DllImport("gdi32.dll")]
        private extern static bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hdcSource, int nXSource, int nYSource, int dwRaster);

        [DllImport("gdi32.dll")]
        private extern static bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hdcSource, int nXSource, int nYSource, int nWidthSrc, int nHeightSrc, int dwRaster);

        [DllImport("gdi32.dll")]
        private extern static IntPtr SelectObject(IntPtr p1, IntPtr p2);

        [DllImport("user32.dll")]
        private extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private extern static bool DeleteObject(IntPtr hObject);

        private AutoResetEvent thread_event_ = new AutoResetEvent(false);
        private IntPtr bitmap_data_ = IntPtr.Zero;
        private int width_;
        private int height_;

        private volatile bool closing_ = false;

        private volatile IntPtr data_;

        private IntPtr picture_box_handle_;
        private IntPtr picture_box_dc_;

        public DrawingThread(int width, int height, IntPtr picture_box_handle)
        {
            width_ = width;
            height_ = height;
            picture_box_handle_ = picture_box_handle;
            picture_box_dc_ = GetDC(picture_box_handle);
        }

        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Run));
            t.IsBackground = true;
            t.Start();
        }

        public void Close()
        {
            closing_ = true;
            thread_event_.Set();
        }

        private void Run()
        {
            thread_event_.WaitOne();

            while (!closing_)
            {
                using (Bitmap bitmap = new Bitmap(width_, height_, width_ * 3, PixelFormat.Format24bppRgb, data_))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        IntPtr hdc1 = g.GetHdc();
                        IntPtr hbmp1 = bitmap.GetHbitmap();
                        IntPtr old_obj = SelectObject(hdc1, hbmp1);

                        BitBlt(picture_box_dc_, 0, 0, width_, height_, hdc1, 0, 0, 0x00CC0020);

                        SelectObject(hdc1, old_obj);
                        DeleteObject(hbmp1);
                        g.ReleaseHdc(hdc1);
                    }
                }
                thread_event_.WaitOne();
            }
        }

        public void Draw(IntPtr data)
        {
            data_ = data;
            thread_event_.Set();
        }
    }

    public interface VideoControllerUser
    {
        void SetPictureBoxSize(Size size);
        void InformChangingFrame(int frame);
        void InformStop();
        void DisplayLoad(bool is_display);
    }
}
