using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace videocut
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        public void SetToConfig(VideoCutConfig config)
        {
            config.IsVideoSizeFixed = checkBoxVideoSizeFixed.Checked;
            int width;
            if (int.TryParse(textBoxVideoSizeFixedWidth.Text, out width) && width > 0)
            {
                config.VideoSizeFixedWidth = width;
            }
            else
            {
                config.VideoSizeFixedWidth = -1;
            }
            int height;
            if (int.TryParse(textBoxVideoSizeFixedHeight.Text, out height) && height > 0)
            {
                config.VideoSizeFixedHeight = height;
            }
            else
            {
                config.VideoSizeFixedHeight = -1;
            }
            config.IsMemorySizeManual = radioButtonMemorySizeManual.Checked;
            int memory_size;
            if (int.TryParse(textBoxMemorySize.Text, out memory_size) && memory_size > 0)
            {
                config.MemorySize = memory_size;
            }
            else
            {
                config.MemorySize = -1;
            }
            config.IsAddingSave = checkBoxIsAddingSave.Checked;
            config.IsAdjustWindow = checkBoxIsAdjustWindow.Checked;
        }

        public void GetFromConfig(VideoCutConfig config)
        {
            checkBoxVideoSizeFixed.Checked = config.IsVideoSizeFixed;
            if (config.VideoSizeFixedWidth > 0)
            {
                textBoxVideoSizeFixedWidth.Text = config.VideoSizeFixedWidth.ToString();
            }
            if (config.VideoSizeFixedHeight > 0)
            {
                textBoxVideoSizeFixedHeight.Text = config.VideoSizeFixedHeight.ToString();
            }
            radioButtonMemorySizeManual.Checked = config.IsMemorySizeManual;
            if (config.MemorySize > 0)
            {
                textBoxMemorySize.Text = config.MemorySize.ToString();
            }
            checkBoxIsAddingSave.Checked = config.IsAddingSave;
            checkBoxIsAdjustWindow.Checked = config.IsAdjustWindow;
        }

        private void checkBoxVideoSizeFixed_CheckedChanged(object sender, EventArgs e)
        {
            textBoxVideoSizeFixedWidth.Enabled = checkBoxVideoSizeFixed.Checked;
            textBoxVideoSizeFixedHeight.Enabled = checkBoxVideoSizeFixed.Checked;
            labelVideoSizeFixed.Enabled = checkBoxVideoSizeFixed.Checked;
        }

        private void radioButtonMemorySizeManual_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMemorySize.Enabled = radioButtonMemorySizeManual.Checked;
            labelMemorySize.Enabled = radioButtonMemorySizeManual.Checked;
        }
    }
}
