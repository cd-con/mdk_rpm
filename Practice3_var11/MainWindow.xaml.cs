using RustyUltimateLib;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Net;
using System.Windows;
using Practice3_var11.Utility;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media;

namespace Practice2_var10
{
    class IntsStorage
    {
        private int[,]? _ints;
        public DataTable? DataTableView { get; private set; }

        public int[,]? Values
        {
            get => _ints;
            set
            {
                _ints = value;
                Push();
            }
        }

        public void RandomInit(int x, int y, int min, int max)
        {
            _ints = new int[x, y];
            BetterArray.Fill(ref _ints, min, max);
            Push();
        }

        public void Reset()
        {
            BetterArray.Clear(ref _ints);
            DataTableView = null;
        }

        /// <summary>
        /// Специальный метод для обновления DataTableView.
        /// </summary>
        public void Push()
        {
            DataTableView = _ints?.ToDataTable();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool NO_BEE_MOVIE = false;
        private IntsStorage storage = new();
        // Этот флаг нужен для контроля ввода пользователя и исполнения соответствующего кода
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {
            Input inputWindow = new("Окно создания нового массива", "Допустимые маски ввода:\n<строки>x<колонки>\n<строки>x<колонки>:<нижний порог>:<верхний порог>");
            
            if (inputWindow.ShowDialog().ToSafe())
            {
                string[] dim_rndl_rndh = inputWindow.ResponseText.Split(":");
                string[] dimensions;

                // Проверка пустого ввода
                if (inputWindow.ResponseText.Length == 0)
                    return;

                // Тест на английскую Х
                if (inputWindow.ResponseText.Contains("x"))
                    dimensions = dim_rndl_rndh[0].Split("x");
                else
                    // Если это не английская - значит русская!
                    dimensions = dim_rndl_rndh[0].Split("х");

                if (dimensions.Length < 2 || !int.TryParse(dimensions[0], out int x) || !int.TryParse(dimensions[1], out int y) || x <= 0 || y <= 0)
                    MessageBox.Show("Для того, чтобы сгенерировать случайную матрицу, введите в поле `Матрица` значения, соответствующие маске <строки>x<колонки>.\n\n" +
                                    "Если нужно также указать диапазон случайных чисел, используйте маску <строки>x<колонки>:<нижний порог>:<верхний порог>");

                else
                {
                    // Защита от OutOfMemory
                    // Раньше стоял лимит на unsigned int16 (65535), но на таких значениях
                    // у ПК с 16 Гб оперативы случается прикол (см. ссылку)
                    // clck.ru/35tUqJ
                    // 
                    // Дополнительно понизил лимит, чтобы не ждать 5 минут в случае если
                    // кто-то решит попробовать сгенерировать большую матрицу
                    if (x > 255 || y > 255)
                    {
                        MessageBox.Show("Указазаны слишком большие размеры массива!");
                        return;
                    }

                    // Парсим аргументы для ГСЧ
                    int rnd_l, rnd_h;
                    switch (dim_rndl_rndh.Length)
                    {
                        // Маска <размеры матрицы>
                        case 1:
                            rnd_l = -10;
                            rnd_h = 10;
                            break;
                        // Маска <размеры матрицы>:<нижний предел>:<верхний предел>
                        case 3:
                            if (int.TryParse(dim_rndl_rndh[1], out rnd_l) && int.TryParse(dim_rndl_rndh[2], out rnd_h))
                            {
                                // Проверяем, что нижний порог ниже верхнего, иначе переворачиваем
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
                        // Остальные маски ввода шлём лесом
                        default:
                            MessageBox.Show("Некорректный ввод!\n\n" +
                                            "Такой комбинации аргументов не существует!\n" +
                                            "Используйте маску <длина массива>:<нижний порог>:<верхний порог>");
                            arrayBox.Focus();
                            arrayBox.SelectAll();
                            return;
                    }
                    storage.RandomInit(x, y, rnd_l, rnd_h);
                    // Не очень элегантное решение, но сойдёт
                    arrayBox.ItemsSource = storage.DataTableView?.DefaultView;
                }
            }            
        }

        /// <summary>
        /// Блок-схема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Solve_Click(object _, RoutedEventArgs __)
        {
            // 11. Дана матрица размера M × N. Найти количество ее строк, элементы которых 
            // упорядочены по возрастанию.
            if (storage.Values != null)
            {
                // У меня болезнь LINQ'филия
                // Тянет меня очень на LINQ
                // Бросила меня девчонка Лия
                // И сказала то, что не нужно усложнять, где не надо
                // Плюсом это ещё не очень производительно
                // И можно запутаться в дальнейшем
                int solution = Enumerable.Range(0, storage.Values.GetLength(0))
                                         .Select(x => Enumerable.Range(0, storage.Values.GetLength(1))
                                         .Select(y => storage.Values[x, y]))
                                         .Count(row => row.ToArray().IsSorted());
                resultBox.Text = $"Этот двумерный массив содержит cледующее кол-во строк, отсортированных по возрастанию: {solution}";
            }
            else
            {
                MessageBox.Show("Ошибка при рассчёте ответа!\nМассив был пуст!");
                resultBox.Text = "Ответ";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (storage.Values == null || storage.Values.Length == 0)
            {
                MessageBox.Show("Отменено.\n\nЕсть ли смысл сохранять пустой массив?");
                return;
            }
            SaveFileDialog dialog = new() { CheckPathExists=true, 
                                            Filter= "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*" };
            if (dialog.ShowDialog() == true)
                storage.Values.Save(dialog.FileName);
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
                    // КОСТЫЛЬ!
                    storage.Values = BetterArray.OpenMatrix<int>(dialog.FileName);
                    // Не очень элегантное решение, но сойдёт
                    arrayBox.ItemsSource = storage.DataTableView?.DefaultView;
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
            storage.Reset();
            arrayBox.ItemsSource = null;
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
                    for (int i = 0; i <= bee_movie_script.Length - 1; i++)
                    {
                        MessageBox.Show(bee_movie_script[i], $"Выход... Прочитано: {i}/{bee_movie_script.Length - 1}");
                    }
                    MessageBox.Show("Поздравляю, ты прочитал весь сценарий `Би Муви: Пчелиный заговор`. Теперь ты можешь выйти.");
                    // NO_BEE_MOVIE = true;
                }
            }
            // Application.Current.Shutdown();
        }

        private void Debugger_Click(object sender, RoutedEventArgs e)
        {
            if (storage.Values?.Length < 256 || storage.Values == null)
                MessageBox.Show($"NO_BEE_MOVIE = {NO_BEE_MOVIE}\n_ints = {(storage.Values == null ? "NULL" : storage.Values.PPrint(", "))}", "Отладчик");
            else
                MessageBox.Show($"Не удалось открыть отладчик: _ints содержит слишком большое кол-во значений {storage.Values?.Length}/255");
        }

        private void Push_Click(object sender, RoutedEventArgs e)
        {
            storage.Push();
            arrayBox.ItemsSource = storage.DataTableView?.DefaultView;
        }

        private void arrayBox_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int column = e.Column.DisplayIndex;
            int row = e.Row.GetIndex();

            if (int.TryParse(((TextBox)e.EditingElement).Text, out int value) && storage.Values != null)
                storage.Values[row, column] = value;
            else
                MessageBox.Show("Неверное значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
