using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace videocut
{
    public partial class DebugInfoForm : Form
    {
        public DebugInfoForm()
        {
            InitializeComponent();
        }

        private VideoController video_controller_ = null;

        public VideoController Video_Controller
        {
            get { return video_controller_; }
            set { video_controller_ = value; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (video_controller_ != null)
            {
                textBoxInfo.Text = video_controller_.Info;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //((AVCodec.FixedDecoder)video_controller_.avcodec_manager_).is_prepared_video = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //((AVCodec.FixedDecoder)video_controller_.avcodec_manager_).is_prepared_audio = checkBox2.Checked;
        }
    }
}
