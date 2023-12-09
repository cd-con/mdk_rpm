using Microsoft.Win32;
using Practice11_var11.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// TODO Закомментировать всё
namespace Practice11_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string resultSeparator = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadStrContainer("strs.txt");
            LoadRegexContainer("regexps.txt");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void LoadStrContainer(string path)
        {
            try
            {
                foreach (string item in File.OpenText(path).ReadToEnd().Split('\n'))
                    if (item != "")
                        StrContainer.Items.Add(item);
            }
            catch (IOException) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void LoadRegexContainer(string path)
        {
            try
            {
                foreach (string item in File.OpenText(path).ReadToEnd().Split('\n'))
                    if (item != "")
                        RegexContainer.Items.Add(item);
            }
            catch (IOException) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void DumpStrContainer(string path)
        {
            File.WriteAllText(path, string.Join("\n", StrContainer.Items.OfType<string>().ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void DumpRegexContainer(string path)
        {
            File.WriteAllText(path, string.Join("\n", RegexContainer.Items.OfType<string>().ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChange_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;

            string? dialogResult = box?.SelectedItem != null ?
                                   OpenInputWindow("Введите новое выражение", (string)box?.SelectedItem) :
                                   OpenInputWindow("Введите новое выражение");

            if (string.IsNullOrEmpty(dialogResult))
                return;

            if (box?.SelectedItem != null)
                box.Items[box.Items.IndexOf(box.SelectedItem)] = dialogResult;
            else
                box?.Items.Add(dialogResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxRMB_Handler(object sender, MouseButtonEventArgs e)
        {
            ListBox? box = sender as ListBox;

            if (box?.SelectedItem != null)
            {
                (box.ContextMenu.Items[2] as MenuItem).Header = "Изменить";
                (box.ContextMenu.Items[3] as MenuItem).IsEnabled = true;
                if (box.ContextMenu.Items.Count > 5)
                    (box.ContextMenu.Items[5] as MenuItem).IsEnabled = true;
            }
            else
            {
                (box.ContextMenu.Items[2] as MenuItem).Header = "Создать";
                (box.ContextMenu.Items[3] as MenuItem).IsEnabled = false;
                if (box.ContextMenu.Items.Count > 5)
                    (box.ContextMenu.Items[5] as MenuItem).IsEnabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static string OpenInputWindow(string value = "Введите значение", string @default = "Значение")
        {
            InputDialog dialog = new(value) { Text = @default };
            if (dialog.ShowDialog() == true)
            {
                return dialog.Text;
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmenuDelete_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;

            if (box?.SelectedItem != null)
                box.Items.Remove(box.SelectedItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmenuValidateSelectedString_Click(object sender, RoutedEventArgs e)
        {
            if (StrContainer.SelectedItem != null)
            {
                Regex testRegex = new((string)RegexContainer.SelectedItem);
                MatchCollection matches = testRegex.Matches((string)StrContainer.SelectedItem);
                MessageBox.Show($"Совпадений: {matches.Count}\n{string.Join(resultSeparator, matches)}", "Успешно!");
            }
            else
            {
                MessageBox.Show("Не выбрана строка из левого списка!", "Невозможно проверить выражение!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmenuValidateAll_Click(object sender, RoutedEventArgs e)
        {
            string resultAccumulator = "";
            foreach (string value in StrContainer.Items)
            {
                Regex testRegex = new((string)RegexContainer.SelectedItem);
                MatchCollection matches = testRegex.Matches(value);
                resultAccumulator += $"Строка: {value}\nСовпадений: {matches.Count}\n{string.Join(resultSeparator, matches.Select(x => x.Value))}\n\n";
            }
            MessageBox.Show(resultAccumulator, "Успешно!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                DumpStrContainer("strs.txt");
                DumpRegexContainer("regexps.txt");
            }
            catch (IOException)
            {
                MessageBox.Show("Не удалось сохранить данные!", "Ошибка чтения/записи!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmenuClear_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;

            box?.Items.Clear();
        }

        /// <summary>
        /// Обработчик нажатий внутри ListBox'а. Нужен для сброса выделения элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContainerClick_Handler(object sender, MouseButtonEventArgs e)
        {
            // Это делалось ТАК просто?
            ListBox? box = sender as ListBox;

            if (box.SelectedItem != null)
                box.SelectedItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new() { };

            MenuItem? menuItem = sender as MenuItem;

            if (dialog.ShowDialog() == true)
            {
                switch (menuItem?.Tag)
                {
                    case "TestString":
                        LoadStrContainer(dialog.FileName);
                        break;
                    case "RegEx":
                        LoadRegexContainer(dialog.FileName);
                        break;
                    default:
                        throw new NotImplementedException($"MenuItem tag {menuItem?.Tag} is not implemented!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new() { };

            MenuItem? menuItem = sender as MenuItem;

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    switch (menuItem?.Tag)
                    {
                        case "TestString":
                            DumpStrContainer(dialog.FileName);
                            break;
                        case "RegEx":
                            DumpRegexContainer(dialog.FileName);
                            break;
                        default:
                            throw new NotImplementedException($"MenuItem tag {menuItem?.Tag} is not implemented!");
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Не удалось сохранить данные!", "Ошибка чтения/записи!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MenuClear_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? menuItem = sender as MenuItem;

            switch (menuItem?.Tag)
            {
                case "TestString":
                    StrContainer.Items.Clear();
                    break;
                case "RegEx":
                    RegexContainer.Items.Clear();
                    break;
                default:
                    throw new NotImplementedException($"MenuItem tag {menuItem?.Tag} is not implemented!");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDefineSeparator_Click(object sender, RoutedEventArgs e)
        {
            resultSeparator = OpenInputWindow("Введите разделитель", resultSeparator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            // Думали что здесь будет что-то полезное >:)
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                UseShellExecute = true
            });
        }
    }
}
