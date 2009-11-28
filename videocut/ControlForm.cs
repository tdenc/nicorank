// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace videocut
{
    public partial class ControlForm : Form
    {
        private MainForm main_form_;

        public ControlForm()
        {
            InitializeComponent();
        }

        public void SetForm(MainForm form)
        {
            main_form_ = form;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            main_form_.OpenVideo(selectFileBoxVideoFile.FileName);
        }

        public string GetFileName()
        {
            return selectFileBoxVideoFile.FileName;
        }

        public void SetFileName(string file_name)
        {
            selectFileBoxVideoFile.FileName = file_name;
        }

        private void ControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            Height = 105;
        }

        private void buttonShowDetail_Click(object sender, EventArgs e)
        {
            if (((Control)sender).Text == "+")
            {
                Height = 480;
                ((Control)sender).Text = "-";
            }
            else
            {
                Height = 105;
                ((Control)sender).Text = "+";
            }
        }

        private void listViewRankFile_DoubleClick(object sender, EventArgs e)
        {
            string id = listViewRankFile.SelectedItems[0].SubItems[0].Text;

            string dir = selectFileBoxVideoFolder.FileName;

            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir, id + "*");
                if (files.Length > 0)
                {
                    selectFileBoxVideoFile.FileName = files[0];
                    buttonOpen_Click(null, null);
                }
                else
                {
                    MessageBox.Show(this, "動画ファイルが存在しません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(this, "動画フォルダが存在しません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonOpenRankFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(selectFileBoxRankFile.FileName))
            {
                MessageBox.Show(this, "ファイルが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] lines = File.ReadAllLines(selectFileBoxRankFile.FileName, Encoding.GetEncoding(932));
            foreach (string s in lines)
            {
                int tab = s.IndexOf('\t');
                string id;
                string info;
                if (tab >= 0)
                {
                    id = s.Substring(0, tab);
                    info = s.Substring(tab + 1);
                }
                else
                {
                    id = s;
                    info = "";
                }
                listViewRankFile.Items.Add(new ListViewItem(new string[] { id, info }));
            }
        }

        public void SetToConfig(VideoCutConfig config)
        {
            config.VideoFileName = selectFileBoxVideoFile.FileName;
            config.VideoDir = selectFileBoxVideoFolder.FileName;
            config.RankFileName = selectFileBoxRankFile.FileName;
            config.IsControlFormDetailOpen = (buttonShowDetail.Text == "-");
        }

        public void LoadFromConfig(VideoCutConfig config)
        {
            selectFileBoxVideoFile.FileName = config.VideoFileName;
            selectFileBoxVideoFolder.FileName = config.VideoDir;
            selectFileBoxRankFile.FileName = config.RankFileName;
            buttonShowDetail.Text = (config.IsControlFormDetailOpen ? "-" : "+");
        }
    }
}
