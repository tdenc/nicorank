namespace videocut
{
    partial class ControlForm
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
            this.buttonOpenVideo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonShowDetail = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOpenRankFile = new System.Windows.Forms.Button();
            this.listViewRankFile = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.selectFileBoxRankFile = new videocut.SelectFileBox();
            this.selectFileBoxVideoFolder = new videocut.SelectFileBox();
            this.selectFileBoxVideoFile = new videocut.SelectFileBox();
            this.SuspendLayout();
            // 
            // buttonOpenVideo
            // 
            this.buttonOpenVideo.Location = new System.Drawing.Point(316, 16);
            this.buttonOpenVideo.Name = "buttonOpenVideo";
            this.buttonOpenVideo.Size = new System.Drawing.Size(43, 31);
            this.buttonOpenVideo.TabIndex = 43;
            this.buttonOpenVideo.Text = "開く";
            this.buttonOpenVideo.UseVisualStyleBackColor = true;
            this.buttonOpenVideo.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "動画ファイル";
            // 
            // buttonShowDetail
            // 
            this.buttonShowDetail.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonShowDetail.Location = new System.Drawing.Point(12, 53);
            this.buttonShowDetail.Name = "buttonShowDetail";
            this.buttonShowDetail.Size = new System.Drawing.Size(18, 19);
            this.buttonShowDetail.TabIndex = 50;
            this.buttonShowDetail.Text = "+";
            this.buttonShowDetail.UseVisualStyleBackColor = true;
            this.buttonShowDetail.Click += new System.EventHandler(this.buttonShowDetail_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "詳細設定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "ランクファイル";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "動画フォルダ";
            // 
            // buttonOpenRankFile
            // 
            this.buttonOpenRankFile.Location = new System.Drawing.Point(316, 135);
            this.buttonOpenRankFile.Name = "buttonOpenRankFile";
            this.buttonOpenRankFile.Size = new System.Drawing.Size(43, 23);
            this.buttonOpenRankFile.TabIndex = 56;
            this.buttonOpenRankFile.Text = "開く";
            this.buttonOpenRankFile.UseVisualStyleBackColor = true;
            this.buttonOpenRankFile.Click += new System.EventHandler(this.buttonOpenRankFile_Click);
            // 
            // listViewRankFile
            // 
            this.listViewRankFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRankFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewRankFile.FullRowSelect = true;
            this.listViewRankFile.Location = new System.Drawing.Point(10, 164);
            this.listViewRankFile.Name = "listViewRankFile";
            this.listViewRankFile.Size = new System.Drawing.Size(350, 280);
            this.listViewRankFile.TabIndex = 57;
            this.listViewRankFile.UseCompatibleStateImageBehavior = false;
            this.listViewRankFile.View = System.Windows.Forms.View.Details;
            this.listViewRankFile.DoubleClick += new System.EventHandler(this.listViewRankFile_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "動画ID";
            this.columnHeader1.Width = 82;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "動画情報";
            this.columnHeader2.Width = 251;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // selectFileBoxRankFile
            // 
            this.selectFileBoxRankFile.FileDialog = this.openFileDialog1;
            this.selectFileBoxRankFile.FileName = "";
            this.selectFileBoxRankFile.FolderBrowserDialog = null;
            this.selectFileBoxRankFile.Location = new System.Drawing.Point(7, 135);
            this.selectFileBoxRankFile.Name = "selectFileBoxRankFile";
            this.selectFileBoxRankFile.Size = new System.Drawing.Size(303, 23);
            this.selectFileBoxRankFile.TabIndex = 55;
            // 
            // selectFileBoxVideoFolder
            // 
            this.selectFileBoxVideoFolder.FileDialog = null;
            this.selectFileBoxVideoFolder.FileName = "";
            this.selectFileBoxVideoFolder.FolderBrowserDialog = this.folderBrowserDialog1;
            this.selectFileBoxVideoFolder.Location = new System.Drawing.Point(8, 95);
            this.selectFileBoxVideoFolder.Name = "selectFileBoxVideoFolder";
            this.selectFileBoxVideoFolder.Size = new System.Drawing.Size(302, 23);
            this.selectFileBoxVideoFolder.TabIndex = 54;
            // 
            // selectFileBoxVideoFile
            // 
            this.selectFileBoxVideoFile.FileDialog = this.openFileDialog1;
            this.selectFileBoxVideoFile.FileName = "";
            this.selectFileBoxVideoFile.FolderBrowserDialog = null;
            this.selectFileBoxVideoFile.Location = new System.Drawing.Point(8, 24);
            this.selectFileBoxVideoFile.Name = "selectFileBoxVideoFile";
            this.selectFileBoxVideoFile.Size = new System.Drawing.Size(302, 23);
            this.selectFileBoxVideoFile.TabIndex = 48;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 456);
            this.Controls.Add(this.listViewRankFile);
            this.Controls.Add(this.buttonOpenRankFile);
            this.Controls.Add(this.selectFileBoxRankFile);
            this.Controls.Add(this.selectFileBoxVideoFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonShowDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectFileBoxVideoFile);
            this.Controls.Add(this.buttonOpenVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "動画ファイル";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenVideo;
        private SelectFileBox selectFileBoxVideoFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonShowDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SelectFileBox selectFileBoxVideoFolder;
        private SelectFileBox selectFileBoxRankFile;
        private System.Windows.Forms.Button buttonOpenRankFile;
        private System.Windows.Forms.ListView listViewRankFile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}