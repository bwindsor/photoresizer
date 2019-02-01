
namespace PhotoResizer
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbar1 = new System.Windows.Forms.ProgressBar();
            this.lblNumFiles = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblDragFiles = new System.Windows.Forms.Label();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.btnShowSelected = new System.Windows.Forms.Button();
            this.lblFileProgress = new System.Windows.Forms.Label();
            this.pbar2 = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblQuality = new System.Windows.Forms.Label();
            this.lblVideoQuality = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.cbxOutputType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResize = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.cbxResizeType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudVideoQuality = new System.Windows.Forms.NumericUpDown();
            this.cbxVideoOutputType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVideoResize = new System.Windows.Forms.TextBox();
            this.lblVideo = new System.Windows.Forms.Label();
            this.cbxVideoResizeType = new System.Windows.Forms.ComboBox();
            this.lblDragTrimVideo = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSetCropBoundaries = new System.Windows.Forms.Button();
            this.txtRatio2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRatio1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVideoQuality)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbar1
            // 
            this.pbar1.Location = new System.Drawing.Point(16, 778);
            this.pbar1.Margin = new System.Windows.Forms.Padding(6);
            this.pbar1.Name = "pbar1";
            this.pbar1.Size = new System.Drawing.Size(1130, 44);
            this.pbar1.TabIndex = 13;
            // 
            // lblNumFiles
            // 
            this.lblNumFiles.AutoSize = true;
            this.lblNumFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumFiles.Location = new System.Drawing.Point(24, 296);
            this.lblNumFiles.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNumFiles.Name = "lblNumFiles";
            this.lblNumFiles.Size = new System.Drawing.Size(210, 31);
            this.lblNumFiles.TabIndex = 1;
            this.lblNumFiles.Text = "0 files selected";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(16, 697);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(6);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(1130, 44);
            this.btnProcess.TabIndex = 11;
            this.btnProcess.Text = "&Process Files";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(10, 747);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(219, 25);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Processed 0 of 0 files";
            // 
            // lblDragFiles
            // 
            this.lblDragFiles.AllowDrop = true;
            this.lblDragFiles.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDragFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDragFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragFiles.Location = new System.Drawing.Point(12, 17);
            this.lblDragFiles.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDragFiles.Name = "lblDragFiles";
            this.lblDragFiles.Size = new System.Drawing.Size(750, 260);
            this.lblDragFiles.TabIndex = 0;
            this.lblDragFiles.Text = "Drag Folders or Files Here\r\n(Photos and Videos)";
            this.lblDragFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(434, 290);
            this.btnClearFiles.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(330, 44);
            this.btnClearFiles.TabIndex = 2;
            this.btnClearFiles.Text = "&Clear files";
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // btnShowSelected
            // 
            this.btnShowSelected.Location = new System.Drawing.Point(776, 290);
            this.btnShowSelected.Margin = new System.Windows.Forms.Padding(6);
            this.btnShowSelected.Name = "btnShowSelected";
            this.btnShowSelected.Size = new System.Drawing.Size(370, 44);
            this.btnShowSelected.TabIndex = 3;
            this.btnShowSelected.Text = "&Show Selected";
            this.btnShowSelected.UseVisualStyleBackColor = true;
            this.btnShowSelected.Click += new System.EventHandler(this.btnShowSelected_Click);
            // 
            // lblFileProgress
            // 
            this.lblFileProgress.AutoSize = true;
            this.lblFileProgress.Location = new System.Drawing.Point(10, 828);
            this.lblFileProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new System.Drawing.Size(137, 25);
            this.lblFileProgress.TabIndex = 17;
            this.lblFileProgress.Text = "File progress";
            // 
            // pbar2
            // 
            this.pbar2.Location = new System.Drawing.Point(16, 858);
            this.pbar2.Margin = new System.Windows.Forms.Padding(6);
            this.pbar2.Name = "pbar2";
            this.pbar2.Size = new System.Drawing.Size(1130, 44);
            this.pbar2.TabIndex = 18;
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(268, 102);
            this.lblQuality.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(140, 25);
            this.lblQuality.TabIndex = 16;
            this.lblQuality.Text = "&JPEG Quality";
            this.toolTip1.SetToolTip(this.lblQuality, "Determines how much a JPEG image is compressed.\r\nA higher number means better qua" +
        "lity but larger\r\nfile size. Typically a digital camera output file might\r\nbe 98." +
        "");
            // 
            // lblVideoQuality
            // 
            this.lblVideoQuality.AutoSize = true;
            this.lblVideoQuality.Location = new System.Drawing.Point(266, 104);
            this.lblVideoQuality.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblVideoQuality.Name = "lblVideoQuality";
            this.lblVideoQuality.Size = new System.Drawing.Size(140, 25);
            this.lblVideoQuality.TabIndex = 28;
            this.lblVideoQuality.Text = "&Video Quality";
            this.toolTip1.SetToolTip(this.lblVideoQuality, "A value of 100 here corresponds to 8 kilobits \r\nper second (1 kilobyte per second" +
        ") at full HD\r\nresolution. For smaller videos the bit rate is\r\nscaled accordingly" +
        ".");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblQuality);
            this.panel1.Controls.Add(this.nudQuality);
            this.panel1.Controls.Add(this.cbxOutputType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtResize);
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Controls.Add(this.cbxResizeType);
            this.panel1.Location = new System.Drawing.Point(16, 350);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 190);
            this.panel1.TabIndex = 23;
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(280, 135);
            this.nudQuality.Margin = new System.Windows.Forms.Padding(6);
            this.nudQuality.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(240, 31);
            this.nudQuality.TabIndex = 17;
            this.nudQuality.Value = new decimal(new int[] {
            98,
            0,
            0,
            0});
            // 
            // cbxOutputType
            // 
            this.cbxOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOutputType.FormattingEnabled = true;
            this.cbxOutputType.Location = new System.Drawing.Point(14, 133);
            this.cbxOutputType.Margin = new System.Windows.Forms.Padding(6);
            this.cbxOutputType.Name = "cbxOutputType";
            this.cbxOutputType.Size = new System.Drawing.Size(238, 33);
            this.cbxOutputType.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "&Output type";
            // 
            // txtResize
            // 
            this.txtResize.Location = new System.Drawing.Point(14, 56);
            this.txtResize.Margin = new System.Windows.Forms.Padding(6);
            this.txtResize.Name = "txtResize";
            this.txtResize.Size = new System.Drawing.Size(220, 31);
            this.txtResize.TabIndex = 12;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(8, 23);
            this.lbl1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(173, 25);
            this.lbl1.TabIndex = 11;
            this.lbl1.Text = "&Resize picture to";
            // 
            // cbxResizeType
            // 
            this.cbxResizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResizeType.FormattingEnabled = true;
            this.cbxResizeType.Items.AddRange(new object[] {
            "% of original size",
            "pixels high",
            "pixels wide"});
            this.cbxResizeType.Location = new System.Drawing.Point(274, 54);
            this.cbxResizeType.Margin = new System.Windows.Forms.Padding(6);
            this.cbxResizeType.Name = "cbxResizeType";
            this.cbxResizeType.Size = new System.Drawing.Size(262, 33);
            this.cbxResizeType.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblVideoQuality);
            this.panel2.Controls.Add(this.nudVideoQuality);
            this.panel2.Controls.Add(this.cbxVideoOutputType);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtVideoResize);
            this.panel2.Controls.Add(this.lblVideo);
            this.panel2.Controls.Add(this.cbxVideoResizeType);
            this.panel2.Location = new System.Drawing.Point(590, 350);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 190);
            this.panel2.TabIndex = 24;
            // 
            // nudVideoQuality
            // 
            this.nudVideoQuality.Location = new System.Drawing.Point(272, 135);
            this.nudVideoQuality.Margin = new System.Windows.Forms.Padding(6);
            this.nudVideoQuality.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudVideoQuality.Name = "nudVideoQuality";
            this.nudVideoQuality.Size = new System.Drawing.Size(240, 31);
            this.nudVideoQuality.TabIndex = 29;
            this.nudVideoQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cbxVideoOutputType
            // 
            this.cbxVideoOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideoOutputType.FormattingEnabled = true;
            this.cbxVideoOutputType.Location = new System.Drawing.Point(18, 137);
            this.cbxVideoOutputType.Margin = new System.Windows.Forms.Padding(6);
            this.cbxVideoOutputType.Name = "cbxVideoOutputType";
            this.cbxVideoOutputType.Size = new System.Drawing.Size(238, 33);
            this.cbxVideoOutputType.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 26;
            this.label3.Text = "&Output type";
            // 
            // txtVideoResize
            // 
            this.txtVideoResize.Location = new System.Drawing.Point(18, 58);
            this.txtVideoResize.Margin = new System.Windows.Forms.Padding(6);
            this.txtVideoResize.Name = "txtVideoResize";
            this.txtVideoResize.Size = new System.Drawing.Size(220, 31);
            this.txtVideoResize.TabIndex = 24;
            this.txtVideoResize.Text = "100";
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(12, 25);
            this.lblVideo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(160, 25);
            this.lblVideo.TabIndex = 23;
            this.lblVideo.Text = "R&esize video to";
            // 
            // cbxVideoResizeType
            // 
            this.cbxVideoResizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideoResizeType.FormattingEnabled = true;
            this.cbxVideoResizeType.Items.AddRange(new object[] {
            "% of original size",
            "pixels high",
            "pixels wide"});
            this.cbxVideoResizeType.Location = new System.Drawing.Point(272, 58);
            this.cbxVideoResizeType.Margin = new System.Windows.Forms.Padding(6);
            this.cbxVideoResizeType.Name = "cbxVideoResizeType";
            this.cbxVideoResizeType.Size = new System.Drawing.Size(262, 33);
            this.cbxVideoResizeType.TabIndex = 25;
            // 
            // lblDragTrimVideo
            // 
            this.lblDragTrimVideo.AllowDrop = true;
            this.lblDragTrimVideo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDragTrimVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDragTrimVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragTrimVideo.Location = new System.Drawing.Point(776, 17);
            this.lblDragTrimVideo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDragTrimVideo.Name = "lblDragTrimVideo";
            this.lblDragTrimVideo.Size = new System.Drawing.Size(368, 260);
            this.lblDragTrimVideo.TabIndex = 25;
            this.lblDragTrimVideo.Text = "Trim Video:\r\nDrag Videos To Trim Here (one at a time)";
            this.lblDragTrimVideo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 944);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip.Size = new System.Drawing.Size(1155, 37);
            this.statusStrip.TabIndex = 26;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusInfo
            // 
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(79, 32);
            this.statusInfo.Text = "Ready";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(292, 654);
            this.txtSuffix.Margin = new System.Windows.Forms.Padding(6);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(220, 31);
            this.txtSuffix.TabIndex = 18;
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(27, 654);
            this.lblSuffix.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(163, 25);
            this.lblSuffix.TabIndex = 18;
            this.lblSuffix.Text = "&File name suffix";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnSetCropBoundaries);
            this.panel3.Controls.Add(this.txtRatio2);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtRatio1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(15, 559);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1128, 66);
            this.panel3.TabIndex = 27;
            // 
            // btnSetCropBoundaries
            // 
            this.btnSetCropBoundaries.Location = new System.Drawing.Point(574, 6);
            this.btnSetCropBoundaries.Margin = new System.Windows.Forms.Padding(6);
            this.btnSetCropBoundaries.Name = "btnSetCropBoundaries";
            this.btnSetCropBoundaries.Size = new System.Drawing.Size(330, 44);
            this.btnSetCropBoundaries.TabIndex = 16;
            this.btnSetCropBoundaries.Text = "Set crop boundaries in&dividually";
            this.btnSetCropBoundaries.UseVisualStyleBackColor = true;
            // 
            // txtRatio2
            // 
            this.txtRatio2.Location = new System.Drawing.Point(458, 9);
            this.txtRatio2.Margin = new System.Windows.Forms.Padding(6);
            this.txtRatio2.Name = "txtRatio2";
            this.txtRatio2.Size = new System.Drawing.Size(64, 31);
            this.txtRatio2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(428, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = ":";
            // 
            // txtRatio1
            // 
            this.txtRatio1.Location = new System.Drawing.Point(352, 9);
            this.txtRatio1.Margin = new System.Windows.Forms.Padding(6);
            this.txtRatio1.Name = "txtRatio1";
            this.txtRatio1.Size = new System.Drawing.Size(64, 31);
            this.txtRatio1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(322, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Crop pi&cture to height:width ratio";
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 981);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblSuffix);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblDragTrimVideo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblFileProgress);
            this.Controls.Add(this.pbar2);
            this.Controls.Add(this.btnShowSelected);
            this.Controls.Add(this.btnClearFiles);
            this.Controls.Add(this.lblDragFiles);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lblNumFiles);
            this.Controls.Add(this.pbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "frmHome";
            this.Text = "Photo Resizer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVideoQuality)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar pbar1;
        private System.Windows.Forms.Label lblNumFiles;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblDragFiles;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Button btnShowSelected;
        private System.Windows.Forms.Label lblFileProgress;
        private System.Windows.Forms.ProgressBar pbar2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.ComboBox cbxOutputType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResize;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cbxResizeType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblVideoQuality;
        private System.Windows.Forms.NumericUpDown nudVideoQuality;
        private System.Windows.Forms.ComboBox cbxVideoOutputType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVideoResize;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.ComboBox cbxVideoResizeType;
        private System.Windows.Forms.Label lblDragTrimVideo;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusInfo;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSetCropBoundaries;
        private System.Windows.Forms.TextBox txtRatio2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRatio1;
        private System.Windows.Forms.Label label2;
    }
}

