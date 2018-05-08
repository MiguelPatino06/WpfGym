using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using DPFP.Processing;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using DPFP.Verification;
using Microsoft.Win32;
using WpfGym.Controls;
using WpfGym.FingerPrint;
using Fingerprint.Integration.DigitalPersona4500;

namespace WpfGym.Views.Trainer
{
    /// <summary>
    /// Interaction logic for TrainerImput.xaml
    /// </summary>
    public partial class TrainerImput : Window
    {

        private class _template
        {
            public byte[] _byte { get; set; }
            public string _size { get; set; }

        }


        private readonly TrainerServices trainerServices = new TrainerServices();

        private byte[] _huella1 { get; set; }
        private byte[] _huella2 { get; set; }
        private string _photoLink { get; set; }
      

        

        public TrainerImput(int? Id)
        {
            TextBox txtPathImg = new TextBox();
            InitializeComponent();
            Enroller = new DPFP.Processing.Enrollment(); // Create an enrollment.
            _photoLink = string.Empty;
            if (Id != null) // Edit Item
            {
                InitializedWindow((int) Id);
            }
            else //New Item
            {
                LblId.Content = "0";
                CmbActive.SelectedIndex = 0;
                FillCombos(null);

                _photoLink = string.Empty;

                ImgTrainer.Source = new BitmapImage(new Uri("../../Component/Resources/POSLoginIDLogoOperador.png", UriKind.Relative));
               // ImgTrainer.Source = new BitmapImage(new Uri("Resources/POSLoginIDLogoOperador.png", UriKind.Relative));
                LblPathHuella.Content = "Doble Clic sobre la imagen";

            }
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            int result = 0;
            bool ValidateCode = false;
            TrainerModel _trainer = new TrainerModel();
            _trainer.Code = TxtCodigo.Text.Trim();
            _trainer.Name = TxtName.Text.Trim();
            _trainer.Description = TxtDescription.Text.Trim();
            _trainer.Type = int.Parse(CmbType.SelectedValue.ToString());
            _trainer.Active = CmbActive.Text == "Activo";
            _trainer.Huella1 = _huella1;
            _trainer.Huella2 = _huella2;
            if(String.IsNullOrEmpty(TxtCosto.Text.Trim()))
            {
                _trainer.Price = 0;
            }
            else
            {
                _trainer.Price = Double.Parse(TxtCosto.Text.Trim());
            }
            
            _trainer.PhotoLink = _photoLink == string.Empty? null: _photoLink;


            if ((!string.IsNullOrEmpty(TxtName.Text.Trim())) || (!string.IsNullOrEmpty(TxtCodigo.Text.Trim())))
            {
                if (LblId.Content == "0") //ADD
                {
                    var existCode = trainerServices.GetByCode(TxtCodigo.Text.Trim());
                    if (existCode == null)
                    {
                        result = trainerServices.Add(_trainer);
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
                    _trainer.Id = _id;
                    var task = trainerServices.Update(_trainer);
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
                        var window = new TrainerList();
                        this.Close();
                        window.ShowDialog();
                    }

                }
                else
                {
                    if (!ValidateCode)
                    {
                        GRDialogError _error = new GRDialogError();
                        _error.Message = "Error";
                        _error.ShowDialog();
                    }
                }
            }
            else
            {
                GRDialogError _error = new GRDialogError();
                _error.Message = "Verificar campos obligatorios";
                _error.ShowDialog();              
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void InitializedWindow(int Id)
        {
            var result = trainerServices.GetByID(Id);
            LblId.Content = Id.ToString();
            TxtCodigo.Text = result.Code;
            TxtName.Text = result.Name;
            TxtDescription.Text = result.Description;
            TxtCosto.Text = result.Price.ToString();

            if (result.PhotoLink != null)
            {
                //_photoLink = result.PhotoLink;
                //BitmapImage myBitmapImage = new BitmapImage(new Uri(result.PhotoLink, UriKind.Relative));
                if(System.IO.File.Exists(result.PhotoLink))
                ImgTrainer.Source = new BitmapImage(new Uri(result.PhotoLink));

                LblPathHuella.Content = GetFileNameNoExt(result.PhotoLink.Trim());
            }
            else
            {
                _photoLink = string.Empty;
                ImgTrainer.Source = new BitmapImage(new Uri("../../Component/Resources/POSLoginIDLogoOperador.png", UriKind.Relative));
                //ImgTrainer.Source =new BitmapImage(new Uri("Component/Resources/POSLoginIDLogoOperador.png", UriKind.Relative));
                LblPathHuella.Content = "Doble Clic sobre la imagen";
            }

            if (!(bool) result.Active)
                CmbActive.SelectedIndex = 1;
            else
                CmbActive.SelectedIndex = 0;

            FillCombos(result.Type);

            if (result.Huella1 != null)
                LblMsgHuella.Visibility = Visibility.Hidden;
            else
                LblMsgHuella.Visibility = Visibility.Visible;



        }

        private void FillCombos(int? IdType)
        {
            //parent
            var result = trainerServices.GetBTrainerType().OrderBy(a => a.Value).ToList();
            CmbType.ItemsSource = result;
            CmbType.SelectedIndex = 0;
            if (IdType != null) CmbType.SelectedValue = (int) IdType;

            //color

        }

        private void CleanControls()
        {
            LblId.Content = "0";
            TxtName.Text = "";
            TxtDescription.Text = "";
            CmbActive.SelectedIndex = 0;
            CmbType.SelectedIndex = 0;
            TxtName.Focus();
        }

        private void Button_Huella(object sender, RoutedEventArgs e)
        {
            if (EnrollmentFingerPrintEnroller(new Sample()))
            {
                _huella1 = DigitalPersona4500.Instance.FingerPrint[0].Bytes.ToArray();
                _huella2 = DigitalPersona4500.Instance.FingerPrint[1].Bytes.ToArray();
            }
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
                // Utils utils = new Utils();


                //var _instance = Fingerprint.DigitalPersona.Fingerprint.Instance;



                //Fingerprint.DigitalPersona.Fingerprint.Instance.Code = LblId.Content.ToString();
                //Fingerprint.DigitalPersona.Fingerprint.Instance.ShowEnrollingForm();

                //var template = Fingerprint.DigitalPersona.Fingerprint.Instance.Templates;

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
                
                //DPFP.Processing.Enrollment enrollment = new Enrollment();





                //template = new Template[2];


                //  DPFP.Processing.FeatureExtraction featureExtraction;









                //DPFP.FeatureSet features = utils.ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

                //// Check quality of the sample and add to enroller if it's good
                //if (features != null) try
                //    {
                //        Enroller.AddFeatures(features);     // Add feature set to template.
                //    }
                //    finally
                //    {
                //        // Check if template has been created.
                //        switch (Enroller.TemplateStatus)
                //        {
                //            case DPFP.Processing.Enrollment.Status.Ready:   // report success and stop capturing
                //                OnTemplate(Enroller.Template);
                //                MessageBox.Show("Click Close, and then click Fingerprint Verification.", "Power Club", MessageBoxButton.OK, MessageBoxImage.Information);
                //                utils.Stop();
                //                break;

                //            case DPFP.Processing.Enrollment.Status.Failed:  // report failure and restart capturing
                //                Enroller.Clear();
                //                utils.Stop();
                //                OnTemplate(null);
                //                utils.Start();
                //                break;
                //        }
                //    }

            }
            catch (Exception ex)
            {

            }
            return IsOk;
        }




        #endregion

        private void ImgTrainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Seleccione una Imagen";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    _photoLink = op.FileName;
                    ImgTrainer.Source = new BitmapImage(new Uri(op.FileName));
                    LblPathHuella.Content = GetFileNameNoExt(_photoLink.Trim());
                }
                else
                {
                    LblPathHuella.Content = "Doble Clic sobre la imagen";
                    _photoLink = string.Empty;
                }

            }
        }

        public string GetFileNameNoExt(string FilePathFileName)
        {
            return System.IO.Path.GetFileNameWithoutExtension(FilePathFileName);
        }
    }
}
    