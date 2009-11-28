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
    public partial class CutListForm : Form
    {
        private MainForm main_form_;

        public CutListForm()
        {
            InitializeComponent();
        }

        public void SetForm(MainForm form)
        {
            main_form_ = form;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 1)
            {
                if (MessageBox.Show(this, "ファイルを読み込みますか？", "確認", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            if (!File.Exists(selectFileBoxCutListFile.FileName))
            {
                MessageBox.Show(this, "ファイルが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] lines = File.ReadAllLines(selectFileBoxCutListFile.FileName, Encoding.GetEncoding(932));

            dataGridView.Rows.Clear();

            foreach (string s in lines)
            {
                string[] ar = s.Split('\t');
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView);

                row.Cells[0].Value = ar[0];
                row.Cells[1].Value = (ar.Length >= 2 ? ar[1] : "");
                row.Cells[2].Value = (ar.Length >= 3 ? ar[2] : "");
                row.Cells[3].Value = (ar.Length >= 4 ? ar[3] : "");
                row.Cells[4].Value = (ar.Length >= 5 ? ar[4] : "");

                if (ar.Length >= 6)
                {
                    int c = -1;
                    for (int i = 0; i < 5; ++i)
                    {
                        c = s.IndexOf('\t', c + 1);
                    }
                    row.Cells[5].Value = s.Substring(c + 1);
                }

                dataGridView.Rows.Add(row);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string filename = selectFileBoxCutListFile.FileName;

            if (File.Exists(filename))
            {
                if (MessageBox.Show(this, "ファイルを上書きしますか？", "確認", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            StringBuilder buff = new StringBuilder();

            for (int i = 0; i < dataGridView.Rows.Count - 1; ++i) // 最後の空行は飛ばす
            {
                DataGridViewRow row = dataGridView.Rows[i];
                string other = (string)row.Cells[5].Value;

                for (int j = 0; j < 4; ++j)
                {
                    buff.Append((string)row.Cells[j].Value);
                    buff.Append('\t');
                }
                buff.Append((string)row.Cells[4].Value);
                if (!string.IsNullOrEmpty(other))
                {
                    buff.Append('\t');
                    buff.Append((string)row.Cells[5].Value);
                }
                buff.Append("\r\n");
            }
            File.WriteAllText(filename, buff.ToString());

            MessageBox.Show(this, "保存しました");
        }

        private void checkBoxFixLength_CheckedChanged(object sender, EventArgs e)
        {
            textBoxVideoLength.Enabled = checkBoxFixLength.Checked;
            labelVideoLength.Enabled = checkBoxFixLength.Checked;
        }

        private void buttonAppend_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView);

            row.Cells[0].Value = Path.GetFileNameWithoutExtension(main_form_.GetFileName());
            row.Cells[1].Value = main_form_.GetStartPoint();
            row.Cells[2].Value = (checkBoxFixLength.Checked ? "" : main_form_.GetEndPoint());
            row.Cells[3].Value = (checkBoxFixLength.Checked ? textBoxVideoLength.Text : "");
            row.Cells[4].Value = "";
            row.Cells[5].Value = "";

            dataGridView.Rows.Add(row);
        }
    }
}
