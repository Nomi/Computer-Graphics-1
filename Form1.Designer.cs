
namespace Computer_Graphics_1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsResultpngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoAllProcessingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labsTabControl = new System.Windows.Forms.TabControl();
            this.lab1TabPage = new System.Windows.Forms.TabPage();
            this.convultionFiltersGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ApplyConvFiltButton = new System.Windows.Forms.Button();
            this.customizeConvFilterButton = new System.Windows.Forms.Button();
            this.embossRadioButton = new System.Windows.Forms.RadioButton();
            this.edgeDetRadioButton = new System.Windows.Forms.RadioButton();
            this.meanRemSharpRadioButton = new System.Windows.Forms.RadioButton();
            this.sharpenRadioButton = new System.Windows.Forms.RadioButton();
            this.gaussSmoothRadioButton = new System.Windows.Forms.RadioButton();
            this.blurRadioButton = new System.Windows.Forms.RadioButton();
            this.funcFilGroupBox = new System.Windows.Forms.GroupBox();
            this.gammaCorrectionButton = new System.Windows.Forms.Button();
            this.contrastEnhanceButton = new System.Windows.Forms.Button();
            this.brightnessCorrection = new System.Windows.Forms.Button();
            this.inversionCheckBox = new System.Windows.Forms.CheckBox();
            this.lab2TabPage = new System.Windows.Forms.TabPage();
            this.lab3TabPage = new System.Windows.Forms.TabPage();
            this.lab4TabPage = new System.Windows.Forms.TabPage();
            this.lab5TabPage = new System.Windows.Forms.TabPage();
            this.imagesTabControl = new System.Windows.Forms.TabControl();
            this.comparisontViewTabPage = new System.Windows.Forms.TabPage();
            this.comparisonTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.newImgLabel = new System.Windows.Forms.Label();
            this.ogImgLabel = new System.Windows.Forms.Label();
            this.ogPictureBox = new System.Windows.Forms.PictureBox();
            this.newPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.labsTabControl.SuspendLayout();
            this.lab1TabPage.SuspendLayout();
            this.convultionFiltersGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.funcFilGroupBox.SuspendLayout();
            this.imagesTabControl.SuspendLayout();
            this.comparisontViewTabPage.SuspendLayout();
            this.comparisonTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Maroon;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveAsResultpngToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 27);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // saveAsResultpngToolStripMenuItem
            // 
            this.saveAsResultpngToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAsResultpngToolStripMenuItem.Name = "saveAsResultpngToolStripMenuItem";
            this.saveAsResultpngToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.saveAsResultpngToolStripMenuItem.Text = "Save as result.png";
            this.saveAsResultpngToolStripMenuItem.Click += new System.EventHandler(this.saveAsResultpngToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoAllProcessingMenuItem});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 27);
            this.toolStripMenuItem1.Text = "Edit";
            // 
            // undoAllProcessingMenuItem
            // 
            this.undoAllProcessingMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undoAllProcessingMenuItem.Name = "undoAllProcessingMenuItem";
            this.undoAllProcessingMenuItem.Size = new System.Drawing.Size(232, 26);
            this.undoAllProcessingMenuItem.Text = "Undo ALL processing";
            this.undoAllProcessingMenuItem.Click += new System.EventHandler(this.undoAllProcessingMenuItem_Click);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.BackColor = System.Drawing.Color.Black;
            this.mainTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainTableLayoutPanel.Controls.Add(this.labsTabControl, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.imagesTabControl, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 31);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(800, 419);
            this.mainTableLayoutPanel.TabIndex = 1;
            // 
            // labsTabControl
            // 
            this.labsTabControl.Controls.Add(this.lab1TabPage);
            this.labsTabControl.Controls.Add(this.lab2TabPage);
            this.labsTabControl.Controls.Add(this.lab3TabPage);
            this.labsTabControl.Controls.Add(this.lab4TabPage);
            this.labsTabControl.Controls.Add(this.lab5TabPage);
            this.labsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labsTabControl.Enabled = false;
            this.labsTabControl.Location = new System.Drawing.Point(562, 6);
            this.labsTabControl.Name = "labsTabControl";
            this.labsTabControl.SelectedIndex = 0;
            this.labsTabControl.Size = new System.Drawing.Size(232, 404);
            this.labsTabControl.TabIndex = 0;
            // 
            // lab1TabPage
            // 
            this.lab1TabPage.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lab1TabPage.Controls.Add(this.convultionFiltersGroupBox);
            this.lab1TabPage.Controls.Add(this.funcFilGroupBox);
            this.lab1TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab1TabPage.Name = "lab1TabPage";
            this.lab1TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lab1TabPage.Size = new System.Drawing.Size(224, 375);
            this.lab1TabPage.TabIndex = 0;
            this.lab1TabPage.Text = "L1";
            // 
            // convultionFiltersGroupBox
            // 
            this.convultionFiltersGroupBox.AutoSize = true;
            this.convultionFiltersGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.convultionFiltersGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.convultionFiltersGroupBox.Controls.Add(this.embossRadioButton);
            this.convultionFiltersGroupBox.Controls.Add(this.edgeDetRadioButton);
            this.convultionFiltersGroupBox.Controls.Add(this.meanRemSharpRadioButton);
            this.convultionFiltersGroupBox.Controls.Add(this.sharpenRadioButton);
            this.convultionFiltersGroupBox.Controls.Add(this.gaussSmoothRadioButton);
            this.convultionFiltersGroupBox.Controls.Add(this.blurRadioButton);
            this.convultionFiltersGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.convultionFiltersGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convultionFiltersGroupBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.convultionFiltersGroupBox.Location = new System.Drawing.Point(3, 149);
            this.convultionFiltersGroupBox.Name = "convultionFiltersGroupBox";
            this.convultionFiltersGroupBox.Size = new System.Drawing.Size(218, 189);
            this.convultionFiltersGroupBox.TabIndex = 4;
            this.convultionFiltersGroupBox.TabStop = false;
            this.convultionFiltersGroupBox.Text = "Convultion Filters";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ApplyConvFiltButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customizeConvFilterButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 152);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(212, 34);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // ApplyConvFiltButton
            // 
            this.ApplyConvFiltButton.AutoSize = true;
            this.ApplyConvFiltButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ApplyConvFiltButton.ForeColor = System.Drawing.Color.Black;
            this.ApplyConvFiltButton.Location = new System.Drawing.Point(109, 3);
            this.ApplyConvFiltButton.Name = "ApplyConvFiltButton";
            this.ApplyConvFiltButton.Size = new System.Drawing.Size(100, 28);
            this.ApplyConvFiltButton.TabIndex = 26;
            this.ApplyConvFiltButton.Text = "Apply";
            this.ApplyConvFiltButton.UseVisualStyleBackColor = true;
            this.ApplyConvFiltButton.Click += new System.EventHandler(this.applyConvFiltButton_Click);
            // 
            // customizeConvFilterButton
            // 
            this.customizeConvFilterButton.AutoSize = true;
            this.customizeConvFilterButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.customizeConvFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customizeConvFilterButton.ForeColor = System.Drawing.Color.Black;
            this.customizeConvFilterButton.Location = new System.Drawing.Point(3, 3);
            this.customizeConvFilterButton.Name = "customizeConvFilterButton";
            this.customizeConvFilterButton.Size = new System.Drawing.Size(100, 28);
            this.customizeConvFilterButton.TabIndex = 25;
            this.customizeConvFilterButton.Text = "Customize Filter";
            this.customizeConvFilterButton.UseVisualStyleBackColor = true;
            // 
            // embossRadioButton
            // 
            this.embossRadioButton.AutoSize = true;
            this.embossRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.embossRadioButton.Location = new System.Drawing.Point(3, 130);
            this.embossRadioButton.Name = "embossRadioButton";
            this.embossRadioButton.Size = new System.Drawing.Size(212, 22);
            this.embossRadioButton.TabIndex = 5;
            this.embossRadioButton.TabStop = true;
            this.embossRadioButton.Text = "Emboss (East)";
            this.embossRadioButton.UseVisualStyleBackColor = true;
            this.embossRadioButton.CheckedChanged += new System.EventHandler(this.embossRadioButton_CheckedChanged);
            // 
            // edgeDetRadioButton
            // 
            this.edgeDetRadioButton.AutoSize = true;
            this.edgeDetRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.edgeDetRadioButton.Location = new System.Drawing.Point(3, 108);
            this.edgeDetRadioButton.Name = "edgeDetRadioButton";
            this.edgeDetRadioButton.Size = new System.Drawing.Size(212, 22);
            this.edgeDetRadioButton.TabIndex = 4;
            this.edgeDetRadioButton.TabStop = true;
            this.edgeDetRadioButton.Text = "Edge Detect (L->R)";
            this.edgeDetRadioButton.UseVisualStyleBackColor = true;
            this.edgeDetRadioButton.CheckedChanged += new System.EventHandler(this.edgeDetRadioButton_SelectIndexChanged);
            // 
            // meanRemSharpRadioButton
            // 
            this.meanRemSharpRadioButton.AutoSize = true;
            this.meanRemSharpRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.meanRemSharpRadioButton.Location = new System.Drawing.Point(3, 86);
            this.meanRemSharpRadioButton.Name = "meanRemSharpRadioButton";
            this.meanRemSharpRadioButton.Size = new System.Drawing.Size(212, 22);
            this.meanRemSharpRadioButton.TabIndex = 3;
            this.meanRemSharpRadioButton.TabStop = true;
            this.meanRemSharpRadioButton.Text = "Mean Removal Sharpen";
            this.meanRemSharpRadioButton.UseVisualStyleBackColor = true;
            this.meanRemSharpRadioButton.CheckedChanged += new System.EventHandler(this.meanRemSharpRadioButton_CheckedChanged);
            // 
            // sharpenRadioButton
            // 
            this.sharpenRadioButton.AutoSize = true;
            this.sharpenRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sharpenRadioButton.Location = new System.Drawing.Point(3, 64);
            this.sharpenRadioButton.Name = "sharpenRadioButton";
            this.sharpenRadioButton.Size = new System.Drawing.Size(212, 22);
            this.sharpenRadioButton.TabIndex = 2;
            this.sharpenRadioButton.TabStop = true;
            this.sharpenRadioButton.Text = "Sharpen";
            this.sharpenRadioButton.UseVisualStyleBackColor = true;
            this.sharpenRadioButton.CheckedChanged += new System.EventHandler(this.sharpenRadioButton_CheckedChanged);
            // 
            // gaussSmoothRadioButton
            // 
            this.gaussSmoothRadioButton.AutoSize = true;
            this.gaussSmoothRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.gaussSmoothRadioButton.Location = new System.Drawing.Point(3, 42);
            this.gaussSmoothRadioButton.Name = "gaussSmoothRadioButton";
            this.gaussSmoothRadioButton.Size = new System.Drawing.Size(212, 22);
            this.gaussSmoothRadioButton.TabIndex = 1;
            this.gaussSmoothRadioButton.TabStop = true;
            this.gaussSmoothRadioButton.Text = "Gaussian Smoothening";
            this.gaussSmoothRadioButton.UseVisualStyleBackColor = true;
            this.gaussSmoothRadioButton.CheckedChanged += new System.EventHandler(this.gaussSmoothRadioButton_CheckedChanged);
            // 
            // blurRadioButton
            // 
            this.blurRadioButton.AutoSize = true;
            this.blurRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.blurRadioButton.Location = new System.Drawing.Point(3, 20);
            this.blurRadioButton.Name = "blurRadioButton";
            this.blurRadioButton.Size = new System.Drawing.Size(212, 22);
            this.blurRadioButton.TabIndex = 0;
            this.blurRadioButton.TabStop = true;
            this.blurRadioButton.Text = "Blur";
            this.blurRadioButton.UseVisualStyleBackColor = true;
            this.blurRadioButton.CheckedChanged += new System.EventHandler(this.blurRadioButton_CheckedChanged);
            // 
            // funcFilGroupBox
            // 
            this.funcFilGroupBox.AutoSize = true;
            this.funcFilGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.funcFilGroupBox.Controls.Add(this.gammaCorrectionButton);
            this.funcFilGroupBox.Controls.Add(this.contrastEnhanceButton);
            this.funcFilGroupBox.Controls.Add(this.brightnessCorrection);
            this.funcFilGroupBox.Controls.Add(this.inversionCheckBox);
            this.funcFilGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.funcFilGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.funcFilGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.funcFilGroupBox.Location = new System.Drawing.Point(3, 3);
            this.funcFilGroupBox.Name = "funcFilGroupBox";
            this.funcFilGroupBox.Size = new System.Drawing.Size(218, 146);
            this.funcFilGroupBox.TabIndex = 2;
            this.funcFilGroupBox.TabStop = false;
            this.funcFilGroupBox.Text = "Functional Filters";
            // 
            // gammaCorrectionButton
            // 
            this.gammaCorrectionButton.AutoSize = true;
            this.gammaCorrectionButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.gammaCorrectionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gammaCorrectionButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gammaCorrectionButton.Location = new System.Drawing.Point(3, 110);
            this.gammaCorrectionButton.Name = "gammaCorrectionButton";
            this.gammaCorrectionButton.Size = new System.Drawing.Size(212, 33);
            this.gammaCorrectionButton.TabIndex = 7;
            this.gammaCorrectionButton.Text = "Gamma Correction";
            this.gammaCorrectionButton.UseVisualStyleBackColor = true;
            this.gammaCorrectionButton.Click += new System.EventHandler(this.gammaCorrectionButton_Click);
            // 
            // contrastEnhanceButton
            // 
            this.contrastEnhanceButton.AutoSize = true;
            this.contrastEnhanceButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.contrastEnhanceButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrastEnhanceButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.contrastEnhanceButton.Location = new System.Drawing.Point(3, 77);
            this.contrastEnhanceButton.Name = "contrastEnhanceButton";
            this.contrastEnhanceButton.Size = new System.Drawing.Size(212, 33);
            this.contrastEnhanceButton.TabIndex = 6;
            this.contrastEnhanceButton.Text = "Contrast Enhancement";
            this.contrastEnhanceButton.UseVisualStyleBackColor = true;
            this.contrastEnhanceButton.Click += new System.EventHandler(this.contrastEnhanceButton_Click);
            // 
            // brightnessCorrection
            // 
            this.brightnessCorrection.AutoSize = true;
            this.brightnessCorrection.Dock = System.Windows.Forms.DockStyle.Top;
            this.brightnessCorrection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightnessCorrection.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.brightnessCorrection.Location = new System.Drawing.Point(3, 44);
            this.brightnessCorrection.Name = "brightnessCorrection";
            this.brightnessCorrection.Size = new System.Drawing.Size(212, 33);
            this.brightnessCorrection.TabIndex = 5;
            this.brightnessCorrection.Text = "Brightness Correction";
            this.brightnessCorrection.UseVisualStyleBackColor = true;
            this.brightnessCorrection.Click += new System.EventHandler(this.brightnessCorrection_Click);
            // 
            // inversionCheckBox
            // 
            this.inversionCheckBox.AutoSize = true;
            this.inversionCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inversionCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inversionCheckBox.Location = new System.Drawing.Point(3, 20);
            this.inversionCheckBox.Name = "inversionCheckBox";
            this.inversionCheckBox.Size = new System.Drawing.Size(212, 24);
            this.inversionCheckBox.TabIndex = 4;
            this.inversionCheckBox.Text = "Invert Colors";
            this.inversionCheckBox.UseVisualStyleBackColor = true;
            this.inversionCheckBox.CheckedChanged += new System.EventHandler(this.inversionCheckBox_CheckedChanged);
            // 
            // lab2TabPage
            // 
            this.lab2TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab2TabPage.Name = "lab2TabPage";
            this.lab2TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lab2TabPage.Size = new System.Drawing.Size(224, 375);
            this.lab2TabPage.TabIndex = 1;
            this.lab2TabPage.Text = "L2";
            this.lab2TabPage.UseVisualStyleBackColor = true;
            // 
            // lab3TabPage
            // 
            this.lab3TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab3TabPage.Name = "lab3TabPage";
            this.lab3TabPage.Size = new System.Drawing.Size(224, 375);
            this.lab3TabPage.TabIndex = 2;
            this.lab3TabPage.Text = "L3";
            this.lab3TabPage.UseVisualStyleBackColor = true;
            // 
            // lab4TabPage
            // 
            this.lab4TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab4TabPage.Name = "lab4TabPage";
            this.lab4TabPage.Size = new System.Drawing.Size(224, 375);
            this.lab4TabPage.TabIndex = 3;
            this.lab4TabPage.Text = "L4";
            this.lab4TabPage.UseVisualStyleBackColor = true;
            // 
            // lab5TabPage
            // 
            this.lab5TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab5TabPage.Name = "lab5TabPage";
            this.lab5TabPage.Size = new System.Drawing.Size(224, 375);
            this.lab5TabPage.TabIndex = 4;
            this.lab5TabPage.Text = "L5";
            this.lab5TabPage.UseVisualStyleBackColor = true;
            // 
            // imagesTabControl
            // 
            this.imagesTabControl.Controls.Add(this.comparisontViewTabPage);
            this.imagesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesTabControl.Location = new System.Drawing.Point(6, 6);
            this.imagesTabControl.Name = "imagesTabControl";
            this.imagesTabControl.SelectedIndex = 0;
            this.imagesTabControl.Size = new System.Drawing.Size(547, 404);
            this.imagesTabControl.TabIndex = 1;
            // 
            // comparisontViewTabPage
            // 
            this.comparisontViewTabPage.Controls.Add(this.comparisonTableLayout);
            this.comparisontViewTabPage.Location = new System.Drawing.Point(4, 25);
            this.comparisontViewTabPage.Name = "comparisontViewTabPage";
            this.comparisontViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.comparisontViewTabPage.Size = new System.Drawing.Size(539, 375);
            this.comparisontViewTabPage.TabIndex = 0;
            this.comparisontViewTabPage.Text = "Comparison View";
            this.comparisontViewTabPage.UseVisualStyleBackColor = true;
            // 
            // comparisonTableLayout
            // 
            this.comparisonTableLayout.BackColor = System.Drawing.Color.Black;
            this.comparisonTableLayout.ColumnCount = 2;
            this.comparisonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.comparisonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.comparisonTableLayout.Controls.Add(this.newImgLabel, 0, 2);
            this.comparisonTableLayout.Controls.Add(this.ogImgLabel, 0, 0);
            this.comparisonTableLayout.Controls.Add(this.ogPictureBox, 0, 1);
            this.comparisonTableLayout.Controls.Add(this.newPictureBox, 0, 3);
            this.comparisonTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparisonTableLayout.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.comparisonTableLayout.Location = new System.Drawing.Point(3, 3);
            this.comparisonTableLayout.Name = "comparisonTableLayout";
            this.comparisonTableLayout.RowCount = 4;
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.comparisonTableLayout.Size = new System.Drawing.Size(533, 369);
            this.comparisonTableLayout.TabIndex = 0;
            // 
            // newImgLabel
            // 
            this.newImgLabel.AutoSize = true;
            this.newImgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newImgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newImgLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.newImgLabel.Location = new System.Drawing.Point(3, 184);
            this.newImgLabel.Name = "newImgLabel";
            this.newImgLabel.Size = new System.Drawing.Size(527, 18);
            this.newImgLabel.TabIndex = 3;
            this.newImgLabel.Text = "After Processing:";
            this.newImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ogImgLabel
            // 
            this.ogImgLabel.AutoSize = true;
            this.ogImgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogImgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ogImgLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ogImgLabel.Location = new System.Drawing.Point(3, 0);
            this.ogImgLabel.Name = "ogImgLabel";
            this.ogImgLabel.Size = new System.Drawing.Size(527, 18);
            this.ogImgLabel.TabIndex = 1;
            this.ogImgLabel.Text = "Original Image:";
            this.ogImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ogPictureBox
            // 
            this.ogPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogPictureBox.Location = new System.Drawing.Point(3, 21);
            this.ogPictureBox.Name = "ogPictureBox";
            this.ogPictureBox.Size = new System.Drawing.Size(527, 160);
            this.ogPictureBox.TabIndex = 4;
            this.ogPictureBox.TabStop = false;
            this.ogPictureBox.DoubleClick += new System.EventHandler(this.ogPictureBox_DoubleClick);
            // 
            // newPictureBox
            // 
            this.newPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPictureBox.Location = new System.Drawing.Point(3, 205);
            this.newPictureBox.Name = "newPictureBox";
            this.newPictureBox.Size = new System.Drawing.Size(527, 161);
            this.newPictureBox.TabIndex = 5;
            this.newPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A Dobby PhotoJoke";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.labsTabControl.ResumeLayout(false);
            this.lab1TabPage.ResumeLayout(false);
            this.lab1TabPage.PerformLayout();
            this.convultionFiltersGroupBox.ResumeLayout(false);
            this.convultionFiltersGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.funcFilGroupBox.ResumeLayout(false);
            this.funcFilGroupBox.PerformLayout();
            this.imagesTabControl.ResumeLayout(false);
            this.comparisontViewTabPage.ResumeLayout(false);
            this.comparisonTableLayout.ResumeLayout(false);
            this.comparisonTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ogPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TabControl labsTabControl;
        private System.Windows.Forms.TabPage lab1TabPage;
        private System.Windows.Forms.TabPage lab2TabPage;
        private System.Windows.Forms.TabPage lab3TabPage;
        private System.Windows.Forms.TabPage lab4TabPage;
        private System.Windows.Forms.TabPage lab5TabPage;
        private System.Windows.Forms.TabControl imagesTabControl;
        private System.Windows.Forms.TabPage comparisontViewTabPage;
        private System.Windows.Forms.TableLayoutPanel comparisonTableLayout;
        private System.Windows.Forms.Label ogImgLabel;
        private System.Windows.Forms.Label newImgLabel;
        private System.Windows.Forms.PictureBox ogPictureBox;
        private System.Windows.Forms.PictureBox newPictureBox;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.GroupBox funcFilGroupBox;
        private System.Windows.Forms.GroupBox convultionFiltersGroupBox;
        private System.Windows.Forms.CheckBox inversionCheckBox;
        private System.Windows.Forms.Button gammaCorrectionButton;
        private System.Windows.Forms.Button contrastEnhanceButton;
        private System.Windows.Forms.Button brightnessCorrection;
        private System.Windows.Forms.ToolStripMenuItem saveAsResultpngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoAllProcessingMenuItem;
        private System.Windows.Forms.RadioButton embossRadioButton;
        private System.Windows.Forms.RadioButton edgeDetRadioButton;
        private System.Windows.Forms.RadioButton meanRemSharpRadioButton;
        private System.Windows.Forms.RadioButton sharpenRadioButton;
        private System.Windows.Forms.RadioButton gaussSmoothRadioButton;
        private System.Windows.Forms.RadioButton blurRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ApplyConvFiltButton;
        private System.Windows.Forms.Button customizeConvFilterButton;
    }
}

