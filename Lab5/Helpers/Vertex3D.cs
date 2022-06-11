using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5.Helpers
{
    public class Vertex3D
    {
        /// <summary>
        /// Actual position in 3D Affine Coordinates
        /// </summary>
        public Vector4 position { get; set; }

        /// <summary>
        /// Position projected after transformations. The X and Y from this are used to draw on our 2D screen.
        /// </summary>
        public Vector4 projectedPosition { get; set; }

        /// <summary>
        /// Defines which point of the texture image would overlap with the current vertex.
        /// </summary>
        public Point textureCoords { get; set; }

        public Vertex3D(Vector4 pos)
        {
            position = pos;
        }

    }
}
