using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PhotoResizeLib;

namespace PhotoResizer
{
    public partial class frmImageCrop : Form
    {
        private Dictionary<string, Rectangle> cropBoundaries;
        private List<string> filenames;
        private int currentIndex;
        private Image currentImage = null;
        private static string formTitle = "Crop Images";
        private float? defaultCropRatio;
        private Rectangle currentRectangle;

        private frmImageCrop()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                currentImage?.Dispose();
            }
            base.Dispose(disposing);
        }

        public frmImageCrop(List<string> filenames, Dictionary<string, Rectangle> previousCropBoundaries, float? defaultCropRatio) : this()
        {
            this.cropBoundaries = previousCropBoundaries;
            this.filenames = filenames;
            this.defaultCropRatio = defaultCropRatio;
            this.currentIndex = 0;
            this.DisplayImage();
        }

        private void DisplayImage()
        {
            this.currentImage?.Dispose();

            string filename = this.filenames[this.currentIndex];
            this.currentImage = Image.FromFile(filename);
            this.pictureMain.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureMain.Image = this.currentImage;
            this.Text = frmImageCrop.formTitle + "(" + (this.currentIndex + 1).ToString() + " of " + this.filenames.Count.ToString() + ")";
            Rectangle cropBoundary = MediaProcessor.GetCropBoundaryForImage(filename, this.currentImage,
                this.cropBoundaries, this.defaultCropRatio);

            if (!this.cropBoundaries.ContainsKey(filename))
            {
                this.cropBoundaries.Add(filename, cropBoundary);
            }
            else
            {
                this.cropBoundaries[filename] = cropBoundary;
            }

            this.currentRectangle = cropBoundary;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.currentIndex += 1;
            if (this.currentIndex == this.filenames.Count)
            {
                this.currentIndex = 0;
            }
            this.DisplayImage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.currentIndex -= 1;
            if (this.currentIndex < 0)
            {
                this.currentIndex = this.filenames.Count - 1;
            }
            this.DisplayImage();
        }

        public void DrawRectangle(PaintEventArgs e)
        {

            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Image and container dimensions
            int w_i = this.pictureMain.Image.Width;
            int h_i = this.pictureMain.Image.Height;
            int w_c = this.pictureMain.Width;
            int h_c = this.pictureMain.Height;

            // Image and container aspect ratios
            float r_i = (float)w_i / h_i;
            float r_c = (float)w_c / h_c;

            float scale;
            int left;
            int top;
            if (r_i > r_c)  // Fills the width
            {
                scale = (float)w_c / w_i;
                left = 0;
                top = (int)((h_c - h_i * scale) / 2);
            }
            else  // Fills the height
            {
                scale = (float)h_c / h_i;
                left = (int)((w_c - w_i * scale) / 2);
                top = 0;

            }

            Rectangle rect = new Rectangle(left + (int)(this.currentRectangle.Left * scale), top + (int)(this.currentRectangle.Top * scale),
                (int)(this.currentRectangle.Width * scale), (int)(this.currentRectangle.Height * scale));

            // Draw rectangle to screen.
            e.Graphics.DrawRectangle(blackPen, rect);
        }
        
        private void pictureMain_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangle(e);
        }
    }
}
