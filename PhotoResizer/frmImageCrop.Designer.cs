namespace PhotoResizer
{
    partial class frmImageCrop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureMain = new System.Windows.Forms.PictureBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureMain
            // 
            this.pictureMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureMain.Location = new System.Drawing.Point(12, 12);
            this.pictureMain.Name = "pictureMain";
            this.pictureMain.Size = new System.Drawing.Size(1363, 756);
            this.pictureMain.TabIndex = 0;
            this.pictureMain.TabStop = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Location = new System.Drawing.Point(12, 784);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(375, 50);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(972, 784);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(403, 51);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(393, 785);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(236, 50);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "&Reset Crop";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(638, 784);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(328, 50);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save and Close";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmImageCrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 847);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.pictureMain);
            this.Name = "frmImageCrop";
            this.Text = "Crop Images";
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureMain;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
    }
}