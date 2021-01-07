using Symplex.Properties;

namespace Lab_1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.addSystemButton = new System.Windows.Forms.Button();
            this.deleteFormulaButton = new System.Windows.Forms.Button();
            this.addFormulaButton = new System.Windows.Forms.Button();
            this.deleteSystemButton = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.labelParsingResult = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTip_Save = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_AddRow = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_RemoveRow = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_AddColumn = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_RemoveColumn = new System.Windows.Forms.ToolTip(this.components);
            this.systemGroupBox = new System.Windows.Forms.GroupBox();
            this.formulaGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButton_Max = new System.Windows.Forms.RadioButton();
            this.radioButton_Min = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addSystemButton
            // 
            this.addSystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addSystemButton.Location = new System.Drawing.Point(295, 221);
            this.addSystemButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.addSystemButton.Name = "addSystemButton";
            this.addSystemButton.Size = new System.Drawing.Size(27, 27);
            this.addSystemButton.TabIndex = 0;
            this.addSystemButton.Text = "+";
            this.toolTip_AddRow.SetToolTip(this.addSystemButton, "Добавить строку");
            this.addSystemButton.UseVisualStyleBackColor = true;
            this.addSystemButton.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // deleteFormulaButton
            // 
            this.deleteFormulaButton.Location = new System.Drawing.Point(12, 32);
            this.deleteFormulaButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.deleteFormulaButton.Name = "deleteFormulaButton";
            this.deleteFormulaButton.Size = new System.Drawing.Size(27, 27);
            this.deleteFormulaButton.TabIndex = 1;
            this.deleteFormulaButton.Text = "-";
            this.toolTip_RemoveColumn.SetToolTip(this.deleteFormulaButton, "Удалить колонку");
            this.deleteFormulaButton.UseVisualStyleBackColor = true;
            this.deleteFormulaButton.Click += new System.EventHandler(this.buttonDeleteColumn_Click);
            // 
            // addFormulaButton
            // 
            this.addFormulaButton.Location = new System.Drawing.Point(39, 32);
            this.addFormulaButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.addFormulaButton.Name = "addFormulaButton";
            this.addFormulaButton.Size = new System.Drawing.Size(27, 27);
            this.addFormulaButton.TabIndex = 2;
            this.addFormulaButton.Text = "+";
            this.toolTip_AddColumn.SetToolTip(this.addFormulaButton, "Добавить колонку");
            this.addFormulaButton.UseVisualStyleBackColor = true;
            this.addFormulaButton.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // deleteSystemButton
            // 
            this.deleteSystemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSystemButton.Location = new System.Drawing.Point(295, 194);
            this.deleteSystemButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.deleteSystemButton.Name = "deleteSystemButton";
            this.deleteSystemButton.Size = new System.Drawing.Size(27, 27);
            this.deleteSystemButton.TabIndex = 4;
            this.deleteSystemButton.Text = "-";
            this.toolTip_RemoveRow.SetToolTip(this.deleteSystemButton, "Удалить строку");
            this.deleteSystemButton.UseVisualStyleBackColor = true;
            this.deleteSystemButton.Click += new System.EventHandler(this.buttonDeleteRow_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSolve.Location = new System.Drawing.Point(247, 32);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(75, 27);
            this.buttonSolve.TabIndex = 3;
            this.buttonSolve.Text = "Посчитать";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // labelParsingResult
            // 
            this.labelParsingResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelParsingResult.Location = new System.Drawing.Point(12, 62);
            this.labelParsingResult.Name = "labelParsingResult";
            this.labelParsingResult.Size = new System.Drawing.Size(276, 49);
            this.labelParsingResult.TabIndex = 6;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Enabled = false;
            this.buttonSave.Image = global::Symplex.Properties.Resources.SaveFloppyIcon;
            this.buttonSave.Location = new System.Drawing.Point(295, 84);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(27, 27);
            this.buttonSave.TabIndex = 5;
            this.toolTip_Save.SetToolTip(this.buttonSave, "Сохранить результаты расчётов в текстовый файл");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // systemGroupBox
            // 
            this.systemGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemGroupBox.Location = new System.Drawing.Point(12, 189);
            this.systemGroupBox.Name = "systemGroupBox";
            this.systemGroupBox.Size = new System.Drawing.Size(276, 100);
            this.systemGroupBox.TabIndex = 11;
            this.systemGroupBox.TabStop = false;
            this.systemGroupBox.Text = "Коэфициенты перед \'x\' в системе:";
            // 
            // formulaGroupBox
            // 
            this.formulaGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formulaGroupBox.Location = new System.Drawing.Point(12, 114);
            this.formulaGroupBox.Name = "formulaGroupBox";
            this.formulaGroupBox.Size = new System.Drawing.Size(276, 69);
            this.formulaGroupBox.TabIndex = 10;
            this.formulaGroupBox.TabStop = false;
            this.formulaGroupBox.Text = "Коэфициенты перед \'x\' в формуле:";
            // 
            // radioButton_Max
            // 
            this.radioButton_Max.AutoSize = true;
            this.radioButton_Max.Checked = true;
            this.radioButton_Max.Location = new System.Drawing.Point(3, 5);
            this.radioButton_Max.Name = "radioButton_Max";
            this.radioButton_Max.Size = new System.Drawing.Size(79, 17);
            this.radioButton_Max.TabIndex = 12;
            this.radioButton_Max.TabStop = true;
            this.radioButton_Max.Text = "Максимум";
            this.radioButton_Max.UseVisualStyleBackColor = true;
            this.radioButton_Max.CheckedChanged += new System.EventHandler(this.radioButton_Max_CheckedChanged);
            // 
            // radioButton_Min
            // 
            this.radioButton_Min.AutoSize = true;
            this.radioButton_Min.Location = new System.Drawing.Point(93, 5);
            this.radioButton_Min.Name = "radioButton_Min";
            this.radioButton_Min.Size = new System.Drawing.Size(73, 17);
            this.radioButton_Min.TabIndex = 13;
            this.radioButton_Min.Text = "Минимум";
            this.radioButton_Min.UseVisualStyleBackColor = true;
            this.radioButton_Min.CheckedChanged += new System.EventHandler(this.radioButton_Min_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_Min);
            this.panel1.Controls.Add(this.radioButton_Max);
            this.panel1.Location = new System.Drawing.Point(72, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 27);
            this.panel1.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 15;
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutProgramToolStripMenuItem.Text = "О программе";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.addProgramToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 301);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.systemGroupBox);
            this.Controls.Add(this.labelParsingResult);
            this.Controls.Add(this.formulaGroupBox);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.deleteSystemButton);
            this.Controls.Add(this.addFormulaButton);
            this.Controls.Add(this.deleteFormulaButton);
            this.Controls.Add(this.addSystemButton);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 340);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Симплекс-таблицы";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addSystemButton;
        private System.Windows.Forms.Button deleteFormulaButton;
        private System.Windows.Forms.Button addFormulaButton;
        private System.Windows.Forms.Button deleteSystemButton;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelParsingResult;
        private System.Windows.Forms.ToolTip toolTip_Save;
        private System.Windows.Forms.ToolTip toolTip_AddRow;
        private System.Windows.Forms.ToolTip toolTip_RemoveRow;
        private System.Windows.Forms.ToolTip toolTip_AddColumn;
        private System.Windows.Forms.ToolTip toolTip_RemoveColumn;
        private System.Windows.Forms.GroupBox systemGroupBox;
        private System.Windows.Forms.GroupBox formulaGroupBox;
        private System.Windows.Forms.RadioButton radioButton_Max;
        private System.Windows.Forms.RadioButton radioButton_Min;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
    }
}

