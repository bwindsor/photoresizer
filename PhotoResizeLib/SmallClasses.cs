using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Expression.Encoder;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace PhotoResizeLib
{

    /// <summary>
    /// Specifies the way in which a resize value is interpreted (percentage scaling, fixed width, or fixed height)
    /// </summary>
    public enum comboOptions : int { percent, height, width };
    /// <summary>
    /// Specifies the output format of an image file.
    /// </summary>
    public enum outTypeOptions : int { match, JPG, PNG, BMP, GIF, TIF };
    /// <summary>
    /// Specifies the output format of a video file.
    /// </summary>
    public enum videoOutTypeOptions : int { WMV };

    /// <summary>
    /// Delegate function which is called to update during processing.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ProgressCallback(object sender, ProgressEventArgs e);

    /// <summary>
    /// Contains arguments returned in the event data for when the processor makes progress.
    /// </summary>
    public class ProgressEventArgs
    {
        public double totalProgress { get; private set; }
        public double fileProgress { get; private set; }
        public string currentFile { get; private set; }
        public int filesComplete { get; private set; }
        public int totalFiles { get; private set; }

        public ProgressEventArgs(double fileProgress, int filesComplete, int totalFiles, string currentFile)
        {
            if (totalFiles == 0)
            {
                this.totalProgress = 0;
            }
            else
            {
                this.totalProgress = (double)filesComplete / (double)totalFiles * 100;
            }
            this.fileProgress = fileProgress;
            this.currentFile = currentFile;
            this.filesComplete = filesComplete;
            this.totalFiles = totalFiles;
        }
    }

    /// <summary>
    /// The set of options required by the processor to run.
    /// </summary>
    public class MediaProcessorOptions
    {
        public comboOptions imageResizeType { get; private set; } = comboOptions.percent;
        public comboOptions videoResizeType { get; private set; } = comboOptions.percent;
        public outTypeOptions imageOutType { get; private set; } = outTypeOptions.JPG;
        public videoOutTypeOptions videoOutType { get; private set; } = videoOutTypeOptions.WMV;
        public int imageResizeValue { get; private set; } = 100;
        public int videoResizeValue { get; private set; } = 100;
        public int jpegQuality { get; private set; } = 98;
        public int videoQuality { get; private set; } = 100;

        public MediaProcessorOptions(comboOptions imageResizeType,
                                     comboOptions videoResizeType,
                                     outTypeOptions imageOutType,
                                     videoOutTypeOptions videoOutType,
                                     int imageResizeValue,
                                     int videoResizeValue,
                                     int jpegQuality,
                                     int videoQuality)
        {
            this.imageResizeType = imageResizeType;
            this.videoResizeType = videoResizeType;
            this.imageOutType = imageOutType;
            this.videoOutType = videoOutType;
            this.imageResizeValue = imageResizeValue;
            this.videoResizeValue = videoResizeValue;
            this.jpegQuality = jpegQuality;
            this.videoQuality = videoQuality;
        }

        /// <summary>
        /// Parse a text value into a valid resize integer.
        /// </summary>
        /// <param name="resizeText">Text to parse</param>
        /// <param name="resizeType">How the text should be interpreted</param>
        /// <param name="result">Integer to parse the result into, if successful</param>
        /// <returns>Boolean indicating success.</returns>
        public static bool TryParseResizeValue(string resizeText, comboOptions resizeType, out int result)
        {
            int x;
            bool success = int.TryParse(resizeText, out x);

            if (success)
            {
                switch (resizeType)
                {
                    case comboOptions.percent:
                        if (x < 1 || x > 1000) { success = false; x = 0; }
                        break;
                    case comboOptions.height:
                        if (x < 1 || x > 10000) { success = false; x = 0; }
                        break;
                    case comboOptions.width:
                        if (x < 1 || x > 10000) { success = false; x = 0; }
                        break;
                };
            }

            result = x;
            return success;
        }
    }

    /// <summary>
    /// Contains information about the result of the processing once it is complete.
    /// </summary>
    public class ProcessingResult
    {
        public int numTotal { get; private set; }
        public int numFailed { get; private set; }
        public int numComplete { get; private set; }
        public List<string> fileList;
        public List<string> failedList;
        public ProcessingResult(List<string> fileList, List<string> failedList)
        {
            this.numTotal = fileList.Count;
            this.numFailed = failedList.Count;
            this.numComplete = this.numTotal - this.numFailed;
            this.fileList = fileList;
            this.failedList = failedList;
        }
    }

    /// <summary>
    /// Contains information about the result of validation of the list of files ready to be processed.
    /// </summary>
    public class FileValidationResult
    {
        public List<bool> ok { get; private set; }
        public List<string> inputFilename { get; private set; }
        public List<string> outputFilename { get; private set; }
        public List<bool> doesInputExist { get; private set; }
        public List<bool> doesOutputExist { get; private set; }
        public List<bool> isFolderSuccess { get; private set; }
        public List<string> noInputList { get; private set; }
        public List<string> outputExistsList { get; private set; }
        public List<string> folderFailedList { get; private set; }
        public string message { get; private set; }

        public FileValidationResult()
        {
            this.ok = new List<bool>();
            this.inputFilename = new List<string>();
            this.outputFilename = new List<string>();
            this.doesInputExist = new List<bool>();
            this.doesOutputExist = new List<bool>();
            this.isFolderSuccess = new List<bool>();
            this.noInputList = new List<string>();
            this.outputExistsList = new List<string>();
            this.folderFailedList = new List<string>();
            this.message = "";
        }

        public FileValidationResult(List<string> inputFilename, List<string> outputFilename,
                                    List<bool> doesInputExist,
                                    List<bool> doesOutputExist,
                                    List<bool> isFolderSuccess)
        {
            for (int ii = 0; ii < inputFilename.Count; ii++)
            {
                this.Push(inputFilename[ii], outputFilename[ii], doesInputExist[ii], doesOutputExist[ii], isFolderSuccess[ii]);
            }
        }
        public void Push(string inputFilename, string outputFilename, bool doesInputExist = true, bool doesOutputExist = false, bool isFolderSuccess = true)
        {
            this.inputFilename.Add(inputFilename);
            this.outputFilename.Add(outputFilename);
            this.doesInputExist.Add(doesInputExist);
            this.doesOutputExist.Add(doesOutputExist);
            this.isFolderSuccess.Add(isFolderSuccess);
            this.ok.Add(doesInputExist && !doesOutputExist && isFolderSuccess);
            if (!doesInputExist)
            {
                this.noInputList.Add(inputFilename);
            }
            if (doesOutputExist)
            {
                this.outputExistsList.Add(outputFilename);
            }
            if (!isFolderSuccess)
            {
                this.folderFailedList.Add(outputFilename);
            }
            this.CreateMessage();
        }
        private void CreateMessage()
        {
            string msg = "";
            if (this.noInputList.Count > 0)
            {
                msg += "Input file does not exist for the following files:\r\n";
                for (int ii = 0; ii < this.noInputList.Count; ii++)
                {
                    msg += (this.noInputList[ii] + "\r\n");
                }
            }
            if (this.folderFailedList.Count > 0)
            {
                msg += "Failed to create folders for the following outputs:\r\n";
                for (int ii = 0; ii < this.folderFailedList.Count; ii++)
                {
                    msg += (this.folderFailedList[ii] + "\r\n");
                }
            }
            if (this.outputExistsList.Count > 0)
            {
                msg += "The following output files already exist:\r\n";
                for (int ii = 0; ii < this.outputExistsList.Count; ii++)
                {
                    msg += (this.outputExistsList[ii] + "\r\n");
                }
            }
            this.message = msg;
        }
    }

}
