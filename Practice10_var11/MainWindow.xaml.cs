using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Practice10_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> _ints = new();
        Random _r = new();
        private bool modifiedFromCode = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {
            modifiedFromCode = true;
            string[] input = arrayBox.Text.Split(":");
            if (!int.TryParse(input[0], out int a) || a <= 0)
            {
                MessageBox.Show("Для того, чтобы сгенерировать случайный массив, введите в поле `Массив` число, соответствующее длине нового массива.\n\n" +
                                "Если нужно также указать диапазон случайных чисел, используйте маску <длина массива>:<нижний порог>:<верхний порог>");
                arrayBox.Text = "Введите число";
                arrayBox.Focus();
                arrayBox.SelectAll();
            }
            else
            {
                _ints.Clear();
                int b, c;
                switch (input.Length)
                {
                    case 1:
                        b = -10;
                        c = 10;
                        break;
                    case 3:
                        if (int.TryParse(input[1], out b) && int.TryParse(input[2], out c))
                        {
                            (b, c) = b > c ? (c, b) : (b, c);
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод!\n\n" +
                                            "Невалидные аргументы!");
                            arrayBox.Focus();
                            arrayBox.SelectAll();
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show("Некорректный ввод!\n\n" +
                                        "Такой комбинации аргументов не существует!\n" +
                                        "Используйте маску <длина массива>:<нижний порог>:<верхний порог>");
                        arrayBox.Focus();
                        arrayBox.SelectAll();
                        return;
                }
                _ints = Enumerable.Range(0, a).Select(i => _r.Next(b, c)).ToList();
                arrayBox.Text = string.Join(", ", _ints);
            }
        }

        /// <summary>
        /// Блок-схема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Solve_Click(object _, RoutedEventArgs __)
        {
            if (_ints.Count != 0)
            {
                int minMax = _ints.Where(x => x < 0).Max();
                resultBox.Text = $"{minMax};{_ints.IndexOf(minMax) + 1}";
            }
            else
            {
                MessageBox.Show("Ошибка при рассчёте ответа!\nМассив был пуст!");
            }
        }

        private void OnTextChange(object sender, TextChangedEventArgs e)
        {
            if (arrayBox.Text == "Массив")
            {
                arrayBox.Focus();
                arrayBox.SelectAll();
            }
            if (!modifiedFromCode)
            {
                try
                {
                    _ints = arrayBox.Text.Replace(", ", ",").Replace(" ", ",").Split(',').Select(item => (int)Convert.ChangeType(item, typeof(int))).ToList();
                }
                // Нам ничего не нужно ловить здесь
                catch (FormatException)
                { }
            }
            modifiedFromCode = false;
        }
    }
}
