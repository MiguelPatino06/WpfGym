using System;
using System.Windows;

namespace Green_Retail.Control
{
    /// <summary>
    /// Interaction logic for GRDialogDate.xaml
    /// </summary>
    public partial class GRDialogDate : Window
    {
        public GRDialogDate()
        {
            //DateEnd.SelectedDate = DateTime.Now;
            //DateBegin.SelectedDate = DateTime.Now.AddDays(-7);

            InitializeComponent();

            DateEnd.SelectedDate = DateTime.Now;
            DateBegin.SelectedDate = DateTime.Now.AddDays(-15);
        }

        public DateTime BeginDate
        {
            get
            {
                return this.DateBegin.SelectedDate.Value;
            }
            set
            {
                DateBegin.SelectedDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.DateEnd.SelectedDate.Value;
            }
            set
            {
                DateEnd.SelectedDate = value;
            }
        }

        private void ButtonAcept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
