using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Computer_Graphics_1.Lab5.Helpers
{
    public static class AffineTransformHelper
    {
        /// <summary>
        /// Gets the rotation around X axis transformation matrix for the given
        /// angle by which to rotate the coordinates.
        /// </summary>
        /// <param name="angle"> Angle in degrees.</param>
        /// <returns></returns>
        public static Matrix4x4 getRotationXMatrix(double angle)
        {
            double angleRotX = Math.PI * angle / 180.0;
            return new Matrix4x4(1, 0, 0, 0,
                                0, (float)Math.Cos(angleRotX), -(float)Math.Sin(angleRotX), 0,
                                0, (float)Math.Sin(angleRotX), (float)Math.Cos(angleRotX), 0,
                                0, 0, 0, 1);
        }

        /// <summary>
        /// Gets the rotation around Y axis transformation matrix for the given
        /// angle by which to rotate the coordinates.
        /// </summary>
        /// <param name="angle"> Angle in degrees.</param>
        /// <returns></returns>
        public static Matrix4x4 getRotationYMatrix(double angle)
        {
            double angleRotY = Math.PI * angle / 180.0;
            return new Matrix4x4((float)Math.Cos(angleRotY), 0, (float)Math.Sin(angleRotY), 0,
                                0, 1, 0, 0,
                                -(float)Math.Sin(angleRotY), 0, (float)Math.Cos(angleRotY), 0,
                                0, 0, 0, 1);
        }

        /// <summary>
        /// Gets the rotation around Z axis transformation matrix for the given
        /// angle by which to rotate the coordinates.
        /// </summary>
        /// <param name="angle"> Angle in degrees.</param>
        /// <returns></returns>
        public static Matrix4x4 getRotationZMatrix(double angle)
        {
            double angleRotZ= Math.PI * angle / 180.0;
            return new Matrix4x4(
                (float)Math.Cos(angleRotZ), (float)Math.Sin(angleRotZ), 0, 0,
                -1 * (float)Math.Sin(angleRotZ), (float)Math.Cos(angleRotZ), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        public static Matrix4x4 getTranslationMatrix(float tX, float tY, float tZ)//, float tW=1)
        {
            Matrix4x4 m = Matrix4x4.Identity;
            m.M14 = tX;
            m.M24 = tY;
            m.M34 = tZ;
            //m.M44 = tW;
            return m;
        }

        public static Matrix4x4 getScaleMatrix(float scaleX, float scaleY, float scaleZ)
        {
            Matrix4x4 m = new Matrix4x4(
                scaleX,0,0,0,
                0,scaleY,0,0,
                0,0,scaleZ,0,
                0,0,0,1
                );
            return m;
        }

        public static Matrix4x4 getProjectionMatrix(double FOVdegrees,double canvasWidth, double canvasHeight)
        {
            double fovRadians = Math.PI * FOVdegrees / 180.0;
            float d = ((float)((canvasWidth / 2) / Math.Tan(fovRadians / 2)));
            float Cx = ((float)(canvasWidth / 2));
            float Cy = ((float)(canvasHeight / 2));

            return new Matrix4x4(-d, 0, Cx, 0,
                                0, d, Cy, 0,
                                0, 0, 0, 1,
                                0, 0, 1, 0);
        }
    }
}
