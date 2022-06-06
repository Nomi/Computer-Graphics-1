using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5
{
    /// <summary>
    /// Representation of a 3D point in the Affine Space.
    /// Where x, y, z, and w are the homogeneous coordinates of a point in 3D space, with the 
    /// corresponding affine coordinates (x/w, y/w, z/w). w is usually just 1 because we are using
    /// normalized coordinates.
    /// </summary>
    public class Point3D_AffC
    {
        //================= MEMBERS =================\\
        public double X;
        public double Y;
        public double Z;
        public double W = 1; //Anchor/Origin. 1 by default, i.e. we're dealing with normalized coordinates.
        public Point textureCoordinates = Point.Empty;//Point.Empty==0,0
        public Point DisplayCoordinates { get => new Point((int)X, (int)Y); } //INTENTIONALLY WRONG RIGHT NOW!

        //================= CONSTRUCTORS =================\\

        /// <summary>
        /// Constructs a Point Using AffineCoordinates (normalized variant, i.e. w=1).
        /// </summary>
        /// <param name="_x">x coordinate</param>
        /// <param name="_y">y coordinate</param>
        /// <param name="_z">z coordinate</param>
        public Point3D_AffC(double _x, double _y, double _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
            W = 1;
        }


        /// <summary>
        /// Constructs a Point Using AffineCoordinates.
        /// </summary>
        /// <param name="_x">x coordinate</param>
        /// <param name="_y">y coordinate</param>
        /// <param name="_z">z coordinate</param>
        /// <param name="_w">w (anchor)</param>
        public Point3D_AffC(double _x, double _y, double _z, double _w)
        {
            X = _x;
            Y = _y;
            Z = _z;
            W = _w;
        }


        //================= METHODS =================\\

    }
}
