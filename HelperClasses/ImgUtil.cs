using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.HelperClasses
{
    public static class ImgUtil
    {
        public static WriteableBitmap GetWritableBitmapFromBitmap(Bitmap bmp) //used to instead have argument string absoluteFilePath
        {
            BitmapSource bmpSrc = GetBitmapSource(bmp);
            return new WriteableBitmap(bmpSrc);
        }
        public static WriteableBitmap GetWriteableBitmapFromAbsURI(string absoluteFilePath)  //absoluteURI can basically be the absoluteFilePath
        {
            //BitmapImage is a derived class of BitmapSource, so we can use it to create our WriteableBitmap.
            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = new Uri(absoluteFilePath, UriKind.Absolute);
            bi.EndInit();
            return new WriteableBitmap(bi);
        }

        public static System.Drawing.Bitmap GetBitmapFromWriteableBitmap(WriteableBitmap writeBmp) //Sourced from https://stackoverflow.com/questions/17298034/converting-writeablebitmap-to-bitmap-in-c-sharp
        {
            System.Drawing.Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }

        private static BitmapSource GetBitmapSource(Bitmap bmp)
        {
            var bitmapData = bmp.LockBits(
              new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
              System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            System.Windows.Media.PixelFormat PixForm = ConvertImagePF2MediaPF(bmp.PixelFormat);//PixelFormats.Bgra32;//bmp.PixelFormat,//PixelFormats.Bgra32,//bitmapData.PixelFormat,
            BitmapSource bitmapSource= BitmapSource.Create(bitmapData.Width,
                                                     bitmapData.Height,
                                                     bmp.HorizontalResolution,
                                                     bmp.VerticalResolution,
                                                     PixForm,
                                                     null,
                                                     bitmapData.Scan0,
                                                     bitmapData.Stride * bitmapData.Height,
                                                     bitmapData.Stride);
            //The following worked ok but I replaced it with the above just because I felt that would be the most appropriate image overall as it's based 100% on all the attributes of the original bitmap.
            //var bitmapSource = BitmapSource.Create(
            //   bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
            //   bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);
            bmp.UnlockBits(bitmapData);
            return bitmapSource;
        }

        private static System.Windows.Media.PixelFormat ConvertImagePF2MediaPF(this System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            //This function was sourced from: https://github.com/mathieumack/PixelFormatsConverter/blob/master/PixelFormatsConverter/PixelFormatsConverter/PixelFormatConverterExtensions.cs
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                return System.Windows.Media.PixelFormats.Gray16;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb555)
                return System.Windows.Media.PixelFormats.Bgr555;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                return System.Windows.Media.PixelFormats.Bgr565;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Indexed)
                return System.Windows.Media.PixelFormats.Bgr101010;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format1bppIndexed)
                return System.Windows.Media.PixelFormats.Indexed1;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed)
                return System.Windows.Media.PixelFormats.Indexed4;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                return System.Windows.Media.PixelFormats.Indexed8;

            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppArgb1555)
            //    return System.Windows.Media.PixelFormats.Bgr101010;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                return System.Windows.Media.PixelFormats.Bgr24;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                return System.Windows.Media.PixelFormats.Bgr32;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                return System.Windows.Media.PixelFormats.Pbgra32;
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb)
                return System.Windows.Media.PixelFormats.Bgr32;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format48bppRgb)
                return System.Windows.Media.PixelFormats.Rgb48;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format64bppArgb)
                return System.Windows.Media.PixelFormats.Prgba64;

            // TODO :
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Alpha)
            //    return System.Windows.Media.PixelFormats.;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Canonical)
            //    return System.Windows.Media.PixelFormats.;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.DontCare)
            //    return System.Windows.Media.PixelFormats.;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Extended)
            //    return System.Windows.Media.PixelFormats.;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Gdi)
            //    return System.Windows.Media.PixelFormats.Default;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.Max)
            //    return System.Windows.Media.PixelFormats.Default;
            //if (pixelFormat == System.Drawing.Imaging.PixelFormat.PAlpha)
            //    return System.Windows.Media.PixelFormats.Default;

            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Undefined)
                return System.Windows.Media.PixelFormats.Default;

            throw new NotSupportedException("Convertion not supported with " + pixelFormat.ToString());
        }

    }
}
