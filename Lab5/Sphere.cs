using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Computer_Graphics_1.Lab3;
using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.Lab5.Helpers;
using System.Windows.Forms;

namespace Computer_Graphics_1.Lab5
{
    public class Sphere : Shape3D
    {
        private readonly int n;
        private readonly int m;
        private readonly int radius;

        private TriMeshFragment[] mesh;
        private Bitmap texture;
        private Vertex3D[] vertices;

        public Sphere(int m, int n, int r, PictureBox targetPictureBox) : base(targetPictureBox)
        {
            this.n = n;
            this.m = m;
            this.radius = r;
            generateVertices();
            generateMesh();
        }

        public void Draw(int angleX = 45, int angleY = 30, int zTranslateMultiplier = 1)
        {
            StartDrawing();
            ////This is for drawing the current position with background color if it was visible previously.
            //foreach(TriMeshFragment triangle in mesh)
            //{
            //    if (isFacingBack(triangle)) 
            //        continue;
            //    triangle.remove();
            //}

            //Replaced the above by just removing the old imgage.
            int translateZ = 3 * radius * (zTranslateMultiplier);


            int FOVdegrees = 60;

            Matrix4x4 matP = AffineTransformHelper.getProjectionMatrix(FOVdegrees, canvas.Width, canvas.Height);


            Matrix4x4 matRx = AffineTransformHelper.getRotationXMatrix(angleX);

            Matrix4x4 matRy = AffineTransformHelper.getRotationYMatrix(angleY);

            Matrix4x4 matT = AffineTransformHelper.getTranslationMatrix(0, 0, translateZ);


            Matrix4x4 matrix = Matrix4x4.Multiply(matP, Matrix4x4.Multiply(matT, Matrix4x4.Multiply(matRx, matRy)));
            foreach (Vertex3D v in vertices)
            {
                float x = matrix.M11 * v.position.X + matrix.M12 * v.position.Y + matrix.M13 * v.position.Z + matrix.M14 * v.position.W;
                float y = matrix.M21 * v.position.X + matrix.M22 * v.position.Y + matrix.M23 * v.position.Z + matrix.M24 * v.position.W;
                float z = matrix.M31 * v.position.X + matrix.M32 * v.position.Y + matrix.M33 * v.position.Z + matrix.M34 * v.position.W;
                float w = matrix.M41 * v.position.X + matrix.M42 * v.position.Y + matrix.M43 * v.position.Z + matrix.M44 * v.position.W;
                x /= w;
                y /= w;
                z /= w;
                w = 1;
                v.projectedPosition = new Vector4(x, y, z, w);

                //v.projectedPosition=(AffineTransformHelper.getProjectionMatrix(1280, 800)
                //        .multiply(translate)
                //        .multiply(AlgebraUtils.getRotationXMatrix(angleX))
                //        .multiply(AlgebraUtils.getRotationYMatrix(angleY))
                //        .multiply(v.getPosition())
                //        .scalarProduct(1.0 / (v.getPositionValues()[2] + translate.getColumns()[3].getValues()[2])));

                //v.setPosition(AlgebraUtils.getRotationXMatrix(angleX)
                //        .multiply(AlgebraUtils.getRotationYMatrix(angleY))
                //        .multiply(v.getPosition()));
            }

            foreach(TriMeshFragment triangle in mesh)
            {
                if (isFacingBack(triangle)) 
                    continue;
                DrawTriangle(triangle);
                if(triangle.texture!=null)
                {
                    triangle.DrawFill(ref graphics);
                }
            }
            StopDrawing();
            UpdatePictureBox(canvas);
        }


        private void generateVertices()
        {
            bool fillProjectedPositionFieldWithActualPositionPreliminarily = true; //=false;
            vertices = new Vertex3D[m * n + 2];

            vertices[0] = new Vertex3D(new Vector4(0, radius, 0, 1));
            vertices[m * n + 1] = new Vertex3D(new Vector4(0, -radius, 0, 1));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    vertices[i * m + j + 1] =
                        new Vertex3D(
                            new Vector4(
                                        (float)(radius * Math.Cos(2 * Math.PI * j / m) * Math.Sin(Math.PI / (n + 1) * (i + 1))),
                                        (float)(radius * Math.Cos(Math.PI / (n + 1) * (i + 1))),
                                        (float)(radius * Math.Sin(2 * Math.PI * j / m) * Math.Sin(Math.PI / (n + 1) * (i + 1))),
                                        1
                                        )
                        );
                    if (fillProjectedPositionFieldWithActualPositionPreliminarily)
                    {
                        float[] pos = new float[4];
                        vertices[i * m + j + 1].position.CopyTo(pos);
                        vertices[i * m + j + 1].projectedPosition = new Vector4(pos[0], pos[1], pos[2], pos[3]);
                    }
                }
            }
        }

        public void generateMesh()
        {
            mesh = new TriMeshFragment[2 * m * n];

            for (int i = 0; i < m - 1; i++)
            {
                mesh[i] = new TriMeshFragment(vertices[0], vertices[i + 2], vertices[i + 1]);
                mesh[(2 * n - 1) * m + i] = new TriMeshFragment(vertices[m * n + 1], vertices[(n - 1) * m + i + 1], vertices[(n - 1) * m + i + 2]);
            }
            mesh[m - 1] = new TriMeshFragment(vertices[0], vertices[1], vertices[m]);
            mesh[(2 * n - 1) * m + m - 1] = new TriMeshFragment(vertices[m * n + 1], vertices[m * n], vertices[(n - 1) * m + 1]);

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    mesh[(2 * i + 1) * m + j - 1] = new TriMeshFragment(vertices[i * m + j], vertices[i * m + j + 1], vertices[(i + 1) * m + j + 1]);
                    mesh[(2 * i + 2) * m + j - 1] = new TriMeshFragment(vertices[i * m + j], vertices[(i + 1) * m + j + 1], vertices[(i + 1) * m + j]);
                }
                mesh[(2 * i + 1) * m + m - 1] = new TriMeshFragment(vertices[(i + 1) * m], vertices[i * m + 1], vertices[(i + 1) * m + 1]);
                mesh[(2 * i + 2) * m + m - 1] = new TriMeshFragment(vertices[(i + 1) * m], vertices[(i + 1) * m + 1], vertices[(i + 2) * m]);
            }
        }

        private void generateTextureCoords()
        { 
            int textureWidth = (int)texture.Width;
            int textureHeight = (int)texture.Height;

            vertices[0].textureCoords=(new Point(textureWidth - 1, (int)(0.5 * (textureHeight - 1))));
            vertices[m * n + 1].textureCoords=(new Point(0, (int)(0.5 * textureHeight - 1)));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    vertices[i * m + j + 1].textureCoords=(
                        new Point(
                            (int)(((double)j /(m - 1)) * (textureWidth - 1)),
                            (int)(((double)(i + 1) / (n + 1)) * (textureHeight - 1))
                            )
                        );
                }
            }

            foreach(TriMeshFragment t in mesh)
            {
                t.texture=texture;
            }
        }

        private bool isFacingBack(TriMeshFragment trms) //for back-face culling.
        {
            double x2 = trms.v2.projectedPosition.X;
            double x1 = trms.v1.projectedPosition.X;
            double y2 = trms.v2.projectedPosition.Y;
            double y1 = trms.v1.projectedPosition.Y;
            double x3 = trms.v3.projectedPosition.X;
            double y3 = trms.v3.projectedPosition.Y;

            Vector3 vec1 = new Vector3((float)(x2 - x1), (float)(y2 - y1), 0);
            Vector3 vec2 = new Vector3((float)(x3 - x1), (float)(y3 - y1), 0);

            Vector3 check = Vector3.Cross(vec1, vec2);

            return check.Z <= 0;
        }

        public void setTexture(Bitmap texture) // ==null to remove.
        {
            this.texture = texture;
            generateTextureCoords();
        }
    }
}
