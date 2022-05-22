//#define _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
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
using Computer_Graphics_1.Lab4;

namespace Computer_Graphics_1
{
    public partial class MainForm : Form
    {
        private Color selectedFillColor = Color.MediumVioletRed;
        private Bitmap selectedFillPattern = Computer_Graphics_1.Properties.Resources.chessPatternImperfectCrop;
        private Bitmap ogBitmap = null;
        private WriteableBitmap wBmpToEdit = null;

        private bool drawingEnabled = false;
        private SupportedShapes selectedShapeType = SupportedShapes.Line;

        private Tuple<int, List<int>> selectedPointsShapeAndPointIndices = null;

        List<Shape> shapes = new List<Shape>();
        //List<ClippingPolygon> clippingPolygons = new List<ClippingPolygon>();

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
            drawingCanvasPictureBox.Image = DrawFilledRectangle(drawingCanvasPictureBox.Width, drawingCanvasPictureBox.Height);
            if (ogPictureBox.Image!=null)
            {
                setApplicationInteractionEnabled(true);
                zoomToolStripMenuItem_Click(null, null);
            }

#if Lab3
            if (selectedShapeType==SupportedShapes.Line)
                lineRadioButton.Checked = true;
            labsTabControl.SelectedTab = lab3TabPage;
            imagesTabControl.SelectedTab = drawingViewTabPage;
#endif

            //selectedFillPictureBox.Image = selectedFillPattern;
            selectedFillPictureBox.BackColor = selectedFillColor;
            selectedFillPictureBox.Image = null;

            ////labsTabControl.SelectedTab = lab2TabPage;
            ////labsTabControl.SelectedTab = lab3TabPage; imagesTabControl.SelectedTab = drawingViewTabPage;

            ////foreach (Control cntrl in lab1TabPage.Controls)
            ////{
            ////    cntrl.Enabled = false;
            ////}
            ////ogPictureBox.Image=
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
        int indexLastSelectedShape = -1;
        private void drawingCanvasPictureBox_Click(object sender, EventArgs e)
        {

            MouseEventArgs mE = (MouseEventArgs)e;
            bool enableClippingButton = false;

            if (drawingEnabled)
            {
                resetAllShapes();
                selectedPointsShapeAndPointIndices = null;
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
                if (selectedPointsShapeAndPointIndices == null || Form.ModifierKeys == Keys.Control || emulateHoldControlCheckbox.Checked || emulateHoldingControlAltCheckbox.Checked)
                {
#if _ENABLE_LAB3_MULTISELECT_EDGESELECT_CHANGEANYSHAPECOLORTHICKNESS
                    throw new NotImplementedException("Need to implement the selection logic for multiselection, edge selection, and selecting a previous shape in order to change it's color still"); //just got reminded that innerexceptions exist, they're nice. Keep in mind for future, will help somewhere in nested exception-ing.
                     neeed to make selectedPointShapeAndPointIndices a list of tuples or points.
                    int currentSelectedPointsCount = 1;
                    if(selectedPointShapeAndPointIndices!=null)
                    {
                        currentSelectedPointsCount=selectedPointShapeAndPointIndices.Count();
                        ...
                    }
#endif
                    int detPixRadius = 6;
                    bool gotPoint = false;
                    for (int i = shapes.Count - 1; i >= 0; i--)//the following two loops select the latest placed point which was in click radius.
                    {
                        for (int j = shapes[i].vertices.Count - 1; j >= 0; j--)
                        {
                            //if ((1 / MathUtil.FastInverseSqRt((float)Math.Pow((mE.X - shapes[i].points[j].Item1), 2) + (float)Math.Pow((mE.Y - shapes[i].points[j].Item2), 2))) <= detPixRadius)
                            if (Math.Sqrt((float)Math.Pow((mE.X - shapes[i].vertices[j].X), 2) + (float)Math.Pow((mE.Y - shapes[i].vertices[j].Y), 2)) <= detPixRadius)
                            {
                                if (selectedPointsShapeAndPointIndices == null)
                                {
                                    selectedPointsShapeAndPointIndices = new Tuple<int, List<int>>(i, new List<int>());
                                    drawingCanvasPictureBox.Cursor = Cursors.Hand;
                                }
                                else
                                {
                                    indexLastSelectedShape = selectedPointsShapeAndPointIndices.Item1;
                                    selectedPointsShapeAndPointIndices = new Tuple<int, List<int>>(i, new List<int>());
                                    drawingCanvasPictureBox.Cursor = Cursors.Hand;
                                }
                                selectedPointsShapeAndPointIndices.Item2.Add(j);
                                gotPoint = true;
                                break;
                            }
                        }
                        if (gotPoint)
                            break;
                    }
                    if (gotPoint)
                    {

                        Shape lastSelectedShape = null;
                        if(indexLastSelectedShape!=-1)
                        {
                            lastSelectedShape = shapes[indexLastSelectedShape];
                        }
                        Shape currentSelectedShape = shapes[selectedPointsShapeAndPointIndices.Item1];
                        if (selectedPointsShapeAndPointIndices == null)
                        {
                            lastSelectedShape = null;
                            indexLastSelectedShape = -1;
                        }
                        if (lastSelectedShape != null) //this is dependent on the if above.
                        {
                            currentSelectedShape = shapes[selectedPointsShapeAndPointIndices.Item1];
                            if (lastSelectedShape.isShapeType(SupportedShapes.Polygon) && currentSelectedShape.isShapeType(SupportedShapes.Polygon))
                            {
                                if ((currentSelectedShape as Polygon).isConvex())
                                {
                                    enableClippingButton = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        indexLastSelectedShape = -1;
                        drawingCanvasPictureBox.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    indexLastSelectedShape = -1;
                    resetAllShapes();
                    drawingCanvasPictureBox.Cursor = Cursors.Default;
                    int i = selectedPointsShapeAndPointIndices.Item1;
                    int j = selectedPointsShapeAndPointIndices.Item2[0];

                    //we move according to the change in the first clicked point!
                    int deltaX = mE.X - shapes[i].vertices[j].X;
                    int deltaY = mE.Y - shapes[i].vertices[j].Y;
                    shapes[i].vertices[j] = new Point(mE.X, mE.Y);
                    //bool firstRun = true;//to skip the first vertice selected because we already moved it.
                    foreach (int k in selectedPointsShapeAndPointIndices.Item2)
                    {
                        //if(firstRun)
                        //{
                        //    firstRun = false;
                        //    continue;
                        //}
                        if (k == j)
                            continue;
                        int currX = shapes[i].vertices[k].X;
                        int currY = shapes[i].vertices[k].Y;
                        shapes[i].vertices[k] = new Point(currX + deltaX, currY + deltaY);
                    }
                    selectedPointsShapeAndPointIndices = null;
                    drawAllShapes(drawPoints);
                }
            }

            enableOrDisableClippingButton(enableClippingButton);
        }

        private void enableOrDisableClippingButton(bool enableClippingButton)
        {
            if (enableClippingButton)
            {
                clipButton.Enabled = true;
                clipButton.ForeColor = Color.Black;
            }
            else
            {
                clipButton.Enabled = false;
                clipButton.ForeColor = Color.Gray;
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
            //if(clippingEnabled)
            //{
            //    toggleDrawClippingPolygon(null, null);
            //}
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
            if (selectedPointsShapeAndPointIndices!=null)
            {
                resetAllShapes();
                drawingCanvasPictureBox.Cursor = Cursors.Default;
                shapes.RemoveAt(selectedPointsShapeAndPointIndices.Item1);
                selectedPointsShapeAndPointIndices = null;
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
            if (labsTabControl.SelectedTab == lab3TabPage || labsTabControl.SelectedTab==lab4TabPage) //can add ||s to let other pages behave the same way.
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
                if(labsTabControl.SelectedTab!=lab3TabPage || labsTabControl.SelectedTab!=lab4TabPage) //can add ||s to let accomadate any other labs that might use the same.
                {
                    labsTabControl.SelectedTab = lab3TabPage;
                }
            }
            else
            {
                if(labsTabControl.SelectedTab==lab3TabPage||labsTabControl.SelectedTab==lab4TabPage) //can add ||s to let accomadate any other labs that might not use the view like lab3TabPage here.
                {
                    labsTabControl.SelectedTab = lab1TabPage;
                }

            }
        }


        //bool clippingEnabled = false;
        //Polygon shpToEdit = null;
        //int indxShpToEdit = -1;
        //private void clipPolygons(object sender, EventArgs e)
        //{
        //    if(clippingEnabled)
        //    {
        //        drawingEnabled = false;
        //        clippingEnabled = false;
        //        //might need to disabled clipping again here.
        //        ClippingPolygon clipper = (ClippingPolygon)shapes.Last();
        //        if (!clipper.isShapeType(SupportedShapes.ClippingPolygon))
        //            throw new Exception("lool");
        //        //clipper.clip(wBmpToEdit, ref shpToEdit);
        //        //shapes[indxShpToEdit] = shpToEdit;
        //        shapes.Count();
        //        WriteableBitmap canvasWbmp = ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image)); //probably inefficient.
        //        Polygon clippedShape = clipper.getClippedPolygon(canvasWbmp, shpToEdit);
        //        ref Color col = ref clippedShape.color;
        //        int red = 255 - col.R;
        //        int green = 255 - col.G;
        //        int blue = 255 - col.B;
        //        if (red > 230 && green > 230 && blue > 230)//to avoid having too light pixels on the white background.
        //        {
        //            red = 210;
        //            green = 230;
        //            blue = 220;
        //        }
        //        col = Color.FromArgb(red, green, blue);
        //        shapes.Add(clippedShape);
        //        shapes.Count();
        //        resetAllShapes();
        //        drawAllShapes(true);
        //        shapes.Count();
        //        //shpToEdit = null;
        //        lineRadioButton.Checked = true;
        //        //testButton.Text = "Test";
        //        return;
        //    }
        //    //MessageBox here: Select polygon/shape to be clipped first!
        //    //Actually, the button needs to be disabled unless the correct shape is selected.

        //    //if (drawingEnabled)
        //    //{
        //        //toggleDrawingButton_Click(null, null);
        //    //}
        //    clippingEnabled = true;
        //    if (shpToEdit == null)
        //    {
        //        shpToEdit = shapes[selectedPointsShapeAndPointIndices.Item1] as Polygon; //try catch to detect invalid shape?
        //        indxShpToEdit = selectedPointsShapeAndPointIndices.Item1;
        //    }
        //    polygonRadioButton.Checked = false;
        //    lineRadioButton.Checked = false;
        //    circleRadioButton.Checked = false;
        //    selectedShapeType = SupportedShapes.ClippingPolygon;

        //    toggleDrawingButton_Click(null, null);

        //    //testButton.Text = "stopTest";
        //}

        private void test2_Click(object sender, EventArgs e)
        {
            if (selectedPointsShapeAndPointIndices == null)
                return;
            //Polygon poly = shapes[selectedPointsShapeAndPointIndices.Item1] as Polygon;
            ////shift to calculate FillPolygon everytime or a similar solution.
            //PolygonFiller.FillPolygon(ref poly, Color.Red);
            //WriteableBitmap wb= ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image));
            //unsafe
            //{
            //    foreach (Point pix in poly.filledPixels)
            //    {
            //            wb.PutPixel(pix.X, pix.Y, poly.fillColor);
            //    }
            //}
            //drawingCanvasPictureBox.Image = ImgUtil.GetBitmapFromWriteableBitmap(wb);
            //shapes[selectedPointsShapeAndPointIndices.Item1] = poly;
            resetAllShapes();
            shapes[selectedPointsShapeAndPointIndices.Item1].fillPattern = Computer_Graphics_1.Properties.Resources._convFilterTest;
            drawAllShapes(true);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void clipButton_Click(object sender, EventArgs e)
        {
            enableOrDisableClippingButton(false);

            Shape lastSelectedShape = shapes[indexLastSelectedShape];
            Shape currentSelectedShape = shapes[selectedPointsShapeAndPointIndices.Item1];

            //validation is done when clicking\selecting shapes, so no further validation will be here (because the button is enabled only after validation and disabled after failing validation).
            ClippingPolygon clipper = new ClippingPolygon((Polygon)currentSelectedShape);
            Polygon toBeClipped = (Polygon)lastSelectedShape; //probably automatically assigned as a reference from my experience so far. That's why the above has "new" to call copy constructor instead.

            WriteableBitmap canvasWbmp = ImgUtil.GetWritableBitmapFromBitmap(new Bitmap(drawingCanvasPictureBox.Image)); //might be a little inefficient.

            Polygon clippedShape = clipper.getClippedPolygon(canvasWbmp, toBeClipped);

            clippedShape.color = pseudoClampedInvertColor(clippedShape.color);

            shapes.Add(clippedShape);

            resetAllShapes();
            drawAllShapes(true);

            lineRadioButton.Checked = true;
            return;
        }

        private static Color pseudoClampedInvertColor(Color col)
        {
            int red = 255 - col.R;
            int green = 255 - col.G;
            int blue = 255 - col.B;
            if (red > 200 && green > 200 && blue > 200)//to avoid having too light pixels on the white background.
            {
                red = 200;
                green = 180;
                blue = 190;
            }
            return Color.FromArgb(red, green, blue);
        }

        private void selectFillButton_Click(object sender, EventArgs e)
        {
            if(fillColorRadioButton.Checked)
            {
                if (drawingColorPicker.ShowDialog() == DialogResult.OK)
                {
                    selectedFillColor = drawingColorPicker.Color;
                    selectedFillPictureBox.BackColor = selectedFillColor;
                }
                else
                {
                    MessageBox.Show("No color selected. Retaining previous color.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if(fillPatternRadioButton.Checked) //could've just used 
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    openFileDialog.Filter = "Image files (*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|Bitmap image file (*.bmp)|*.bmp;|All files|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFillPattern = new Bitmap(openFileDialog.FileName);
                        selectedFillPictureBox.Image = selectedFillPattern;
                    }
                    else
                    {
                        MessageBox.Show("No image selected. Retaining previous image.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void fillColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fillColorRadioButton.Checked)
            {
                //int width = selectedFillPictureBox.Width;
                //int height = selectedFillPictureBox.Height;
                //selectedFillPictureBox.Image = new Bitmap(width,height);
                selectedFillPictureBox.Image = null;
                selectedFillPictureBox.BackColor = selectedFillColor;
            }
        }

        private void fillPatternRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fillPatternRadioButton.Checked)
            {
                selectedFillPictureBox.Image = selectedFillPattern;
                selectedFillPictureBox.BackColor = Color.Transparent;
            }
        }

        private void fillButton_Click(object sender, EventArgs e)
        {
            if (selectedPointsShapeAndPointIndices == null)
                return;

            resetAllShapes();

            ////testing empty color serialization: //conclusion: it works! No need to worry about serialization of shapes' colors. Well, I don't serialize pattern (I could've used file paths to serialize but I have more important stuff to do right now.)
            //int col = Color.Empty.ToArgb();
            //Color cololo = Color.FromArgb(col);

            if (fillColorRadioButton.Checked)
            {
                shapes[selectedPointsShapeAndPointIndices.Item1].fillColor = selectedFillColor;
                shapes[selectedPointsShapeAndPointIndices.Item1].fillPattern = null;
            }
            else if (fillPatternRadioButton.Checked)
            {
                shapes[selectedPointsShapeAndPointIndices.Item1].fillPattern = selectedFillPattern;
                shapes[selectedPointsShapeAndPointIndices.Item1].fillColor = Color.Empty;
            }

            drawAllShapes(true);
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
