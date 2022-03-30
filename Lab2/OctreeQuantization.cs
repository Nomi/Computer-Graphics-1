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
            QuantizingOctree oct = new QuantizingOctree();
            oct.root = new QuantOcNode();
            oct.root.parent = null;
            oct.root.depth = 0;
            oct.colorLimit = k;
            OctreeAverageQuantization octAvg = new OctreeAverageQuantization();
            int colors = 0;
            unsafe
            {
                wbmp.Lock();
                int colorsAdded = 0;
                for (int y = 0; y < wbmp.PixelHeight; y++)
                {
                    for (int x = 0; x < wbmp.PixelWidth; x++)
                    {
                        _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y, x);
                        Color colPix = Color.FromArgb(pixPtr->red, pixPtr->green, pixPtr->blue);
                        //colorsAdded+=octreeQuant.quantizer.AddColor(colPix); //get it to return if a new leaf was added.
                        ////if(colorsAdded>k)
                        ////{
                        ////    octreeQuant.limitedPalette = octreeQuant.quantizer.LimitPalette(k);
                        ////    colorsAdded = octreeQuant.limitedPalette.Count();
                        ////}
                        //oct.insert(ref oct.root, colPix, 0);
                        colors = octAvg.insert(colPix);
                        if (colors > k)
                        {
                            octAvg.RemoveExtraColors(k);
                        }

                        ////if a new leaf is added and it EXCEEDS maximum color count,
                        ////quantize the current colors.

                        ////don't go forward after a leaf(image)
                    }
                }



                int uniqColors =colors;// oct.root.GetChildTreeColorCount();
                uniqColors = octAvg.root.GetChildTreeUniqueColorCount();
                int limit = k;  //Decimal.ToInt32(numericUpDown.Value) seems like a useful function (should be helpful instead of casting numeric up down of Average Dithering).
                //octreeQuant.limitedPalette = octreeQuant.quantizer.GetPalette(limit);

                for (int y = 0; y < wbmp.PixelHeight; y++)
                {
                    for (int x = 0; x < wbmp.PixelWidth; x++)
                    {
                        _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y, x);
                        Color colPix = Color.FromArgb(pixPtr->red, pixPtr->green, pixPtr->blue);
                        Color quantizedColPix = octAvg.GetQuantizedColor(colPix);//octreeQuant.limitedPalette[octreeQuant.quantizer.GetPaletteIndex(colPix)];
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
