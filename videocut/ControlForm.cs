﻿// Copyright (c) 2008 - 2009 rankingloid
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
                    MessageBox.Show("動画ファイルが存在しません");
                }
            }
            else
            {
                MessageBox.Show("動画フォルダが存在しません");
            }
        }

        private void buttonOpenRankFile_Click(object sender, EventArgs e)
        {
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
    }
}
