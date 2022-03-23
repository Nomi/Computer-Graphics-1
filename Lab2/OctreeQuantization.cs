using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using Computer_Graphics_1.Lab2.OctreeQuantizationHelper;

//

namespace Computer_Graphics_1.Lab2
{
    public static class OctreeQuantization
    {
        private static class octreeQuant
        {
            public static OctreeQuantizer quantizer = null;
            public static List<Color> limitedPalette = null;
            public static List<Color> originalPalette = null;
        }
        public static WriteableBitmap Apply(WriteableBitmap wbmp,int k=2)
        {
            octreeQuant.quantizer = new OctreeQuantizer();
            octreeQuant.limitedPalette = new List<Color>();
            octreeQuant.originalPalette = new List<Color>();
            WriteableBitmap cloneWbmp = wbmp.Clone();

            unsafe
            {
                wbmp.Lock();
                for (int y = 0; y < cloneWbmp.PixelHeight; y++)
                {
                    for (int x = 0; x < cloneWbmp.PixelWidth; x++)
                    {
                        _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)cloneWbmp.GetPixelIntPtrAt(y, x);
                        Color colPix = Color.FromArgb(pixPtr->red, pixPtr->green, pixPtr->blue);
                        octreeQuant.quantizer.AddColor(colPix);
                    }
                }




                int limit = k;  //Decimal.ToInt32(numericUpDown.Value) seems like a useful function (should be helpful instead of casting numeric up down of Average Dithering).
                octreeQuant.limitedPalette = octreeQuant.quantizer.GetPalette(limit);

                for (int y = 0; y < wbmp.PixelHeight; y++)
                {
                    for (int x = 0; x < wbmp.PixelWidth; x++)
                    {
                        _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y, x);
                        Color colPix = Color.FromArgb(pixPtr->red, pixPtr->green, pixPtr->blue);
                        Color quantizedColPix = octreeQuant.limitedPalette[octreeQuant.quantizer.GetPaletteIndex(colPix)];
                        pixPtr->blue = quantizedColPix.B;
                        pixPtr->green = quantizedColPix.G;
                        pixPtr->red = quantizedColPix.R;
                    }
                }
                wbmp.Unlock();
            }

            return wbmp;
        }

    }
}
