using System.Windows.Forms;

namespace Lab_1
{
    public partial class SymplexTableForm : Form
    {
        private const int CellWidth = 65;
        private const int CellHeight = 35;

        public SymplexTable SymplexTable { get; }

        public SymplexTableForm(SymplexTable symplexTable, int iteration) : base()
        {
            SymplexTable = symplexTable;

            InitializeComponent();

            Text = string.Format("[План: {0}]", iteration + 1);

            // Scores
            labelScores.Location = new System.Drawing.Point(labelNumber.Location.X,
                labelNumber.Location.Y + labelNumber.Height + CellHeight * symplexTable.Height);

            // F
            Controls.Add(new Label
            {
                BackColor = System.Drawing.SystemColors.ControlLightLight,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                Location = new System.Drawing.Point(labelScores.Location.X + labelScores.Width, labelScores.Location.Y),
                Width = labelPivot.Width,
                Height = labelScores.Height / 2,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "F",
            });

            // FC
            Controls.Add(new Label
            {
                BackColor = System.Drawing.SystemColors.ControlLightLight,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                Location = new System.Drawing.Point(labelScores.Location.X + labelScores.Width, labelScores.Location.Y + labelScores.Height / 2),
                Width = labelPivot.Width,
                Height = labelScores.Height / 2,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = symplexTable.F.Equals(double.NaN) ? "-" : System.Math.Round(symplexTable.F, 4).ToString("G5"),
            });

            // Q
            Controls.Add(new Label
            {
                BackColor = System.Drawing.SystemColors.ControlLightLight,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + symplexTable.Width * CellWidth, labelPivot.Location.Y),
                Width = CellWidth,
                Height = labelNumber.Height,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "Q",
            });


            for (int i = 0; i < symplexTable.Height; i++)
            {
                // Number
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelNumber.Location.X, labelNumber.Location.Y + labelNumber.Height + i * CellHeight),
                    Width = labelNumber.Width,
                    Height = CellHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = (i + 1).ToString(),
                });

                // Cb
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelCb.Location.X, labelCb.Location.Y + labelCb.Height + i * CellHeight),
                    Width = labelCb.Width,
                    Height = CellHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = System.Math.Round(symplexTable.Cb[i], 2).ToString("G3"),
                });

                // Basis
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelBasis.Location.X, labelBasis.Location.Y + labelBasis.Height + i * CellHeight),
                    Width = labelBasis.Width,
                    Height = CellHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = string.Format("A{0}", symplexTable.A[i] + 1),
                });

                // Pivot
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelPivot.Location.X, labelPivot.Location.Y + labelPivot.Height + i * CellHeight),
                    Width = labelPivot.Width,
                    Height = CellHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = symplexTable.Pivot[i].Equals(double.NaN) ? "-" : System.Math.Round(symplexTable.Pivot[i], 4).ToString("G5"),
                });

                // QC
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + symplexTable.Width * CellWidth, labelPivot.Location.Y + labelPivot.Height + i * CellHeight),
                    Width = CellWidth,
                    Height = CellHeight,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = symplexTable.Q[i].Equals(double.NaN) ? "-" : System.Math.Round(symplexTable.Q[i], 3).ToString("G3"),
                });

                for (int j = 0; j < symplexTable.Width; j++)
                {
                    // X
                    Label labelMatrixCell = new Label
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                        Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + j * CellWidth, labelPivot.Height + 13 + i * CellHeight),
                        Width = CellWidth,
                        Height = CellHeight,
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        Text = symplexTable.Matrix[i][j].Equals(double.NaN) ? "-" : System.Math.Round(symplexTable.Matrix[i][j], 4).ToString("G5"),
                    };

                    if (i == symplexTable.MainRow && j == symplexTable.MainColumn)
                    {
                        labelMatrixCell.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (i == symplexTable.MainRow)
                    {
                        labelMatrixCell.BackColor = System.Drawing.Color.LightPink;
                    }
                    else if (j == symplexTable.MainColumn)
                    {
                        labelMatrixCell.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        labelMatrixCell.BackColor = System.Drawing.SystemColors.ControlLightLight;
                    }

                    Controls.Add(labelMatrixCell);
                }
            }

            for (int i = 0; i < symplexTable.Width; i++)
            {
                // C
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + i * CellWidth, 13),
                    Width = CellWidth,
                    Height = labelPivot.Height,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = symplexTable.C[i].ToString(),
                });

                // Alpha
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Symbol", 10),
                    Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + i * CellWidth, labelPivot.Location.Y + labelPivot.Height + symplexTable.Height * CellHeight),
                    Width = CellWidth,
                    Height = labelScores.Height / 2,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = string.Format("a{0}", i + 1),
                });

                // AlphaC
                Controls.Add(new Label
                {
                    BackColor = System.Drawing.SystemColors.ControlLightLight,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10),
                    Location = new System.Drawing.Point(labelPivot.Location.X + labelPivot.Width + i * CellWidth, labelPivot.Location.Y + labelPivot.Height + symplexTable.Height * CellHeight + labelScores.Height / 2),
                    Width = CellWidth,
                    Height = labelScores.Height / 2,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = symplexTable.Alpha[i].Equals(double.NaN) ? "-" : System.Math.Round(symplexTable.Alpha[i], 2).ToString("G3"),
                });
            }

            Width = (symplexTable.Width + 1) * CellWidth + 39 + labelNumber.Width + labelCb.Width + labelBasis.Width + labelPivot.Width;

            Height = symplexTable.Height * CellHeight + 66 + labelNumber.Height + labelScores.Height;
        }
    }
}
