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
        public static void Apply(int[,] sqrCnvMat, _coords anchorKernel, WriteableBitmap wbmp, double divisor = -99999, int offset=0)
        {
            WriteableBitmap cloneWbmp = wbmp.Clone();
            //int cnvMatSiz = (int)Math.Sqrt(sqrCnvMat.Length); //square matrix nxn length = n^2
            int cnvMatRowCount = sqrCnvMat.GetLength(0);
            int cnvMatColCount = sqrCnvMat.GetLength(1);

            int wbmpRowCount = wbmp.PixelHeight;
            int wbmpColCount = wbmp.PixelWidth;

            double sumCM = 0;
            foreach (int c in sqrCnvMat)
            {
                sumCM += c;
            }

            if (divisor == -99999)
            {
                divisor = sumCM;
                if(divisor==0)
                {
                    divisor = 1;
                }
            }


            //int mid = (cnvMatSiz - 1) / 2 + 1;
            int midR = (cnvMatRowCount - 1) / 2 + 1;
            int midC = (cnvMatColCount - 1) / 2 + 1;
            if (anchorKernel.c == -1)
                anchorKernel.c = midR; //midpoint of col ///DEPRECATED:sets midpoint of column //remember, this works because COnv mat size is odd by our requirements
            if (anchorKernel.r == -1)
                anchorKernel.r = midC; //midpoint of row ///DEPRECATED:same as above, just for row.

            _coords anchorOffsetCenter; 
            anchorOffsetCenter.r = anchorKernel.r - midR;
            anchorOffsetCenter.c = anchorKernel.c - midC;

            int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
            int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;

            unsafe // do: upper row, left col, right col, lower row, all the corner squares, and the middle part that isn't missing pixels
            {
                wbmp.Lock();
                int TotalMissingRowsTop = anchorKernel.r - 1;
                int TotalMissingColsLeft = anchorKernel.c - 1; //for the full row
                int TotalMissingColsRight = (cnvMatColCount - anchorKernel.c); //for the full row
                int TotalMissingRowsBottom = (cnvMatRowCount - anchorKernel.r);
                //For math min/max functions i might need to us imgRow and/or imgCo with + or minus.
                //For img pixels missing both upper rows and left columns (diagonal square/rectangle)
                for (int imgRow = 0; imgRow < TotalMissingRowsTop; imgRow++)
                {
                    int currMissingRows = anchorKernel.r - (imgRow + 1);
                    for (int imgCol = 0; imgCol < TotalMissingColsLeft; imgCol++)
                    {
                        int currMissingCols = anchorKernel.c - (imgCol + 1);

                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount - currMissingCols) * numBytesPerPix];

                        int rLeftSart = imgCol - anchorKernel.c + 1 + currMissingCols;
                        int rTopStart = imgRow - anchorKernel.r + 1 + currMissingRows;
                        int rWidth = cnvMatColCount - currMissingCols;
                        int rHeight = cnvMatRowCount - currMissingRows;
                        int arrStride = rWidth * numBytesPerPix;

                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);
                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxRow = Math.Max(r - currMissingRows, 0);
                                int pxCol = Math.Max(c - currMissingCols, 0);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);


                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;
                    }
                }
                //For img pixels missing both upper rows and right columns (diagonal square/rectangle)
                for (int imgRow = 0; imgRow < TotalMissingRowsTop; imgRow++)
                {
                    int currMissingRows = anchorKernel.r - (imgRow + 1);
                    for (int imgCol = wbmpColCount - TotalMissingColsRight; imgCol < wbmpColCount; imgCol++)
                    {
                        int colsToRightOfImgRow = wbmpColCount - (imgCol + 1);
                        int colsNeededToRightOfImgCol = (cnvMatColCount - anchorKernel.c) - colsToRightOfImgRow;
                        int currMissingCols = colsNeededToRightOfImgCol;

                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount - currMissingCols) * numBytesPerPix];

                        int rLeftSart = imgCol - anchorKernel.c + 1;
                        int rTopStart = imgRow - anchorKernel.r + 1 + currMissingRows;
                        int rWidth = cnvMatColCount - currMissingCols;
                        int rHeight = cnvMatRowCount - currMissingRows;
                        int arrStride = rWidth * numBytesPerPix;

                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);
                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxRow = Math.Max(r - currMissingRows, 0);
                                int pxCol = Math.Min(c, cnvMatColCount - 1 - currMissingCols);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);


                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;
                    }
                }
                //For img pixels missing both lower rows and left columns (diagonal square/rectangle)
                for (int imgRow = wbmpRowCount-TotalMissingRowsBottom; imgRow < wbmpRowCount; imgRow++)
                {
                    int rowsBelowImgRow = wbmpRowCount - (imgRow + 1);
                    int rowsNeededBelowImgRow = (cnvMatRowCount - anchorKernel.r) - rowsBelowImgRow;
                    int currMissingRows = rowsNeededBelowImgRow;
                    for (int imgCol = 0; imgCol < wbmpColCount - TotalMissingColsRight; imgCol++)
                    {
                        int currMissingCols = anchorKernel.c - (imgCol + 1);

                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount - currMissingCols) * numBytesPerPix];

                        int rLeftSart = imgCol - anchorKernel.c + 1 + currMissingCols;
                        int rTopStart = imgRow - anchorKernel.r + 1;
                        int rWidth = cnvMatColCount - currMissingCols;
                        int rHeight = cnvMatRowCount - currMissingRows;
                        int arrStride = rWidth * numBytesPerPix;

                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);
                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxRow = Math.Max(r - currMissingRows, 0);
                                int pxCol = Math.Min(c, cnvMatColCount - 1 - currMissingCols);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);


                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;

                    }
                }
                //For img pixels missing both lower rows and right columns (diagonal square/rectangle)
                for (int imgRow = wbmpRowCount-TotalMissingRowsBottom; imgRow < wbmpRowCount; imgRow++)
                {
                    int rowsBelowImgRow = wbmpRowCount - (imgRow + 1);
                    int rowsNeededBelowImgRow = (cnvMatRowCount - anchorKernel.r) - rowsBelowImgRow;
                    int currMissingRows = rowsNeededBelowImgRow;

                    for (int imgCol = wbmpColCount - TotalMissingColsRight; imgCol < wbmpColCount; imgCol++)
                    {
                        int colsToRightOfImgRow = wbmpColCount - (imgCol + 1);
                        int colsNeededToRightOfImgCol = (cnvMatColCount - anchorKernel.c) - colsToRightOfImgRow;
                        int currMissingCols = colsNeededToRightOfImgCol;

                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount - currMissingCols) * numBytesPerPix];

                        int rLeftSart = imgCol - anchorKernel.c + 1;
                        int rTopStart = imgRow - anchorKernel.r + 1;
                        int rWidth = cnvMatColCount - currMissingCols;
                        int rHeight = cnvMatRowCount - currMissingRows;
                        int arrStride = rWidth * numBytesPerPix;

                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);
                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxRow = Math.Min(c, cnvMatRowCount - 1 - currMissingRows);
                                int pxCol = Math.Min(c, cnvMatColCount - 1 - currMissingCols);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);


                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;


                    }
                }
                //For img pixels that are missing upper rows:
                for (int imgRow=0;imgRow<TotalMissingRowsTop;imgRow++)
                {
                    int currMissingRows = anchorKernel.r - (imgRow + 1); //(mid + anchorOffsetCenter.r) - (imgRow + 1);

                    for (int imgCol=TotalMissingColsLeft;imgCol<wbmpColCount-TotalMissingColsRight;imgCol++)
                    {
                        //since I've chosen the boundaries of this for loop carefully so as to only extend the first row upwards, we don't have any missing columns here.
                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount) * numBytesPerPix];
                        int rLeftSart = imgCol - anchorKernel.c +1;
                        int rTopStart = imgRow - anchorKernel.r +1 + currMissingRows;
                        int rWidth = cnvMatColCount;
                        int rHeight = cnvMatRowCount-currMissingRows;
                        int arrStride = (cnvMatColCount) * numBytesPerPix; //numcolumns*numBYtesPerPixel
                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);
                        //So far,
                        //We have gotten the non-missing pixels in subPixDat array.
                        //After this we try to use the first non-missing row for all the missing rows
                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);

                        int red = 0; int green = 0; int blue=0;
                        //the following goes through cnvMat
                        for(int r=0;r<cnvMatRowCount;r++)
                        {
                            for(int c=0;c<cnvMatColCount;c++)
                            {
                                int pxRow = Math.Max(r - currMissingRows,0);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, c, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green= ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);

                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;


                    }
                }
                //For img pixels that are missing left columns:
                for (int imgRow = TotalMissingRowsTop; imgRow < wbmpRowCount -TotalMissingRowsBottom; imgRow++)
                {
                    for (int imgCol = 0; imgCol < wbmpColCount - TotalMissingColsRight; imgCol++)
                    {
                        int currMissingCols = anchorKernel.c - (imgCol + 1);
                        //since I've chosen the boundaries of this for loop carefully so as to only extend the first column to left, we don't have any missing rows here.
                        byte[] subPixDat = new byte[(cnvMatRowCount) * (cnvMatColCount -currMissingCols) * numBytesPerPix];
                        int rLeftSart = imgCol - anchorKernel.c + 1+currMissingCols;
                        int rTopStart = imgRow - anchorKernel.r + 1;
                        int rWidth = cnvMatColCount-currMissingCols;
                        int rHeight = cnvMatRowCount;
                        int arrStride = (cnvMatColCount -currMissingCols) * numBytesPerPix;
                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);

                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxCol = Math.Max(c-currMissingCols, 0);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(r, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);

                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;


                    }
                }
                //The part that isn't missing any edges:
                for (int imgRow = TotalMissingRowsTop; imgRow < wbmpRowCount-TotalMissingRowsBottom; imgRow++)
                {
                    for (int imgCol = TotalMissingColsLeft; imgCol < wbmpColCount-TotalMissingColsRight; imgCol++)
                    {
                        byte[] pixDat = new byte[cnvMatRowCount * cnvMatColCount* numBytesPerPix];
                        int rLeftSart = imgCol - (anchorKernel.c-1); //-1 because of indexing
                        int rTopStart = imgRow - (anchorKernel.r-1); //-1 because of indexing
                        int rWidth = cnvMatColCount;
                        int rHeight = cnvMatRowCount;
                        int arrStride = cnvMatColCount * numBytesPerPix;
                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), pixDat, arrStride, 0);

                        int red = 0, green = 0, blue = 0;
                        for (int mR = 0, i = 0; mR < cnvMatRowCount; mR++)   //might only work for bgr pixels.
                        {
                            for (int mC = 0; mC < cnvMatColCount; mC++, i += 3)
                            {
                                int baseIndex = mR * cnvMatRowCount + mC * 3;
                                baseIndex = i; //the above might work by itself;
                                blue += sqrCnvMat[mR, mC] * pixDat[baseIndex + 0]; ///clamp values to 255 and 0
                                green += sqrCnvMat[mR, mC] * pixDat[baseIndex + 1];
                                red += sqrCnvMat[mR, mC] * pixDat[baseIndex + 2];
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);

                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;


                    }
                }
                //For img pixels that are missing lower rows:
                for (int imgRow = wbmpRowCount-TotalMissingRowsBottom; imgRow < wbmpRowCount; imgRow++)
                {
                    int rowsBelowImgRow= wbmpRowCount - (imgRow+1);
                    int rowsNeededBelowImgRow = (cnvMatRowCount - anchorKernel.r) - rowsBelowImgRow;
                    int currMissingRows = rowsNeededBelowImgRow;
                    //currMissingRows= anchorKernel.r - (imgRow + 1); ;
                    for (int imgCol = TotalMissingColsLeft; imgCol < wbmpColCount - TotalMissingColsRight; imgCol++)
                    {
                        //since I've chosen the boundaries of this for loop carefully so as to only extend the first row upwards, we don't have any missing columns here.
                        byte[] subPixDat = new byte[(cnvMatRowCount - currMissingRows) * (cnvMatColCount) * numBytesPerPix];
                        int rLeftSart = imgCol - anchorKernel.c + 1;
                        int rTopStart = imgRow - anchorKernel.r + 1;
                        int rWidth = cnvMatColCount;
                        int rHeight = cnvMatRowCount - currMissingRows;
                        int arrStride = (cnvMatColCount) * numBytesPerPix; //numcolumns*numBYtesPerPixel
                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);
                        //So far,
                        //We have gotten the non-missing pixels in subPixDat array.
                        //After this we try to use the first non-missing row for all the missing rows
                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);

                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxRow = Math.Min(r, cnvMatRowCount -1-currMissingRows);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(pxRow, c, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);

                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;


                    }
                }
                //For img pixels that are missing right columns:
                for (int imgRow = TotalMissingRowsTop; imgRow < wbmpRowCount - TotalMissingRowsBottom; imgRow++)
                {
                    for (int imgCol = wbmpColCount - TotalMissingColsRight; imgCol < wbmpColCount; imgCol++)
                    {
                        int colsToRightOfImgRow = wbmpColCount - (imgCol + 1);
                        int colsNeededToRightOfImgCol = (cnvMatColCount - anchorKernel.c) - colsToRightOfImgRow;
                        int currMissingCols = colsNeededToRightOfImgCol;
                        //since I've chosen the boundaries of this for loop carefully so as to only extend the first column to left, we don't have any missing rows here.
                        byte[] subPixDat = new byte[(cnvMatRowCount) * (cnvMatColCount - currMissingCols) * numBytesPerPix];
                        int rLeftSart = imgCol - anchorKernel.c + 1;
                        int rTopStart = imgRow - anchorKernel.r+1;
                        int rWidth = cnvMatColCount - currMissingCols;
                        int rHeight = cnvMatRowCount;
                        int arrStride = (cnvMatColCount - currMissingCols) * numBytesPerPix;
                        cloneWbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), subPixDat, arrStride, 0);

                        byte[,] sub2DPixDat = subPixDat.ConvertTo2D(rWidth * numChannelsPerPix, rHeight);

                        int red = 0; int green = 0; int blue = 0;
                        //the following goes through cnvMat
                        for (int r = 0; r < cnvMatRowCount; r++)
                        {
                            for (int c = 0; c < cnvMatColCount; c++)
                            {
                                int pxCol = Math.Min(c, cnvMatColCount -1-currMissingCols);
                                _pixel_bgr24_bgra32 px = sub2DPixDat.GetPixelAt(r, pxCol, numChannelsPerPix);
                                blue += sqrCnvMat[r, c] * px.blue;
                                green += sqrCnvMat[r, c] * px.green;
                                red += sqrCnvMat[r, c] * px.red;
                            }
                        }
                        blue = (int)(blue / (double)divisor);
                        green = (int)(green / (double)divisor);
                        red = (int)(red / (double)divisor);
                        blue += offset;
                        green += offset;
                        red += offset;
                        blue = ImgUtil.Clamp(blue, 0, 255);
                        green = ImgUtil.Clamp(green, 0, 255);
                        red = ImgUtil.Clamp(red, 0, 255);

                        //_pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow + anchorOffsetCenter.r, imgCol + anchorOffsetCenter.c);
                        _pixel_bgr24_bgra32* pxl = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(imgRow, imgCol);
                        pxl->blue = (byte)blue;
                        pxl->green = (byte)green;
                        pxl->red = (byte)red;
                        

                    }
                }
                wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                wbmp.Unlock();
            }
        }
        public static void ConvolutionFilterOld(int[,] sqrCnvMat, WriteableBitmap wbmp)//, ref System.Windows.Forms.PictureBox pb) //remember sqrCnvMat can have odd dimensions according to specification.
        {
            int _mSz = (int)Math.Sqrt(sqrCnvMat.Length); // or sqrCnvMat.First().Length //Also, remember sqrCnvMat can have odd dimensions according to specification.
            int rowCount = wbmp.PixelHeight;//ogPictureBox.Image.Height;
            int columnCount = wbmp.PixelWidth;// ogPictureBox.Image.Width;
            unsafe
            {
                /// IMPLEMENT FULLY WITH TRY https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.writeablebitmap?view=windowsdesktop-6.0#:~:text=Properties%20%20%20Back%20Buffer%20%20%20Gets,this%20DispatcherObjec%20...%20%2013%20more%20rows%20
                //Program supports only images with 8-bit BGR(A) Pixel formats for now. Because of GetPixelIntPtrAt extension containing the line "int pixelNumChannels = wbmp.Format.BitsPerPixel / 8;". It was in turn added to have compatibility of both BGRA and BGR channel configs.


                //Update, it should work. IDK why I thought it wouldn't. Weariness maybe?//missing corner part might be wrong as it doesn't take into account the position of the current pixel and so on.
                //BUT: I also don't really set the pixels calculated inside the deeper for loops anywhere
                //for right side and lower side, need to calculate new missingCOl and missingRow

                //or maybe I could remove the corner parts from here and do that at the end because the image would only be missing the diagonal squares
                //Handles upper and left edges:
                for (int row =0; row < (_mSz - 1) / 2; row++) 
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
                        //Should I calculate missingRowsRight here to help duplicate upper right corner?
                        //or maybe I could remove the corner parts from here and do that at the end because the image would only be missing the diagonal squares

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


                        _pixel_bgr24_bgra32* pxPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col);
                        pxPtr->blue = pixDat[3 * midPt];
                        pxPtr->green = pixDat[3 * midPt + 1];
                        pxPtr->red = pixDat[3 * midPt + 2];

                    }
                }
                //Handles Non-Edge parts
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

                        wbmp.WritePixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth, rHeight), pixDat, arrStride, 0);

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


                //Next, change the following copy-paste from upper-left handler to do lower-right
                for (int row = rowCount - (_mSz - 1) / 2; row < rowCount; row++) //Handles lower and right edges
                {
                    //lower-right corner square here to replace upper-left in previous code and so on.
                    for (int col = columnCount - (_mSz - 1) / 2; col < columnCount; col++)
                    {
                        int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
                        int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;

                        int missingRows = (_mSz - 1) / 2 + 1 - (row + 1);//(_mSz-(row+1)-1);  //_mSz-(row+1) includes the middle element, so to remove that we subtract 1
                        int missingCols = (_mSz - 1) / 2 + 1 - (col + 1);//(_mSz - (col + 1) - 1);
                        byte[] subPixDat = new byte[(_mSz - missingRows) * (_mSz - missingCols) * numBytesPerPix];
                        int rLeftSart = col + 1 - missingCols; //- (_mSz - 1) / 2; //or col - missingCols?
                        int rTopStart = row + 1 - missingRows;// - (_mSz - 1) / 2;
                        int rWidth = _mSz;
                        int rHeight = _mSz;
                        int arrStride = (_mSz - missingCols) * numBytesPerPix;
                        wbmp.CopyPixels(new System.Windows.Int32Rect(rLeftSart, rTopStart, rWidth - missingCols, rHeight - missingRows), subPixDat, arrStride, 0);
                        byte[] pixDat = new byte[_mSz * _mSz * numBytesPerPix];
                        for (int mR = 0; mR < missingRows; mR++) //handles the upper edge of missing content
                        {
                            for (int j = 0; j < rWidth; j++)
                            {
                                int tempSubInd = 0 * _mSz + (j) * 3; //first row repeated
                                int tempInd = mR * _mSz + (missingRows + j) * 3;
                                pixDat[tempInd + 0] = subPixDat[tempSubInd + 0];
                                pixDat[tempInd + 1] = subPixDat[tempSubInd + 1];
                                pixDat[tempInd + 2] = subPixDat[tempSubInd + 2];
                            }
                        }
                        for (int mC = 0; mC < missingCols; mC++) //handles the left edge of missing content
                        {
                            for (int i = 0; i < rHeight; i++)
                            {
                                int tempSubInd = i * _mSz + 0 * 3; //first row repeated
                                int tempInd = (i + missingCols) * _mSz + mC * 3;
                                pixDat[tempInd + 0] = subPixDat[tempSubInd + 0];
                                pixDat[tempInd + 1] = subPixDat[tempSubInd + 1];
                                pixDat[tempInd + 2] = subPixDat[tempSubInd + 2];
                            }
                        }
                        for (int r = 0; r < missingRows; r++) //handles the upper-left square that will be left behind in the above loops by copying the top-left-most pixel everywhere
                        {
                            for (int c = 0; c < missingCols; c++)
                            {
                                int tempInd = r * _mSz + c * 3;
                                pixDat[tempInd + 0] = subPixDat[0 + 0];
                                pixDat[tempInd + 1] = subPixDat[0 + 1];
                                pixDat[tempInd + 2] = subPixDat[0 + 2];
                            }
                        }
                        for (int r = 0; r < missingRows; r++) //handles the upper-left square that will be left behind in the above loops by copying the top-left-most pixel everywhere
                        {
                            for (int c = 0; c < missingCols; c++)
                            {
                                int tempInd = r * _mSz + c * 3;
                                pixDat[tempInd + 0] = subPixDat[0 + 0];
                                pixDat[tempInd + 1] = subPixDat[0 + 1];
                                pixDat[tempInd + 2] = subPixDat[0 + 2];
                            }
                        }
                        for (int mR = missingRows, subMR = 0; mR < _mSz; mR++, subMR++) //copies stuff from subPixelDat to pixelDat
                        {
                            for (int mC = missingCols, subMC = 0; mC < _mSz; mC++, subMC++) //might only work for bgr (not bgra) pixels because of +=3;
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


                        _pixel_bgr24_bgra32* pxPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col);
                        pxPtr->blue = pixDat[3 * midPt];
                        pxPtr->green = pixDat[3 * midPt + 1];
                        pxPtr->red = pixDat[3 * midPt + 2];

                    }
                }
                //here, need to also handle upper-right,  and lower-left corner squares.

                wbmp.AddDirtyRect(new System.Windows.Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                wbmp.Unlock();
            }
        }
        public static void MedianFilter3x3(WriteableBitmap wbmp)
        {
            WriteableBitmap cloneWbmp = wbmp.Clone();
            //int[,] pixelSample = new int[3, 3];
            //List<byte> blueVals= new List<byte>
            //The following will contain the respective colors of the pixels in a one-dimensional array to make picking mean easier.
            byte[] blueVals = new byte[3*3];
            byte[] greenVals = new byte[3 * 3];
            byte[] redVals = new byte[3 * 3];
            unsafe
            {
                wbmp.Lock();
                for (int row = 0; row < wbmp.PixelHeight; row++)
                {
                    for (int col = 0; col < wbmp.PixelWidth; col++)
                    {
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                int r = ImgUtil.Clamp(row + i, 0, wbmp.PixelHeight - 1);
                                int c = ImgUtil.Clamp(col + j, 0, wbmp.PixelWidth - 1);
                                _pixel_bgr24_bgra32* currPx = (_pixel_bgr24_bgra32*) cloneWbmp.GetPixelIntPtrAt(r, c);
                                blueVals[(i+1)*3 + (j+1)] = currPx->blue;
                                greenVals[(i + 1) * 3 + (j + 1)] = currPx->green;
                                redVals[(i + 1) * 3 + (j + 1)] = currPx->red;
                            }
                        }
                        Array.Sort(blueVals);
                        Array.Sort(greenVals);
                        Array.Sort(redVals);
                        _pixel_bgr24_bgra32* px = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col);
                        px->blue = blueVals[4];
                        px->green = greenVals[4];
                        px->red = redVals[4];
                    }
                }
                //wbmp.AddDirtyRect(
                wbmp.Unlock();
            }
        }

        public static void ConvFilCleanCode(int[,] sqrCnvMat, _coords anchorKernel, WriteableBitmap wbmp, double divisor = -99999, int offset = 0)
        {
            WriteableBitmap cloneWbmp = wbmp.Clone();
            int cnvMatRowCount = sqrCnvMat.GetLength(0);
            int cnvMatColCount = sqrCnvMat.GetLength(1);

            int wbmpRowCount = wbmp.PixelHeight;
            int wbmpColCount = wbmp.PixelWidth;

            double sumCM = 0;
            foreach (int c in sqrCnvMat)
            {
                sumCM += c;
            }

            if (divisor == -99999)
            {
                divisor = sumCM;
                if (divisor == 0)
                {
                    divisor = 1;
                }
            }
            int midR = (cnvMatRowCount - 1) / 2 + 1; //can't accept even matrices
            int midC = (cnvMatColCount - 1) / 2 + 1; //can't accept even matrices

            _coords anchorOffsetCenter; //doesn't contatin the center itself, just the number of elements before that
            anchorOffsetCenter.r = anchorKernel.r - midR;
            anchorOffsetCenter.c = anchorKernel.c - midC;

            int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
            int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;
            unsafe
            {
                wbmp.Lock();
                for (int row = 0; row < wbmpRowCount; row++)
                {
                    for (int col = 0; col < wbmpColCount; col++)
                    {
                        int sumBlue = 0;
                        int sumGreen = 0;
                        int sumRed = 0;
                        for (int i = -(anchorKernel.r-1), cnvI=0; i < (cnvMatRowCount-(anchorKernel.r-1)); i++,cnvI++) //We have -1s inside brackets because anchorKernel is indexed from 1, while we need index from 0. //+1 outside to include last element //notice that 0 is included in this (for the center element)
                        {
                            for (int j = -(anchorKernel.c - 1), cnvJ=0; j < (cnvMatColCount - (anchorKernel.c - 1)); j++,cnvJ++)
                            {
                                int r = ImgUtil.Clamp(row + i, 0, wbmp.PixelHeight - 1);
                                int c = ImgUtil.Clamp(col + j, 0, wbmp.PixelWidth - 1);
                                _pixel_bgr24_bgra32* currPx = (_pixel_bgr24_bgra32*)cloneWbmp.GetPixelIntPtrAt(r, c);
                                sumBlue += currPx->blue*sqrCnvMat[cnvI,cnvJ];
                                sumGreen += currPx->green * sqrCnvMat[cnvI, cnvJ];
                                sumRed += currPx->red * sqrCnvMat[cnvI, cnvJ];
                            }
                        }
                        sumBlue = (int)(sumBlue / (double)divisor);
                        sumGreen = (int)(sumGreen / (double)divisor);
                        sumRed = (int)(sumRed / (double)divisor);
                        sumBlue += offset;
                        sumGreen += offset;
                        sumRed += offset;
                        _pixel_bgr24_bgra32* px = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(row, col);
                        px->blue = (byte)ImgUtil.Clamp(sumBlue, 0,255);
                        px->green = (byte)ImgUtil.Clamp(sumGreen, 0, 255);
                        px->red = (byte)ImgUtil.Clamp(sumRed, 0, 255);
                    }
                }
                wbmp.Unlock();
            }
        }


        public static void convFiltMultiThreaded(int[,] sqrCnvMat, _coords anchorKernel, WriteableBitmap wbmp, double divisor = -99999, int offset = 0)
        {
            WriteableBitmap cloneWbmp = wbmp.Clone();
            int cnvMatRowCount = sqrCnvMat.GetLength(0);
            int cnvMatColCount = sqrCnvMat.GetLength(1);

            int wbmpRowCount = wbmp.PixelHeight;
            int wbmpColCount = wbmp.PixelWidth;

            double sumCM = 0;
            foreach (int c in sqrCnvMat)
            {
                sumCM += c;
            }

            if (divisor == -99999)
            {
                divisor = sumCM;
                if (divisor == 0)
                {
                    divisor = 1;
                }
            }
            int midR = (cnvMatRowCount - 1) / 2 + 1; //can't accept even matrices
            int midC = (cnvMatColCount - 1) / 2 + 1; //can't accept even matrices

            _coords anchorOffsetCenter; //doesn't contatin the center itself, just the number of elements before that
            anchorOffsetCenter.r = anchorKernel.r - midR;
            anchorOffsetCenter.c = anchorKernel.c - midC;

            int numChannelsPerPix = (wbmp.GetPixelSizeBytes() / 8);
            int numBytesPerPix = (wbmp.Format.BitsPerPixel + 7) / 8;

            unsafe
            {
                wbmp.Lock();
                
                wbmp.Unlock();
            }

        }
        private class cfPPMTPparams
        {
            public int wbmpRowCount;
            public int wbmpColCount;
            public _coords anchorKernel;
            public WriteableBitmap cloneWbmp;
            public int divisor;
            public int offset;
            public WriteableBitmap wbmp;
            public cfPPMTPparams(int _wbmpRowCount,int _wbmpColCount, _coords _anchorKernel, WriteableBitmap _cloneWbmp, int _divisor, int _offset, WriteableBitmap _wbmp)
            {
                wbmpRowCount = _wbmpRowCount;
                wbmpColCount = _wbmpColCount;
                anchorKernel = _anchorKernel;
                cloneWbmp = _cloneWbmp;
                divisor = _divisor;
                offset = _offset;
                wbmp = _wbmp;
            }
        }
        private static WriteableBitmap CfPrivParamMultiThreadedPart(Object paramsObj)
        {
            cfPPMTPparams param = (cfPPMTPparams) paramsObj;
            int wbmpRowCount = param.wbmpRowCount;
            int wbmpColCount = param.wbmpColCount;
            _coords anchorKernel = param.anchorKernel;
            WriteableBitmap cloneWbmp = param.cloneWbmp;
            int divisor = param.divisor;
            int offset = param.offset;
            WriteableBitmap wbmp = param.wbmp;
            unsafe
            {
                for (int row = 0; row < wbmpRowCount; row++)
                {
                    for (int col = 0; col < wbmpColCount; col++)
                    {
                        int sumBlue = 0;
                        int sumGreen = 0;
                        int sumRed = 0;
                        for (int i = -(anchorKernel.r - 1), cnvI = 0; i < (cnvMatRowCount - (anchorKernel.r - 1)); i++, cnvI++) //We have -1s inside brackets because anchorKernel is indexed from 1, while we need index from 0. //+1 outside to include last element //notice that 0 is included in this (for the center element)
                        {
                            for (int j = -(anchorKernel.c - 1), cnvJ = 0; j < (cnvMatColCount - (anchorKernel.c - 1)); j++, cnvJ++)
                            {
                                int r = ImgUtil.Clamp(row + i, 0, wbmp.PixelHeight - 1);
                                int c = ImgUtil.Clamp(col + j, 0, wbmp.PixelWidth - 1);
                                _pixel_bgr24_bgra32* currPx = (_pixel_bgr24_bgra32*)cloneWbmp.GetPixelIntPtrAt(r, c);
                                sumBlue += currPx->blue * sqrCnvMat[cnvI, cnvJ];
                                sumGreen += currPx->green * sqrCnvMat[cnvI, cnvJ];
                                sumRed += currPx->red * sqrCnvMat[cnvI, cnvJ];
                            }
                        }
                        sumBlue = (int)(sumBlue / (double)divisor);
                        sumGreen = (int)(sumGreen / (double)divisor);
                        sumRed = (int)(sumRed / (double)divisor);
                        sumBlue += offset;
                        sumGreen += offset;
                        sumRed += offset;
                        _pixel_bgr24_bgra32* px = (_pixel_bgr24_bgra32*)wbmp->GetPixelIntPtrAt(row, col);
                        px->blue = (byte)ImgUtil.Clamp(sumBlue, 0, 255);
                        px->green = (byte)ImgUtil.Clamp(sumGreen, 0, 255);
                        px->red = (byte)ImgUtil.Clamp(sumRed, 0, 255);
                    }
                }
            }
            return wbmp;
        }
    }
}
