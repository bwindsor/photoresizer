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

        private frmImageCrop()
        {
            InitializeComponent();
        }

        public frmImageCrop(List<string> filenames, Dictionary<string, Rectangle> previousCropBoundaries) : this()
        {
            this.cropBoundaries = previousCropBoundaries;
            this.filenames = filenames;
        }
    }
}
