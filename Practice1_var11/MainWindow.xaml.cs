using System.Collections.Generic;
using System.Windows;
using Lib_11;
using RustyUltimateLib;
namespace Practice1_var11
{
    public partial class MainWindow : Window
    {
        private List<int> _list = new List<int>();
        public MainWindow(){ InitializeComponent(); }

        private void RegenerateList()
        { 
            _list = Practice1.RegenerateList(Lib.ValidatedInput(i.Text, new IntRange(1), (a) => MessageBox.Show(a).ToString())); 
            g.Text = string.Join(", ", _list); 
        }
        private void Compute() 
        { 
            if (_list.Count > 0) 
            { 
                h.Text = _list.GetMul().ToString(); 
            } 
            else 
            { 
                MessageBox.Show("ШОЙГУ!!! ГЕРАСИМОВ!!! ГДЕ ЭЛЕМЕНТЫ В СПИСКЕ???\n\n(пустой список)");
                i.SelectAll();
            } 
        }
        private void RegenerateList_Click(object sender, RoutedEventArgs e) => RegenerateList();

        private void Compute_Click(object sender, RoutedEventArgs e) => Compute();

        private void DoAllInOne_Click(object sender, RoutedEventArgs e) { 
            RegenerateList(); 
            Compute(); 
        }

        private void i_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (i.Text.Length == 0)
            {
                i.Text = "0";
                i.SelectAll();
            }
        }

        private void i_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            i.Text = "0";
        }
    }
}
