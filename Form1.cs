﻿//#define _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
 /*
 * To avoid defining this symbol in every file, refer to: https://stackoverflow.com/questions/436369/how-to-define-a-constant-globally-in-c-sharp-like-debug
 * Also, learn about Conditional Attribute and the like here: https://stackoverflow.com/a/975370
 */
 #define Lab3
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
using Computer_Graphics_1.Lab1.LabPart;
using Computer_Graphics_1.Lab2;
using Computer_Graphics_1.Lab3;


namespace Computer_Graphics_1
{
    public partial class MainForm : Form
    {
        private Bitmap ogBitmap = null;
        private WriteableBitmap wBmpToEdit = null;

        private bool drawingEnabled = false;
        private SupportedShapes selectedShapeType = SupportedShapes.Line;

        private Tuple<int, int> selectedPointShapeAndPointIndices = null;

        List<Shape> shapes = new List<Shape>();
        bool guptaSproulAntiAliasingEnabled = false;
        bool superSamplingEnabled = false;
        bool drawPoints = true;
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

            setComparisonViewImage(global::Computer_Graphics_1.Properties.Resources._default);
            if(ogPictureBox.Image!=null)
            {
                setApplicationInteractionEnabled(true);
                zoomToolStripMenuItem_Click(null, null);
            }

#if Lab3
            drawingCanvasPictureBox.Image = DrawFilledRectangle(drawingCanvasPictureBox.Width, drawingCanvasPictureBox.Height);
            if (selectedShapeType==SupportedShapes.Line)
                lineRadioButton.Checked = true;
            labsTabControl.SelectedTab = lab3TabPage;
            imagesTabControl.SelectedTab = drawingViewTabPage;
#endif
            //labsTabControl.SelectedTab = lab2TabPage;
            //labsTabControl.SelectedTab = lab3TabPage; imagesTabControl.SelectedTab = drawingViewTabPage;

            //foreach (Control cntrl in lab1TabPage.Controls)
            //{
            //    cntrl.Enabled = false;
            //}
            //ogPictureBox.Image=
        }

        private void undoAllProcessingMenuItem_Click(object sender, EventArgs e)
        {
            if (ogBitmap == null)
                return;
            wBmpToEdit = ImgUtil.GetWritableBitmapFromBitmap(ogBitmap);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
            inversionCheckBox.Checked = false;
            if (drawingCanvasPictureBox.Image != null)
            {
                drawingCanvasPictureBox.Image = DrawFilledRectangle(drawingCanvasPictureBox.Width, drawingCanvasPictureBox.Height);
                foreach (Shape shp in shapes)
                {
                    shp.vertices.Clear();
                }
                if (drawingEnabled)
                    toggleDrawingButton_Click(null, null);
            }
        }


        private Bitmap DrawFilledRectangle(int x, int y, Brush brush=null)
        {
            if(brush==null)
            {
                brush = Brushes.White;
            }
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(brush, ImageSize);
            }
            return bmp;
        }
#region testing if sub regions work!
#region do they?
        //Turns out they do!
#endregion
#endregion
        private void setApplicationInteractionEnabled(bool isEnabled)
        {
            imagesTabControl.Enabled = isEnabled;
            labsTabControl.Enabled = isEnabled;
            undoAllProcessingMenuItem.Enabled = isEnabled;
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
                    setComparisonViewImage(new Bitmap(openFileDialog.FileName));
                }
                else
                {
                    MessageBox.Show("No image selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void setComparisonViewImage(Bitmap img = null)
        {
            if (img != null)
                ogBitmap = img;
            if (ogBitmap == null)//was already null.
                throw new Exception("Dunno why, should be simple. Will write later, since it doesn't hit.");
            ogPictureBox.Image = ogBitmap;
            wBmpToEdit = ImgUtil.GetWritableBitmapFromBitmap(ogBitmap);
            //wBmpToEdit = ImageUtil.GetWriteableBitmapFromAbsURI(openFileDialog.FileName);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
            //foreach (Control cntrl in lab1TabPage.Controls)
            //{
            //    cntrl.Enabled = true;
            //}

            setApplicationInteractionEnabled(true);
            inversionCheckBox.Checked = false;
        }


#region LAB1-SPECIFIC-REGION
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
                ////ConvolutionFilters.Apply(blurConvMat3x3, Coords, wBmpToEdit, 0.1);//,0.1);//(blurConvMat3x3,Coords,wBmpToEdit,0); ,0.1
                //ConvolutionFilters.Apply(cnvFilt.Mat, cnvFilt.anchorCoords, wBmpToEdit, cnvFilt.divisor, cnvFilt.offset);
                ConvolutionFilters.ConvFilCleanCode(cnvFilt.Mat, cnvFilt.anchorCoords, wBmpToEdit, cnvFilt.divisor, cnvFilt.offset);
                newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
            }
        }

        private void inversionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ////WriteableBitmap writtenImg = new WriteableBitmap(wBmpToEdit);
            //WriteableBitmap writtenImg = ImageUtil.GetWritableBitmapFromBitmap(new Bitmap(newPictureBox.Image)); //If this is efficient enough, I could just use this and change wBmpToEdit to be "const".
            Lab1.FunctionalFilters.InvertWriteableBitmap(wBmpToEdit);//(writtenImg);
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);//(writtenImg
            
            //TBH, this checkbox should have been a button rather than a checkbox.
        }

        private void invertFilter_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void edgeDetRadioButton_SelectIndexChanged(object sender, EventArgs e)
        {
            if (edgeDetRadioButton.Checked)
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
            if (blurRadioButton.Checked)
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
            if (gaussSmoothRadioButton.Checked)
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
            if (sharpenRadioButton.Checked)
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
            if (meanRemSharpRadioButton.Checked)
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
            if (embossRadioButton.Checked)
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
            if (!cnvFilt.convFilterParametersSet)
            {
                MessageBox.Show("Select a base Convolution Filter to customize first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (ConvFiltCustomizer cFCD = new ConvFiltCustomizer(cnvFilt.Mat, cnvFilt.anchorCoords, cnvFilt.divisor, cnvFilt.offset))
            {
                if (cFCD.ShowDialog() == DialogResult.OK)
                {
                    //ConvolutionFilters.Apply(cFCD.sqrCnvMat, cFCD.anchorKernel, wBmpToEdit, cFCD.divisor, cFCD.offset);
                    ConvolutionFilters.ConvFilCleanCode(cFCD.sqrCnvMat, cFCD.anchorKernel, wBmpToEdit, cFCD.divisor, cFCD.offset);
                    newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
                }
                else
                {
                    MessageBox.Show("Custom filter not applied.", "Action cancelled", MessageBoxButtons.OK);
                }
            }
        }

        private void medianFilterButtonClick(object sender, EventArgs e)
        {
            MedianFilter.MedianFilter3x3(wBmpToEdit); //passed by reference by default.
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }
        private void lab1TabPage_Click(object sender, EventArgs e)
        {

        }
#endregion


        private void saveAsResultpngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wBmpToEdit.SaveAsPNG("result.png");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openImageToolStripMenuItem_Click(sender, e);
        }

        private void centerimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            newPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            centerToolStripMenuItem.Checked = true;
            stretchToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            autoSizescrollbarToolStripMenuItem.Checked = false;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            newPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            centerToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = true;
            zoomToolStripMenuItem.Checked = false;
            autoSizescrollbarToolStripMenuItem.Checked = false;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            newPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            centerToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = true;
            autoSizescrollbarToolStripMenuItem.Checked = false;
        }

#region LAB2-SPECIFIC-REGION
        private void averageDitheringButton_Click(object sender, EventArgs e)
        {
            AverageDithering.apply(wBmpToEdit, (int)colorperchannelNumericUpDown.Value);

            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }


        private void cnvrtToGrayscaleButton_Click(object sender, EventArgs e)
        {
            wBmpToEdit.ConvertRGB2GrayscaleRGB();
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }


        private void colorperchannelNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            if(colorperchannelNumericUpDown.Value%2!=0)
            {
                colorperchannelNumericUpDown.Value += 1;
            }
        }

        private void octreeQuantizationButton_Click(object sender, EventArgs e)
        {
            OctreeQuantization.ApplyAverageBasedPerformanceIntensive(wBmpToEdit, Decimal.ToInt32(octreeColorsPerChannelNumericUpDown.Value));
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void autoSizescrollbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            newPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            centerToolStripMenuItem.Checked = false;
            stretchToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            autoSizescrollbarToolStripMenuItem.Checked = true;
        }

        private void popOctreeMemIntnsv_Button_Click(object sender, EventArgs e)
        {
            OctreeQuantization.ApplyPopularityBasedMemoryIntensive(wBmpToEdit, Decimal.ToInt32(octreeColorsPerChannelNumericUpDown.Value));
            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AverageDithering.applyYCbCr(wBmpToEdit, (int)colorperchannelNumericUpDown.Value);

            newPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wBmpToEdit);
        }
#endregion

#region LAB3-SPECIFIC-REGION
        //Shape shp = new PolyLine();
        private void drawingCanvasPictureBox_Click(object sender, EventArgs e)
        {


            MouseEventArgs mE = (MouseEventArgs) e;

            if(drawingEnabled)
            {
                resetAllShapes();
                selectedPointShapeAndPointIndices = null;
                shapes.Last().AddVertices(mE.Location.X, mE.Location.Y); //x is col, y is row
                //if(selectedShapeType==SupportedShapes.Line)
                //drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(((PolyLine)shp).draw(ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image)),false));

                drawAllShapes(drawPoints);
                if (shapes.Last().vertices.Count() == 2 && shapes.Last().isShapeType(SupportedShapes.Circle))
                {
                    toggleDrawingButton_Click(null, null);
                }
            }
            else
            {
                if(selectedPointShapeAndPointIndices==null || Form.ModifierKeys == Keys.Control || emulateHoldControlCheckbox.Checked)
                {
#if _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
                    throw new NotImplementedException("Need to implement the selection logic for multiselection, edge selection, and selecting a previous shape in order to change it's color still"); //just got reminded that innerexceptions exist, they're nice. Keep in mind for future, will help somewhere in nested exception-ing.
                    // neeed to make selectedPointShapeAndPointIndices a list of tuples or points.
                    int currentSelectedPointsCount = 1;
                    if(selectedPointShapeAndPointIndices!=null)
                    {
                        currentSelectedPointsCount=selectedPointShapeAndPointIndices.Count();
                        ...
                    }
#endif
                    int detPixRadius = 6;
                    for (int i = shapes.Count - 1; i >= 0 && selectedPointShapeAndPointIndices==null; i--)//the following two loops select the latest placed point which was in click radius.
                    {
                        for (int j = shapes[i].vertices.Count - 1; j >= 0 && selectedPointShapeAndPointIndices == null; j--)
                        {
                            //if ((1 / MathUtil.FastInverseSqRt((float)Math.Pow((mE.X - shapes[i].points[j].Item1), 2) + (float)Math.Pow((mE.Y - shapes[i].points[j].Item2), 2))) <= detPixRadius)
                            if (Math.Sqrt((float)Math.Pow((mE.X - shapes[i].vertices[j].X), 2) + (float)Math.Pow((mE.Y - shapes[i].vertices[j].Y), 2)) <= detPixRadius)
                            {
                                drawingCanvasPictureBox.Cursor = Cursors.Hand;
                                selectedPointShapeAndPointIndices = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }

                }
                else
                {
                    resetAllShapes();
                    drawingCanvasPictureBox.Cursor = Cursors.Default;
                    int i = selectedPointShapeAndPointIndices.Item1; int j = selectedPointShapeAndPointIndices.Item2;
                    selectedPointShapeAndPointIndices = null;
                    shapes[i].vertices[j] = new Point(mE.X, mE.Y);
                    drawAllShapes(drawPoints);
                }
            }
        }

        private void resetAllShapes()
        {
            drawingCanvasPictureBox.Image = DrawFilledRectangle(drawingCanvasPictureBox.Image.Width, drawingCanvasPictureBox.Image.Height);
        }
        private void drawAllShapes(bool drawPoints)
        {
            if (superSamplingEnabled)
            {
                resetAllShapes();
                WriteableBitmap downSampled = null;

                Size superSampledSize = new Size(2 * drawingCanvasPictureBox.Image.Width, 2 * drawingCanvasPictureBox.Image.Height);
                Bitmap superSampledImage = new Bitmap(drawingCanvasPictureBox.Image, superSampledSize);
                foreach (Shape shp in shapes)
                {
                    shp.thickness = shp.thickness * 2;
                    for (int i=0;i<shp.vertices.Count();i++)
                    {
                        shp.vertices[i] = new Point(2 * shp.vertices[i].X, 2 * shp.vertices[i].Y);
                    }
                    superSampledImage = ImgUtil.GetBitmapFromWriteableBitmap(shp.draw(ImgUtil.GetWritableBitmapFromBitmap(superSampledImage), drawPoints));
                    shp.thickness = shp.thickness / 2;
                    for (int i = 0; i < shp.vertices.Count(); i++)
                    {
                        shp.vertices[i] = new Point(shp.vertices[i].X / 2, shp.vertices[i].Y / 2);
                    }
                }
                downSampled = downSampling2x(ImgUtil.GetWritableBitmapFromBitmap(superSampledImage));
                drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(downSampled);
                if (downSampled != null)
                    drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(downSampled);
                return;
            }
            foreach (Shape shp in shapes)
            {
                if (guptaSproulAntiAliasingEnabled && (shp.isShapeType(SupportedShapes.Line)))
                {
                    drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(shp.drawGSAA(ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image)), drawPoints));
                }
                else
                {
                    drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(shp.draw(ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image)), drawPoints));

                }
            }
        }

        private WriteableBitmap downSampling2x(WriteableBitmap upsampledWbmp)//,int scalingFactor=2
        {
            Size resultSize = new Size(upsampledWbmp.PixelWidth/2, upsampledWbmp.PixelHeight/2);
            //Bitmap tempBMP = new Bitmap(drawingCanvasPictureBox.Image, resultSize);
            Bitmap tempBMP = new Bitmap(drawingCanvasPictureBox.Image,resultSize);

            WriteableBitmap result = ImgUtil.GetWritableBitmapFromBitmap(tempBMP);
            for (int r=0;r<upsampledWbmp.PixelHeight;r+=2)
            {
                for(int c=0;c<upsampledWbmp.PixelWidth;c+=2)
                {
                    unsafe
                    {
                        int redAvg = 0; int blueAvg = 0; int greenAvg = 0;
                        for(int i=r;i<=r+1;i++)
                        {
                            for(int j=c;j<=c+1;j++)
                            {
                                _pixel_bgr24_bgra32* ptrPx = (_pixel_bgr24_bgra32*)upsampledWbmp.GetPixelIntPtrAt(i, j);
                                blueAvg += ptrPx->blue;
                                greenAvg += ptrPx->green;
                                redAvg += ptrPx->red;
                            }
                        }
                        blueAvg = blueAvg / 4;
                        greenAvg = greenAvg / 4;
                        redAvg = redAvg / 4;

                        _pixel_bgr24_bgra32* ptrResPx = (_pixel_bgr24_bgra32*)result.GetPixelIntPtrAt(r/2,c/2);
                        ptrResPx->blue = (byte)blueAvg;
                        ptrResPx->green = (byte)greenAvg;
                        ptrResPx->red = (byte)redAvg;
                    }
                }
            }
            return result;
        }

        private void lineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (lineRadioButton.Checked)
                selectedShapeType = SupportedShapes.Line;
        }

        private void polygonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (polygonRadioButton.Checked)
            {
                selectedShapeType = SupportedShapes.Polygon;
            }
        }

        private void circleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (circleRadioButton.Checked)
                selectedShapeType = SupportedShapes.Circle;
        }

        private void toggleDrawingButton_Click(object sender, EventArgs e)
        {
            drawingEnabled = !drawingEnabled;
            if(drawingEnabled)
            {
                Shape shp = Shape.ConstructRequiredShape(selectedShapeType);
                this.shapes.Add(shp);
                shapes.Last().thickness = (int)thicknessNumericUpDown.Value ;
                drawingCanvasPictureBox.Cursor = Cursors.Cross;
                toggleDrawingButton.Text = "Stop Drawing";
            }
            else
            {
                drawingCanvasPictureBox.Cursor = Cursors.Default;
                toggleDrawingButton.Text = "Start Drawing";
            }
        }

        private void colorPickerButton_Click(object sender, EventArgs e)
        {
            if(shapes.Count==0)
            {
                MessageBox.Show("Start drawing a shape before choosing color.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (drawingColorPicker.ShowDialog() == DialogResult.OK)
            {
                resetAllShapes();
                shapes.Last().color = drawingColorPicker.Color;
                drawAllShapes(drawPoints);
            }
            else
            {
                MessageBox.Show("No color selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void thicknessNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (shapes.Count > 0)
            {
                resetAllShapes();
                shapes.Last().thickness = (int)thicknessNumericUpDown.Value;
                drawAllShapes(drawPoints);
            }
            //else
            //{
            //    thicknessNumericUpDown.Value = 1;
            //    MessageBox.Show("Start drawing a shape before choosing thickness.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void deleteSelectedShapeButton_Click(object sender, EventArgs e)
        {
            if (selectedPointShapeAndPointIndices!=null)
            {
                resetAllShapes();
                drawingCanvasPictureBox.Cursor = Cursors.Default;
                shapes.RemoveAt(selectedPointShapeAndPointIndices.Item1);
                selectedPointShapeAndPointIndices = null;
                drawAllShapes(drawPoints);
            }
            else
            {
                MessageBox.Show("No shape selected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void antiAliasingGuptaSproulCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            resetAllShapes();
            guptaSproulAntiAliasingEnabled = antiAliasingGuptaSproulCheckBox.Checked;
            drawAllShapes(drawPoints);
        }

        private void cleanShapesList()
        {
            if (drawingEnabled)
                toggleDrawingButton_Click(null, null);

            List<Shape> cleanedShapesList = new List<Shape>();
            foreach (var shp in shapes)
            {
                if ((shp.isShapeType(SupportedShapes.Line) && shp.vertices.Count < 2)
                    || (shp.isShapeType(SupportedShapes.Polygon) && shp.vertices.Count < 3)
                    || (shp.isShapeType(SupportedShapes.Circle) && shp.vertices.Count != 2))
                {
                    continue;
                }
                cleanedShapesList.Add(shp);
            }
            shapes = cleanedShapesList;
        }

        private void saveAllShapesButton_Click(object sender, EventArgs e)
        {
            if (drawingEnabled)
                toggleDrawingButton_Click(null, null);
            cleanShapesList();
            shapes.SerializeToXML("kek.xml");
        }

        private void loadNewShapesButton_Click(object sender, EventArgs e)
        {
            if (drawingEnabled)
                toggleDrawingButton_Click(null, null);
            resetAllShapes();
            shapes.Deserialize("kek.xml");
            drawAllShapes(drawPoints);
        }

        private void superSamplingAntiAliasingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            superSamplingEnabled = superSamplingAntiAliasingCheckBox.Checked;
            resetAllShapes();
            drawAllShapes(drawPoints);
        }
        #endregion

        private void labsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labsTabControl.SelectedTab == lab3TabPage) //can add ||s to let other pages behave the same way.
            {
                if (imagesTabControl.SelectedTab != drawingViewTabPage)
                {
                    imagesTabControl.SelectedTab = drawingViewTabPage;
                }
            }
            else
            {
                if (imagesTabControl.SelectedTab == drawingViewTabPage)
                {
                    imagesTabControl.SelectedTab = comparisontViewTabPage;
                }
            }
        }
        private void imagesTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(imagesTabControl.SelectedTab==drawingViewTabPage)
            {
                if(labsTabControl.SelectedTab!=lab3TabPage) //can add ||s to let accomadate any other labs that might use the same.
                {
                    labsTabControl.SelectedTab = lab3TabPage;
                }
            }
            else
            {
                if(labsTabControl.SelectedTab==lab3TabPage) //can add ||s to let accomadate any other labs that might not use the view like lab3TabPage here.
                {
                    labsTabControl.SelectedTab = lab1TabPage;
                }

            }
        }

        //private void labsTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (labsTabControl.SelectedTab == lab3TabPage && imagesTabControl.SelectedTab!=drawingViewTabPage)
        //    {
        //        imagesTabControl.SelectedTab = drawingViewTabPage;
        //    }
        //}

        //private void labsTabControl_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (labsTabControl.SelectedTab == lab3TabPage && imagesTabControl.SelectedTab == drawingViewTabPage)
        //        imagesTabControl.SelectedTab = comparisontViewTabPage;
        //}

        //private void imagesTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (imagesTabControl.SelectedTab == drawingViewTabPage && labsTabControl.SelectedTab!=lab3TabPage)
        //        labsTabControl.SelectedTab = lab3TabPage;
        //}

        //private void imagesTabControl_Deselecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (imagesTabControl.SelectedTab == drawingViewTabPage && labsTabControl.SelectedTab == lab3TabPage )
        //        labsTabControl.SelectedTab = lab1TabPage;
        //}
    }
}
