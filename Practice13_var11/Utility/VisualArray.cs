using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_var11.Utility
{
    public static class VisualArrayLINQ
    {
        //Метод для одномерного массива
        public static DataTable ToDataTableLINQ<T>(this T[] arr, string customHeader = "col%id%")
        {
            DataTable res = new();
            DataRow row = res.NewRow();

            Enumerable.Range(0, arr.Length).Select(x => res.Columns.Add(customHeader.Replace("%id%", x.ToString()), typeof(T)));

            Enumerable.Range(0, arr.Length).Select(x => row[x] = arr[x]);
            res.Rows.Add(row);

            return res;
        }
        //Метод для двухмерного массива
        public static DataTable ToDataTableLINQ<T>(this T[,] matrix, string customHeader = "col%id%")
        {
            DataTable res = new();

            Enumerable.Range(0, matrix.GetLength(1)).Select(x => res.Columns.Add(customHeader.Replace("%id%", x.ToString()), typeof(T)));

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                DataRow row = res.NewRow();
                Enumerable.Range(0, matrix.GetLength(1)).Select(y => row[x] = matrix[x, y]);
                res.Rows.Add(row);
            }

            return res;
        }


    }
    public static class VisualArray
    {
        //Метод для одномерного массива
        public static DataTable ToDataTable<T>(this T[] arr)
        {
            var res = new DataTable();
            for (int i = 0; i < arr.Length; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < arr.Length; i++)
            {
                row[i] = arr[i];
            }
            res.Rows.Add(row);
            return res;
        }
        //Метод для двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }


    }
}
