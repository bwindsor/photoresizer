using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.Expression.Encoder;

namespace PhotoResizer
{
    public partial class frmHome : Form
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetLblProgressCallback(int numFailed);
        delegate void SetProgressDelegate(int numComplete);
        delegate void ProcessingCancelledDelegate();
        delegate void SetCurrentProgressDelegate(int currentProgress, string currentFile);

        private string[] allExt = { ".jpg", ".png", ".jpeg", ".gif", ".tif", ".tiff", ".bmp", ".avi", ".mov", ".wmv" };
        private string[] videoExt = { ".avi", ".mov", ".wmv" };

        private List<string> fileList = new List<string>();
        private enum comboOptions: int { percent, height, width };
        private enum outTypeOptions : int { match, JPG, PNG, BMP, GIF, TIF };
        private enum videoOutTypeOptions : int { WMV };
        private int[] comboTxtDefaults = { 50, 1000, 2000 };
        private int comboLastIdx = 0;
        private bool isProcessing = false;
        private CancellationTokenSource cancelSource = new CancellationTokenSource();

        public frmHome()
        {
            InitializeComponent();
            this.cbxResizeType.SelectedIndex = 0;
            this.cbxOutputType.SelectedIndex = 0;
            this.cbxVideoOutputType.SelectedIndex = 0;
            this.cbxVideoResizeType.SelectedIndex = 0;
            this.lblDragFiles.DragEnter += new DragEventHandler(lblDragFiles_DragEnter);
            this.lblDragFiles.DragDrop += new DragEventHandler(lblDragFiles_DragDrop);
        }


        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (this.isProcessing)
            {
                this.cancelSource.Cancel();
                return;
            }
            if (this.fileList.Count == 0)
            {
                MessageBox.Show("No files selected.", "No files selected", MessageBoxButtons.OK);
                return;
            }
            int resizeValue = TryReadResizeText(this.txtResize, (comboOptions)this.cbxResizeType.SelectedIndex);
            if (resizeValue < 0)
            {
                MessageBox.Show("Invalid value entered for resizing images to.", "Invalid entry", MessageBoxButtons.OK);
                return;
            }
            int videoResizeValue = TryReadResizeText(this.txtVideoResize, (comboOptions)this.cbxVideoResizeType.SelectedIndex);
            if (videoResizeValue < 0)
            {
                MessageBox.Show("Invalid value entered for resizing videos to.", "Invalid entry", MessageBoxButtons.OK);
                return;
            }

            this.OnProcessingStart();

            

            comboOptions comboOption = (comboOptions)this.cbxResizeType.SelectedIndex;
            comboOptions videoComboOption = (comboOptions)this.cbxVideoResizeType.SelectedIndex;
            outTypeOptions outTypeOption = (outTypeOptions)this.cbxOutputType.SelectedIndex;
            videoOutTypeOptions videoOutTypeOption = (videoOutTypeOptions)this.cbxVideoOutputType.SelectedIndex;
            int jpegQuality = (int)nudQuality.Value;
            int mpegQuality = (int)nudVideoQuality.Value;
            CancellationToken token = this.cancelSource.Token;

            Task processingTask = Task.Factory.StartNew(() =>
           {
               
               List<string> failedList = new List<string>();
               for (int ii = 0; ii < this.fileList.Count; ii++)
               {
                   if (token.IsCancellationRequested) { break; }
                   try
                   {
                       if (!IsVideoFile(this.fileList[ii]))
                       {
                           ProcessImageFile(this.fileList[ii], resizeValue, comboOption, outTypeOption, jpegQuality);
                       }
                       else
                       {
                           ProcessVideoFile(this.fileList[ii], videoResizeValue, videoComboOption, videoOutTypeOption, mpegQuality);
                       }
                       if (token.IsCancellationRequested) { break; }
                       SetProgress(ii + 1);
                   }
                   catch (Exception exp)
                   {
                       Console.WriteLine(String.Format("{0} - {1}", exp.Message, exp.StackTrace));
                       failedList.Add(this.fileList[ii]);
                   }
               }
               if (token.IsCancellationRequested)
               {
                   this.OnProcessingCancelled();
               }
               else
               {
                   this.OnProcessingComplete(failedList.Count);
               }
           }, token);

        }

        private void OnProcessingStart()
        {
            // Does any tasks which should be done when processing begins
            this.btnProcess.Text = "Cancel";
            SetControlsEnable(false);
            this.isProcessing = true;
            SetProgress(0);
            SetCurrentProgress(0, "");
        }
        
        private void SetControlsEnable(bool enbl)
        {
            this.btnClearFiles.Enabled = enbl;

            this.txtResize.Enabled = enbl;
            this.cbxOutputType.Enabled = enbl;
            this.cbxResizeType.Enabled = enbl;
            this.nudQuality.Enabled = enbl;

            this.txtVideoResize.Enabled = enbl;
            this.cbxVideoOutputType.Enabled = enbl;
            this.cbxVideoResizeType.Enabled = enbl;
            this.nudVideoQuality.Enabled = enbl;
        }

        private void OnProcessingComplete(int numFailed)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblProgress.InvokeRequired)
            {
                SetLblProgressCallback d = new SetLblProgressCallback(OnProcessingComplete);
                this.Invoke(d, new object[] { numFailed });
            }
            else
            {
                lblProgress.Text = string.Format("Processing complete on {0} files. {1} files failed.",
                                this.fileList.Count.ToString(),
                                numFailed.ToString());
                this.isProcessing = false;
                this.btnProcess.Text = "Process Files";
                SetControlsEnable(true);
            }            
        }
        private void OnProcessingCancelled()
        {
            if (this.lblProgress.InvokeRequired)
            {
                ProcessingCancelledDelegate d = new ProcessingCancelledDelegate(OnProcessingCancelled);
                this.Invoke(d, new object[] { });
            }
            else
            {
                lblProgress.Text += " (cancelled)";
                this.isProcessing = false;
                this.btnProcess.Text = "Process Files";
                SetControlsEnable(true);
                this.cancelSource = new CancellationTokenSource(); // reset cancel token
            }
        }

        private void ProcessVideoFile(string filename, int resizeValue, comboOptions resizeOption, 
                                    videoOutTypeOptions outTypeOption, int mpegQuality)
        {
            SetCurrentProgress(0, filename);

            // TODO - check if input file exists
                        
            string outFile = GetOutputFilename(filename, outTypeOption);
            EnsureDir(outFile); // Will throw exception if can't create folder
            DialogResult result = WarnIfExists(outFile);
            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    return;
                case DialogResult.Cancel:
                    this.cancelSource.Cancel();
                    return;
            }

            Job j = new Job();
            MediaItem mediaItem = new MediaItem(filename);
            var originalSize = mediaItem.OriginalVideoSize;
            // Get new dimensions
            Tuple<int, int> newSize = GetNewSize(resizeOption, originalSize.Width, originalSize.Height, resizeValue);
            int newWidth = newSize.Item1;
            int newHeight = newSize.Item2;

            double bitsPerSecondPerPixel = 8000.0 / 2000000.0;  // Assume 8kbps for full HD
            int bitRate = Convert.ToInt32(bitsPerSecondPerPixel * mpegQuality * newWidth * newHeight / 100);

            WindowsMediaOutputFormat outFormat = new WindowsMediaOutputFormat();
            outFormat.AudioProfile = new Microsoft.Expression.Encoder.Profiles.WmaAudioProfile();
            outFormat.VideoProfile = new Microsoft.Expression.Encoder.Profiles.MainVC1VideoProfile();
            outFormat.VideoProfile.AspectRatio = mediaItem.OriginalAspectRatio;
            outFormat.VideoProfile.AutoFit = true;
            outFormat.VideoProfile.Bitrate = new Microsoft.Expression.Encoder.Profiles.VariableUnconstrainedBitrate(bitRate);
            outFormat.VideoProfile.Size = new Size(newWidth, newHeight);
            FileInfo finfo = new FileInfo(filename);
            
            mediaItem.VideoResizeMode = VideoResizeMode.Letterbox;
            mediaItem.OutputFormat = outFormat;

            mediaItem.OutputFileName = Path.GetFileName(outFile);

            j.MediaItems.Add(mediaItem);
            j.CreateSubfolder = false;
            j.OutputDirectory = Path.GetDirectoryName(outFile);

            j.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnJobEncodeProgress);
            j.Encode();
            j.Dispose();
            
        }
        private void OnJobEncodeProgress(object sender, EncodeProgressEventArgs e)
        {
            SetCurrentProgress((int)(((e.CurrentPass - 1) * 100 + e.Progress) / e.TotalPasses), 
                                e.CurrentItem.SourceFileName);
        }


        private void ProcessImageFile(string filename, int resizeValue, comboOptions resizeOption, 
                                    outTypeOptions outTypeOption, int jpegQuality)
        {
            SetCurrentProgress(0, filename);

            // Read file - will throw exception if file doesn't exist
            Image img = Image.FromFile(filename);

            string outFile = GetOutputFilename(filename, outTypeOption);
            EnsureDir(outFile); // Will throw exception if can't create folder
            DialogResult result = WarnIfExists(outFile);
            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    return;
                case DialogResult.Cancel:
                    this.cancelSource.Cancel();
                    return;
            }

            // Get new dimensions
            Tuple<int, int> newSize = GetNewSize(resizeOption, img.Width, img.Height, resizeValue);
            int newWidth = newSize.Item1;
            int newHeight = newSize.Item2;
            
            // Do actual resize
            Bitmap resizedBmp = ResizeImage(img, newWidth, newHeight);

            // Save result
            if (outTypeOption == outTypeOptions.match)
            {
                switch (Path.GetExtension(filename).ToLower())
                {
                    case ".jpg": case ".jpeg":
                        outTypeOption = outTypeOptions.JPG;
                        break;
                    case ".bmp":
                        outTypeOption = outTypeOptions.BMP;
                        break;
                    case ".gif":
                        outTypeOption = outTypeOptions.GIF;
                        break;
                    case ".png":
                        outTypeOption = outTypeOptions.PNG;
                        break;
                    case ".tif": case ".tiff":
                        outTypeOption = outTypeOptions.TIF;
                        break;
                }
            }
            EncoderParameters eps = null;
            ImageCodecInfo ici = GetEncoderInfo("image/bmp");
            switch (outTypeOption)
            {
                case outTypeOptions.JPG:
                    eps = new EncoderParameters(1);
                    eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)jpegQuality);
                    ici = GetEncoderInfo("image/jpeg");
                    break;
                case outTypeOptions.BMP:
                    // Nothing to do - defaults are fine
                    break;
                case outTypeOptions.GIF:
                    ici = GetEncoderInfo("image/gif");
                    break;
                case outTypeOptions.PNG:
                    ici = GetEncoderInfo("image/png");
                    break;
                case outTypeOptions.TIF:
                    ici = GetEncoderInfo("image/tiff");
                    break;
            }
            
            foreach (PropertyItem propItem in img.PropertyItems)
            {
                resizedBmp.SetPropertyItem(propItem);
            }
            
            resizedBmp.Save(outFile, ici, eps);
        }

        private static Tuple<int, int> GetNewSize(comboOptions resizeOption, int width, int height, int resizeValue)
        {
            int newWidth = 0;
            int newHeight = 0;
            switch (resizeOption)
            {
                case comboOptions.percent:
                    newWidth = width * resizeValue / 100;
                    newHeight = height * resizeValue / 100;
                    break;
                case comboOptions.height:
                    newHeight = resizeValue;
                    newWidth = width * resizeValue / height;
                    break;
                case comboOptions.width:
                    newHeight = height * resizeValue / width;
                    newWidth = resizeValue;
                    break;
            }
            if (newWidth == 0 || newHeight == 0)
            {
                throw new Exception("New image is too small.");
            }
            return new Tuple<int, int>(newWidth, newHeight);
        }

        private static DialogResult WarnIfExists(string filename)
        {
            // if file exists, asks the user for a response
            DialogResult result = DialogResult.Yes;
            if (File.Exists(filename))
            {
                result = MessageBox.Show("File " + filename + " already exists. \r\nYes to overwrite\r\nNo to skip this file\r\nCancel to stop processing", "File already exists", MessageBoxButtons.YesNoCancel);
            }
            return result;
        }

        private static string GetOutputFilename(string filename, videoOutTypeOptions outType)
        {
            // Gets an output filename
            string extension = Path.GetExtension(filename);
            switch (outType)
            {
                case videoOutTypeOptions.WMV:
                    extension = ".wmv";
                    break;
            }
            return _CreateOutputFilename(filename, extension);
        }
        private static string GetOutputFilename(string filename, outTypeOptions outType)
        {
            // Gets an output filename
            string extension = Path.GetExtension(filename);
            switch (outType)
            {
                case outTypeOptions.BMP:
                    extension = ".bmp";
                    break;
                case outTypeOptions.GIF:
                    extension = ".gif";
                    break;
                case outTypeOptions.JPG:
                    extension = ".jpg";
                    break;
                case outTypeOptions.PNG:
                    extension = ".png";
                    break;
                case outTypeOptions.TIF:
                    extension = ".tif";
                    break;
            }
            return _CreateOutputFilename(filename, extension);
        }
        private static string _CreateOutputFilename(string filename, string extension)
        {
            return Path.Combine(Path.GetDirectoryName(filename), "Resized", string.Concat(Path.GetFileNameWithoutExtension(filename), extension));
        }

        private static void EnsureDir(string filename)
        {
            string dirName = Path.GetDirectoryName(filename);
            // Ensures the directory for a given filename exists
            if (!Directory.Exists(dirName)) { Directory.CreateDirectory(dirName); }
        }

        private void SetFileCount()
        {
            lblNumFiles.Text = this.fileList.Count.ToString() + " files selected.";
        }

        private void SetProgress(int numComplete)
        {
            if (this.lblProgress.InvokeRequired)
            {
                SetProgressDelegate d = new SetProgressDelegate(SetProgress);
                this.Invoke(d, new object[] { numComplete });
            }
            else
            {
                int numTotal = this.fileList.Count;
                lblProgress.Text = "Processed " + numComplete.ToString() + " of " + numTotal.ToString() + " files.";
                pbar1.Value = numComplete * pbar1.Maximum / numTotal;
            }
        }
        private void SetCurrentProgress(int currentProgress, string currentFile)
        {
            if (this.lblFileProgress.InvokeRequired)
            {
                SetCurrentProgressDelegate d = new SetCurrentProgressDelegate(SetCurrentProgress);
                this.Invoke(d, new object[] { currentProgress, currentFile });
            }
            else
            {
                int numTotal = this.fileList.Count;
                lblFileProgress.Text = "Processing file " + currentFile;
                pbar2.Value = currentProgress;
            }
        }

        private bool IsVideoFile(string filename)
        {
            return videoExt.Contains(Path.GetExtension(filename).ToLower());
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void cbxResizeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resizeValue = TryReadResizeText(this.txtResize, (comboOptions)this.comboLastIdx);
            if (resizeValue >= 0)
            {
                this.comboTxtDefaults[this.comboLastIdx] = resizeValue;
            }
            this.txtResize.Text = this.comboTxtDefaults[this.cbxResizeType.SelectedIndex].ToString();
            this.comboLastIdx = this.cbxResizeType.SelectedIndex;
        }

        private int TryReadResizeText(TextBox txtBox, comboOptions comboIdx)
        {
            int x;
            bool success = int.TryParse(txtBox.Text, out x);

            if (success)
            {
                switch (comboIdx)
                {
                    case comboOptions.percent:
                        if (x < 1 || x > 1000) { x = -1; }
                        break;
                    case comboOptions.height:
                        if (x < 1 || x > 10000) { x = -1; }
                        break;
                    case comboOptions.width:
                        if (x < 1 || x > 10000) { x = -1; }
                        break;
                };
            }
            else
            {
                x = -1;
            }
            return x;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void lblDragFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (this.isProcessing)
            {
                return;
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void lblDragFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (this.isProcessing)
            {
                return;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string droppedFile in files)
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(@droppedFile);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        // It's a directory - get list of all files inside it recursively
                        List<string> subFiles = allExt
                                .SelectMany(i => Directory.EnumerateFiles(droppedFile, "*" + i, SearchOption.AllDirectories))
                                .ToList();

                        foreach (string subFile in subFiles)
                        {
                            if (!this.fileList.Contains(subFile))
                            {
                                this.fileList.Add(subFile);
                            }
                        }
                    }
                    else
                    {
                        // It's a file
                        string thisExt = Path.GetExtension(droppedFile).ToLower();
                        if (allExt.Contains(thisExt) && !this.fileList.Contains(droppedFile))
                        {
                            this.fileList.Add(droppedFile);
                        }
                    }
                }
                catch { Console.WriteLine("Failed to read a dropped file."); }
            }
                
            SetFileCount();
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            this.fileList = new List<string>();
            SetFileCount();
        }

        private void cbxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            outTypeOptions currentOption = (outTypeOptions)this.cbxOutputType.SelectedIndex;
            if (currentOption == outTypeOptions.match ||
                currentOption == outTypeOptions.JPG)
            {
                this.lblQuality.Visible = true;
                this.nudQuality.Visible = true;
            }
            else
            {
                this.lblQuality.Visible = false;
                this.nudQuality.Visible = false;
            }
        }

        private void btnShowSelected_Click(object sender, EventArgs e)
        {
            string dispString = "";
            int count = 0;
            foreach (string filename in this.fileList)
            {
                dispString += (filename + "\r\n");
                count++;
                if (count >= 50)
                {
                    dispString += String.Format("...and {0} more files.", (this.fileList.Count - count).ToString());
                    break;
                }
            }
            MessageBox.Show(dispString, "Currently Selected Files", MessageBoxButtons.OK);
        }

    }
}
