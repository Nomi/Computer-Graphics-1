//#define _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
/*
* To avoid defining this symbol in every file, refer to: https://stackoverflow.com/questions/436369/how-to-define-a-constant-globally-in-c-sharp-like-debug
* Also, learn about Conditional Attribute and the like here: https://stackoverflow.com/a/975370
*/
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
    public static class GuptaSproulAA //got a little help from some GitHub repo.
    {
        public static double Lerp(int a, int b, double t)
        {
            return (1 - t) * a + t * b;
        }

        public static double cov(double d, double r)
        {
            if (d <= r)
                return ((Math.Acos(d / r) - d * Math.Sqrt(r * r - d * d)) / Math.PI);
            else
                return 0;
        }

        public static double Coverage(double w, double D, double r)
        {
            if (w >= r)
            {
                if (w <= D) return cov(D - w, r);
                else if (0 <= D && D <= w) return 1 - cov(w - D, r);
            }
            else
            {
                if (0 <= D && D <= w) return 1 - cov(w - D, r) - cov(w + D, r);
                else if (w <= D && D <= r - w) return cov(D - w, r) - cov(D + w, r);
                else if (r - w <= D && D <= r + w) return cov(D - w, r);
            }
            return 0;
        }

        public static double IntensifyPixel(ref WriteableBitmap wbmp,int x, int y, double thickness, double distance, Color lineColor)
        {
            double r = 0.5;
            double cov = Coverage(thickness, distance, r);

            if (cov > 0)
            {
                unsafe
                {
                    _pixel_bgr24_bgra32* ptrPx = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(y, x);
                    ptrPx->red= (byte)Lerp(255, lineColor.R, cov);
                    ptrPx->green= (byte)Lerp(255, lineColor.G, cov);
                    ptrPx->blue= (byte)Lerp(255, lineColor.B, cov);
                }
            }
            return cov;
        }

        private static void CopyPixelsAA(ref WriteableBitmap wbmp,int x, int y, double thickness, Color c, double d_invDenom, double v_d, int dx, int dy)
        {
            wbmp.PutPixel(x, y, c);
            //IntensifyPixel()
            for (int i = 1; IntensifyPixel(ref wbmp, x + i * dx, y + i * dy, thickness, i * d_invDenom - v_d, c) > 0; ++i) ;
            for (int i = 1; IntensifyPixel(ref wbmp, x - i * dx, y - i * dy, thickness, i * d_invDenom + v_d, c) > 0; ++i) ;
        }

        public static WriteableBitmap drawGSAA(this Shape shp, WriteableBitmap wbmp, bool showPoints=true)
        {
#if _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
        throw new NotImplementedException("Need to implement the Gupta-Sproul part's pixel tracking logic by modifying everywhere you put pixels to also store pixels in the relevant array provided in Shape class."); //just got reminded that innerexceptions exist, they're nice. Keep in mind for future, will help somewhere in nested exception-ing.
#endif
            if (!(shp.isShapeType(SupportedShapes.Line)))
                throw new NotSupportedException("GuptaSproul only works for lines");
            if (showPoints)
                wbmp = shp.drawPoints(wbmp);
            if (shp.vertices.Count >= 2)
            {
                int thickness = shp.thickness;
                if ( thickness < 1)
                {
                    thickness = 1;
                }
                for (int i = 1; i < shp.vertices.Count; i++)//we start from i=1 because we'll be drawing from previous index to current one (initially from 0th to 1th)
                {
                    //For points, x is column, y is row ( and item1 is x, item2 is y)

                    //the following only handles lines with angles between 0 to 45 degrees (inclusive of 0 and 45).
                    int x1 = shp.vertices[i - 1].X;
                    int x2 = shp.vertices[i].X;

                    int y1 = shp.vertices[i - 1].Y;
                    int y2 = shp.vertices[i].Y;
                    wbmp = GuptaSproull(shp, wbmp, x1, y1, x2, y2, shp.color, thickness);
                }
            }
            return wbmp;
        }

        private static WriteableBitmap GuptaSproull(Shape shp,WriteableBitmap wbmp, int x1, int y1, int x2, int y2, Color color, double thickness)
        {
            int X1 = x1, X2 = x2, Y1 = y1, Y2 = y2;
            if (x2 < x1)
            {
                X1 = x2; Y1 = y2;
                X2 = x1; Y2 = y1;
            }
            int dy = Y2 - Y1;
            int dx = X2 - X1;
            if (Math.Abs(dx) > Math.Abs(dy)) //horizontal
                HorizontalLine(ref wbmp,X1, Y1, X2, Y2, color, thickness);
            else
                VerticalLine(ref wbmp, X1, Y1, X2, Y2, color, thickness);
            return wbmp;
        }

        public static void HorizontalLine(ref WriteableBitmap wbmp,int x1, int y1, int x2, int y2, Color color, double thickness)
        {
            int dx = x2 - x1;
            int dy = Math.Abs(y2 - y1);
            int d = 2 * (dy - dx);
            int dE = 2 * dy;
            int dNE = 2 * (dy - dx);
            int xStart = x1, yStart = y1;
            int xEnd = x2, yEnd = y2;

            int two_v_dx = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dx * invDenom; //precomputed constant

            int delta = 1;
            if (y2 - y1 < 0) delta = -1;
            CopyPixelsAA(ref wbmp, xStart, yStart, thickness / 2, color, two_dy_invDenom, two_v_dx * invDenom, 0, delta);
            CopyPixelsAA(ref wbmp, xEnd, yEnd, thickness / 2, color, two_dy_invDenom, two_v_dx * invDenom, 0, -delta);
            while (xStart < xEnd)
            {
                xStart += 1;
                xEnd -= 1;
                if (d < 0)
                {
                    two_v_dx = d + dx;
                    d += dE;
                }
                else
                {
                    two_v_dx = d - dx;
                    d += dNE;
                    yStart += delta;
                    yEnd -= delta;
                }
                CopyPixelsAA(ref wbmp, xStart, yStart, thickness / 2, color, two_dy_invDenom, two_v_dx * invDenom, 0, delta);
                CopyPixelsAA(ref wbmp, xEnd, yEnd, thickness / 2, color, two_dy_invDenom, two_v_dx * invDenom, 0, -delta);
            }
        }

        public static void VerticalLine(ref WriteableBitmap wbmp,int x1, int y1, int x2, int y2, Color color, double thickness)
        {
            int dx = x2 - x1;
            int dy = Math.Abs(y2 - y1);
            int d = 2 * (dx - dy);
            int dN = 2 * dx;
            int dNE = 2 * (dx - dy);
            int xStart = x1, yStart = y1;
            int xEnd = x2, yEnd = y2;

            int two_v_dy = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dy * invDenom; //precomputed constant

            int delta = 1;
            if (y2 - y1 < 0) delta = -1;
            CopyPixelsAA(ref wbmp, xStart, yStart, thickness / 2, color, two_dy_invDenom, two_v_dy * invDenom, 1, 0);
            CopyPixelsAA(ref wbmp, xEnd, yEnd, thickness / 2, color, two_dy_invDenom, two_v_dy * invDenom, -1, 0);
            while (delta * (yStart - yEnd) < 0)
            {
                yStart += delta;
                yEnd -= delta;
                if (d < 0)
                {
                    two_v_dy = d + dy;
                    d += dN;
                }
                else
                {
                    two_v_dy = d - dy;
                    d += dNE;
                    xStart += 1;
                    xEnd -= 1;
                }
                CopyPixelsAA(ref wbmp, xStart, yStart, thickness / 2, color, two_dy_invDenom, two_v_dy * invDenom, 1, 0);
                CopyPixelsAA(ref wbmp, xEnd, yEnd, thickness / 2, color, two_dy_invDenom, two_v_dy * invDenom, -1, 0);
            }
        }
    }
}
