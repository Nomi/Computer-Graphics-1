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
    public static class ConvolutionFilters //pretty universal, huh?
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

                for(int row =0; row < (_mSz - 1) / 2; row++) //Handles upper and left edges
                {
                    for(int col=0; col<(_mSz-1)/2;col++)
                    {
                        int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
                        int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;

                        int missingRows = (_mSz - 1) / 2 +1 - (row+1);//(_mSz-(row+1)-1);  //_mSz-(row+1) includes the middle element, so to remove that we subtract 1
                        int missingCols = (_mSz - 1) / 2 + 1 - (col + 1);//(_mSz - (col + 1) - 1);
                        byte[] subPixDat = new byte[ (_mSz - missingRows)*(_mSz-missingCols)*numBytesPerPix];
                        int rLeftSart = col+1-missingCols; //- (_mSz - 1) / 2; //or col - missingCols?
                        int rTopStart = row+1-missingRows;// - (_mSz - 1) / 2;
                        int rWidth = _mSz;
                        int rHeight = _mSz; 
                        int arrStride = (_mSz-missingCols)*numBytesPerPix;
                        wbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth-missingCols, rHeight-missingRows), subPixDat, arrStride, 0);
                        byte[] pixDat = new byte[_mSz * _mSz * numBytesPerPix];
                        for (int mR=0; mR < missingRows; mR++) //handles the upper edge of missing content
                        {
                            for (int j = 0; j < rWidth; j++)
                            {
                                int tempSubInd = 0 * _mSz + (j) * 3; //first row repeated
                                int tempInd = mR * _mSz + (missingRows + j) * 3;
                                pixDat[tempInd + 0] = subPixDat[tempSubInd+0];
                                pixDat[tempInd + 1] = subPixDat[tempSubInd+1];
                                pixDat[tempInd + 2] = subPixDat[tempSubInd+2];
                            }
                        }
                        for (int mC=0; mC< missingCols; mC++) //handles the left edge of missing content
                        {
                            for (int i = 0; i < rHeight; i++)
                            {
                                int tempSubInd = i * _mSz + 0 * 3; //first row repeated
                                int tempInd = (i+missingCols) * _mSz + mC * 3;
                                pixDat[tempInd + 0] = subPixDat[tempSubInd + 0];
                                pixDat[tempInd + 1] = subPixDat[tempSubInd + 1];
                                pixDat[tempInd + 2] = subPixDat[tempSubInd + 2];
                            }
                        }
                        for (int r = 0; r < missingRows; r++) //handles the upper-left square that will be left behind in the above loops by copying the top-left-most pixel everywhere
                        {
                            for (int c=0;c<missingCols;c++)
                            {
                                int tempInd = r* _mSz + c* 3;
                                pixDat[tempInd + 0] = subPixDat[0 + 0];
                                pixDat[tempInd + 1] = subPixDat[0 + 1];
                                pixDat[tempInd + 2] = subPixDat[0 + 2];
                            }
                        }
                        for (int mR = missingRows,subMR=0; mR < _mSz; mR++,subMR++) //copies stuff from subPixelDat to pixelDat
                        {
                            for (int mC = missingCols,subMC=0; mC < _mSz; mC++,subMC++) //might only work for bgr (not bgra) pixels because of +=3;
                            {
                                int subArrIndex = subMR * _mSz + subMC * 3;
                                int arrIndex = mR * _mSz + mC * 3;
                                pixDat[arrIndex + 0] = subPixDat[subArrIndex + 0];
                                pixDat[arrIndex + 1] = subPixDat[subArrIndex + 1];
                                pixDat[arrIndex + 2] = subPixDat[subArrIndex + 2];
                            }
                        }
                        int sumCM = 0;
                        foreach (int c in sqrCnvMat)
                        {
                            sumCM += c;
                        }

                        int red = 0; int green = 0; int blue = 0;
                        for (int mR = 0, i = 0; mR < _mSz; mR++)   //might only work for bgr pixels.
                        {
                            for (int mC = 0; mC < _mSz; mC++, i += 3)
                            {
                                int baseIndex = mR * _mSz + mC * 3;
                                baseIndex = i; //the above might work by itself;
                                blue += sqrCnvMat[mR, mC] * pixDat[baseIndex + 0]; ///clamp values to 255 and 0
                                green += sqrCnvMat[mR, mC] * pixDat[baseIndex + 1];
                                red += sqrCnvMat[mR, mC] * pixDat[baseIndex + 2];
                            }
                        }
                        int midPt = ((rWidth * rHeight - 1) / 2) + 1;
                        midPt--; //array indexing starts at 0.

                        pixDat[3 * midPt] = (byte)(blue / (double)sumCM); //sum can be 0 as well??
                        pixDat[3 * midPt + 1] = (byte)(green / (double)sumCM);
                        pixDat[3 * midPt + 2] = (byte)(red / (double)sumCM);

                        wbmp.Lock();
                        _pixel_bgr24_bgra32* pxPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col);
                        pxPtr->blue = pixDat[3 * midPt];
                        pxPtr->green = pixDat[3 * midPt + 1];
                        pxPtr->red = pixDat[3 * midPt + 2];
                        wbmp.Unlock();
                    }
                }

                for (int row = (_mSz-1)/2; row < rowCount - (_mSz - 1) / 2; row++) //Handles Non-Edge part
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
                            for (int mC = 0; mC < _mSz; mC++, i+=3)
                            {
                                int baseIndex = mR * _mSz + mC * 3;
                                baseIndex = i; //the above might work by itself;
                                blue += sqrCnvMat[mR,mC] * pixDat[baseIndex + 0]; ///clamp values to 255 and 0
                                green += sqrCnvMat[mR,mC] * pixDat[baseIndex + 1];
                                red += sqrCnvMat[mR, mC] * pixDat[baseIndex + 2];
                            }
                        }
                        int midPt = ((rWidth * rHeight - 1) / 2) + 1;
                        midPt--; //array indexing starts at 0.
                        pixDat[3 * midPt] = (byte)(blue / (double)sumCM); //sum can be 0 as well??
                        pixDat[3 * midPt + 1] = (byte)(green / (double)sumCM);
                        pixDat[3 * midPt + 2] = (byte)(red / (double)sumCM);
                        wbmp.Lock();
                        wbmp.WritePixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), pixDat, arrStride, 0);
                        wbmp.Unlock();
                        //row = row + rHeight - 1;
                        //col = col + rWidth - 1;
                        //wbmp.WritePixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, _mSz * numBytesPerPix, 0);
                        ////////wbmp.CopyPixels(new System.Windows.Int32Rect(col - (_mSz - 1) / 2, row - (_mSz - 1) / 2, col + (_mSz - 1) / 2, row + (_mSz - 1) / 2), pixDat, stride, 0);
                        ////wbmp.CopyPixels(pixDat,_mSz* _mSz * numBytesPerPix, 0);
                        //for (int mR = 0; mR < _mSz; mR++)
                        //{
                        //    for (int mC = 0; mC < _mSz; mC++)
                        //    {
                        //    }
                        //}
                    }
                }

                //need to handle lower-right edges here

                wbmp.Lock();
                wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                wbmp.Unlock();
            }
        }
    }
}
