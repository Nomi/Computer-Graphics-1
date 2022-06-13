using Computer_Graphics_1.Lab3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Computer_Graphics_1.Lab5.Helpers.Filling;

namespace Computer_Graphics_1.Lab5.Helpers
{
    public class TriMeshFragment
    {
        public Vertex3D v1 { get; }
        public Vertex3D v2 { get; }
        public Vertex3D v3 { get; }

        public Bitmap texture=null; //if null, don't fill.

        public TriMeshFragment(Vertex3D _a, Vertex3D _b, Vertex3D _c)
        {
            v1 = _a;
            v2 = _b;
            v3 = _c;
        }

        public Vertex3D this[int index]
        {
            get
            {
                if (index == 0)
                    return v1;
                if (index == 1)
                    return v2;
                if (index == 2)
                    return v3;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public void DrawFill(ref Graphics g)//draw fill here! :D
        {
            if (texture == null)
                throw new NullReferenceException();
            fill();
            //PixelsToDraw = Computer_Graphics_1.Lab4.PolygonFiller.FillTriMeshFragment(this, texture);
            foreach(PixelRep pxr in PixelsToDraw)
            {
                Brush brush = new SolidBrush(pxr.color);
                g.FillRectangle(brush, pxr.X, pxr.Y, 1, 1);
            }
            PixelsToDraw.Clear();
        }

        ////////////////////////////////////////////////
        ////////        FILLING PORTION         ////////
        ////////////////////////////////////////////////

        List<PixelRep> PixelsToDraw = new List<PixelRep>();
        private List<Tuple<Vertex3D,Vertex3D>> lines=new List<Tuple<Vertex3D, Vertex3D>>();
        private int minY = int.MaxValue;
        private bool hasPatternFilling {get{ return texture != null; }}

        private void fillLinesArray()
        {
            lines.Clear();

            TriMeshFragment n = this;
            for (int i=0;i<3;i++)
            {
                lines.Add(new Tuple<Vertex3D, Vertex3D>(n[i], n[(i+1)%3]));
            }
            
        }

        private EdgeTable generateEdgeTable()
        {
            fillLinesArray();

            EdgeTable table = new EdgeTable();
            
            foreach(Tuple<Vertex3D,Vertex3D> line in lines)
            {
                float Y1 = line.Item1.projectedPosition.Y;
                float Y2 = line.Item2.projectedPosition.Y;
                if (Y1 == Y2) 
                    continue;
                float X1 = line.Item1.projectedPosition.X;
                float X2 = line.Item2.projectedPosition.X;



                int yMax = ((int)Math.Max(Y1, Y2));
                int yMin = (int)Math.Min(Y1, Y2);
                int xMin = (int)X1; //float xMin = X1;
                float slope = (float)((X2 - X1) / (float)(Y2 - Y1));

                EdgeEntry edge = new EdgeEntry();
                edge.v1 = line.Item1;
                edge.v2 = line.Item2;
                edge.textureX = ((float)line.Item1.textureCoords.X);
                edge.textureY = ((float)line.Item1.textureCoords.Y);
                edge.xMax = X2;

                if (Y1 > Y2)
                {
                    xMin = (int)X2;
                    edge.xMax = X1;
                    edge.textureX = line.Item2.textureCoords.X;
                    edge.textureY = line.Item2.textureCoords.Y;
                    edge.v1 = line.Item2;
                    edge.v2 = line.Item1;
                }
                edge.xMin = xMin;
                edge.yMax = yMax;
                edge.yMin = yMin;
                edge.inverseSlope = slope;
                edge.length= (float)Math.Sqrt(Math.Pow(X2 - X1, 2)
                                + Math.Pow(Y2 - Y1, 2));
                table.add(yMin, edge);

                if (yMin < minY) 
                    minY = yMin;
            }

            return table; //not sorted maybe?
        }


        public void fill()
        {
            List<EdgeEntry> activeEdgeTable = new List<EdgeEntry>();
            var edgeTable = generateEdgeTable();
            int y = minY;

            while (activeEdgeTable.Count!=0 || !edgeTable.isEmpty())
            {
                int parity = 0;

                if (edgeTable.containsKey(y))
                {
                    List<EdgeEntry> entry = edgeTable.pop(y);
                    activeEdgeTable.AddRange(entry);
                }

                activeEdgeTable = activeEdgeTable.OrderBy(e => e.xMin).ToList();//might or might not be wrong order.
                //activeEdgeTable =activeEdgeTable.OrderByDescending(e => e.xMin).ToList(); //might or might not be wrong order.


                for (int i = 0; i < activeEdgeTable.Count - 1; i++)
                {
                    if (parity++ % 2 == 0)
                    {
                        fillTheLine(
                                activeEdgeTable[i],
                                activeEdgeTable[i + 1],
                                y);
                    }
                }
                ++y;

                int finalY = y;
                activeEdgeTable.RemoveAll(e => e.yMax == finalY);
                foreach(EdgeEntry edge in activeEdgeTable)
                {
                    edge.xMin = edge.xMin + edge.inverseSlope;
                    float remainingLength= 
                                (float)Math.Sqrt(
                                    Math.Pow(edge.xMax - edge.xMin, 2)
                                    + Math.Pow(edge.yMax - finalY, 2));

                    float t = (edge.length - remainingLength) / edge.length;
                    float z_1 = (float)edge.v1.projectedPosition.Z;
                    float z_2 = (float)edge.v2.projectedPosition.Z;
                    float z_t = (z_2 - z_1) * t + z_1;
                    float u;
                    if (z_1 == z_2)
                        u = t;
                    else
                        u= ((1 / z_t) - (1 / z_1)) / ((1 / z_2) - (1 / z_1));
                    edge.textureX = (float)(u * (edge.v2.textureCoords.X - edge.v1.textureCoords.X) + edge.v1.textureCoords.X);
                    edge.textureY = (float)(u * (edge.v2.textureCoords.Y - edge.v1.textureCoords.Y) + edge.v1.textureCoords.Y);
                }
            }
        }

        private void fillTheLine(EdgeEntry e1, EdgeEntry e2, int height)
        {
            int start = (int)Math.Floor(e1.xMin);
            int end = (int)Math.Ceiling(e2.xMin);

            int w = texture.Width;
            int h = texture.Height;

            float t, z_t, u;
            var v1 = e1.v1;
            var v2 = e1.v2;
            float z_1 = (float)v1.projectedPosition.Z;
            float z_2 = (float)v2.projectedPosition.Z;

            Point p1_g = new Point((int)e1.textureX, (int)e1.textureY);
            Point p2_g = new Point((int)e2.textureX, (int)e2.textureY);

            for (int i = start; i <= end; i++)
            {
                t = (float)(i - start) / (end - start);
                z_t = (z_2 - z_1) * t + z_1;
                if (z_1 == z_2) u = t;
                else u = ((1 / z_t) - (1 / z_1)) / ((1 / z_2) - (1 / z_1));

                var point = new Point(
                        (int)(u * (p2_g.X - p1_g.X) + p1_g.X),
                        (int)((u * (p2_g.Y - p1_g.Y)) + p1_g.Y)
                        );
                PixelsToDraw.Add(new PixelRep(i, height, texture.GetPixel(point.X % w, point.Y % h)));
            }
        }
    }
}
