﻿// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Collections.Generic;
using System.Text;

namespace AVCodec
{
    public interface IDecoder
    {
        int VideoPictureBufferSize {get; }
        int AudioChannel { get; }
        int AudioSampleRate { get; }
        int AudioBytesPerSample { get; }
        int Rate { get; }
        int Scale { get; }
        int FrameLength { get; }
        int Width { get; }
        int Height { get; }
        bool HasVideo { get; }
        bool HasAudio { get; }
        int[] KeyFrameList { get; }
        int CurrentFrame { get; }
        bool[] FrameExistsList { get; }
        bool IsOpen { get; }

        void Open(string filename);
        void Open(string filename, int video_width, int video_height, int memory_size);
        void Close();
        void RequireSeeking(int frame);

        BufferContainer GetFrame(int frame);
        bool SupplyAudioData(IntPtr data, ref long pos, int size);
        bool IsPreparedVideo(int frame);
        bool IsPreparedAudio(long start, int length);
    }
}
