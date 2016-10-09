namespace PhotoResizer
{
    partial class frmVideoTrim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVideoTrim));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblEndPoint = new System.Windows.Forms.Label();
            this.lblStartPoint = new System.Windows.Forms.Label();
            this.btnSetEndPoint = new System.Windows.Forms.Button();
            this.btnSetStartPoint = new System.Windows.Forms.Button();
            this.btnStepRight = new System.Windows.Forms.Button();
            this.btnStepLeft = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wmp1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDone);
            this.panel1.Controls.Add(this.lblEndPoint);
            this.panel1.Controls.Add(this.lblStartPoint);
            this.panel1.Controls.Add(this.btnSetEndPoint);
            this.panel1.Controls.Add(this.btnSetStartPoint);
            this.panel1.Controls.Add(this.btnStepRight);
            this.panel1.Controls.Add(this.btnStepLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 444);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 74);
            this.panel1.TabIndex = 8;
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(723, 6);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(174, 56);
            this.btnDone.TabIndex = 14;
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblEndPoint
            // 
            this.lblEndPoint.AutoSize = true;
            this.lblEndPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndPoint.Location = new System.Drawing.Point(220, 37);
            this.lblEndPoint.Name = "lblEndPoint";
            this.lblEndPoint.Size = new System.Drawing.Size(91, 20);
            this.lblEndPoint.TabIndex = 13;
            this.lblEndPoint.Text = "End not set";
            // 
            // lblStartPoint
            // 
            this.lblStartPoint.AutoSize = true;
            this.lblStartPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartPoint.Location = new System.Drawing.Point(220, 6);
            this.lblStartPoint.Name = "lblStartPoint";
            this.lblStartPoint.Size = new System.Drawing.Size(97, 20);
            this.lblStartPoint.TabIndex = 12;
            this.lblStartPoint.Text = "Start not set";
            // 
            // btnSetEndPoint
            // 
            this.btnSetEndPoint.Location = new System.Drawing.Point(124, 37);
            this.btnSetEndPoint.Name = "btnSetEndPoint";
            this.btnSetEndPoint.Size = new System.Drawing.Size(90, 23);
            this.btnSetEndPoint.TabIndex = 11;
            this.btnSetEndPoint.Text = "Set End Point";
            this.btnSetEndPoint.UseVisualStyleBackColor = true;
            this.btnSetEndPoint.Click += new System.EventHandler(this.btnSetEndPoint_Click_1);
            // 
            // btnSetStartPoint
            // 
            this.btnSetStartPoint.Location = new System.Drawing.Point(123, 6);
            this.btnSetStartPoint.Name = "btnSetStartPoint";
            this.btnSetStartPoint.Size = new System.Drawing.Size(91, 26);
            this.btnSetStartPoint.TabIndex = 10;
            this.btnSetStartPoint.Text = "Set Start Point";
            this.btnSetStartPoint.UseVisualStyleBackColor = true;
            // 
            // btnStepRight
            // 
            this.btnStepRight.Location = new System.Drawing.Point(63, 4);
            this.btnStepRight.Name = "btnStepRight";
            this.btnStepRight.Size = new System.Drawing.Size(54, 57);
            this.btnStepRight.TabIndex = 9;
            this.btnStepRight.Text = "Forward one frame";
            this.btnStepRight.UseVisualStyleBackColor = true;
            this.btnStepRight.Click += new System.EventHandler(this.btnStepRight_Click);
            // 
            // btnStepLeft
            // 
            this.btnStepLeft.Location = new System.Drawing.Point(3, 4);
            this.btnStepLeft.Name = "btnStepLeft";
            this.btnStepLeft.Size = new System.Drawing.Size(54, 55);
            this.btnStepLeft.TabIndex = 8;
            this.btnStepLeft.Text = "Back one second";
            this.btnStepLeft.UseVisualStyleBackColor = true;
            this.btnStepLeft.Click += new System.EventHandler(this.btnStepLeft_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.wmp1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 521);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // wmp1
            // 
            this.wmp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmp1.Enabled = true;
            this.wmp1.Location = new System.Drawing.Point(3, 3);
            this.wmp1.Name = "wmp1";
            this.wmp1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp1.OcxState")));
            this.wmp1.Size = new System.Drawing.Size(900, 435);
            this.wmp1.TabIndex = 1;
            // 
            // frmVideoTrim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 521);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmVideoTrim";
            this.Text = "Video Trimmer";
            this.Load += new System.EventHandler(this.frmVideoTrim_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmp1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label lblEndPoint;
        private System.Windows.Forms.Label lblStartPoint;
        private System.Windows.Forms.Button btnSetEndPoint;
        private System.Windows.Forms.Button btnSetStartPoint;
        private System.Windows.Forms.Button btnStepRight;
        private System.Windows.Forms.Button btnStepLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AxWMPLib.AxWindowsMediaPlayer wmp1;
    }
}