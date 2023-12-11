using Practice12_var11.Pages;
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

namespace Practice12_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Загружаем разметку
            Frame f1 = new() { Content = new Task1().Content };
            Frame f2 = new() { Content = new Task2().Content };
            ((TabItem)Tabs.Items[0]).Content = f1;
            ((TabItem)Tabs.Items[1]).Content = f2;
        }
    }
}
