
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
            this.pbar1 = new System.Windows.Forms.ProgressBar();
            this.lblNumFiles = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.cbxResizeType = new System.Windows.Forms.ComboBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtResize = new System.Windows.Forms.TextBox();
            this.lblDragFiles = new System.Windows.Forms.Label();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxOutputType = new System.Windows.Forms.ComboBox();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.btnShowSelected = new System.Windows.Forms.Button();
            this.txtVideoResize = new System.Windows.Forms.TextBox();
            this.lblVideo = new System.Windows.Forms.Label();
            this.cbxVideoResizeType = new System.Windows.Forms.ComboBox();
            this.lblFileProgress = new System.Windows.Forms.Label();
            this.pbar2 = new System.Windows.Forms.ProgressBar();
            this.lblMpegQuality = new System.Windows.Forms.Label();
            this.nudMpegQuality = new System.Windows.Forms.NumericUpDown();
            this.cbxVideoOutputType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpegQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // pbar1
            // 
            this.pbar1.Location = new System.Drawing.Point(6, 308);
            this.pbar1.Name = "pbar1";
            this.pbar1.Size = new System.Drawing.Size(528, 23);
            this.pbar1.TabIndex = 13;
            // 
            // lblNumFiles
            // 
            this.lblNumFiles.AutoSize = true;
            this.lblNumFiles.Location = new System.Drawing.Point(12, 154);
            this.lblNumFiles.Name = "lblNumFiles";
            this.lblNumFiles.Size = new System.Drawing.Size(77, 13);
            this.lblNumFiles.TabIndex = 1;
            this.lblNumFiles.Text = "0 files selected";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(6, 266);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(528, 23);
            this.btnProcess.TabIndex = 11;
            this.btnProcess.Text = "&Process Files";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(6, 292);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(108, 13);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Processed 0 of 0 files";
            // 
            // cbxResizeType
            // 
            this.cbxResizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResizeType.FormattingEnabled = true;
            this.cbxResizeType.Items.AddRange(new object[] {
            "% of original size",
            "pixels high",
            "pixels wide"});
            this.cbxResizeType.Location = new System.Drawing.Point(130, 194);
            this.cbxResizeType.Name = "cbxResizeType";
            this.cbxResizeType.Size = new System.Drawing.Size(133, 21);
            this.cbxResizeType.TabIndex = 6;
            this.cbxResizeType.SelectedIndexChanged += new System.EventHandler(this.cbxResizeType_SelectedIndexChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(6, 179);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(86, 13);
            this.lbl1.TabIndex = 4;
            this.lbl1.Text = "&Resize picture to";
            // 
            // txtResize
            // 
            this.txtResize.Location = new System.Drawing.Point(12, 195);
            this.txtResize.Name = "txtResize";
            this.txtResize.Size = new System.Drawing.Size(112, 20);
            this.txtResize.TabIndex = 5;
            // 
            // lblDragFiles
            // 
            this.lblDragFiles.AllowDrop = true;
            this.lblDragFiles.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDragFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDragFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDragFiles.Location = new System.Drawing.Point(6, 9);
            this.lblDragFiles.Name = "lblDragFiles";
            this.lblDragFiles.Size = new System.Drawing.Size(530, 136);
            this.lblDragFiles.TabIndex = 0;
            this.lblDragFiles.Text = "Drag Folders or Files Here";
            this.lblDragFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(130, 153);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(133, 23);
            this.btnClearFiles.TabIndex = 2;
            this.btnClearFiles.Text = "&Clear files";
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "&Output type";
            // 
            // cbxOutputType
            // 
            this.cbxOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOutputType.FormattingEnabled = true;
            this.cbxOutputType.Items.AddRange(new object[] {
            "Match input",
            "JPG",
            "PNG",
            "BMP",
            "GIF",
            "TIF"});
            this.cbxOutputType.Location = new System.Drawing.Point(6, 239);
            this.cbxOutputType.Name = "cbxOutputType";
            this.cbxOutputType.Size = new System.Drawing.Size(121, 21);
            this.cbxOutputType.TabIndex = 8;
            this.cbxOutputType.SelectedIndexChanged += new System.EventHandler(this.cbxOutputType_SelectedIndexChanged);
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(133, 240);
            this.nudQuality.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(120, 20);
            this.nudQuality.TabIndex = 10;
            this.nudQuality.Value = new decimal(new int[] {
            98,
            0,
            0,
            0});
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(133, 222);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(69, 13);
            this.lblQuality.TabIndex = 9;
            this.lblQuality.Text = "&JPEG Quality";
            // 
            // btnShowSelected
            // 
            this.btnShowSelected.Location = new System.Drawing.Point(285, 153);
            this.btnShowSelected.Name = "btnShowSelected";
            this.btnShowSelected.Size = new System.Drawing.Size(112, 23);
            this.btnShowSelected.TabIndex = 3;
            this.btnShowSelected.Text = "&Show Selected";
            this.btnShowSelected.UseVisualStyleBackColor = true;
            this.btnShowSelected.Click += new System.EventHandler(this.btnShowSelected_Click);
            // 
            // txtVideoResize
            // 
            this.txtVideoResize.Location = new System.Drawing.Point(285, 195);
            this.txtVideoResize.Name = "txtVideoResize";
            this.txtVideoResize.Size = new System.Drawing.Size(112, 20);
            this.txtVideoResize.TabIndex = 15;
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(279, 179);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(80, 13);
            this.lblVideo.TabIndex = 14;
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
            this.cbxVideoResizeType.Location = new System.Drawing.Point(403, 194);
            this.cbxVideoResizeType.Name = "cbxVideoResizeType";
            this.cbxVideoResizeType.Size = new System.Drawing.Size(133, 21);
            this.cbxVideoResizeType.TabIndex = 16;
            // 
            // lblFileProgress
            // 
            this.lblFileProgress.AutoSize = true;
            this.lblFileProgress.Location = new System.Drawing.Point(6, 341);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new System.Drawing.Size(66, 13);
            this.lblFileProgress.TabIndex = 17;
            this.lblFileProgress.Text = "File progress";
            // 
            // pbar2
            // 
            this.pbar2.Location = new System.Drawing.Point(6, 357);
            this.pbar2.Name = "pbar2";
            this.pbar2.Size = new System.Drawing.Size(528, 23);
            this.pbar2.TabIndex = 18;
            // 
            // lblMpegQuality
            // 
            this.lblMpegQuality.AutoSize = true;
            this.lblMpegQuality.Location = new System.Drawing.Point(403, 223);
            this.lblMpegQuality.Name = "lblMpegQuality";
            this.lblMpegQuality.Size = new System.Drawing.Size(73, 13);
            this.lblMpegQuality.TabIndex = 21;
            this.lblMpegQuality.Text = "&MPEG Quality";
            // 
            // nudMpegQuality
            // 
            this.nudMpegQuality.Location = new System.Drawing.Point(403, 241);
            this.nudMpegQuality.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMpegQuality.Name = "nudMpegQuality";
            this.nudMpegQuality.Size = new System.Drawing.Size(120, 20);
            this.nudMpegQuality.TabIndex = 22;
            this.nudMpegQuality.Value = new decimal(new int[] {
            98,
            0,
            0,
            0});
            // 
            // cbxVideoOutputType
            // 
            this.cbxVideoOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideoOutputType.FormattingEnabled = true;
            this.cbxVideoOutputType.Items.AddRange(new object[] {
            "AVI"});
            this.cbxVideoOutputType.Location = new System.Drawing.Point(276, 240);
            this.cbxVideoOutputType.Name = "cbxVideoOutputType";
            this.cbxVideoOutputType.Size = new System.Drawing.Size(121, 21);
            this.cbxVideoOutputType.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "&Output type";
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 383);
            this.Controls.Add(this.lblMpegQuality);
            this.Controls.Add(this.nudMpegQuality);
            this.Controls.Add(this.cbxVideoOutputType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFileProgress);
            this.Controls.Add(this.pbar2);
            this.Controls.Add(this.txtVideoResize);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.cbxVideoResizeType);
            this.Controls.Add(this.btnShowSelected);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.nudQuality);
            this.Controls.Add(this.cbxOutputType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClearFiles);
            this.Controls.Add(this.lblDragFiles);
            this.Controls.Add(this.txtResize);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.cbxResizeType);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lblNumFiles);
            this.Controls.Add(this.pbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmHome";
            this.Text = "Photo Resizer";
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpegQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar pbar1;
        private System.Windows.Forms.Label lblNumFiles;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ComboBox cbxResizeType;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtResize;
        private System.Windows.Forms.Label lblDragFiles;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxOutputType;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Button btnShowSelected;
        private System.Windows.Forms.TextBox txtVideoResize;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.ComboBox cbxVideoResizeType;
        private System.Windows.Forms.Label lblFileProgress;
        private System.Windows.Forms.ProgressBar pbar2;
        private System.Windows.Forms.Label lblMpegQuality;
        private System.Windows.Forms.NumericUpDown nudMpegQuality;
        private System.Windows.Forms.ComboBox cbxVideoOutputType;
        private System.Windows.Forms.Label label3;
    }
}

