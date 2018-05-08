using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfGym.Reports;

namespace WpfGym.Views
{
    /// <summary>
    /// Interaction logic for MenuReport.xaml
    /// </summary>
    public partial class MenuReport : Window
    {
        public MenuReport()
        {
            InitializeComponent();
        }

        private void Button_Sustitutos(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _window = new Reportes();
            _window.Show();
        }

        private void Button_Tardanzas(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _window = new Tardanzas();
            _window.Show();
        }

        private void Button_General(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var _window = new Clases();
            _window.Show();
        }
    }
}
