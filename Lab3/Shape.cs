using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1.Lab3
{ 
    public class Shape //will make it abstract later..
    {
        public List<Tuple<int,int>> points = new List<Tuple<int,int>>();
        //when moving, only change the latest point (via sorting?)?
        public WriteableBitmap draw(WriteableBitmap wbmp) 
        {
            int pointSquareDimensions = 20;
            if (pointSquareDimensions == 0)
                return wbmp;
            int pseudoRadius = ((pointSquareDimensions - 1) / 2 + 1)-1;
            int borderTh = 3;
            unsafe
            {
                foreach (var point in points)
                {
                    if (point.Item1 >= 0 && point.Item1 <= wbmp.PixelHeight)
                    {
                        if (point.Item2 >= 0 && point.Item2 <= wbmp.PixelWidth)
                        {
                            int minR = MathUtil.Clamp(point.Item1 - pseudoRadius, 0, wbmp.PixelHeight);
                            int maxR = MathUtil.Clamp(point.Item1 + pseudoRadius, 0, wbmp.PixelHeight);
                            int minC = MathUtil.Clamp(point.Item2 - pseudoRadius, 0, wbmp.PixelWidth);
                            int maxC = MathUtil.Clamp(point.Item2 + pseudoRadius, 0, wbmp.PixelWidth);
                            for (int r=minR; r<=maxR;r++)
                            {
                                for(int c= minC; c<=maxC;c++)
                                {
                                    _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(r, c);
                                    pixPtr->blue = 0;
                                    pixPtr->green = 0;
                                    pixPtr->red = 255;
                                    if ((r <= minR + borderTh || r >= maxR - borderTh) || (c <= minC + borderTh || c >= maxC - borderTh)) //(r <= minR + borderTh || r >= maxR - borderTh) && (c <= minC + borderTh || c >= maxC - borderTh) this one makes a cross
                                        pixPtr->red = 0;
                                }
                            }
                        }
                    }
                }
            }
            return wbmp;
        }
    }
}
