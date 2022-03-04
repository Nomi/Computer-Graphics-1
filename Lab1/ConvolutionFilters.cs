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
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = wbmp.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.

                for (int row = (_mSz-1)/2; row < rowCount - (_mSz - 1) / 2; row++)
                {
                    for (int col = (_mSz-1)/2; col < columnCount-(_mSz - 1) / 2; col++)
                    {
                        int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
                        int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;
                        byte[] pixDat = new byte[_mSz * _mSz * numBytesPerPix];
                        //int stride = wbmp.PixelWidth*(wbmp.Format.BitsPerPixel + 7) / 8;
                        int rLeftSart = col - (_mSz - 1) / 2;
                        int rTopStart = row - (_mSz - 1) / 2;
                        int rWidth = _mSz;//col + (_mSz - 1) / 2;
                        int rHeight = _mSz; //row + (_mSz - 1) / 2);
                        //int offset = (int)((long)wbmp.BackBuffer + (row - (_mSz - 1) / 2) * wbmp.BackBufferStride + (col - (_mSz - 1) / 2) * numChannelsPerPix);
                        int arrOffset = (row - (_mSz - 1) / 2)*(_mSz * numBytesPerPix) + (col - (_mSz - 1) / 2) * numChannelsPerPix;
                        int arrStride = _mSz * numBytesPerPix;
                        wbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart,rTopStart,rWidth,rHeight), pixDat, arrStride, 0);

                        //for(int i=0;i<pixDat.Length;i++)
                        //{
                        //    pixDat[i] = (byte)0;
                        //}
                        int red = 0, green = 0, blue = 0;
                        double sumCM = 0;
                        foreach(int c in sqrCnvMat)
                        {
                            sumCM += c;
                        }
                        for (int mR = 0,i=0; mR < _mSz; mR++)   //might only work for bgr pixels.
                        {
                            for (int mC = 0; mC < _mSz; mC++,i++)
                            {
                                int baseIndex = mR * _mSz + mC * 3;
                                blue += sqrCnvMat[mC, mR] * pixDat[baseIndex + 0];
                                green += sqrCnvMat[mC, mR] * pixDat[baseIndex + 1];
                                red += sqrCnvMat[mC, mR] * pixDat[baseIndex + 2];
                            }
                            //THe following might need to be outside the loop.
                            int midPt=((rWidth*rHeight-1)/2)+1;
                            pixDat[3*midPt] = (byte)(blue / (double)sumCM);
                            pixDat[3*midPt+1] = (byte)(green / (double)sumCM);
                            pixDat[3*midPt+2] = (byte)(red / (double)sumCM);
                        }
                        wbmp.Lock();
                        wbmp.WritePixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), pixDat, arrStride, 0);
                        wbmp.Unlock();
                        //row = row + rHeight - 1;
                        col = col + rWidth - 1;
                        //wbmp.WritePixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, _mSz * numBytesPerPix, 0);
                        ////////wbmp.CopyPixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, stride, 0);
                        ////wbmp.CopyPixels(pixDat,_mSz* _mSz * numBytesPerPix, 0);
                        for (int mR = 0; mR < _mSz; mR++)
                        {
                            for (int mC = 0; mC < _mSz; mC++)
                            {
                            }
                        }
                    }
                }
                wbmp.Lock();
                wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                wbmp.Unlock();

            }
        }
    }
}
