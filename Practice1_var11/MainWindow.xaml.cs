using System.Collections.Generic;
using System.Windows;
using Lib_11;
using RustyUltimateLib;
namespace Practice1_var11
{
    public partial class MainWindow : Window
    {
        private List<int> _f = new List<int>();
        public MainWindow(){ InitializeComponent(); }

        private void a(){ _f = Practice1.RegenerateList(Lib.ValidatedInput(i.Text, new IntRange(1), (a) => MessageBox.Show(a).ToString())); g.Text = string.Join(", ", _f); }

        private void b() { if (_f.Count > 0) { h.Text = _f.GetMul().ToString(); } else { MessageBox.Show("ШОЙГУ!!! ГЕРАСИМОВ!!! ГДЕ ЭЛЕМЕНТЫ В СПИСКЕ???\n\n(пустой список)"); } }

        private void c(object sender, RoutedEventArgs e) => a();

        private void d(object sender, RoutedEventArgs e) => b();

        private void e(object sender, RoutedEventArgs e) { a(); b(); }
    }
}
