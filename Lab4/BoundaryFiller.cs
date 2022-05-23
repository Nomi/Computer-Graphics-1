using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Computer_Graphics_1.Lab3;
using Computer_Graphics_1.HelperClasses.Extensions;
using System.Windows;

namespace Computer_Graphics_1.Lab4
{
    public static class BoundaryFiller
    {
        public static unsafe void BoundaryFill4(WriteableBitmap wbmp, int x, int y, Color borderColor, Color fillColor)
        {
            Stack<(int, int)> pointStoreStack = new Stack<(int, int)>();
            pointStoreStack.Push((x, y));

            wbmp.Lock();
            while (pointStoreStack.Count != 0)
            {
                var item = pointStoreStack.Pop();
                if (item.Item1 < 0 || item.Item1 >= wbmp.PixelWidth || item.Item2 < 0 || item.Item2 >= wbmp.PixelHeight)
                    continue;

                var col = GetColorOfPixel(wbmp, item.Item1, item.Item2);
                if (!(borderColor.R==col.R && borderColor.G==col.G&&borderColor.B==col.B) && !(fillColor.R == col.R && fillColor.G == col.G && fillColor.B == col.B) )
                {
                    wbmp.PutPixel(item.Item1, item.Item2, fillColor);

                    pointStoreStack.Push((item.Item1 + 1, item.Item2));
                    pointStoreStack.Push((item.Item1 - 1, item.Item2));
                    pointStoreStack.Push((item.Item1, item.Item2 + 1));
                    pointStoreStack.Push((item.Item1, item.Item2 - 1));
                }
            }
            wbmp.Unlock();
        }

        public static unsafe void BoundaryFill8(WriteableBitmap wbmp, int x, int y, Color borderColor, Color fillColor)
        {
            Stack<(int, int)> pointStoreStack = new Stack<(int, int)>();
            pointStoreStack.Push((x, y));
            System.Windows.Media.Color oldColor = GetColorOfPixel(wbmp, x, y);

            wbmp.Lock();
            while (pointStoreStack.Count != 0)
            {
                var item = pointStoreStack.Pop();
                if (item.Item1 < 0 || item.Item1 >= wbmp.PixelWidth || item.Item2 < 0 || item.Item2 >= wbmp.PixelHeight)
                    continue;

                var col = GetColorOfPixel(wbmp, item.Item1, item.Item2);
                if (!(borderColor.R == col.R && borderColor.G == col.G && borderColor.B == col.B) && !(fillColor.R == col.R && fillColor.G == col.G && fillColor.B == col.B))
                {
                    wbmp.PutPixel(item.Item1, item.Item2, fillColor);

                    pointStoreStack.Push((item.Item1 + 1, item.Item2));
                    pointStoreStack.Push((item.Item1 - 1, item.Item2));
                    pointStoreStack.Push((item.Item1, item.Item2 + 1));
                    pointStoreStack.Push((item.Item1, item.Item2 - 1));

                    pointStoreStack.Push((item.Item1 + 1, item.Item2 + 1));
                    pointStoreStack.Push((item.Item1 - 1, item.Item2 - 1));
                    pointStoreStack.Push((item.Item1 - 1, item.Item2 + 1));
                    pointStoreStack.Push((item.Item1 + 1, item.Item2 - 1));                }
            }
            wbmp.Unlock();
        }

        public static System.Windows.Media.Color GetColorOfPixel(WriteableBitmap bitmap, int x, int y)
        {
            var color = new System.Windows.Media.Color();
            if (x < 0 || y < 0 || x >= bitmap.PixelWidth || y >= bitmap.PixelHeight)
                return color;
            unsafe
            {
                IntPtr pBackBuffer = bitmap.BackBuffer + y * bitmap.BackBufferStride + x * 4;

                int color_data = *((int*)pBackBuffer);
                color.B = (byte)((color_data & 0x000000FF) >> 0);
                color.G = (byte)((color_data & 0x0000FF00) >> 8);
                color.R = (byte)((color_data & 0x00FF0000) >> 16);
                color.A = (byte)((color_data & 0xFF000000) >> 24);
            }
            return color;
        }
    }
}
