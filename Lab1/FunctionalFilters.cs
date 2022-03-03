using Computer_Graphics_1.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Computer_Graphics_1.HelperClasses.Extensions;
namespace Computer_Graphics_1.Lab1
{
    public static class FunctionalFilters
    {
        public static void InvertWriteableBitmap(WriteableBitmap writtenImg)
        {
            int rowCount = writtenImg.PixelHeight;//ogPictureBox.Image.Height;
            int columnCount = writtenImg.PixelWidth;// ogPictureBox.Image.Width;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.
                writtenImg.Lock();
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)writtenImg.GetPixelIntPtrAt(row, col); //Type Punning for the win!
                        pxl->blue = (byte)(255 - pxl->blue);
                        pxl->red = (byte)(255 - pxl->red);
                        pxl->green = (byte)(255 - pxl->green);
                    }
                }
                writtenImg.AddDirtyRect(new System.Windows.Int32Rect(0, 0, writtenImg.PixelWidth, writtenImg.PixelHeight));
                writtenImg.Unlock();
            }
        }

        public static void BrightnessCorrectionWbmp(WriteableBitmap writtenImg, int brightnessIncrease)
        {
            int brtInc = brightnessIncrease;
            int rowCount = writtenImg.PixelHeight;//ogPictureBox.Image.Height;
            int columnCount = writtenImg.PixelWidth;// ogPictureBox.Image.Width;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.
                writtenImg.Lock();
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        //byte[] pixCols = BitConverter.GetBytes(*(int*)writtenImg.BackBuffer);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)writtenImg.GetPixelIntPtrAt(row, col); //Type Punning for the win!
                        int newBlue = (brtInc + pxl->blue);
                        newBlue = newBlue > 255 ? 255 : newBlue;

                        int newGreen = (brtInc + pxl->green);
                        newGreen = newGreen > 255 ? 255 : newGreen;

                        int newRed= (brtInc + pxl->red);
                        newRed = newRed > 255 ? 255 : newRed;

                        pxl->blue = (byte) newBlue;
                        pxl->red = (byte) newRed;
                        pxl->green = (byte) newGreen;
                    }
                }
                writtenImg.AddDirtyRect(new System.Windows.Int32Rect(0, 0, writtenImg.PixelWidth, writtenImg.PixelHeight));
                writtenImg.Unlock();
            }
        }

        public static void ContrastEnhancementWbmp(WriteableBitmap writtenImg, double scalar)
        {
            int rowCount = writtenImg.PixelHeight;
            int columnCount = writtenImg.PixelWidth;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.
                writtenImg.Lock();
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        //byte[] pixCols = BitConverter.GetBytes(*(int*)writtenImg.BackBuffer);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)writtenImg.GetPixelIntPtrAt(row, col); //Type Punning for the win!

                        int newBlue = (int)Math.Ceiling((((double)(((int)pxl->blue) / 255.0 - 0.5) * scalar) + 0.5) * 255.0);
                        if (newBlue < 0) newBlue = 0;
                        if (newBlue > 255) newBlue = 255;

                        int newGreen = (int)Math.Ceiling((((double)(((int)pxl->green) / 255.0 - 0.5) * scalar) + 0.5) * 255.0);
                        if (newGreen < 0) newGreen = 0;
                        if (newGreen > 255) newGreen = 255;

                        int newRed = (int)Math.Ceiling((((double)(((int)pxl->red) / 255.0 - 0.5) * scalar) + 0.5) * 255.0);
                        if (newRed < 0) newRed = 0;
                        if (newRed > 255) newRed = 255;

                        pxl->blue = (byte)newBlue;
                        pxl->red = (byte)newRed;
                        pxl->green = (byte)newGreen;
                    }
                }
                writtenImg.AddDirtyRect(new System.Windows.Int32Rect(0, 0, writtenImg.PixelWidth, writtenImg.PixelHeight));
                writtenImg.Unlock();
            }
        }


        public static void GammaCorrection(WriteableBitmap writtenImg, double gamma)
        {
            int rowCount = writtenImg.PixelHeight;
            int columnCount = writtenImg.PixelWidth;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.
                writtenImg.Lock();
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)writtenImg.GetPixelIntPtrAt(row, col); //Type Punning for the win!

                        int newBlue = (int)Math.Floor(255 * Math.Pow(((int)pxl->blue/255.0), gamma));
                        if (newBlue > 255) newBlue = 255;

                        int newGreen = (int)Math.Floor(255 * Math.Pow(((int)pxl->green / 255.0), gamma));
                        if (newGreen > 255) newGreen = 255;

                        int newRed = (int)Math.Floor(255 * Math.Pow(((int)pxl->red / 255.0), gamma));
                        if (newRed > 255) newRed = 255;

                        pxl->blue = (byte)newBlue;
                        pxl->red = (byte)newRed;
                        pxl->green = (byte)newGreen;
                    }
                }
                writtenImg.AddDirtyRect(new System.Windows.Int32Rect(0, 0, writtenImg.PixelWidth, writtenImg.PixelHeight));
                writtenImg.Unlock();
            }
        }

        
        /*//Unclean version of Invert, with too many comments
        public static void InvertWriteableBitmap(WriteableBitmap writtenImg)
        {
            int rowCount = writtenImg.PixelHeight;//ogPictureBox.Image.Height;
            int columnCount = writtenImg.PixelWidth;// ogPictureBox.Image.Width;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8; //Program supports only images with 8-bit BGR(A) Pixel formats for now.
                writtenImg.Lock();
                //for (long i=0;i<pixelCount;i++)
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        //long pixelPtr = (long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels;
                        ////byte[] brokenDownPixel = BitConverter.GetBytes(*((int*)pixelPtr));
                        //////void* pxlPtr = BitConverter.GetBytes(*((int*)pixelPtr));
                        ////brokenDownPixel[2] = (byte)((255 - brokenDownPixel[2]));
                        ////brokenDownPixel[1] = (byte)((255 - brokenDownPixel[1]));
                        ////brokenDownPixel[0] = (byte)((255 - brokenDownPixel[0]));
                        ////*((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 0) = brokenDownPixel[0];
                        ////*((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 1) = brokenDownPixel[1];
                        ////*((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 2) = brokenDownPixel[2];
                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)pixelPtr; //Type Punning for the win!
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)writtenImg.GetPixelIntPtrAt(row, col); //Type Punning for the win!
                        pxl->blue = (byte)(255 - pxl->blue);
                        pxl->red = (byte)(255 - pxl->red);
                        pxl->green = (byte)(255 - pxl->green);
                    }
                    //// Compute the pixel's color.
                    //int color_data = red << 16; // R
                    //color_data |= green << 8;   // G
                    //color_data |= blue << 0;   // B

                    //// Assign the color data to the pixel.
                    //*((int*)pixelPtr) = color_data;
                }
                writtenImg.AddDirtyRect(new System.Windows.Int32Rect(0, 0, writtenImg.PixelWidth, writtenImg.PixelHeight));
                writtenImg.Unlock();
            }
        }
        */
    }
}
