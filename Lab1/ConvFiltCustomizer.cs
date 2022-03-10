using Computer_Graphics_1.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

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
            //kernelDataGridView.Columns.Clear();
            //kernelDataGridView.Rows.Clear();
            int oldColCount = kernelDataGridView.Columns.Count;
            int oldRowCount = kernelDataGridView.RowCount;
            int rowCount = sqrCnvMat.GetLength(0);
            int colCount = sqrCnvMat.GetLength(1);
            for (int c = 0; c < colCount; c++)
            {
                kernelDataGridView.Columns.Add(c.ToString(), "");
                
                if (kernelDataGridView.RowCount == oldRowCount)//==0
                {
                    kernelDataGridView.Rows.Add(rowCount);
                }
                for (int r = 0; r < rowCount; r++)
                {
                    kernelDataGridView[c, r].Value = sqrCnvMat[r, c];
                    //if(kernelDataGridView[c, r].Selected)
                    //{
                    //    kernelDataGridView[c,r]
                    //}
                }
            }
            for(int i=0;i<oldColCount;i++)
            {
                kernelDataGridView.Columns.RemoveAt(kernelDataGridView.Columns.Count-1);
            }
            for(int i=0; i<oldRowCount;i++)
            {
                kernelDataGridView.Rows.RemoveAt(kernelDataGridView.Rows.Count-1);
            }
            //kernelDataGridView.
            kernelDataGridView.Refresh();
            kernelDataGridView.Update();
            kernelDataGridView.RefreshEdit();
            //kernelDataGridView.RefreshEdit();
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

        public string serializeToXML(string filePath)
        {
            Computer_Graphics_1.Lab1.cFDSerializerMidpoint serializerMidpoint = new Lab1.cFDSerializerMidpoint(sqrCnvMat, anchorKernel, divisor, offset);
            var xSer = new XmlSerializer(serializerMidpoint.GetType());


            //string filePath = System.AppContext.BaseDirectory + "/Custom Filters/filter.xml";
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory.Create();

            FileStream fs = File.Create(filePath);
            fs.Close();

            using (TextWriter tw = new StreamWriter(filePath))
            {
                xSer.Serialize(tw, serializerMidpoint);
            }
            return filePath;

            //throw new NotImplementedException();
        }
        public string deserializeFromXML(string filePath)
        {
            //string filePath = System.AppContext.BaseDirectory + "/Custom Filters/filter.xml";
            Lab1.cFDSerializerMidpoint cfdmp = new Lab1.cFDSerializerMidpoint();
            using(var sr= new StreamReader(filePath))
            {
                var xSer = new XmlSerializer(cfdmp.GetType());//typeof(Lab1.cFDSerializerMidpoint));
                //sr.ReadToEnd();
                cfdmp =(Lab1.cFDSerializerMidpoint) xSer.Deserialize(sr);
            }
            if(cfdmp!=null)
            {
                setFilterParameters(cfdmp.getSqrCnvMat(), cfdmp.anchorKernel, cfdmp.divisor, cfdmp.offset);
                return filePath;
            }
            else
            {
                throw new Exception("Done fudged up.");
            }
        }
        private void kernelDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string data=kernelDataGridView[e.ColumnIndex, e.RowIndex].Value?.ToString();

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
                if(kernelDataGridView.Columns.Count!=0)
                {
                    for (int i = 0; i < chgdNumRows - oldNumRows; i++)
                    {
                        //kernelDataGridView.Rows.AddCopy(kernelDataGridView.RowCount - 1);
                        int lastAddedRow = kernelDataGridView.Rows.Add();
                        for (int c = 0; c < kernelDataGridView.ColumnCount; c++)
                        {
                            kernelDataGridView[c, lastAddedRow].Value = string.Copy("0");
                        }
                    }
                    convertDatagridCnvMat();
                }
            }
            //int test = sqrCnvMat.Length;

        }

        private void kernelDimCol_TextChanged(object sender, EventArgs e)
        {
            if (kernelDimCol.Text.Length <= 0)
                return;
            int chgdNumCols = int.Parse(kernelDimCol.Text);
            int oldNumCols = kernelDataGridView.ColumnCount;
            if (chgdNumCols < oldNumCols)
            {
                for (int j = 0; j < oldNumCols - chgdNumCols; j++)
                {
                    kernelDataGridView.Columns.RemoveAt(kernelDataGridView.ColumnCount - 1);
                }
                convertDatagridCnvMat();
            }
            else if (chgdNumCols > oldNumCols)
            {
                for (int j = 0; j < chgdNumCols - oldNumCols; j++)
                {
                    //kernelDataGridView.cols.AddCopy(kernelDataGridView.colCount - 1);
                    int lastAddedCol = kernelDataGridView.Columns.Add(kernelDataGridView.ColumnCount.ToString(),"");
                    if (oldNumCols == 0&&j==0)
                    {
                        kernelDataGridView.Rows.Add(int.Parse(kernelDimRow.Text));
                    }
                    for (int r = 0; r < kernelDataGridView.RowCount; r++)
                    {
                        kernelDataGridView[lastAddedCol,r].Value = string.Copy("0");
                    }
                }
                convertDatagridCnvMat();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.RestoreDirectory = false;
                openFileDialog.InitialDirectory = AppContext.BaseDirectory + "Custom Filters\\";
                openFileDialog.Filter = "Filter files (*.filt)|*.filt|XML file (*.XML)|*.XML;|All files|*.*";
                openFileDialog.FilterIndex = 1;
                

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    deserializeFromXML(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("No filter selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = AppContext.BaseDirectory + "Custom Filters\\";
                saveFileDialog.Filter = "Filter files (*.filt)|*.filt|XML file (*.XML)|*.XML;|All files|*.*";
                saveFileDialog.FilterIndex = 1;
                //saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "customFilter";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    serializeToXML(saveFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("No destination file selected.", "Saving cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //private void kernelDataGridView_Leave(object sender, EventArgs e)
        //{
        //    kernelDataGridView.ClearSelection();
        //    kernelDataGridView.Refresh();
        //}
    }


}
