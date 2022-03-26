using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab2
{
    public static class AverageDithering
    {
        public static WriteableBitmap apply(WriteableBitmap wbmp,int K=2)
        {
            unsafe
            {
                wbmp.Lock();

                int sumBlue = 0, sumGreen = 0, sumRed = 0;
                int numChannels = wbmp.GetPixelSizeBytes();
                int totalPixels = wbmp.PixelWidth * wbmp.PixelHeight;
                for (int i = 0; i < wbmp.PixelHeight; i++)
                {
                    for (int j = 0; j < wbmp.BackBufferStride; j += numChannels)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j / numChannels);
                        sumBlue+=ptrPX->blue;
                        sumGreen+=ptrPX->green;
                        sumRed+=ptrPX->red;
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

                int l_blueIntervalSize = avgBlue / (K/2);
                int l_greenIntervalSize = avgGreen / (K/2);
                int l_redIntervalSize = avgRed / (K / 2);

                int r_blueIntervalSize = ((255 - avgBlue) / (K / 2));
                int r_greenIntervalSize = ((255 - avgGreen) / (K / 2));
                int r_redIntervalSize = ((255 - avgRed) / (K / 2));

                List<int> blueLevels = new List<int>();
                List<int> greenLevels = new List<int>();
                List<int> redLevels = new List<int>();
                for (int i=0;i<K/2;i++)
                {
                    blueLevels.Add(i*l_blueIntervalSize);
                    greenLevels.Add(i * l_greenIntervalSize);
                    redLevels.Add(i * l_redIntervalSize);

                    blueLevels.Add(255- i*r_blueIntervalSize);
                    greenLevels.Add(255- i * r_greenIntervalSize);
                    redLevels.Add(255- i * r_redIntervalSize);
                }

                for (int i = 0; i < wbmp.PixelHeight; i++)
                {
                    for (int j = 0; j < wbmp.BackBufferStride; j += numChannels)
                    {
                        _pixel_bgr24_bgra32* ptrPX = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(i, j / numChannels);
                        int closestBlue = blueLevels.OrderBy(item => Math.Abs(ptrPX->blue - item)).First();
                        ptrPX->blue = (byte) closestBlue;
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
