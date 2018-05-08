using System;
using System.Collections.Generic;
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
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;

namespace WpfGym.Views.ClassSchedule
{

   
    /// <summary>
    /// Interaction logic for SearchClassSchedule.xaml
    /// </summary>
    public partial class SearchClassSchedule : Window
    {
        private readonly WorkoutServices workoutServices = new WorkoutServices();
        private readonly TrainerServices trainerServices = new TrainerServices();
        private readonly BranchOfficeServices officeServices = new BranchOfficeServices();
        public event RoutedEventHandler AgregarEventHandler;

        public SearchClassSchedule()
        {
            InitializeComponent();
            IntializeComboBanchOffice();
            IntializeComboWorkout();
            IntializeComboTrainer();
        }

        private void IntializeComboBanchOffice()
        {
            var result = officeServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new BranchOfficeModel {Id = 0, Name = "Todos.." });
            CmbBranchOffice.ItemsSource = result;
            CmbBranchOffice.SelectedIndex = 0;
        }

        private void IntializeComboWorkout()
        {

            var result = workoutServices.GetAll();
            //.Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result = result == null ? new List<WorkoutModel>() : result.Where(a => a.Active == true).OrderBy(a => a.Name).ToList();

            result.Insert(0, new WorkoutModel { Id = 0, Name = "Todos.."} );
            CmbWorkout.ItemsSource = result;
            CmbWorkout.SelectedIndex = 0;
        }

        private void IntializeComboTrainer()
        {
            var result = trainerServices.GetAll().Where(a => a.Active == true).OrderBy(a => a.Name).ToList();
            result.Insert(0, new TrainerModel { Id = 0, Name = "Todos.." });
            CmbTrainer.ItemsSource = result;
            CmbTrainer.SelectedIndex = 0;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            int office = int.Parse(CmbBranchOffice.SelectedValue.ToString());
            int workout = int.Parse(CmbWorkout.SelectedValue.ToString());
            int trainer = int.Parse(CmbTrainer.SelectedValue.ToString());

            if ((office == 1) && (workout == 1) && (trainer == 1))
            {
                MessageBox.Show("Seleciona un valor de Busqueda");
            }
            else
            {
                if (AgregarEventHandler != null)
                {
                    AgregarEventHandler(sender, e);
                }
            }

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
