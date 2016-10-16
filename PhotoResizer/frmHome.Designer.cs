
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVideoQuality)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbar1
            // 
            this.pbar1.Location = new System.Drawing.Point(8, 330);
            this.pbar1.Name = "pbar1";
            this.pbar1.Size = new System.Drawing.Size(565, 23);
            this.pbar1.TabIndex = 13;
            // 
            // lblNumFiles
            // 
            this.lblNumFiles.AutoSize = true;
            this.lblNumFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumFiles.Location = new System.Drawing.Point(12, 154);
            this.lblNumFiles.Name = "lblNumFiles";
            this.lblNumFiles.Size = new System.Drawing.Size(118, 17);
            this.lblNumFiles.TabIndex = 1;
            this.lblNumFiles.Text = "0 files selected";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(8, 288);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(565, 23);
            this.btnProcess.TabIndex = 11;
            this.btnProcess.Text = "&Process Files";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(5, 314);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(108, 13);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Processed 0 of 0 files";
            // 
            // lblDragFiles
            // 
            this.lblDragFiles.AllowDrop = true;
            this.lblDragFiles.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDragFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDragFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragFiles.Location = new System.Drawing.Point(6, 9);
            this.lblDragFiles.Name = "lblDragFiles";
            this.lblDragFiles.Size = new System.Drawing.Size(376, 136);
            this.lblDragFiles.TabIndex = 0;
            this.lblDragFiles.Text = "Drag Folders or Files Here\r\n(Photos and Videos)";
            this.lblDragFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(217, 151);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(165, 23);
            this.btnClearFiles.TabIndex = 2;
            this.btnClearFiles.Text = "&Clear files";
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // btnShowSelected
            // 
            this.btnShowSelected.Location = new System.Drawing.Point(388, 151);
            this.btnShowSelected.Name = "btnShowSelected";
            this.btnShowSelected.Size = new System.Drawing.Size(185, 23);
            this.btnShowSelected.TabIndex = 3;
            this.btnShowSelected.Text = "&Show Selected";
            this.btnShowSelected.UseVisualStyleBackColor = true;
            this.btnShowSelected.Click += new System.EventHandler(this.btnShowSelected_Click);
            // 
            // lblFileProgress
            // 
            this.lblFileProgress.AutoSize = true;
            this.lblFileProgress.Location = new System.Drawing.Point(5, 356);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new System.Drawing.Size(66, 13);
            this.lblFileProgress.TabIndex = 17;
            this.lblFileProgress.Text = "File progress";
            // 
            // pbar2
            // 
            this.pbar2.Location = new System.Drawing.Point(8, 372);
            this.pbar2.Name = "pbar2";
            this.pbar2.Size = new System.Drawing.Size(565, 23);
            this.pbar2.TabIndex = 18;
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(134, 53);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(69, 13);
            this.lblQuality.TabIndex = 16;
            this.lblQuality.Text = "&JPEG Quality";
            this.toolTip1.SetToolTip(this.lblQuality, "Determines how much a JPEG image is compressed.\r\nA higher number means better qua" +
        "lity but larger\r\nfile size. Typically a digital camera output file might\r\nbe 98." +
        "");
            // 
            // lblVideoQuality
            // 
            this.lblVideoQuality.AutoSize = true;
            this.lblVideoQuality.Location = new System.Drawing.Point(133, 54);
            this.lblVideoQuality.Name = "lblVideoQuality";
            this.lblVideoQuality.Size = new System.Drawing.Size(69, 13);
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
            this.panel1.Location = new System.Drawing.Point(8, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 100);
            this.panel1.TabIndex = 23;
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(140, 70);
            this.nudQuality.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(120, 20);
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
            this.cbxOutputType.Location = new System.Drawing.Point(7, 69);
            this.cbxOutputType.Name = "cbxOutputType";
            this.cbxOutputType.Size = new System.Drawing.Size(121, 21);
            this.cbxOutputType.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "&Output type";
            // 
            // txtResize
            // 
            this.txtResize.Location = new System.Drawing.Point(7, 29);
            this.txtResize.Name = "txtResize";
            this.txtResize.Size = new System.Drawing.Size(112, 20);
            this.txtResize.TabIndex = 12;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(4, 12);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(86, 13);
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
            this.cbxResizeType.Location = new System.Drawing.Point(137, 28);
            this.cbxResizeType.Name = "cbxResizeType";
            this.cbxResizeType.Size = new System.Drawing.Size(133, 21);
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
            this.panel2.Location = new System.Drawing.Point(295, 182);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 100);
            this.panel2.TabIndex = 24;
            // 
            // nudVideoQuality
            // 
            this.nudVideoQuality.Location = new System.Drawing.Point(136, 70);
            this.nudVideoQuality.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudVideoQuality.Name = "nudVideoQuality";
            this.nudVideoQuality.Size = new System.Drawing.Size(120, 20);
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
            this.cbxVideoOutputType.Location = new System.Drawing.Point(9, 71);
            this.cbxVideoOutputType.Name = "cbxVideoOutputType";
            this.cbxVideoOutputType.Size = new System.Drawing.Size(121, 21);
            this.cbxVideoOutputType.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "&Output type";
            // 
            // txtVideoResize
            // 
            this.txtVideoResize.Location = new System.Drawing.Point(9, 30);
            this.txtVideoResize.Name = "txtVideoResize";
            this.txtVideoResize.Size = new System.Drawing.Size(112, 20);
            this.txtVideoResize.TabIndex = 24;
            this.txtVideoResize.Text = "100";
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(6, 13);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(80, 13);
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
            this.cbxVideoResizeType.Location = new System.Drawing.Point(136, 30);
            this.cbxVideoResizeType.Name = "cbxVideoResizeType";
            this.cbxVideoResizeType.Size = new System.Drawing.Size(133, 21);
            this.cbxVideoResizeType.TabIndex = 25;
            // 
            // lblDragTrimVideo
            // 
            this.lblDragTrimVideo.AllowDrop = true;
            this.lblDragTrimVideo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDragTrimVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDragTrimVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragTrimVideo.Location = new System.Drawing.Point(388, 9);
            this.lblDragTrimVideo.Name = "lblDragTrimVideo";
            this.lblDragTrimVideo.Size = new System.Drawing.Size(185, 136);
            this.lblDragTrimVideo.TabIndex = 25;
            this.lblDragTrimVideo.Text = "Trim Video:\r\nDrag Videos To Trim Here (one at a time)";
            this.lblDragTrimVideo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 404);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(583, 22);
            this.statusStrip.TabIndex = 26;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusInfo
            // 
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(39, 17);
            this.statusInfo.Text = "Ready";
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 426);
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
    }
}

