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
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using System.Reflection;
using WpfGym.Controls;

namespace WpfGym.Views.Workout
{
    /// <summary>
    /// Interaction logic for WorkoutImput.xaml
    /// </summary>
    public partial class WorkoutImput : Window
    {

        #region Variables

        private readonly WorkoutServices _workoutServices = new WorkoutServices();

        #endregion

        public WorkoutImput(int? Id)
        {
            InitializeComponent();
            if (Id != null) // Edit Item
            {
                InitializedWindow((int)Id);
            }
            else //New Item
            {
                LblId.Content = "0";
                CmbActive.SelectedIndex = 0;
                FillCombos(null, null);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            int result = 0;
            bool ValidateCode = false;
            WorkoutModel _workout = new WorkoutModel();
            _workout.CodigoWorkout = TxtCodigo.Text.Trim();
            _workout.Name = TxtName.Text.Trim();
            _workout.Description = TxtDescription.Text.Trim();
            _workout.Parent = int.Parse(CmbParent.SelectedValue.ToString());
            _workout.Color = CmbColor.brushSelectionBox.Text.Replace("System.Windows.Media.SolidColorBrush ", " ").Trim();
            _workout.Active = CmbActive.Text == "Activo";

            if ((!string.IsNullOrEmpty(TxtName.Text.Trim())) || (!string.IsNullOrEmpty(TxtCodigo.Text.Trim())))
            {
                if (LblId.Content == "0") //ADD
                {
                    var existCode = _workoutServices.GetByCode(TxtCodigo.Text.Trim());
                    if (existCode == null)
                    {
                        result = _workoutServices.Add(_workout);
                        //result = task0.Result;
                        LblId.Content = result.ToString();
                    }
                    else
                    {
                        ValidateCode = true;
                        GRDialogInformation _error01 = new GRDialogInformation();
                        _error01.Message = "El codigo ingresado pertenece a otro registro";
                        _error01.ShowDialog();
                        TxtCodigo.Focus();
                    }

                }
                else //EDIT
                {
                    int _id = int.Parse(LblId.Content.ToString());
                    _workout.Id = _id;
                    var task = _workoutServices.Update(_workout);
                    result = task ? _id : 0;
                }

                if (result > 0)
                {


                    GRDialogConsultation _var = new GRDialogConsultation();
                    _var.Message = "Registro Guardado, desea crear otro Registro?";
                    if (_var.ShowDialog() == true)
                    {
                        CleanControls();
                        TxtName.Focus();
                    }
                    else
                    {
                        var window = new WorkoutList();
                        this.Close();
                        window.ShowDialog();
                    }

                }
                else
                {
                    if (!ValidateCode)
                    {
                        GRDialogError _error = new GRDialogError();
                        _error.Message = "Ocurrion un error al guardar el resgitro";
                        _error.ShowDialog();
                    }           
                }
            }
            else
            {
                GRDialogInformation _error1 = new GRDialogInformation();
                _error1.Message = "Verificar campos obligatorios";
                _error1.ShowDialog();              
            }


        }


        private void InitializedWindow(int Id)
        {
            var result = _workoutServices.GetByID(Id);
            LblId.Content = Id.ToString();
            TxtName.Text = result.Name;
            TxtDescription.Text = result.Description;
            TxtCodigo.Text = result.CodigoWorkout;

            if (!(bool)result.Active)
                CmbActive.SelectedIndex = 1;
            else
                CmbActive.SelectedIndex = 0;

            FillCombos(result.Parent, result.Color);

        }


        private void FillCombos(int? IdParent, string Color)
        {
            //parent
            var result = _workoutServices.GetList().Where(a => a.Parent == null && a.Active == true).ToList();
            result.Insert(0, new WorkoutModel { Id = 0, Name = null });
            CmbParent.ItemsSource = result;
            CmbParent.SelectedIndex = 0;
            if (IdParent != null) CmbParent.SelectedValue = (int)IdParent;

            //color

        }


        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CleanControls()
        {
            LblId.Content = "0";
            TxtCodigo.Text = "0";
            TxtName.Text = "";
            TxtDescription.Text = "";
            CmbActive.SelectedIndex = 0;
            CmbParent.SelectedIndex = 0;
            //CmbColor.Content = "";
        }

    }
}
