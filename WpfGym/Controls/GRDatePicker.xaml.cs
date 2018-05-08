using System;
using System.Windows;

namespace Green_Retail.Control
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class DatePicker : Window
    {
        public DatePicker()
        {
            InitializeComponent();
            DatePickerGR.SelectedDate = Convert.ToDateTime(DateTime.Today);
            //this.ShowDialog();
        }

        private DateTime mfecha;
        public DateTime fecha { get { return mfecha; } }

        private void Bok_Click(object sender, RoutedEventArgs e)
        {
            mfecha = DatePickerGR.SelectedDate.Value;
            this.DialogResult = true;
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
