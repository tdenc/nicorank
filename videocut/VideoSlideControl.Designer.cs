namespace videocut
{
    partial class VideoSlideControl
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxSeek = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSeek)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSeek
            // 
            this.pictureBoxSeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSeek.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSeek.Name = "pictureBoxSeek";
            this.pictureBoxSeek.Size = new System.Drawing.Size(494, 53);
            this.pictureBoxSeek.TabIndex = 49;
            this.pictureBoxSeek.TabStop = false;
            this.pictureBoxSeek.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSeek_MouseMove);
            this.pictureBoxSeek.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSeek_MouseDown);
            this.pictureBoxSeek.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSeek_Paint);
            this.pictureBoxSeek.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSeek_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VideoSlideControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxSeek);
            this.Name = "VideoSlideControl";
            this.Size = new System.Drawing.Size(494, 53);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSeek)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSeek;
        private System.Windows.Forms.Timer timer1;
    }
}
