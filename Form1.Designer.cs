﻿
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
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labsTabControl = new System.Windows.Forms.TabControl();
            this.lab1TabPage = new System.Windows.Forms.TabPage();
            this.lab2TabPage = new System.Windows.Forms.TabPage();
            this.lab3TabPage = new System.Windows.Forms.TabPage();
            this.lab4TabPage = new System.Windows.Forms.TabPage();
            this.lab5TabPage = new System.Windows.Forms.TabPage();
            this.imagesTabControl = new System.Windows.Forms.TabControl();
            this.comparisontViewTabPage = new System.Windows.Forms.TabPage();
            this.comparisonTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ogImgLabel = new System.Windows.Forms.Label();
            this.newImgLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.labsTabControl.SuspendLayout();
            this.imagesTabControl.SuspendLayout();
            this.comparisontViewTabPage.SuspendLayout();
            this.comparisonTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainTableLayoutPanel.Controls.Add(this.labsTabControl, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.imagesTabControl, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 30);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(800, 420);
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
            this.labsTabControl.Location = new System.Drawing.Point(563, 3);
            this.labsTabControl.Name = "labsTabControl";
            this.labsTabControl.SelectedIndex = 0;
            this.labsTabControl.Size = new System.Drawing.Size(234, 414);
            this.labsTabControl.TabIndex = 0;
            // 
            // lab1TabPage
            // 
            this.lab1TabPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lab1TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab1TabPage.Name = "lab1TabPage";
            this.lab1TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lab1TabPage.Size = new System.Drawing.Size(226, 385);
            this.lab1TabPage.TabIndex = 0;
            this.lab1TabPage.Text = "L1";
            // 
            // lab2TabPage
            // 
            this.lab2TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab2TabPage.Name = "lab2TabPage";
            this.lab2TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lab2TabPage.Size = new System.Drawing.Size(226, 385);
            this.lab2TabPage.TabIndex = 1;
            this.lab2TabPage.Text = "L2";
            this.lab2TabPage.UseVisualStyleBackColor = true;
            // 
            // lab3TabPage
            // 
            this.lab3TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab3TabPage.Name = "lab3TabPage";
            this.lab3TabPage.Size = new System.Drawing.Size(226, 385);
            this.lab3TabPage.TabIndex = 2;
            this.lab3TabPage.Text = "L3";
            this.lab3TabPage.UseVisualStyleBackColor = true;
            // 
            // lab4TabPage
            // 
            this.lab4TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab4TabPage.Name = "lab4TabPage";
            this.lab4TabPage.Size = new System.Drawing.Size(226, 385);
            this.lab4TabPage.TabIndex = 3;
            this.lab4TabPage.Text = "L4";
            this.lab4TabPage.UseVisualStyleBackColor = true;
            // 
            // lab5TabPage
            // 
            this.lab5TabPage.Location = new System.Drawing.Point(4, 25);
            this.lab5TabPage.Name = "lab5TabPage";
            this.lab5TabPage.Size = new System.Drawing.Size(226, 385);
            this.lab5TabPage.TabIndex = 4;
            this.lab5TabPage.Text = "L5";
            this.lab5TabPage.UseVisualStyleBackColor = true;
            // 
            // imagesTabControl
            // 
            this.imagesTabControl.Controls.Add(this.comparisontViewTabPage);
            this.imagesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesTabControl.Location = new System.Drawing.Point(3, 3);
            this.imagesTabControl.Name = "imagesTabControl";
            this.imagesTabControl.SelectedIndex = 0;
            this.imagesTabControl.Size = new System.Drawing.Size(554, 414);
            this.imagesTabControl.TabIndex = 1;
            // 
            // comparisontViewTabPage
            // 
            this.comparisontViewTabPage.Controls.Add(this.comparisonTableLayout);
            this.comparisontViewTabPage.Location = new System.Drawing.Point(4, 25);
            this.comparisontViewTabPage.Name = "comparisontViewTabPage";
            this.comparisontViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.comparisontViewTabPage.Size = new System.Drawing.Size(546, 385);
            this.comparisontViewTabPage.TabIndex = 0;
            this.comparisontViewTabPage.Text = "Comparison View";
            this.comparisontViewTabPage.UseVisualStyleBackColor = true;
            // 
            // comparisonTableLayout
            // 
            this.comparisonTableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.comparisonTableLayout.ColumnCount = 2;
            this.comparisonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.comparisonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.comparisonTableLayout.Controls.Add(this.ogImgLabel, 0, 0);
            this.comparisonTableLayout.Controls.Add(this.newImgLabel, 1, 0);
            this.comparisonTableLayout.Controls.Add(this.pictureBox1, 0, 1);
            this.comparisonTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparisonTableLayout.Location = new System.Drawing.Point(3, 3);
            this.comparisonTableLayout.Name = "comparisonTableLayout";
            this.comparisonTableLayout.RowCount = 2;
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.comparisonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.comparisonTableLayout.Size = new System.Drawing.Size(540, 379);
            this.comparisonTableLayout.TabIndex = 0;
            // 
            // ogImgLabel
            // 
            this.ogImgLabel.AutoSize = true;
            this.ogImgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ogImgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ogImgLabel.Location = new System.Drawing.Point(4, 1);
            this.ogImgLabel.Name = "ogImgLabel";
            this.ogImgLabel.Size = new System.Drawing.Size(262, 18);
            this.ogImgLabel.TabIndex = 1;
            this.ogImgLabel.Text = "Original Image";
            this.ogImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // newImgLabel
            // 
            this.newImgLabel.AutoSize = true;
            this.newImgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newImgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newImgLabel.Location = new System.Drawing.Point(273, 1);
            this.newImgLabel.Name = "newImgLabel";
            this.newImgLabel.Size = new System.Drawing.Size(263, 18);
            this.newImgLabel.TabIndex = 2;
            this.newImgLabel.Text = "After Processing:";
            this.newImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
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
            this.imagesTabControl.ResumeLayout(false);
            this.comparisontViewTabPage.ResumeLayout(false);
            this.comparisonTableLayout.ResumeLayout(false);
            this.comparisonTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

