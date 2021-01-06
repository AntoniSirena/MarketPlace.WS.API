using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace JS.Utilities
{
    public static class JS_File
    {

        public static string GetStrigBase64(string path)
        {
            string result = string.Empty;

            if (!String.IsNullOrEmpty(path))
            {
                byte[] file = File.ReadAllBytes(path);
                result = Convert.ToBase64String(file);
            }

            return result;
        }

        public static byte[] GetImgBytes(string path)
        {
            byte[] result = File.ReadAllBytes(path);

            return result;
        }

        public static void DownloadFileImg(byte[] file, string name = "")
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "aplication/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.BinaryWrite(file);
            HttpContext.Current.Response.End();
        }

        public static byte[] ResizeImage(System.Drawing.Image image, int width, int height)
        {
            byte[] result = null;

            var destRect = new Rectangle(0, 0, width, height);
            var imageResult = new Bitmap(width, height);

            ImageFormat originalFormat = image.RawFormat;

            imageResult.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(imageResult))
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

            //Convert Bitmap to array byte
            MemoryStream stream = new MemoryStream();
            imageResult.Save(stream, originalFormat);
            result = stream.ToArray();

            return result;
        }

    }
}
