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
using PhotoResizeLib;
using System.Drawing;

namespace PhotoResizer
{

    public partial class frmHome : Form
    {
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetLblProgressCallback(ProcessingResult result);
        delegate void SetProgressDelegate(ProgressEventArgs e);
        delegate void ProcessingCancelledDelegate();
        delegate void SetCurrentProgressDelegate(ProgressEventArgs e);
        
        private int[] comboTxtDefaults = { 50, 1000, 2000 };
        private int comboLastIdx = 0;
        private CancellationTokenSource cancelSource = new CancellationTokenSource();
        private MediaProcessor MP = new MediaProcessor();

        public frmHome()
        {
            InitializeComponent();

            this.cbxOutputType.Items.AddRange(OutputStringTypes.GetImageTypes());
            this.cbxVideoOutputType.Items.AddRange(OutputStringTypes.GetVideoTypes());
            this.cbxResizeType.SelectedIndex = 0;
            this.cbxOutputType.SelectedIndex = 0;
            this.txtResize.Text = this.comboTxtDefaults[this.cbxResizeType.SelectedIndex].ToString();
            this.cbxVideoOutputType.SelectedIndex = 0;
            this.cbxVideoResizeType.SelectedIndex = 0;

            this.lblDragFiles.DragEnter += new DragEventHandler(lblDragFiles_DragEnter);
            this.lblDragFiles.DragDrop += new DragEventHandler(lblDragFiles_DragDrop);
            this.lblDragTrimVideo.DragEnter += new DragEventHandler(lblDragTrimVideo_DragEnter);
            this.lblDragTrimVideo.DragDrop += new DragEventHandler(lblDragTrimVideo_DragDrop);

            string[] imageExt = MediaProcessor.GetAllAllowedImageExtensions();
            string[] videoExt = MediaProcessor.GetAllAllowedVideoExtensions();
            string allTooltip = "";
            string videoTooltip = "";
            for (int ii = 0; ii < imageExt.Length; ii++)
            {
                allTooltip += (imageExt[ii] + " ");
            }
            allTooltip += "\r\n";
            for (int ii = 0; ii < videoExt.Length; ii++)
            {
                allTooltip += (videoExt[ii] + " ");
                videoTooltip += (videoExt[ii] + " ");
            }
            this.toolTip1.SetToolTip(this.lblDragFiles, allTooltip);
            this.toolTip1.SetToolTip(this.lblDragTrimVideo, videoTooltip);
        }

        private bool TryReadDefaultCropRatio(out float? defaultCropRatio)
        {
            bool success = true;
            defaultCropRatio = null;
            if (this.txtRatio1.Text.Length > 0 && this.txtRatio2.Text.Length > 0)
            {
                float cropRatio;
                success = MediaProcessorOptions.TryParseCropRatio(this.txtRatio1.Text + ":" + this.txtRatio2.Text, out cropRatio);
                if (success)
                {
                    defaultCropRatio = cropRatio;
                }
            }
            return success;
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
            int resizeValue = 0;
            bool success = MediaProcessorOptions.TryParseResizeValue(this.txtResize.Text, (comboOptions)this.cbxResizeType.SelectedIndex, out resizeValue);
            if (!success)
            {
                MessageBox.Show("Invalid value entered for resizing images to.", "Invalid entry", MessageBoxButtons.OK);
                return;
            }
            int videoResizeValue = 0;
            success = MediaProcessorOptions.TryParseResizeValue(this.txtVideoResize.Text, (comboOptions)this.cbxVideoResizeType.SelectedIndex, out videoResizeValue);
            if (!success)
            {
                MessageBox.Show("Invalid value entered for resizing videos to.", "Invalid entry", MessageBoxButtons.OK);
                return;
            }

            success = this.TryReadDefaultCropRatio(out float? defaultCropRatio);
            if (!success)
            {
                MessageBox.Show("Invalid value entered for crop ratio.", "Invalid entry", MessageBoxButtons.OK);
                return;
            }
            
            MediaProcessorOptions options = new MediaProcessorOptions(
                (comboOptions)this.cbxResizeType.SelectedIndex,
                (comboOptions)this.cbxVideoResizeType.SelectedIndex,
                (outTypeOptions)this.cbxOutputType.SelectedIndex,
                (videoOutTypeOptions)this.cbxVideoOutputType.SelectedIndex,
                resizeValue,
                videoResizeValue,
                (int)nudQuality.Value,
                (int)nudVideoQuality.Value,
                defaultCropRatio
            );
            MP.SetOptions(options);
            MP.SetFileSuffix(txtSuffix.Text);

            FileValidationResult val = MP.ValidateFileList();
            if (val.noInputList.Count > 0 || val.folderFailedList.Count > 0)
            {
                MessageBox.Show(val.message, "Invalid file list", MessageBoxButtons.OK);
                return;
            }
            if (val.outputExistsList.Count > 0)
            {
                DialogResult res = MessageBox.Show("Overwrite these files?\r\n\r\n" + val.message, "Files already exist", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                {
                    return;
                }
            }

            CancellationToken token = this.cancelSource.Token;
            this.OnProcessingStart();
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
            this.btnShowSelected.Enabled = enbl;

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
                this.Invoke(d, new object[] { result });
            }
            else
            {
                lblProgress.Text = string.Format("Processing complete on {0} of {1} files. {2} files failed.",
                                result.numComplete.ToString(),
                                result.numTotal.ToString(),
                                result.numFailed.ToString());
                this.btnProcess.Text = "Process Files";
                SetControlsEnable(true);
                MP.ClearFiles();
                SetFileCount();
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
            SetCurrentProgress(e);
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
                    pbar1.Value = Convert.ToInt32(e.totalProgress * pbar1.Maximum / 100);
                }
            }
        }
        private void SetCurrentProgress(ProgressEventArgs e)
        {
            if (this.lblFileProgress.InvokeRequired)
            {
                SetCurrentProgressDelegate d = new SetCurrentProgressDelegate(SetCurrentProgress);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                int numTotal = MP.GetNumFiles();
                lblFileProgress.Text = "Processing file " + e.currentFile;
                pbar2.Value = Convert.ToInt32(e.fileProgress);
            }
        }

        private void cbxResizeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resizeValue = 0;
            bool success = MediaProcessorOptions.TryParseResizeValue(this.txtResize.Text, (comboOptions)this.comboLastIdx, out resizeValue);
            if (!success)
            {
                this.comboTxtDefaults[this.comboLastIdx] = resizeValue;
            }
            this.txtResize.Text = this.comboTxtDefaults[this.cbxResizeType.SelectedIndex].ToString();
            this.comboLastIdx = this.cbxResizeType.SelectedIndex;
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
            this.statusInfo.Text = numNew.ToString() + " new files were added.";
            if (numIgnored > 0)
            {
                this.statusInfo.Text += (" " + numIgnored.ToString() + " were ignored due to already being in the list or having an unsupported file type.");
            }
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

            using (frmVideoTrim trimmer = new frmVideoTrim(filename))
            {
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
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            MP.ClearFiles();
            SetFileCount();
            SetProgress(new ProgressEventArgs(0, 0, 0, ""));
            SetCurrentProgress(new ProgressEventArgs(0, 0, 0, "(processing not started)"));
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

        private void btnSetCropBoundaries_Click(object sender, EventArgs e)
        {
            if (this.MP.GetNumFiles() == 0)
            {
                MessageBox.Show("No files selected", "No files selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool success = this.TryReadDefaultCropRatio(out float? defaultCropRatio);
            if (!success)
            {
                MessageBox.Show("Invalid default crop ratio entered", "Invalid default crop ratio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (frmImageCrop cropper = new frmImageCrop(this.MP.GetFileList(), this.MP.GetCropBoundaries(), defaultCropRatio))
            {
                DialogResult dialogResult = cropper.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    var setBoundaries = cropper.GetCropBoundaries();
                    foreach (KeyValuePair<string, Rectangle> p in setBoundaries)
                    {
                        MP.SetCropBoundaryForFile(p.Key, p.Value);
                    }
                    this.statusInfo.Text = "Updated custom crop areas.";
                }
                else
                {
                    this.statusInfo.Text = "Custom crop areas were not updated. Cropping was cancelled.";
                }
            }
        }
    }
}
