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
using WpfGym.Core;
using WpfGym.Reports;
using WpfGym.Views;
using WpfGym.Views.BranchOffice;
using WpfGym.Views.ClassSchedule;
using WpfGym.Views.ExportFile;
using WpfGym.Views.Staff;
using WpfGym.Views.Trainer;
using WpfGym.Views.Workout;

namespace WpfGym
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        
        public Index()
        {
            InitializeComponent();
        }

        private void Button_BranchOffice(object sender, RoutedEventArgs e)
        {
            var branchOffice = new BranchOfficeList();
            branchOffice.Show();
        }

        private void Button_Trainer(object sender, RoutedEventArgs e)
        {
            var trainer = new TrainerList();
            trainer.Show();
        }

        private void Button_ClassSchedule(object sender, RoutedEventArgs e)
        {
            var _classSchedule = new MainWindow();
            _classSchedule.Show();
        }

        private void Button_Class(object sender, RoutedEventArgs e)
        {
            var _class = new ClassScheduleList();
            _class.Show();
        }

        private void Button_Workout(object sender, RoutedEventArgs e)
        {
            var _workout = new WorkoutList();
            _workout.Show();
        }

        private void Button_ClassScheduleReviewList(object sender, RoutedEventArgs e)
        {
            var _classReview = new ClassScheduleReviewList(null, null, null);
            _classReview.Show();
        }

        private void Button_Staff(object sender, RoutedEventArgs e)
        {
            var _staff = new StaffList();
            _staff.Show();
        }

        private void Button_Reports(object sender, RoutedEventArgs e)
        {
            var _report = new MenuReport();
            _report.Show();
        }


        private void Access(int code)
        {
            switch (code)
            {
                case 1: // Administrador

                    break;
                case 2: // Staff

                    break;
                case 3: // Operador

                    break;
            }
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
            var window = new Export();
            window.Show();
        }
    }
}
