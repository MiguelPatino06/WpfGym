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
using WpfGym.Views.Trainer;

namespace WpfGym.Views.Staff
{
    /// <summary>
    /// Interaction logic for StaffList.xaml
    /// </summary>
    public partial class StaffList : Window
    {
        private readonly StaffServices services = new StaffServices();
        ObservableCollection<StaffModel> myCollection = new ObservableCollection<StaffModel>();
        private ObservableCollection<StaffModel> MyCollection
        {
            get { return myCollection; }
            set { myCollection = value; }
        }
        public StaffList()
        {
            InitializeComponent();
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            StaffModel _staff = DataGridStaff.SelectedItem as StaffModel;

            if (_staff != null)
            {
                int? id = _staff.Id;
                this.Close();
                var _window = new StaffImput(id);
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
            var window = new StaffImput(null);
            window.Show();
        }

        private void ButtonAgregarEliminar_Click(object sender, RoutedEventArgs e)
        {
            StaffModel staff = DataGridStaff.SelectedItem as StaffModel;

            if (staff != null)
            {
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Eliminar Registro Selecionado?";
                if (_var.ShowDialog() == true)
                { 
                    int id = staff.Id;
                    var result = services.DeleteUpdate(id);

                    if (result)
                    {
                        MyCollection.Remove(staff);
                        DataGridStaff.ItemsSource = MyCollection;
                        DataGridStaff.Items.Refresh();
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

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyCollection = services.GetAll();
            DataGridStaff.ItemsSource = MyCollection.Count > 0 ? MyCollection.OrderBy(a => a.Name) : null;
            lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count;
        }


               
    }
}
