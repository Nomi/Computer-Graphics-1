using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab1.LabPart
{
    public static class MedianFilter
    {
        public static void MedianFilter3x3(WriteableBitmap wbmp)
        {
            WriteableBitmap cloneWbmp = wbmp.Clone();
            //int[,] pixelSample = new int[3, 3];
            //List<byte> blueVals= new List<byte>
            byte[] blueVals = new byte[3 * 3];
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
                                int r = MathUtil.Clamp(row + i, 0, wbmp.PixelHeight - 1);
                                int c = MathUtil.Clamp(col + j, 0, wbmp.PixelWidth - 1);
                                _pixel_bgr24_bgra32* currPx = (_pixel_bgr24_bgra32*)cloneWbmp.GetPixelIntPtrAt(r, c);
                                blueVals[(i + 1) * 3 + (j + 1)] = currPx->blue;
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
    }
}
