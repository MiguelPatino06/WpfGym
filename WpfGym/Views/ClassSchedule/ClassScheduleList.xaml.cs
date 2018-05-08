using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;

namespace WpfGym.Views.ClassSchedule
{
    /// <summary>
    /// Interaction logic for ClassScheduleList.xaml
    /// </summary>
    public partial class ClassScheduleList : Window
    {
        readonly static ClassScheduleServices _class = new ClassScheduleServices();
        private SearchClassSchedule windowSearch = new SearchClassSchedule();

        ObservableCollection<ClassScheduleModel> myCollection = new ObservableCollection<ClassScheduleModel>();
        private ObservableCollection<ClassScheduleModel> MyCollection
        {
            get { return myCollection; }
            set { myCollection = value; }
        }

        public ClassScheduleList()
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
            var list = _class.GetList();
            DataGridClassSchedule.ItemsSource = list.Count > 0 ? list.OrderBy(a => a.NameBranchOffice).ToList() : null;
            lblTotalReg.Content = "Cantidad de Registro: " + list.Count;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            try
            {

                windowSearch.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                windowSearch.ShowDialog();
            }
            catch
            {
                windowSearch = new SearchClassSchedule();
                windowSearch.AgregarEventHandler += new RoutedEventHandler(ag_AgregarEventHandler);
                windowSearch.ShowDialog();
            }

        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            ClassScheduleModel classScheduleModel = DataGridClassSchedule.SelectedItem as ClassScheduleModel;

            if (classScheduleModel != null)
            {
                int? id = classScheduleModel.Id;
                this.Close();
                var windowClassSchedule = new ClassScheduleImput(id);
                windowClassSchedule.Show();

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
            var _class = new ClassScheduleImput(null);
            _class.Show();
        }

        private void ButtonAgregarEliminar_Click(object sender, RoutedEventArgs e)
        {

            ClassScheduleModel classSchedule = DataGridClassSchedule.SelectedItem as ClassScheduleModel;

            if (classSchedule != null)
            {
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Eliminar Registro Seleccionado?";
                if (_var.ShowDialog() == true)
                {
                    int id = classSchedule.Id;
                    var result = _class.DeleteUpdate(id);

                    if (result)
                    {
                        MyCollection.Remove(classSchedule);
                        DataGridClassSchedule.ItemsSource = MyCollection;
                        DataGridClassSchedule.Items.Refresh();
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

        private void ag_AgregarEventHandler(object sender, RoutedEventArgs e)
        {
            var list =
                _class.GetSearch(int.Parse(windowSearch.CmbBranchOffice.SelectedValue.ToString()),
                    int.Parse(windowSearch.CmbWorkout.SelectedValue.ToString()),
                    int.Parse(windowSearch.CmbTrainer.SelectedValue.ToString())).ToList();
            DataGridClassSchedule.ItemsSource = list.Count > 0 ? list : null;
            DataGridClassSchedule.Items.Refresh();
            lblTotalReg.Content = "Cantidad de Registro: " + list.Count;
        }
    }
}
