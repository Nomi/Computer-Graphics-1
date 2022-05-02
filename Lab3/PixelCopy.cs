using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab3
{
    public static class PixelCopy
    {
        private static void pixelCopy_TrackPixelsInList(this WriteableBitmap wbmp,int x, int y, Color color, int thickness, bool isHorizontal, ref List<Point> drawnPixels)
        {
            for (int j = -thickness / 2; j <= thickness / 2; ++j)
            {
                int useX=-1, useY=-1;
                if (isHorizontal)
                {
                    int new_y = y + j;
                    if (new_y >= 0 && new_y <= wbmp.PixelHeight)
                    {
                        useX = x; useY = new_y;
                    }
                }
                else
                {
                    int new_x = x + j;
                    if (new_x >= 0 && new_x <= wbmp.PixelWidth)
                    {
                        useX = new_x; useY = y;
                    }
                }

                if(useX!=-1&& useY!=-1)
                {
                    drawnPixels.Add(new Point(useX, useY));
                    wbmp.PutPixel(useX, useY, color);
                }
            }
        }

        public static void pxlCpyPutPixel_TrackPixelsInList(this WriteableBitmap wbmp, int x, int y, Color color, int thickness, bool isHorizontal, ref List<Point> drawnPixels)
        {
            unsafe
            {
                if (x >= 0 && x <= wbmp.PixelWidth && y >= 0 && y <= wbmp.PixelHeight)
                {
                    drawnPixels.Add(new Point(x, y));
                    wbmp.PutPixel(x, y, color);
                    if (thickness > 1)
                    {
                        wbmp.pixelCopy_TrackPixelsInList(x, y, color, thickness, isHorizontal, ref drawnPixels);
                    }
                }
            }
        }


        private static void pixelCopy(this WriteableBitmap wbmp, int x, int y, Color color, int thickness, bool isHorizontal)
        {
            for (int j = -thickness / 2; j <= thickness / 2; ++j)
            {
                int useX = -1, useY = -1;
                if (isHorizontal)
                {
                    int new_y = y + j;
                    if (new_y >= 0 && new_y <= wbmp.PixelHeight)
                    {
                        useX = x; useY = new_y;
                    }
                }
                else
                {
                    int new_x = x + j;
                    if (new_x >= 0 && new_x <= wbmp.PixelWidth)
                    {
                        useX = new_x; useY = y;
                    }
                }

                if (useX != -1 && useY != -1)
                {
                    wbmp.PutPixel(useX, useY, color);
                }
            }
        }

        public static void pxlCpyPutPixel(this WriteableBitmap wbmp, int x, int y, Color color, int thickness, bool isHorizontal)
        {
            unsafe
            {
                if (x >= 0 && x <= wbmp.PixelWidth && y >= 0 && y <= wbmp.PixelHeight)
                {
                    wbmp.PutPixel(x, y, color);
                    if (thickness > 1)
                    {
                        wbmp.pixelCopy(x, y, color, thickness, isHorizontal);
                    }
                }
            }
        }
    }
}
