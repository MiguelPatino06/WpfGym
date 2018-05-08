using System.Windows;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRDialogInformation.xaml
    /// </summary>
    /// 

    public partial class GRDialogInformation : Window
    {
        public string Message
        {
            get { return MessageOut.Text; }
            set { MessageOut.Text = value; }
        }
        public GRDialogInformation()
        {
            InitializeComponent();
        }

        private void ButtonAcept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
