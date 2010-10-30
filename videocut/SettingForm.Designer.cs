namespace videocut
{
    partial class SettingForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxVideoSizeFixed = new System.Windows.Forms.CheckBox();
            this.textBoxVideoSizeFixedWidth = new System.Windows.Forms.TextBox();
            this.labelVideoSizeFixed = new System.Windows.Forms.Label();
            this.textBoxVideoSizeFixedHeight = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMemorySize = new System.Windows.Forms.Label();
            this.textBoxMemorySize = new System.Windows.Forms.TextBox();
            this.radioButtonMemorySizeManual = new System.Windows.Forms.RadioButton();
            this.radioButtonMemorySizeAuto = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxIsAddingSave = new System.Windows.Forms.CheckBox();
            this.checkBoxIsAdjustWindow = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(187, 237);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(279, 237);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxVideoSizeFixed
            // 
            this.checkBoxVideoSizeFixed.AutoSize = true;
            this.checkBoxVideoSizeFixed.Location = new System.Drawing.Point(12, 12);
            this.checkBoxVideoSizeFixed.Name = "checkBoxVideoSizeFixed";
            this.checkBoxVideoSizeFixed.Size = new System.Drawing.Size(129, 16);
            this.checkBoxVideoSizeFixed.TabIndex = 2;
            this.checkBoxVideoSizeFixed.Text = "動画サイズを固定する";
            this.checkBoxVideoSizeFixed.UseVisualStyleBackColor = true;
            this.checkBoxVideoSizeFixed.CheckedChanged += new System.EventHandler(this.checkBoxVideoSizeFixed_CheckedChanged);
            // 
            // textBoxVideoSizeFixedWidth
            // 
            this.textBoxVideoSizeFixedWidth.Enabled = false;
            this.textBoxVideoSizeFixedWidth.Location = new System.Drawing.Point(33, 34);
            this.textBoxVideoSizeFixedWidth.Name = "textBoxVideoSizeFixedWidth";
            this.textBoxVideoSizeFixedWidth.Size = new System.Drawing.Size(43, 19);
            this.textBoxVideoSizeFixedWidth.TabIndex = 3;
            // 
            // labelVideoSizeFixed
            // 
            this.labelVideoSizeFixed.AutoSize = true;
            this.labelVideoSizeFixed.Enabled = false;
            this.labelVideoSizeFixed.Location = new System.Drawing.Point(80, 37);
            this.labelVideoSizeFixed.Name = "labelVideoSizeFixed";
            this.labelVideoSizeFixed.Size = new System.Drawing.Size(17, 12);
            this.labelVideoSizeFixed.TabIndex = 4;
            this.labelVideoSizeFixed.Text = "×";
            // 
            // textBoxVideoSizeFixedHeight
            // 
            this.textBoxVideoSizeFixedHeight.Enabled = false;
            this.textBoxVideoSizeFixedHeight.Location = new System.Drawing.Point(99, 34);
            this.textBoxVideoSizeFixedHeight.Name = "textBoxVideoSizeFixedHeight";
            this.textBoxVideoSizeFixedHeight.Size = new System.Drawing.Size(43, 19);
            this.textBoxVideoSizeFixedHeight.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelMemorySize);
            this.groupBox1.Controls.Add(this.textBoxMemorySize);
            this.groupBox1.Controls.Add(this.radioButtonMemorySizeManual);
            this.groupBox1.Controls.Add(this.radioButtonMemorySizeAuto);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 91);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ピクチャ使用メモリサイズ";
            // 
            // labelMemorySize
            // 
            this.labelMemorySize.AutoSize = true;
            this.labelMemorySize.Enabled = false;
            this.labelMemorySize.Location = new System.Drawing.Point(90, 65);
            this.labelMemorySize.Name = "labelMemorySize";
            this.labelMemorySize.Size = new System.Drawing.Size(22, 12);
            this.labelMemorySize.TabIndex = 7;
            this.labelMemorySize.Text = "MB";
            // 
            // textBoxMemorySize
            // 
            this.textBoxMemorySize.Enabled = false;
            this.textBoxMemorySize.Location = new System.Drawing.Point(29, 61);
            this.textBoxMemorySize.Name = "textBoxMemorySize";
            this.textBoxMemorySize.Size = new System.Drawing.Size(56, 19);
            this.textBoxMemorySize.TabIndex = 7;
            // 
            // radioButtonMemorySizeManual
            // 
            this.radioButtonMemorySizeManual.AutoSize = true;
            this.radioButtonMemorySizeManual.Location = new System.Drawing.Point(16, 40);
            this.radioButtonMemorySizeManual.Name = "radioButtonMemorySizeManual";
            this.radioButtonMemorySizeManual.Size = new System.Drawing.Size(47, 16);
            this.radioButtonMemorySizeManual.TabIndex = 7;
            this.radioButtonMemorySizeManual.Text = "手動";
            this.radioButtonMemorySizeManual.UseVisualStyleBackColor = true;
            this.radioButtonMemorySizeManual.CheckedChanged += new System.EventHandler(this.radioButtonMemorySizeManual_CheckedChanged);
            // 
            // radioButtonMemorySizeAuto
            // 
            this.radioButtonMemorySizeAuto.AutoSize = true;
            this.radioButtonMemorySizeAuto.Checked = true;
            this.radioButtonMemorySizeAuto.Location = new System.Drawing.Point(16, 20);
            this.radioButtonMemorySizeAuto.Name = "radioButtonMemorySizeAuto";
            this.radioButtonMemorySizeAuto.Size = new System.Drawing.Size(47, 16);
            this.radioButtonMemorySizeAuto.TabIndex = 7;
            this.radioButtonMemorySizeAuto.TabStop = true;
            this.radioButtonMemorySizeAuto.Text = "自動";
            this.radioButtonMemorySizeAuto.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "一部オプションは動画を開き直すと有効になります";
            // 
            // checkBoxIsAddingSave
            // 
            this.checkBoxIsAddingSave.AutoSize = true;
            this.checkBoxIsAddingSave.Location = new System.Drawing.Point(12, 168);
            this.checkBoxIsAddingSave.Name = "checkBoxIsAddingSave";
            this.checkBoxIsAddingSave.Size = new System.Drawing.Size(213, 16);
            this.checkBoxIsAddingSave.TabIndex = 8;
            this.checkBoxIsAddingSave.Text = "「追加」ボタン押下時にカットリストを保存";
            this.checkBoxIsAddingSave.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsAdjustWindow
            // 
            this.checkBoxIsAdjustWindow.AutoSize = true;
            this.checkBoxIsAdjustWindow.Location = new System.Drawing.Point(12, 190);
            this.checkBoxIsAdjustWindow.Name = "checkBoxIsAdjustWindow";
            this.checkBoxIsAdjustWindow.Size = new System.Drawing.Size(253, 16);
            this.checkBoxIsAdjustWindow.TabIndex = 9;
            this.checkBoxIsAdjustWindow.Text = "動画を開いたときに自動でウィンドウサイズを調節";
            this.checkBoxIsAdjustWindow.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(366, 272);
            this.Controls.Add(this.checkBoxIsAdjustWindow);
            this.Controls.Add(this.checkBoxIsAddingSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxVideoSizeFixedHeight);
            this.Controls.Add(this.labelVideoSizeFixed);
            this.Controls.Add(this.textBoxVideoSizeFixedWidth);
            this.Controls.Add(this.checkBoxVideoSizeFixed);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxVideoSizeFixed;
        private System.Windows.Forms.TextBox textBoxVideoSizeFixedWidth;
        private System.Windows.Forms.Label labelVideoSizeFixed;
        private System.Windows.Forms.TextBox textBoxVideoSizeFixedHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxMemorySize;
        private System.Windows.Forms.RadioButton radioButtonMemorySizeManual;
        private System.Windows.Forms.RadioButton radioButtonMemorySizeAuto;
        private System.Windows.Forms.Label labelMemorySize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxIsAddingSave;
        private System.Windows.Forms.CheckBox checkBoxIsAdjustWindow;
    }
}