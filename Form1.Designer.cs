
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
            this.undoAllProcessingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labsTabControl = new System.Windows.Forms.TabControl();
            this.lab1TabPage = new System.Windows.Forms.TabPage();
            this.convultionFiltersGroupBox = new System.Windows.Forms.GroupBox();
            this.blur9x9ConvFiltButton = new System.Windows.Forms.Button();
            this.funcFilGroupBox = new System.Windows.Forms.GroupBox();
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
            this.inversionCheckBox = new System.Windows.Forms.CheckBox();
            this.brightnessCorrection = new System.Windows.Forms.Button();
            this.contrastEnhanceButton = new System.Windows.Forms.Button();
            this.gammaCorrectionButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.labsTabControl.SuspendLayout();
            this.lab1TabPage.SuspendLayout();
            this.convultionFiltersGroupBox.SuspendLayout();
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
            this.fileToolStripMenuItem});
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
            this.undoAllProcessingMenuItem});
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
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // undoAllProcessingMenuItem
            // 
            this.undoAllProcessingMenuItem.Enabled = false;
            this.undoAllProcessingMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undoAllProcessingMenuItem.Name = "undoAllProcessingMenuItem";
            this.undoAllProcessingMenuItem.Size = new System.Drawing.Size(236, 26);
            this.undoAllProcessingMenuItem.Text = "Undo ALL Processing";
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
            this.convultionFiltersGroupBox.Controls.Add(this.blur9x9ConvFiltButton);
            this.convultionFiltersGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.convultionFiltersGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convultionFiltersGroupBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.convultionFiltersGroupBox.Location = new System.Drawing.Point(3, 149);
            this.convultionFiltersGroupBox.Name = "convultionFiltersGroupBox";
            this.convultionFiltersGroupBox.Size = new System.Drawing.Size(218, 100);
            this.convultionFiltersGroupBox.TabIndex = 4;
            this.convultionFiltersGroupBox.TabStop = false;
            this.convultionFiltersGroupBox.Text = "Convultion Filters";
            // 
            // blur9x9ConvFiltButton
            // 
            this.blur9x9ConvFiltButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.blur9x9ConvFiltButton.ForeColor = System.Drawing.Color.Black;
            this.blur9x9ConvFiltButton.Location = new System.Drawing.Point(3, 20);
            this.blur9x9ConvFiltButton.Name = "blur9x9ConvFiltButton";
            this.blur9x9ConvFiltButton.Size = new System.Drawing.Size(212, 23);
            this.blur9x9ConvFiltButton.TabIndex = 0;
            this.blur9x9ConvFiltButton.Text = "Blur (9x9 Conv Mat)";
            this.blur9x9ConvFiltButton.UseVisualStyleBackColor = true;
            this.blur9x9ConvFiltButton.Click += new System.EventHandler(this.button1_Click);
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
            this.ogPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            this.newPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.newPictureBox.TabIndex = 5;
            this.newPictureBox.TabStop = false;
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
            this.Text = "Computer Graphics 1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.labsTabControl.ResumeLayout(false);
            this.lab1TabPage.ResumeLayout(false);
            this.lab1TabPage.PerformLayout();
            this.convultionFiltersGroupBox.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem undoAllProcessingMenuItem;
        private System.Windows.Forms.Button blur9x9ConvFiltButton;
        private System.Windows.Forms.CheckBox inversionCheckBox;
        private System.Windows.Forms.Button gammaCorrectionButton;
        private System.Windows.Forms.Button contrastEnhanceButton;
        private System.Windows.Forms.Button brightnessCorrection;
    }
}

