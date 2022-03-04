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
using Computer_Graphics_1.HelperClasses;
using Computer_Graphics_1.Lab1;

namespace Computer_Graphics_1
{
    public partial class MainForm : Form
    {
        private Bitmap ogBitmap = null;
        private WriteableBitmap wBmpToEdit = null;
        public MainForm()
        {
            InitializeComponent();

            //foreach (Control cntrl in lab1TabPage.Controls)
            //{
            //    cntrl.Enabled = false;
            //}
        }

        private void ogPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (ogPictureBox.Image == null)
            {
                openImageToolStripMenuItem_Click(null, null);
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "Bitmap image file (*.bmp)|*.bmp;|Image files (*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|All files|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ogBitmap = new Bitmap(openFileDialog.FileName);
                    ogPictureBox.Image = ogBitmap;
                    wBmpToEdit = ImgUtil.GetWritableBitmapFromBitmap(ogBitmap);
                    //wBmpToEdit = ImageUtil.GetWriteableBitmapFromAbsURI(openFileDialog.FileName);
                    newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
                    //foreach (Control cntrl in lab1TabPage.Controls)
                    //{
                    //    cntrl.Enabled = true;
                    //}
                    labsTabControl.Enabled = true;
                    undoAllProcessingMenuItem.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No image selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void invertFilter_Click(object sender, EventArgs e)
        {
            ////WriteableBitmap writtenImg = new WriteableBitmap(wBmpToEdit);
            //WriteableBitmap writtenImg = ImageUtil.GetWritableBitmapFromBitmap(new Bitmap(newPictureBox.Image)); //If this is efficient enough, I could just use this and change wBmpToEdit to be "const".
            Lab1.FunctionalFilters.InvertWriteableBitmap(wBmpToEdit);//(writtenImg);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);//(writtenImg);
            if (invertFilterButton.ForeColor == SystemColors.ActiveCaptionText|| invertFilterButton.ForeColor==Color.Black)
                invertFilterButton.ForeColor = Color.Green;
            else
                invertFilterButton.ForeColor = Color.Black;
        }

        private void undoAllProcessingMenuItem_Click(object sender, EventArgs e)
        {
            wBmpToEdit = ImgUtil.GetWritableBitmapFromBitmap(ogBitmap);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void brightnessCorrection_Click(object sender, EventArgs e)
        {
            FunctionalFilters.BrightnessCorrectionWbmp(wBmpToEdit, 20);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void contrastEnhanceButton_Click(object sender, EventArgs e)
        {
            FunctionalFilters.ContrastEnhancementWbmp(wBmpToEdit, 1.25);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void gammaCorrectionButton_Click(object sender, EventArgs e)
        {
            FunctionalFilters.GammaCorrection(wBmpToEdit, 1.666);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] blurConvMat3x3 = new int[3, 3];
            for(int i=0;i<3;i++)
            {
               for(int j=0; j<3; j++)
                {
                    blurConvMat3x3[i,j] = 1;
                }
            }
            ConvolutionFilters.ConvolutionFilter(blurConvMat3x3, wBmpToEdit,ref newPictureBox);
            newPictureBox.Image= ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }
    }
}
