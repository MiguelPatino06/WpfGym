using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Reporting.WinForms;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;

namespace WpfGym.Reports
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        private readonly ReportServices services = new ReportServices();
        private readonly BranchOfficeServices officeServices = new BranchOfficeServices();
        private readonly WorkoutServices workoutServices = new WorkoutServices();

        public Reportes()
        {
           
            InitializeComponent();
            IntializeComboBanchOffice();
            IntializeComboWorkout();

            DateTime _date = DateTime.Now;
            DpkDesde.SelectedDate = new DateTime(_date.Year, _date.Month, 1);
            int lastDayOfMonth = DateTime.DaysInMonth(_date.Year, _date.Month);
            DpkHasta.SelectedDate = new DateTime(_date.Year, _date.Month, lastDayOfMonth);
            var x = _date.Day;

        }



        private void Button_Buscar(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime d1, d2;
                d1 = Convert.ToDateTime(DpkDesde.Text);
                d2 = Convert.ToDateTime(DpkHasta.Text);
                if (d1 <= d2)
                {
                    ReportViewerSustitucionesa.Reset();
                    ParametersSustituciones pr = new ParametersSustituciones
                    {
                        IdOffice = int.Parse(CmbBranchOffice.SelectedValue.ToString()),
                        IdWorkout = int.Parse(CmbWorkout.SelectedValue.ToString()),
                        DateStart = Convert.ToDateTime(DpkDesde.Text),
                        DateEnd = Convert.ToDateTime(DpkHasta.Text),
                    };
                    DataTable dt = services.GetDataSustitutos("GetSustitute", pr);
                    ReportDataSource ds = new ReportDataSource("DataSetSustitutos", dt);
                    ReportViewerSustitucionesa.LocalReport.DataSources.Add(ds);
                    ReportViewerSustitucionesa.LocalReport.ReportEmbeddedResource = "WpfGym.Reports.rptSustituciones.rdlc";

                    ReportViewerSustitucionesa.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Rango de fecha Invalido");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }

        }

        private void IntializeComboBanchOffice()
        {
            CmbBranchOffice.ItemsSource = officeServices.GetAll().Where(a => a.Active == true).OrderBy(a=> a.Name).ToList();
            CmbBranchOffice.SelectedIndex = 0;
        }

        private void IntializeComboWorkout()
        {
            var result = workoutServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new WorkoutModel { Id = 0, Name = null });

            CmbWorkout.ItemsSource = result;
            CmbWorkout.SelectedIndex = 0;
        }
    }
}
