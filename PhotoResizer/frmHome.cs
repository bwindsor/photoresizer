using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Expression.Encoder;
using PhotoResizeLib;

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

        private List<string> fileList = new List<string>(); // List of files to process
        private List<string> trimFiles = new List<string>();
        private List<Tuple<double, double>> trimRanges = new List<Tuple<double, double>>();
        
        private int[] comboTxtDefaults = { 50, 1000, 2000 };
        private int comboLastIdx = 0;
        private bool isProcessing = false;
        private CancellationTokenSource cancelSource = new CancellationTokenSource();

        public frmHome()
        {
            InitializeComponent();
            this.cbxResizeType.SelectedIndex = 0;
            this.cbxOutputType.SelectedIndex = 0;
            this.txtResize.Text = this.comboTxtDefaults[this.cbxResizeType.SelectedIndex].ToString();
            this.cbxVideoOutputType.SelectedIndex = 0;
            this.cbxVideoResizeType.SelectedIndex = 0;
            this.lblDragFiles.DragEnter += new DragEventHandler(lblDragFiles_DragEnter);
            this.lblDragFiles.DragDrop += new DragEventHandler(lblDragFiles_DragDrop);
            this.lblDragTrimVideo.DragEnter += new DragEventHandler(lblDragTrimVideo_DragEnter);
            this.lblDragTrimVideo.DragDrop += new DragEventHandler(lblDragTrimVideo_DragDrop);
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
                       string outFilename = "";
                       bool cont = true;
                       bool isVideo = IsVideoFile(this.fileList[ii]);
                       try
                       {
                           if (isVideo)
                           {
                               outFilename = GetOutputFilename(this.fileList[ii], outTypeOption);
                           }
                           else
                           {
                               outFilename = GetOutputFilename(this.fileList[ii], videoOutTypeOption);
                           }
                           MediaProcessor.CheckFiles(this.fileList[ii], outFilename);
                       }
                       catch (FileAlreadyExistsException exept)
                       {
                           cont = ShouldContinueWhenFileExists(exept.Message);
                       }

                        if (cont)
                        {
                            if (!IsVideoFile(this.fileList[ii]))
                            {
                                SetCurrentProgress(0, this.fileList[ii]);
                                MediaProcessor.ProcessImageFile(this.fileList[ii], outFilename, resizeValue, comboOption, outTypeOption, jpegQuality);
                            }
                            else
                            {
                                Tuple<double, double> trimRange = null;
                                int idx = this.trimFiles.FindIndex(a => a == this.fileList[ii]);
                                if (idx >= 0)
                                {
                                    trimRange = this.trimRanges[idx];
                                }
                                SetCurrentProgress(0, this.fileList[ii]);
                                MediaProcessor.ProcessVideoFile(this.fileList[ii], outFilename, videoResizeValue, videoComboOption, videoOutTypeOption, mpegQuality, trimRange, OnJobEncodeProgress);
                            }
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

        private bool ShouldContinueWhenFileExists(string message)
        {
            DialogResult result = MessageBox.Show("File " + message + ". \r\nYes to overwrite\r\nNo to skip this file\r\nCancel to stop processing", "File already exists", MessageBoxButtons.YesNoCancel);
            switch (result)
            {
                case DialogResult.Yes:
                    return true;
                case DialogResult.No:
                    return false;
                case DialogResult.Cancel:
                    this.cancelSource.Cancel();
                    return false;
                default:
                    return false;
            }
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
                SetCurrentProgress(100, "");
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

        
        private void OnJobEncodeProgress(object sender, EncodeProgressEventArgs e)
        {
            SetCurrentProgress((int)(((e.CurrentPass - 1) * 100 + e.Progress) / e.TotalPasses), 
                                e.CurrentItem.SourceFileName);
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
                if (numTotal == 0)
                {
                    pbar1.Value = 0;
                }
                else
                {
                    pbar1.Value = numComplete * pbar1.Maximum / numTotal;
                }
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

            int oldCount = this.fileList.Count;
            AddNewFilesToList(files, allExt, this.fileList);

            int numNew = this.fileList.Count - oldCount;
            int numIgnored = files.Length - numNew;
            this.statusInfo.Text = numNew.ToString() + " new files were added. " + numIgnored.ToString() + " were ignored.";
            SetFileCount();
        }
        private void lblDragTrimVideo_DragEnter(object sender, DragEventArgs e)
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
        private void lblDragTrimVideo_DragDrop(object sender, DragEventArgs e)
        {
            if (this.isProcessing)
            {
                return;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> allowedFiles = new List<string>();
            AddNewFilesToList(files, videoExt, allowedFiles);
            if (allowedFiles.Count == 0)
            {
                this.statusInfo.Text = "None of the dragged files were supported video files.";
                return;
            }

            string filename = allowedFiles[0];
            if (this.fileList.Contains(filename))
            {
                this.statusInfo.Text = "Dragged file is already in the list.";
                return;
            }
            
            frmVideoTrim trimmer = new frmVideoTrim(filename);
            DialogResult dialogResult = trimmer.ShowDialog(this);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    Tuple<double, double> startStop = trimmer.GetTimeRange();
                    this.trimRanges.Add(startStop);
                    this.trimFiles.Add(filename);
                    this.fileList.Add(filename);
                    this.statusInfo.Text = "1 new file was added.";
                    SetFileCount();
                    break;
                default:
                    this.statusInfo.Text = "0 new files were added. Trimming was cancelled.";
                    break;
            }
            trimmer.Dispose();
        }

        private static void AddNewFilesToList(string[] files, string[] extensions, List<string>currentList)
        {
            // Since currentList is passed by reference we can just modify it

            foreach (string droppedFile in files)
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(@droppedFile);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        // It's a directory - get list of all files inside it recursively
                        List<string> subFiles = extensions
                                .SelectMany(i => Directory.EnumerateFiles(droppedFile, "*" + i, SearchOption.AllDirectories))
                                .ToList();

                        foreach (string subFile in subFiles)
                        {
                            if (!currentList.Contains(subFile))
                            {
                                currentList.Add(subFile);
                            }
                        }
                    }
                    else
                    {
                        // It's a file
                        string thisExt = Path.GetExtension(droppedFile).ToLower();
                        if (extensions.Contains(thisExt) && !currentList.Contains(droppedFile))
                        {
                            currentList.Add(droppedFile);
                        }
                    }
                }
                catch { Console.WriteLine("Failed to read a dropped file."); }
            }
        }


        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            this.fileList = new List<string>();
            this.trimFiles = new List<string>();
            this.trimRanges = new List<Tuple<double, double>>();
            SetFileCount();
            SetProgress(0);
            SetCurrentProgress(0, "(processing not started)");
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
