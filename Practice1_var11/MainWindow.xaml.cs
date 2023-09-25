using System.Collections.Generic;
using System.Windows;
using Lib_11;
using RustyUltimateLib;
namespace Practice1_var11
{
    public partial class MainWindow : Window
    {
        private List<int> _list = new();
        public MainWindow(){ InitializeComponent(); }

        private void RegenerateList(){ 
            _list = Practice1.RegenerateList(Lib.ValidatedInput(range.Text, new IntRange(1), (a) => MessageBox.Show(a).ToString())); 
            generatedRange.Text = string.Join(", ", _list); 
        }

        private void Compute() { 
            if (_list.Count > 0) { 
                result.Text = _list.GetMul().ToString(); 
            } else { 
                MessageBox.Show("ШОЙГУ!!! ГЕРАСИМОВ!!! ГДЕ ЭЛЕМЕНТЫ В СПИСКЕ???\n\n(пустой список)"); 
            } 
        }

        private void RegenerateList_Click(object sender, RoutedEventArgs e) => RegenerateList();

        private void Compute_Click(object sender, RoutedEventArgs e) => Compute();

        private void Regenerate_n_Compute_Click(object sender, RoutedEventArgs e) { 
            RegenerateList(); 
            Compute(); 
        }
    }
}
