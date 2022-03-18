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
using Computer_Graphics_1.HelperClasses.Extensions;
using Computer_Graphics_1.Lab1;

namespace Computer_Graphics_1
{
    public partial class MainForm : Form
    {
        private Bitmap ogBitmap = null;
        private WriteableBitmap wBmpToEdit = null;
        public static class cnvFilt
        {
            public static bool convFilterParametersSet = false;
            public static int[,] Mat = null;
            public static double divisor = -99999;
            public static _coords anchorCoords; //indexed from 1
            public static int offset = 0;
        }

        
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
                openFileDialog.Filter = "Image files (*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|Bitmap image file (*.bmp)|*.bmp;|All files|*.*";
                openFileDialog.FilterIndex = 1;
                //openFileDialog.RestoreDirectory = true;

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

                    inversionCheckBox.Checked = false;
                }
                else
                {
                    MessageBox.Show("No image selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //private void invertFilter_Click(object sender, EventArgs e)
        //{
        //    ////WriteableBitmap writtenImg = new WriteableBitmap(wBmpToEdit);
        //    //WriteableBitmap writtenImg = ImageUtil.GetWritableBitmapFromBitmap(new Bitmap(newPictureBox.Image)); //If this is efficient enough, I could just use this and change wBmpToEdit to be "const".
        //    Lab1.FunctionalFilters.InvertWriteableBitmap(wBmpToEdit);//(writtenImg);
        //    newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);//(writtenImg);
        //    if (invertFilterButton.ForeColor == SystemColors.ActiveCaptionText|| invertFilterButton.ForeColor==Color.Black)
        //        invertFilterButton.ForeColor = Color.Green;
        //    else
        //        invertFilterButton.ForeColor = Color.Black;
        //}

        private void undoAllProcessingMenuItem_Click(object sender, EventArgs e)
        {
            wBmpToEdit = ImgUtil.GetWritableBitmapFromBitmap(ogBitmap);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
            inversionCheckBox.Checked = false;
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

        private void applyConvFiltButton_Click(object sender, EventArgs e)
        {
            if(cnvFilt.convFilterParametersSet)
            {
                int[,] blurConvMat3x3 = new int[3, 3];//[9, 9];
                for (int i = 0; i < 3; i++)//i<9
                {
                    for (int j = 0; j < 3; j++)//j<9
                    {
                        blurConvMat3x3[i, j] = 1;//0;//1;
                    }
                }
                /////the above sets for blur and it works
                /////Gaussian Smoothening/blur: (should work)
                ////blurConvMat3x3[0, 0] = 0; blurConvMat3x3[0, 1] = 1; blurConvMat3x3[0, 2] = 0;
                ////blurConvMat3x3[1, 0] = 1; blurConvMat3x3[1, 1] = 4; blurConvMat3x3[1, 2] = 1;
                ////blurConvMat3x3[2, 0] = 0; blurConvMat3x3[2, 1] = 1; blurConvMat3x3[2, 2] = 0;
                /////Sharpen: (works)
                ////blurConvMat3x3[0, 0] = 0; blurConvMat3x3[0, 1] = -1; blurConvMat3x3[0, 2] = 0;
                ////blurConvMat3x3[1, 0] = -1; blurConvMat3x3[1, 1] = 5; blurConvMat3x3[1, 2] = -1;
                ////blurConvMat3x3[2, 0] = 0; blurConvMat3x3[2, 1] = -1; blurConvMat3x3[2, 2] = 0;
                /////Mean removal sharpen?: (probably works as it should)
                ////blurConvMat3x3[0, 0] = -1; blurConvMat3x3[0, 1] = -1; blurConvMat3x3[0, 2] = -1;
                ////blurConvMat3x3[1, 0] = -1; blurConvMat3x3[1, 1] = 9; blurConvMat3x3[1, 2] = -1;
                ////blurConvMat3x3[2, 0] = -1; blurConvMat3x3[2, 1] = -1; blurConvMat3x3[2, 2] = -1;
                /////Edge detection: (Works! (USE 0.1 divisor THO)
                blurConvMat3x3[0, 0] = 0; blurConvMat3x3[0, 1] = -1; blurConvMat3x3[0, 2] = 0;
                blurConvMat3x3[1, 0] = 0; blurConvMat3x3[1, 1] = 1; blurConvMat3x3[1, 2] = 0;
                blurConvMat3x3[2, 0] = 0; blurConvMat3x3[2, 1] = 0; blurConvMat3x3[2, 2] = 0;
                /////Emboss: (works)
                ////blurConvMat3x3[0, 0] = -1; blurConvMat3x3[0, 1] = 0; blurConvMat3x3[0, 2] = 1;
                ////blurConvMat3x3[1, 0] = -1; blurConvMat3x3[1, 1] = 1; blurConvMat3x3[1, 2] = 1;
                ////blurConvMat3x3[2, 0] = -1; blurConvMat3x3[2, 1] = 0; blurConvMat3x3[2, 2] = 1;

                ////ConvolutionFilters.ConvolutionFilter(blurConvMat3x3, wBmpToEdit);//,ref newPictureBox);
                _coords Coords; Coords.r = 2; Coords.c = 2; //7x7:5,6 //6,5 //indexed from 1
                //ConvolutionFilters.Apply(blurConvMat3x3, Coords, wBmpToEdit, 0.1);//,0.1);//(blurConvMat3x3,Coords,wBmpToEdit,0); ,0.1
                ConvolutionFilters.Apply(cnvFilt.Mat, cnvFilt.anchorCoords, wBmpToEdit, cnvFilt.divisor, cnvFilt.offset);
                //ConvolutionFilters.ConvFilCleanCode(cnvFilt.Mat, cnvFilt.anchorCoords, wBmpToEdit, cnvFilt.divisor, cnvFilt.offset);
                newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
            }
        }

        private void inversionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ////WriteableBitmap writtenImg = new WriteableBitmap(wBmpToEdit);
            //WriteableBitmap writtenImg = ImageUtil.GetWritableBitmapFromBitmap(new Bitmap(newPictureBox.Image)); //If this is efficient enough, I could just use this and change wBmpToEdit to be "const".
            Lab1.FunctionalFilters.InvertWriteableBitmap(wBmpToEdit);//(writtenImg);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);//(writtenImg

        }

        private void invertFilter_Click(object sender, EventArgs e)
        {

        }

        private void saveAsResultpngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wBmpToEdit.SaveAsPNG("result.png");
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void edgeDetRadioButton_SelectIndexChanged(object sender, EventArgs e)
        {
            if(edgeDetRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 128;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                ///Edge detection: (Works! (USE 0.1 divisor THO)
                cnvFilt.Mat[0, 0] = 0; cnvFilt.Mat[0, 1] = 0; cnvFilt.Mat[0, 2] = 0;
                cnvFilt.Mat[1, 0] = -1; cnvFilt.Mat[1, 1] = 1; cnvFilt.Mat[1, 2] = 0;
                cnvFilt.Mat[2, 0] = 0; cnvFilt.Mat[2, 1] = 0; cnvFilt.Mat[2, 2] = 0;

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void blurRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(blurRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 0;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                for (int i = 0; i < 3; i++)//i<9
                {
                    for (int j = 0; j < 3; j++)//j<9
                    {
                        cnvFilt.Mat[i, j] = 1;//0;//1;
                    }
                }

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void gaussSmoothRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(gaussSmoothRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 0;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                ///Gaussian Smoothening/blur: (should work)
                cnvFilt.Mat[0, 0] = 0; cnvFilt.Mat[0, 1] = 1; cnvFilt.Mat[0, 2] = 0;
                cnvFilt.Mat[1, 0] = 1; cnvFilt.Mat[1, 1] = 4; cnvFilt.Mat[1, 2] = 1;
                cnvFilt.Mat[2, 0] = 0; cnvFilt.Mat[2, 1] = 1; cnvFilt.Mat[2, 2] = 0;

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void sharpenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(sharpenRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 0;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                ///Sharpen: (works)
                cnvFilt.Mat[0, 0] = 0; cnvFilt.Mat[0, 1] = -1; cnvFilt.Mat[0, 2] = 0;
                cnvFilt.Mat[1, 0] = -1; cnvFilt.Mat[1, 1] = 5; cnvFilt.Mat[1, 2] = -1;
                cnvFilt.Mat[2, 0] = 0; cnvFilt.Mat[2, 1] = -1; cnvFilt.Mat[2, 2] = 0;

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void meanRemSharpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(meanRemSharpRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 0;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                ///Mean removal sharpen?: (probably works as it should)
                cnvFilt.Mat[0, 0] = -1; cnvFilt.Mat[0, 1] = -1; cnvFilt.Mat[0, 2] = -1;
                cnvFilt.Mat[1, 0] = -1; cnvFilt.Mat[1, 1] = 9; cnvFilt.Mat[1, 2] = -1;
                cnvFilt.Mat[2, 0] = -1; cnvFilt.Mat[2, 1] = -1; cnvFilt.Mat[2, 2] = -1;

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void embossRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(embossRadioButton.Checked)
            {
                cnvFilt.Mat = new int[3, 3];
                cnvFilt.divisor = -99999;
                cnvFilt.offset = 0;
                cnvFilt.anchorCoords.r = 2; cnvFilt.anchorCoords.c = 2;
                ///Emboss: (works)
                cnvFilt.Mat[0, 0] = -1; cnvFilt.Mat[0, 1] = 0; cnvFilt.Mat[0, 2] = 1;
                cnvFilt.Mat[1, 0] = -1; cnvFilt.Mat[1, 1] = 1; cnvFilt.Mat[1, 2] = 1;
                cnvFilt.Mat[2, 0] = -1; cnvFilt.Mat[2, 1] = 0; cnvFilt.Mat[2, 2] = 1;

                cnvFilt.convFilterParametersSet = true;
            }
        }

        private void customizeConvFilterButton_Click(object sender, EventArgs e)
        {
            if(!cnvFilt.convFilterParametersSet)
            {
                MessageBox.Show("Select a base Convolution Filter to customize first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (ConvFiltCustomizer cFCD = new ConvFiltCustomizer(cnvFilt.Mat, cnvFilt.anchorCoords, cnvFilt.divisor, cnvFilt.offset))
            {
                if(cFCD.ShowDialog()==DialogResult.OK)
                {
                    ConvolutionFilters.Apply(cFCD.sqrCnvMat, cFCD.anchorKernel, wBmpToEdit, cFCD.divisor, cFCD.offset);
                    //ConvolutionFilters.ConvFilCleanCode(cFCD.sqrCnvMat, cFCD.anchorKernel, wBmpToEdit, cFCD.divisor, cFCD.offset);
                    newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
                }
                else
                {
                    MessageBox.Show("Custom filter not applied.", "Action cancelled", MessageBoxButtons.OK);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openImageToolStripMenuItem_Click(sender, e);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.Normal;
            newPictureBox.SizeMode = PictureBoxSizeMode.Normal;
            normalToolStripMenuItem.Checked = true;
            stretchToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            newPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            normalToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            normalToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvolutionFilters.MedianFilter3x3(wBmpToEdit);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void lab1TabPage_Click(object sender, EventArgs e)
        {

        }
    }
}
