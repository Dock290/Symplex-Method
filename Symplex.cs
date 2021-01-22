using System.Collections.Generic;
using System.Text;

namespace Lab_1
{
    public class Symplex
    {
        /// <summary>
        /// Предел количества итераций
        /// </summary>
        public static int ITERATION_LIMIT { set; get; } = 50;

        /// <summary>
        /// Коды ошибок
        /// </summary>
        public enum ERROR_CODES
        {
            NO_ERROR,
            NO_LIMIT,
            NO_NEGATIVE_ELEMENTS_IN_MAIN_ROW,
            NO_NEGATIVE_ELEMENTS_IN_Q,
            NO_POSITIVE_ELEMENTS_IN_Q,
            NO_NEGATIVE_ELEMENTS_IN_ALPHA,
            NO_POSITIVE_ELEMENTS_IN_ALPHA,
            ITERATION_LIMIT,
        }

        /// <summary>
        /// Соответствуие кодов ошибок с сообщениями
        /// </summary>
        public static Dictionary<ERROR_CODES, string> ERROR_CODE_MESSAGE_PAIRS = new Dictionary<ERROR_CODES, string>
        {
            { ERROR_CODES.NO_ERROR, "Решение найдено" },
            { ERROR_CODES.NO_LIMIT, "Функция не ограничена. Оптимальное решение отсутствует" },
            { ERROR_CODES.NO_NEGATIVE_ELEMENTS_IN_MAIN_ROW, "Отсутствуют отрицательные элементы в ведущей строке" },
            { ERROR_CODES.NO_NEGATIVE_ELEMENTS_IN_Q, "Отсутствуют отрицательные элементы в симплекс-отношениях Q" },
            { ERROR_CODES.NO_POSITIVE_ELEMENTS_IN_Q, "Отсутствуют положительные элементы в симплекс-отношениях Q" },
            { ERROR_CODES.NO_NEGATIVE_ELEMENTS_IN_ALPHA, "Отсутствуют отрицательные элементы в строке альфы" },
            { ERROR_CODES.NO_POSITIVE_ELEMENTS_IN_ALPHA, "Отсутствуют положительные элементы в строке альфы" },
            { ERROR_CODES.ITERATION_LIMIT, "Превышен предел итераций" },
        };

        // Запрет на создание экземпляров статического класса
        static Symplex()
        {

        }

        /// <summary>
        /// Запуск решения методом симплекс-таблиц
        /// </summary>
        /// <param name="k">Коэффициенты перед иксами в главной формуле, длина массива - количество иксов</param>
        /// <param name="x">Коэффициенты перед иксами и значения формул, длина массива - количество формул</param>
        /// <param name="isMax">Если истина, то происходит поиск максимума, иначе - минимума</param>
        /// <returns></returns>
        public static List<SymplexTable> Solve(double[] k, double[][] x, bool isMax)
        {
            // Инициализация списка таблиц
            List<SymplexTable> symplexTables = new List<SymplexTable>
            {
                new SymplexTable(k, x)
            };

            // Если обнаружена ошибка или найден ответ, то не начинать поиск
            if (symplexTables[0].CheckSolutionStop(isMax))
            {
                return symplexTables;
            }

            symplexTables[0].FindMainElement(isMax);

            for (int i = 0; i < symplexTables.Count; i++)
            {
                if (i >= ITERATION_LIMIT)
                {
                    symplexTables[i].ErrorCode = ERROR_CODES.ITERATION_LIMIT;
                }

                if (symplexTables[i].IsSolved || symplexTables[i].IsError)
                {
                    break;
                }
                else
                {
                    symplexTables.Add(symplexTables[i].GetNext(isMax));
                    if (symplexTables[i + 1].IsSolved || symplexTables[i + 1].IsError)
                    {
                        break;
                    }
                }
            }

            return symplexTables;
        }
    }

    public class SymplexTable
    {
        /// <summary>
        /// Значение элемента в случае ошибки
        /// </summary>
        public static int ERROR = -1;

        /// <summary>
        /// Ширина таблицы
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Высота таблицы
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Истина если ответ найден, иначе - ложь
        /// </summary>
        public bool IsSolved { get; private set; }

        /// <summary>
        /// Истина если обнаружена ошибка, иначе - ложь
        /// </summary>
        public bool IsError => ErrorCode != 0;

        /// <summary>
        /// Код ошибки
        /// </summary>
        public Symplex.ERROR_CODES ErrorCode { get; set; }

        /// <summary>
        /// Массив значений базиса
        /// </summary>
        public int[] A { get; private set; }

        /// <summary>
        /// Массив значений изначальных базисов
        /// </summary>
        public double[] C { get; private set; }

        /// <summary>
        /// Массив коэффициентов целевой функции
        /// </summary>
        public double[] Cb { get; private set; }

        /// <summary>
        /// Массив значений опорного решения
        /// </summary>
        public double[] Pivot { get; private set; }

        /// <summary>
        /// Массив альф
        /// </summary>
        public double[] Alpha { get; private set; }

        /// <summary>
        /// Значение функции
        /// </summary>
        public double F { get; private set; }

        /// <summary>
        /// Массив значений симплекс-отношений
        /// </summary>
        public double[] Q { get; private set; }

        /// <summary>
        /// Номер ведущей строки
        /// </summary>
        public int MainRow { get; private set; }

        /// <summary>
        /// Номер ведущего столбца
        /// </summary>
        public int MainColumn { get; private set; }

        /// <summary>
        /// Таблица элементов
        /// </summary>
        public double[][] Matrix { get; private set; }

        /// <summary>
        /// Проверка наличия отрицательных значений в массиве опорного решения
        /// </summary>
        public bool IsContainsNegativeInPivot
        {
            get
            {
                bool result = false;
                for (int i = 0; i < Height; i++)
                {
                    if (Pivot[i] < 0)
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Создание пустой симплекс-таблицы
        /// </summary>
        /// <param name="width">Количество колонок</param>
        /// <param name="height">Количество строк</param>
        public SymplexTable(int width, int height)
        {
            Width = width;
            Height = height;

            A = new int[height];
            C = new double[width];
            Cb = new double[height];
            Pivot = new double[height];
            Alpha = new double[width];

            Q = new double[height];

            Matrix = new double[height][];

            for (int i = 0; i < height; i++)
            {
                Matrix[i] = new double[width];
            }

            MainRow = -1;
            MainColumn = -1;

            IsSolved = false;
            ErrorCode = Symplex.ERROR_CODES.NO_ERROR;
        }

        /// <summary>
        /// Создание симплес-таблицы и заполнение переданными значениями
        /// </summary>
        /// <param name="k">Коэффициенты в формуле</param>
        /// <param name="x">Коэффициенты в системе</param>
        public SymplexTable(double[] k, double[][] x) : this(k.Length, x.Length)
        {
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x[i].Length - 1; j++)
                {
                    Matrix[i][j] = x[i][j];
                }
                Pivot[i] = x[i][x[i].Length - 1];
            }

            for (int i = 0; i < Height; i++)
            {
                A[i] = Width - Height + i;
            }

            for (int i = 0; i < k.Length; i++)
            {
                C[i] = k[i];
            }

            for (int i = Height - 1, j = Width - 1; i >= 0; i--, j--)
            {
                Cb[i] = C[j];
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Alpha[i] += Cb[j] * Matrix[j][i];
                }

                Alpha[i] -= C[i];
            }
        }

        /// <summary>
        /// Создание новой таблицы со следующими наследуемыми параметрами переданной таблицы:
        /// </summary>
        /// <remarks>
        /// <para>Width - ширина</para>
        /// <para>Height - высота</para>
        /// <para>A - базис</para>
        /// <para>C - изначальный базис</para>
        /// <para>Cb - коэффициеты целевой функции</para>
        /// <para>Pivot - опорное решение</para>
        /// <para>Alpha - альфа</para>
        /// <para>Matrix - матрица</para>
        /// </remarks>
        /// <param name="symplexTable">Наследуемая таблица</param>
        public SymplexTable(SymplexTable symplexTable)
        {
            Width = symplexTable.Width;
            Height = symplexTable.Height;

            MainRow = -1;
            MainColumn = -1;

            IsSolved = false;
            ErrorCode = Symplex.ERROR_CODES.NO_ERROR;

            A = new int[symplexTable.A.Length];
            symplexTable.A.CopyTo(A, 0);

            C = new double[symplexTable.C.Length];
            symplexTable.C.CopyTo(C, 0);

            Cb = new double[symplexTable.Cb.Length];
            symplexTable.Cb.CopyTo(Cb, 0);

            Pivot = new double[symplexTable.Pivot.Length];
            symplexTable.Pivot.CopyTo(Pivot, 0);

            Alpha = new double[symplexTable.Alpha.Length];
            symplexTable.Alpha.CopyTo(Alpha, 0);

            Q = new double[symplexTable.Q.Length];

            Matrix = new double[symplexTable.Matrix.Length][];
            for (int i = 0; i < Matrix.Length; i++)
            {
                Matrix[i] = new double[symplexTable.Matrix[i].Length];
                symplexTable.Matrix[i].CopyTo(Matrix[i], 0);
            }
        }

        /// <summary>
        /// Получение симплекс-таблицы из следующей итерации
        /// </summary>
        /// <param name="isMax">Если истина, то происходит поиск максимума, иначе - минимума</param>
        /// <returns>Следующая итерация симплекс-таблицы</returns>
        public SymplexTable GetNext(bool isMax)
        {
            SymplexTable symplexTable = new SymplexTable(this);

            // Заполнение таблицы новыми значениями
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == MainRow && j == MainColumn)
                    {
                        symplexTable.Matrix[i][j] = 1;
                    }
                    else if (i == MainRow)
                    {
                        symplexTable.Matrix[i][j] = Matrix[i][j] / Matrix[MainRow][MainColumn];
                    }
                    else if (j == MainColumn)
                    {
                        symplexTable.Matrix[i][j] = 0;
                    }
                    else
                    {
                        symplexTable.Matrix[i][j] = Matrix[i][j] - Matrix[MainRow][j]
                            / Matrix[MainRow][MainColumn] * Matrix[i][MainColumn];
                    }
                }
            }

            // Замена базиса
            for (int i = 0; i < Height; i++)
            {
                if (i == MainRow)
                {
                    symplexTable.A[MainRow] = MainColumn;
                    symplexTable.Cb[MainRow] = C[MainColumn];
                }
            }

            // Вычисление опорного решения
            for (int i = 0; i < Height; i++)
            {
                if (i == MainRow)
                {
                    symplexTable.Pivot[i] = Pivot[i] / Matrix[MainRow][MainColumn];
                }
                else
                {
                    symplexTable.Pivot[i] = Pivot[i] - Pivot[MainRow] / Matrix[MainRow][MainColumn] * Matrix[i][MainColumn];
                }
            }

            // Вычисление оценки
            symplexTable.F = 0;
            for (int i = 0; i < Height; i++)
            {
                symplexTable.F += symplexTable.Cb[i] * symplexTable.Pivot[i];
            }

            // Вычисление альфы
            for (int i = 0; i < Width; i++)
            {
                symplexTable.Alpha[i] = 0;

                for (int j = 0; j < Height; j++)
                {
                    symplexTable.Alpha[i] += symplexTable.Cb[j] * symplexTable.Matrix[j][i];
                }

                symplexTable.Alpha[i] -= C[i];
            }

            // Проверка решения
            bool isContainsNegative = symplexTable.IsContainsNegativeInPivot;

            if (!symplexTable.CheckSolutionStop(isMax, isContainsNegative))
            {
                // Проверка
                symplexTable.FindMainElement(isMax);
            }

            return symplexTable;
        }

        /// <summary>
        /// Получение массива с координатами ведущего элемента и проверкой наличия отрицательных элементов в опорном решении
        /// </summary>
        /// <param name="isMax">Если истина, то происходит поиск в случае максимума, иначе - минимума</param>
        /// <returns></returns>
        public int[] FindMainElement(bool isMax) => FindMainElement(isMax, IsContainsNegativeInPivot);

        /// <summary>
        /// Получение массива с координатами ведущего элемента
        /// </summary>
        /// <param name="isMax">Если истина, то происходит поиск в случае максимума, иначе - минимума</param>
        /// <param name="isContainsNegative">
        /// Если истина, то происходит преобразование решения,
        /// иначе используется стандартный алгоритм
        /// </param>
        /// <returns></returns>
        public int[] FindMainElement(bool isMax, bool isContainsNegative)
        {
            if (IsError) // Если обнаружена ошибка, то не начинать поиск
            {
                MainRow = ERROR;
                MainColumn = ERROR;
            }
            else if (!IsSolved) // Если ответ не был найден, то начать поиск
            {
                if (isContainsNegative) // Если в опорном решении содержится отрицательное значение, то происходит поиск ведущей строки, затем ведущего столбца
                {
                    FindMainRow(isContainsNegative);
                    FindMainColumn(isMax, isContainsNegative);
                }
                else // Если в опорном решении не содержится отрицательное значение, то происходит поиск ведущего столбца, затем ведущей строки
                {
                    FindMainColumn(isMax, isContainsNegative);
                    FindMainRow(isContainsNegative);
                }
            }

            return new int[] { MainRow, MainColumn };
        }

        /// <summary>
        /// Поиск ведущей колонки с проверкой наличия отрицательных элементов в опорном решении
        /// </summary>
        /// <param name="isMax">Если истина, то происходит поиск в случае максимума, иначе - минимума</param>
        /// <returns>Номер ведущей колонки</returns>
        public int FindMainColumn(bool isMax) => FindMainColumn(isMax, IsContainsNegativeInPivot);

        /// <summary>
        /// Поиск ведущей колонки
        /// </summary>
        /// <param name="isMax">Если истина, то происходит поиск в случае максимума, иначе - минимума</param>
        /// <param name="isContainsNegative">
        /// Если истина, то происходит преобразование решения,
        /// иначе используется стандартный алгоритм
        /// </param>
        /// <returns>Номер ведущей колонки</returns>
        public int FindMainColumn(bool isMax, bool isContainsNegative)
        {
            if (IsError) // Если обнаружена ошибка, то не начинать поиск
            {
                MainColumn = ERROR;
            }
            else if (!IsSolved) // Если ответ не был найден, то начать поиск
            {
                double buffer;

                if (isContainsNegative) // Если в опорном решении содержится отрицательное число
                {
                    buffer = double.MaxValue;

                    for (int i = 0; i < Width; i++)
                    {
                        if (Matrix[MainRow][i] < 0 && Matrix[MainRow][i] < buffer)
                        {
                            buffer = Matrix[MainRow][i];
                            MainColumn = i;
                        }
                    }

                    if (MainColumn == ERROR)
                    {
                        ErrorCode = Symplex.ERROR_CODES.NO_NEGATIVE_ELEMENTS_IN_MAIN_ROW;
                    }
                }
                else
                {
                    if (isMax) // Если направление целевой фунции - максимум, то происходит поиск минимального отрицательного элемента в массиве альф
                    {
                        buffer = double.MaxValue;

                        for (int i = 0; i < Width; i++)
                        {
                            if (Alpha[i] < 0 && Alpha[i] < buffer)
                            {
                                buffer = Alpha[i];
                                MainColumn = i;
                            }
                        }

                        if (MainColumn == ERROR)
                        {
                            ErrorCode = Symplex.ERROR_CODES.NO_NEGATIVE_ELEMENTS_IN_ALPHA;
                        }
                    }
                    else // Если направление целевой фунции - минимум, то происходит поиск максимального отрицательного элемента в массиве альф
                    {
                        buffer = double.MinValue;

                        for (int i = 0; i < Width; i++)
                        {
                            if (Alpha[i] > 0 && Alpha[i] > buffer)
                            {
                                buffer = Alpha[i];
                                MainColumn = i;
                            }
                        }

                        if (MainColumn == ERROR)
                        {
                            ErrorCode = Symplex.ERROR_CODES.NO_POSITIVE_ELEMENTS_IN_ALPHA;
                        }
                    }
                }
            }

            return MainColumn;
        }

        /// <summary>
        /// Поиск ведущей строки с проверкой наличия отрицательных элементов в опорном решении
        /// </summary>
        /// <returns>Номер ведущей строки</returns>
        public int FindMainRow() => FindMainRow(IsContainsNegativeInPivot);

        /// <summary>
        /// Поиск ведущей строки
        /// </summary>
        /// <param name="isContainsNegative">
        /// Если истина, то происходит преобразование решения,
        /// иначе используется стандартный алгоритм
        /// </param>
        /// <returns>Номер ведущей строки</returns>
        public int FindMainRow(bool isContainsNegative)
        {
            if (IsError) // Если обнаружена ошибка, то не начинать поиск
            {
                MainRow = ERROR;
            }
            else if (!IsSolved) // Если ответ не был найден, то начать поиск
            {
                double buffer;

                if (isContainsNegative) // Если ведущая строка по минимальному отрицательному элементу
                {
                    buffer = double.MaxValue;
                    for (int i = 0; i < Height; i++)
                    {
                        if (Pivot[i] < 0)
                        {
                            if (Pivot[i] < buffer)
                            {
                                buffer = Pivot[i];
                                MainRow = i;
                            }
                        }
                    }
                }
                else
                {
                    // Вычисление отношения координат
                    for (int i = 0; i < Height; i++)
                    {
                        Q[i] = Matrix[i][MainColumn] != 0 ? Pivot[i] / Matrix[i][MainColumn] : 0;
                    }

                    // Поиск минимального положительного элемента в массиве отношений координат Q

                    buffer = double.MaxValue;
                    for (int i = 0; i < Height; i++)
                    {
                        if (Q[i] > 0 && Q[i] < buffer)
                        {
                            buffer = Q[i];
                            MainRow = i;
                        }
                    }

                    if (MainRow == ERROR)
                    {
                        ErrorCode = Symplex.ERROR_CODES.NO_POSITIVE_ELEMENTS_IN_Q;
                    }
                }
            }

            return MainRow;
        }

        /// <summary>
        /// Проверка следует ли прекратить дальнейшее решение с проверкой наличия отрицательных элементов в опорном решении
        /// </summary>
        /// <returns>Истина, если найден ответ или возникла ошибка, иначе - ложь</returns>
        public bool CheckSolutionStop(bool isMax) => CheckSolutionStop(isMax, IsContainsNegativeInPivot);

        /// <summary>
        /// Проверка следует ли прекратить дальнейшее решение
        /// </summary>
        /// <param name="isContainsNegative">
        /// Если истина, то происходит преобразование решения,
        /// иначе используется стандартный алгоритм
        /// </param>
        /// <returns>Истина, если найден ответ или возникла ошибка, иначе - ложь</returns>
        public bool CheckSolutionStop(bool isMax, bool isContainsNegative)
        {
            if (IsError) // Если обнаружена ошибка, то не начинать проверку
            {
                IsSolved = false;
            }
            else if (!IsSolved) // Если решение не было найдено, то проверить наличие решения
            {
                if (isContainsNegative) // Если в столбце Pivot содержатся отрицательные элементы, то ответ не найден
                {
                    IsSolved = false;
                }
                else
                {
                    // Проверка на оптимальность текущего плана
                    IsSolved = true;

                    if (isMax) // Если направление целевой фунции - максимум, то происходит проверка наличия отрицательных элементов в массиве Alpha
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            if (Alpha[i] < 0)
                            {
                                IsSolved = false;
                                break;
                            }
                        }
                    }
                    else // Если направление целевой фунции - минимум, то происходит проверка наличия положительных элементов в массиве Alpha
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            if (Alpha[i] > 0)
                            {
                                IsSolved = false;
                                break;
                            }
                        }
                    }
                }
            }

            return IsSolved || IsError;
        }

        /// <summary>
        /// Возвращает текстовое представление симплекс-таблицы
        /// </summary>
        /// <returns>Строка, содержащая текстовое представление симплекс-таблицы</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(string.Format("Main Row: {0}\tMain Column: {1}", MainRow + 1, MainColumn + 1));

            stringBuilder.Append("\n\t\t\t");
            for (int i = 0; i < Width; i++)
            {
                stringBuilder.Append(string.Format("{0}\t", C[i]));
            }

            stringBuilder.Append("\nA\tc\tb\t");
            for (int i = 0; i < Width; i++)
            {
                stringBuilder.Append(string.Format("A{0}\t", i + 1));
            }
            stringBuilder.Append("Q\n");

            for (int i = 0; i < Height; i++)
            {
                stringBuilder.Append(string.Format("A{0}\t{1}\t{2}\t", A[i], Cb[i], Pivot[i].ToString()));

                for (int j = 0; j < Width; j++)
                {
                    stringBuilder.Append(string.Format("{0}\t", Matrix[i][j]));
                }

                stringBuilder.Append(string.Format("{0}\n", Q[i]));
            }

            stringBuilder.Append("\t\tf\t");
            for (int i = 0; i < Width; i++)
            {
                stringBuilder.Append(string.Format("a{0}\t", i + 1));
            }

            stringBuilder.Append(string.Format("\n\t\t{0}\t", F));
            for (int i = 0; i < Width; i++)
            {
                stringBuilder.Append(string.Format("{0}\t", Alpha[i]));
            }

            return stringBuilder.ToString();
        }
    }
}
