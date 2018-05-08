using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;

namespace WpfGym.Views.BranchOffice
{
    /// <summary>
    /// Interaction logic for BranchOfficeList.xaml
    /// </summary>
    public partial class BranchOfficeList : Window
    {
        private readonly BranchOfficeServices branchOfficeServices = new BranchOfficeServices();


        ObservableCollection<BranchOfficeModel> myCollection = new ObservableCollection<BranchOfficeModel>();
        private ObservableCollection<BranchOfficeModel> MyCollection
        {
            get { return myCollection; }
            set { myCollection = value; }
        }


        public BranchOfficeList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyCollection =  branchOfficeServices.GetAll();
            DataGridBranchOffice.ItemsSource = MyCollection.OrderBy(a => a.Name); 
            lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count; 
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void ButtonAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var branchOffice = new BranchOfficeImput(null);
            branchOffice.Show();
        }

        private void ButtonAgregarEliminar_Click(object sender, RoutedEventArgs e)
        {                             
            BranchOfficeModel branchOffice = DataGridBranchOffice.SelectedItem as BranchOfficeModel;

            if (branchOffice != null)
            {
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Eliminar Registro Selecionado?";
                if (_var.ShowDialog() == true)
                {
                    int id = branchOffice.Id;
                    var result = branchOfficeServices.DeleteUpdate(id);

                    if (result)
                    {
                        MyCollection.Remove(branchOffice);
                        DataGridBranchOffice.ItemsSource = MyCollection;
                        DataGridBranchOffice.Items.Refresh();
                        lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count;
                    }
                }
            }
            else
            {
                GRDialogInformation _var = new GRDialogInformation();
                _var.Message = "Debe Seleccionar un Registro";
                _var.ShowDialog();
            }
        }


        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            BranchOfficeModel branchOfficeModelModel = DataGridBranchOffice.SelectedItem as BranchOfficeModel;

            if (branchOfficeModelModel != null)
            {
                int? id = branchOfficeModelModel.Id;
                this.Close();
                var _window = new BranchOfficeImput(id);
                _window.Show();
            }
            else
            {
                GRDialogInformation _var = new GRDialogInformation();
                _var.Message = "Debe Seleccionar un Registro";
                _var.ShowDialog();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
