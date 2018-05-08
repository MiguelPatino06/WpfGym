using System.Windows;

namespace WpfGym.Controls
{
    /// <summary>
    /// Interaction logic for GRDialogConsultation.xaml
    /// </summary>
    public partial class GRDialogConsultation : Window
    {
        public string Message
        {
            get { return MessageOut.Text; }
            set { MessageOut.Text = value; }
        }

        public GRDialogConsultation()
        {
            InitializeComponent();
        }

        private void ButtonAcept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
