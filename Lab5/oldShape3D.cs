using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Numerics;
using Computer_Graphics_1.Lab5.Helpers;

namespace Computer_Graphics_1.Lab5
{
    public class oldShape3D
    {
        public List<Point3D_AffC> vertices;// { get; }
        public List<Point> vertices2D;// {get;}

        public PictureBox pictureBox;
        public Color color = Color.Red;

        //Useful only for some of the methods.
        protected Bitmap canvas;
        protected Graphics graphics;
        protected Pen pen = new Pen(Color.Red, 1);


        public oldShape3D(PictureBox targetPictureBox)
        {
            pictureBox = targetPictureBox;
            vertices = new List<Point3D_AffC>();
        }

        /// <summary>
        /// Removes the vertices from the vertices array and may perform other related cleanup in the future if I decide so.
        /// </summary>
        public void ClearVertices()
        {
            vertices.Clear();
        }




        protected void StartDrawing()
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(canvas);
        }
        protected void DrawLine(int x1, int y1, int x2, int y2)
        {
            //graphics.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));

            //x1 += pictureBox.Width / 2;
            //x2 += pictureBox.Width / 2;
            //y1 += pictureBox.Height / 2;
            //y2 += pictureBox.Height / 2;
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        protected void DrawPolyLine(List<int> listX, List<int> listY)
        {
            if (listX.Count != listY.Count)
                throw new ArgumentException("Count of input list for X and Y each should be equal (Note: they should also be in the order of their x,y pairs but that is to be made sure of by the user of the function).");
            for(int i=1; i<listX.Count;i++)
            {
                int x1 = listX[i - 1];
                int y1 = listY[i - 1];

                int x2 = listX[i];
                int y2 = listY[i];

                graphics.DrawLine(pen, x1, y1, x2, y2);
            }
        }
        protected void StopDrawing()
        {
            graphics.Dispose();
            graphics = null;
            UpdatePictureBox(canvas);
        }
        protected void UpdatePictureBox(Bitmap canvas)
        {
            pictureBox.Image = canvas;
            pictureBox.Invalidate();
        }
    }
}
