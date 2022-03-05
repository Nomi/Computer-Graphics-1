using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.HelperClasses
{
    public static class ByteArrayEx
    {
        public static byte[,] ConvertTo2D(this byte[] bArr, int colCount, int rowCount)//for pixels, need to multiply enter stride (or colCount*numChannels) for colCount.
        {
            byte[,] b2DArr = new byte[rowCount, colCount];
            for(int i=0,B=0; i<rowCount;i++)
            {
                for(int j=0;j<colCount;j++,B++)
                {
                    b2DArr[i, j] = bArr[B];
                }
            }
            return b2DArr;
        }
        public static _pixel_bgr24_bgra32 GetPixelAt(this byte[,] bArr, int row, int col, int numChannelsPerPixel) //Might only work for 8bit-per-color formats (also true for ConvertTo2D)
        {
            int numChan = numChannelsPerPixel;
            _pixel_bgr24_bgra32 pix;
            pix.blue = bArr[row, numChan*col + 0];
            pix.green = bArr[row, numChan*col + 1];
            pix.red = bArr[row, numChan*col + 2];
            return pix;
        }
    }
}
