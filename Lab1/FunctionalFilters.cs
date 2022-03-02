using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
                int pixelNumChannels = writtenImg.Format.BitsPerPixel / 8; //Program supports only images with 8-bit BGR(A) Pixel formats for now.
                writtenImg.Lock();
                //for (long i=0;i<pixelCount;i++)
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < columnCount; col++)
                    {
                        long pixelPtr = (long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels;
                        byte[] brokenDownPixel = BitConverter.GetBytes(*((int*)pixelPtr));
                        //byte red = (byte)((255-brokenDownPixel[2]));
                        //byte green = (byte)((255 - brokenDownPixel[1]));
                        //byte blue = (byte)((255 - brokenDownPixel[0]));
                        brokenDownPixel[2] = (byte)((255 - brokenDownPixel[2]));
                        brokenDownPixel[1] = (byte)((255 - brokenDownPixel[1]));
                        brokenDownPixel[0] = (byte)((255 - brokenDownPixel[0]));
                        *((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 0) = brokenDownPixel[0];
                        *((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 1) = brokenDownPixel[1];
                        *((byte*)((long)writtenImg.BackBuffer + row * writtenImg.BackBufferStride + col * pixelNumChannels) + 2) = brokenDownPixel[2];
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
    }
}
