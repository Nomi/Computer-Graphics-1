using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab1
{
    public static class ConvolutionFilters
    {
        public static void ConvolutionFilter(int[,] sqrCnvMat, WriteableBitmap wbmp)//, ref System.Windows.Forms.PictureBox pb) //remember sqrCnvMat can have odd dimensions according to specification.
        {
            int _mSz = (int)Math.Sqrt(sqrCnvMat.Length); // or sqrCnvMat.First().Length //Also, remember sqrCnvMat can have odd dimensions according to specification.
            int rowCount = wbmp.PixelHeight;//ogPictureBox.Image.Height;
            int columnCount = wbmp.PixelWidth;// ogPictureBox.Image.Width;
            _mSz = 1000;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = wbmp.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.
                //wbmp.Lock();
                for (int row = (_mSz-1)/2+100; row < rowCount; row++)
                {
                    for (int col = (_mSz-1)/2+100; col < columnCount; col++)
                    {
                        int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
                        int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;
                        byte[] pixDat = new byte[_mSz * _mSz * numBytesPerPix];
                        int stride = wbmp.PixelWidth*(wbmp.Format.BitsPerPixel + 7) / 8;

                        int offset = (int)((long)wbmp.BackBuffer + (row - (_mSz - 1) / 2) * wbmp.BackBufferStride + (col - (_mSz - 1) / 2) * numChannelsPerPix);
                        wbmp.CopyPixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, _mSz * numBytesPerPix, 0);

                        wbmp.Lock();
                        for (int r = 0; r < rowCount; r++)
                        {
                            for (int c = 0; c < columnCount; c++)
                            {
                                _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(r, c); //Type Punning for the win!
                                pxl->blue = (byte)0;
                                pxl->red = (byte)0;
                                pxl->green = (byte)0;
                            }
                        }
                        
                        wbmp.WritePixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, _mSz * numBytesPerPix, 0);
                        //////wbmp.CopyPixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, stride, 0);
                        //wbmp.CopyPixels(pixDat,_mSz* _mSz * numBytesPerPix, 0);
                        ////for (int mR=0;mR<_mSz;mR++)
                        //{
                        //    for(int mC=0;mC<_mSz;mC++)
                        //    {
                        //    }
                        //}
                        ////_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col); //Type Punning for the win!
                        ////pxl->blue = (byte)(255 - pxl->blue);
                        ////pxl->red = (byte)(255 - pxl->red);
                        ////pxl->green = (byte)(255 - pxl->green);
                        wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                        wbmp.Unlock();
                        if (row == (_mSz - 1) / 2 && col == (_mSz - 1) / 2) break;
                    }
                    return;
                }
                //wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                //wbmp.Unlock();
            }
        }
    }
}
