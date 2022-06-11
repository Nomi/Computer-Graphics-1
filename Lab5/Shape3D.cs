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
    public class Shape3D
    {

        public PictureBox pictureBox;
        public Color color = Color.Red;
        protected Bitmap canvas;
        protected Graphics graphics;
        protected Pen pen = new Pen(Color.Red, 1);

        public Shape3D(PictureBox targetPictureBox)
        {
            pictureBox = targetPictureBox;
        }

        protected void StartDrawing()
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(canvas);
        }
        protected void DrawLine(int x1, int y1, int x2, int y2)
        {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        protected void DrawTriangle(TriMeshFragment t)
        {
            DrawLine((int)t.v1.projectedPosition.X, (int)t.v1.projectedPosition.Y, (int)t.v2.projectedPosition.X, (int)t.v2.projectedPosition.Y);
            DrawLine((int)t.v2.projectedPosition.X, (int)t.v2.projectedPosition.Y, (int)t.v3.projectedPosition.X, (int)t.v3.projectedPosition.Y);
            DrawLine((int)t.v3.projectedPosition.X, (int)t.v3.projectedPosition.Y, (int)t.v1.projectedPosition.X, (int)t.v1.projectedPosition.Y);
        }
        protected void StopDrawing()
        {
            graphics.Dispose();
            graphics = null;
            UpdatePictureBox();
        }
        protected void UpdatePictureBox(Bitmap _canvas=null)
        {
            if (_canvas == null)
                _canvas = canvas;
            pictureBox.Image = _canvas;
            pictureBox.Invalidate();
        }
    }
}
