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
    public class Polygon : Shape
    {
        public override void AddVertices(int x, int y) //doesn't draw. Need to draw after this manually.
        {
            if (vertices.Count >= 4)
                vertices.RemoveAt(vertices.Count - 1);
            vertices.Add(new Point(x, y)); //x is column, y is row ( and item1 is x, item2 is y)
            if (vertices.Count >= 3)
                vertices.Add(vertices.First());
        }
        public override WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness = 1) //uses Symmetric Midpoint Line Algorithm
        {
            if (showPoints)
                wbmp = drawPoints(wbmp);
            if (vertices.Count >= 3)
            {
                
                #region OLD_COMMENTED_OUT_CODE_REPLACED_BY_THE_CODE_BELOW_THIS_REGION
                //    if (_thickness < 1)
                //    {
                //        thickness = 1;
                //    }
                //    for (int i = 1; i < vertices.Count; i++)//we start from i=1 because we'll be drawing from previous index to current one
                //    {
                //        List<Point> drawnPixels = new List<Point>();
                //        //For points, x is column, y is row ( and item1 is x, item2 is y)

                //        //the following only handles lines with angles between 0 to 45 degrees (inclusive of 0 and 45).
                //        int x1 = vertices[i - 1].X;
                //        int x2 = vertices[i].X;

                //        int yOffset = wbmp.PixelHeight; int yMultiplier = -1;
                //        int y1 = yOffset + yMultiplier * vertices[i - 1].Y;
                //        int y2 = yOffset + yMultiplier * vertices[i].Y;

                //        bool isVerticalSoXYFlipped = false;
                //        if (Math.Abs(y2 - y1) > Math.Abs(x2 - x1))
                //        {
                //            int tmp;

                //            tmp = x2; x2 = y2; y2 = tmp;

                //            tmp = x1; x1 = y1; y1 = tmp;

                //            isVerticalSoXYFlipped = true;
                //        }

                //        if (x2 < x1)
                //        {
                //            //if(xySwitched)
                //            //{
                //            int tmp;

                //            tmp = x1; x1 = x2; x2 = tmp;

                //            tmp = y1; y1 = y2; y2 = tmp;
                //            //}
                //            //else
                //            //{
                //            //    x2 = points[i - 1].Item1;
                //            //    x1 = points[i].Item1;

                //            //    y2 = yOffset + yMultiplier * points[i - 1].Item2;
                //            //    y1 = yOffset + yMultiplier * points[i].Item2;
                //            //}
                //        }
                //        //if (y2 < y1)
                //        //{
                //        //    y1 = yOffset + yMultiplier * y1;
                //        //    y2 = yOffset + yMultiplier * y2;
                //        //    yOffset = 0; yMultiplier = 1;
                //        //}


                //        int deltaX = x2 - x1;
                //        int deltaY = y2 - y1;
                //        int stepYf = 1;
                //        int stepYb = -1;
                //        if (y2 < y1)
                //        {
                //            deltaY = Math.Abs(deltaY);
                //            stepYf = -1;
                //            stepYb = 1;
                //        }

                //        int d = 2 * deltaY - deltaX;
                //        int dE = 2 * deltaY; //dEast?
                //        int dNE = 2 * (deltaY - deltaX); //dNorthEast?

                //        int xf = x1, yf = y1;
                //        int xb = x2, yb = y2;


                //        if (isVerticalSoXYFlipped)
                //        {
                //            wbmp.pxlCpyPutPixel_TrackPixelsInList(yf, yOffset + yMultiplier * xf, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //            wbmp.pxlCpyPutPixel_TrackPixelsInList(yb, yOffset + yMultiplier * xb, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //        }
                //        else
                //        {
                //            wbmp.pxlCpyPutPixel_TrackPixelsInList(xf, yOffset + yMultiplier * yf, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //            wbmp.pxlCpyPutPixel_TrackPixelsInList(xb, yOffset + yMultiplier * yb, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //        }
                //        while (xf < xb)
                //        {
                //            ++xf; --xb;
                //            if (d < 0)
                //                d += dE;
                //            else
                //            {
                //                d += dNE;
                //                yf += stepYf;
                //                yb += stepYb;
                //            }
                //            if (isVerticalSoXYFlipped)
                //            {
                //                wbmp.pxlCpyPutPixel_TrackPixelsInList(yf, yOffset + yMultiplier * xf, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //                wbmp.pxlCpyPutPixel_TrackPixelsInList(yb, yOffset + yMultiplier * xb, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //            }
                //            else
                //            {
                //                wbmp.pxlCpyPutPixel_TrackPixelsInList(xf, yOffset + yMultiplier * yf, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //                wbmp.pxlCpyPutPixel_TrackPixelsInList(xb, yOffset + yMultiplier * yb, color, thickness, !isVerticalSoXYFlipped, ref drawnPixels);
                //            }
                //        }
                //        pixelsDrawnByTwoVertices[i - 1] = drawnPixels;
                //    }
                #endregion
                PolyLine polygonConvertedToLine = new PolyLine();
                polygonConvertedToLine.vertices = vertices;
                polygonConvertedToLine.color = color;
                polygonConvertedToLine.thickness = thickness;
                //polygonConvertedToLine.vertices.Add(polygonConvertedToLine.vertices.First());
                wbmp=polygonConvertedToLine.draw(wbmp, showPoints, _thickness);
                //polygonConvertedToLine.vertices.RemoveAt(polygonConvertedToLine.vertices.Count() - 1);
                pixelsDrawnByTwoVertices = polygonConvertedToLine.pixelsDrawnByTwoVertices;
            }
            return wbmp;
        }
        public override bool isShapeType(SupportedShapes givenShape)
        {
            if (givenShape == SupportedShapes.Polygon)
                return true;
            return false;
        }
    }
}
