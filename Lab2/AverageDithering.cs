using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab2
{
    public static class AverageDithering
    {
        private class _Interval
        {
            public int sumChannel;
            public int pixelCount;
            int _start, _end;
            //public _Interval(int startInt, endInt)
            //{

            //}
            public _Interval()
            {
                sumChannel = 0;pixelCount = 0;
            }
            public float GetAverage()
            {
                return (sumChannel / (float)pixelCount);
            }
            public float GetThreshold()
            {
                return this.GetAverage();
            }
        }
        public static WriteableBitmap apply(WriteableBitmap wbmp,int K=2)
        {
            K=MathUtil.Clamp(K, 3, 254);
            if(K%2!=0)
            {
                K--;
            }

            unsafe
            {
                wbmp.Lock();

                int sumBlue = 0, sumGreen = 0, sumRed = 0;
                int numChannels = wbmp.GetPixelNumChannels8bit();
                int totalPixels = wbmp.PixelWidth * wbmp.PixelHeight;
                decimal decIntervalSize = (255 /(decimal)(K-1)); //K-1 because for K values there are k-1 intervals.
                int intervalSize =(int) Decimal.Ceiling(decIntervalSize);
                List<int> levels = new List<int>(); //intervals are the levels.
                List<_Interval> blueIntervals = new List<_Interval>();
                List<_Interval> greenIntervals = new List<_Interval>();
                List<_Interval> redIntervals = new List<_Interval>(); 
                for (int i=0; i<=K-1; i++) //will occur k times, obviously.
                {
                    int lvl = i * intervalSize;
                    if(lvl > 255) lvl = 255; //cuz intervalSize and intervals should've been floats as 255 is odd, and k is always even. and my division above makes intervalSize Greater than it is
                    levels.Add(lvl); //notice content is already sorted in ascending order.
                    blueIntervals.Add(new _Interval()); //notice that this intervals array is aligned with the levels array,
                                                    //that is: i-th interval goes from i-th level to i+1th interval.
                                                    //This leaves us with one extra interval, which we will handle right after the for loop.
                    greenIntervals.Add(new _Interval());
                    redIntervals.Add(new _Interval());
                }
                blueIntervals.RemoveAt(K-1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;
                greenIntervals.RemoveAt(K-1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;
                redIntervals.RemoveAt(K-1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;


                for (int i = 0; i < wbmp.PixelHeight; i++) //can convert to multithreaded using bytearray instead of _pixel.. struct.
                {
                    for (int j = 0; j < wbmp.PixelWidth; j ++)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j);

                        int indx_lowerBlue = levels.FindLastIndex(x=> x<ptrPX->blue);//Where(x=> x<ptrPX->blue).OrderBy(item => Math.Abs(ptrPX->blue - item)).First();
                        if (indx_lowerBlue == -1)
                        {
                            if (ptrPX->blue == 0)
                            {
                                indx_lowerBlue = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerBlue = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerBlue = levels.ElementAt(indx_lowerBlue);
                        int upperBlue = MathUtil.Clamp(lowerBlue + intervalSize, 0, 255);
                        blueIntervals.ElementAt(indx_lowerBlue).pixelCount++; //interval from lowerblue to upperblue.
                        blueIntervals.ElementAt(indx_lowerBlue).sumChannel+=ptrPX->blue; //interval from lowerblue to upperblue.

                        int indx_lowerGreen = levels.FindLastIndex(x => x < ptrPX->green);
                        if (indx_lowerGreen == -1)
                        {
                            if (ptrPX->green == 0)
                            {
                                indx_lowerGreen = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerGreen = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerGreen = levels.ElementAt(indx_lowerGreen);
                        int upperGreen = MathUtil.Clamp(lowerGreen + intervalSize, 0, 255);
                        greenIntervals.ElementAt(indx_lowerGreen).pixelCount++;
                        greenIntervals.ElementAt(indx_lowerGreen).sumChannel += ptrPX->green;

                        int indx_lowerRed = levels.FindLastIndex(x => x < ptrPX->red);
                        if (indx_lowerRed == -1)
                        {
                            if (ptrPX->red == 0)
                            {
                                indx_lowerRed = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerRed = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerRed = levels.ElementAt(indx_lowerRed);
                        int upperRed = MathUtil.Clamp(lowerRed + intervalSize, 0, 255);
                        redIntervals.ElementAt(indx_lowerRed).pixelCount++; 
                        redIntervals.ElementAt(indx_lowerRed).sumChannel += ptrPX->red; 
                    }
                }

                

                for(int r=0; r< wbmp.PixelHeight;r++)
                {
                    for(int c=0;c<wbmp.PixelWidth;c++)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(r, c);

                        int indx_lowerBlue = levels.FindLastIndex(x => x < ptrPX->blue);
                        if (indx_lowerBlue == -1)
                        {
                            if(ptrPX->blue==0)
                            {
                                indx_lowerBlue = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerBlue = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerBlue = levels.ElementAt(indx_lowerBlue);
                        int upperBlue = MathUtil.Clamp(lowerBlue + intervalSize, 0, 255);
                        if (ptrPX->blue >= blueIntervals.ElementAt(indx_lowerBlue).GetThreshold())
                        {
                            ptrPX->blue = (byte)upperBlue;
                        }
                        else
                        {
                            ptrPX->blue = (byte)lowerBlue;
                        }

                        int indx_lowerGreen = levels.FindLastIndex(x => x < ptrPX->green);
                        if (indx_lowerGreen == -1)
                        {
                            if (ptrPX->green == 0)
                            {
                                indx_lowerGreen = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerGreen = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerGreen = levels.ElementAt(indx_lowerGreen);
                        int upperGreen = MathUtil.Clamp(lowerGreen + intervalSize, 0, 255);
                        if (ptrPX->green >= greenIntervals.ElementAt(indx_lowerGreen).GetThreshold())
                        {
                            ptrPX->green = (byte)upperGreen;
                        }
                        else
                        {
                            ptrPX->green = (byte)lowerGreen;
                        }

                        int indx_lowerRed = levels.FindLastIndex(x => x < ptrPX->red);
                        if (indx_lowerRed == -1)
                        {
                            if (ptrPX->red == 0)
                            {
                                indx_lowerRed = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerRed = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerRed = levels.ElementAt(indx_lowerRed);
                        int upperRed = MathUtil.Clamp(lowerRed + intervalSize, 0, 255);
                        if (ptrPX->red >= blueIntervals.ElementAt(indx_lowerRed).GetThreshold())
                        {
                            ptrPX->red = (byte)upperRed;
                        }
                        else
                        {
                            ptrPX->red = (byte)lowerRed;
                        }
                    }
                }

                wbmp.Unlock();
            }
            return wbmp;
        }

        public static WriteableBitmap applyYCbCr(WriteableBitmap wbmp, int K = 2)
        {
            K = MathUtil.Clamp(K, 3, 254);
            if (K % 2 != 0)
            {
                K--;
            }
            _pixel_YCbCr24[,] imgYCbCr = new _pixel_YCbCr24[wbmp.PixelHeight, wbmp.PixelWidth];
            unsafe
            {
                wbmp.Lock();

                int sumY = 0, sumCb = 0, sumCr = 0;
                int numChannels = wbmp.GetPixelNumChannels8bit();
                int totalPixels = wbmp.PixelWidth * wbmp.PixelHeight;
                decimal decIntervalSize = (255 / (decimal)(K - 1)); //K-1 because for K values there are k-1 intervals.
                int intervalSize = (int)Decimal.Ceiling(decIntervalSize);
                List<int> levels = new List<int>(); //intervals are the levels.
                List<_Interval> yIntervals = new List<_Interval>();
                List<_Interval> CbIntervals = new List<_Interval>();
                List<_Interval> CrIntervals = new List<_Interval>();
                for (int i = 0; i <= K - 1; i++) //will occur k times, obviously.
                {
                    int lvl = i * intervalSize;
                    if (lvl > 255) lvl = 255; //cuz intervalSize and intervals should've been floats as 255 is odd, and k is always even. and my division above makes intervalSize Greater than it is
                    levels.Add(lvl); //notice content is already sorted in ascending order.
                    yIntervals.Add(new _Interval()); //notice that this intervals array is aligned with the levels array,
                                                        //that is: i-th interval goes from i-th level to i+1th interval.
                                                        //This leaves us with one extra interval, which we will handle right after the for loop.
                    CbIntervals.Add(new _Interval());
                    CrIntervals.Add(new _Interval());
                }
                yIntervals.RemoveAt(K - 1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;
                CbIntervals.RemoveAt(K - 1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;
                CrIntervals.RemoveAt(K - 1); //indexed from 0, removing k-th element actually //there should only be k-1 intervals for k levels;


                for (int i = 0; i < wbmp.PixelHeight; i++) //can convert to multithreaded using bytearray instead of _pixel.. struct.
                {
                    for (int j = 0; j < wbmp.PixelWidth; j++)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j);
                        _pixel_YCbCr24 ycbcrPX;
                        ycbcrPX.Y = (byte)MathUtil.Clamp((int)(0.299 * ptrPX->red + 0.587 * ptrPX->green + 0.114 * ptrPX->blue), 0, 255);
                        ycbcrPX.Cb= (byte)MathUtil.Clamp((int)(128 - 0.169 * ptrPX->red - 0.331 * ptrPX->green + 0.5 * ptrPX->blue), 0, 255);
                        ycbcrPX.Cr = (byte)MathUtil.Clamp((int)(128 + 0.5 * ptrPX->red - 0.419 * ptrPX->green - 0.081 * ptrPX->blue), 0, 255);
                        imgYCbCr[i, j] = ycbcrPX;

                        int indx_lowerY = levels.FindLastIndex(x => x < ycbcrPX.Y);//Where(x=> x<ptrPX->blue).OrderBy(item => Math.Abs(ptrPX->blue - item)).First();
                        if (indx_lowerY == -1)
                        {
                            if (ycbcrPX.Y == 0)
                            {
                                indx_lowerY = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerY = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerY = levels.ElementAt(indx_lowerY);
                        int upperY = MathUtil.Clamp(lowerY + intervalSize, 0, 255);
                        yIntervals.ElementAt(indx_lowerY).pixelCount++; //interval from lowerblue to upperblue.
                        yIntervals.ElementAt(indx_lowerY).sumChannel += ycbcrPX.Y; //interval from lowerblue to upperblue.

                        int indx_lowerCb = levels.FindLastIndex(x => x < ycbcrPX.Cb);
                        if (indx_lowerCb == -1)
                        {
                            if (ycbcrPX.Cb == 0)
                            {
                                indx_lowerCb = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerCb = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerCb = levels.ElementAt(indx_lowerCb);
                        int upperCb = MathUtil.Clamp(lowerCb + intervalSize, 0, 255);
                        CbIntervals.ElementAt(indx_lowerCb).pixelCount++;
                        CbIntervals.ElementAt(indx_lowerCb).sumChannel += ycbcrPX.Cb;

                        int indx_lowerCr = levels.FindLastIndex(x => x < ycbcrPX.Cr);
                        if (indx_lowerCr == -1)
                        {
                            if (ycbcrPX.Cr == 0)
                            {
                                indx_lowerCr = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerCr = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerCr = levels.ElementAt(indx_lowerCr);
                        int upperCr = MathUtil.Clamp(lowerCr + intervalSize, 0, 255);
                        CrIntervals.ElementAt(indx_lowerCr).pixelCount++;
                        CrIntervals.ElementAt(indx_lowerCr).sumChannel += ycbcrPX.Cr;
                    }
                }



                for (int r = 0; r < wbmp.PixelHeight; r++)
                {
                    for (int c = 0; c < wbmp.PixelWidth; c++)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(r, c);
                        _pixel_YCbCr24 ycbcrPX = imgYCbCr[r, c];
                        int indx_lowerY = levels.FindLastIndex(x => x < ycbcrPX.Y);
                        if (indx_lowerY == -1)
                        {
                            if (ycbcrPX.Y == 0)
                            {
                                indx_lowerY = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerY = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerY = levels.ElementAt(indx_lowerY);
                        int upperY = MathUtil.Clamp(lowerY + intervalSize, 0, 255);
                        if (ycbcrPX.Y >= yIntervals.ElementAt(indx_lowerY).GetThreshold())
                        {
                            ycbcrPX.Y = (byte)upperY;
                        }
                        else
                        {
                            ycbcrPX.Y = (byte)lowerY;
                        }



                        int indx_lowerCb = levels.FindLastIndex(x => x < ycbcrPX.Cb);
                        if (indx_lowerCb == -1)
                        {
                            if (ycbcrPX.Cb == 0)
                            {
                                indx_lowerCb = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerCb = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerCb = levels.ElementAt(indx_lowerCb);
                        int upperCb = MathUtil.Clamp(lowerCb + intervalSize, 0, 255);
                        if (ycbcrPX.Cb >= CbIntervals.ElementAt(indx_lowerCb).GetThreshold())
                        {
                            ycbcrPX.Cb = (byte)upperCb;
                        }
                        else
                        {
                            ycbcrPX.Cb = (byte)lowerCb;
                        }




                        int indx_lowerCr = levels.FindLastIndex(x => x < ycbcrPX.Cr);
                        if (indx_lowerCr == -1)
                        {
                            if (ycbcrPX.Cr == 0)
                            {
                                indx_lowerCr = 0;
                            }
                            else //when it is 255
                            {
                                indx_lowerCr = levels.Count - 2; //one -1 because indexed from 0, another because 255 belongs in the interval between the previous element and 255
                            }
                        }
                        int lowerCr = levels.ElementAt(indx_lowerCr);
                        int upperCr = MathUtil.Clamp(lowerCr + intervalSize, 0, 255);
                        if (ycbcrPX.Cr >= yIntervals.ElementAt(indx_lowerCr).GetThreshold())
                        {
                            ycbcrPX.Cr = (byte)upperCr;
                        }
                        else
                        {
                            ycbcrPX.Cr = (byte)lowerCr;
                        }

                        ptrPX->red = (byte)MathUtil.Clamp((int)(ycbcrPX.Y+1.402*(ycbcrPX.Cr-128)), 0, 255);
                        int Y = ycbcrPX.Y, Cb = ycbcrPX.Cb, Cr = ycbcrPX.Cr;
                        ptrPX->green = (byte)MathUtil.Clamp((int)(Y-0.344*(Cb-128)-0.714*(Cr-128)), 0, 255);
                        ptrPX->blue = (byte)MathUtil.Clamp((int)(Y+1.772*(Cb-128)), 0, 255);
                    }
                }



                wbmp.Unlock();
            }
            return wbmp;
        }

        public static WriteableBitmap MyBadAttemptWhichTurnedIntoAverage_QUANTIZATION_instead(WriteableBitmap wbmp, int K = 2)
        {
            unsafe
            {
                wbmp.Lock();

                int sumBlue = 0, sumGreen = 0, sumRed = 0;
                int numChannels = wbmp.GetPixelNumChannels8bit();
                int totalPixels = wbmp.PixelWidth * wbmp.PixelHeight;
                for (int i = 0; i < wbmp.PixelHeight; i++)
                {
                    for (int j = 0; j < wbmp.BackBufferStride; j += numChannels)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j / numChannels);
                        sumBlue += ptrPX->blue;
                        sumGreen += ptrPX->green;
                        sumRed += ptrPX->red;
                    }
                }
                int avgBlue = sumBlue / totalPixels;
                int avgGreen = sumGreen / totalPixels;
                int avgRed = sumRed / totalPixels;

                //The following left and right interval sizes are based on something like percentiles
                //For example:
                //if green has a high average (meaning there probably are more pixels of greater than average value and lesser ones of smaller than average)
                //then, we'll end up with levels of bigger gaps between levels (less frequent) before\smaller_than the average
                //and smaller gaps between levels (more frequent) after\greater_than the average.

                int l_blueIntervalSize = avgBlue / (K / 2);
                int l_greenIntervalSize = avgGreen / (K / 2);
                int l_redIntervalSize = avgRed / (K / 2);

                int r_blueIntervalSize = ((255 - avgBlue) / (K / 2));
                int r_greenIntervalSize = ((255 - avgGreen) / (K / 2));
                int r_redIntervalSize = ((255 - avgRed) / (K / 2));

                List<int> blueLevels = new List<int>();
                List<int> greenLevels = new List<int>();
                List<int> redLevels = new List<int>();
                for (int i = 0; i < K / 2; i++)
                {
                    blueLevels.Add(i * l_blueIntervalSize);
                    greenLevels.Add(i * l_greenIntervalSize);
                    redLevels.Add(i * l_redIntervalSize);

                    blueLevels.Add(255 - i * r_blueIntervalSize);
                    greenLevels.Add(255 - i * r_greenIntervalSize);
                    redLevels.Add(255 - i * r_redIntervalSize);
                }

                for (int i = 0; i < wbmp.PixelHeight; i++)
                {
                    for (int j = 0; j < wbmp.BackBufferStride; j += numChannels)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j / numChannels);
                        int closestBlue = blueLevels.OrderBy(item => Math.Abs(ptrPX->blue - item)).First();
                        ptrPX->blue = (byte)closestBlue;
                        int closestGreen = greenLevels.OrderBy(item => Math.Abs(ptrPX->green - item)).First();
                        ptrPX->green = (byte)closestGreen;
                        int closestRed = redLevels.OrderBy(item => Math.Abs(ptrPX->red - item)).First();
                        ptrPX->red = (byte)closestRed;
                    }
                }

                wbmp.Unlock();
            }
            return wbmp;
        }
    }
}
