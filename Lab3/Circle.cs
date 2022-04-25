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
    public class Circle:Shape
    {
        public override WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness= 1) //uses Midpoint Circle (v2 with additions only) as per lecture notes.
        {
            //y might need to be reversed like in line, and pixelcopy needs to be used everywhere
            if (showPoints)
                wbmp=drawPoints(wbmp);
            if (points.Count() != 2)
                return wbmp;
            if (_thickness < 1)
            {
                thickness = 1;
            }
            Point center = new Point(points[0].X, points[0].Y);
            int radius = (int) Math.Round(
                Math.Sqrt(
                (float)Math.Pow(center.X-points[1].X,2)
                +(float)Math.Pow(center.Y - points[1].Y, 2)
                ));

            int dE = 3;
            int dSE = 5 - 2 * radius;
            int d = 1 - radius;
            int x = 0;
            int y = radius;
            wbmp.pxlCpyPutPixel(center.X, center.Y + y, color, thickness, true);
            wbmp.pxlCpyPutPixel(center.X, center.Y - y, color, thickness, true);
            wbmp.pxlCpyPutPixel(center.X + y, center.Y, color, thickness, false);
            wbmp.pxlCpyPutPixel(center.X - y, center.Y, color, thickness, false);
            while (y > x)
            {
                if (d < 0)
                {
                    d += dE;
                    dE += 2;
                    dSE += 2;
                }
                else
                {
                    d += dSE;
                    dE += 2;
                    dSE += 4;
                    --y;
                }
                ++x;
                wbmp.pxlCpyPutPixel(center.X + x, center.Y + y, color, thickness, true);
                wbmp.pxlCpyPutPixel(center.X - x, center.Y + y, color, thickness, true);
                wbmp.pxlCpyPutPixel(center.X + x, center.Y - y, color, thickness, true);
                wbmp.pxlCpyPutPixel(center.X - x, center.Y - y, color, thickness, true);
                wbmp.pxlCpyPutPixel(center.X + y, center.Y + x, color, thickness, false);
                wbmp.pxlCpyPutPixel(center.X - y, center.Y + x, color, thickness, false);
                wbmp.pxlCpyPutPixel(center.X + y, center.Y - x, color, thickness, false);
                wbmp.pxlCpyPutPixel(center.X - y, center.Y - x, color, thickness, false);
            }

            return wbmp;
        }


        public override bool isShapeType(SupportedShapes givenShape)
        {
            if (givenShape == SupportedShapes.Circle)
                return true;
            return false;
        }
    }
}
