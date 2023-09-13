using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib_11
{
    /// <summary>
    /// Найти произведение n целых случайных чисел X., распределенных в диапазоне от
    /// 0 до n. Вывести на экран на одной строке сгенерированные числа, на другой
    /// строке результат.
    /// </summary>
    public static class Practice1
    {
        private static Random _rnd = new Random();

        /// <summary>
        /// Создаёт и заполяет массив n элементами в диапазоне от 0 до n
        /// </summary>
        /// <param name="x">Ссылка на список</param>
        /// <param name="n"></param>
        public static void RegenerateList(this List<int> x, int n) {
            x = Enumerable.Range(0, n).Select(val => val = _rnd.Next(0, n)).ToList();
        }

        /// <summary>
        /// Найти произведение целых чисел в диапазоне от 1 до n
        /// </summary>
        /// <param name="n">Верхняя граница диапазона</param>
        /// <returns>Произведение диапазона</returns>
        public static int GetMulOfRange(int n) => Enumerable.Range(1, n).Aggregate(1, (a, b) => a * b);

        /// <summary>
        /// Найти произведение списка чисел
        /// </summary>
        /// <param name="x">Ссылка на список</param>
        /// <returns>Произведение списка</returns>
        public static int GetMul(this List<int> x) => x.Aggregate(1, (a, b) => a * b);
    }
}
