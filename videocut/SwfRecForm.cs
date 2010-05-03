using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using IJLib.Vfw;
using System.IO;

namespace videocut
{
    public partial class SwfRecForm : Form
    {
        private MainForm main_form_;
        private double frame_rate_;
        private double start_sec_;
        private double duration_;
        private string output_avi_filename_;
        private int output_width_;
        private int output_height_;

        private int swf_width_;
        private int swf_height_;
        private volatile bool is_stopping_ = false;
        private List<Bitmap> bmp_list_ = new List<Bitmap>();
        private Point flash_left_top_;
        private int end_frame_;
        private string old_title_;

        public SwfRecForm()
        {
            InitializeComponent();
        }

        public void SetForm(MainForm form)
        {
            main_form_ = form;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            output_avi_filename_ = selectFileBoxOutputFileName.FileName;
            if (output_avi_filename_ == "")
            {
                MessageBox.Show(this, "ファイル名が空です。",
                    "videocut", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Path.GetExtension(output_avi_filename_).ToLower() != ".avi")
            {
                MessageBox.Show(this, "ファイル名の拡張子は .avi にする必要があります。",
                    "videocut", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {

                    frame_rate_ = double.Parse(textBoxFramePerSec.Text);
                    start_sec_ = double.Parse(textBoxStartSec.Text);
                    duration_ = double.Parse(textBoxDurationSec.Text);
                    end_frame_ = main_form_.GetSwfTotalFrames();
                    swf_width_ = main_form_.GetSwfWidth();
                    swf_height_ = main_form_.GetSwfHeight();
                    output_width_ = int.Parse(textBoxOutputWidth.Text);
                    output_height_ = int.Parse(textBoxOutputHeight.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show(this, "入力された値が不正です。",
                        "videocut", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                flash_left_top_ = main_form_.GetSwfLeftPoint();
                bmp_list_.Clear();
                is_stopping_ = false;

                old_title_ = main_form_.GetTitle();
                main_form_.SetTitle("録画中です。ウィンドウを動かさないでください。");
                main_form_.Play();

                Thread t = new Thread(new ThreadStart(RecThread));
                t.IsBackground = true;
                t.Start();
            }
        }

        public void SetToConfig(VideoCutConfig config)
        {
            config.OutputAviFileName = selectFileBoxOutputFileName.FileName;
            config.OutputAviSize = new Size(int.Parse(textBoxOutputWidth.Text), int.Parse(textBoxOutputHeight.Text));
            config.FramePerSec = double.Parse(textBoxFramePerSec.Text);
            config.StartSec = double.Parse(textBoxStartSec.Text);
            config.Duration = double.Parse(textBoxDurationSec.Text);
        }

        public void LoadFromConfig(VideoCutConfig config)
        {
            selectFileBoxOutputFileName.FileName = config.OutputAviFileName;
            textBoxOutputWidth.Text = config.OutputAviSize.Width.ToString();
            textBoxOutputHeight.Text = config.OutputAviSize.Height.ToString();
            textBoxFramePerSec.Text = config.FramePerSec.ToString();
            textBoxStartSec.Text = config.StartSec.ToString();
            textBoxDurationSec.Text = config.Duration.ToString();
        }

        public void AppendInfo(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(AppendInfo), new object[] { text });
            }
            else
            {
                textBoxInfo.AppendText(text);
            }
        }

        private void RecThread()
        {
            bool is_first_time = true;
            AppendInfo("録画待機\r\n");
            int last_reced_frame = -1;
            int start = System.Environment.TickCount + (int)(start_sec_ * 1000.0);
            int counter = 0;

            using (Bitmap temp_bmp = new Bitmap(swf_width_, swf_height_))
            using (Graphics temp_graphics = Graphics.FromImage(temp_bmp))
            {
                while (!is_stopping_)
                {
                    int time = Environment.TickCount - start;
                    if (is_first_time) // start_sec_ 秒だけ飛ばす
                    {
                        if (time >= 0)
                        {
                            is_first_time = false;
                            AppendInfo("録画開始\r\n");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (time * frame_rate_ >= (last_reced_frame + 1) * 1000.0)
                    {
                        if (time >= duration_ * 1000) // duration_ 秒を超えると終了
                        {
                            break;
                        }
                        if (time > counter)
                        {
                            AppendInfo(".");
                            counter += 1000;
                        }

                        int frame = (int)(time * frame_rate_ / 1000.0);
                        temp_graphics.CopyFromScreen(flash_left_top_, new Point(0, 0), new Size(swf_width_, swf_height_));
                        Bitmap bmp = new Bitmap(output_width_, output_height_, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(temp_bmp, new Rectangle(0, 0, output_width_, output_height_), new Rectangle(0, 0, swf_width_, swf_height_), GraphicsUnit.Pixel);
                        }
                        while (frame - last_reced_frame > 1)
                        {
                            bmp_list_.Add(null); // コマ落ち
                            ++last_reced_frame;
                        }
                        bmp_list_.Add(bmp);
                        last_reced_frame = frame;
                        if (frame >= end_frame_ - 1)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            is_stopping_ = false;
            AppendInfo("\r\nファイルに保存しています\r\n");
            VfwAviWriter avi_writer = new VfwAviWriter(output_avi_filename_, (int)frame_rate_, 1);

            int last_pic_index;
            for (last_pic_index = 0; last_pic_index < bmp_list_.Count; ++last_pic_index)
            {
                if (bmp_list_[last_pic_index] != null)
                {
                    break;
                }
            }
            for (int i = last_pic_index; i < bmp_list_.Count; ++i)
            {
                if (bmp_list_[i] != null)
                {
                    avi_writer.AddFrame(bmp_list_[i]);
                    last_pic_index = i;
                }
                else
                {
                    avi_writer.AddFrame(bmp_list_[last_pic_index]);
                }
            }
            avi_writer.Close();

            main_form_.SetTitle(old_title_);
            AppendInfo("保存しました\r\n");
            AppendInfo("終了しました\r\n");
        }

        private void SwfRecForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
