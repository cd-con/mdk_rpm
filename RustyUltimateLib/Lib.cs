using System;
using System.IO;
using System.Linq;

namespace RustyUltimateLib
{
    public struct IntRange
    {
        public int Start;
        public int End;

        public IntRange(int start = int.MinValue, int end = int.MaxValue)
        {
            Start = start;
            End = end;
        }
    }
    public static class Lib
    {
        /// <summary>
        /// Валидатор ввода пользователя со встроенным диалоговым окном
        /// </summary>
        /// <param name="reading">Строка для валидации</param>
        /// <param name="range">Диапазон допустимых значений</param>
        /// <param name="onFail">Вызывается при неудачной попытке конвертации ввода пользователя. Передаваемый параметр - текст ошибки</param>
        /// <returns>Число</returns>
        public static int ValidatedInput(string reading, IntRange range = new IntRange(), Func<string, string> onFail = null)
        {            
            if (int.TryParse(reading, out int n) && n >= range.Start && n <= range.End)
                return n;
            onFail?.Invoke($"Неверный ввод. Допустимые значения от {range.Start} до {range.End}");
            return 0;
        }
    }

    public static class BetterArray
    {
        /// <summary>
        /// Удобная библеотека для работы с массивами от cd-con
        /// </summary>
        private static Random _r = new();

        //
        //  *** МЕТОДЫ ДЛЯ ОДНОМЕРНЫХ МАССИВОВ ***
        //

        /// <summary>
        /// Метод очистки одномерного массива
        /// </summary>
        /// <typeparam name="T">Тип данных массива</typeparam>
        /// <param name="array">Ссылка на массив</param>
        public static void Clear<T>(ref T[] array)
        {
            array = new T[array.Length];
        }

        /// <summary>
        /// Возвращает массив, заполненый случайными числами в диапазоне.
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="min">Нижний предел случайных чисел</param>
        /// <param name="max">Верхний предел случайных чисел</param>
        private static T[] Fill<T>(T[] array, int min, int max) => array.Select(i => (T)Convert.ChangeType(_r.Next(min, max), typeof(T))).ToArray();

        /// <summary>
        /// Заполняет массив случаныйми числами в диапазоне
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="min">Нижний предел случайных чисел</param>
        /// <param name="max">Верхний предел случайных чисел</param>
        public static void Fill<T>(ref T[] array, int min, int max)
        {
            array = Fill(array, min, max);
        }

        /// <summary>
        /// Читает массив из файла
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="path">Путь до файла</param>
        /// <param name="customSeparator">Кастомный разделитель значений</param>
        /// <returns></returns>
        public static T[] Open<T>(string path, char customSeparator = ',') => string.Join(customSeparator, File.ReadAllLines(path))
                                                                                    .ToArray<T>();

        /// <summary>
        /// Сохранение массива в файл
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="path">Путь до файла</param>
        /// <param name="customSeparator">Кастомный разделитель значений</param>
        public static void Save<T>(this T[] array, string path, char customSeparator = ',')
        {
            File.WriteAllText(path, string.Join(customSeparator, array));
        }

        //
        //  *** МЕТОДЫ ДЛЯ ДВУМЕРНЫХ МАССИВОВ ***
        //

        /// <summary>
        /// Метод очистки многомерного массива
        /// </summary>
        /// <typeparam name="T">Тип данных матрицы</typeparam>
        /// <param name="matrix">Ссылка на матрицу</param>
        public static void Clear<T>(ref T[,] matrix)
        {
            matrix = new T[matrix.GetLength(0), matrix.GetLength(1)];
        }

        /// <summary>
        /// Заполняет матрицу случаныйми числами в диапазоне
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="matrix">Матрица</param>
        /// <param name="min">Нижний предел случайных чисел</param>
        /// <param name="max">Верхний предел случайных чисел</param>
        public static void Fill<T>(ref T[,] matrix, int min, int max)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
                SetRow(matrix, Fill(matrix.GetRow(x), min, max), x);
        }

        /// <summary>
        /// Сохраниение матрицы в файл
        /// </summary>
        /// <typeparam name="T">Массив</typeparam>
        /// <param name="matrix">Матрица</param>
        /// <param name="path">Путь до файла</param>
        /// <param name="customSeparator">Кастомный разделитель значений</param>
        public static void Save<T>(this T[,] matrix, string path, char customSeparator = ',')
        {
            File.WriteAllText(path, matrix.PPrint(customSeparator.ToString()));
        }

        /// <summary>
        /// Читает матрицу из файла
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="path">Путь до файла</param>
        /// <param name="customSeparator">Кастомный разделитель значений</param>
        /// <returns></returns>
        public static T[,] OpenMatrix<T>(string path, char customSeparator = ',')
        {
            string[] lines = File.ReadAllLines(path);
            T[,] result = new T[lines.Length, lines[0].Split(customSeparator).Length];

            for (int index = 0; index < result.GetLength(0); index++)
            {
                SetRow(result, lines[index].ToArray<T>(), index);
            }

            return result;
        }

        //
        //  *** ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ***
        //

        /// <summary>
        /// Конвертирует строку в массив
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="customSeparator"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this string str, char customSeparator = ',') => str.Split(customSeparator)
                                                                                        .Select(item => (T)Convert.ChangeType(item, typeof(T)))
                                                                                        .ToArray();

        /// <summary>
        /// Устанавливает строку в матрицу
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="matrix">Матрица</param>
        /// <param name="row">Новая строка</param>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="wrapOnMatrixRowOverflow">Включение автоматического оборачивания бОльшего массива в матрицу</param>
        /// <returns>Результат операции</returns>
        public static bool SetRow<T>(T[,] matrix, T[] row, int rowIndex, bool wrapOnMatrixRowOverflow = true)
        {
            if (rowIndex > matrix.GetLength(0))
                return false;

            for (int i = 0; i < Math.Max(matrix.GetLength(1), row.Length); i++)
            {
                // Если массив больше матрицы - переносим его на новую строку
                if (i == matrix.GetLength(1))
                {
                    if (wrapOnMatrixRowOverflow && rowIndex < matrix.GetLength(0) - 1) { rowIndex++; } else { return false; }
                }
                if (i == row.Length)
                    return false;

                matrix[rowIndex, i % matrix.GetLength(1)] = row[i];
            }

            return true;
        }

        /// <summary>
        /// Возвращает строку из матрицы в виде массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix">Матрица</param>
        /// <param name="row">Номер строки</param>
        /// <returns></returns>
        public static T[] GetRow<T>(this T[,] matrix, int row) => Enumerable.Range(0, matrix.GetLength(1)).Select(colIndex => matrix[row, colIndex]).ToArray();

        /// <summary>
        /// Создаёт красивое отображение матрицы
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="matrix">Матрица</param>
        /// <param name="separator">Кастомный разделитель значений</param>
        /// <param name="newLine">Кастомный разделитель строк</param>
        /// <returns></returns>
        public static string PPrint<T>(this T[,] matrix, string separator, string newLine = "\n")
        {
            string result = string.Empty;
            for (int x = 0; x < matrix.GetLength(0); x++)
                result += string.Join(separator, matrix.GetRow(x)) + newLine;
            return result;
        }
    }
}
