using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Practice12_var11.Elements
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class HintTextBox : UserControl
    {
        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set {
                TextContainer.Text = HintText;
                SetValue(HintTextProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Property1.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintTextProperty
            = DependencyProperty.Register(
                  "HintText",
                  typeof(string),
                  typeof(HintTextBox), 
                  new PropertyMetadata("Type something...")
              );
        public SolidColorBrush hintTextBrush = new() { Color = Color.FromArgb(50, 0, 0, 0) };
        public SolidColorBrush mainTextBrush = new() { Color = Color.FromArgb(255, 0, 0, 0) };

        public HintTextBox()
        {
            InitializeComponent();

            TextContainer.GotFocus += RemoveText;
            TextContainer.LostFocus += AddText;
            //AddText(null, null);
            //TextContainer.PreviewMouseLeftButtonUp += LeaveFocus;
        }

        public void RemoveText(object sender, EventArgs e)
        {
            if (TextContainer.Text == HintText)
            {
                TextContainer.Text = "";
                TextContainer.Foreground = mainTextBrush;
            }
        }
        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextContainer.Text))
            {
                TextContainer.Text = HintText;
                TextContainer.Foreground = hintTextBrush;
            }
        }

        public void LeaveFocus(object sender, EventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
