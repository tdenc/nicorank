// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace videocut
{
    public partial class MainForm : Form, VideoControllerUser
    {
        private delegate void IntegerDelegate(int value);
        private delegate void DoubleDelegate(double value);
        private delegate void BoolDelegate(bool b);
        private delegate void SizeDelegate(Size size);
        private delegate void ObjectDelegate(object obj);

        private VideoController video_controller_;
        private Bitmap bitmap_play_button_ = null;
        private Bitmap bitmap_stop_button_ = null;
        private bool is_playing_;
        private bool is_pausing_;
        private double frame_per_sec_ = 24.0;

        private bool swf_mode_ = true;

        private ControlForm control_form_;
        private CutListForm cut_list_form_;

        public MainForm()
        {
            InitializeComponent();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(
                                                   Application_ThreadException);
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(
                                                   CurrentDomain_UnhandledException);
            videoSlideControl1.Dragging += videoSlideControl1_Dragging;
            Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            video_controller_ = new VideoController(this);
            video_controller_.PictureBoxHandle = pictureBoxMain.Handle;
            
            bitmap_play_button_ = Properties.Resources.play;
            bitmap_stop_button_ = Properties.Resources.stop;
            
            control_form_ = new ControlForm();
            control_form_.Owner = this;
            control_form_.SetForm(this);

            control_form_.Show();
            control_form_.Left = this.Left + this.Width;
            control_form_.Top = this.Top;

            cut_list_form_ = new CutListForm();
            cut_list_form_.Owner = this;
            cut_list_form_.SetForm(this);

            cut_list_form_.Show();
            cut_list_form_.Left = this.Left + this.Width;
            cut_list_form_.Top = control_form_.Top + control_form_.Height;

#if DEBUG
            DebugInfoForm form_info = new DebugInfoForm();
            form_info.Owner = this;
            form_info.Video_Controller = video_controller_;

            form_info.Show();
            form_info.Left = cut_list_form_.Left + cut_list_form_.Width;
            form_info.Top = this.Top + 384;
#endif
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (is_playing_)
            {
                Stop();
            }
            if (swf_mode_)
            {
                timerForSwf.Enabled = false;
            }
            else
            {
                video_controller_.Close();
            }
            if (bitmap_play_button_ != null)
            {
                bitmap_play_button_.Dispose();
                bitmap_play_button_ = null;
            }
            if (bitmap_stop_button_ != null)
            {
                bitmap_stop_button_.Dispose();
                bitmap_stop_button_ = null;
            }
        }

        public void OpenVideo(string filename)
        {
            if (is_playing_)
            {
                Stop();
            }
            EnableControl(false);

            if (filename != "" && !File.Exists(filename))
            {
                MessageBox.Show("ファイルが存在しません。");
                return;
            }
            try
            {
                if (swf_mode_)
                {
                    timerForSwf.Enabled = false;
                }
                else
                {
                    video_controller_.Close();
                    Clear();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            swf_mode_ = JudgeSwf(filename);

            if (swf_mode_)
            {
                axShockwaveFlash1.Show();
                axShockwaveFlash1.LoadMovie(0, filename);
                axShockwaveFlash1.StopPlay();
                axShockwaveFlash1.Rewind();
                videoSlideControl1.Position = 0;
                videoSlideControl1.Length = axShockwaveFlash1.TotalFrames;
                videoSlideControl1.KeyFrameList = null;
                videoSlideControl1.FrameExistsList = null;
                frame_per_sec_ = 25.0;
                timerForSwf.Enabled = true;
                textBoxMark2Frame.Text = axShockwaveFlash1.TotalFrames.ToString();
            }
            else
            {
                try
                {
                    axShockwaveFlash1.Hide();
                    video_controller_.OpenFiles(filename);
                    videoSlideControl1.Position = 0;
                    videoSlideControl1.Length = video_controller_.FrameLength;
                    videoSlideControl1.KeyFrameList = video_controller_.KeyFrameList;
                    videoSlideControl1.FrameExistsList = video_controller_.FrameExistsList;
                    frame_per_sec_ = video_controller_.FramePerSec;
                    textBoxMark2Frame.Text = video_controller_.FrameLength.ToString();
                }
                catch (Exception err)
                {
                    MessageBox.Show(this, err.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            textBoxFramePerSec.Text = frame_per_sec_.ToString("0.000");
            EnableControl(true);

            textBoxMark1Frame.Text = "0";
        }

        private void Clear()
        {
            videoSlideControl1.Position = 0;
            is_playing_ = false;
            is_pausing_ = false;

            videoSlideControl1.KeyFrameList = null;
        }

        private void textBoxCurrentFrame_TextChanged(object sender, EventArgs e)
        {
            //今のバージョンでは何もしないでおく
        }

        private void textBoxCurrentTime_TextChanged(object sender, EventArgs e)
        {
            //今のバージョンでは何もしないでおく
        }

        private void UpdateCurrentFrame()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(UpdateCurrentFrame));
            }
            else
            {
                textBoxCurrentFrame.TextChanged -= textBoxCurrentFrame_TextChanged;
                textBoxCurrentFrame.Text = videoSlideControl1.Position.ToString();
                textBoxCurrentFrame.TextChanged += textBoxCurrentFrame_TextChanged;

                textBoxCurrentTime.TextChanged -= textBoxCurrentTime_TextChanged;
                textBoxCurrentTime.Text = (videoSlideControl1.Position / frame_per_sec_).ToString("0.00");
                textBoxCurrentTime.TextChanged += textBoxCurrentTime_TextChanged;

                videoSlideControl1.Invalidate();
            }
        }

        public void InformChangingFrame(int frame)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IntegerDelegate(InformChangingFrame), new object[]{ frame });
            }
            else
            {
                videoSlideControl1.Position = frame;
                UpdateCurrentFrame();
            }
        }

        public void DisplayLoad(bool is_display)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new BoolDelegate(DisplayLoad), new object[] { is_display });
            }
            else
            {
                if (is_display)
                {
                    label8.Text = "ロード中…";
                }
                else
                {
                    label8.Text = "";
                }
            }
        }

        public void SetPictureBoxSize(Size box_size)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SizeDelegate(SetPictureBoxSize), new object[] { box_size });
            }
            else
            {
                int old_width = pictureBoxMain.Width;
                int old_height = pictureBoxMain.Height;
                
                pictureBoxMain.Size = box_size;
                this.Width = Math.Max(box_size.Width, 512) + 8;
                this.Height += box_size.Height - old_height;
            }
        }

        public void InformStop()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(InformStop));
            }
            else
            {
                // video_controller_.Stop() は呼ぶ必要がないので、Stop() は呼ばない
                pictureBoxPlayButton.Image = bitmap_play_button_;
                is_playing_ = false;
            }
        }

        private void pictureBoxPlayButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (is_playing_)
            {
                Stop();
            }
            else
            {
                SetStoppingPoint();
                Play();
            }
        }

        private void Play()
        {
            if (swf_mode_)
            {
                pictureBoxPlayButton.Image = bitmap_stop_button_;
                axShockwaveFlash1.Play();
                is_playing_ = true;
            }
            else
            {
                if (videoSlideControl1.Position < video_controller_.FrameLength)
                {
                    pictureBoxPlayButton.Image = bitmap_stop_button_;
                    video_controller_.Start(videoSlideControl1.Position);
                    is_playing_ = true;
                }
            }
        }

        private void Stop()
        {
            pictureBoxPlayButton.Image = bitmap_play_button_;
            if (swf_mode_)
            {
                axShockwaveFlash1.Stop();
            }
            else
            {
                video_controller_.Stop();
            }
            is_playing_ = false;
        }

        private void Seek(int frame)
        {
            if (swf_mode_)
            {
                if (is_playing_)
                {
                    axShockwaveFlash1.Stop();
                }
                axShockwaveFlash1.GotoFrame(frame);
                if (is_playing_)
                {
                    axShockwaveFlash1.Play();
                }
            }
            else
            {
                if (is_playing_)
                {
                    video_controller_.Start(frame); // 再スタート
                }
                else
                {
                    video_controller_.SeekAndDraw(frame);
                }
            }
            UpdateCurrentFrame();
        }

        private void videoSlideControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (is_playing_)
            {
                System.Diagnostics.Debug.WriteLine("MouseDown " + videoSlideControl1.Position);
                pictureBoxPlayButton.Image = bitmap_play_button_;
                if (swf_mode_)
                {
                    axShockwaveFlash1.GotoFrame(videoSlideControl1.Position);
                    axShockwaveFlash1.Stop();
                }
                else
                {
                    video_controller_.StopSeek(videoSlideControl1.Position);
                }
                is_playing_ = false;
                is_pausing_ = true;
            }
            else
            {
                if (swf_mode_)
                {
                    axShockwaveFlash1.GotoFrame(videoSlideControl1.Position);
                }
                else
                {
                    video_controller_.SeekAndDraw(videoSlideControl1.Position); // マウスダウンによって位置が変わる場合があるため
                }
            }
            UpdateCurrentFrame();
        }

        private void videoSlideControl1_Dragging(object sender, MouseDraggingEventArgs e)
        {
            if (e.Kind == MouseDraggingEventArgs.EventKind.SeekingPosition)
            {
                if (swf_mode_)
                {
                    axShockwaveFlash1.GotoFrame(videoSlideControl1.Position);
                }
                else
                {
                    video_controller_.SeekAndDraw(videoSlideControl1.Position);
                }
                UpdateCurrentFrame();
            }
            else if (e.Kind == MouseDraggingEventArgs.EventKind.SeekingStartPosition)
            {
                textBoxMark1Frame.Text = e.Position.ToString();
            }
            else if (e.Kind == MouseDraggingEventArgs.EventKind.SeekingEndPosition)
            {
                textBoxMark2Frame.Text = e.Position.ToString();
            }
        }

        private void videoSlideControl1_MouseUp(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MouseUp " + videoSlideControl1.Position);
            if (is_pausing_)
            {
                is_pausing_ = false;
                Play();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (panelControl.Enabled && !is_playing_)
            {
                if (e.KeyCode == Keys.Left)
                {
                    --videoSlideControl1.Position;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    ++videoSlideControl1.Position;
                }
                Seek(videoSlideControl1.Position);
            }
        }

        private void EnableControl(bool enable)
        {
            panelControl.Enabled = enable;
        }

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            if (swf_mode_)
            {
                //axShockwaveFlash1.Invalidate();
            }
            else
            {
                video_controller_.DrawCurrentFrame();
            }
        }

        private void buttonMark_Click(object sender, EventArgs e)
        {
            int frame = videoSlideControl1.Position;
            if (sender == buttonMark1)
            {
                textBoxMark1Frame.Text = frame.ToString();
            }
            else if (sender == buttonMark2)
            {
                textBoxMark2Frame.Text = frame.ToString();
            }
            SetStoppingPoint();
        }

        private void buttonMarkStop_Click(object sender, EventArgs e)
        {
            int frame = videoSlideControl1.Position;
            if (sender == buttonMark2)
            {
                textBoxMark2Frame.Text = frame.ToString();
            }
            if (is_playing_)
            {
                Stop();
            }
            SetStoppingPoint();
        }

        private void buttonPlayFromMark_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBox textBox = (button.Name.StartsWith("buttonPlayFromMark1") ? textBoxMark1Frame : textBoxMark2Frame);

            int mark_starting_frame = int.Parse(textBox.Text);
            bool is_prev;
            int offset;

            GetOffsetFromName(button.Name, out offset, out is_prev);

            if (offset != 0)
            {
                mark_starting_frame += offset;
                if (mark_starting_frame < 0)
                {
                    mark_starting_frame = 0;
                }
                else if (mark_starting_frame >= (swf_mode_ ? axShockwaveFlash1.TotalFrames : video_controller_.FrameLength))
                {
                    mark_starting_frame = (swf_mode_ ? axShockwaveFlash1.TotalFrames : video_controller_.FrameLength);
                }
                textBox.Text = mark_starting_frame.ToString();
            }
            if (is_prev)
            {
                mark_starting_frame -= 120;
            }
            
            if (!is_playing_)
            {
                is_playing_ = true;
                pictureBoxPlayButton.Image = bitmap_stop_button_;
            }
            SetStoppingPoint();
            if (swf_mode_)
            {
                axShockwaveFlash1.Stop();
                axShockwaveFlash1.GotoFrame(mark_starting_frame);
                axShockwaveFlash1.Play();
            }
            else
            {
                video_controller_.Start(mark_starting_frame);
            }
        }

        private void GetOffsetFromName(string name, out int offset, out bool is_prev)
        {
            bool is_minus = (name.IndexOf("Minus") >= 0);
            int c = name.IndexOf("Prev");
            if (c >= 0)
            {
                is_prev = true;
                name = name.Substring(0, c);
            }
            else
            {
                is_prev = false;
            }
            if (is_minus || name.IndexOf("Plus") >= 0)
            {
                offset = name[name.Length - 1] - '0';
                if (is_minus)
                {
                    offset = -offset;
                }
            }
            else
            {
                offset = 0;
            }
        }

        private void textBoxMark1Frame_TextChanged(object sender, EventArgs e)
        {
            int val;
            if (int.TryParse(textBoxMark1Frame.Text, out val))
            {
                textBoxMark1Sec.Text = (val / frame_per_sec_).ToString("0.00");
                videoSlideControl1.StartPosition = val;
            }
        }

        private void textBoxMark2Frame_TextChanged(object sender, EventArgs e)
        {
            int val;
            if (int.TryParse(textBoxMark2Frame.Text, out val))
            {
                textBoxMark2Sec.Text = (val / frame_per_sec_).ToString("0.00");
                videoSlideControl1.EndPosition = val;
            }
        }

        private void SetStoppingPoint()
        {
            if (swf_mode_)
            {

            }
            else
            {
                video_controller_.SetStoppingPoint(int.Parse(textBoxMark2Frame.Text));
            }
        }

        private void checkBoxMarkStop_CheckedChanged(object sender, EventArgs e)
        {
            SetStoppingPoint();
        }

        private bool JudgeSwf(string filename)
        {
            FileStream file_stream = File.OpenRead(filename);
            byte[] data = new byte[3];
            try
            {
                file_stream.Read(data, 0, data.Length);
            }
            finally
            {
                file_stream.Close();
            }
            return ((data[0] == 'C' || data[0] == 'F') && data[1] == 'W' && data[2] == 'S');
        }

        private void timerForSwf_Tick(object sender, EventArgs e)
        {
            videoSlideControl1.Position = axShockwaveFlash1.FrameNum;
            UpdateCurrentFrame();
        }

        private void buttonSeekFrame_Click(object sender, EventArgs e)
        {
            int[] dx = { -99999999, -100, -10, -1, 1, 10, 100, 99999999 };
            int num = ((Button)sender).Name[15] - '1';

            videoSlideControl1.Position += dx[num];
            Seek(videoSlideControl1.Position);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string filename = GetDropFilename(e);
            control_form_.SetFileName(filename);
            OpenVideo(filename);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private string GetDropFilename(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    return files[0];
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        public string GetFileName()
        {
            return control_form_.GetFileName();
        }

        public string GetStartPoint()
        {
            return textBoxMark1Sec.Text;
        }

        public string GetEndPoint()
        {
            return textBoxMark2Sec.Text;
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + ", " + e.Exception.StackTrace , "エラー");
        }

        private void CurrentDomain_UnhandledException(object sender,
            UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show(ex.Message + ", " + ex.StackTrace, "エラー");
        }
    }
}
