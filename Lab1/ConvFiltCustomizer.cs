using Computer_Graphics_1.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Computer_Graphics_1
{
    public partial class ConvFiltCustomizer : Form
    {
        public int[,] sqrCnvMat;
        public _coords anchorKernel;
        public double divisor = -99999;
        public int offset = 0;
        public ConvFiltCustomizer(int[,] _sqrCnvMat, _coords _anchorKernel, double _divisor, int _offset)
        {
            InitializeComponent();
            //kernelDataGridView.AllowUserToAddRows = false;
            //kernelDataGridView.AllowUserToDeleteRows = false;
            //kernelDataGridView.AllowUserToOrderColumns = false;
            setFilterParameters(_sqrCnvMat, _anchorKernel, _divisor, _offset);
        }
        private void setFilterParameters(int[,] _sqrCnvMat, _coords _anchorKernel, double _divisor, int _offset)
        {
            this.sqrCnvMat = _sqrCnvMat;
            this.anchorKernel = _anchorKernel;
            this.divisor = _divisor;
            this.offset = _offset;

            setKernelDataGrid();
            divisorNumericUpDown.Value = (decimal)divisor;
            offsetMaskedTextBox.Text = offset.ToString();

            kernelDimRow.Text = this.sqrCnvMat.GetLength(0).ToString();
            kernelDimCol.Text = this.sqrCnvMat.GetLength(1).ToString();

            anchorCoordR.Text = this.anchorKernel.r.ToString();
            anchorCoordC.Text = this.anchorKernel.c.ToString();
        }

        private void setKernelDataGrid()
        {
            kernelDataGridView.Rows.Clear();
            kernelDataGridView.Columns.Clear();
            int rowCount = sqrCnvMat.GetLength(0);
            int colCount = sqrCnvMat.GetLength(1);
            for (int c = 0; c < colCount; c++)
            {
                kernelDataGridView.Columns.Add(c.ToString(), "");
                
                if (kernelDataGridView.RowCount == 0)
                {
                    kernelDataGridView.Rows.Add(rowCount);
                }
                for (int r = 0; r < rowCount; r++)
                {
                    kernelDataGridView[c, r].Value = sqrCnvMat[r, c];
                }
            }
            ////kernelDataGridView.DataSource = this.sqrCnvMat;
        }

        private void convertDatagridCnvMat()
        {
            int[,] newConvMat = new int[kernelDataGridView.RowCount, kernelDataGridView.ColumnCount];
            for(int r=0;r<kernelDataGridView.RowCount;r++)
            {
                for (int c = 0; c < kernelDataGridView.ColumnCount; c++)
                {
                    if (kernelDataGridView[c, r].Value != null)
                        newConvMat[r, c] = int.Parse(kernelDataGridView[c, r].Value.ToString());
                    else
                        newConvMat[r, c] = 0;
                }   
            }
            sqrCnvMat = newConvMat;
        }

        private void kernelDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string data = kernelDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
            if (!int.TryParse(data, out int value))
            {
                MessageBox.Show("Enter valid integer.");
                kernelDataGridView[e.ColumnIndex, e.RowIndex].Value = null;
                e.Cancel = true;
            }
        }


        private void kernelDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //convertDatagridCnvMat();
            sqrCnvMat[e.RowIndex, e.ColumnIndex] = int.Parse(kernelDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void divisorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            divisor =(double) divisorNumericUpDown.Value; //decimal is more precise than double, so have to cast to only copy double part.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                divisor = -99999;
                divisorNumericUpDown.Enabled = false;
            }
            else
            {
                divisor = 1;
                divisorNumericUpDown.Value = (decimal)divisor;
                divisorNumericUpDown.Enabled = true;
            }
        }

        private void offsetMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            if(offsetMaskedTextBox.Text.Length>0)
                offset = int.Parse(offsetMaskedTextBox.Text);
        }

        private void anchorCoordR_TextChanged(object sender, EventArgs e)
        {
            this.anchorKernel.r = int.Parse(anchorCoordR.Text);
        }

        private void anchorCoordC_TextChanged(object sender, EventArgs e)
        {
            this.anchorKernel.c = int.Parse(anchorCoordC.Text);
        }

        private void kernelDimRow_TextChanged(object sender, EventArgs e)
        {
            if (kernelDimRow.Text.Length <= 0)
                return;

            int chgdNumRows = int.Parse(kernelDimRow.Text);
            int oldNumRows = kernelDataGridView.RowCount;
            if (chgdNumRows < oldNumRows)
            {
                for (int i = 0; i < oldNumRows - chgdNumRows; i++)
                {
                    kernelDataGridView.Rows.RemoveAt(kernelDataGridView.RowCount-1);
                }
                convertDatagridCnvMat();
            }
            else if (chgdNumRows> oldNumRows)
            {
                for(int i=0; i< chgdNumRows- oldNumRows; i++)
                {
                    //kernelDataGridView.Rows.AddCopy(kernelDataGridView.RowCount - 1);
                    int lastAddedRow=kernelDataGridView.Rows.Add();
                    for(int c=0;c<kernelDataGridView.ColumnCount;c++)
                    {
                        kernelDataGridView[ c, lastAddedRow].Value = string.Copy("0");
                    }
                }
                convertDatagridCnvMat();
            }
            //int test = sqrCnvMat.Length;

        }
    }
}
