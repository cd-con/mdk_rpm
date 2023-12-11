using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice12_var11.Pages
{
    /// <summary>
    /// Логика взаимодействия для Task2.xaml
    /// </summary>
    public partial class Task2 : Page
    {
        public Task2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(FSize.Text, out int bytesize)){
                MessageBox.Show($"{bytesize / 1024} кБайт\n\nСкопировано в буфер обмена!");
                Clipboard.SetText($"{bytesize / 1024} кБайт");
            }
        }
    }
}
