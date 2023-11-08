using System;
using System.Windows;
using System.Windows.Controls;
using Lib_11;

namespace Practice5_var11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pair a = new();
        Pair b = new();

        RightAngled triangle = new();
        int scale = 1; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextChangeHandler(object sender, TextChangedEventArgs e)
        {

            TextBox box = sender as TextBox;
            
            if (box.Text.Length == 0)
            {
                box.Text = "0";
                box.SelectAll();
            }

            if (int.TryParse(box.Text, out int result))
            {
                switch (box.Name)
                {
                    case "nA1":
                        a.A = result;
                        break;
                    case "nA2":
                        a.B = result;
                        break;
                    case "nB1":
                        b.A = result;
                        break;
                    case "nB2":
                        b.B = result;
                        break;
                    case "MulNumPair_Scale":
                        scale = result;
                        break;

                    default:
                        //throw new ArgumentException($"Field {box.Name} does not exist in parser function.");
                        break;
                }

                if (AB != null && AC != null)
                {
                    if (AB.IsChecked == true)
                        triangle.CathetA = new(a, b);
                    if (AC.IsChecked == true)
                        triangle.CathetB = new(a, b);

                    Hypo.Text = $"({triangle.Hypotenuse?.A.A}; {triangle.Hypotenuse?.A.B}) ({triangle.Hypotenuse?.B.A};{triangle.Hypotenuse?.B.B})";
                    sqr.Text = triangle.Square.ToString();
                }
            }
            else
            {
                MessageBox.Show($"Ошибка парсинга значений из {box.Name}!\nПлохой ввод, переделывай");
                box.SelectAll();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (op1A.IsChecked == true)
                a *= scale;

            if (op1B.IsChecked == true)
                b *= scale;

            Update();
        }

        private void Update()
        {
            nA1.Text = a.A.ToString();
            nA2.Text = a.B.ToString();
            nB1.Text = b.A.ToString();
            nB2.Text = b.B.ToString();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pair result = a * b;
            if (op2A.IsChecked == true)
                a = result;

            if (op2B.IsChecked == true)
                b = result;

            Update();
        }

        private void AB_Click(object sender, RoutedEventArgs e)
        {
            a = triangle.CathetA.A;
            b = triangle.CathetA.B;
            nA1.Text = triangle.CathetA.A.A.ToString();
            nA2.Text = triangle.CathetA.A.B.ToString();
            nB1.Text = triangle.CathetA.B.A.ToString();
            nB2.Text = triangle.CathetA.B.B.ToString();
        }

        private void AC_Click(object sender, RoutedEventArgs e)
        {
            a = triangle.CathetB.A;
            b = triangle.CathetB.B;
            nA1.Text = triangle.CathetB.A.A.ToString();
            nA2.Text = triangle.CathetB.A.B.ToString();
            nB1.Text = triangle.CathetB.B.A.ToString();
            nB2.Text = triangle.CathetB.B.B.ToString();
        }
    }
}
