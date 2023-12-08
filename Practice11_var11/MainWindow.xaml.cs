using Practice11_var11.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Practice11_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                foreach (string item in File.OpenText("strs.txt").ReadToEnd().Split('\n'))
                    if (item != "")
                        StrContainer.Items.Add(item);


                foreach (string item in File.OpenText("regexps.txt").ReadToEnd().Split('\n'))
                    if (item != "")
                        RegExpContainer.Items.Add(item);
            }
            catch (IOException) { }
        }

        private void AddChange_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;
            string? dialogResult = box?.SelectedItem != null ?
                                   OpenInputWindow("Введите новое выражение", (string)box?.SelectedItem) :
                                   OpenInputWindow("Введите новое выражение");

            if (box?.SelectedItem != null)
                box.Items[box.Items.IndexOf(box.SelectedItem)] = dialogResult;
            else
                box?.Items.Add(dialogResult);
        }

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

        public string? OpenInputWindow(string value = "Введите значение", string @default = "Значение")
        {
            InputDialog dialog = new(value) { Text = @default};
            if (dialog.ShowDialog() == true)
            {
                return dialog.Text;
            }
            return null;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;

            if (box?.SelectedItem != null)
                box.Items.Remove(box.SelectedItem);
        }

        private void ValidateSelectedString(object sender, RoutedEventArgs e)
        {
            if(StrContainer.SelectedItem != null)
            {
                Regex testRegex = new((string)RegExpContainer.SelectedItem);
                MatchCollection matches = testRegex.Matches((string)StrContainer.SelectedItem);
                MessageBox.Show($"Совпадений: {matches.Count}\n{string.Join("", matches)}", "Успешно!");
            }
            else
            {
                MessageBox.Show("Не выбрана строка из левого списка!", "Невозможно проверить выражение!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void ValidateAll(object sender, RoutedEventArgs e)
        {
            string resultAccumulator = "";
            foreach (string value in StrContainer.Items)
            {
                Regex testRegex = new((string)RegExpContainer.SelectedItem);
                MatchCollection matches = testRegex.Matches(value);
                resultAccumulator += $"Строка: {value}\nСовпадений: {matches.Count}\n{string.Join("", matches.Select(x=>x.Value))}\n";
            }
            MessageBox.Show(resultAccumulator, "Успешно!");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                File.WriteAllText("strs.txt", string.Join("\n", StrContainer.Items.OfType<string>().ToArray()));
                File.WriteAllText("regexps.txt", string.Join("\n", RegExpContainer.Items.OfType<string>().ToArray()));
            }
            catch (IOException)
            {
                MessageBox.Show("Не удалось сохранить данные!", "Ошибка чтения/записи!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Мааагия
            ListBox? box = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as ListBox;

            box?.Items.Clear();
        }
    }
}
