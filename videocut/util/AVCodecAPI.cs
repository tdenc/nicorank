using System;
using System.Runtime.InteropServices;

namespace AVCodec
{
    public class AVCodecAPI
    {
        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static void av_register_all();

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static int av_open_input_file(out IntPtr ic_ptr, string filename, IntPtr fmt, int buf_size, IntPtr ap);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static void av_close_input_file(IntPtr s);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static int av_find_stream_info(IntPtr s);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static int av_seek_frame(IntPtr s, int stream_index, long timestamp, int flags);

        [DllImport(@"..\ffmpeg\avutil-50.dll")]
        public extern static IntPtr av_malloc(int size);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static IntPtr avcodec_find_decoder(int codec_id);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avcodec_open(IntPtr avctx, IntPtr codec);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avcodec_close(IntPtr avctx);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static IntPtr avcodec_alloc_frame();

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avpicture_get_size(int pix_fmt, int width, int height);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avpicture_fill(IntPtr picture, IntPtr ptr,
                   int pix_fmt, int width, int height);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static int av_read_frame(IntPtr s, IntPtr pkt);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static void av_free_packet(IntPtr pkt);

        [DllImport(@"..\ffmpeg\avformat-52.dll")]
        public extern static void av_destruct_packet(IntPtr pkt);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avcodec_decode_video(IntPtr avctx, IntPtr picture,
                         out int got_picture_ptr,
                         IntPtr buf, int buf_size);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int avcodec_decode_audio2(IntPtr avctx, IntPtr samples,
            ref int frame_size_ptr, IntPtr buf, int buf_size);

        [DllImport(@"..\ffmpeg\avcodec-52.dll")]
        public extern static int vp56_decode_frame(IntPtr avctx, AVFrame picture,
                         ref int got_picture_ptr,
                         IntPtr buf, int buf_size);

        [DllImport(@"..\ffmpeg\swscale-0.dll")]
        public extern static IntPtr sws_getContext(int srcW, int srcH, int srcFormat, int dstW, int dstH, int dstFormat, int flags,
                                  IntPtr srcFilter, IntPtr dstFilter, ref double param);

        [DllImport(@"..\ffmpeg\swscale-0.dll")]
        public extern static int sws_scale(IntPtr context, IntPtr srcSlice, IntPtr srcStride, int srcSliceY,
              int srcSliceH, IntPtr dst, IntPtr dstStride);

        [DllImport(@"..\ffmpeg\swscale-0.dll")]
        public extern static void sws_freeContext(IntPtr swsContext);

        public enum AVPixelFormat
        {
            PIX_FMT_YUV420P, PIX_FMT_YUYV422, PIX_FMT_RGB24, PIX_FMT_BGR24
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AVRational
    {
        public int num;
        public int den;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AVFrac
    {
        public long val, num, den;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVPacket
    {
        public long pts;
        public long dts;
        public IntPtr data;
        public int size;
        public int stream_index;
        public int flags;
        public int duration;
        public IntPtr destruct;
        public IntPtr priv;
        public long pos;
        public long convergence_duration;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVStream
    {
        public int index;
        public int id;
        public IntPtr codec;
        public AVRational r_frame_rate;
        public IntPtr priv_data;
        public long first_dts;
        public AVFrac pts;
        public AVRational time_base;
        public int pts_wrap_bits;
        public int stream_copy;
        public int discard;
        float quality;
        public long start_time;
        public long duration;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] language;
        public int need_parsing;
        public IntPtr parser;
        public long cur_dts;
        public int last_IP_duration;
        public long last_IP_pts;
        public IntPtr index_entries;
        public int nb_index_entries;
        public uint index_entries_allocated_size;
        public long nb_frames;
        public long unused;
        public IntPtr filename;
        public int disposition;
        public IntPtr probe_data;
        public long pts_buffer;
        public IntPtr sample_aspect_ratio;
        public IntPtr metadata;
        public IntPtr cur_ptr;
        public int cur_len;
        public IntPtr cur_pkt;
        public long reference_dts;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVCodecContext
    {
        public IntPtr av_class;
        public int bit_rate;
        public int bit_rate_tolerance;
        public int flags;
        public int sub_id;
        public int me_method;
        public IntPtr extradata;
        public int extradata_size;
        public int time_base_num;
        public int time_base_den;
        public int width, height;
        public int gop_size;
        public int pix_fmt;
        public int rate_emu;
        public IntPtr draw_horiz_band;
        public int sample_rate;
        public int channels;
        public int sample_fmt;
        public int frame_size;
        public int frame_number;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] dummy3;
        public IntPtr codec;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 84)]
        public byte[] dummy2;
        public int codec_type;
        public int codec_id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 644)]
        public byte[] dummy;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVCodec
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 68)]
        public byte[] dummy;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVFrame
    {
        public IntPtr data0, data1, data2, data3;
        public int linesize0, linesize1, linesize2, linesize3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 176)]
        public byte[] dummy;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AVFormatContext
    {
        public IntPtr av_class;

        public IntPtr iformat;
        public IntPtr oformat;
        public IntPtr priv_data;
        public IntPtr pb;
        public uint nb_streams;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public IntPtr[] streams;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        char[] filename;

        public long timestamp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        char[] title;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        char[] author;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        char[] copyright;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        char[] comment;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        char[] album;
        public int year;
        public int track;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        char[] genre;

        public int ctx_flags;
        public IntPtr packet_buffer;
        public long start_time;
        public long duration;
        public long file_size;
        public int bit_rate;
        public IntPtr cur_st;
        public IntPtr cur_ptr_deprecated;
        public int cur_len_deprecated;
        public AVPacket cur_pkt_deprecated;
        public long data_offset;
        public int index_built;
        public int mux_rate;
        public int packet_size;
        public int preload;
        public int max_delay;
        public int loop_output;
        public int flags;
        public int loop_input;
        public uint probesize;
        public int max_analyze_duration;
        public IntPtr key;
        public int keylen;
        public uint nb_programs;
        public IntPtr programs;
        public int video_codec_id;
        public int audio_codec_id;
        public int subtitle_codec_id;
        public uint max_index_size;
        public uint max_picture_buffer;
        public uint nb_chapters;
        public IntPtr chapters;
        public int debug;
        public IntPtr raw_packet_buffer;
        public IntPtr raw_packet_buffer_end;
        public IntPtr packet_buffer_end;
        public IntPtr metadata;
    }
}
