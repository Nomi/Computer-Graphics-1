
namespace Computer_Graphics_1
{
    partial class ConvFiltCustomizer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kernelDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.anchorCoordR = new System.Windows.Forms.MaskedTextBox();
            this.kernelDimCol = new System.Windows.Forms.MaskedTextBox();
            this.kernelDimRow = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.anchorCoordC = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.offsetMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.divisorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.divisorLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kernelDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisorNumericUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 355);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kernelDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kernel Coefficients";
            // 
            // kernelDataGridView
            // 
            this.kernelDataGridView.AllowUserToAddRows = false;
            this.kernelDataGridView.AllowUserToDeleteRows = false;
            this.kernelDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kernelDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.kernelDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kernelDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kernelDataGridView.ColumnHeadersVisible = false;
            this.kernelDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kernelDataGridView.Location = new System.Drawing.Point(3, 18);
            this.kernelDataGridView.Name = "kernelDataGridView";
            this.kernelDataGridView.RowHeadersVisible = false;
            this.kernelDataGridView.RowHeadersWidth = 51;
            this.kernelDataGridView.RowTemplate.Height = 24;
            this.kernelDataGridView.Size = new System.Drawing.Size(277, 150);
            this.kernelDataGridView.TabIndex = 0;
            this.kernelDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.kernelDataGridView_CellValidated);
            this.kernelDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.kernelDataGridView_CellValidating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 172);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kernel Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.anchorCoordR, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.kernelDimCol, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.kernelDimRow, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.anchorCoordC, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(277, 151);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // anchorCoordR
            // 
            this.anchorCoordR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.anchorCoordR.Location = new System.Drawing.Point(141, 101);
            this.anchorCoordR.Mask = "00000000000";
            this.anchorCoordR.Name = "anchorCoordR";
            this.anchorCoordR.Size = new System.Drawing.Size(60, 22);
            this.anchorCoordR.TabIndex = 9;
            this.anchorCoordR.ValidatingType = typeof(int);
            this.anchorCoordR.TextChanged += new System.EventHandler(this.anchorCoordR_TextChanged);
            // 
            // kernelDimCol
            // 
            this.kernelDimCol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kernelDimCol.Location = new System.Drawing.Point(210, 27);
            this.kernelDimCol.Mask = "00000000000";
            this.kernelDimCol.Name = "kernelDimCol";
            this.kernelDimCol.Size = new System.Drawing.Size(61, 22);
            this.kernelDimCol.TabIndex = 9;
            this.kernelDimCol.ValidatingType = typeof(int);
            this.kernelDimCol.TextChanged += new System.EventHandler(this.kernelDimCol_TextChanged);
            // 
            // kernelDimRow
            // 
            this.kernelDimRow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kernelDimRow.Location = new System.Drawing.Point(141, 27);
            this.kernelDimRow.Mask = "00000000000";
            this.kernelDimRow.Name = "kernelDimRow";
            this.kernelDimRow.Size = new System.Drawing.Size(60, 22);
            this.kernelDimRow.TabIndex = 8;
            this.kernelDimRow.ValidatingType = typeof(int);
            this.kernelDimRow.TextChanged += new System.EventHandler(this.kernelDimRow_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 71);
            this.label3.TabIndex = 2;
            this.label3.Text = "Anchor (row,col) [1-Indexed]";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 71);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kernel Dimensions: (Row x Cols)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // anchorCoordC
            // 
            this.anchorCoordC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.anchorCoordC.Location = new System.Drawing.Point(210, 101);
            this.anchorCoordC.Mask = "00000000000";
            this.anchorCoordC.Name = "anchorCoordC";
            this.anchorCoordC.Size = new System.Drawing.Size(61, 22);
            this.anchorCoordC.TabIndex = 9;
            this.anchorCoordC.ValidatingType = typeof(int);
            this.anchorCoordC.TextChanged += new System.EventHandler(this.anchorCoordC_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.offsetMaskedTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.divisorNumericUpDown);
            this.groupBox3.Controls.Add(this.divisorLabel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(292, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 171);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other Settings";
            // 
            // offsetMaskedTextBox
            // 
            this.offsetMaskedTextBox.Location = new System.Drawing.Point(91, 83);
            this.offsetMaskedTextBox.Mask = "00000000000";
            this.offsetMaskedTextBox.Name = "offsetMaskedTextBox";
            this.offsetMaskedTextBox.Size = new System.Drawing.Size(100, 22);
            this.offsetMaskedTextBox.TabIndex = 7;
            this.offsetMaskedTextBox.ValidatingType = typeof(int);
            this.offsetMaskedTextBox.TextChanged += new System.EventHandler(this.offsetMaskedTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Offset";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(18, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(225, 21);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Calculate Divisor automatically.";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // divisorNumericUpDown
            // 
            this.divisorNumericUpDown.DecimalPlaces = 3;
            this.divisorNumericUpDown.Enabled = false;
            this.divisorNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.divisorNumericUpDown.Location = new System.Drawing.Point(91, 28);
            this.divisorNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.divisorNumericUpDown.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.divisorNumericUpDown.Name = "divisorNumericUpDown";
            this.divisorNumericUpDown.Size = new System.Drawing.Size(120, 22);
            this.divisorNumericUpDown.TabIndex = 4;
            this.divisorNumericUpDown.ValueChanged += new System.EventHandler(this.divisorNumericUpDown_ValueChanged);
            // 
            // divisorLabel
            // 
            this.divisorLabel.AutoSize = true;
            this.divisorLabel.Location = new System.Drawing.Point(15, 28);
            this.divisorLabel.Name = "divisorLabel";
            this.divisorLabel.Size = new System.Drawing.Size(51, 17);
            this.divisorLabel.TabIndex = 1;
            this.divisorLabel.Text = "Divisor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.saveButton);
            this.groupBox4.Controls.Add(this.loadButton);
            this.groupBox4.Controls.Add(this.applyButton);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(292, 180);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 172);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Apply/Save/Load";
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveButton.Location = new System.Drawing.Point(3, 88);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(277, 27);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save Filter";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.AutoSize = true;
            this.loadButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadButton.Location = new System.Drawing.Point(3, 115);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(277, 27);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load Filter";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.AutoSize = true;
            this.applyButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.applyButton.Location = new System.Drawing.Point(3, 142);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(277, 27);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // ConvFiltCustomizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 355);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConvFiltCustomizer";
            this.Text = "ConvFiltCustomizer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kernelDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisorNumericUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView kernelDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label divisorLabel;
        private System.Windows.Forms.MaskedTextBox offsetMaskedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown divisorNumericUpDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MaskedTextBox kernelDimRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox anchorCoordR;
        private System.Windows.Forms.MaskedTextBox kernelDimCol;
        private System.Windows.Forms.MaskedTextBox anchorCoordC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button applyButton;
    }
}