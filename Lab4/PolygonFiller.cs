using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Computer_Graphics_1.Lab3;
using Computer_Graphics_1.HelperClasses.Extensions;

namespace Computer_Graphics_1.Lab4
{
    public static class PolygonFiller
    {
        public static byte Clamp(int value)
        {
            int result = value;
            if (value.CompareTo(255) > 0)
                result = 255;
            if (value.CompareTo(0) < 0)
                result = 0;
            return (byte)result;
        }
        private static Point LowwerY(Point p1, Point p2)
        {
            if (p1.Y <= p2.Y) return p1;
            return p2;
        }
        private static Point UpperY(Point p1, Point p2)
        {
            if (p1.Y >= p2.Y) return p1;
            return p2;
        }

        public static void FillPolygon(ref Polygon poly, Color color)
        {
            List<Point> vertices = poly.vertices;
            poly.fillColor = color;
            int N = vertices.Count();
            List<(int, double, double)> AET = new List<(int, double, double)>();
            var P = vertices;
            var P1 = P.OrderBy(p => p.Y).ToList();
            int[] indices = new int[N];
            for (int j = 0; j < N; j++)
                indices[j] = P.IndexOf(P.Find(x => x == P1[j]));
            int k = 0;
            int i = indices[k];
            int y, ymin, ymax;
            y = ymin = P[indices[0]].Y;
            ymax = P[indices[N - 1]].Y;
            while (y < ymax)
            {
                while (P[i].Y == y)
                {
                    if (i > 0)
                    {
                        if (P[i - 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[i - 1], P[i]);
                            var u = UpperY(P[i - 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[i - 1].X - P[i].X) / (P[i - 1].Y - P[i].Y)));
                        }
                    }
                    else
                    {
                        if (P[N - 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[N - 1], P[i]);
                            var u = UpperY(P[N - 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[N - 1].X - P[i].X) / (P[N - 1].Y - P[i].Y)));
                        }
                    }
                    if (i < N - 1)
                    {
                        if (P[i + 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[i + 1], P[i]);
                            var u = UpperY(P[i + 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[i + 1].X - P[i].X) / (P[i + 1].Y - P[i].Y)));
                        }
                    }
                    else
                    {
                        if (P[0].Y > P[i].Y)
                        {
                            var l = LowwerY(P[0], P[i]);
                            var u = UpperY(P[0], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[0].X - P[i].X) / (P[0].Y - P[i].Y)));
                        }
                    }
                    ++k;
                    i = indices[k];
                }
                //sort AET by x value
                AET = AET.OrderBy(item => item.Item2).ToList();
                //fill pixels between pairs of intersections
                for (int j = 0; j < AET.Count; j += 2)
                {
                    if (j + 1 < AET.Count)
                    {
                        for (int x = (int)AET[j].Item2; x <= (int)AET[j + 1].Item2; x++)
                        {
                            poly.filledPixels.Add(new Point(x, y));
                        }
                    }
                }
                ++y;
                //remove from AET edges for which ymax = y
                AET.RemoveAll(x => x.Item1 == y);

                for (int j = 0; j < AET.Count; j++)
                    AET[j] = (AET[j].Item1, AET[j].Item2 + AET[j].Item3, AET[j].Item3);
            }
        }

        public static unsafe void FillPolygon(ref Polygon poly,ref WriteableBitmap wbmp, Bitmap pattern)
        {
            poly.fillPattern = pattern;
            List<Point> vertices = poly.vertices;
            int N = vertices.Count();
            List<(int, double, double)> AET = new List<(int, double, double)>();
            var P = vertices;
            var P1 = P.OrderBy(p => p.Y).ToList();
            int[] indices = new int[N];
            for (int j = 0; j < N; j++)
                indices[j] = P.IndexOf(P.Find(x => x == P1[j]));
            int k = 0;
            int i = indices[k];
            int y, ymin, ymax, xmin;
            y = ymin = P[indices[0]].Y;
            ymax = P[indices[N - 1]].Y;
            xmin = P.OrderBy(p => p.X).First().X;

            BitmapData bData = pattern.LockBits(new System.Drawing.Rectangle(0, 0, (int)pattern.Width, (int)pattern.Height), ImageLockMode.ReadOnly, pattern.PixelFormat);
            byte* scan0 = (byte*)bData.Scan0.ToPointer();
            byte bitsPerPixel = (byte)(Image.GetPixelFormatSize(bData.PixelFormat));

            while (y < ymax)
            {
                while (P[i].Y == y)
                {
                    if (i > 0)
                    {
                        if (P[i - 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[i - 1], P[i]);
                            var u = UpperY(P[i - 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[i - 1].X - P[i].X) / (P[i - 1].Y - P[i].Y)));
                        }
                    }
                    else
                    {
                        if (P[N - 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[N - 1], P[i]);
                            var u = UpperY(P[N - 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[N - 1].X - P[i].X) / (P[N - 1].Y - P[i].Y)));
                        }
                    }
                    if (i < N - 1)
                    {
                        if (P[i + 1].Y > P[i].Y)
                        {
                            var l = LowwerY(P[i + 1], P[i]);
                            var u = UpperY(P[i + 1], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[i + 1].X - P[i].X) / (P[i + 1].Y - P[i].Y)));
                        }
                    }
                    else
                    {
                        if (P[0].Y > P[i].Y)
                        {
                            var l = LowwerY(P[0], P[i]);
                            var u = UpperY(P[0], P[i]);
                            AET.Add((u.Y, l.X, (double)(P[0].X - P[i].X) / (P[0].Y - P[i].Y)));
                        }
                    }
                    ++k;
                    i = indices[k];
                }
                //sort AET by x value
                AET = AET.OrderBy(item => item.Item2).ToList();
                //fill pixels between pairs of intersections
                unsafe 
                {
                    for (int j = 0; j < AET.Count; j += 2)
                    {
                        if (j + 1 < AET.Count)
                        {
                            for (int x = (int)AET[j].Item2; x <= (int)AET[j + 1].Item2; x++)
                            {
                                Color colCurrentPixel;
                                byte* tmp = scan0 + ((y - ymin) % bData.Height) * bData.Stride + ((x - xmin) % bData.Width) * bitsPerPixel / 8;
                                colCurrentPixel = Color.FromArgb(tmp[2], tmp[1], tmp[0]);
                                wbmp.PutPixel(x, y, colCurrentPixel);
                            }
                        }
                    }
                }
                ++y;
                //remove from AET edges for which ymax = y
                AET.RemoveAll(x => x.Item1 == y);

                for (int j = 0; j < AET.Count; j++)
                    AET[j] = (AET[j].Item1, AET[j].Item2 + AET[j].Item3, AET[j].Item3);
            }
            pattern.UnlockBits(bData);
        }




    }
}