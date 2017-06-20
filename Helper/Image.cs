using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Helpers
{
    public static class Image
    {
        public static bool WriteImg(string filePath, string Base64Img)
        {
            bool result = false;
            try
            {
                int startIndex = Base64Img.IndexOf(",");
                var bytes = Convert.FromBase64String(Base64Img.Substring(startIndex + 1));
                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static string convertFileToBase64(string imgPath)
        {
            Byte[] bytes = File.ReadAllBytes(imgPath);
            String file = Convert.ToBase64String(bytes);
            return file;
        }

        //return images names with extention
        public static List<string> GetFolderImages(string folderPath)
        {
            List<String> LstImages = new List<string>();
            if (Directory.Exists(folderPath))
            {
                string[] fileEntries = Directory.GetFiles(folderPath);
                foreach (var file in fileEntries)
                {
                    // LstImages.Add("data:image/jpeg;base64," + convertFileToBase64(file));
                    LstImages.Add(Path.GetFileName(file));
                }
            }
            return LstImages;
        }

        public static List<string> GetFolderImagesPathes(string folderPath)
        {
            List<string> fileEntries = new List<string>();
            if (Directory.Exists(folderPath))
            {
               fileEntries = Directory.GetFiles(folderPath).ToList();
            }
            return fileEntries ;
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static bool ScaleImage(string SourceImagePath, int maxWidth, int maxHeight, int quality, string DestinationfilePath)
        {
            try
            {
                //Bitmap image = new Bitmap(System.Drawing.Image.FromFile(SourceImagePath));
                Bitmap image = null;
                using (var img = new Bitmap(SourceImagePath))
                {
                    image = new Bitmap(img);
                }
                // Get the image's original width and height
                int originalWidth = image.Width;
                int originalHeight = image.Height;

                // To preserve the aspect ratio
                float ratioX = (float)maxWidth / (float)originalWidth;
                float ratioY = (float)maxHeight / (float)originalHeight;
                float ratio = Math.Min(ratioX, ratioY);

                // New width and height based on aspect ratio
                int newWidth = (int)(originalWidth * ratio);
                int newHeight = (int)(originalHeight * ratio);

                // Convert other formats (including CMYK) to RGB.
                Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format16bppRgb555);

                // Draws the image in the specified size with quality mode set to HighQuality
                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                // Get an ImageCodecInfo object that represents the JPEG codec.
                ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

                // Create an Encoder object for the Quality parameter.
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                EncoderParameters encoderParameters = new EncoderParameters(1);

                // Save the image as a JPEG file with quality level.
                EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
                encoderParameters.Param[0] = encoderParameter;
                newImage.Save(DestinationfilePath, imageCodecInfo, encoderParameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool scaleFolderImages(string SourceFolderPath, int maxWidth, int maxHeight, int quality, string DestinationfolderPath)
        {
            try
            {
                List<string> imagesPathes = GetFolderImagesPathes(SourceFolderPath);
                List<string> imagesNames = GetFolderImages(SourceFolderPath);

                for(int i=0; i<imagesPathes.Count() ;i++)
                {
                    ScaleImage(imagesPathes[i], maxWidth, maxHeight, quality, DestinationfolderPath + @"\" + imagesNames[i]);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
