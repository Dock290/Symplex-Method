using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class MainForm : Form
    {
        private const string ABOUT_PROGRAM_TITLE = "О программе";
        private const string ABOUT_PROGRAM_TEXT = "Программа решает базовую задачу линейного программирования симплекс-методом " +
            "с помощью симплекс-таблиц." +
            "\n\n" +
            "Для решения задачи линейного программирования задайте количество ограничений, количество переменных, " +
            "выберите направление целевой функции и задайте коэффициенты. " +
            "Затем введите данные в ячейки и нажмите на кнопку \"Посчитать\"." +
            "\n" +
            "Ограничения нобходимо указать в канонической форме. " +
            "Подразумевается, что у них стоит знак ≤." +
            "\n\n" +
            "Разработано на кафедре ПОУТС ФГБОУ ВО ПГУТИ" +
            "\n(pouts.psuti.ru)";

        private const string ERROR_UNKNOWN = "Неизвестная ошибка";
        private const string ERROR_PARSE = "Ошибка преобразования строки в число";

        private const int MIN_WIDTH = 2;
        private const int MIN_HEIGHT = 2;
        private const int MAX_WIDTH = 30;
        private const int MAX_HEIGHT = 20;

        private readonly int CELL_WIDTH = 30;
        private readonly int CELL_HEIGHT = 15;
        private readonly int MARGIN = 5;

        private int startTabIndex;

        private int tableWidth = 0;
        private int tableHeight = 0;

        private bool isMax = true;

        private readonly List<Label> formulaLabels;
        private readonly List<TextBox> formulaInputTable;

        private readonly List<Label> systemLabels;
        private readonly List<List<TextBox>> systemInputTable;

        private readonly List<SymplexTableForm> symplexTableForms;

        private double f;

        public MainForm()
        {
            InitializeComponent();

            formulaLabels = new List<Label>();
            formulaInputTable = new List<TextBox>();

            systemLabels = new List<Label>();
            systemInputTable = new List<List<TextBox>>();

            symplexTableForms = new List<SymplexTableForm>();

            Init(MIN_WIDTH, MIN_HEIGHT);
        }

        /// <summary>
        /// Инициализация интерфейса
        /// </summary>
        /// <param name="width">Начальная ширина таблицы</param>
        /// <param name="height">Начальная высота таблицы</param>
        private void Init(int width, int height)
        {
            tableHeight++;

            // Создание первого заголовка в системе
            Label systemLabel = new Label
            {
                Location = new System.Drawing.Point(tableWidth * CELL_WIDTH + MARGIN, CELL_HEIGHT),
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Text = "Св",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            systemGroupBox.Controls.Add(systemLabel);
            systemLabels.Add(systemLabel);

            // Создание первой ячейки в системе
            TextBox systemInputTextBox = new TextBox
            {
                Location = new System.Drawing.Point(MARGIN, tableHeight * CELL_WIDTH),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12),
                Width = CELL_WIDTH,
                Height = CELL_WIDTH,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Center,
                Text = "0",
            };

            systemInputTextBox.Enter += textBox_Enter;
            systemInputTextBox.Click += textBox_Enter;
            systemInputTextBox.Leave += textBox_Leave;

            systemInputTable.Add(new List<TextBox>());
            systemInputTable[tableHeight - 1].Add(systemInputTextBox);

            systemGroupBox.Controls.Add(systemInputTextBox);

            startTabIndex = systemInputTextBox.TabIndex;

            for (int i = 1; i < height; i++)
            {
                AddRow();
            }

            for (int i = 0; i < width; i++)
            {
                AddColumn();
            }

            CheckSize();
        }

        /// <summary>
        /// Восстановление последовательности перехода по клавише Tab между ячейками
        /// </summary>
        private void RestoreTabIndex()
        {
            int currentTabIndex = startTabIndex;

            foreach (TextBox formulaInput in formulaInputTable)
            {
                formulaInput.TabIndex = currentTabIndex++;
            }

            foreach (List<TextBox> systemInputSubTable in systemInputTable)
            {
                foreach (TextBox systemInput in systemInputSubTable)
                {
                    systemInput.TabIndex = currentTabIndex++;
                }
            }
        }

        /// <summary>
        /// Проверка размера таблицы и включение кнопок управления таблицей
        /// </summary>
        private void CheckSize()
        {
            addFormulaButton.Enabled = tableWidth < MAX_WIDTH;
            deleteFormulaButton.Enabled = tableWidth > MIN_WIDTH;
            addSystemButton.Enabled = tableHeight < MAX_HEIGHT;
            deleteSystemButton.Enabled = tableHeight > MIN_HEIGHT;
        }

        /// <summary>
        /// Добавить колонку
        /// </summary>
        private void AddColumn()
        {
            tableWidth++;

            Label formulaLabel = new Label
            {
                Location = new System.Drawing.Point((tableWidth - 1) * CELL_WIDTH + MARGIN, CELL_HEIGHT),
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Text = string.Format("x{0}", tableWidth),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            formulaGroupBox.Controls.Add(formulaLabel);
            formulaLabels.Add(formulaLabel);

            TextBox formulaInputTextBox = new TextBox
            {
                Location = new System.Drawing.Point(formulaLabel.Location.X, formulaLabel.Location.Y + formulaLabel.Height),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12),
                Width = CELL_WIDTH,
                Height = CELL_WIDTH,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Center,
                Text = "0",
            };

            formulaInputTextBox.Enter += textBox_Enter;
            formulaInputTextBox.Click += textBox_Enter;
            formulaInputTextBox.Leave += textBox_Leave;

            formulaGroupBox.Controls.Add(formulaInputTextBox);
            formulaInputTable.Add(formulaInputTextBox);


            Label systemLabel = new Label
            {
                Location = new System.Drawing.Point((tableWidth - 1) * CELL_WIDTH + MARGIN, CELL_HEIGHT),
                Width = CELL_WIDTH,
                Height = CELL_HEIGHT,
                Text = string.Format("x{0}", tableWidth),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            systemGroupBox.Controls.Add(systemLabel);
            systemLabels.Insert(tableWidth - 1, systemLabel);

            systemLabels[tableWidth].Location = new System.Drawing.Point(systemLabel.Location.X + CELL_WIDTH, systemLabel.Location.Y);

            for (int i = 0; i < tableHeight; i++)
            {
                TextBox systemInputTextBox = new TextBox
                {
                    Location = new System.Drawing.Point(tableWidth * CELL_WIDTH + MARGIN, i * CELL_WIDTH + formulaLabel.Location.Y + formulaLabel.Height),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12),
                    Width = CELL_WIDTH,
                    Height = CELL_WIDTH,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = HorizontalAlignment.Center,
                    Text = "0",
                };

                systemInputTextBox.Enter += textBox_Enter;
                systemInputTextBox.Click += textBox_Enter;
                systemInputTextBox.Leave += textBox_Leave;

                systemGroupBox.Controls.Add(systemInputTextBox);
                systemInputTable[i].Add(systemInputTextBox);
            }

            Width = systemGroupBox.Location.X + 68 + systemInputTable[0][tableWidth].Location.X + CELL_WIDTH;

            RestoreTabIndex();
        }

        /// <summary>
        /// Удалить колонку
        /// </summary>
        private void DeleteColumn()
        {
            if (tableWidth > 1)
            {
                tableWidth--;

                formulaLabels[tableWidth].Dispose();
                formulaLabels.RemoveAt(tableWidth);

                formulaInputTable[tableWidth].Dispose();
                formulaInputTable.RemoveAt(tableWidth);

                systemLabels[tableWidth].Dispose();
                systemLabels.RemoveAt(tableWidth);

                for (int i = 0; i < tableHeight; i++)
                {
                    systemInputTable[i][tableWidth].Dispose();
                    systemInputTable[i].RemoveAt(tableWidth);

                    systemInputTable[i][tableWidth].Location = new System.Drawing.Point(systemInputTable[i][tableWidth].Location.X - CELL_WIDTH, systemInputTable[i][tableWidth].Location.Y);
                }

                systemLabels[tableWidth].Location = new System.Drawing.Point(systemLabels[tableWidth].Location.X - CELL_WIDTH, systemLabels[tableWidth].Location.Y);

                Width = systemGroupBox.Location.X + 68 + systemInputTable[0][tableWidth - 1].Location.X + CELL_WIDTH;

                RestoreTabIndex();
            }
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        private void AddRow()
        {
            tableHeight++;

            systemInputTable.Add(new List<TextBox>());

            for (int j = 0; j <= tableWidth; j++)
            {
                TextBox systemInputTextBox = new TextBox
                {
                    Location = new System.Drawing.Point(j * CELL_WIDTH + MARGIN, tableHeight * CELL_WIDTH),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 12),
                    Width = CELL_WIDTH,
                    Height = CELL_WIDTH,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = HorizontalAlignment.Center,
                    Text = "0",
                };

                systemInputTextBox.Enter += textBox_Enter;
                systemInputTextBox.Click += textBox_Enter;
                systemInputTextBox.Leave += textBox_Leave;

                systemInputTable[tableHeight - 1].Add(systemInputTextBox);

                systemGroupBox.Controls.Add(systemInputTextBox);
            }

            Height = systemGroupBox.Location.Y + 55 + systemInputTable[tableHeight - 1][0].Location.Y + CELL_WIDTH;

            RestoreTabIndex();
        }

        /// <summary>
        /// Добавить строку
        /// </summary>
        private void DeleteRow()
        {
            if (tableHeight > 1)
            {
                tableHeight--;

                for (int i = 0; i <= tableWidth; i++)
                {
                    systemGroupBox.Controls.Remove(systemInputTable[tableHeight][i]);
                }

                systemInputTable.RemoveAt(tableHeight);

                Height = systemGroupBox.Location.Y + 55 + systemInputTable[tableHeight - 1][0].Location.Y + CELL_WIDTH;

                RestoreTabIndex();
            }
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            // Сохранение введённых значений во временный буфер
            string[] formulaInputs = new string[tableWidth];
            for (int i = 0; i < formulaInputs.Length; i++)
            {
                formulaInputs[i] = formulaInputTable[i].Text;
            }

            string[][] systemInputs = new string[tableHeight][];
            for (int i = 0; i < systemInputs.Length; i++)
            {
                systemInputs[i] = new string[tableWidth + 1];
                for (int j = 0; j < systemInputs[0].Length; j++)
                {
                    systemInputs[i][j] = systemInputTable[i][j].Text;
                }
            }

            // Добавление колонки
            AddColumn();

            // Загрузка введённых значений из временного буфера
            for (int i = 0; i < formulaInputs.Length; i++)
            {
                formulaInputTable[i].Text = formulaInputs[i];
            }

            for (int i = 0; i < systemInputs.Length; i++)
            {
                for (int j = 0; j < systemInputs[0].Length - 1; j++)
                {
                    systemInputTable[i][j].Text = systemInputs[i][j];
                }
                systemInputTable[i][systemInputs[0].Length].Text = systemInputs[i][systemInputs[0].Length - 1];
                systemInputTable[i][systemInputs[0].Length - 1].Text = "0";
            }

            // Проверка размера таблицы
            CheckSize();
        }

        private void buttonDeleteColumn_Click(object sender, EventArgs e)
        {
            // Сохранение введённых значений во временный буфер
            string[] formulaInputs = new string[tableWidth - 1];
            for (int i = 0; i < formulaInputs.Length; i++)
            {
                formulaInputs[i] = formulaInputTable[i].Text;
            }

            string[][] systemInputs = new string[tableHeight][];
            for (int i = 0; i < systemInputs.Length; i++)
            {
                systemInputs[i] = new string[tableWidth];
                for (int j = 0; j < systemInputs[0].Length - 1; j++)
                {
                    systemInputs[i][j] = systemInputTable[i][j].Text;
                }

                systemInputs[i][systemInputs[0].Length - 1] = systemInputTable[i][systemInputs[0].Length].Text;
            }

            // Удаление колонки
            DeleteColumn();

            // Загрузка введённых значений из временного буфера
            for (int i = 0; i < formulaInputs.Length; i++)
            {
                formulaInputTable[i].Text = formulaInputs[i];
            }

            for (int i = 0; i < systemInputs.Length; i++)
            {
                for (int j = 0; j < systemInputs[0].Length - 1; j++)
                {
                    systemInputTable[i][j].Text = systemInputs[i][j];
                }
            }

            // Проверка размера таблицы
            CheckSize();
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            AddRow();
            CheckSize();
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            DeleteRow();
            CheckSize();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            for (int i = symplexTableForms.Count - 1; i >= 0; i--)
            {
                symplexTableForms[i].Dispose();
                symplexTableForms.RemoveAt(i);
            }

            double[] k = new double[tableWidth];

            for (int i = 0; i < tableWidth; i++)
            {
                if (!double.TryParse(formulaInputTable[i].Text.Replace('.', ','), out k[i]))
                {
                    labelParsingResult.Text = ERROR_PARSE;
                    labelParsingResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
                    labelParsingResult.ForeColor = System.Drawing.Color.Red;
                    buttonSave.Enabled = false;
                    return;
                }
            }

            double[][] x = new double[tableHeight][];

            for (int i = 0; i < tableHeight; i++)
            {
                x[i] = new double[tableWidth + 1];

                for (int j = 0; j <= tableWidth; j++)
                {
                    if (!double.TryParse(systemInputTable[i][j].Text.Replace('.', ','), out x[i][j]))
                    {
                        labelParsingResult.Text = ERROR_PARSE;
                        labelParsingResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
                        labelParsingResult.ForeColor = System.Drawing.Color.Red;
                        buttonSave.Enabled = false;
                        return;
                    }
                }
            }

            bool isError = false;
            string error = ERROR_UNKNOWN;

            try
            {
                List<SymplexTable> symplexTables = Symplex.Solve(k, x, isMax);

                int current = symplexTables.Count - 1;

                f = symplexTables[current].F;

                isError = symplexTables[current].IsError;

                Symplex.ERROR_CODE_MESSAGE_PAIRS.TryGetValue(symplexTables[current].ErrorCode, out error);

                while (current >= 0)
                {
                    symplexTableForms.Add(new SymplexTableForm(symplexTables[current], current));
                    symplexTableForms[symplexTableForms.Count - 1].Show();
                    symplexTableForms[symplexTableForms.Count - 1].Location = new System.Drawing.Point((Location.X + Width), Location.Y + 20 * current);

                    current--;
                }
            }
            catch (Exception exeption)
            {
                isError = true;
                error = exeption.Message;
            }

            if (isError)
            {
                labelParsingResult.Text = error;
                labelParsingResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
                labelParsingResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                labelParsingResult.Text = $"f = {f}";
                labelParsingResult.Font = new System.Drawing.Font("Courier New", 8.25f);
                labelParsingResult.ForeColor = System.Drawing.SystemColors.ControlText;
            }

            buttonSave.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = symplexTableForms.Count - 1; i >= 0; i--)
            {
                stringBuilder.Append(symplexTableForms[i].SymplexTable.ToString());
                stringBuilder.Append("\n\n");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Application.StartupPath,
                RestoreDirectory = true,
                FileName = "output.txt",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file;
                if ((file = new StreamWriter(saveFileDialog.OpenFile())) != null)
                {
                    file.Write(stringBuilder.ToString());

                    file.Close();
                }
                else
                {
                    labelParsingResult.Text = $"Не удалось открыть файл\n{saveFileDialog.FileName}";
                    labelParsingResult.ForeColor = System.Drawing.SystemColors.ControlText;
                    labelParsingResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
                }
            }
        }

        private void radioButton_Min_CheckedChanged(object sender, EventArgs e)
        {
            isMax = false;
        }

        private void radioButton_Max_CheckedChanged(object sender, EventArgs e)
        {
            isMax = true;
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.TextLength == 0)
            {
                textBox.Text = "0";
            }
        }

        private void addProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, ABOUT_PROGRAM_TEXT, ABOUT_PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
