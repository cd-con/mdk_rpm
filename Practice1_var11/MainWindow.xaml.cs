using System.Collections.Generic;
using System.Windows;
using Lib_11;
namespace Practice1_var11
{
    public partial class MainWindow : Window
    {
        private List<int> _list = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegenTask()
        {
            if (int.TryParse(NumInput.Text, out int n))
            {
                _list.RegenerateList(n);
                RandValDisplay.Text = string.Join(", ", _list);
            }
            else
            {
                MessageBox.Show("невалидный ввод :wheelchair:");
            }
        }

        private void ComputeTask()
        {
            if (_list.Count > 0)
            {
                ResultDisplay.Text = _list.GetMul().ToString();
            }
            else
            {
                MessageBox.Show("ШОЙГУ!!! ГЕРАСИМОВ!!! ГДЕ ЭЛЕМЕНТЫ В СПИСКЕ???\n\n(пустой список)");
            }
        }

        private void GenerateRandList_Click(object sender, RoutedEventArgs e)
        {
            RegenTask();
        }

        private void Compute_Click(object sender, RoutedEventArgs e)
        {
            ComputeTask();
        }

        private void ComputeAll_Click(object sender, RoutedEventArgs e)
        {
            RegenTask();
            ComputeTask();
        }
    }
}
