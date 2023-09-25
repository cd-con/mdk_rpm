using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib_11
{
    public static class Practice1
    {
        private static Random _rnd = new Random();

        /// <summary>
        /// Создаёт и заполняет список длинной n элементов целыми числами в диапазоне от 0 до n.
        /// </summary>
        /// <param name="n">Граница диапазона и длинна генерируемого массива.</param>
        /// <returns>Список длинной n элементов целыми числами в диапазоне от 0 до n.</returns>
        public static List<int> RegenerateList(int n) => Enumerable.Range(0, n).Select(val => val = _rnd.Next(0, n)).ToList();


        /// <summary>
        ///Метод расширения. Находит произведение списка чисел.
        /// </summary>
        /// <param name="x">Входной список.</param>
        /// <returns>Произведение списка чисел.</returns>
        public static int GetMul(this List<int> x) => x.Aggregate(1, (a, b) => b == 0 ? a : a * b);
    }
}
