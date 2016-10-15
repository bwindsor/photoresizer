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
        delegate void SetLblProgressCallback(ProcessingResult result);
        delegate void SetProgressDelegate(ProgressEventArgs e);
        delegate void ProcessingCancelledDelegate();
        delegate void SetCurrentProgressDelegate(int currentProgress, string currentFile);
        
        private int[] comboTxtDefaults = { 50, 1000, 2000 };
        private int comboLastIdx = 0;
        private CancellationTokenSource cancelSource = new CancellationTokenSource();
        private MediaProcessor MP = new MediaProcessor();

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

            if (MP.isProcessing)
            {
                this.cancelSource.Cancel();
                return;
            }
            if (MP.GetNumFiles() == 0)
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

            MediaProcessorOptions options = new MediaProcessorOptions(
                imageResizeType: (comboOptions)this.cbxResizeType.SelectedIndex,
                videoResizeType: (comboOptions)this.cbxVideoResizeType.SelectedIndex,
                imageOutType: (outTypeOptions)this.cbxOutputType.SelectedIndex,
                videoOutType: (videoOutTypeOptions)this.cbxVideoOutputType.SelectedIndex,
                imageResizeValue: resizeValue,
                videoResizeValue: videoResizeValue,
                jpegQuality: (int)nudQuality.Value,
                videoQuality: (int)nudVideoQuality.Value
            );
            MP.SetOptions(options);
            CancellationToken token = this.cancelSource.Token;

            Task processingTask = Task.Factory.StartNew(() =>
            {
                MP.Run(token, UpdateOnProgress);
                if (token.IsCancellationRequested)
                {
                    this.OnProcessingCancelled();
                }
                else
                {
                    this.OnProcessingComplete(MP.GetResult());
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

        private void OnProcessingComplete(ProcessingResult result)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblProgress.InvokeRequired)
            {
                SetLblProgressCallback d = new SetLblProgressCallback(OnProcessingComplete);
                this.Invoke(d, new object[] { result.numFailed });
            }
            else
            {
                lblProgress.Text = string.Format("Processing complete on {0} of {1} files. {2} files failed.",
                                result.numComplete.ToString(),
                                result.numTotal.ToString(),
                                result.numFailed.ToString());
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
                this.btnProcess.Text = "Process Files";
                SetControlsEnable(true);
                this.cancelSource = new CancellationTokenSource(); // reset cancel token
            }
        }
        
        private void SetFileCount()
        {
            lblNumFiles.Text = MP.GetNumFiles().ToString() + " files selected.";
        }

        private void UpdateOnProgress(object sender, ProgressEventArgs e)
        {
            SetProgress(e);
        }

        private void SetProgress(ProgressEventArgs e)
        {
            if (this.lblProgress.InvokeRequired)
            {
                SetProgressDelegate d = new SetProgressDelegate(SetProgress);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                lblProgress.Text = "Processed " + e.filesComplete.ToString() + " of " + e.totalFiles.ToString() + " files.";
                if (e.totalFiles == 0)
                {
                    pbar1.Value = 0;
                }
                else
                {
                    pbar1.Value = Convert.ToInt32(e.totalProgress * pbar1.Maximum);
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
                int numTotal = MP.GetNumFiles();
                lblFileProgress.Text = "Processing file " + currentFile;
                pbar2.Value = currentProgress;
            }
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
            if (MP.isProcessing)
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
            if (MP.isProcessing)
            {
                return;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            int oldCount = MP.GetNumFiles();
            MP.AddFiles(files);
            
            int numNew = MP.GetNumFiles() - oldCount;
            int numIgnored = files.Length - numNew;
            this.statusInfo.Text = numNew.ToString() + " new files were added. " + numIgnored.ToString() + " were ignored.";
            SetFileCount();
        }
        private void lblDragTrimVideo_DragEnter(object sender, DragEventArgs e)
        {
            if (MP.isProcessing)
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
            if (MP.isProcessing)
            {
                return;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            string[] allowedFiles = MP.GetAllowedVideoFiles(files);
            if (allowedFiles.Length == 0)
            {
                this.statusInfo.Text = "None of the dragged files were supported video files.";
                return;
            }

            string filename = allowedFiles[0];
            if (MP.IsInList(filename))
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
                    MP.AddTrimmedVideo(filename, startStop);
                    this.statusInfo.Text = "1 new file was added.";
                    SetFileCount();
                    break;
                default:
                    this.statusInfo.Text = "0 new files were added. Trimming was cancelled.";
                    break;
            }
            trimmer.Dispose();
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            SetFileCount();
            SetProgress(new ProgressEventArgs(0, 0, 0, ""));
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
            List<string> fileList = MP.GetFileList();
            string dispString = "";
            int count = 0;
            foreach (string filename in fileList)
            {
                dispString += (filename + "\r\n");
                count++;
                if (count >= 50)
                {
                    dispString += String.Format("...and {0} more files.", (fileList.Count - count).ToString());
                    break;
                }
            }
            MessageBox.Show(dispString, "Currently Selected Files", MessageBoxButtons.OK);
        }

    }
}
