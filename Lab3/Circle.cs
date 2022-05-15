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
            List<Point> drawnPixels = new List<Point>();
            //y might need to be reversed like in line, and pixelcopy needs to be used everywhere
            if (showPoints)
                wbmp=drawPoints(wbmp);
            if (vertices.Count() != 2)
                return wbmp;
            if (_thickness < 1)
            {
                thickness = 1;
            }
            Point center = new Point(vertices[0].X, vertices[0].Y);
            int radius = (int) Math.Round(
                Math.Sqrt(
                (float)Math.Pow(center.X-vertices[1].X,2)
                +(float)Math.Pow(center.Y - vertices[1].Y, 2)
                ));

            int dE = 3;
            int dSE = 5 - 2 * radius;
            int d = 1 - radius;
            int x = 0;
            int y = radius;
            wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X, center.Y + y, color, thickness, true, ref drawnPixels);
            wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X, center.Y - y, color, thickness, true, ref drawnPixels);
            wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X + y, center.Y, color, thickness, false, ref drawnPixels);
            wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X - y, center.Y, color, thickness, false, ref drawnPixels);

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

                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X + x, center.Y + y, color, thickness, true, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X - x, center.Y + y, color, thickness, true, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X + x, center.Y - y, color, thickness, true, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X - x, center.Y - y, color, thickness, true, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X + y, center.Y + x, color, thickness, false, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X - y, center.Y + x, color, thickness, false, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X + y, center.Y - x, color, thickness, false, ref drawnPixels);
                wbmp.pxlCpyPutPixel_TrackPixelsInList(center.X - y, center.Y - x, color, thickness, false, ref drawnPixels);

            }
            if(pixelsDrawnByTwoVertices.Count>=1)
            {
                pixelsDrawnByTwoVertices.Clear();
            }
            pixelsDrawnByTwoVertices.Add(drawnPixels); //in a circle, there's only one set of two points.
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
