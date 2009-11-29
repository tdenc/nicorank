// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace AVCodec
{
    public class AVCodecManager : IDecoder
    {
        private AVFormatContext format_context;
        private IntPtr p_avformat_context;

        private IntPtr p_video_codec;
        private IntPtr p_audio_codec;

        private int video_stream_index = -1;
        private int audio_stream_index = -1;

        private AVCodecContext video_codec_context;
        private AVCodecContext audio_codec_context;

        private AVStream video_stream;
        private AVStream audio_stream;

        private IntPtr p_frame;
        private IntPtr p_frame_rgb;
        private IntPtr p_packet;
        private IntPtr p_audio_buffer;

        private VideoBufferManager video_buffer_manager_;
        private AudioBufferManager audio_buffer_manager_;

        private int video_rate_;
        private int video_scale_;

        // なぜかこうすると落ちる
        // private const int audio_temp_buffer_size_ = 192000; // from avcodec.h 
        private const int audio_temp_buffer_size_ = 600000; // ←この数字は適当

        private List<int> key_frame_list_; // キーフレームの場所をフレーム数単位で表す
        private List<long> key_dts_list_;   // キーフレームの場所をdts単位で表す

        private int current_frame_;        // 現在デコードしているフレーム

        private int last_requested_frame_; // 最後にユーザに要求されたフレーム

        private int audio_priority_times_; // この値が正のときは音声優先
        private Queue<PacketContainer> av_packet_queue_; // パケットをキャッシュするキュー

        private ManualResetEvent thread_end_event_; // デコードスレッドの終了を待つためのイベント

        private volatile List<Command> command_queue_; // コマンドキュー

        private int fixed_video_width_;
        private int fixed_video_height_;

        public AVCodecManager()
        {
            Clear();
        }

        public int CurrentFrame // for debug
        {
            get { return current_frame_; }
        }

        public int VideoPictureBufferSize
        {
            get { return AVCodecAPI.avpicture_get_size((int)AVCodecAPI.AVPixelFormat.PIX_FMT_BGR24,
                Width, Height); }
        }

        public int AudioChannel
        {
            get { return audio_codec_context.channels; }
        }

        public int AudioSampleRate
        {
            get { return audio_codec_context.sample_rate; }
        }

        public int AudioBytesPerSample
        {
            get
            {
                if (audio_codec_context.sample_fmt == 1)
                {
                    return 2;
                }
                else
                {
                    throw new AVCodecException();
                }
            }
        }

        public int Rate
        {
            get { return HasVideo ? video_rate_ : 24; }
        }

        public int Scale
        {
            get { return HasVideo ? video_scale_ : 1; }
        }

        public int FrameLength
        {
            get { return (int)(format_context.duration * Rate / Scale / 1000000); }
        }

        public int Width
        {
            get { return (fixed_video_width_ >= 0 ? fixed_video_width_ : video_codec_context.width); }
        }

        public int Height
        {
            get { return (fixed_video_height_ >= 0 ? fixed_video_height_ : video_codec_context.height); }
        }

        public bool HasVideo
        {
            get { return video_stream_index >= 0; }
        }

        public bool HasAudio
        {
            get { return audio_stream_index >= 0; }
        }

        public int[] KeyFrameList
        {
            get { return key_frame_list_.ToArray(); }
        }

        public bool[] FrameExistsList
        {
            get { return (video_buffer_manager_ != null ? video_buffer_manager_.FrameExistsList : null); }
        }

        public bool IsOpen
        {
            get { return p_avformat_context != IntPtr.Zero; }
        }

        /// <summary>
        /// 動画ファイルを開く
        /// </summary>
        /// <param name="filename">ファイルパス</param>
        public void Open(string filename)
        {
            Open(filename, -1, -1, -1);
        }

        /// <summary>
        /// 動画ファイルを開く
        /// </summary>
        /// <param name="filename">ファイルパス</param>
        /// <param name="width">動画サイズを固定する場合の幅（負の値を指定した場合は元の動画サイズになる）</param>
        /// <param name="height">動画サイズを固定する場合の高さ（負の値を指定した場合は元の動画サイズになる）</param>
        /// <param name="memory_size">使用するメモリサイズ（MB）（負の値を指定した場合は自動設定）</param>
        public void Open(string filename, int video_width, int video_height, int memory_size)
        {
            OpenCodec(filename);

            fixed_video_width_ = video_width;
            fixed_video_height_ = video_height;

            video_rate_ = video_stream.r_frame_rate.num;
            video_scale_ = video_stream.r_frame_rate.den;

            p_frame = AVCodecAPI.avcodec_alloc_frame();
            p_frame_rgb = AVCodecAPI.avcodec_alloc_frame();

            p_packet = AVCodecAPI.av_malloc(Marshal.SizeOf(new AVPacket()));

            long last_dts = 0;
            int c = 0;

            bool is_first = true;
            int start_frame = 0;

            while (c >= 0)
            {
                if (!is_first)
                {
                    AVCodecAPI.av_seek_frame(p_avformat_context, 0, last_dts + 1, 0);
                }

                while ((c = AVCodecAPI.av_read_frame(p_avformat_context, p_packet)) >= 0)
                {
                    PacketContainer container = new PacketContainer(p_packet);
                    if (container.packet.stream_index == video_stream_index)
                    {
                        last_dts = container.packet.dts;
                        key_dts_list_.Add(last_dts);
                        key_frame_list_.Add(DtsToVideoFrame(last_dts));
                        if (is_first)
                        {
                            is_first = false;
                            start_frame = key_frame_list_[key_frame_list_.Count - 1];
                        }
                        break;
                    }
                    container.Destruct();
                }
            }
            AVCodecAPI.av_seek_frame(p_avformat_context, 0, 0, 0);

            if (IsTimeScaleFour())
            {
                video_scale_ *= 4;
                for (int i = 0; i < key_frame_list_.Count; ++i)
                {
                    key_frame_list_[i] /= 4;
                }
            }

            if (video_rate_ / video_scale_ > 60)
            {
                FixIllegalFrameRate();
            }

            if (HasVideo)
            {
                if (memory_size <= 0)
                {
                    memory_size = Math.Min(Math.Max(BufferContainer.GetMemorySize() / (1024 * 1024) - 500, 100), 500); // 最小100MB、最大500MB
                }
                video_buffer_manager_ = new VideoBufferManager(this, FrameLength, memory_size * 1024 * 1024 / VideoPictureBufferSize, start_frame);
            }
            if (HasAudio)
            {
                audio_buffer_manager_ = new AudioBufferManager();
                audio_buffer_manager_.SetDataLength(AudioSampleRate * AudioChannel
                    * AudioBytesPerSample * format_context.duration / 1000000);

                p_audio_buffer = Marshal.AllocHGlobal(audio_temp_buffer_size_);

                audio_priority_times_ = 10;
            }
            StartDecoding();
        }

        /// <summary>
        /// ファイルを閉じる
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                lock (command_queue_)
                {
                    command_queue_.Add(new Command(Command.Kind.EndThread));
                }
                thread_end_event_.WaitOne();

                if (video_stream_index >= 0)
                {
                    AVCodecAPI.avcodec_close(video_stream.codec);
                }
                if (audio_stream_index >= 0)
                {
                    AVCodecAPI.avcodec_close(audio_stream.codec);
                }
                AVCodecAPI.av_close_input_file(p_avformat_context);

                if (HasVideo)
                {
                    video_buffer_manager_.Close();
                }
                if (HasAudio)
                {
                    audio_buffer_manager_.Close();
                }

                Clear();
            }
        }

        /// <summary>
        /// 必要ならシークをデコードスレッドに要求する
        /// </summary>
        /// <param name="frame">フレーム番号</param>
        public void RequireSeeking(int frame)
        {
            lock (command_queue_)
            {
                command_queue_.Add(new Command(Command.Kind.Seek, frame));
            }
        }

        /// <summary>
        /// フレームを取得する。デコードがまだされていない場合は null が返ると同時に
        /// デコードスレッドに指定したフレーム番号をデコードするように要求する
        /// </summary>
        /// <param name="frame">フレーム番号</param>
        /// <returns></returns>
        public BufferContainer GetFrame(int frame)
        {
            if (frame < 0)
            {
                frame = 0;
            }
            else if (frame >= FrameLength)
            {
                frame = FrameLength - 1;
            }
            BufferContainer buffer = video_buffer_manager_.GetFrame(frame);
            if (buffer == null)
            {
                RequireSeeking(frame);
            }
            return buffer;
        }

        /// <summary>
        /// 引数で指定した data に位置 pos、大きさ size のデータをコピーする
        /// </summary>
        /// <param name="data">コピー先バッファ</param>
        /// <param name="pos">コピー元の位置</param>
        /// <param name="size">コピーするサイズ</param>
        /// <returns></returns>
        public bool SupplyAudioData(IntPtr data, ref long pos, int size)
        {
            return audio_buffer_manager_.SupplyAudioData(data, ref pos, size);
        }

        /// <summary>
        /// 指定したフレーム番号がデコード済みかどうか調べる
        /// </summary>
        /// <param name="frame">フレーム番号</param>
        /// <returns>デコード済みなら true</returns>
        public bool IsPreparedVideo(int frame)
        {
            last_requested_frame_ = frame;
            return video_buffer_manager_.IsPreparedVideo(frame);
        }

        /// <summary>
        /// 指定した位置、長さの音声がデコード済みかどうか調べる
        /// </summary>
        /// <param name="start">位置</param>
        /// <param name="length">長さ</param>
        /// <returns>デコード済みなら true</returns>
        public bool IsPreparedAudio(long start, int length)
        {
            return audio_buffer_manager_.IsFilled(start, length);
        }

        private void StartDecoding()
        {
            Thread t = new Thread(new ThreadStart(Run));
            t.IsBackground = true;
            t.Start();
        }

        private void OpenCodec(string filename)
        {
            AVCodecAPI.av_register_all();

            int error_code = AVCodecAPI.av_open_input_file(out p_avformat_context, filename, IntPtr.Zero, 0, IntPtr.Zero);
            if (error_code != 0)
            {
                throw new AVCodecCannotOpenFileException();
            }
            error_code = AVCodecAPI.av_find_stream_info(p_avformat_context);
            if (error_code < 0)
            {
                throw new AVCodecException();
            }
            format_context = (AVFormatContext)Marshal.PtrToStructure(p_avformat_context, typeof(AVFormatContext));

            for (int i = 0; i < format_context.nb_streams; ++i)
            {
                AVStream stream = (AVStream)Marshal.PtrToStructure(format_context.streams[i], typeof(AVStream));
                AVCodecContext temp_codec_context = (AVCodecContext)Marshal.PtrToStructure(stream.codec,
                                                                                           typeof(AVCodecContext));
                if (temp_codec_context.codec_type == 0)
                {
                    if (video_stream_index < 0)
                    {
                        video_stream_index = i;
                        video_stream = stream;
                        video_codec_context = temp_codec_context;
                    }
                }
                else if (temp_codec_context.codec_type == 1)
                {
                    if (audio_stream_index < 0)
                    {
                        audio_stream_index = i;
                        audio_stream = stream;
                        audio_codec_context = temp_codec_context;
                    }
                }
            }

            if (video_stream_index >= 0)
            {
                p_video_codec = AVCodecAPI.avcodec_find_decoder(video_codec_context.codec_id);

                error_code = AVCodecAPI.avcodec_open(video_stream.codec, p_video_codec);
                if (error_code < 0)
                {
                    throw new AVCodecException();
                }
            }

            if (audio_stream_index >= 0)
            {
                p_audio_codec = AVCodecAPI.avcodec_find_decoder(audio_codec_context.codec_id);

                error_code = AVCodecAPI.avcodec_open(audio_stream.codec, p_audio_codec);
                if (error_code < 0)
                {
                    throw new AVCodecException();
                }
            }
        }

        // H.264 動画で TimeScale が4倍精度になってるか調べる
        public bool IsTimeScaleFour()
        {
            int c;
            int count = 0;
            long[] dts = new long[4];

            if (video_codec_context.codec_id != 28) // H.264 でないので関係ない
            {
                return false;
            }

            while ((c = AVCodecAPI.av_read_frame(p_avformat_context, p_packet)) >= 0)
            {
                PacketContainer container = new PacketContainer(p_packet);
                if (container.packet.stream_index == video_stream_index)
                {
                    dts[count] = container.packet.dts;
                    ++count;
                }
                container.Destruct();
                if (count >= dts.Length)
                {
                    break;
                }
            }
            AVCodecAPI.av_seek_frame(p_avformat_context, 0, 0, 0);

            return dts[1] - dts[0] == 1001 && dts[2] - dts[1] == 1001 && dts[3] - dts[2] == 2002;
        }

        public void FixIllegalFrameRate()
        {
            int c;
            int count = 0;
            long[] dts = new long[50];

            while ((c = AVCodecAPI.av_read_frame(p_avformat_context, p_packet)) >= 0)
            {
                PacketContainer container = new PacketContainer(p_packet);
                if (container.packet.stream_index == video_stream_index)
                {
                    dts[count] = container.packet.dts;
                    ++count;
                }
                container.Destruct();
                if (count >= dts.Length)
                {
                    break;
                }
            }
            double average_dts = 0.0;
            for (int i = 5; i < count - 1; ++i) // 最初の5フレームは捨てる
            {
                average_dts += dts[i + 1] - dts[i];
            }
            average_dts /= (double)(count - 1 - 5);

            if (average_dts > 0)
            {
                double new_frame_rate_d = (double)video_stream.time_base.den / video_stream.time_base.num / average_dts;
                int new_frame_rate = (int)(new_frame_rate_d * 1000);
                int new_frame_scale = 1000;

                new_frame_rate = 30;
                new_frame_scale = 1;

                for (int i = 0; i < key_frame_list_.Count; ++i)
                {
                    key_frame_list_[i] = (int)((double)key_frame_list_[i] * new_frame_rate * video_scale_
                        / ((double)new_frame_scale * video_rate_));
                }
                video_rate_ = new_frame_rate;
                video_scale_ = new_frame_scale;
            }

            AVCodecAPI.av_seek_frame(p_avformat_context, 0, 0, 0);
        }

        private void Clear()
        {
            format_context = null;
            p_avformat_context = IntPtr.Zero;

            p_video_codec = IntPtr.Zero;
            p_audio_codec = IntPtr.Zero;

            video_stream_index = -1;
            audio_stream_index = -1;

            video_codec_context = null;
            audio_codec_context = null;

            video_stream = null;
            audio_stream = null;

            p_frame = IntPtr.Zero;
            p_frame_rgb = IntPtr.Zero;
            p_packet = IntPtr.Zero;
            if (p_audio_buffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(p_audio_buffer);
                p_audio_buffer = IntPtr.Zero;
            }

            video_buffer_manager_ = null;
            audio_buffer_manager_ = null;

            video_rate_ = 24;
            video_scale_ = 1;

            key_frame_list_ = new List<int>();
            key_dts_list_ = new List<long>();

            current_frame_ = 0;

            last_requested_frame_ = 0;

            audio_priority_times_ = 0;
            av_packet_queue_ = new Queue<PacketContainer>();

            thread_end_event_ = new ManualResetEvent(true);
            command_queue_ = new List<Command>();

            fixed_video_width_ = -1;
            fixed_video_height_ = -1;
        }

        // デコードスレッド
        private void Run()
        {
            thread_end_event_.Reset();
            while (true)
            {
                Command command = GetCommand();
                if (command != null)
                {
                    if (command.kind_ == Command.Kind.EndThread)
                    {
                        break;
                    }
                    else if (command.kind_ == Command.Kind.Seek)
                    {
                        SeekInner(command.frame_);
                    }
                    else if (command.kind_ == Command.Kind.Wait)
                    {
                        Thread.Sleep(1);
                        continue;
                    }
                }

                if (HasVideo && current_frame_ >= last_requested_frame_
                                    + video_buffer_manager_.BufferingFrameNum / 2)
                {
                    Thread.Sleep(1); // デコードしないで寝る
                }
                else
                {
                    DecodePacket();
                }
            }

            thread_end_event_.Set();
            System.Diagnostics.Debug.WriteLine("AVCodec Thread End");
        }

        private Command GetCommand()
        {
            Command command = null;
            lock (command_queue_)
            {
                for (int i = 0; i < command_queue_.Count; ++i) // EndThread だけは最優先
                {
                    if (command_queue_[i].kind_ == Command.Kind.EndThread)
                    {
                        command = command_queue_[i];
                        command_queue_.Clear();
                        return command;
                    }
                }
                if (command_queue_.Count > 0)
                {
                    for (int i = command_queue_.Count - 1; i >= 0; --i)
                    {
                        if (command_queue_[i].kind_ == Command.Kind.Seek)
                        {
                            if (command == null) // 最初
                            {
                                command = command_queue_[i];
                            }
                        }
                        command_queue_.RemoveAt(i);
                    }
                }
                if (command != null)
                {
                    return command;
                }
                else
                {
                    if (command_queue_.Count > 0)
                    {
                        return command_queue_[command_queue_.Count - 1];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // frame の属するブロック番号を取得
        private int GetBlockNumber(int frame)
        {
            for (int i = 1; i < key_frame_list_.Count; ++i)
            {
                if (frame < key_frame_list_[i])
                {
                    return i - 1;
                }
            }
            return key_frame_list_.Count - 1;
        }

        private long VideoFrameToDts(int frame)
        {
            return (long)((double)frame * video_scale_ * video_stream.time_base.den
                / ((double)video_rate_ * video_stream.time_base.num) + 0.5);
        }

        private int DtsToVideoFrame(long dts)
        {
            return (int)((double)dts * video_rate_ * video_stream.time_base.num
                / ((double)video_scale_ * video_stream.time_base.den) + 0.5);
        }

        private int AudioByteToDts(int byte_size)
        {
            return (int)((double)byte_size * audio_stream.time_base.den
                / (((double)audio_codec_context.sample_rate * audio_codec_context.channels * AudioBytesPerSample)
                * audio_stream.time_base.num) + 0.5);
        }

        private int DtsToAudioByte(long dts)
        {
            return (int)((double)dts
                * ((double)audio_codec_context.sample_rate * audio_codec_context.channels
                    * AudioBytesPerSample)
                * audio_stream.time_base.num / audio_stream.time_base.den + 0.5);
        }

        private void DecodePacket()
        {
            PacketContainer packet;
            if ((packet = ReadPacket()) != null)
            {
                if (packet.packet.stream_index == video_stream_index)
                {
                    DecodeVideo(packet.packet);
                }
                else if (packet.packet.stream_index == audio_stream_index)
                {
                    DecodeAudio(packet.packet);
                }
                packet.Destruct();
            }
            else // 最後まで到達したので待機
            {
                lock (command_queue_)
                {
                    command_queue_.Insert(0, new Command(Command.Kind.Wait));
                }
            }
        }

        private PacketContainer ReadPacket()
        {
            if (audio_priority_times_ > 0)
            {
                --audio_priority_times_;

                int c;
                while ((c = AVCodecAPI.av_read_frame(p_avformat_context, p_packet)) >= 0)
                {
                    PacketContainer packet = new PacketContainer(p_packet);
                    
                    if (packet.packet.stream_index == audio_stream_index)
                    {
                        return packet;
                    }
                    else
                    {
                        av_packet_queue_.Enqueue(packet);
                    }
                }
                if (av_packet_queue_.Count > 0)
                {
                    return av_packet_queue_.Dequeue();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (av_packet_queue_.Count > 0)
                {
                    return av_packet_queue_.Dequeue();
                }
                else
                {
                    int c = AVCodecAPI.av_read_frame(p_avformat_context, p_packet);
                    if (c >= 0)
                    {
                        PacketContainer packet = new PacketContainer(p_packet);
                        return packet;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private void DecodeVideo(AVPacket packet)
        {
            int frame_finished;
            AVCodecAPI.avcodec_decode_video(video_stream.codec, p_frame, out frame_finished, packet.data, packet.size);

            if (frame_finished > 0)
            {
                BufferContainer buffer = video_buffer_manager_.GetEmptyBufferContainer(current_frame_);
                double dummy_val = 0;

                AVCodecAPI.avpicture_fill(p_frame_rgb, buffer.Buffer, (int)AVCodecAPI.AVPixelFormat.PIX_FMT_BGR24,
                    Width, Height);

                IntPtr sws_context = AVCodecAPI.sws_getContext(video_codec_context.width, video_codec_context.height,
                    video_codec_context.pix_fmt, Width, Height,
                    (int)AVCodecAPI.AVPixelFormat.PIX_FMT_BGR24, 4, IntPtr.Zero, IntPtr.Zero, ref dummy_val);
                try
                {
                    AVCodecAPI.sws_scale(sws_context, p_frame, new IntPtr(p_frame.ToInt32() + 16), 0,
                        video_codec_context.height, p_frame_rgb, new IntPtr(p_frame_rgb.ToInt32() + 16));
                }
                finally
                {
                    AVCodecAPI.sws_freeContext(sws_context);
                }
                buffer.Time = (int)packet.dts;
                current_frame_ = DtsToVideoFrame((int)packet.dts);
                buffer.Frame = current_frame_;
                video_buffer_manager_.SetBufferContainer(buffer);
            }
        }

        private void DecodeAudio(AVPacket packet)
        {
            int output_size = audio_temp_buffer_size_;
            int c = AVCodecAPI.avcodec_decode_audio2(audio_stream.codec, p_audio_buffer,
                        ref output_size, packet.data, packet.size);
            if (c < 0 || c != packet.size)
            {
                throw new AVCodecException();
            }
            if (output_size > 0)
            {
                int audio_byte = DtsToAudioByte((int)packet.dts);
                // output_size 単位にまるめる
                audio_byte = ((audio_byte + output_size / 2) / output_size) * output_size;

                audio_buffer_manager_.SetAudioData(p_audio_buffer, output_size, audio_byte);
            }
        }

        private void SeekInner(int frame)
        {
            int frame_block_number = GetBlockNumber(frame);
            int current_block_number = GetBlockNumber(current_frame_);

            if (frame_block_number == current_block_number && current_frame_ <= frame) // シークする必要なし
            {
                return;
            }

            if (frame_block_number > 0)
            {
                long dts = key_dts_list_[frame_block_number - 1];
                AVCodecAPI.av_seek_frame(p_avformat_context, 0, dts + 1, 0);
                current_frame_ = DtsToVideoFrame(key_dts_list_[frame_block_number]);
            }
            else
            {
                AVCodecAPI.av_seek_frame(p_avformat_context, 0, 0, 0);
                current_frame_ = 0;
            }
            av_packet_queue_.Clear(); // パケットをフラッシュする

            audio_priority_times_ = 10; // シークした直後は音声優先
        }

        private class VideoBufferManager
        {
            private AVCodecManager parent_;

            private VideoBufferList video_buffer_list_;
            private Stack<BufferContainer> empty_buffer_stack_ = new Stack<BufferContainer>();

            private int buffering_frame_limit_;
            private int current_using_frame_num_ = 0;
            private int start_frame_;

            public VideoBufferManager(AVCodecManager parent, int frame_length, int buffering_frame_limit, int start_frame)
            {
                parent_ = parent;
                video_buffer_list_ = new VideoBufferList(frame_length);
                buffering_frame_limit_ = buffering_frame_limit;
                start_frame_ = start_frame;
            }

            public bool[] FrameExistsList
            {
                get
                {
                    return video_buffer_list_.FrameExistsList;
                }
            }

            public int BufferingFrameNum
            {
                get { return buffering_frame_limit_; }
            }

            public bool IsExists(int frame)
            {
                if (frame >= parent_.FrameLength)
                {
                    frame = parent_.FrameLength - 1;
                }
                return video_buffer_list_[frame] != null;
            }

            public void Close()
            {
                for (int i = 0; i < video_buffer_list_.Length; ++i)
                {
                    if (video_buffer_list_[i] != null)
                    {
                        video_buffer_list_[i].Free();
                        video_buffer_list_[i] = null;
                    }
                }
                video_buffer_list_ = null;

                foreach (BufferContainer buffer in empty_buffer_stack_)
                {
                    buffer.Free();
                }
                empty_buffer_stack_.Clear();
                empty_buffer_stack_ = new Stack<BufferContainer>();

                current_using_frame_num_ = 0;
                parent_.last_requested_frame_ = 0;
            }

            public BufferContainer GetEmptyBufferContainer(int frame)
            {
                if (current_using_frame_num_ < buffering_frame_limit_)
                {
                    BufferContainer buffer = null;
                    try
                    {
                        buffer = new BufferContainer(parent_.VideoPictureBufferSize);
                    }
                    catch (OutOfMemoryException)
                    {
                        buffering_frame_limit_ = current_using_frame_num_; // ここで打ち止め
                    }
                    if (buffer != null)
                    {
                        ++current_using_frame_num_;
                        return buffer;
                    }
                }
                
                if (empty_buffer_stack_.Count > 0)
                {
                    return empty_buffer_stack_.Pop();
                }
                else
                {
                    int empty_index = GetEmptyIndex(frame);
                    BufferContainer buffer = video_buffer_list_[empty_index];
                    video_buffer_list_[empty_index] = null;
                    return buffer;
                }
            }

            private int GetEmptyIndex(int frame)
            {
                for (int i = video_buffer_list_.Length - 1; i >= parent_.last_requested_frame_ + buffering_frame_limit_; --i)
                {
                    if (video_buffer_list_[i] != null)
                    {
                        return i;
                    }
                }
                for (int i = 0; i < parent_.last_requested_frame_ - buffering_frame_limit_ / 2; ++i)
                {
                    if (video_buffer_list_[i] != null)
                    {
                        return i;
                    }
                }
                for (int i = Math.Min(parent_.last_requested_frame_ + buffering_frame_limit_, parent_.FrameLength - 1);
                    i >= parent_.last_requested_frame_ + buffering_frame_limit_ / 2; --i)
                {
                    if (video_buffer_list_[i] != null)
                    {
                        return i;
                    }
                }
                for (int i = Math.Max(frame - buffering_frame_limit_ / 2, 0); i < frame + buffering_frame_limit_ / 2; ++i)
                {
                    if (video_buffer_list_[i] != null)
                    {
                        return i;
                    }
                }
                throw new AVCodecException();
            }

            public void SetBufferContainer(BufferContainer buffer)
            {
                if (buffer.Frame < 0 || buffer.Frame >= parent_.FrameLength)
                {
                    empty_buffer_stack_.Push(buffer);
                }
                else if (video_buffer_list_[buffer.Frame] != null)
                {
                    empty_buffer_stack_.Push(buffer);
                }
                else
                {
                    video_buffer_list_[buffer.Frame] = buffer;
                }
            }

            public BufferContainer GetFrame(int frame)
            {
                if (frame >= parent_.FrameLength)
                {
                    frame = parent_.FrameLength - 1;
                }
                if (frame < start_frame_)
                {
                    frame = start_frame_;
                }

                return video_buffer_list_[frame];
            }

            public bool IsPreparedVideo(int frame)
            {
                if (frame >= parent_.FrameLength)
                {
                    frame = parent_.FrameLength - 1;
                }
                if (frame < start_frame_)
                {
                    frame = start_frame_;
                }

                if (video_buffer_list_[frame] != null)
                {
                    return true;
                }
                else
                {
                    if (frame > 0)
                    {
                        if (video_buffer_list_[frame - 1] == null)
                        {
                            return false;
                        }
                    }
                    else // 0 フレーム目の場合は false
                    {
                        return false;
                    }
                    if (frame < parent_.FrameLength - 1)
                    {
                        if (video_buffer_list_[frame + 1] == null)
                        {
                            return false;
                        }
                    }
                    else // 最終フレームの場合は false
                    {
                        return false;
                    }
                    return true; // 両端が null でないなら true にする
                }
            }

            private class VideoBufferList
            {
                private BufferContainer[] video_buffer_list_;
                private bool[] frame_exists_list_;

                public VideoBufferList(int length)
                {
                    video_buffer_list_ = new BufferContainer[length];
                    frame_exists_list_ = new bool[length];
                }

                public int Length
                {
                    get { return video_buffer_list_.Length; }
                }

                public BufferContainer this[int index]
                {
                    get { return video_buffer_list_[index]; }
                    set
                    {
                        frame_exists_list_[index] = (value != null);
                        video_buffer_list_[index] = value;
                    }
                }

                public bool[] FrameExistsList
                {
                    get { return frame_exists_list_; }
                }
            }
        }

        private class AudioBufferManager
        {
            private const int buffer_length_ = 1024 * 1024;
            private List<byte[]> audio_buffer_list_ = new List<byte[]>();
            private IntervalSet interval_set_ = new IntervalSet();

            private long data_length_ = 0;

            public AudioBufferManager()
            {
                lock (interval_set_)
                {
                    interval_set_.Add(new Interval(0, 10000));
                }
            }

            public void SetDataLength(long data_length)
            {
                data_length_ = data_length;
            }

            public void SetAudioData(IntPtr data, int size, int byte_pos2)
            {
                int len = Math.Min(buffer_length_ - byte_pos2 % buffer_length_, size);
                int i = byte_pos2 / buffer_length_;
                int start_pos = byte_pos2 % buffer_length_;

                int n = size;

                while (n > 0)
                {
                    while (i >= audio_buffer_list_.Count)
                    {
                        audio_buffer_list_.Add(new byte[buffer_length_]);
                    }
                    Marshal.Copy(data, audio_buffer_list_[i], start_pos, len);
                    n -= len;
                    data = new IntPtr(data.ToInt32() + len);
                    len += Math.Min(buffer_length_, n);
                    ++i;
                    start_pos = 0;
                }

                lock (interval_set_)
                {
                    interval_set_.Add(new Interval(byte_pos2, size));
                }
            }

            public bool SupplyAudioData(IntPtr data, ref long pos, int size)
            {
                int len = (int)Math.Min(buffer_length_ - pos % buffer_length_, size);
                int i = (int)(pos / buffer_length_);
                long start_pos = pos % buffer_length_;

                if (pos < data_length_)
                {
                    while (pos + size >= data_length_)
                    {
                        Thread.Sleep(1);
                    }
                    size = (int)(pos + size < data_length_ ? size : data_length_ - pos);
                    int n = size;
                    while (n > 0)
                    {
                        if (i < audio_buffer_list_.Count)
                        {
                            Marshal.Copy(audio_buffer_list_[i], (int)start_pos, data, len);
                        }
                        else
                        {
                            byte[] buff = new byte[start_pos + len]; // 空バッファ
                            Marshal.Copy(buff, (int)start_pos, data, len);
                        }
                        n -= len;
                        data = new IntPtr(data.ToInt32() + len);
                        len = Math.Min(buffer_length_, n);
                        ++i;
                        start_pos = 0;
                    }
                    pos += size;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsFilled(long start, int length)
            {
                lock (interval_set_)
                {
                    return interval_set_.IsIntersectTailWithEnd(new Interval(start, length));
                }
            }

            public void Close()
            {
                audio_buffer_list_.Clear();
            }
        }
    }

    public interface IVideoBufferManager
    {
        BufferContainer GetEmptyBufferContainer(int frame);
        void SetBufferContainer(BufferContainer buffer);
    }

    public interface IAudioBufferManager
    {
        void SetAudioData(IntPtr data, int size, int byte_pos);
    }

    public class BufferContainer
    {
        [DllImport("kernel32.dll")]
        private extern static void GlobalMemoryStatus(ref MemoryStatus buffer);

        public struct MemoryStatus
        {
            public int length;
            public int memory_load;
            public int total_phys;
            public int avail_phys;
            public int total_page_file;
            public int avail_page_file;
            public int total_virtual;
            public int avail_virtual;
        }

        private IntPtr buffer;
        private int time;
        private int frame;

        public BufferContainer()
        {
            buffer = IntPtr.Zero;
        }

        public BufferContainer(int size)
        {
            //if (GetMemorySize() < 300 * 1024 * 1024)
            //{
            //    throw new OutOfMemoryException();
            //}
            buffer = Marshal.AllocHGlobal(size);
        }

        public IntPtr Buffer
        {
            get { return buffer; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public int Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public void Free()
        {
            Marshal.FreeHGlobal(buffer);
            buffer = IntPtr.Zero;
        }

        public void SetBuffer(IntPtr buff)
        {
            buffer = buff;
        }

        public static int GetMemorySize()
        {
            MemoryStatus status = new MemoryStatus();
            status.length = Marshal.SizeOf(status);

            GlobalMemoryStatus(ref status);

            return status.avail_virtual;
        }
    }

    public class PacketContainer
    {
        public IntPtr ptr;
        public AVPacket packet;

        public PacketContainer(IntPtr p)
        {
            PtrToPacket(p);
        }
        
        ~PacketContainer()
        {
            Destruct();
        }

        private void PtrToPacket(IntPtr p)
        {
            packet = (AVPacket)Marshal.PtrToStructure(p, typeof(AVPacket));
        }

        public void Destruct()
        {
            if (ptr != IntPtr.Zero && packet.destruct != IntPtr.Zero)
            {
                AVCodecAPI.av_destruct_packet(ptr);
                packet = null;
                ptr = IntPtr.Zero;
            }
        }
    }

    public class Interval
    {
        public long start;
        public long length;

        public Interval()
        {
            // Nothing
        }

        public Interval(long start, long length)
        {
            this.start = start;
            this.length = length;
        }

        public long end
        {
            get { return start + length; }
            set { length = value - start; }
        }

        public bool IsIntersect(Interval interval)
        {
            return (start < interval.start && interval.start < end) ||
                (start < interval.end && interval.end < end);
        }

        public bool IsIntersectTailWithEnd(Interval interval)
        {
            return (start < interval.end && interval.end <= end);
        }

        public bool IsIntersectWithEnd(Interval interval)
        {
            return (start <= interval.start && interval.start <= end) ||
                (start <= interval.end && interval.end <= end);
        }

        public bool IsIncluded(Interval interval)
        {
            return interval.start <= start && end <= interval.end;
        }
    }

    public class IntervalSet
    {
        private List<Interval> interval_list_ = new List<Interval>();

        public void Add(Interval interval)
        {
            int index;
            for (index = 0; index < interval_list_.Count; ++index)
            {
                if (interval.start <= interval_list_[index].start)
                {
                    break;
                }
            }
            int start_intersect = (index > 0 && interval_list_[index - 1].IsIntersectWithEnd(interval)
                                      ? index - 1 : index);

            for (index = start_intersect; index < interval_list_.Count; ++index)
            {
                if (!interval_list_[index].IsIntersectWithEnd(interval))
                {
                    break;
                }
            }
            int end_intersect = index;
            if (start_intersect < end_intersect)
            {
                Interval new_interval = new Interval();
                new_interval.start = Math.Min(interval_list_[start_intersect].start, interval.start);
                new_interval.end = Math.Max(interval_list_[end_intersect - 1].end, interval.end);

                interval_list_.RemoveRange(start_intersect, end_intersect - start_intersect);
                interval_list_.Insert(start_intersect, new_interval);
            }
            else
            {
                interval_list_.Insert(start_intersect, interval);
            }
        }

        public bool IsIntersect(Interval interval)
        {
            foreach (Interval ival in interval_list_)
            {
                if (ival.IsIntersect(interval))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsIntersectTailWithEnd(Interval interval)
        {
            foreach (Interval ival in interval_list_)
            {
                if (ival.IsIntersectTailWithEnd(interval))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsIncluding(Interval interval)
        {
            foreach (Interval ival in interval_list_)
            {
                if (interval.IsIncluded(ival))
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Command
    {
        public enum Kind { Seek, Wait, EndThread };
        public Kind kind_;
        public int frame_;

        public Command(Kind kind)
        {
            kind_ = kind;
        }

        public Command(Kind kind, int frame)
        {
            kind_ = kind;
            frame_ = frame;
        }
    }

    public class AVCodecException : Exception
    {
        public AVCodecException()
        {

        }

        public AVCodecException(string message)
            : base(message)
        {

        }
    }

    public class AVCodecCannotOpenFileException : AVCodecException
    {
        public AVCodecCannotOpenFileException()
        {

        }

        public AVCodecCannotOpenFileException(string message)
            : base(message)
        {

        }
    }
}
