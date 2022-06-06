using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Numerics;
using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;

namespace Computer_Graphics_1.Lab5
{
    class Sphere : Shape3D
    {
        public int Radius { get; set; }
        public int N { get; set; }
        public int M { get; set; }

        public TriangleCoordinates[] TriMesh = null;

        public WriteableBitmap texture = null;

        public Sphere(int n, int m, int r, PictureBox targetPictureBox): base(targetPictureBox)
        {
            N = n;
            M = m;
            Radius = r;
            vertices = new List<Point3D_AffC>();
        }


        /// <summary>
        /// Sets the vertices list to contain the required vertices for the sphere.
        /// </summary>
        public void PopulateVertices()
        {
            int r = Radius;
            int n = N;
            int m = M;
            double PI = Math.PI;

            this.ClearVertices();
            vertices = new List<Point3D_AffC>((n*m)+2);
            //vertices[0] = new Point3D_AffC(0, r, 0);
            vertices.Add(new Point3D_AffC(0, r, 0));
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    double a = (r * Math.Cos(2 * PI * j / m) * Math.Sin(PI * (i + 1) / (n + 1)));
                    double b = (r * Math.Cos(PI * (i + 1) / (n + 1)));
                    double c = (r * Math.Sin(2 * PI * j / m) * Math.Sin(PI * (i + 1) / (n + 1)));
                    //vertices[i * m + j + 1] = new Point3D_AffC(a, b, c);
                    vertices.Add(new Point3D_AffC(a, b, c));
                }
            }
            //vertices[m * n + 1] = new Point3D_AffC(0, -r, 0);
            vertices.Add(new Point3D_AffC(0, -r, 0));


            if (texture!=null)
            {                
                for(int i=0;i<n;i++)
                {
                    for(int j=0;j<m;j++)
                    {
                        vertices[i * m + j + 1].textureVector = new Vector2(j / (float)(m - 1), (i + 1) /(float)(n + 1));
                    }
                }
                vertices[0].textureVector = new Vector2((float)0.5, 0); //bottom
                vertices[m * n + 1].textureVector = new Vector2((float)0.5,1);//top
            }

            //note to self, builder pattern for transformations?
        }

        public void CreateTriangularMesh()
        {
            int r = Radius;
            int n = N;
            int m = M;
            double PI = Math.PI;

            int arraySize = 2 * n * m;//4*n; //From notes about trimesh given by teacher
            int vertexCount = 4 * n + 2; //From notes about trimesh given by teacher


            TriMesh = new TriangleCoordinates[arraySize];
            for (int i = 0; i <= m - 2; i++)
            {
                TriMesh[i] = new TriangleCoordinates(vertices[0], vertices[i + 2], vertices[i + 1]);
                TriMesh[2 * (n - 1) * m + i + m] = new TriangleCoordinates(vertices[m * n + 1], vertices[(n - 1) * m + i + 1], vertices[(n - 1) * m + i + 2]);
            }
            TriMesh[m - 1] = new TriangleCoordinates(vertices[0], vertices[1], vertices[m]);
            TriMesh[2 * (n - 1) * m + m - 1 + m] = new TriangleCoordinates(vertices[m * n + 1], vertices[m * n], vertices[(n - 1) * m + 1]);
            for (int i = 0; i <= n - 2; i++)
            {
                for (int j = 1; j <= m - 1; j++)
                {
                    TriMesh[(2 * i + 1) * m + j - 1] = new TriangleCoordinates(vertices[i * m + j], vertices[i * m + j + 1], vertices[(i + 1) * m + j + 1]);
                    TriMesh[(2 * i + 2) * m + j - 1] = new TriangleCoordinates(vertices[i * m + j], vertices[(i + 1) * m + j + 1], vertices[(i + 1) * m + j]);
                }
                TriMesh[(2 * i + 1) * m + m - 1] = new TriangleCoordinates(vertices[(i + 1) * m], vertices[i * m + 1], vertices[(i + 1) * m + 1]);
                TriMesh[(2 * i + 2) * m + m - 1] = new TriangleCoordinates(vertices[(i + 1) * m], vertices[(i + 1) * m + 1], vertices[(i + 2) * m]);
            }
        }


        public void Transform(double angleX = 0, double angleY = 0, int zTranslateMultiplier=1)
        {
            int translateZ = 3*Radius *(zTranslateMultiplier);

            double angleRotX = Math.PI * angleX / 180.0;
            double angleRotY = Math.PI * angleY / 180.0;
            int FOVdegrees = 60;

            double fovRadians = Math.PI * FOVdegrees / 180.0;


            float d = (canvas.Width / 2) / (float)Math.Tan(fovRadians / 2);
            float Cx = (canvas.Width / 2);
            float Cy = (canvas.Height / 2);

            Matrix4x4 P = new Matrix4x4(-d, 0, Cx, 0,
                                        0, d, Cy, 0,
                                        0, 0, 0, 1,
                                        0, 0, 1, 0);


            Matrix4x4 Rx = new Matrix4x4(1, 0, 0, 0,
                                         0, (float)Math.Cos(angleRotX), -(float)Math.Sin(angleRotX), 0,
                                         0, (float)Math.Sin(angleRotX), (float)Math.Cos(angleRotX), 0,
                                         0, 0, 0, 1);

            Matrix4x4 Ry = new Matrix4x4((float)Math.Cos(angleRotY), 0, (float)Math.Sin(angleRotY), 0,
                                         0, 1, 0, 0,
                                         -(float)Math.Sin(angleRotY), 0, (float)Math.Cos(angleRotY), 0,
                                         0, 0, 0, 1);

            Matrix4x4 Tz = new Matrix4x4(1, 0, 0, 0,
                                         0, 1, 0, 0,
                                         0, 0, 1, translateZ,
                                         0, 0, 0, 1);

            Matrix4x4 matrix = Matrix4x4.Multiply(P, Matrix4x4.Multiply(Tz, Matrix4x4.Multiply(Rx, Ry)));

            //List<Point3D_AffC> result = new List<Point3D_AffC>();
            vertices2D = new List<Point>();
            for(int i=0;i<vertices.Count;i++)
            {
                var v = vertices[i];

                double x = matrix.M11 * v.X + matrix.M12 * v.Y + matrix.M13 * v.Z + matrix.M14 * v.W;
                double y = matrix.M21 * v.X + matrix.M22 * v.Y + matrix.M23 * v.Z + matrix.M24 * v.W;
                double z = matrix.M31 * v.X + matrix.M32 * v.Y + matrix.M33 * v.Z + matrix.M34 * v.W;
                double w = matrix.M41 * v.X + matrix.M42 * v.Y + matrix.M43 * v.Z + matrix.M44 * v.W;

                x /= w;
                y /= w;
                z /= w;
                w = 1;

                Point3D_AffC temp = new Point3D_AffC(x,y,z,w);
                temp.textureVector = vertices[i].textureVector;
                vertices[i] = temp;
                //vertices2D.Add(new Point((int)x, (int)y));
            }

            //vertices.Clear();
            //vertices.AddRange(result);
        }


        public void Draw(int angleX = 45, int angleY = 30, int zTranslateMultiplier = 1)
        {
            bool enableFill = true;
            StartDrawing();



            PopulateVertices();

            Transform(angleX,angleY, zTranslateMultiplier);

            CreateTriangularMesh();

            DrawMesh();



            StopDrawing();
            UpdatePictureBox(canvas);
        }

        public unsafe void DrawMesh()
        {
            //if (enableFill)
            //{
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\Screenshot 2022-06-06 183336.jpg"));
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\chessPattern_ImperfectCropjpg.jpg"));
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\AiMr7BuljIvpPR04Vd9bB3DdspBhySnQ9hUkPE6q.bmp"));
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\redandwhite.png"));
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\sample_1280×853.bmp"));
            //    ////texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(new Bitmap("E:\\[Library]\\Gallery\\Pictures\\10644.jpg"));
            //    //texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(Properties.Resources._convFilterTest);
            //    //texture = HelperClasses.ImgUtil.GetWritableBitmapFromBitmap(Properties.Resources.chessPatternImperfectCrop);
            //}

            for (int i = 0; i < TriMesh.Length; i++)
            {
                TriangleCoordinates t = TriMesh[i];
                double x2 = t.v2.X;
                double x1 = t.v1.X;
                double y2 = t.v2.Y;
                double y1 = t.v1.Y;
                double x3 = t.v3.X;
                double y3 = t.v3.Y;

                Vector3 vec1 = new Vector3((float)(x2 - x1),(float) (y2 - y1), 0);
                Vector3 vec2 = new Vector3((float)(x3 - x1), (float)(y3 - y1), 0);

                Vector3 check = Vector3.Cross(vec1, vec2);
                if (check.Z > 0) //Back-face culling
                {

                    if (texture == null)
                    {
                        DrawTriangle(t);
                    }
                    else 
                    {
                        ////DrawTriangle(t);//debug

                        int xTex = (int)(t.v1.textureVector.X * (texture.PixelWidth - 1));
                        int yTex = (int)(t.v1.textureVector.Y * (texture.PixelHeight - 1));
                        _pixel_bgr24_bgra32* px = (_pixel_bgr24_bgra32*)texture.GetPixelIntPtrAt(yTex, xTex);


                        Color currFillColor = Color.FromArgb(px->red, px->green, px->blue);
                        //currFillColor=Color.Blue;

                        SolidBrush br = new SolidBrush(currFillColor);

                        Lab3.Polygon polygon = new Lab3.Polygon();
                        polygon.AddVertices((int)x1, (int)y1);
                        polygon.AddVertices((int)x2, (int)y2);
                        polygon.AddVertices((int)x3, (int)y3);
                        polygon.fillColor = currFillColor;
                        Lab4.PolygonFiller.FillPolygon(ref polygon, currFillColor);

                        //SolidBrush br = new SolidBrush(polygon.fillColor);
                        foreach (Point pt in polygon.filledPixels)
                        {
                            graphics.FillRectangle(br, pt.X, pt.Y, 1, 1);
                        }

                        //DrawTriangle(t);//debug
                    }
                }
            }
        }

        private void DrawTriangle(TriangleCoordinates t)
        {
            DrawLine((int)t.v1.X, (int)t.v1.Y, (int)t.v2.X, (int)t.v2.Y);
            DrawLine((int)t.v2.X, (int)t.v2.Y, (int)t.v3.X, (int)t.v3.Y);
            DrawLine((int)t.v3.X, (int)t.v3.Y, (int)t.v1.X, (int)t.v1.Y);
        }
    }
}
