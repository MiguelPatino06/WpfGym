using System;
using System.Collections.Generic;
using System.Configuration;
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
using PowerClub.Bussiness.Services;

namespace WpfGym.Views.ClassSchedule
{
    /// <summary>
    /// Interaction logic for ClassScheduleReviewList.xaml
    /// </summary>
    public partial class ClassScheduleReviewList : Window
    {
        private ClassScheduleReviewServices services = new ClassScheduleReviewServices();

        public ClassScheduleReviewList(int? Id, DateTime? d1, DateTime? d2)
        {
            InitializeComponent();

            if (Id == null)
            {
                DateTime _date = DateTime.Now;
                d1 = new DateTime(_date.Year, _date.Month, 1);
                int lastDayOfMonth = DateTime.DaysInMonth(_date.Year, _date.Month);
                d2 = new DateTime(_date.Year, _date.Month, lastDayOfMonth);
                Id = 0;

                
            }


            services.Delay = ConfigurationSettings.AppSettings.Get("Delaytime");
            var list = services.GetAllByParameter((int) Id, (DateTime) d1, (DateTime) d2).ToList();
            DataGridClassScheduleRev.ItemsSource = list.Count > 0 ? list.OrderByDescending(a => a.Id) : null;
            DataGridClassScheduleRev.Items.Refresh();
            lblTotalReg.Content = "Cantidad de Registro: " + list.Count;

        }

        private SearchScheRev windowSearch = new SearchScheRev();

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            try
            {

                windowSearch.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                windowSearch.ShowDialog();
            }
            catch
            {
                windowSearch = new SearchScheRev();
                windowSearch.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                windowSearch.ShowDialog();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void ag_AgregarEventHandler(object sender, RoutedEventArgs e)
        {
            services.Delay = ConfigurationSettings.AppSettings.Get("Delaytime");
            var list =
                services.GetAllByParameter(int.Parse(windowSearch.CmbBranchOffice.SelectedValue.ToString()),
                    DateTime.Parse(windowSearch.DpkDesde.ToString()), DateTime.Parse(windowSearch.DpkHasta.ToString()))
                    .ToList();
            DataGridClassScheduleRev.ItemsSource = list.Count > 0 ? list.OrderByDescending(a => a.Id) : null;
            DataGridClassScheduleRev.Items.Refresh();
            lblTotalReg.Content = "Cantidad de Registro: " + list.Count;
        }
    }
}