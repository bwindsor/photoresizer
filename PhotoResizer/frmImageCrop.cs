using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoResizer
{
    public partial class frmImageCrop : Form
    {
        private Dictionary<string, Rectangle> cropBoundaries;
        private List<string> filenames;
        private int currentIndex;
        private Image currentImage = null;

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

        public frmImageCrop(List<string> filenames, Dictionary<string, Rectangle> previousCropBoundaries) : this()
        {
            this.cropBoundaries = previousCropBoundaries;
            this.filenames = filenames;
            this.currentIndex = 0;
            this.DisplayImage();
        }

        private void DisplayImage()
        {
            this.currentImage?.Dispose();

            this.currentImage = Image.FromFile(this.filenames[this.currentIndex]);
            this.pictureMain.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureMain.Image = this.currentImage;
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
    }
}
