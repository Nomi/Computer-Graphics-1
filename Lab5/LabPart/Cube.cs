using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Numerics;

namespace Computer_Graphics_1.Lab5.LabPart
{
    public class Cube
    {
        public List<Point3D_AffC> vertices;
        public List<Point> vertices2D;
        public PictureBox pictureBox;
        public Color color = Color.Red;

        //Useful only for some of the methods.
        private Bitmap canvas;
        private Graphics graphics;
        private Pen pen = new Pen(Color.Red, 4);


        public Cube(PictureBox _pictureBox)
        {
            pictureBox = _pictureBox;
            vertices = new List<Point3D_AffC>();
        }
        private void StartDrawing()
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(canvas);
        }
        private void DrawLine(int x1,int y1, int x2, int y2)
        {
            //graphics.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));

            //x1 += pictureBox.Width / 2;
            //x2 += pictureBox.Width / 2;
            //y1 += pictureBox.Height / 2;
            //y2 += pictureBox.Height / 2;
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }
        private void StopDrawing()
        {
            graphics.Dispose();
            graphics = null;
            UpdatePictureBox(canvas);
        }
        private void UpdatePictureBox(Bitmap canvas)
        {
            pictureBox.Image = canvas;
            pictureBox.Invalidate();
        }





        public void DrawDiagonalTop2DownLeft2Right()
        {
            //Bitmap canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            //Graphics gr = Graphics.FromImage(canvas);
            //Pen p = new Pen(Color.Red, 4);
            //gr.DrawLine(p, new Point(0, 0), new Point(pictureBox.Width, pictureBox.Height));
            //gr.Dispose();
            //UpdatePictureBox(canvas);

            StartDrawing();
            DrawLine(0, 0, canvas.Width, canvas.Height);
            StopDrawing();
        }

        public void MakeThisACube(int len)
        {
            int a = len / 2;
            vertices.Clear();
            vertices.Add(new Point3D_AffC(-a, -a, -a));
            vertices.Add(new Point3D_AffC(+a, -a, -a));
            vertices.Add(new Point3D_AffC(+a, -a, +a));
            vertices.Add(new Point3D_AffC(-a, -a, +a));
            vertices.Add(new Point3D_AffC(-a, +a, -a));
            vertices.Add(new Point3D_AffC(+a, +a, -a));
            vertices.Add(new Point3D_AffC(+a, +a, +a));
            vertices.Add(new Point3D_AffC(-a, +a, +a));

            //for (int i = 0; i < vertices.Count; i++)
            //{
            //    double x = vertices[i].X;
            //    double y = vertices[i].Y;
            //    vertices[i].X = (canvas.Width * (1 + x)) / (double)2;
            //    vertices[i].Y = (canvas.Height * (1 - y)) / (double)2;
            //}
        }
        public void DrawCube(int angleX=45, int angleY=30)
        {
            StartDrawing();
            MakeThisACube(30);
            Transform(angleX, angleY);

            for (int i = 0; i < 3; i++)
            {
                DrawLine((int)vertices2D[i].X, (int)vertices2D[i].Y, (int)vertices2D[i + 1].X, (int)vertices2D[i + 1].Y);
                DrawLine((int)vertices2D[i + 4].X, (int)vertices2D[i + 4].Y, (int)vertices2D[i + 5].X, (int)vertices2D[i + 5].Y);
                DrawLine((int)vertices2D[i].X, (int)vertices2D[i].Y, (int)vertices2D[i + 4].X, (int)vertices2D[i + 4].Y);
            }
            DrawLine((int)vertices2D[0].X, (int)vertices2D[0].Y, (int)vertices2D[3].X, (int)vertices2D[3].Y);
            DrawLine((int)vertices2D[4].X, (int)vertices2D[4].Y, (int)vertices2D[7].X, (int)vertices2D[7].Y);
            DrawLine((int)vertices2D[3].X, (int)vertices2D[3].Y, (int)vertices2D[7].X, (int)vertices2D[7].Y);

            StopDrawing();
        }

        //public void DrawLine(int x1, int y1, int x2, int y2)
        //{
        //    Line myLine = new Line();
        //    myLine.Stroke = System.Windows.Media.Brushes.Black;
        //    myLine.X1 = x1;
        //    myLine.X2 = x2;
        //    myLine.Y1 = y1;
        //    myLine.Y2 = y2;
        //    myLine.HorizontalAlignment = HorizontalAlignment.Left;
        //    myLine.VerticalAlignment = VerticalAlignment.Top;
        //    myLine.StrokeThickness = 1;
        //    wbmpToDrawOn.Children.Add(myLine);
        //}

        //public void ClearVertices()
        //{
        //    vertices.Clear();
        //}


        public void Transform(double angleX=0,double angleY=0)
        {
            int FOVdegrees = 60;

            //Item1 - projection result, Item2 - normal vector, Item3 - global coordinates
            double angleRotX = Math.PI * angleX / 180.0;
            double angleRotY = Math.PI * angleY / 180.0;

            double fovRadians = Math.PI * FOVdegrees / 180.0;


            float d = (canvas.Width/2)/(float)Math.Tan(fovRadians/2);
            float Cx = (canvas.Width / 2);
            float Cy = (canvas.Height / 2);


            Matrix4x4 P = new Matrix4x4(d, 0, Cx, 0,
                                        0, -d, Cy, 0,
                                        0, 0, 0, 0,
                                        0, 0, 1, 0);

            Matrix4x4 Rx = new Matrix4x4(1, 0, 0, 0,
                                         0, (float)Math.Cos(angleRotX), -(float)Math.Sin(angleRotX), 0,
                                         0, (float)Math.Sin(angleRotX), (float)Math.Cos(angleRotX), 0,
                                         0, 0, 0, 1);

            Matrix4x4 Ry = new Matrix4x4((float)Math.Cos(angleRotY), 0, (float)Math.Sin(angleRotY), 0,
                                         0, 1, 0, 0,
                                         -(float)Math.Sin(angleRotY), 0, (float)Math.Cos(angleRotY), 0,
                                         0, 0, 0, 1);

            int t = 3 * 2 * Math.Abs((int)vertices[0].X); //temp, supposed to be 3a i.e. 3*len

            Matrix4x4 Tz = new Matrix4x4(1, 0, 0, 0,
                                         0, 1, 0, 0,
                                         0, 0, 1, t,
                                         0, 0, 0, 0);

            Matrix4x4 matrix = Matrix4x4.Multiply(P,Matrix4x4.Multiply(Tz, Matrix4x4.Multiply(Rx,Ry)));

            //List<Point3D_AffC> result = new List<Point3D_AffC>();
            vertices2D = new List<Point>();
            foreach (var v in vertices)
            {

                //double x = matrix.M11 * v.Item1.X + matrix.M12 * v.Item1.Y + matrix.M13 * v.Item1.Z + matrix.M14 * v.Item1.W;
                //double y = matrix.M21 * v.Item1.X + matrix.M22 * v.Item1.Y + matrix.M23 * v.Item1.Z + matrix.M24 * v.Item1.W;
                //double z = matrix.M31 * v.Item1.X + matrix.M32 * v.Item1.Y + matrix.M33 * v.Item1.Z + matrix.M34 * v.Item1.W;
                //double w = matrix.M41 * v.Item1.X + matrix.M42 * v.Item1.Y + matrix.M43 * v.Item1.Z + matrix.M44 * v.Item1.W;

                double x = matrix.M11 * v.X + matrix.M12 * v.Y + matrix.M13 * v.Z + matrix.M14 * v.W;
                double y = matrix.M21 * v.X + matrix.M22 * v.Y + matrix.M23 * v.Z + matrix.M24 * v.W;
                double z = matrix.M31 * v.X + matrix.M32 * v.Y + matrix.M33 * v.Z + matrix.M34 * v.W;
                double w = matrix.M41 * v.X + matrix.M42 * v.Y + matrix.M43 * v.Z + matrix.M44 * v.W;
                x /= w;
                y /= w;
                z /= w;
                w = 1;
                vertices2D.Add(new Point((int)x, (int)y));
                //result.Add(new Point3D_AffC(x/y));
            }
            //vertices.Clear();
            //vertices.AddRange(result);
        }


        //public void rotateAroundY(int angleAlpha)
        //{
        //    Matrix4x4 rotationMatrix= new Matrix4x4(1, 0, 0, 0,
        //                                 0, (float)Math.Cos(angleAlpha), -(float)Math.Sin(angleAlpha), 0,
        //                                 0, (float)Math.Sin(angleAlpha), (float)Math.Cos(angleAlpha), 0,
        //                                 0, 0, 0, 1);
        //    for (int i = 0; i < vertices.Count; i++)
        //    {
        //        //Basic assignments/setup:
        //        Point3D_AffC curr3dPoint = this.vertices[i];
        //        Vector4 currVrtx = new Vector4(curr3dPoint.X, curr3dPoint.Y, curr3dPoint.Z, curr3dPoint.W);

        //        //Transformation:
        //        double x = rotationMatrix.M11 * currVrtx.X + rotationMatrix.M12 * currVrtx.Y + rotationMatrix.M13 * currVrtx.Z + rotationMatrix.M14 * currVrtx.W;
        //        double y = rotationMatrix.M21 * currVrtx.X + rotationMatrix.M22 * currVrtx.Y + rotationMatrix.M23 * currVrtx.Z + rotationMatrix.M24 * currVrtx.W;
        //        double z = rotationMatrix.M31 * currVrtx.X + rotationMatrix.M32 * currVrtx.Y + rotationMatrix.M33 * currVrtx.Z + rotationMatrix.M34 * currVrtx.W;
        //        double w = rotationMatrix.M41 * currVrtx.X + rotationMatrix.M42 * currVrtx.Y + rotationMatrix.M43 * currVrtx.Z + rotationMatrix.M44 * currVrtx.W;

        //        this.vertices[i].X
        //    }
        //}
    }
}
