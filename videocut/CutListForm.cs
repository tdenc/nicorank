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
        private bool is_adding_save_ = false;
        private bool is_modifying_ = false;
        private string cut_list_path_ = "";
        private List<string> history_filename_list_ = new List<string>();

        private const string title_prefix_ = "カットリスト";
        private const string modifying_text_ = "(更新)";
        private const string opening_text_ = "開く...";
        private const int max_history_num_ = 10;

        public CutListForm()
        {
            InitializeComponent();
        }

        public bool IsAddingSave
        {
            get { return is_adding_save_; }
            set { is_adding_save_ = value; }
        }

        public bool IsModifyingDataGridView
        {
            get { return is_modifying_; }
            set { is_modifying_ = value; }
        }

        public void SetForm(MainForm form)
        {
            main_form_ = form;
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
            if (is_adding_save_ && cut_list_path_ != "")
            {
                SaveFile(cut_list_path_);
            }
            else
            {
                SetModifying();
            }
        }

        public void SetToConfig(VideoCutConfig config)
        {
            config.IsFixLength = checkBoxFixLength.Checked;
            config.VideoLength = textBoxVideoLength.Text;
            config.CutListHistoryFileNameList.Clear();
            config.CutListHistoryFileNameList.AddRange(history_filename_list_);
        }

        public void LoadFromConfig(VideoCutConfig config)
        {
            checkBoxFixLength.Checked = config.IsFixLength;
            textBoxVideoLength.Text = config.VideoLength;
            history_filename_list_.Clear();
            history_filename_list_.AddRange(config.CutListHistoryFileNameList);

            is_adding_save_ = config.IsAddingSave; // ロードのみ
        }

        private void CutListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_modifying_)
            {
                if (!ConfirmSave())
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void toolStripSplitButtonOpenFile_ButtonClick(object sender, EventArgs e)
        {
            OpenWithConfirming("");
        }

        private void toolStripSplitButtonOpenFile_DropDownOpening(object sender, EventArgs e)
        {
            toolStripSplitButtonOpenFile.DropDown.Items.Clear();

            for (int i = history_filename_list_.Count - 1; i >= 0; --i)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(history_filename_list_[i]);
                item.Click += new EventHandler(delegate(object sender2, EventArgs e2)
                {
                    OpenWithConfirming(((ToolStripMenuItem)sender2).Text);
                });
                toolStripSplitButtonOpenFile.DropDown.Items.Add(item);
            }
            toolStripSplitButtonOpenFile.DropDown.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem item_open = new ToolStripMenuItem(opening_text_);
            item_open.Click += new EventHandler(delegate
            {
                OpenWithConfirming("");
            });
            toolStripSplitButtonOpenFile.DropDown.Items.Add(item_open);
        }

        private void toolStripSplitButtonSaveFile_ButtonClick(object sender, EventArgs e)
        {
            SaveWithDialog(false);
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            SaveWithDialog(false);
        }

        private void toolStripMenuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveWithDialog(true);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetModifying();
        }

        private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            SetModifying();
        }

        private void SetModifying()
        {
            if (!is_modifying_)
            {
                this.Text += modifying_text_;
                is_modifying_ = true;
            }
        }

        private void OpenWithConfirming(string filename)
        {
            if (is_modifying_)
            {
                if (!ConfirmSave())
                {
                    return;
                }
            }

            if (filename == "")
            {
                string old_path = Directory.GetCurrentDirectory();
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                Directory.SetCurrentDirectory(old_path);
                OpenFile(openFileDialog1.FileName);
            }
            else
            {
                OpenFile(filename);
            }
        }

        private void OpenFile(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show(this, "ファイルが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] lines = File.ReadAllLines(filename, Encoding.GetEncoding(932));

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
            AddToHistory(filename);
            is_modifying_ = false;
            cut_list_path_ = filename;

            this.Text = title_prefix_ + " - " + Path.GetFileName(filename);
        }

        private void AddToHistory(string filename)
        {
            if (history_filename_list_.Contains(filename))
            {
                // 要素を先頭へ
                history_filename_list_.Remove(filename);
            }
            history_filename_list_.Add(filename);
            if (history_filename_list_.Count > max_history_num_)
            {
                history_filename_list_.RemoveRange(0, history_filename_list_.Count - max_history_num_);
            }
        }

        private bool ConfirmSave()
        {
            DialogResult result = MessageBox.Show(this, "カットリストの変更を保存しますか？", "確認",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (!SaveWithDialog(false))
                {
                    return false;
                }
                MessageBox.Show(this, "保存しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }
            return true;
        }

        // is_show_dialog が true で、かつ cut_list_path_ が空なら保存ダイアログを出さない。それ以外の場合は出す
        private bool SaveWithDialog(bool is_show_dialog)
        {
            if (is_show_dialog || cut_list_path_ == "")
            {
                string old_path = Directory.GetCurrentDirectory();
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                Directory.SetCurrentDirectory(old_path);
                SaveFile(saveFileDialog1.FileName);
            }
            else
            {
                SaveFile(cut_list_path_);
            }
            return true;
        }

        private void SaveFile(string filename)
        {
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
            System.Diagnostics.Debug.WriteLine("Save: " + filename);
            File.WriteAllText(filename, buff.ToString(), Encoding.GetEncoding(932));

            AddToHistory(filename);

            is_modifying_ = false;
            cut_list_path_ = filename;
            this.Text = title_prefix_ + " - " + Path.GetFileName(filename);
        }
    }
}
