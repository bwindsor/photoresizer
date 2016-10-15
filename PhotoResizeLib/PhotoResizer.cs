﻿using System;
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

    public class ProgressEventArgs
    {
        public double totalProgress { get; private set; }
        public double fileProgress { get; private set; }
        public string currentFile { get; private set; }
        public int filesComplete { get; private set; }
        public int totalFiles { get; private set; }

        public ProgressEventArgs(double fileProgress, int filesComplete, int totalFiles, string currentFile)
        {
            this.totalProgress = (double)filesComplete / (double)totalFiles;
            this.fileProgress = fileProgress;
            this.currentFile = currentFile;
            this.filesComplete = filesComplete;
            this.totalFiles = totalFiles;
        }
    }

    /// <summary>
    /// Thrown if a file already exists and it is expected not to.
    /// </summary>
    public class FileAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the FileAlreadyExistsException class
        /// </summary>
        public FileAlreadyExistsException() : base()
        { }

        /// <summary>
        /// Initialises a new instance of the FileAlreadyExistsException class with the message message
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public FileAlreadyExistsException(string message) : base(message)
        {
        }
    }

    public class MediaProcessorOptions
    {
        public comboOptions imageResizeType { get; private set; }
        public comboOptions videoResizeType { get; private set; }
        public outTypeOptions imageOutType { get; private set; }
        public videoOutTypeOptions videoOutType { get; private set; }
        public int imageResizeValue { get; private set; }
        public int videoResizeValue { get; private set; }
        public int jpegQuality { get; private set; }
        public int videoQuality { get; private set; }

        public MediaProcessorOptions(comboOptions imageResizeType = comboOptions.percent, 
                                     comboOptions videoResizeType = comboOptions.percent,
                                     outTypeOptions imageOutType = outTypeOptions.JPG,
                                     videoOutTypeOptions videoOutType = videoOutTypeOptions.WMV,
                                     int imageResizeValue = 100,
                                     int videoResizeValue = 100,
                                     int jpegQuality = 98,
                                     int videoQuality = 100)
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
    }

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
    /// Manages the processing of a set of media files.
    /// </summary>
    public class MediaProcessor
    {
        public bool isProcessing { get; private set; } = false;
        private static string outputSubfolderName = "Resized";
        private static string[] imageExt = { ".jpg", ".png", ".jpeg", ".gif", ".tif", ".tiff", ".bmp"};
        private static string[] videoExt = { ".avi", ".mov", ".wmv" };
        private CancellationToken cancelToken;
        private delegate void VideoProgressDelegateCallback(object sender, EncodeProgressEventArgs e);
        private List<string> fileList = new List<string>(); // List of files to process
        private List<string> failedList = new List<string>(); // List of files which failed
        private List<string> trimFiles = new List<string>();
        private List<Tuple<double, double>> trimRanges = new List<Tuple<double, double>>();
        private MediaProcessorOptions options = null;
        private ProgressCallback onProgress = null;
        private int filesComplete = 0;
        private double fileProgress = 0;
        private string currentFile = "";

        public MediaProcessor()
        {
            
        }

        public void SetOptions(MediaProcessorOptions options)
        {
            this.options = options;
        }

        public int GetNumFiles()
        {
            return this.fileList.Count;
        }
        
        private void DoOnProgress(object sender, ProgressEventArgs e)
        {
            if (this.onProgress != null)
            {
                if (e.fileProgress >= 0)
                {
                    this.fileProgress = e.fileProgress;
                }
                if (e.filesComplete >= 0)
                {
                    this.filesComplete = e.filesComplete;
                }
                if (e.currentFile != "")
                {
                    this.currentFile = e.currentFile;
                }
                this.onProgress(this, new ProgressEventArgs(this.fileProgress, this.filesComplete, this.GetNumFiles(), this.currentFile));
            }
        }

        public ProcessingResult GetResult()
        {
            return new ProcessingResult(this.fileList, this.failedList);
        }

        /// <summary>
        /// Runs the processing on this instance of the MediaProcessor
        /// </summary>
        public void Run(CancellationToken token, ProgressCallback onProgress = null)
        {
            if (this.options == null)
            {
                throw new Exception("Media processor options must be set before MediaProcessor can be run.");
            }
            this.onProgress = onProgress;
            this.OnProcessingStart();

            this.cancelToken = token;

            this.failedList = new List<string>();
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
                            outFilename = GetOutputFilename(this.fileList[ii], this.options.imageOutType);
                        }
                        else
                        {
                            outFilename = GetOutputFilename(this.fileList[ii], this.options.videoOutType);
                        }
                        MediaProcessor.CheckFiles(this.fileList[ii], outFilename);
                    }
                    catch (FileAlreadyExistsException exept)
                    {
                        // TODO - sort out the pre-check for files already existing instead of catching this
                        // cont = ShouldContinueWhenFileExists(exept.Message);
                        cont = true;
                    }

                    if (cont)
                    {
                        if (!IsVideoFile(this.fileList[ii]))
                        {
                            DoOnProgress(this, new ProgressEventArgs(0, 0, this.GetNumFiles(), this.fileList[ii]));
                            MediaProcessor.ProcessImageFile(this.fileList[ii], outFilename, 
                                        this.options.imageResizeValue, this.options.imageResizeType,
                                        this.options.imageOutType, this.options.jpegQuality);
                        }
                        else
                        {
                            Tuple<double, double> trimRange = null;
                            int idx = this.trimFiles.FindIndex(a => a == this.fileList[ii]);
                            if (idx >= 0)
                            {
                                trimRange = this.trimRanges[idx];
                            }
                            DoOnProgress(this, new ProgressEventArgs(0, 0, this.GetNumFiles(), this.fileList[ii]));
                            MediaProcessor.ProcessVideoFile(this.fileList[ii], outFilename, 
                                            this.options.videoResizeValue, this.options.videoResizeType, 
                                            this.options.videoOutType, this.options.videoQuality, trimRange, OnVideoEncodeProgress);
                        }
                    }
                    if (token.IsCancellationRequested) { break; }
                    DoOnProgress(this, new ProgressEventArgs(0, (ii + 1), this.GetNumFiles(), this.fileList[ii]));
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
                this.OnProcessingComplete();
            }
        }
        private void OnProcessingStart()
        {
            this.isProcessing = true;
            this.DoOnProgress(this, new ProgressEventArgs(0, 0, this.GetNumFiles(), "."));
        }

        private void OnProcessingCancelled()
        {
            this.isProcessing = false;
        }
        private void OnProcessingComplete()
        {
            this.isProcessing = false;
            this.DoOnProgress(this, new ProgressEventArgs(100, 100, this.GetNumFiles(), "."));
        }

        public void AddFiles(string[] files)
        {
            AddNewFilesToList(files, MediaProcessor.GetAllAllowedExtensions(), this.fileList);
        }
        public bool IsInList(string file)
        {
            return this.fileList.Contains(file);
        }
        public bool HasAllowedVideoExt(string file)
        {
            string thisExt = Path.GetExtension(file).ToLower();
            return MediaProcessor.GetAllAllowedVideoExtensions().Contains(thisExt);
        }
        public string[] GetAllowedVideoFiles(string[] files)
        {
            List<string> allowed = new List<string>();
            for (int ii = 0; ii < files.Length; ii++)
            {
                if (HasAllowedVideoExt(files[ii]))
                {
                    allowed.Add(files[ii]);
                }
            }
            return allowed.ToArray();
        }
        public void AddTrimmedVideo(string file, Tuple<double, double> trimRange)
        {
            this.trimRanges.Add(trimRange);
            this.trimFiles.Add(file);
            this.fileList.Add(file);
        }
        public void ClearFiles()
        {
            this.fileList = new List<string>();
            this.trimFiles = new List<string>();
            this.trimRanges = new List<Tuple<double, double>>();
        }
        public List<string> GetFileList()
        {
            return this.fileList;
        }

        private void OnVideoEncodeProgress(object sender, EncodeProgressEventArgs e)
        {
            this.DoOnProgress(this, new ProgressEventArgs(
                (int)(((e.CurrentPass - 1) * 100 + e.Progress) / e.TotalPasses),
                -1,
                this.GetNumFiles(),
                e.CurrentItem.SourceFileName));
        }

        /// <summary>
        /// Gets a string array of all allowable file extensions.
        /// </summary>
        /// <returns>String array of all allowable file extensions.</returns>
        public static string[] GetAllAllowedExtensions()
        {
            string[] allExt = new string[MediaProcessor.imageExt.Length + MediaProcessor.videoExt.Length];
            imageExt.CopyTo(allExt, 0);
            videoExt.CopyTo(allExt, MediaProcessor.imageExt.Length);
            return allExt;
        }
        /// <summary>
        /// Gets a string array of all allowable image file extensions.
        /// </summary>
        /// <returns>String array of all allowable image file extensions.</returns>
        public static string[] GetAllAllowedImageExtensions()
        {
            return MediaProcessor.imageExt;
        }
        /// <summary>
        /// Gets a string array of all allowable video file extensions.
        /// </summary>
        /// <returns>String array of all allowable video file extensions.</returns>
        public static string[] GetAllAllowedVideoExtensions()
        {
            return MediaProcessor.videoExt;
        }

        /// <summary>
        /// Checks the input and output files. The input file must exist and the output file must not.
        /// </summary>
        /// <param name="inputFilename">The full path to the input file.</param>
        /// <param name="outFile">The full path to the output file.</param>
        /// <returns>Void. Throws exceptions if conditions are not satisified.</returns>
        public static void CheckFiles(string inputFilename, string outFile)
        {
            if (!File.Exists(inputFilename))
            {
                throw new FileNotFoundException();
            }
            if (File.Exists(outFile))
            {
                throw new FileAlreadyExistsException(outFile + " already exists");
            }
            EnsureDir(outFile); // Will throw exception if can't create folder
        }

        /// <summary>
        /// Resize and save an image file.
        /// </summary>
        /// <param name="filename">Full path to the image to resize.</param>
        /// <param name="outFile">Full path to where the output file should be saved.</param>
        /// <param name="resizeValue">The height, width, or precentage to resize to, as specified by the resizeOption parameter.</param>
        /// <param name="resizeOption">The type of resize which resizeValue is referring to.</param>
        /// <param name="outTypeOption">The output image file type.</param>
        /// <param name="jpegQuality">Only used if outTypeOption is JPEG, the quality parameter of the output JPEG image</param>
        /// <returns>Void.</returns>
        public static void ProcessImageFile(string filename, string outFile, int resizeValue, comboOptions resizeOption,
                                    outTypeOptions outTypeOption, int jpegQuality = 98)
        {
            // Read file - will throw exception if file doesn't exist
            Image img = Image.FromFile(filename);

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
                    case ".jpg":
                    case ".jpeg":
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
                    case ".tif":
                    case ".tiff":
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

        /// <summary>
        /// Resize, trim, and save a video file.
        /// </summary>
        /// <param name="filename">Full path to the video to resize.</param>
        /// <param name="outFile">Full path to where the output file should be saved.</param>
        /// <param name="resizeValue">The height, width, or precentage to resize to, as specified by the resizeOption parameter.</param>
        /// <param name="resizeOption">The type of resize which resizeValue is referring to.</param>
        /// <param name="outTypeOption">The output video file type.</param>
        /// <param name="videoQuality">The quality of the output video. A value of 100 means 8kbps for full HD (1980x1080)</param>
        /// <param name="trimRange">The range, in seconds, to trim the video clip to. Set to null to not trim the clip.</param>
        /// <param name="jobProgressCallback">Delegate function which is called at regular intervals for progress updates during the job.</param>
        /// <returns>Void.</returns>
        private static void ProcessVideoFile(string filename, string outFile, int resizeValue, comboOptions resizeOption,
                                    videoOutTypeOptions outTypeOption = videoOutTypeOptions.WMV,
                                    int videoQuality = 100,
                                    Tuple<double, double> trimRange = null,
                                    VideoProgressDelegateCallback jobProgressCallback = null)
        {
            Job j = new Job();
            MediaItem mediaItem = new MediaItem(filename);
            var originalSize = mediaItem.OriginalVideoSize;
            // Get new dimensions
            Tuple<int, int> newSize = GetNewSize(resizeOption, originalSize.Width, originalSize.Height, resizeValue);
            // Round to the nearest 4 pixels - this is required by encoder
            // Encoder says the value must be an even integer between 64 and 4096 and a multiple of 4
            int newWidth = Convert.ToInt32(Math.Round(newSize.Item1 / 4.0) * 4);
            int newHeight = Convert.ToInt32(Math.Round(newSize.Item2 / 4.0) * 4);
            if (newWidth < 64 || newHeight < 64 || newWidth > 4096 || newHeight > 4096)
            {
                throw new Exception("New height and width must be between 64 and 4096 pixels");
            }

            double bitsPerSecondPerPixel = 8000.0 / 2000000.0;  // Assume 8kbps for full HD
            int bitRate = Convert.ToInt32(bitsPerSecondPerPixel * videoQuality * newWidth * newHeight / 100);

            WindowsMediaOutputFormat outFormat = new WindowsMediaOutputFormat();
            outFormat.AudioProfile = new Microsoft.Expression.Encoder.Profiles.WmaAudioProfile();
            outFormat.VideoProfile = new Microsoft.Expression.Encoder.Profiles.AdvancedVC1VideoProfile();
            outFormat.VideoProfile.AspectRatio = mediaItem.OriginalAspectRatio;
            outFormat.VideoProfile.AutoFit = true;
            outFormat.VideoProfile.Bitrate = new Microsoft.Expression.Encoder.Profiles.VariableUnconstrainedBitrate(bitRate);
            outFormat.VideoProfile.Size = new Size(newWidth, newHeight);

            mediaItem.VideoResizeMode = VideoResizeMode.Letterbox;
            mediaItem.OutputFormat = outFormat;

            if (!(trimRange == null))
            {
                Source source = mediaItem.Sources[0];
                source.Clips[0].StartTime = TimeSpan.FromSeconds(trimRange.Item1);
                source.Clips[0].EndTime = TimeSpan.FromSeconds(trimRange.Item2);
            }

            mediaItem.OutputFileName = Path.GetFileName(outFile);

            j.MediaItems.Add(mediaItem);
            j.CreateSubfolder = false;
            j.OutputDirectory = Path.GetDirectoryName(outFile);

            if (jobProgressCallback != null)
            {
                j.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(jobProgressCallback);
            }
            j.Encode();
            j.Dispose();
        }

        /// <summary>
        /// Ensure a directory exists for a file. If it doesn't exist, it will be created.
        /// </summary>
        /// <param name="filename">Full path to a file.</param>
        /// <returns>Void.</returns>
        private static void EnsureDir(string filename)
        {
            string dirName = Path.GetDirectoryName(filename);
            // Ensures the directory for a given filename exists
            if (!Directory.Exists(dirName)) { Directory.CreateDirectory(dirName); }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private static Bitmap ResizeImage(Image image, int width, int height)
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

        /// <summary>
        /// Get the new dimensions of a photo or video based on the options requested.
        /// </summary>
        /// <param name="resizeOption">How to interpret the parameter resizeValue.</param>
        /// <param name="width">The width of the original media.</param>
        /// <param name="height">The height of the original media.</param>
        /// <param name="resizeValue">The value to resize to interpreted in the way specified by resizeOption</param>
        /// <returns>A tuple containing the dimensions of the new image.</returns>
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

        /// <summary>
        /// Gets an ImageCodecInfo object for a requested image encode type.
        /// </summary>
        /// <param name="mimeType">The mime type to return the image encoder for.</param>
        /// <returns>The requested image encoder. Returns null if no encoder is found.</returns>
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

        /// <summary>
        /// Returns whether a given file name is an image or a video (based on its extension)
        /// </summary>
        /// <param name="filename">Filename of the file to check.</param>
        /// <returns>A boolean which is true if it is an acceptable video file and false otherwise.</returns>
        private static bool IsVideoFile(string filename)
        {
            return MediaProcessor.GetAllAllowedVideoExtensions().Contains(Path.GetExtension(filename).ToLower());
        }

        /// <summary>
        /// Gets the output filename for a video file.
        /// </summary>
        /// <param name="filename">Full path of the input file.</param>
        /// <param name="outType">Output file type.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the output filename for an image file.
        /// </summary>
        /// <param name="filename">Full path of the input file.</param>
        /// <param name="outType">Output file type.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates an output filename based on an input filename and the output extension
        /// </summary>
        /// <param name="filename">Full path of the input file.</param>
        /// <param name="extension">Extension of the output file.</param>
        /// <returns>A path with the correct extension inside a "resized" folder.</returns>
        private static string _CreateOutputFilename(string filename, string extension)
        {
            return Path.Combine(Path.GetDirectoryName(filename), MediaProcessor.outputSubfolderName, string.Concat(Path.GetFileNameWithoutExtension(filename), extension));
        }


        /// <summary>
        /// Adds an array of new files to a list, ignoring it if it is already in the list or if it does not have a valid extension.
        /// </summary>
        /// <param name="files">String array of files or folders to add. All subfolders and files within a folder will be added.</param>
        /// <param name="extensions">String array of allowable extensions</param>
        /// <param name="currentList">Current list of strings to add to.</param>
        private static void AddNewFilesToList(string[] files, string[] extensions, List<string> currentList)
        {
            // Since currentList is passed by reference we can just modify it

            foreach (string newFile in files)
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(newFile);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        // It's a directory - get list of all files inside it recursively
                        List<string> subFiles = extensions
                                .SelectMany(i => Directory.EnumerateFiles(newFile, "*" + i, SearchOption.AllDirectories))
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
                        string thisExt = Path.GetExtension(newFile).ToLower();
                        if (extensions.Contains(thisExt) && !currentList.Contains(newFile))
                        {
                            currentList.Add(newFile);
                        }
                    }
                }
                catch { Console.WriteLine("Failed to read a dropped file."); }
            }
        }
    }
}
