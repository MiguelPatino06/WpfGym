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
using DPFP;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;
using Fingerprint.Integration.DigitalPersona4500;

namespace WpfGym.Views.Staff
{

    

    /// <summary>
    /// Interaction logic for StaffImput.xaml
    /// </summary>
    public partial class StaffImput : Window
    {

        private class _template
        {
            public byte[] _byte { get; set; }
            public string _size { get; set; }

        }


        private readonly StaffServices staffServices = new StaffServices();

        private byte[] _huella1 { get; set; }
        private byte[] _huella2 { get; set; }

        public StaffImput(int? Id)
        {
            InitializeComponent();
            if (Id != null) // Edit Item
            {
                LblId.Content = Id.ToString();
                InitializedWindow((int)Id);
            }
            else //New Item
            {
                LblId.Content = "0";
                CmbActive.SelectedIndex = 0;
            }
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            int result = 0;
            StaffModel staff = new StaffModel();
            staff.Name = TxtName.Text.Trim();
            staff.Code = TxtCode.Text.Trim();
            staff.Active = CmbActive.Text == "Activo";
            staff.Huella1 = _huella1;
            staff.Huella2 = _huella2;


            if ((!string.IsNullOrEmpty(TxtName.Text.Trim()))|| (!string.IsNullOrEmpty(TxtCode.Text.Trim())))
            {
                if (LblId.Content == "0") //ADD
                {
                    result = staffServices.Add(staff);
                    //result = task0.Result;
                    LblId.Content = result.ToString();
                }
                else //EDIT
                {
                    int _id = int.Parse(LblId.Content.ToString());
                    staff.Id = _id;
                    var task = staffServices.Update(staff);
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
                        var window = new StaffList();
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
                GRDialogInformation _var = new GRDialogInformation();
                _var.Message = "Verificar campos obligatorios";
                _var.ShowDialog();
                
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CleanControls()
        {
            LblId.Content = "0";
            TxtName.Text = "";
            TxtCode.Text = "";
            TxtCode.Focus();
        }

        private void Button_Huella(object sender, RoutedEventArgs e)
        {
                    
            //if (DigitalPersona4500.Instance.Enroll())
            //{

            //    _huella1 = DigitalPersona4500.Instance.FingerPrint[0].Bytes.ToArray();
            //    _huella2 = DigitalPersona4500.Instance.FingerPrint[1].Bytes.ToArray();
                               
            //}


            if (EnrollmentFingerPrintEnroller(new Sample()))
            {
                _huella1 = DigitalPersona4500.Instance.FingerPrint[0].Bytes.ToArray();
                _huella2 = DigitalPersona4500.Instance.FingerPrint[1].Bytes.ToArray();
            }

        }

    private void InitializedWindow(int Id)
        {

            var result = staffServices.GetById(Id);
            LblId.Content = Id.ToString();
            TxtCode.Text = result.Code;
            TxtName.Text = result.Name;


            if (!(bool)result.Active)
                CmbActive.SelectedIndex = 1;
            else
                CmbActive.SelectedIndex = 0;

            if (result.Huella1 != null)
                LblMsgHuella.Visibility = Visibility.Hidden;
            else
                LblMsgHuella.Visibility = Visibility.Visible;



        }

        #region FINGERPRINT ENROLLMENT

        private DPFP.Processing.Enrollment Enroller;

        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate;


        public bool EnrollmentFingerPrintEnroller(DPFP.Sample Sample)
        {
            bool IsOk = false;
            try
            {

                List<DigitalPersona4500Template> dpTemplate = new List<DigitalPersona4500Template>();
                var temp = new List<_template>();

                if (DigitalPersona4500.Instance.Enroll())
                {
                    foreach (DigitalPersona4500Template t in DigitalPersona4500.Instance.FingerPrint)
                    {
                        temp.Add(new _template { _byte = t.Bytes.ToArray(), _size = t.Size.ToString() });
                    }
                    IsOk = true;
                }


                var z = temp;

            }
            catch (Exception ex)
            {

            }
            return IsOk;
        }




        #endregion

    }
}
