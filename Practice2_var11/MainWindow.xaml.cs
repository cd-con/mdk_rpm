using RustyUltimateLib;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Net;
using System.Windows;

namespace Practice2_var10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const bool NO_BEE_MOVIE = false;
        private int[]? _ints;
        private bool modifiedFromCode = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {
            modifiedFromCode = true;
            string[] input = arrayBox.Text.Split(":");
            if (!int.TryParse(input[0], out int a) && a <= 0)
            {
                MessageBox.Show("Для того, чтобы сгенерировать случайный массив, введите в поле `Массив` число, соответствующее длине нового массива.\n\n" +
                                "Если нужно также указать диапазон случайных чисел, используйте маску <длина массива>:<нижний порог>:<верхний порог>");
                arrayBox.Text = "Введите число";
                arrayBox.Focus();
                arrayBox.SelectAll();
            }
            else
            {
                _ints = new int[a];
                int b, c;
                switch (input.Length)
                {
                    case 1:
                        b = -10;
                        c= 10;
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
                BetterArray.Fill(ref _ints, b, c);
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
            if (_ints != null)
            {
                resultBox.Text = (_ints.Where(x => x != 0).Aggregate(_ints[0], (a, n) => a - n) + _ints[0]).ToString();
            }
            else
            {
                MessageBox.Show("Ошибка при рассчёте ответа!\nМассив был пуст!");
            }
        }

        private void OnTextChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
                    // Можно сделать и через регулярку, но да по_фигу
                    _ints = arrayBox.Text.Replace(", ", ",").Replace(" ", ",").ToArray<int>();
                }
                // Нам ничего не нужно ловить здесь
                catch (FormatException)
                { }
            }
            modifiedFromCode = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_ints == null || _ints.Length == 0)
            {
                MessageBox.Show("Отменено.\n\nЕсть ли смысл сохранять пустой массив?");
                return;
            }
            SaveFileDialog dialog = new() { CheckPathExists=true, 
                                            Filter= "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*" };
            if (dialog.ShowDialog() == true)
                _ints.Save(dialog.FileName);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _ints = BetterArray.Open<int>(dialog.FileName);
                    arrayBox.Text = string.Join(", ", _ints);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ошибка чтения файла!\n\n" +
                                    "Открываемый файл не соответствует формату, поддерживаемым приложением.");
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            modifiedFromCode = true;
            BetterArray.Clear(ref _ints);
            arrayBox.Text = "Массив";
            resultBox.Text = "Ответ";
        }

        private void ReadBeeMovie_Click(object sender, RoutedEventArgs e)
        {
            ShowBeeMovie();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowBeeMovie();
        }

        public void ShowBeeMovie()
        {
            if (!NO_BEE_MOVIE)
            {
                Random rnd = new();
                if (rnd.Next(10) < 2)
                {
                    using var wb = new WebClient();
                    // >:)
                    string[] bee_movie_script = wb.DownloadString("https://gist.githubusercontent.com/MattIPv4/045239bc27b16b2bcf7a3a9a4648c08a/raw/2411e31293a35f3e565f61e7490a806d4720ea7e/bee%2520movie%2520script").Split("\n");
                    for (int i = 0; i < bee_movie_script.Length - 1; i++)
                    {
                        MessageBox.Show(bee_movie_script[i], $"Выход... Прочитано: {i}/{bee_movie_script.Length - 1}");
                    }
                    MessageBox.Show("Поздравляю, ты прочитал весь сценарий `Би Муви: Пчелиный заговор`. Теперь ты можешь выйти.");
                }
            }
            Application.Current.Shutdown();
        }
    }
}
