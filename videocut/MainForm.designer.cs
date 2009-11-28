namespace videocut
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.textBoxCurrentTime = new System.Windows.Forms.TextBox();
            this.textBoxCurrentFrame = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFramePerSec = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMark1Frame = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonMark1 = new System.Windows.Forms.Button();
            this.buttonPlayFromMark1 = new System.Windows.Forms.Button();
            this.buttonPlayFromMark1Plus1 = new System.Windows.Forms.Button();
            this.buttonPlayFromMark1Minus1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMark1Sec = new System.Windows.Forms.TextBox();
            this.buttonPlayFromMark1Minus5 = new System.Windows.Forms.Button();
            this.buttonPlayFromMark1Plus5 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonPlayFromMark2Minus5Prev = new System.Windows.Forms.Button();
            this.buttonPlayFromMark2Plus5Prev = new System.Windows.Forms.Button();
            this.buttonPlayFromMark2Minus1Prev = new System.Windows.Forms.Button();
            this.buttonPlayFromMark2Plus1Prev = new System.Windows.Forms.Button();
            this.buttonPlayFromMark2Prev = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMark2Sec = new System.Windows.Forms.TextBox();
            this.buttonMark2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMark2Frame = new System.Windows.Forms.TextBox();
            this.pictureBoxPlayButton = new System.Windows.Forms.PictureBox();
            this.panelControl = new System.Windows.Forms.Panel();
            this.buttonSeekFrame1 = new System.Windows.Forms.Button();
            this.buttonSeekFrame2 = new System.Windows.Forms.Button();
            this.buttonSeekFrame3 = new System.Windows.Forms.Button();
            this.buttonSeekFrame8 = new System.Windows.Forms.Button();
            this.buttonSeekFrame4 = new System.Windows.Forms.Button();
            this.buttonSeekFrame7 = new System.Windows.Forms.Button();
            this.buttonSeekFrame5 = new System.Windows.Forms.Button();
            this.buttonSeekFrame6 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.labelListWriteDone = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.timerForSwf = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemControlForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCutListForm = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSlideControl1 = new videocut.VideoSlideControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayButton)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(512, 384);
            this.pictureBoxMain.TabIndex = 5;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMain_Paint);
            // 
            // textBoxCurrentTime
            // 
            this.textBoxCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrentTime.Location = new System.Drawing.Point(412, 83);
            this.textBoxCurrentTime.Name = "textBoxCurrentTime";
            this.textBoxCurrentTime.ReadOnly = true;
            this.textBoxCurrentTime.Size = new System.Drawing.Size(47, 19);
            this.textBoxCurrentTime.TabIndex = 15;
            // 
            // textBoxCurrentFrame
            // 
            this.textBoxCurrentFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrentFrame.Location = new System.Drawing.Point(412, 58);
            this.textBoxCurrentFrame.Name = "textBoxCurrentFrame";
            this.textBoxCurrentFrame.ReadOnly = true;
            this.textBoxCurrentFrame.Size = new System.Drawing.Size(47, 19);
            this.textBoxCurrentFrame.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(465, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "秒";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "フレーム";
            // 
            // textBoxFramePerSec
            // 
            this.textBoxFramePerSec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFramePerSec.Location = new System.Drawing.Point(391, 112);
            this.textBoxFramePerSec.Name = "textBoxFramePerSec";
            this.textBoxFramePerSec.ReadOnly = true;
            this.textBoxFramePerSec.Size = new System.Drawing.Size(47, 19);
            this.textBoxFramePerSec.TabIndex = 19;
            this.textBoxFramePerSec.Text = "24.0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "フレーム/秒";
            // 
            // textBoxMark1Frame
            // 
            this.textBoxMark1Frame.Location = new System.Drawing.Point(14, 50);
            this.textBoxMark1Frame.Name = "textBoxMark1Frame";
            this.textBoxMark1Frame.Size = new System.Drawing.Size(47, 19);
            this.textBoxMark1Frame.TabIndex = 21;
            this.textBoxMark1Frame.TextChanged += new System.EventHandler(this.textBoxMark1Frame_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "フレーム";
            // 
            // buttonMark1
            // 
            this.buttonMark1.Location = new System.Drawing.Point(11, 15);
            this.buttonMark1.Name = "buttonMark1";
            this.buttonMark1.Size = new System.Drawing.Size(48, 24);
            this.buttonMark1.TabIndex = 23;
            this.buttonMark1.Text = "マーク";
            this.buttonMark1.UseVisualStyleBackColor = true;
            this.buttonMark1.Click += new System.EventHandler(this.buttonMark_Click);
            // 
            // buttonPlayFromMark1
            // 
            this.buttonPlayFromMark1.Location = new System.Drawing.Point(203, 15);
            this.buttonPlayFromMark1.Name = "buttonPlayFromMark1";
            this.buttonPlayFromMark1.Size = new System.Drawing.Size(48, 24);
            this.buttonPlayFromMark1.TabIndex = 24;
            this.buttonPlayFromMark1.Tag = "";
            this.buttonPlayFromMark1.Text = "再生";
            this.buttonPlayFromMark1.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark1.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark1Plus1
            // 
            this.buttonPlayFromMark1Plus1.Location = new System.Drawing.Point(257, 15);
            this.buttonPlayFromMark1Plus1.Name = "buttonPlayFromMark1Plus1";
            this.buttonPlayFromMark1Plus1.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark1Plus1.TabIndex = 25;
            this.buttonPlayFromMark1Plus1.Tag = "";
            this.buttonPlayFromMark1Plus1.Text = ">";
            this.buttonPlayFromMark1Plus1.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark1Plus1.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark1Minus1
            // 
            this.buttonPlayFromMark1Minus1.Location = new System.Drawing.Point(165, 15);
            this.buttonPlayFromMark1Minus1.Name = "buttonPlayFromMark1Minus1";
            this.buttonPlayFromMark1Minus1.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark1Minus1.TabIndex = 26;
            this.buttonPlayFromMark1Minus1.Text = "<";
            this.buttonPlayFromMark1Minus1.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark1Minus1.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxMark1Sec);
            this.groupBox1.Controls.Add(this.buttonPlayFromMark1Minus5);
            this.groupBox1.Controls.Add(this.buttonPlayFromMark1Plus5);
            this.groupBox1.Controls.Add(this.buttonMark1);
            this.groupBox1.Controls.Add(this.buttonPlayFromMark1);
            this.groupBox1.Controls.Add(this.buttonPlayFromMark1Minus1);
            this.groupBox1.Controls.Add(this.buttonPlayFromMark1Plus1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxMark1Frame);
            this.groupBox1.Location = new System.Drawing.Point(31, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 78);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "再生ポイント";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "秒";
            // 
            // textBoxMark1Sec
            // 
            this.textBoxMark1Sec.Location = new System.Drawing.Point(127, 50);
            this.textBoxMark1Sec.Name = "textBoxMark1Sec";
            this.textBoxMark1Sec.ReadOnly = true;
            this.textBoxMark1Sec.Size = new System.Drawing.Size(42, 19);
            this.textBoxMark1Sec.TabIndex = 41;
            // 
            // buttonPlayFromMark1Minus5
            // 
            this.buttonPlayFromMark1Minus5.Location = new System.Drawing.Point(127, 15);
            this.buttonPlayFromMark1Minus5.Name = "buttonPlayFromMark1Minus5";
            this.buttonPlayFromMark1Minus5.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark1Minus5.TabIndex = 33;
            this.buttonPlayFromMark1Minus5.Text = "<<";
            this.buttonPlayFromMark1Minus5.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark1Minus5.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark1Plus5
            // 
            this.buttonPlayFromMark1Plus5.Location = new System.Drawing.Point(295, 15);
            this.buttonPlayFromMark1Plus5.Name = "buttonPlayFromMark1Plus5";
            this.buttonPlayFromMark1Plus5.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark1Plus5.TabIndex = 32;
            this.buttonPlayFromMark1Plus5.Tag = "";
            this.buttonPlayFromMark1Plus5.Text = ">>";
            this.buttonPlayFromMark1Plus5.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark1Plus5.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonPlayFromMark2Minus5Prev);
            this.groupBox2.Controls.Add(this.buttonPlayFromMark2Plus5Prev);
            this.groupBox2.Controls.Add(this.buttonPlayFromMark2Minus1Prev);
            this.groupBox2.Controls.Add(this.buttonPlayFromMark2Plus1Prev);
            this.groupBox2.Controls.Add(this.buttonPlayFromMark2Prev);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxMark2Sec);
            this.groupBox2.Controls.Add(this.buttonMark2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxMark2Frame);
            this.groupBox2.Location = new System.Drawing.Point(31, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 77);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "停止ポイント";
            // 
            // buttonPlayFromMark2Minus5Prev
            // 
            this.buttonPlayFromMark2Minus5Prev.Location = new System.Drawing.Point(127, 14);
            this.buttonPlayFromMark2Minus5Prev.Name = "buttonPlayFromMark2Minus5Prev";
            this.buttonPlayFromMark2Minus5Prev.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark2Minus5Prev.TabIndex = 51;
            this.buttonPlayFromMark2Minus5Prev.Text = "<<";
            this.buttonPlayFromMark2Minus5Prev.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark2Minus5Prev.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark2Plus5Prev
            // 
            this.buttonPlayFromMark2Plus5Prev.Location = new System.Drawing.Point(295, 14);
            this.buttonPlayFromMark2Plus5Prev.Name = "buttonPlayFromMark2Plus5Prev";
            this.buttonPlayFromMark2Plus5Prev.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark2Plus5Prev.TabIndex = 51;
            this.buttonPlayFromMark2Plus5Prev.Tag = "";
            this.buttonPlayFromMark2Plus5Prev.Text = ">>";
            this.buttonPlayFromMark2Plus5Prev.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark2Plus5Prev.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark2Minus1Prev
            // 
            this.buttonPlayFromMark2Minus1Prev.Location = new System.Drawing.Point(165, 14);
            this.buttonPlayFromMark2Minus1Prev.Name = "buttonPlayFromMark2Minus1Prev";
            this.buttonPlayFromMark2Minus1Prev.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark2Minus1Prev.TabIndex = 50;
            this.buttonPlayFromMark2Minus1Prev.Text = "<";
            this.buttonPlayFromMark2Minus1Prev.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark2Minus1Prev.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark2Plus1Prev
            // 
            this.buttonPlayFromMark2Plus1Prev.Location = new System.Drawing.Point(257, 14);
            this.buttonPlayFromMark2Plus1Prev.Name = "buttonPlayFromMark2Plus1Prev";
            this.buttonPlayFromMark2Plus1Prev.Size = new System.Drawing.Size(32, 24);
            this.buttonPlayFromMark2Plus1Prev.TabIndex = 50;
            this.buttonPlayFromMark2Plus1Prev.Tag = "";
            this.buttonPlayFromMark2Plus1Prev.Text = ">";
            this.buttonPlayFromMark2Plus1Prev.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark2Plus1Prev.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // buttonPlayFromMark2Prev
            // 
            this.buttonPlayFromMark2Prev.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonPlayFromMark2Prev.Location = new System.Drawing.Point(203, 14);
            this.buttonPlayFromMark2Prev.Name = "buttonPlayFromMark2Prev";
            this.buttonPlayFromMark2Prev.Size = new System.Drawing.Size(48, 24);
            this.buttonPlayFromMark2Prev.TabIndex = 45;
            this.buttonPlayFromMark2Prev.Tag = "";
            this.buttonPlayFromMark2Prev.Text = "再生";
            this.buttonPlayFromMark2Prev.UseVisualStyleBackColor = true;
            this.buttonPlayFromMark2Prev.Click += new System.EventHandler(this.buttonPlayFromMark_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 43;
            this.label7.Text = "秒";
            // 
            // textBoxMark2Sec
            // 
            this.textBoxMark2Sec.Location = new System.Drawing.Point(127, 48);
            this.textBoxMark2Sec.Name = "textBoxMark2Sec";
            this.textBoxMark2Sec.ReadOnly = true;
            this.textBoxMark2Sec.Size = new System.Drawing.Size(42, 19);
            this.textBoxMark2Sec.TabIndex = 42;
            // 
            // buttonMark2
            // 
            this.buttonMark2.Location = new System.Drawing.Point(11, 14);
            this.buttonMark2.Name = "buttonMark2";
            this.buttonMark2.Size = new System.Drawing.Size(48, 24);
            this.buttonMark2.TabIndex = 23;
            this.buttonMark2.Text = "マーク";
            this.buttonMark2.UseVisualStyleBackColor = true;
            this.buttonMark2.Click += new System.EventHandler(this.buttonMarkStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "フレーム";
            // 
            // textBoxMark2Frame
            // 
            this.textBoxMark2Frame.Location = new System.Drawing.Point(14, 48);
            this.textBoxMark2Frame.Name = "textBoxMark2Frame";
            this.textBoxMark2Frame.Size = new System.Drawing.Size(47, 19);
            this.textBoxMark2Frame.TabIndex = 21;
            this.textBoxMark2Frame.TextChanged += new System.EventHandler(this.textBoxMark2Frame_TextChanged);
            // 
            // pictureBoxPlayButton
            // 
            this.pictureBoxPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPlayButton.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlayButton.Image")));
            this.pictureBoxPlayButton.Location = new System.Drawing.Point(31, 59);
            this.pictureBoxPlayButton.Name = "pictureBoxPlayButton";
            this.pictureBoxPlayButton.Size = new System.Drawing.Size(37, 37);
            this.pictureBoxPlayButton.TabIndex = 40;
            this.pictureBoxPlayButton.TabStop = false;
            this.pictureBoxPlayButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlayButton_MouseUp);
            // 
            // panelControl
            // 
            this.panelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelControl.Controls.Add(this.buttonSeekFrame1);
            this.panelControl.Controls.Add(this.buttonSeekFrame2);
            this.panelControl.Controls.Add(this.buttonSeekFrame3);
            this.panelControl.Controls.Add(this.buttonSeekFrame8);
            this.panelControl.Controls.Add(this.buttonSeekFrame4);
            this.panelControl.Controls.Add(this.buttonSeekFrame7);
            this.panelControl.Controls.Add(this.buttonSeekFrame5);
            this.panelControl.Controls.Add(this.videoSlideControl1);
            this.panelControl.Controls.Add(this.buttonSeekFrame6);
            this.panelControl.Controls.Add(this.label8);
            this.panelControl.Controls.Add(this.labelListWriteDone);
            this.panelControl.Controls.Add(this.pictureBoxPlayButton);
            this.panelControl.Controls.Add(this.groupBox2);
            this.panelControl.Controls.Add(this.groupBox1);
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.textBoxFramePerSec);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.textBoxCurrentFrame);
            this.panelControl.Controls.Add(this.textBoxCurrentTime);
            this.panelControl.Enabled = false;
            this.panelControl.Location = new System.Drawing.Point(0, 385);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(512, 271);
            this.panelControl.TabIndex = 41;
            // 
            // buttonSeekFrame1
            // 
            this.buttonSeekFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame1.Location = new System.Drawing.Point(78, 72);
            this.buttonSeekFrame1.Name = "buttonSeekFrame1";
            this.buttonSeekFrame1.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame1.TabIndex = 58;
            this.buttonSeekFrame1.Text = "|<";
            this.buttonSeekFrame1.UseVisualStyleBackColor = true;
            this.buttonSeekFrame1.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame2
            // 
            this.buttonSeekFrame2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame2.Location = new System.Drawing.Point(116, 72);
            this.buttonSeekFrame2.Name = "buttonSeekFrame2";
            this.buttonSeekFrame2.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame2.TabIndex = 57;
            this.buttonSeekFrame2.Text = "<<<";
            this.buttonSeekFrame2.UseVisualStyleBackColor = true;
            this.buttonSeekFrame2.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame3
            // 
            this.buttonSeekFrame3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame3.Location = new System.Drawing.Point(154, 72);
            this.buttonSeekFrame3.Name = "buttonSeekFrame3";
            this.buttonSeekFrame3.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame3.TabIndex = 56;
            this.buttonSeekFrame3.Text = "<<";
            this.buttonSeekFrame3.UseVisualStyleBackColor = true;
            this.buttonSeekFrame3.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame8
            // 
            this.buttonSeekFrame8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame8.Location = new System.Drawing.Point(344, 72);
            this.buttonSeekFrame8.Name = "buttonSeekFrame8";
            this.buttonSeekFrame8.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame8.TabIndex = 55;
            this.buttonSeekFrame8.Tag = "";
            this.buttonSeekFrame8.Text = ">|";
            this.buttonSeekFrame8.UseVisualStyleBackColor = true;
            this.buttonSeekFrame8.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame4
            // 
            this.buttonSeekFrame4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame4.Location = new System.Drawing.Point(192, 72);
            this.buttonSeekFrame4.Name = "buttonSeekFrame4";
            this.buttonSeekFrame4.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame4.TabIndex = 54;
            this.buttonSeekFrame4.Text = "<";
            this.buttonSeekFrame4.UseVisualStyleBackColor = true;
            this.buttonSeekFrame4.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame7
            // 
            this.buttonSeekFrame7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame7.Location = new System.Drawing.Point(306, 72);
            this.buttonSeekFrame7.Name = "buttonSeekFrame7";
            this.buttonSeekFrame7.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame7.TabIndex = 53;
            this.buttonSeekFrame7.Tag = "";
            this.buttonSeekFrame7.Text = ">>>";
            this.buttonSeekFrame7.UseVisualStyleBackColor = true;
            this.buttonSeekFrame7.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame5
            // 
            this.buttonSeekFrame5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame5.Location = new System.Drawing.Point(230, 72);
            this.buttonSeekFrame5.Name = "buttonSeekFrame5";
            this.buttonSeekFrame5.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame5.TabIndex = 52;
            this.buttonSeekFrame5.Text = ">";
            this.buttonSeekFrame5.UseVisualStyleBackColor = true;
            this.buttonSeekFrame5.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // buttonSeekFrame6
            // 
            this.buttonSeekFrame6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSeekFrame6.Location = new System.Drawing.Point(268, 72);
            this.buttonSeekFrame6.Name = "buttonSeekFrame6";
            this.buttonSeekFrame6.Size = new System.Drawing.Size(32, 24);
            this.buttonSeekFrame6.TabIndex = 51;
            this.buttonSeekFrame6.Tag = "";
            this.buttonSeekFrame6.Text = ">>";
            this.buttonSeekFrame6.UseVisualStyleBackColor = true;
            this.buttonSeekFrame6.Click += new System.EventHandler(this.buttonSeekFrame_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(276, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 12);
            this.label8.TabIndex = 47;
            // 
            // labelListWriteDone
            // 
            this.labelListWriteDone.AutoSize = true;
            this.labelListWriteDone.Location = new System.Drawing.Point(420, 311);
            this.labelListWriteDone.Name = "labelListWriteDone";
            this.labelListWriteDone.Size = new System.Drawing.Size(0, 12);
            this.labelListWriteDone.TabIndex = 42;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(0, 0);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(512, 384);
            this.axShockwaveFlash1.TabIndex = 43;
            this.axShockwaveFlash1.Visible = false;
            // 
            // timerForSwf
            // 
            this.timerForSwf.Interval = 10;
            this.timerForSwf.Tick += new System.EventHandler(this.timerForSwf_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemControlForm,
            this.toolStripMenuItemCutListForm});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItemControlForm
            // 
            this.toolStripMenuItemControlForm.Name = "toolStripMenuItemControlForm";
            this.toolStripMenuItemControlForm.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemControlForm.Text = "動画ファイルウィンドウ";
            this.toolStripMenuItemControlForm.Click += new System.EventHandler(this.toolStripMenuItemControlForm_Click);
            // 
            // toolStripMenuItemCutListForm
            // 
            this.toolStripMenuItemCutListForm.Name = "toolStripMenuItemCutListForm";
            this.toolStripMenuItemCutListForm.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemCutListForm.Text = "カットリストウィンドウ";
            this.toolStripMenuItemCutListForm.Click += new System.EventHandler(this.toolStripMenuItemCutListForm_Click);
            // 
            // videoSlideControl1
            // 
            this.videoSlideControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.videoSlideControl1.EndPosition = 1;
            this.videoSlideControl1.FrameExistsList = null;
            this.videoSlideControl1.KeyFrameList = null;
            this.videoSlideControl1.Length = 1;
            this.videoSlideControl1.Location = new System.Drawing.Point(9, 2);
            this.videoSlideControl1.Name = "videoSlideControl1";
            this.videoSlideControl1.Position = 0;
            this.videoSlideControl1.Size = new System.Drawing.Size(494, 53);
            this.videoSlideControl1.StartPosition = 0;
            this.videoSlideControl1.TabIndex = 49;
            this.videoSlideControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.videoSlideControl1_MouseDown);
            this.videoSlideControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.videoSlideControl1_MouseUp);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 656);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.axShockwaveFlash1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.pictureBoxMain);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "動画カッター";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayButton)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.TextBox textBoxCurrentTime;
        private System.Windows.Forms.TextBox textBoxCurrentFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFramePerSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMark1Frame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonMark1;
        private System.Windows.Forms.Button buttonPlayFromMark1;
        private System.Windows.Forms.Button buttonPlayFromMark1Plus1;
        private System.Windows.Forms.Button buttonPlayFromMark1Minus1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPlayFromMark1Minus5;
        private System.Windows.Forms.Button buttonPlayFromMark1Plus5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonMark2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMark2Frame;
        private System.Windows.Forms.PictureBox pictureBoxPlayButton;
        private System.Windows.Forms.TextBox textBoxMark1Sec;
        private System.Windows.Forms.TextBox textBoxMark2Sec;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelListWriteDone;
        private System.Windows.Forms.Button buttonPlayFromMark2Prev;
        private System.Windows.Forms.Button buttonPlayFromMark2Minus5Prev;
        private System.Windows.Forms.Button buttonPlayFromMark2Plus5Prev;
        private System.Windows.Forms.Button buttonPlayFromMark2Minus1Prev;
        private System.Windows.Forms.Button buttonPlayFromMark2Plus1Prev;
        private System.Windows.Forms.Label label8;
        private VideoSlideControl videoSlideControl1;
        private AxShockwaveFlashObjects.AxShockwaveFlash axShockwaveFlash1;
        private System.Windows.Forms.Timer timerForSwf;
        private System.Windows.Forms.Button buttonSeekFrame2;
        private System.Windows.Forms.Button buttonSeekFrame3;
        private System.Windows.Forms.Button buttonSeekFrame8;
        private System.Windows.Forms.Button buttonSeekFrame4;
        private System.Windows.Forms.Button buttonSeekFrame7;
        private System.Windows.Forms.Button buttonSeekFrame5;
        private System.Windows.Forms.Button buttonSeekFrame6;
        private System.Windows.Forms.Button buttonSeekFrame1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemControlForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCutListForm;
    }
}

