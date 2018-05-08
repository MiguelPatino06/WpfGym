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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PowerClub.Bussiness;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.ViewModels;
using WpfGym.Views.ClassSchedule;

namespace WpfGym
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TrainerServices fTrainerServices = new TrainerServices();
        WorkoutServices workoutServices = new WorkoutServices();
        ClassScheduleServices classSchedule = new ClassScheduleServices();

        public List<WorkoutModel> _workout { get; set; }




        public MainWindow()
        {
            InitializeComponent();
            //BindWorkout();
        }



        private void WeekScheduler_OnEventDoubleClick(object sender, ClassScheduleModel e)
        {
            ScheduleViewModel vm = grid.DataContext as ScheduleViewModel;
         
            int id = e.Id;
            var _classScheduleReview = new ClassScheduleReview(id);
            _classScheduleReview.Show();

        }

        private void WeekScheduler_OnScheduleDoubleClick(object sender, DateTime date)
        {
            ScheduleViewModel vm = grid.DataContext as ScheduleViewModel;
            //vm.NewEventCommand.Execute(date);
        }

        private void FillWorkout()
        {

        }

        //private void BindWorkout()
        //{
        //    var items = workoutServices.GetAll().Where(a=> a.Parent == null).ToList();
        //    _workout = items;
        //    DataContext = _workout;
        //}

        private void cmbWorkout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            
            int? IdBranchOffice = int.Parse(cmbBranchOffice.SelectedValue.ToString());
            int? IdLivingroom = cmbLivingroom.SelectedValue == null ? (int?)null : int.Parse(cmbLivingroom.SelectedValue.ToString());
            int? IdWorkout = cmbWorkout.SelectedValue == null ? (int?)null : int.Parse(cmbWorkout.SelectedValue.ToString());
            

            scheduleControl.NextPage(IdBranchOffice, IdLivingroom, IdWorkout);

        }

        //private void CmbWorkout_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //   cmbWorkout.ItemsSource = workoutServices.GetAll().Where(a => a.Parent == null && a.Active == true).ToList();
        //   cmbWorkout.SelectedIndex = 0;
        //}

        private void cmbBranchOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var item = cmbBranchOffice.SelectedValue.ToString();
            var items = classSchedule.GetClassScheduleBranchOfice(int.Parse(item));
            //cmbWorkout.ItemsSource = items;
            //cmbWorkout.SelectedIndex = 0;
            cmbLivingroom.ItemsSource = items;
            cmbLivingroom.SelectedIndex = 0;

            //if (items.Count > 0)
            //    cmbWorkout_SelectionChanged(null, null);
            if (items.Count > 0)
                cmbLivingroom_SelectionChanged(null, null);

        }

        private void cmbBranchOffice_OnLoaded(object sender, RoutedEventArgs e)
        {
            cmbBranchOffice.ItemsSource = classSchedule.GetBranchOfice();
            cmbBranchOffice.SelectedIndex = 0;

            //var itemLivingroom = cmbWorkout.SelectedValue.ToString();
            //var itemBranchoffice = cmbLivingroom.SelectedValue.ToString();
            //var items = classSchedule.GetClassScheduleBranchOficeAndWorkout(int.Parse(itemBranchoffice), int.Parse(itemLivingroom));

            //cmbLivingroom.ItemsSource = items;
            //cmbLivingroom.SelectedIndex = 0;
        }

        private void cmbLivingroom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           // ClassScheduleModel classSchedule = new ClassScheduleModel();

            int? IdLivingroom = cmbLivingroom.SelectedValue == null ? (int?)null : int.Parse(cmbLivingroom.SelectedValue.ToString());
            int? IdBranchOffice = int.Parse(cmbBranchOffice.SelectedValue.ToString());

            scheduleControl.NextPage(IdBranchOffice, IdLivingroom, null);



            if (IdLivingroom != null)
            {
                try
                {
                    cmbWorkout.ItemsSource = null;
                    var itemLivingroom = cmbLivingroom.SelectedValue.ToString();
                    var itemBranchoffice = cmbBranchOffice.SelectedValue.ToString();
                    var items = classSchedule.GetClassScheduleBranchOficeAndWorkout(int.Parse(itemBranchoffice), int.Parse(itemLivingroom));

                    cmbWorkout.ItemsSource = items;
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }


            //scheduleControl.NextPage(int.Parse(itemBranchoffice), int.Parse(itemLivingroom));

        }
    }
}
