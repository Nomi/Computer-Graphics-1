using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.HelperClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Computer_Graphics_1.Lab3
{
    public enum SupportedShapes : int
    {
        Line,
        Polygon,
        Circle,
        ClippingPolygon
    }

    [XmlInclude(typeof(PolyLine))]
    [XmlInclude(typeof(Polygon))]
    [XmlInclude(typeof(Circle))]
    public class Shape //will make it abstract later..
    {
        public List<Point> vertices = new List<Point>();

        [XmlIgnore] //it will be populated by drawing again anyway, so no reason to bloat our storage file.
        public List<List<Point>> pixelsDrawnByTwoVertices = new List<List<Point>>(); //Can use this list to draw points instead of calculating again and again in order to save performance btw, but I'm just going to calculate and render everything anew for now because I don't have the time, energy, or incentive to improve this.

        [XmlIgnore]
        public Color color = Color.Black;

        [XmlElement("color")]
        public int colorAsRGB //Surrogate property for XML serialization of Color. There are better ways (not neccessarily for all scenarios): https://stackoverflow.com/questions/3280362/most-elegant-xml-serialization-of-color-structure
        {
            get { return color.ToArgb(); }
            set { color = Color.FromArgb(value); }
        }
        public int thickness = 1;
        //when moving, only change the latest point (via sorting?)?

        public virtual void AddVertices(int x, int y) //doesn't draw. Need to draw after this manually.
        {
            vertices.Add(new Point(x, y)); //x is column, y is row ( and item1 is x, item2 is y)
        }
        public virtual WriteableBitmap drawPoints(WriteableBitmap wbmp)
        {
            int pointSquareDimensions = 20;
            if (pointSquareDimensions == 0)
                return wbmp;
            int pseudoRadius = ((pointSquareDimensions - 1) / 2 + 1) - 1;
            int borderTh = 3;
            unsafe
            {
                foreach (var point in vertices)
                {
                    if (point.X >= 0 && point.X <= wbmp.PixelWidth)
                    {
                        if (point.Y >= 0 && point.Y <= wbmp.PixelHeight)
                        {
                            int minC = MathUtil.Clamp(point.X - pseudoRadius, 0, wbmp.PixelWidth);
                            int maxC = MathUtil.Clamp(point.X + pseudoRadius, 0, wbmp.PixelWidth);
                            int minR = MathUtil.Clamp(point.Y - pseudoRadius, 0, wbmp.PixelHeight);
                            int maxR = MathUtil.Clamp(point.Y + pseudoRadius, 0, wbmp.PixelHeight);
                            for (int r = minR; r <= maxR; r++)
                            {
                                for (int c = minC; c <= maxC; c++)
                                {
                                    _pixel_bgr24_bgra32* pixPtr = (_pixel_bgr24_bgra32*)wbmp.GetPixelIntPtrAt(r, c);
                                    pixPtr->blue = 0;
                                    pixPtr->green = 0;
                                    pixPtr->red = 255;
                                    if ((r <= minR + borderTh || r >= maxR - borderTh) || (c <= minC + borderTh || c >= maxC - borderTh)) //(r <= minR + borderTh || r >= maxR - borderTh) && (c <= minC + borderTh || c >= maxC - borderTh) this one makes a cross
                                        pixPtr->red = 0;
                                }
                            }
                        }
                    }
                }
            }
            return wbmp;
        }

        public virtual WriteableBitmap draw(WriteableBitmap wbmp, bool showPoints = true, int _thickness = 1)  //public abstract //;
        {
            if (showPoints)
                wbmp = drawPoints(wbmp);
            return wbmp;
        }

        public virtual bool isShapeType(SupportedShapes givenShape)
        {
            throw new NotSupportedException("isShape shouldn't be called on a\"Shape\" type but it's children. If it is a child, use visitor pattern and make sure the child has this function overriden there at all.");
            //return false;
        }


        public static Shape ConstructRequiredShape(SupportedShapes selectedShp) //factory method //pattern
        {
            switch (selectedShp)
            {
                case SupportedShapes.Line:
                    return new PolyLine();
                case SupportedShapes.Polygon:
                    return new Polygon();
                    //break;
                case SupportedShapes.Circle:
                    return new Circle();
                //break;
                case SupportedShapes.ClippingPolygon:
                    return new ClippingPolygon();
                default:
                    throw new ArgumentOutOfRangeException(null);
            }
        }
    }
}