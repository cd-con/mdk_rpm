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
using Lib_11;

namespace Practice8_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public float money = 0f;
        public int pivo = 0;
        public int candies = 0;

        Batya batya = new("Валера");
        SinaKorzina son;
        public MainWindow()
        {
            InitializeComponent();
            son = new("Валерик", batya, "Валерьивич");
        }

        private void BatyaTalkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pivo > 1)
            {
                pivo--;
                MessageBox.Show(batya.Talk());
            }
            else
            {
                MessageBox.Show("А ГДЕ ПИВО??");
            }

        }
    }
}
