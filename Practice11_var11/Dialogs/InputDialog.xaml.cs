using System.Windows;


namespace Practice11_var11.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        /// <summary>
        /// Init dialog window
        /// </summary>
        public InputDialog(string label) {
            InitializeComponent(); 
            TextLabel.Text = label;
        }

        public string Text
        {
            get => InputBox.Text;
            set => InputBox.Text = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
