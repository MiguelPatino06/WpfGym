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

namespace WpfGym.Views.Trainer
{
    /// <summary>
    /// Interaction logic for TrainerList.xaml
    /// </summary>
    public partial class TrainerList : Window
    {
        TrainerServices _trainer = new TrainerServices();
        ObservableCollection<TrainerModel> myCollection = new ObservableCollection<TrainerModel>();
        private ObservableCollection<TrainerModel> MyCollection
        {
            get { return myCollection; }
            set { myCollection = value; }
        }
        public TrainerList()
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
            MyCollection = _trainer.GetAll();
            DataGridTrainer.ItemsSource = MyCollection.Count > 0 ? MyCollection.OrderBy(a=> a.Name) : null;
            lblTotalReg.Content = "Cantidad de Registro: " + MyCollection.Count;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {
            TrainerModel _trainer = DataGridTrainer.SelectedItem as TrainerModel;

            if (_trainer != null)
            {
                int? id = _trainer.Id;
                this.Close();
                var _window = new TrainerImput(id);
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
            var _window = new TrainerImput(null);
            _window.Show();
        }

        private void ButtonAgregarEliminar_Click(object sender, RoutedEventArgs e)
        {
            TrainerModel trainer = DataGridTrainer.SelectedItem as TrainerModel;

            if (trainer != null)
            {
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Eliminar Registro Selecionado?";
                if (_var.ShowDialog() == true)
                {
                    int id = trainer.Id;
                    var result = _trainer.DeleteUpdate(id);

                    if (result)
                    {
                        MyCollection.Remove(trainer);
                        DataGridTrainer.ItemsSource = MyCollection;
                        DataGridTrainer.Items.Refresh();
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
