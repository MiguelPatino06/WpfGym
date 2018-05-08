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
using PowerClub.Bussiness.Utils;
using WpfGym.Controls;
using System.Xaml;


namespace WpfGym.Views.BranchOffice
{
    /// <summary>
    /// Interaction logic for BranchOfficeImput.xaml
    /// </summary>
    public partial class BranchOfficeImput : Window
    {
        private readonly BranchOfficeServices branchOfficeServices = new BranchOfficeServices();

      

        public BranchOfficeImput(int? Id)
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
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            bool hourStart = Common.ValidateHour(mkbHoraInicio.Text);
            bool hourEnd = Common.ValidateHour(mkbHoraFin.Text);

            if (!string.IsNullOrEmpty(TxtSucursal.Text.Trim()) || !string.IsNullOrEmpty(TxtCodigo.Text.Trim()))
            {
                if ((hourStart) && (hourEnd))
                {
                    if (TimeSpan.Parse(mkbHoraInicio.Text) < TimeSpan.Parse(mkbHoraFin.Text))
                    {
                        int result = 0;
                        BranchOfficeModel _class = new BranchOfficeModel();
                        _class.Code = TxtCodigo.Text.Trim();
                        _class.Name = TxtSucursal.Text.Trim();
                        _class.Adress = TxtDir.Text.Trim();
                        _class.Observation = TxtObs.Text.Trim();
                        _class.StarHour = TimeSpan.Parse(mkbHoraInicio.Text);
                        _class.EndHour = TimeSpan.Parse(mkbHoraFin.Text);
                        _class.Active = CmbActive.Text == "Activo";

                        var existBranchOffice = branchOfficeServices.GetByCode(TxtCodigo.Text.Trim());

                        if (LblId.Content == "0") //ADD
                        {
                            if (existBranchOffice == null)
                            {
                                result = branchOfficeServices.Add(_class);
                                LblId.Content = result.ToString();
                            }
                            else
                            {
                                GRDialogInformation _error0 = new GRDialogInformation();
                                _error0.Message = "El Codigo ingresado ya esta en uso";
                                _error0.ShowDialog();
                                TxtCodigo.Focus();
                            }
                        }
                        else //EDIT
                        {
                            int _id = int.Parse(LblId.Content.ToString());
                            if ((existBranchOffice == null) || (existBranchOffice.Id == _id))
                            {
                                _class.Id = _id;
                                var task = branchOfficeServices.Update(_class);
                                result = task ? _id : 0;
                            }                          
                        }
                        if (result > 0)
                        {
                            
                            GRDialogConsultation _var = new GRDialogConsultation();
                            _var.Message = "Registro Guardado, desea crear otro Registro?";
                            if (_var.ShowDialog() == true)
                            {
                                CleanControls();
                                TxtSucursal.Focus();
                            }
                            else
                            {
                                var window = new BranchOfficeList();
                                this.Close();
                                window.ShowDialog();
                            }
                        }
                        else
                        {
                            GRDialogError _error = new GRDialogError();
                            _error.Message = "Ocurrion un error al guardar el resgitro";
                            _error.ShowDialog();                           
                        }
                    }
                    else
                    {
                        GRDialogInformation _error1 = new GRDialogInformation();
                        _error1.Message = "La Hora de Inicio no puede ser mayor a la Hora Fin";
                        _error1.ShowDialog();
                    }

                }
                else
                {
                    GRDialogInformation _error2 = new GRDialogInformation();
                    _error2.Message = "Verificar campos Obligatorios / Formato de Horas";
                    _error2.ShowDialog();
                }
            }
            else
            {
                GRDialogInformation _error2 = new GRDialogInformation();
                _error2.Message = "Verificar campos Obligatorios";
                _error2.ShowDialog();
            }
        }

        private void InitializedWindow(int Id)
        {
            var result = branchOfficeServices.GetByID(Id);
            LblId.Content = Id.ToString();
            TxtCodigo.Text = result.Code;
            TxtSucursal.Text = result.Name;
            TxtDir.Text = result.Adress;
            TxtObs.Text = result.Observation;
            mkbHoraInicio.Text = result.StringStartHour;
            mkbHoraFin.Text = result.StringEndHour;

            if (!(bool)result.Active)
                CmbActive.SelectedIndex = 1;
            else
                CmbActive.SelectedIndex = 0;

            
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mkbHoraInicio_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            mkbHoraInicio.Text = mkbHoraInicio.Text.Replace("_", "0");
        }

        private void mkbHoraFin_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            mkbHoraFin.Text = mkbHoraFin.Text.Replace("_", "0");
        }

        private void CleanControls()
        {
            LblId.Content = "0";
            TxtCodigo.Text = "";
            TxtSucursal.Text = "";
            TxtObs.Text = "";
            TxtDir.Text = "";
            mkbHoraInicio.Text = "";
            mkbHoraFin.Text = "";
            CmbActive.SelectedIndex = 0;
        }
    }
}
