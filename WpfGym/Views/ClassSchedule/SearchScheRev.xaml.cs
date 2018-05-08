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
    /// Interaction logic for SearchScheRev.xaml
    /// </summary>
    public partial class SearchScheRev : Window
    {
        private readonly BranchOfficeServices officeServices = new BranchOfficeServices();
        private readonly ClassScheduleReviewServices classScheduleRevServices = new ClassScheduleReviewServices();
        
        public event RoutedEventHandler AgregarEventHandler;

        public SearchScheRev()
        {
            InitializeComponent();
            IntializeComboBanchOffice();
        
            DateTime _date = DateTime.Now;
            DpkDesde.SelectedDate = new DateTime(_date.Year, _date.Month, 1);
            int lastDayOfMonth = DateTime.DaysInMonth(_date.Year, _date.Month);
            DpkHasta.SelectedDate = new DateTime(_date.Year, _date.Month, lastDayOfMonth);
        }

        private void IntializeComboBanchOffice()
        {
            CmbBranchOffice.ItemsSource = officeServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            CmbBranchOffice.SelectedIndex = 0;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdB = int.Parse(CmbBranchOffice.SelectedValue.ToString());
                DateTime dt1 = Convert.ToDateTime(DpkDesde.Text);
                DateTime dt2 = Convert.ToDateTime(DpkHasta.Text);

                if (dt1 < dt2)
                {
                    if (AgregarEventHandler != null)
                    {
                        AgregarEventHandler(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("verificar fechas ingresadas");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }                                 
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
