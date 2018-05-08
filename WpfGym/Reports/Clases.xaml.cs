using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
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
using WpfGym.Controls;

namespace WpfGym.Reports
{
    /// <summary>
    /// Interaction logic for Clases.xaml
    /// </summary>
    public partial class Clases : Window
    {
        private readonly ReportServices services = new ReportServices();
        private readonly BranchOfficeServices officeServices = new BranchOfficeServices();
        private readonly WorkoutServices workoutServices = new WorkoutServices();
        private readonly TrainerServices trainerServices = new TrainerServices();
        private readonly StaffServices staffServices = new StaffServices();

        public Clases()
        {
            InitializeComponent();
            IntializeComboBanchOffice();
            IntializeComboWorkout();
            IntializeComboTrainer();
            IntializeComboStaff();
            IntializeComboStatus();

            DateTime _date = DateTime.Now;
            DpkDesde.SelectedDate = new DateTime(_date.Year, _date.Month, 1);
            int lastDayOfMonth = DateTime.DaysInMonth(_date.Year, _date.Month);
            DpkHasta.SelectedDate = new DateTime(_date.Year, _date.Month, lastDayOfMonth);
            var x = _date.Day;
        }

        private void IntializeComboBanchOffice()
        {
            var result = officeServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new BranchOfficeModel { Id = 0, Name = null });

            CmbBranchOffice.ItemsSource = result;
            CmbBranchOffice.SelectedIndex = 0;
        }
        private void IntializeComboWorkout()
        {
            var result = workoutServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new WorkoutModel { Id = 0, Name = null });

            CmbWorkout.ItemsSource = result;
            CmbWorkout.SelectedIndex = 0;
        }
        private void IntializeComboTrainer()
        {
            var result = trainerServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new TrainerModel { Id = 0, Name = null });

            CmbTrainer.ItemsSource = result;
            CmbTrainer.SelectedIndex = 0;
        }

        private void IntializeComboStaff()
        {
            var result = staffServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new StaffModel { Id = 0, Name = null });

            CmbStaff.ItemsSource = result;
            CmbStaff.SelectedIndex = 0;
        }

        private void IntializeComboStatus()
        {
            var staff = new List<StaffModel>();

            staff.Add(new StaffModel { Id = 0, Name = null });
            staff.Add(new StaffModel { Id = 1, Name = "ABIERTO" });
            staff.Add(new StaffModel { Id = 2, Name = "CERRADO" });
            staff.Add(new StaffModel { Id = 3, Name = "CANCELADO" });

            CmbStatus.ItemsSource = new ObservableCollection<StaffModel>(staff);
            CmbStatus.SelectedIndex = 0;
        }

        private void Button_Buscar(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime d1, d2;
                d1 = Convert.ToDateTime(DpkDesde.Text);
                d2 = Convert.ToDateTime(DpkHasta.Text);

                int check = ckbAsistencia.IsChecked != null && (ckbAsistencia.IsChecked.Value) ? 1 : 0;

                if (d1 <= d2)
                {
                    ReportViewerClases.Reset();
                    ParametersClases pr = new ParametersClases
                    {
                        IdOffice = int.Parse(CmbBranchOffice.SelectedValue.ToString()),
                        IdTrainer = int.Parse(CmbTrainer.SelectedValue.ToString()),
                        IdWorkout = int.Parse(CmbWorkout.SelectedValue.ToString()),
                        IdStaff = int.Parse(CmbStaff.SelectedValue.ToString()),
                        IdStatus = int.Parse(CmbStatus.SelectedValue.ToString()),
                        DateStart = Convert.ToDateTime(DpkDesde.Text),
                        DateEnd = Convert.ToDateTime(DpkHasta.Text),
                        Asistencia  = check
                    };


                    DataTable dt = services.GetClases("GeClassScheduleReview", pr);
                    ReportDataSource ds = new ReportDataSource("DataSetClassReview", dt);
                    ReportViewerClases.LocalReport.DataSources.Add(ds);
                    ReportViewerClases.LocalReport.ReportEmbeddedResource = "WpfGym.Reports.rptClases.rdlc";

                    ReportViewerClases.RefreshReport();
                }
                else
                {
                    GRDialogInformation _var = new GRDialogInformation();
                    _var.Message = "Rango de fecha Invalido";
                    _var.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GRDialogInformation _var = new GRDialogInformation();
                _var.Message = "Error";
                _var.ShowDialog();
            }
        }
    }
}
