using System.Windows;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRDialogError.xaml
    /// </summary>
    public partial class GRDialogError : Window
    {
        public string Message
        {
            get { return MessageOut.Text; }
            set { MessageOut.Text = value; }
        }

        public GRDialogError()
        {
            InitializeComponent();
        }

        private void ButtonAcept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
