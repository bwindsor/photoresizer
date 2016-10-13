using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Expression.Encoder;
using System.Drawing.Drawing2D;

namespace PhotoResizeLib
{

    public enum comboOptions : int { percent, height, width };
    public enum outTypeOptions : int { match, JPG, PNG, BMP, GIF, TIF };
    public enum videoOutTypeOptions : int { WMV };
    public delegate void VideoProgressDelegateCallback(object sender, EncodeProgressEventArgs e);

    public class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException() : base()
        { }
        public FileAlreadyExistsException(string message) : base(message)
        {
        }
    }

    public class MediaProcessor
    {
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

        public static void ProcessImageFile(string filename, string outFile, int resizeValue, comboOptions resizeOption,
                                    outTypeOptions outTypeOption, int jpegQuality)
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

        public static void ProcessVideoFile(string filename, string outFile, int resizeValue, comboOptions resizeOption,
                                    videoOutTypeOptions outTypeOption = videoOutTypeOptions.WMV,
                                    int mpegQuality = 100,
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
            int bitRate = Convert.ToInt32(bitsPerSecondPerPixel * mpegQuality * newWidth * newHeight / 100);

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
    }
}
