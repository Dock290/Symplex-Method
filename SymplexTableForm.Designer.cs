namespace Lab_1
{
    partial class SymplexTableForm
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
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelCb = new System.Windows.Forms.Label();
            this.labelBasis = new System.Windows.Forms.Label();
            this.labelPivot = new System.Windows.Forms.Label();
            this.labelScores = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNumber
            // 
            this.labelNumber.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNumber.Location = new System.Drawing.Point(13, 13);
            this.labelNumber.Margin = new System.Windows.Forms.Padding(0);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(40, 30);
            this.labelNumber.TabIndex = 0;
            this.labelNumber.Text = "№";
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCb
            // 
            this.labelCb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelCb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCb.Location = new System.Drawing.Point(53, 13);
            this.labelCb.Margin = new System.Windows.Forms.Padding(0);
            this.labelCb.Name = "labelCb";
            this.labelCb.Size = new System.Drawing.Size(40, 30);
            this.labelCb.TabIndex = 1;
            this.labelCb.Text = "Св";
            this.labelCb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBasis
            // 
            this.labelBasis.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelBasis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBasis.Location = new System.Drawing.Point(93, 13);
            this.labelBasis.Margin = new System.Windows.Forms.Padding(0);
            this.labelBasis.Name = "labelBasis";
            this.labelBasis.Size = new System.Drawing.Size(40, 30);
            this.labelBasis.TabIndex = 2;
            this.labelBasis.Text = "Базис";
            this.labelBasis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPivot
            // 
            this.labelPivot.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelPivot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPivot.Location = new System.Drawing.Point(133, 13);
            this.labelPivot.Margin = new System.Windows.Forms.Padding(0);
            this.labelPivot.Name = "labelPivot";
            this.labelPivot.Size = new System.Drawing.Size(70, 30);
            this.labelPivot.TabIndex = 3;
            this.labelPivot.Text = "b (Опорное решение)";
            this.labelPivot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScores
            // 
            this.labelScores.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelScores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelScores.Location = new System.Drawing.Point(13, 192);
            this.labelScores.Margin = new System.Windows.Forms.Padding(0);
            this.labelScores.Name = "labelScores";
            this.labelScores.Size = new System.Drawing.Size(120, 60);
            this.labelScores.TabIndex = 4;
            this.labelScores.Text = "Оценки";
            this.labelScores.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SymplexTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelScores);
            this.Controls.Add(this.labelPivot);
            this.Controls.Add(this.labelBasis);
            this.Controls.Add(this.labelCb);
            this.Controls.Add(this.labelNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "SymplexTableForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Label labelCb;
        private System.Windows.Forms.Label labelBasis;
        private System.Windows.Forms.Label labelPivot;
        private System.Windows.Forms.Label labelScores;
    }
}