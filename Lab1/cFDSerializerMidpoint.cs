using Computer_Graphics_1.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab1
{
    [Serializable]
    public class cFDSerializerMidpoint
    {
        public int[] OneD_sqrCnvMat;
        public int numColsInRow;//numCols
        public _coords anchorKernel;
        public double divisor;
        public int offset;
        public cFDSerializerMidpoint()
        {

        }
        public cFDSerializerMidpoint(int[,] _sqrCnvMat, _coords _anchorKernel, double _divisor, int _offset)
        {
            this.numColsInRow = _sqrCnvMat.GetLength(1);
            this.OneD_sqrCnvMat = new int[_sqrCnvMat.Length];
            int index = 0;
            foreach (int num in _sqrCnvMat)
            {
                OneD_sqrCnvMat[index] = num;
                index++;
            }
            this.anchorKernel.c = _anchorKernel.c;
            this.anchorKernel.r = _anchorKernel.r;
            this.divisor = _divisor;
            this.offset = _offset;
        }
        public int[,] getSqrCnvMat()
        {
            int cols = numColsInRow;
            int rows = OneD_sqrCnvMat.Length / numColsInRow;
            int[,] _sqrCnvMat = new int[rows, cols];
            for(int i=0;i<rows;i++)
            {
                for(int j=0;j<cols;j++)
                {
                    _sqrCnvMat[i, j] = OneD_sqrCnvMat[(i*numColsInRow)+j];
                }
            }
            return _sqrCnvMat;
        }
    }
}
