namespace videocut
{
    partial class CutListForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonAppend = new System.Windows.Forms.Button();
            this.checkBoxFixLength = new System.Windows.Forms.CheckBox();
            this.textBoxVideoLength = new System.Windows.Forms.TextBox();
            this.labelVideoLength = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonOpenFile = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSplitButtonSaveFile = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView.Location = new System.Drawing.Point(0, 25);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(367, 367);
            this.dataGridView.TabIndex = 53;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserDeletedRow);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 70F;
            this.Column1.HeaderText = "ファイル名（ID）";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 60F;
            this.Column2.HeaderText = "開始時間";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 60F;
            this.Column3.HeaderText = "終了時間";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 60F;
            this.Column4.HeaderText = "長さ";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 60F;
            this.Column5.HeaderText = "自動調整";
            this.Column5.Name = "Column5";
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "その他";
            this.Column6.Name = "Column6";
            // 
            // buttonAppend
            // 
            this.buttonAppend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAppend.Location = new System.Drawing.Point(281, 398);
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(75, 29);
            this.buttonAppend.TabIndex = 55;
            this.buttonAppend.Text = "追加";
            this.buttonAppend.UseVisualStyleBackColor = true;
            this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
            // 
            // checkBoxFixLength
            // 
            this.checkBoxFixLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFixLength.AutoSize = true;
            this.checkBoxFixLength.Location = new System.Drawing.Point(28, 405);
            this.checkBoxFixLength.Name = "checkBoxFixLength";
            this.checkBoxFixLength.Size = new System.Drawing.Size(92, 16);
            this.checkBoxFixLength.TabIndex = 56;
            this.checkBoxFixLength.Text = "動画長さ固定";
            this.checkBoxFixLength.UseVisualStyleBackColor = true;
            this.checkBoxFixLength.CheckedChanged += new System.EventHandler(this.checkBoxFixLength_CheckedChanged);
            // 
            // textBoxVideoLength
            // 
            this.textBoxVideoLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxVideoLength.Enabled = false;
            this.textBoxVideoLength.Location = new System.Drawing.Point(126, 403);
            this.textBoxVideoLength.Name = "textBoxVideoLength";
            this.textBoxVideoLength.Size = new System.Drawing.Size(27, 19);
            this.textBoxVideoLength.TabIndex = 57;
            this.textBoxVideoLength.Text = "25";
            // 
            // labelVideoLength
            // 
            this.labelVideoLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVideoLength.AutoSize = true;
            this.labelVideoLength.Enabled = false;
            this.labelVideoLength.Location = new System.Drawing.Point(159, 406);
            this.labelVideoLength.Name = "labelVideoLength";
            this.labelVideoLength.Size = new System.Drawing.Size(17, 12);
            this.labelVideoLength.TabIndex = 58;
            this.labelVideoLength.Text = "秒";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonOpenFile,
            this.toolStripSplitButtonSaveFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(367, 25);
            this.toolStrip1.TabIndex = 59;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButtonOpenFile
            // 
            this.toolStripSplitButtonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonOpenFile.Image = global::videocut.Properties.Resources.folder;
            this.toolStripSplitButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonOpenFile.Name = "toolStripSplitButtonOpenFile";
            this.toolStripSplitButtonOpenFile.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonOpenFile.Text = "開く";
            this.toolStripSplitButtonOpenFile.ButtonClick += new System.EventHandler(this.toolStripSplitButtonOpenFile_ButtonClick);
            this.toolStripSplitButtonOpenFile.DropDownOpening += new System.EventHandler(this.toolStripSplitButtonOpenFile_DropDownOpening);
            // 
            // toolStripSplitButtonSaveFile
            // 
            this.toolStripSplitButtonSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonSaveFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSave,
            this.toolStripMenuItemSaveAs});
            this.toolStripSplitButtonSaveFile.Image = global::videocut.Properties.Resources.action_save;
            this.toolStripSplitButtonSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonSaveFile.Name = "toolStripSplitButtonSaveFile";
            this.toolStripSplitButtonSaveFile.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonSaveFile.Text = "保存";
            this.toolStripSplitButtonSaveFile.ButtonClick += new System.EventHandler(this.toolStripSplitButtonSaveFile_ButtonClick);
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemSave.Text = "保存";
            this.toolStripMenuItemSave.Click += new System.EventHandler(this.toolStripMenuItemSave_Click);
            // 
            // toolStripMenuItemSaveAs
            // 
            this.toolStripMenuItemSaveAs.Name = "toolStripMenuItemSaveAs";
            this.toolStripMenuItemSaveAs.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemSaveAs.Text = "名前をつけて保存...";
            this.toolStripMenuItemSaveAs.Click += new System.EventHandler(this.toolStripMenuItemSaveAs_Click);
            // 
            // CutListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 434);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelVideoLength);
            this.Controls.Add(this.textBoxVideoLength);
            this.Controls.Add(this.checkBoxFixLength);
            this.Controls.Add(this.buttonAppend);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CutListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "カットリスト - (無題)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CutListForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button buttonAppend;
        private System.Windows.Forms.CheckBox checkBoxFixLength;
        private System.Windows.Forms.TextBox textBoxVideoLength;
        private System.Windows.Forms.Label labelVideoLength;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonOpenFile;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonSaveFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}