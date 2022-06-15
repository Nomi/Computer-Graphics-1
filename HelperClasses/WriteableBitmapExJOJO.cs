using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Computer_Graphics_1;

namespace Computer_Graphics_1.HelperClasses.Extensions
{
    public static class WriteableBitmapExJOJO
    {
        public static unsafe IntPtr GetPixelIntPtrAt(this WriteableBitmap wbmp, int rowX, int colY) //can make it faster if I do the pixelNumChannel calculation only once and outside of this.
        {
            int pixelNumChannels = wbmp.GetPixelNumChannels8bit(); //Because of this, program supports only images with 8-bit color and BGR/BGRA channel layout Pixel formats for now. This command exists in order to maintain compatibility wiht both BGR and BGRA config layout.
            return (IntPtr)((long)wbmp.BackBuffer + rowX * wbmp.BackBufferStride + colY * pixelNumChannels);
        }

        public static unsafe Color GetPixelColor(this WriteableBitmap wbmp, int x, int y)
        {
            unsafe
            {
                _pixel_bgr24_bgra32* pxPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y, x);
                return Color.FromArgb(255,pxPtr->red, pxPtr->green, pxPtr->blue);
            }
        }

        //public static _pixel_bgr24_bgra32 operator [
        public static int GetPixelNumChannels8bit(this WriteableBitmap writtenImg)
        {
            return writtenImg.Format.BitsPerPixel/8;
        }

        // Save the WriteableBitmap into a PNG file.
        public static void SaveAsPNG(this WriteableBitmap wbitmap,
            string filename)
        {
            // Save the bitmap into a file.
            using (FileStream stream =
                new FileStream(filename, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbitmap));
                encoder.Save(stream);
            }
        }

        public static unsafe void ConvertRGB2GrayscaleRGB(this WriteableBitmap wbmp)
        {
            int numChannels = wbmp.GetPixelNumChannels8bit();
            unsafe
            {
                wbmp.Lock();
                for (int i = 0; i < wbmp.PixelHeight; i++)
                {
                    for (int j = 0; j < wbmp.BackBufferStride; j += numChannels)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*) wbmp.GetPixelIntPtrAt(i, j / numChannels);
                        int avg = (ptrPX->blue + ptrPX->green + ptrPX->red) / 3;
                        //avg = ImgUtil.Clamp(avg, 0, 255); //Commented out because it's not really needed considering it's the average of numbers between 0 and 255 (should also be in that range).
                        ptrPX->blue = ptrPX->green = ptrPX->red = (byte) avg;
                    }
                }
                wbmp.Unlock();
            }
        }

        public static unsafe void PutPixel(this WriteableBitmap wbmp, int x, int y, Color color)
        {
            unsafe
            {
                _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y,x); //x is column, y is row
                ptrPX->blue = color.B;
                ptrPX->green = color.G;
                ptrPX->red = color.R;
            }
        }
    }
}
