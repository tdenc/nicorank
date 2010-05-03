namespace videocut
{
    partial class SwfRecForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFramePerSec = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxStartSec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDurationSec = new System.Windows.Forms.TextBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxOutputWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxOutputHeight = new System.Windows.Forms.TextBox();
            this.selectFileBoxOutputFileName = new videocut.SelectFileBox();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "動画ファイル(*.avi)|*.avi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "出力AVIファイル名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "1秒間に";
            // 
            // textBoxFramePerSec
            // 
            this.textBoxFramePerSec.Location = new System.Drawing.Point(62, 106);
            this.textBoxFramePerSec.Name = "textBoxFramePerSec";
            this.textBoxFramePerSec.Size = new System.Drawing.Size(45, 19);
            this.textBoxFramePerSec.TabIndex = 3;
            this.textBoxFramePerSec.Text = "12";
            this.textBoxFramePerSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "フレーム取得";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "SWF録画は開発中の機能です";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(370, 112);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(122, 57);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "スタート";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxStartSec
            // 
            this.textBoxStartSec.Location = new System.Drawing.Point(129, 130);
            this.textBoxStartSec.Name = "textBoxStartSec";
            this.textBoxStartSec.Size = new System.Drawing.Size(31, 19);
            this.textBoxStartSec.TabIndex = 7;
            this.textBoxStartSec.Text = "2";
            this.textBoxStartSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "スタートボタン押下後、";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "秒後に録画開始";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "秒間録画";
            // 
            // textBoxDurationSec
            // 
            this.textBoxDurationSec.Location = new System.Drawing.Point(15, 155);
            this.textBoxDurationSec.Name = "textBoxDurationSec";
            this.textBoxDurationSec.Size = new System.Drawing.Size(39, 19);
            this.textBoxDurationSec.TabIndex = 10;
            this.textBoxDurationSec.Text = "20";
            this.textBoxDurationSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(19, 188);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(473, 59);
            this.textBoxInfo.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "出力AVIサイズ";
            // 
            // textBoxOutputWidth
            // 
            this.textBoxOutputWidth.Location = new System.Drawing.Point(105, 83);
            this.textBoxOutputWidth.Name = "textBoxOutputWidth";
            this.textBoxOutputWidth.Size = new System.Drawing.Size(35, 19);
            this.textBoxOutputWidth.TabIndex = 14;
            this.textBoxOutputWidth.Text = "512";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "×";
            // 
            // textBoxOutputHeight
            // 
            this.textBoxOutputHeight.Location = new System.Drawing.Point(168, 83);
            this.textBoxOutputHeight.Name = "textBoxOutputHeight";
            this.textBoxOutputHeight.Size = new System.Drawing.Size(35, 19);
            this.textBoxOutputHeight.TabIndex = 16;
            this.textBoxOutputHeight.Text = "384";
            // 
            // selectFileBoxOutputFileName
            // 
            this.selectFileBoxOutputFileName.FileDialog = this.saveFileDialog1;
            this.selectFileBoxOutputFileName.FileName = "";
            this.selectFileBoxOutputFileName.FolderBrowserDialog = null;
            this.selectFileBoxOutputFileName.Location = new System.Drawing.Point(14, 55);
            this.selectFileBoxOutputFileName.Name = "selectFileBoxOutputFileName";
            this.selectFileBoxOutputFileName.Size = new System.Drawing.Size(478, 23);
            this.selectFileBoxOutputFileName.TabIndex = 0;
            // 
            // SwfRecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 259);
            this.Controls.Add(this.textBoxOutputHeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxOutputWidth);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDurationSec);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxStartSec);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFramePerSec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectFileBoxOutputFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SwfRecForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SWF録画";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SwfRecForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SelectFileBox selectFileBoxOutputFileName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFramePerSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxStartSec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDurationSec;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxOutputWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxOutputHeight;
    }
}