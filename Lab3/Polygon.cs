﻿using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace Computer_Graphics_1.Lab3
{
    public class Polygon : Shape
    {
        public override WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness = 1) //uses Symmetric Midpoint Line Algorithm
        {
            if (showPoints)
                wbmp = drawPoints(wbmp);
            if (points.Count >= 3)
            {
                points.Add(points.First());
                if (_thickness < 1)
                {
                    thickness = 1;
                }
                for (int i = 1; i < points.Count; i++)//we start from i=1 because we'll be drawing from previous index to current one
                {
                    //For points, x is column, y is row ( and item1 is x, item2 is y)

                    //the following only handles lines with angles between 0 to 45 degrees (inclusive of 0 and 45).
                    int x1 = points[i - 1].X;
                    int x2 = points[i].X;

                    int yOffset = wbmp.PixelHeight; int yMultiplier = -1;
                    int y1 = yOffset + yMultiplier * points[i - 1].Y;
                    int y2 = yOffset + yMultiplier * points[i].Y;

                    bool isVerticalSoXYFlipped = false;
                    if (Math.Abs(y2 - y1) > Math.Abs(x2 - x1))
                    {
                        int tmp;

                        tmp = x2; x2 = y2; y2 = tmp;

                        tmp = x1; x1 = y1; y1 = tmp;

                        isVerticalSoXYFlipped = true;
                    }

                    if (x2 < x1)
                    {
                        //if(xySwitched)
                        //{
                        int tmp;

                        tmp = x1; x1 = x2; x2 = tmp;

                        tmp = y1; y1 = y2; y2 = tmp;
                        //}
                        //else
                        //{
                        //    x2 = points[i - 1].Item1;
                        //    x1 = points[i].Item1;

                        //    y2 = yOffset + yMultiplier * points[i - 1].Item2;
                        //    y1 = yOffset + yMultiplier * points[i].Item2;
                        //}
                    }
                    //if (y2 < y1)
                    //{
                    //    y1 = yOffset + yMultiplier * y1;
                    //    y2 = yOffset + yMultiplier * y2;
                    //    yOffset = 0; yMultiplier = 1;
                    //}


                    int deltaX = x2 - x1;
                    int deltaY = y2 - y1;
                    int stepYf = 1;
                    int stepYb = -1;
                    if (y2 < y1)
                    {
                        deltaY = Math.Abs(deltaY);
                        stepYf = -1;
                        stepYb = 1;
                    }

                    int d = 2 * deltaY - deltaX;
                    int dE = 2 * deltaY; //dEast?
                    int dNE = 2 * (deltaY - deltaX); //dNorthEast?

                    int xf = x1, yf = y1;
                    int xb = x2, yb = y2;


                    if (isVerticalSoXYFlipped)
                    {
                        wbmp.pxlCpyPutPixel(yf, yOffset + yMultiplier * xf, color, thickness, !isVerticalSoXYFlipped);
                        wbmp.pxlCpyPutPixel(yb, yOffset + yMultiplier * xb, color, thickness, !isVerticalSoXYFlipped);
                    }
                    else
                    {
                        wbmp.pxlCpyPutPixel(xf, yOffset + yMultiplier * yf, color, thickness, !isVerticalSoXYFlipped);
                        wbmp.pxlCpyPutPixel(xb, yOffset + yMultiplier * yb, color, thickness, !isVerticalSoXYFlipped);
                    }
                    while (xf < xb)
                    {
                        ++xf; --xb;
                        if (d < 0)
                            d += dE;
                        else
                        {
                            d += dNE;
                            yf += stepYf;
                            yb += stepYb;
                        }
                        if (isVerticalSoXYFlipped)
                        {
                            wbmp.pxlCpyPutPixel(yf, yOffset + yMultiplier * xf, color, thickness, !isVerticalSoXYFlipped);
                            wbmp.pxlCpyPutPixel(yb, yOffset + yMultiplier * xb, color, thickness, !isVerticalSoXYFlipped);
                        }
                        else
                        {
                            wbmp.pxlCpyPutPixel(xf, yOffset + yMultiplier * yf, color, thickness, !isVerticalSoXYFlipped);
                            wbmp.pxlCpyPutPixel(xb, yOffset + yMultiplier * yb, color, thickness, !isVerticalSoXYFlipped);
                        }
                    }
                }
                points.RemoveAt(points.Count-1);
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
