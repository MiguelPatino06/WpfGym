using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;

namespace WpfGym.Views.Workout
{
    /// <summary>
    /// Interaction logic for WorkoutList.xaml
    /// </summary>
    public partial class WorkoutList : Window
    {
        WorkoutServices _workout = new WorkoutServices();
        ObservableCollection<WorkoutModel> myCollection = new ObservableCollection<WorkoutModel>();
        private ObservableCollection<WorkoutModel> MyCollection
        {
            get { return myCollection; }
            set { myCollection = value; }
        }
        public WorkoutList()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Updatewindows();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Updatewindows()
        {
            MyCollection = _workout.GetList();
            DataGridWorkout.ItemsSource = MyCollection.Count > 0 ? MyCollection.OrderBy(a=> a.Name) : null;
            lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            WorkoutModel _workout = DataGridWorkout.SelectedItem as WorkoutModel;

            if (_workout != null)
            {
                int? id = _workout.Id;
                this.Close();
                var _window = new WorkoutImput(id);
                _window.Show();
            }
            else
            {
                GRDialogInformation _msg = new GRDialogInformation();
                _msg.Message = "Debe Seleccionar un Registro";
                _msg.ShowDialog();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var _workout = new WorkoutImput(null);
            _workout.Show();
        }

        private void ButtonAgregarEliminar_Click(object sender, RoutedEventArgs e)
        {
            WorkoutModel workout = DataGridWorkout.SelectedItem as WorkoutModel;

            if (workout != null)
            {
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Eliminar Registro Selecionado?";
                if (_var.ShowDialog() == true)
                {
                    int id = workout.Id;
                    var result = _workout.DeleteUpdate(id);

                    if (result)
                    {
                        MyCollection.Remove(workout);
                        DataGridWorkout.ItemsSource = MyCollection.OrderBy(a=> a.Name);
                        DataGridWorkout.Items.Refresh();
                        lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count;
                    }
                }
            }
            else
            {
                GRDialogInformation _msg = new GRDialogInformation();
                _msg.Message = "Debe Seleccionar un Registro";
                _msg.ShowDialog();
            }
        }
    }
}
