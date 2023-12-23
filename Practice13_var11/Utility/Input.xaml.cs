using System.Windows;

namespace Practice3_var11.Utility
{
    /// <summary>
    /// Логика взаимодействия для ExceptionWindow.xaml
    /// </summary>
    public partial class Input : Window
    {
        public Input(string caption = "Input", string message = "Enter text")
        {
            InitializeComponent();
            Title = caption;
            MessageBox.Text = message;
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}