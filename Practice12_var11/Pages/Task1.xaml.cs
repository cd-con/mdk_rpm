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
    /// Логика взаимодействия для Task1.xaml
    /// </summary>
    public partial class Task1 : Page
    {
        public Task1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int?[] coordsA = Array.ConvertAll(PointA.Text.Split(";"), s => int.TryParse(s, out int n) ? n : (int?)null);
            int?[] coordsB = Array.ConvertAll(PointB.Text.Split(";"), s => int.TryParse(s, out int n) ? n : (int?)null);

            if (coordsA.Contains(null) || coordsB.Contains(null))
            {
                MessageBox.Show("Некорректный ввод");
                return;
            }

            float sideA = GetLength((int)coordsA[0], (int)coordsB[1], (int)coordsA[0], (int)coordsA[0]);
            float sideB = GetLength((int)coordsB[0], (int)coordsA[1], (int)coordsA[0], (int)coordsA[0]);

            if (sideA == float.NaN || sideB == float.NaN)
            {
                MessageBox.Show("Координата B < координаты А");
                return;
            }

            float perim =  sideA * 2 + sideB * 2;

            float square = sideA * sideB;

            MessageBox.Show($"P = {perim};\nS = {square}");
        }

        float GetLength(int a, int b, int c, int d) => MathF.Sqrt((a - c) * 2 + (b - d) * 2);
    }
}
