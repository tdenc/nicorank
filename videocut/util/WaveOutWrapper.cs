// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WaveOut
{
    public class WaveOutWrapper
    {
        #region DLL

        public const int MM_WOM_OPEN = 0x3BB;
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;

        [DllImport("winmm.dll", EntryPoint = "waveOutOpen", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutOpen(ref IntPtr phwo, IntPtr uDeviceID, ref WAVEFORMATEX pwfx, WaveCallbackDelegate dwCallback, IntPtr dwInstance, uint fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutPrepareHeader(IntPtr hwo, IntPtr pwh, uint cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveOutWrite", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutWrite(IntPtr hwo, IntPtr pwh, uint cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveOutClose", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutClose(IntPtr hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutReset", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutReset(IntPtr hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutPause", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutPause(IntPtr hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutRestart", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutRestart(IntPtr hwo);

        [DllImport("winmm.dll", EntryPoint = "waveOutSetVolume", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutSetVolume(IntPtr hwo, uint dwVolume);

        [DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern uint waveOutUnprepareHeader(IntPtr hwo, IntPtr pwh, uint cbwh);

        [StructLayout(LayoutKind.Explicit, Size = 18)]
        private struct WAVEFORMATEX
        {
            [FieldOffset(0)]
            public ushort wFormatTag;

            [FieldOffset(2)]
            public ushort nChannels;

            [FieldOffset(4)]
            public uint nSamplesPerSec;

            [FieldOffset(8)]
            public uint nAvgBytesPerSec;

            [FieldOffset(12)]
            public ushort nBlockAlign;

            [FieldOffset(14)]
            public ushort wBitsPerSample;

            [FieldOffset(16)]
            public ushort cbSize;
        }

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        private struct WAVEHDR
        {
            [FieldOffset(0)]
            public IntPtr lpData;

            [FieldOffset(4)]
            public uint dwBufferLength;

            [FieldOffset(8)]
            public uint dwBytesRecorded;

            [FieldOffset(12)]
            public uint dwUser;

            [FieldOffset(16)]
            public uint dwFlags;

            [FieldOffset(20)]
            public uint dwLoops;

            [FieldOffset(24)]
            public IntPtr lpNext;

            [FieldOffset(28)]
            public uint reserved;
        }

        #endregion

        public delegate CallbackKind AudioCallbackDelegate(IntPtr data, int size, object user_object);
        public delegate void StoppedDelegate();
        public delegate void TimeOutDelegate();
        private delegate void WaveCallbackDelegate(IntPtr hwo, uint message, uint instance, uint param1, uint param2);

        /// <summary>
        /// 本クラスの状態を表す
        /// StoppingPlaying : 停止処理中に再度再生要求が発生した状態
        /// </summary>
        public enum StateKind { NotOpened, Preparing, Prepared, Playing, Stopping, StoppingPlaying }

        /// <summary>
        /// AudioCallbackDelegate で使う戻り値
        /// </summary>
        public enum CallbackKind { Play, Wait, Stop }

        /// <summary>
        /// WAVEHDR 構造体のサイズ。本来は sizeof(WAVEHDR) とすべき
        /// </summary>
        private const int WAVEHDR_STRUCT_SIZE_ = 32;

        /// <summary>
        /// 用意するWAVEHDR構造体の個数
        /// </summary>
        private const int WAVEHDR_NUM_ = 2;

        /// <summary>
        /// 再生するPCMの周波数。ユーザがOpen関数の引数で指定する
        /// </summary>
        private int sample_rate_;
        /// <summary>
        /// 再生するPCMのチャンネル数。ユーザがOpen関数の引数で指定する
        /// </summary>
        private int channel_number_;
        /// <summary>
        /// 1サンプル辺りのビット数。ユーザがOpen関数の引数で指定する
        /// </summary>
        private int bits_per_sample_;
        /// <summary>
        /// WAVEHDR構造体のlpDataで確保するバッファの大きさ。ユーザがOpen関数の引数で指定する
        /// </summary>
        private int buffer_length_;

        /// <summary>
        /// AudioCallbackの引数に渡されるユーザオブジェクト。ユーザがプロパティで設定する。
        /// </summary>
        private object user_object_ = null;

        /// <summary>
        /// WaveManager の状態
        /// </summary>
        private volatile StateKind state_ = StateKind.NotOpened;

        /// <summary>
        /// 状態を変えるときにかけるロックのためのオブジェクト
        /// </summary>
        private object changing_state_lock_ = new object();
        
        /// <summary>
        /// waveOutWrite で書き込んだデータの再生が終了したときに呼び出されるコールバック
        /// </summary>
        private WaveCallbackDelegate dlg_;

        /// <summary>
        /// HWAVEOUT
        /// </summary>
        private IntPtr h_wave_out_;

        /// <summary>
        /// WAVEHDR 構造体へのポインタ。
        /// WAVEHDR 構造体は（ガベージコレクタが勝手に場所を動かさないように） Marshal.AllocHGlobal で確保
        /// p_wave_hdr_list_[i] の指す WAVEHDR 構造体は、wave_hdr_list_[i] そのものではない
        /// </summary>
        private IntPtr[] p_wave_hdr_list_ = new IntPtr[WAVEHDR_NUM_];

        /// <summary>
        /// WAVEHDR 構造体のデータのコピー。p_wave_hdr_list_ のコメント参照。
        /// Marshal.AllocHGlobal で確保した構造体を、Marshal.PtrToStructure でマネージドオブジェクトに
        /// 変換したもの。wave_hdr_list_[i] 自体をAPIに渡すわけではなく、単に p_wave_hdr_list_[i] の
        /// メンバ変数（例えばlpData）を取得するときだけに使う。
        /// </summary>
        private WAVEHDR[] wave_hdr_list_ = new WAVEHDR[WAVEHDR_NUM_];

        /// <summary>
        /// ループスレッドが存在するなら true
        /// </summary>
        private bool is_thread_alive_ = false;

        /// <summary>
        /// APIからのコールバックの完了などを管理するクラス
        /// </summary>
        private WaitingManager waiting_manager_ = new WaitingManager(WAVEHDR_NUM_);

        /// <summary>
        /// ループスレッドの終了を待つためのイベント
        /// </summary>
        private AutoResetEvent close_event_ = new AutoResetEvent(false);

        /// <summary>
        /// Stop() が呼び出される回数（OnStopを呼び出す回数）
        /// </summary>
        private int stopping_times_ = 0;

        /// <summary>
        /// 再生データ要求のときに呼び出されるコールバック。ユーザがプロパティで設定する。
        /// </summary>
        private AudioCallbackDelegate audio_callback = null;

        /// <summary>
        /// 停止するときに呼び出されるコールバック。ユーザがプロパティで設定する。
        /// </summary>
        private StoppedDelegate OnStopped = null;

        /// <summary>
        /// データが供給されずタイムアウトが発生したときに呼び出されるコールバック。
        /// ユーザがプロパティで設定する。
        /// </summary>
        private TimeOutDelegate OnTimeOut = null;

        ~WaveOutWrapper()
        {
            if (state_ != StateKind.NotOpened)
            {
                Close();
            }
        }

        public StateKind State
        {
            get { return state_; }
        }

        public int BufferLength
        {
            get { return buffer_length_; }
        }

        public AudioCallbackDelegate AudioCallback
        {
            get { return audio_callback; }
            set { audio_callback = value; }
        }

        public StoppedDelegate Stopped
        {
            get { return OnStopped; }
            set { OnStopped = value; }
        }

        public TimeOutDelegate TimeOut
        {
            get { return OnTimeOut; }
            set { OnTimeOut = value; }
        }

        public object UserObject
        {
            get { return user_object_; }
            set { user_object_ = value; }
        }

        /// <summary>
        /// オープン状態にする
        /// </summary>
        /// <param name="sample_rate">サンプルレート（44100等）</param>
        /// <param name="channels">チャンネル数（2なら普通のステレオ）</param>
        /// <param name="bits_per_sample">サンプルあたりのビット数（通常は8か16）</param>
        public void Open(int sample_rate, int channels, int bits_per_sample)
        {
            Open(sample_rate, channels, bits_per_sample, sample_rate * channels * bits_per_sample / 8 / 4);
        }

        /// <summary>
        /// オープン状態にする
        /// </summary>
        /// <param name="sample_rate">サンプルレート（44100等）</param>
        /// <param name="channels">チャンネル数（2なら普通のステレオ）</param>
        /// <param name="bits_per_sample">サンプルあたりのビット数（通常は8か16）</param>
        /// <param name="buffer_length">WAVEHDR の lpData で確保するバッファの大きさ</param>
        public void Open(int sample_rate, int channels, int bits_per_sample, int buffer_length)
        {
            lock (changing_state_lock_)
            {
                if (state_ != StateKind.NotOpened)
                {
                    throw new WaveOutStateIllegalException(state_);
                }
                state_ = StateKind.Preparing;
            }
            System.Diagnostics.Debug.Write("WaveManager.Open");

            if (bits_per_sample != 8 && bits_per_sample != 16)
            {
                throw new ArgumentOutOfRangeException("bit per sample は8か16である必要があります");
            }

            sample_rate_ = sample_rate;
            channel_number_ = channels;
            bits_per_sample_ = bits_per_sample;
            buffer_length_ = buffer_length;

            waiting_manager_.Initialize();

            WaveOutOpen();
        }

        /// <summary>
        /// クローズ状態にする
        /// </summary>
        public void Close()
        {
            if (state_ == StateKind.Playing)
            {
                Stop();
                while (state_ != StateKind.Prepared)
                {
                    Thread.Sleep(1);
                }
            }
            for (int i = 0; i < WAVEHDR_NUM_; ++i)
            {
                Marshal.FreeHGlobal(wave_hdr_list_[i].lpData);
            }
            uint error_code = waveOutClose(h_wave_out_);
            if (error_code != 0)
            {
                throw new WaveOutApiException("waveOutClose", error_code);
            }
            // waveOutClose を呼び出して、コールバックが帰ってきて後処理が完了するまで待つ
            close_event_.WaitOne();
            audio_callback = null;
            OnStopped = null;
            stopping_times_ = 0;
            state_ = StateKind.NotOpened;
        }

        /// <summary>
        /// 音声の再生を開始する。このメソッドを呼ぶと AudioCallback が定期的に呼び出されるようになる
        /// </summary>
        public void Play()
        {
            lock (changing_state_lock_)
            {
                // 準備中か準備完了か停止処理中ならスレッド起動（準備中なら起動したスレッド内で待つ）
                if (state_ == StateKind.Preparing || state_ == StateKind.Prepared)
                {
                    if (audio_callback == null)
                    {
                        throw new WaveOutAudioCallbackException();
                    }
                    if (!is_thread_alive_)
                    {
                        is_thread_alive_ = true;
                        Thread t = new Thread(new ThreadStart(Run));
                        t.IsBackground = true;
                        t.Start();
                    }
                }
                else if (state_ == StateKind.Stopping)
                {
                    state_ = StateKind.StoppingPlaying;
                }
                else
                {
                    throw new WaveOutStateIllegalException(state_);
                }
            }
        }

        /// <summary>
        /// waveOutPause API を呼び出す。使用非推奨
        /// </summary>
        public void Pause()
        {
            uint error_code = waveOutPause(h_wave_out_);
            if (error_code != 0)
            {
                throw new WaveOutApiException("waveOutPause", error_code);
            }
        }

        /// <summary>
        /// waveOutRestart API を呼び出す。使用非推奨
        /// </summary>
        public void Restart()
        {
            uint error_code = waveOutRestart(h_wave_out_);
            if (error_code != 0)
            {
                throw new WaveOutApiException("waveOutRestart", error_code);
            }
        }

        /// <summary>
        /// 音量を設定する（waveOutSetVolume API を呼び出す）
        /// </summary>
        /// <param name="left">左の音量（0～65535）</param>
        /// <param name="right">右の音量（0～65535）</param>
        public void SetVolume(ushort left, ushort right)
        {
            uint error_code = waveOutSetVolume(h_wave_out_, (uint)(left | (right << 16)));
            if (error_code != 0)
            {
                throw new WaveOutApiException("waveOutSetVolume", error_code);
            }
        }

        /// <summary>
        /// 再生していた音声をストップする
        /// </summary>
        public void Stop()
        {
            bool is_call_reset = false;

            lock (changing_state_lock_)
            {
                if (state_ == StateKind.Playing)
                {
                    state_ = StateKind.Stopping;
                    is_call_reset = true;
                }
                else if (state_ == StateKind.StoppingPlaying)
                {
                    state_ = StateKind.Stopping;
                }
                else
                {
                    throw new WaveOutStateIllegalException(state_);
                }
            }

            ++stopping_times_;

            if (is_call_reset)
            {
                System.Diagnostics.Debug.Write("call waveOutReset");
                uint error_code = waveOutReset(h_wave_out_);
                if (error_code != 0)
                {
                    throw new WaveOutApiException("waveOutReset", error_code);
                }
            }
        }

        private void WaveCallbackProc(IntPtr h_wave_out, uint message, uint instance, uint param1, uint param2)
        {
            System.Diagnostics.Debug.Write("called WaveCallbackProc : " + message + ", " + param1 + ", " + param2);

            switch (message)
            {
                case MM_WOM_OPEN:
                    // waveOutPrepareHeader をこのスレッドで呼んではいけないので、
                    // 別スレッドを作って呼び出す。
                    Thread t = new Thread(new ThreadStart(Prepare));
                    t.IsBackground = true;
                    t.Start();
                    break;
                case MM_WOM_CLOSE:
                    for (int i = 0; i < WAVEHDR_NUM_; ++i)
                    {
                        waveOutUnprepareHeader(h_wave_out_, p_wave_hdr_list_[i], WAVEHDR_STRUCT_SIZE_);
                        Marshal.FreeHGlobal(p_wave_hdr_list_[i]);
                    }
                    close_event_.Set(); // 後処理完了を通知
                    break;
                case MM_WOM_DONE:
                    IntPtr ptr = new IntPtr(param1);
                    WAVEHDR wave_hdr = (WAVEHDR)Marshal.PtrToStructure(ptr, typeof(WAVEHDR));
                    int index = (int)wave_hdr.dwUser; // 何番目の WAVEHDR 構造体かを取得
                    waiting_manager_.Set(index); // 再生完了のシグナル
                    break;
            }
        }

        private void Prepare()
        {
            System.Diagnostics.Debug.Write("call waveOutPrepareHeader");

            Thread.Sleep(100); // これがないとなぜか落ちる

            for (int i = 0; i < WAVEHDR_NUM_; ++i)
            {
                wave_hdr_list_[i] = new WAVEHDR();

                wave_hdr_list_[i].lpData = Marshal.AllocHGlobal(buffer_length_);
                wave_hdr_list_[i].dwBufferLength = (uint)buffer_length_;
                wave_hdr_list_[i].dwBytesRecorded = 0;
                wave_hdr_list_[i].dwUser = (uint)i;
                wave_hdr_list_[i].dwFlags = 0;
                wave_hdr_list_[i].dwLoops = 1;
                wave_hdr_list_[i].lpNext = (IntPtr)0;
                wave_hdr_list_[i].reserved = 0;

                // アンマネージドオブジェクト（WAVEHDR）を確保
                p_wave_hdr_list_[i] = Marshal.AllocHGlobal(WAVEHDR_STRUCT_SIZE_);
                Marshal.StructureToPtr(wave_hdr_list_[i], p_wave_hdr_list_[i], false);

                uint error_code = waveOutPrepareHeader(h_wave_out_, p_wave_hdr_list_[i], WAVEHDR_STRUCT_SIZE_);
                if (error_code != 0)
                {
                    throw new WaveOutApiException("waveOutPrepareHeader " + h_wave_out_.ToInt32().ToString(), error_code);
                }
            }

            state_ = StateKind.Prepared;
        }

        private void WaveOutOpen()
        {
            // WAVEデバイス設定
            WAVEFORMATEX waveformatex = new WAVEFORMATEX();
            waveformatex.wFormatTag = 0x0001; // PCM
            waveformatex.nChannels = (ushort)channel_number_;
            waveformatex.nSamplesPerSec = (uint)sample_rate_;
            waveformatex.wBitsPerSample = (ushort)(bits_per_sample_);
            waveformatex.nBlockAlign = (ushort)(waveformatex.nChannels * waveformatex.wBitsPerSample / 8);
            waveformatex.nAvgBytesPerSec = waveformatex.nSamplesPerSec * waveformatex.nBlockAlign;
            waveformatex.cbSize = 0;

            h_wave_out_ = new IntPtr();
            dlg_ = new WaveCallbackDelegate(WaveCallbackProc);

            System.Diagnostics.Debug.Write("call waveOutOpen");
            uint error_code = waveOutOpen(ref h_wave_out_, (IntPtr)0x0000, ref waveformatex, dlg_, (IntPtr)0, 0x00030000);
            if (error_code != 0)
            {
                throw new WaveOutApiException("waveOutOpen", error_code);
            }
        }

        // ループスレッド
        private void Run()
        {
            System.Diagnostics.Debug.Write("WaveManager Thread Start : " + Thread.CurrentThread.ManagedThreadId);
            while (state_ == StateKind.Preparing)
            {
                Thread.Sleep(1);
            }
            lock (changing_state_lock_)
            {
                if (state_ == StateKind.Prepared)
                {
                    state_ = StateKind.Playing;
                }
            }

            while (true)
            {
                System.Diagnostics.Debug.Write("Playing");

                int proceeding_kind;

                for (int written_data_num = 0; ; ++written_data_num)
                {
                    int index = written_data_num % WAVEHDR_NUM_;

                    System.Diagnostics.Debug.Write("Wait : index = " + index + ", state = " + state_);
                    waiting_manager_.WaitOne(index);
                    System.Diagnostics.Debug.Write("Wake up : index = " + index + ", state = " + state_);

                    proceeding_kind = ProceedWrite(index);

                    if (proceeding_kind != 1)
                    {
                        if (proceeding_kind == 4) // 再生終了まで待つ
                        {
                            waiting_manager_.Set(index); // 自分自身は待つ必要がない
                            waiting_manager_.WaitAll();
                        }
                        break;
                    }
                }
                System.Diagnostics.Debug.Write("Exit while loop in WaveManager Thread");

                waiting_manager_.SetAll(); // 次回の再生にそなえて準備

                lock (changing_state_lock_)
                {
                    if (state_ == StateKind.Playing)
                    {
                        state_ = StateKind.Stopping;
                    }
                }
                if (OnStopped != null)
                {
                    for (; stopping_times_ > 0; --stopping_times_)
                    {
                        OnStopped();
                    }
                }
                if (proceeding_kind == 3)
                {
                    if (OnTimeOut != null)
                    {
                        OnTimeOut();
                    }
                }

                lock (changing_state_lock_)
                {
                    if (state_ == StateKind.StoppingPlaying) // 再度再生する
                    {
                        state_ = StateKind.Playing;
                    }
                    else // 終了
                    {
                        is_thread_alive_ = false;
                        state_ = StateKind.Prepared;
                        System.Diagnostics.Debug.Write("Thread End : " + Thread.CurrentThread.ManagedThreadId);
                        return;
                    }
                }
            }
        }

        // 返り値 1: Write 成功、2: 状態が Stopping または StoppingPlaying に、3: タイムアウト、4: 再生終了
        private int ProceedWrite(int index)
        {
            while (state_ == StateKind.Playing)
            {
                CallbackKind callback_kind = audio_callback(wave_hdr_list_[index].lpData, buffer_length_, user_object_);
                if (state_ == StateKind.Stopping || state_ == StateKind.StoppingPlaying)
                {
                    return 2;
                }
                if (callback_kind == CallbackKind.Play)
                {
                    uint error_code = waveOutWrite(h_wave_out_, p_wave_hdr_list_[index], WAVEHDR_STRUCT_SIZE_);
                    if (error_code != 0)
                    {
                        throw new WaveOutApiException("waveOutWrite", error_code);
                    }
                    return 1;
                }
                else if (callback_kind == CallbackKind.Wait)
                {
                    if (waiting_manager_.IsSettingAllExcept(index)) // 自分以外すべての再生が終了したなら
                    {
                        return 3;
                    }
                    Thread.Sleep(1);
                }
                else // callback_kind == CallbackKind.Stop
                {
                    ++stopping_times_;
                    return 4;
                }
            }
            return 2;
        }

        private class WaitingManager
        {
            /// <summary>
            /// WAVEHDRが使用中かどうかを表すAutoResetEventオブジェクト。Unset 状態なら「waveOutWrite呼び出し後～再生完了前」
            /// ということを表す。
            /// </summary>
            private AutoResetEvent[] wait_object_list_;
            /// <summary>
            /// AutoResetEvent に set 状態かどうか調べるメンバがないので、別管理
            /// </summary>
            private bool[] wait_state_;

            public WaitingManager(int num)
            {
                wait_object_list_ = new AutoResetEvent[num];
                wait_state_ = new bool[num];
            }

            public void Initialize()
            {
                for (int i = 0; i < wait_object_list_.Length; ++i)
                {
                    wait_object_list_[i] = new AutoResetEvent(true);
                    wait_state_[i] = true;
                }
            }

            public void Set(int index)
            {
                wait_object_list_[index].Set();
                wait_state_[index] = true;
            }

            public void SetAll()
            {
                for (int i = 0; i < wait_object_list_.Length; ++i)
                {
                    wait_object_list_[i].Set();
                    wait_state_[i] = true;
                }
            }

            public bool IsSettingAll()
            {
                for (int i = 0; i < wait_object_list_.Length; ++i)
                {
                    if (!wait_state_[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            // index 以外のすべてが set 状態か
            public bool IsSettingAllExcept(int index)
            {
                for (int i = 0; i < wait_object_list_.Length; ++i)
                {
                    if (i == index)
                    {
                        continue;
                    }
                    if (!wait_state_[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public void WaitOne(int index)
            {
                wait_object_list_[index].WaitOne();
                wait_state_[index] = false;
            }

            public void WaitAll()
            {
                WaitHandle.WaitAll(wait_object_list_);
            }
        }
    }

    public class WaveOutStateIllegalException : Exception
    {
        WaveOutWrapper.StateKind current_state_;

        public WaveOutStateIllegalException(WaveOutWrapper.StateKind current_state)
        {
            current_state_ = current_state;
        }
    }

    public class WaveOutApiException : Exception
    {
        string api_name_;
        uint error_code_;

        public WaveOutApiException(string api_name, uint error_code)
            : base(api_name + " の呼び出しでエラーが起こりました。エラーコード: " + error_code)
        {
            api_name_ = api_name;
            error_code_ = error_code;
        }
    }

    public class WaveOutAudioCallbackException : Exception
    {
        
    }
}
