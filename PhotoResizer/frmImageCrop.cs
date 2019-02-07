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
    public enum DragHandle
    {
        TOP_LEFT,
        TOP,
        TOP_RIGHT,
        RIGHT,
        BOTTOM_RIGHT,
        BOTTOM,
        BOTTOM_LEFT,
        LEFT,
        MIDDLE,
        NONE
    }

    public partial class frmImageCrop : Form
    {
        private Dictionary<string, Rectangle> cropBoundaries;
        private List<string> filenames;
        private int currentIndex;
        private Image currentImage = null;
        private static string formTitle = "Crop Images";
        private float? defaultCropRatio;
        private Rectangle currentRectangle;  // Currently displayed rectangle in image coordinates
        private Rectangle currentDragRectangle;   // Currently dragged rectangle in screen coordinates
        private Rectangle currentDragInitialRectangle;  // Rectangle from the start of the drag in screen coordinates
        private Point currentDragStart;   // Drag start point in screen coordinates
        private float currentDragInitialRatio; // Image ratio at start of drag
        private DragHandle currentDragHandle = DragHandle.NONE;

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

        private Rectangle ConvertCoords(Rectangle startCoords, bool isImageToScreen)
        {
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

            Rectangle rect;
            if (isImageToScreen)
            {
                rect = new Rectangle(left + (int)(startCoords.Left * scale), top + (int)(startCoords.Top * scale),
                    (int)(startCoords.Width * scale), (int)(startCoords.Height * scale));
            }
            else
            {
                rect = new Rectangle((int)((startCoords.Left - left) / scale), (int)((startCoords.Top - top) / scale),
                    (int)(startCoords.Width / scale), (int)(startCoords.Height / scale));
            }
            return rect;
        }

        public void DrawRectangle(PaintEventArgs e)
        {

            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            Rectangle rect = (this.currentDragHandle == DragHandle.NONE 
                ? ConvertCoords(this.currentRectangle, isImageToScreen: true)
                : this.currentDragRectangle);

            // Draw rectangle to screen.
            e.Graphics.DrawRectangle(blackPen, rect);
        }
        
        private void pictureMain_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangle(e);
        }

        private void pictureMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.currentDragHandle = GetDragHandle(e);
            if (this.currentDragHandle != DragHandle.NONE)
            {
                this.currentDragRectangle = ConvertCoords(this.currentRectangle, isImageToScreen: true);
                this.currentDragInitialRectangle = this.currentDragRectangle;
                this.currentDragInitialRatio = (float)this.currentDragRectangle.Width / this.currentDragRectangle.Height;
                this.currentDragStart = e.Location;
            }
        }

        private void pictureMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.currentDragHandle != DragHandle.NONE)
            {
                this.currentRectangle = ConvertCoords(this.currentDragRectangle, isImageToScreen: false);
            }
            this.currentDragHandle = DragHandle.NONE;
        }

        private Size GetLockedAspectSize(Rectangle rect)
        {
            // Should only be called during a drag operation
            float targetRatio = this.currentDragInitialRatio;
            float currentRatio = (float)rect.Width / rect.Height;
            if (currentRatio > targetRatio)
            {
                return new Size((int)(rect.Height * targetRatio), rect.Height);
            }
            else
            {
                return new Size(rect.Width, (int)(rect.Width / targetRatio));
            }
        }

        private Rectangle MoveWithinBounds(Rectangle rect)
        {
            const int MIN_DIM = 10;
            Rectangle newRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            newRect.X = Math.Max(0, Math.Min(newRect.X, pictureMain.Width - MIN_DIM));
            newRect.Y = Math.Max(0, Math.Min(newRect.Y, pictureMain.Height - MIN_DIM));
            newRect.Width = Math.Min(pictureMain.Width - newRect.X, Math.Max(MIN_DIM, newRect.Width));
            newRect.Height = Math.Min(pictureMain.Height - newRect.Y, Math.Max(MIN_DIM, newRect.Height));
            return newRect;
        }
        private void pictureMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Size sz;
                Rectangle rect = this.currentDragRectangle;
                switch (this.currentDragHandle)
                {
                    case DragHandle.NONE:
                        break;
                    case DragHandle.BOTTOM_RIGHT:
                        rect = new Rectangle(rect.Left, rect.Top, e.X - rect.Left, e.Y - rect.Top);
                        rect = MoveWithinBounds(rect);
                        sz = GetLockedAspectSize(rect);
                        rect.Size = sz;
                        break;
                    case DragHandle.BOTTOM_LEFT:
                        rect = new Rectangle(e.X, rect.Top, rect.Right - e.X, e.Y - rect.Top);
                        rect = MoveWithinBounds(rect);
                        sz = GetLockedAspectSize(rect);
                        rect.X = rect.Right - sz.Width;
                        rect.Size = sz;
                        break;
                    case DragHandle.TOP_LEFT:
                        rect = new Rectangle(e.X, e.Y, rect.Right - e.X, rect.Bottom - e.Y);
                        rect = MoveWithinBounds(rect);
                        sz = GetLockedAspectSize(rect);
                        rect.X = rect.Right - sz.Width;
                        rect.Y = rect.Bottom - sz.Height;
                        rect.Size = sz;
                        break;
                    case DragHandle.TOP_RIGHT:
                        rect = new Rectangle(rect.Left, e.Y, e.X - rect.Left, rect.Bottom - e.Y);
                        rect = MoveWithinBounds(rect);
                        sz = GetLockedAspectSize(rect);
                        rect.Y = rect.Bottom - sz.Height;
                        rect.Size = sz;
                        break;
                    case DragHandle.RIGHT:
                        rect = new Rectangle(rect.Left, rect.Top, e.X - rect.Left, rect.Height);
                        rect = MoveWithinBounds(rect);
                        break;
                    case DragHandle.LEFT:
                        rect = new Rectangle(e.X, rect.Top, rect.Right - e.X, rect.Height);
                        rect = MoveWithinBounds(rect);
                        break;
                    case DragHandle.TOP:
                        rect = new Rectangle(rect.Left, e.Y, rect.Width, rect.Bottom - e.Y);
                        rect = MoveWithinBounds(rect);
                        break;
                    case DragHandle.BOTTOM:
                        rect = new Rectangle(rect.Left, rect.Top, rect.Width, e.Y - rect.Top);
                        rect = MoveWithinBounds(rect);
                        break;
                    case DragHandle.MIDDLE:
                        int spaceRight = pictureMain.Width - currentDragInitialRectangle.Width;
                        int spaceLeft = currentDragInitialRectangle.Left;
                        int spaceTop = currentDragInitialRectangle.Top;
                        int spaceBottom = pictureMain.Height - currentDragInitialRectangle.Height;

                        int dx = Math.Min(Math.Max(e.X - currentDragStart.X, -spaceLeft), spaceRight);
                        int dy = Math.Min(Math.Max(e.Y - currentDragStart.Y, -spaceTop), spaceBottom);
                        rect = new Rectangle(currentDragInitialRectangle.Left + dx, currentDragInitialRectangle.Top + dy, currentDragInitialRectangle.Width, currentDragInitialRectangle.Height);
                        break;
                }
                this.currentDragRectangle = rect;

                this.pictureMain.Refresh();

            }
        }
        
        private DragHandle GetDragHandle(MouseEventArgs e)
        {
            Rectangle rect = ConvertCoords(this.currentRectangle, isImageToScreen: true);
            int margin = 3;
            bool isRight = Math.Abs(e.X - rect.Right) <= margin;
            bool isLeft = Math.Abs(e.X - rect.Left) <= margin;
            bool isTop = Math.Abs(e.Y - rect.Top) <= margin;
            bool isBottom = Math.Abs(e.Y - rect.Bottom) <= margin;
            
            if (isRight && isBottom)
            {
                return DragHandle.BOTTOM_RIGHT;
            }
            else if (isRight && isTop)
            {
                return DragHandle.TOP_RIGHT;
            }
            else if (isLeft && isTop)
            {
                return DragHandle.TOP_LEFT;
            }
            else if (isLeft && isBottom)
            {
                return DragHandle.BOTTOM_LEFT;
            }
            else if (isBottom)
            {
                return DragHandle.BOTTOM;
            }
            else if (isTop)
            {
                return DragHandle.TOP;
            }
            else if (isLeft)
            {
                return DragHandle.LEFT;
            }
            else if (isRight)
            {
                return DragHandle.RIGHT;
            }
            else if (rect.Contains(e.X, e.Y))
            {
                return DragHandle.MIDDLE;
            }
            else
            {
                return DragHandle.NONE;
            }
        }
    }
}
