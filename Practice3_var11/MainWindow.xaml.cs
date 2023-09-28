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
        bool NO_BEE_MOVIE = false;
        private int[,]? _ints;
        private bool modifiedFromCode = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {

            modifiedFromCode = true;

            string[] dim_rndl_rndh = arrayBox.Text.Split(":");
            string[] dimensions;

            if(dim_rndl_rndh.Length == 0)
                MessageBox.Show("Пустой ввод!");

            // Тест на английскую Х
            if (arrayBox.Text.Contains("x"))
            {
                dimensions = dim_rndl_rndh[0].Split("x");
            }
            else
            {
                // Если это не английская - значит русская!
                dimensions = dim_rndl_rndh[0].Split("х");
            }
            
            if (dimensions.Length < 2 || !int.TryParse(dimensions[0], out int x) || !int.TryParse(dimensions[1], out int y) || x <= 0 || y <= 0)
            {
                MessageBox.Show("Для того, чтобы сгенерировать случайную матрицу, введите в поле `Матрица` значения, соответствующие маске <строки>x<колонки>.\n\n" +
                                "Если нужно также указать диапазон случайных чисел, используйте маску <строки>x<колонки>:<нижний порог>:<верхний порог>");
                arrayBox.Text = "Введите число";
                arrayBox.Focus();
                arrayBox.SelectAll();
            }
            else
            {
                // Защита от OutOfMemory
                // Раньше стоял лимит на unsigned int16 (65535), но на таких значениях
                // у ПК с 16 Гб оперативы случается прикол (см. ссылку)
                // clck.ru/35tUqJ
                if (x > 512 || y > 512)
                {
                    MessageBox.Show("Указазаны слишком большие размеры массива!");
                    arrayBox.Text = "Ошибка!";
                    arrayBox.Focus();
                    arrayBox.SelectAll();
                    return;
                }
                _ints = new int[x, y];
                int rnd_l, rnd_h;
                switch (dim_rndl_rndh.Length)
                {
                    case 1:
                        rnd_l = -10;
                        rnd_h = 10;
                        break;
                    case 3:
                        if (int.TryParse(dim_rndl_rndh[1], out rnd_l) && int.TryParse(dim_rndl_rndh[2], out rnd_h))
                        {
                            (rnd_l, rnd_h) = rnd_l > rnd_h ? (rnd_h, rnd_l) : (rnd_l, rnd_h);
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
                BetterArray.Fill(ref _ints, rnd_l, rnd_h);
                arrayBox.Text = _ints.PPrint(", ");
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
                int[] mins = new int[_ints.GetLength(0)];
                for (int row = 0; row < _ints.GetLength(0); row++)
                {
                    // КАР КАР КАР КАР
                    int[] cRow = _ints.GetRow(row);
                    mins[row] = cRow.Min();
                }
                resultBox.Text = $"`{mins.Max()}` из строки {Array.FindIndex(mins, i => i == mins.Max()) + 1}";
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
                    _ints = arrayBox.Text.Replace(", ", ",").Replace(" ", ",").ToMatrix<int>();
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
                    _ints = BetterArray.OpenMatrix<int>(dialog.FileName);
                    arrayBox.Text = _ints.PPrint(", ");
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
            // Небольшой багфикс, чтобы наказание два раза подряд не показывалось при закрытии
            Window_Closing(null, null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                    NO_BEE_MOVIE = true;
                }
            }
            Application.Current.Shutdown();
        }
    }
}
