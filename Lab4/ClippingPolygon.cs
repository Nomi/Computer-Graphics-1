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
    public class ClippingPolygon : Shape
    {
        public override WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness = 1)
        {
            if (showPoints)
                wbmp = drawPoints(wbmp);
            if (vertices.Count >= 3)
            {
                vertices.Add(vertices.First());
                PolyLine polygonConvertedToLine = new PolyLine();
                polygonConvertedToLine.vertices = vertices;
                polygonConvertedToLine.color = color;
                polygonConvertedToLine.thickness = 1;
                wbmp = polygonConvertedToLine.draw(wbmp, showPoints, 1);
                pixelsDrawnByTwoVertices = polygonConvertedToLine.pixelsDrawnByTwoVertices;
                vertices.RemoveAt(vertices.Count - 1);
            }
            return wbmp;
        }
        public override bool isShapeType(SupportedShapes givenShape)
        {
            if (givenShape == SupportedShapes.ClippingPolygon)
                return true;
            return false;
        }

        public WriteableBitmap clip(WriteableBitmap wbmp, ref Polygon polygonToClip)
        {
            //int newIndex = -1;
            //int newX = -1;
            //int oldY = -1;
            //foreach(var vertice in polygonToClip.vertices)
            //{
            //    foreach(var pt in pixelsDrawnByTwoVertices[0])
            //    {
            //        if(vertice.X<pt.X)
            //        {
            //            newIndex = polygonToClip.vertices.IndexOf(vertice);
            //            newX = pt.X;
            //            oldY = vertice.Y;
            //            break;
            //        }
            //    }
            //    if (newIndex != -1)
            //        break;
            //}
            //polygonToClip.vertices[newIndex] = new Point(newX, oldY);
            //return wbmp; //no changes to it anyway. Wasn't even used!


            //////NEWER::

            //polygonToClip.vertices = polygonToClip.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            //List<Point> extraVertices = this.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            //for (int i = 0; i < extraVertices.Count - 1; i++)
            //{
            //    if (extraVertices[i] == extraVertices[i + 1])
            //        continue;
            //    polygonToClip.vertices = SutherlandHodgemanClippingAlgorithm(ref polygonToClip, extraVertices[i], extraVertices[i + 1]);
            //}
            //polygonToClip.vertices = (SutherlandHodgemanClippingAlgorithm(ref polygonToClip, extraVertices.Last(), extraVertices.First()));
            ////polygonToClip.vertices = outVertices;
            //return wbmp; //no changes to it anyway. Wasn't even used!


            ///NEWEST:
            //polygonToClip.vertices = polygonToClip.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            float sum = 0;
            int n = polygonToClip.vertices.Count();
            for (int i=1;i<n+1;i++)
            {
                int deltaX = polygonToClip.vertices[i%n].X - polygonToClip.vertices[(i - 1)%n].X;
                int deltay = (polygonToClip.vertices[i%n].Y-wbmp.PixelHeight) - (polygonToClip.vertices[(i - 1) % n].Y-wbmp.PixelHeight);
                sum += deltaX / (float)deltay;
            }
            if (sum < 0)
                //throw new Exception("Given Polygon is not drawn clockwise!");
                polygonToClip.vertices.Reverse();
            this.newClipping(ref polygonToClip);
            foreach (Point pt in polygonToClip.vertices)
            {
                if (pt.X < 0 || pt.Y < 0)
                    throw new Exception("WHAAAT???");
            }
            //polygonToClip.vertices.Add(polygonToClip.vertices.First());
            //delete in Form1.cs if vertices == 0?
            return wbmp;
        }

        private static List<Point> SutherlandHodgemanClippingAlgorithm(ref Polygon polygonToClip, Point clipEdgeStart, Point clipEdgeEnd)
        {
            List<Point> outPoly = new List<Point>();
            List<Point> inPoly = polygonToClip.vertices;
            if (inPoly.Count() > 0)
            {
                Point i, p = inPoly[0], s = inPoly.Last(); //start with the last point
                for (int j = 0; j < inPoly.Count(); ++j)
                {
                    if (isInside(p, clipEdgeStart, clipEdgeEnd))
                    {
                        if (isInside(s, clipEdgeStart, clipEdgeEnd))
                            outPoly.Add(p); //Case1
                        else
                        {
                            i = intersection(s, p, clipEdgeStart, clipEdgeEnd);
                            outPoly.Add(i); //Case 4
                            outPoly.Add(p);
                        }
                    }
                    else if (isInside(s, clipEdgeStart, clipEdgeEnd))
                    {
                        i = intersection(s, p, clipEdgeStart, clipEdgeEnd);
                        outPoly.Add(i); //Case 2
                    }
                    //case 3 both of them outside, so nothing added.
                    s = p;
                    p = inPoly[j];
                }
            }
            return outPoly;
        }

        //private static int x_intersect(Point startL1, Point endL1,Point startL2,Point endL2,int x1L1, int y1L1, int x2L1, int y2L1,
        //int x1L2, int y1L2, int x2L2, int y2L2)
        //{
        //    int num=(startL1.X*endL1.Y -
        //    int num = (x1L1 * y2L1 - y1L1 * x2L1) * (x1L2 - x2L2) -
        //              (x1L1 - x2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
        //    int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
        //    return num / den;
        //}

        private static bool isInside(Point vertex, Point clipStart, Point clipEnd)
        {
            int vertexPosRelativeToClipLine = ((clipEnd.X - clipStart.X) * (vertex.Y - clipStart.Y))
                - ((clipEnd.Y - clipStart.Y) * (vertex.X - clipStart.X));
            return vertexPosRelativeToClipLine >0; //<= for Convex?
            //Point v2 = clipEnd;
            //Point v1 = clipStart;
            //Point point = vertex;
            //return ((v2.X - v1.X) * (point.Y - v1.Y) - (v2.Y - v1.Y) * (point.X - v1.X)) > 0;
        }
        private static Point intersection(Point start, Point end, Point clipStart, Point clipEnd)
        {
            int x = x_intersect(start.X, start.Y, end.X, end.Y, clipStart.X, clipStart.Y, clipEnd.X, clipEnd.Y);
            int y = y_intersect(start.X, start.Y, end.X, end.Y, clipStart.X, clipStart.Y, clipEnd.X, clipEnd.Y);
            return new Point(x, y);
            //the following causes some points to be returned as negative
            //prev= c1 curr=c2 v1=s v2 = e (p here)
            //float a1 = (clipStart.Y - clipEnd.Y) / (clipStart.X - clipEnd.X);
            //float b1 = clipStart.Y - (a1 * clipStart.X);
            //float a2 = (start.Y - end.Y) / (start.X - end.X);
            //float b2 = start.Y - (a2 * start.X);
            //float x = (b2 - b1) / (a1 - a2);
            //float y = a1 * x + b1;
            //return new Point((int)x, (int)y);
        }
        private static int x_intersect(int x1L1, int y1L1, int x2L1, int y2L1,
                int x1L2, int y1L2, int x2L2, int y2L2)
        {
            int num = (x1L1 * y2L1 - y1L1 * x2L1) * (x1L2 - x2L2) -
                      (x1L1 - x2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
            int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
            return num / den;
        }
        private static int y_intersect(int x1L1, int y1L1, int x2L1, int y2L1,
                        int x1L2, int y1L2, int x2L2, int y2L2)
        {
            int num = (x1L1 * y2L1 - y1L1 * x2L1) * (y1L2 - y2L2) -
                      (y1L1 - y2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
            int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
            return num / den;
        }



        private void newClipping(ref Polygon polygonToClip)
        {
            List<Point> outputList = polygonToClip.vertices;
            Point clipStart = this.vertices[this.vertices.Count - 1];
            for (int j = 0; j < this.vertices.Count(); j++)
            {
                Point clipEnd = this.vertices[j];
                //if (clipEnd == clipStart)
                //    continue;
                List<Point> inputList = outputList;
                outputList = new List<Point>();
                if (inputList.Count <= 0)
                    break;
                Point s = inputList[inputList.Count() - 1]; //last on the input list
                for (int i = 0; i < inputList.Count(); i++)
                {
                    Point e = inputList[i];
                    //if (e == s)
                    //    continue;
                    if (isInside(e, clipStart, clipEnd))
                    {
                        if (!isInside(s, clipStart, clipEnd))
                        {
                            outputList.Add(intersection(s, e, clipStart, clipEnd));
                        }
                        outputList.Add(e);
                    }
                    else if (isInside(s, clipStart, clipEnd))
                    {
                        outputList.Add(intersection(s, e, clipStart, clipEnd));
                    }
                    else
                    {
                        //if none of them are inside, we don't add anything.
                    }
                    s = e;
                }
                clipStart = clipEnd;
            }
            polygonToClip.vertices = outputList;
        }

        //private static Point getLineFromPoint(Point p1, Point p2)
        //{

        //}
    }
}




//THE WHOLE FILE BEFORE I HAD TO UNDO A LOT:
#region THE WHOLE FILE BEFORE I HAD TO UNDO A LOT:
/*
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
    public class ClippingPolygon : Shape
    {
        public override WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness = 1)
        {
            if (showPoints)
                wbmp = drawPoints(wbmp);
            if (vertices.Count >= 3)
            {
                vertices.Add(vertices.First());
                PolyLine polygonConvertedToLine = new PolyLine();
                polygonConvertedToLine.vertices = vertices;
                polygonConvertedToLine.color = color;
                polygonConvertedToLine.thickness = 1;
                wbmp = polygonConvertedToLine.draw(wbmp, showPoints, 1);
                pixelsDrawnByTwoVertices = polygonConvertedToLine.pixelsDrawnByTwoVertices;
                vertices.RemoveAt(vertices.Count - 1);
            }
            return wbmp;
        }
        public override bool isShapeType(SupportedShapes givenShape)
        {
            if (givenShape == SupportedShapes.ClippingPolygon)
                return true;
            return false;
        }

        public WriteableBitmap clip(WriteableBitmap wbmp, ref Polygon polygonToClip)
        {
            //int newIndex = -1;
            //int newX = -1;
            //int oldY = -1;
            //foreach(var vertice in polygonToClip.vertices)
            //{
            //    foreach(var pt in pixelsDrawnByTwoVertices[0])
            //    {
            //        if(vertice.X<pt.X)
            //        {
            //            newIndex = polygonToClip.vertices.IndexOf(vertice);
            //            newX = pt.X;
            //            oldY = vertice.Y;
            //            break;
            //        }
            //    }
            //    if (newIndex != -1)
            //        break;
            //}
            //polygonToClip.vertices[newIndex] = new Point(newX, oldY);
            //return wbmp; //no changes to it anyway. Wasn't even used!


            //////NEWER::

            //polygonToClip.vertices = polygonToClip.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            //List<Point> extraVertices = this.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            //for (int i = 0; i < extraVertices.Count - 1; i++)
            //{
            //    if (extraVertices[i] == extraVertices[i + 1])
            //        continue;
            //    polygonToClip.vertices = SutherlandHodgemanClippingAlgorithm(ref polygonToClip, extraVertices[i], extraVertices[i + 1]);
            //}
            //polygonToClip.vertices = (SutherlandHodgemanClippingAlgorithm(ref polygonToClip, extraVertices.Last(), extraVertices.First()));
            ////polygonToClip.vertices = outVertices;
            //return wbmp; //no changes to it anyway. Wasn't even used!


            ///NEWEST:
            //polygonToClip.vertices = polygonToClip.vertices.OrderBy(x => Math.Atan2(x.X, x.Y)).ToList();
            float sum = 0;
            int n = polygonToClip.vertices.Count();
            polygonToClip.vertices.Reverse();
            for (int i=1;i<n+1;i++)
            {
                int deltaX = polygonToClip.vertices[i%n].X - polygonToClip.vertices[(i - 1)%n].X;
                int deltay = (polygonToClip.vertices[i%n].Y-wbmp.PixelHeight) - (polygonToClip.vertices[(i - 1) % n].Y-wbmp.PixelHeight);
                sum += deltaX / (float)deltay;
            }
            if (sum < 0)
                //throw new Exception("Given Polygon is not drawn clockwise!");
                polygonToClip.vertices.Reverse();
            this.newClipping(ref polygonToClip,wbmp);
            foreach (Point pt in polygonToClip.vertices)
            {
                if (pt.X < 0 || pt.Y < 0)
                    throw new Exception("WHAAAT???");
            }
            //polygonToClip.vertices.Add(polygonToClip.vertices.First());
            //delete in Form1.cs if vertices == 0?
            return wbmp;
        }

        private static List<Point> SutherlandHodgemanClippingAlgorithm(ref Polygon polygonToClip, Point clipEdgeStart, Point clipEdgeEnd)
        {
            List<Point> outPoly = new List<Point>();
            List<Point> inPoly = polygonToClip.vertices;
            if (inPoly.Count() > 0)
            {
                Point i, p = inPoly[0], s = inPoly.Last(); //start with the last point
                for (int j = 0; j < inPoly.Count(); ++j)
                {
                    if (isInside(p, clipEdgeStart, clipEdgeEnd))
                    {
                        if (isInside(s, clipEdgeStart, clipEdgeEnd))
                            outPoly.Add(p); //Case1
                        else
                        {
                            i = intersection(s, p, clipEdgeStart, clipEdgeEnd);
                            outPoly.Add(i); //Case 4
                            outPoly.Add(p);
                        }
                    }
                    else if (isInside(s, clipEdgeStart, clipEdgeEnd))
                    {
                        i = intersection(s, p, clipEdgeStart, clipEdgeEnd);
                        outPoly.Add(i); //Case 2
                    }
                    //case 3 both of them outside, so nothing added.
                    s = p;
                    p = inPoly[j];
                }
            }
            return outPoly;
        }

        //private static int x_intersect(Point startL1, Point endL1,Point startL2,Point endL2,int x1L1, int y1L1, int x2L1, int y2L1,
        //int x1L2, int y1L2, int x2L2, int y2L2)
        //{
        //    int num=(startL1.X*endL1.Y -
        //    int num = (x1L1 * y2L1 - y1L1 * x2L1) * (x1L2 - x2L2) -
        //              (x1L1 - x2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
        //    int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
        //    return num / den;
        //}

        private static bool isInside(Point vertex, Point clipStart, Point clipEnd)
        {
            int vertexPosRelativeToClipLine = ((clipEnd.X - clipStart.X) * (vertex.Y - clipStart.Y))
                - ((clipEnd.Y - clipStart.Y) * (vertex.X - clipStart.X));
            return vertexPosRelativeToClipLine < 0;     //<= for Convex?
            //Point v2 = clipEnd;
            //Point v1 = clipStart;
            //Point point = vertex;
            //return ((v2.X - v1.X) * (point.Y - v1.Y) - (v2.Y - v1.Y) * (point.X - v1.X)) > 0;
        }
        private static Point intersection(Point start, Point end, Point clipStart, Point clipEnd)
        {
            int x = x_intersect(start.X, start.Y, end.X, end.Y, clipStart.X, clipStart.Y, clipEnd.X, clipEnd.Y);
            int y = y_intersect(start.X, start.Y, end.X, end.Y, clipStart.X, clipStart.Y, clipEnd.X, clipEnd.Y);
            return new Point(x, y);
            //the following causes some points to be returned as negative
            //prev= c1 curr=c2 v1=s v2 = e (p here)
            //float a1 = (clipStart.Y - clipEnd.Y) / (clipStart.X - clipEnd.X);
            //float b1 = clipStart.Y - (a1 * clipStart.X);
            //float a2 = (start.Y - end.Y) / (start.X - end.X);
            //float b2 = start.Y - (a2 * start.X);
            //float x = (b2 - b1) / (a1 - a2);
            //float y = a1 * x + b1;
            //return new Point((int)x, (int)y);
        }
        private static int x_intersect(int x1L1, int y1L1, int x2L1, int y2L1,
                int x1L2, int y1L2, int x2L2, int y2L2)
        {
            int num = (x1L1 * y2L1 - y1L1 * x2L1) * (x1L2 - x2L2) -
                      (x1L1 - x2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
            int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
            return num / den;
        }
        private static int y_intersect(int x1L1, int y1L1, int x2L1, int y2L1,
                        int x1L2, int y1L2, int x2L2, int y2L2)
        {
            int num = (x1L1 * y2L1 - y1L1 * x2L1) * (y1L2 - y2L2) -
                      (y1L1 - y2L1) * (x1L2 * y2L2 - y1L2 * x2L2);
            int den = (x1L1 - x2L1) * (y1L2 - y2L2) - (y1L1 - y2L1) * (x1L2 - x2L2);
            return num / den;
        }



        private void newClipping(ref Polygon polygonToClip,WriteableBitmap wbmp)
        {
            List<Point> outputList = polygonToClip.vertices;
            Point clipStart = this.vertices[this.vertices.Count - 1];
            for (int j = 0; j < this.vertices.Count(); j++)
            {
                Point clipEnd = this.vertices[j];
                ////Trying to extend the line beyond it's current coordinates:
                //Tuple<Point, Point> newClipLine = findNewEndPoints(clipStart, clipEnd, wbmp);
                //clipStart = newClipLine.Item1;
                //clipEnd = newClipLine.Item2;

                List<Point> inputList = outputList;
                outputList = new List<Point>();
                if (inputList.Count <= 0)
                    break;
                Point s = inputList[inputList.Count() - 1]; //last on the input list
                for (int i = 0; i < inputList.Count(); i++)
                {
                    Point e = inputList[i];
                    //if (e == s)
                    //    continue;
                    Point pt;
                    if (isInside(e, clipStart, clipEnd))
                    {
                        if (!isInside(s, clipStart, clipEnd))
                        {
                            outputList.Add(intersection(s, e, clipStart, clipEnd));
                        }
                        outputList.Add(e);
                    }
                    else if (isInside(s, clipStart, clipEnd))
                    {
                        outputList.Add(intersection(s, e, clipStart, clipEnd));
                    }
                    else
                    {
                        //if none of them are inside, we don't add anything.
                    }
                    s = e;
                }
                clipStart = clipEnd;
            }
            polygonToClip.vertices = outputList;
        }

        private static Tuple<double, double> getLineFromPoints(Point p1, Point p2)
        {
            double slope = (p2.Y - p1.Y) / (double)(p2.X - p1.X);
            double intercept = p2.Y - slope * p2.X;
            return new Tuple<double, double>(slope, intercept);
        }
        private static Tuple<Point,Point> findNewEndPoints(Point cp1,Point cp2, WriteableBitmap wbmp)
        {
            Tuple<double, double> slopeInterceptTuple = getLineFromPoints(cp1, cp2);
            int x1 = 0;
            int x2 = wbmp.PixelWidth-1;
            int y1 = ((int)Math.Round(slopeInterceptTuple.Item1 * x1 + slopeInterceptTuple.Item2));
            int y2 = ((int)Math.Round(slopeInterceptTuple.Item1 * x2 + slopeInterceptTuple.Item2));
            Point newCP1 = new Point(x1, y1);
            Point newCP2 = new Point(x2, y2);
            return new Tuple<Point, Point>(newCP1, newCP2);
        }
    }
} 

*/
#endregion