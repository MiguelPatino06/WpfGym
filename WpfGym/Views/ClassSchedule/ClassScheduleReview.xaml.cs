using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DPFP;
using DPFP.Verification;
using DPFPCtlXTypeLibNET;
using DPFPShrXTypeLibNET;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using PowerClub.Bussiness.Utils;
using WpfGym.Controls;
using WpfGym.FingerPrint;
using MessageBox = System.Windows.MessageBox;
using Fingerprint.Integration.DigitalPersona4500;

namespace WpfGym.Views.ClassSchedule
{
    /// <summary>
    /// Interaction logic for ClassScheduleReview.xaml
    /// </summary>
    public partial class ClassScheduleReview : Window
    {
        private readonly ClassScheduleServices classScheduleServices = new ClassScheduleServices();
        private readonly TrainerServices _trainer = new TrainerServices();
        private readonly StaffServices _staff = new StaffServices();
        private readonly ClassScheduleReviewServices services = new ClassScheduleReviewServices();

        

        public int _DayOfWeek { get; set; }

        private byte[] _huella1 { get; set; }
        private byte[] _huella2 { get; set; }
        private byte[] _huellaStaff1 { get; set; }
        private byte[] _huellaStaff2 { get; set; }

        public ClassScheduleReview(int? Id)
        {
            InitializeComponent();


            var _Date = DateTime.Now;

            

            if (Id != 0)
            {
                InitializeView(Id);
                var result = services.GetByIdAndDate((int) Id);
                if (_DayOfWeek == (int) _Date.DayOfWeek) //compare Day of week from table ClassSchedule 
                {
                    if (result != null)
                    {
                        LblId.Content = result.Id.ToString();
                        if (result.Estatus == (int)Constants.StatusClass.OPEN)
                        {
                            ButtonApertura.Visibility = Visibility.Hidden;
                            ButtonCierre.Visibility = Visibility.Visible;
                            LblStatusClass.Visibility = Visibility.Hidden;

                            mkbHoraInicio.Text = result.Start.ToString().Remove(result.Start.ToString().Length - 3);
                            mkbHoraFin.Text = "";
                            mkbHoraInicio.IsEnabled = false;
                            mkbHoraFin.IsEnabled = false;
                            LblIdStaff.Content = result.IdStaff.ToString();

                            if (result.IdStaff != null)
                                CmbStaf.SelectedValue = result.IdStaff.ToString();
                        }
                        else
                        {
                            TxtObservacion.Text = result.Observation;
                            intMembers.Text = result.Members.ToString();
                            mkbHoraInicio.Text = result.Start.ToString().Remove(result.Start.ToString().Length - 3);
                            mkbHoraFin.Text = result.End.ToString().Remove(result.End.ToString().Length - 3);
                            ButtonCierre.Visibility = Visibility.Hidden;
                            ButtonApertura.Visibility = Visibility.Hidden;
                            ButtonCancelar.Visibility = Visibility.Hidden;
                            LblStatusClass.Visibility = Visibility.Visible;
                            LblStatusClass.Content = "CLASE CERRADA";
                            mkbHoraInicio.IsEnabled = false;
                            mkbHoraFin.IsEnabled = false;
                            TxtObservacion.IsEnabled = false;
                            CmbStaf.IsEnabled = false;
                            intMembers.IsEnabled = false;
                            CmbTrainer.IsEnabled = false;
                            LblIdStaff.Content = result.IdStaff.ToString();

                            if (result.IdStaff != null)
                                CmbStaf.SelectedValue = result.IdStaff.ToString();
                        }

                        if (result.Estatus == (int) Constants.StatusClass.CANCELED)
                        {
                            ButtonCancelar.Visibility = Visibility.Hidden;
                            LblStatusClass.Content = "CLASE CANCELADA";
                        }
                           
                    }
                    else
                    {

                        if (_huella1 != null)
                        {
                            ButtonApertura.Visibility = Visibility.Visible;
                            ButtonCierre.Visibility = Visibility.Hidden;
                            LblStatusClass.Visibility = Visibility.Hidden;
                            ButtonApertura.IsEnabled = true;

                        }
                        else
                        {
                            ButtonCierre.Visibility = Visibility.Hidden;
                            ButtonApertura.Visibility = Visibility.Visible;
                            LblStatusClass.Visibility = Visibility.Visible;
                            LblStatusClass.Content = "Huella No Registrada";
                            ButtonApertura.IsEnabled = false;
                        }


                        //GridClass.RowDefinitions[1].Height = new GridLength(0); //Hide Row
                        //GridClass.RowDefinitions[5].Height = new GridLength(0); //Hide Row
                        mkbHoraInicio.IsEnabled = false;
                        mkbHoraFin.IsEnabled = false;
                        mkbHoraInicio.Text = "";
                        mkbHoraFin.Text = "";
                    }

                }
                else
                {
                    if (result != null)
                    {
                        TxtObservacion.Text = result.Observation;
                        intMembers.Text = result.Members.ToString();
                        mkbHoraInicio.Text = result.Start.ToString().Remove(result.Start.ToString().Length - 3);
                        mkbHoraFin.Text = result.End.ToString().Remove(result.End.ToString().Length - 3);
                        ButtonCierre.Visibility = Visibility.Hidden;
                        ButtonApertura.Visibility = Visibility.Hidden;
                        LblStatusClass.Visibility = Visibility.Hidden;
                        mkbHoraInicio.IsEnabled = false;
                        mkbHoraFin.IsEnabled = false;
                        TxtObservacion.IsEnabled = false;
                        CmbStaf.IsEnabled = false;
                        intMembers.IsEnabled = false;
                        CmbTrainer.IsEnabled = false;
                    }
                    else
                    {
                        ButtonCierre.Visibility = Visibility.Hidden;
                        ButtonApertura.Visibility = Visibility.Hidden;
                        LblStatusClass.Visibility = Visibility.Hidden;
                        mkbHoraInicio.IsEnabled = false;
                        mkbHoraFin.IsEnabled = false;
                        TxtObservacion.IsEnabled = false;
                        CmbStaf.IsEnabled = false;
                        intMembers.IsEnabled = false;
                        CmbTrainer.IsEnabled = false;
                    }

                }
            }

        }

        private void Button_Apertura(object sender, RoutedEventArgs e)
        {

            ClassScheduleReviewModel classScheduleReview = new ClassScheduleReviewModel();

            if (DigitalPersona4500.Instance.Validate(new DigitalPersona4500Template(new System.IO.MemoryStream(_huella1)), new DigitalPersona4500Template(new System.IO.MemoryStream(_huella2))))
            {
                DateTime _date = DateTime.Now;

                int result = 0;
                classScheduleReview.IdClassSchedule = int.Parse(LblIdClass.Content.ToString());
                classScheduleReview.Observation = TxtObservacion.Text;
                classScheduleReview.IdStaff = int.Parse(CmbStaf.SelectedValue.ToString());
                classScheduleReview.IsTrainer = (LblIdTrainer.Content == CmbTrainer.SelectedValue);
                //if diferente Trainer then Susititute
                classScheduleReview.IdTrainer = int.Parse(CmbTrainer.SelectedValue.ToString());
                classScheduleReview.Members = int.Parse(intMembers.Text);
                classScheduleReview.Start = new TimeSpan(_date.Hour, _date.Minute, _date.Second);
                classScheduleReview.Estatus = (int)Constants.StatusClass.OPEN;

                result = services.Add(classScheduleReview);

                if (result > 0)
                {
                    GRDialogInformation _msg = new GRDialogInformation();
                    _msg.Message = "Huella Registrada";
                    _msg.ShowDialog();                  
                    this.Close();
                }
                else
                {
                    GRDialogError _err = new GRDialogError();
                    _err.Message = "Error guardando datos";
                    _err.ShowDialog();                  
                }
            }
            else
            {
                GRDialogError _err0 = new GRDialogError();
                _err0.Message = "Error, Verificando huella";
                _err0.ShowDialog();               
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            GRDialogConsultation _var = new GRDialogConsultation();
            _var.Message = "Desea Cancelar la clase?";
            if (_var.ShowDialog() == true)
            {



                ClassScheduleReviewModel classScheduleReview = new ClassScheduleReviewModel();


                if (!string.IsNullOrEmpty(TxtObservacion.Text.Trim()))
                {
                    DateTime _date = DateTime.Now;
                    int result = 0;
                   // classScheduleReview.Id = int.Parse(LblId.Content.ToString());
                    classScheduleReview.IdClassSchedule = int.Parse(LblIdClass.Content.ToString());
                    classScheduleReview.Observation = TxtObservacion.Text;
                    classScheduleReview.IdStaff = int.Parse(CmbStaf.SelectedValue.ToString());
                    classScheduleReview.IsTrainer = (LblIdTrainer.Content == CmbTrainer.SelectedValue);
                    //if diferente Trainer then Susititute
                    classScheduleReview.IdTrainer = int.Parse(CmbTrainer.SelectedValue.ToString());
                    classScheduleReview.Members = int.Parse(intMembers.Text);
                    classScheduleReview.Start = new TimeSpan(_date.Hour, _date.Minute, _date.Second);
                    classScheduleReview.End = new TimeSpan(_date.Hour, _date.Minute, _date.Second);
                    classScheduleReview.Estatus = (int)Constants.StatusClass.CANCELED;

                    result = services.Add(classScheduleReview);
                    //result = task.Result;
                    if (result > 0)
                    {
                        GRDialogInformation _msg = new GRDialogInformation();
                        _msg.Message = "Clase Cancelada";
                        _msg.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        GRDialogError _err = new GRDialogError();
                        _err.Message = "Error guardando datos";
                        _err.ShowDialog();
                    }
                }
                else
                {
                    GRDialogInformation _msg0 = new GRDialogInformation();
                    _msg0.Message = "Debe Ingresar una Observacion";
                    _msg0.ShowDialog();
                    TxtObservacion.Focus();
                }




            }

        }

        private void InitializeView(int? Id)
        {

            if (Id != null)
            {
                var result = classScheduleServices.GetById((int) Id);
                if (result != null)
                {
                    LblClassScheduleReview.Content = result.NameBranchOffice + " - " + result.Day + " - " +
                                                     result.Subject;

                    LblHoraInicio.Content = "Hora Inicio (" + result.StartHour + "):";
                    LblHoraFin.Content = "Hora Fin (" + result.EndHour + "):";
                    mkbHoraInicio.Value = result.StartHour;
                    mkbHoraFin.Value = result.EndHour;
                    LblIdTrainer.Content = result.IdTrainer.ToString();
                    LblIdClass.Content = Id.ToString();
                    _huella1 = result.HuellaTrainer1;
                    _huella2 = result.HuellaTrainer2;
                    if (result.WeekDay != null) _DayOfWeek = (int) result.WeekDay;
                }

                #region Fill CmbTRainer // CmbStaf

                var result0 = _trainer.GetAll().Where(a => a.Active == true).ToList();
                CmbTrainer.ItemsSource = result0;

                result0.Insert(0, new TrainerModel {Id = 0, Name = null});

                if (result.IdTrainer != null)
                    CmbTrainer.SelectedValue = result.IdTrainer.ToString();
                else
                    CmbTrainer.SelectedIndex = 0;


                var result1 = _staff.GetAll().Where(a => a.Active == true).ToList();
                result1.Insert(0, new StaffModel { Id = 0, Name = "Seleccione Staff" });
                CmbStaf.ItemsSource = result1;


                #endregion

                TxtObservacion.Focus();
            }
        }

        private void Button_Cierre(object sender, RoutedEventArgs e)
        {
            ClassScheduleReviewModel classScheduleReview = new ClassScheduleReviewModel();

            if (!string.IsNullOrEmpty(TxtObservacion.Text.Trim()))
            {
                if (int.Parse(intMembers.Text.ToString()) > 0)
                {
                    #region Validar Huella Staff / Cerrar Clase

                    if (
                    DigitalPersona4500.Instance.Validate(
                        new DigitalPersona4500Template(new System.IO.MemoryStream(_huellaStaff1)),
                        new DigitalPersona4500Template(new System.IO.MemoryStream(_huellaStaff2))))
                    {
                        DateTime _date = DateTime.Now;
                        bool result = false;
                        classScheduleReview.Id = int.Parse(LblId.Content.ToString());
                        classScheduleReview.IdClassSchedule = int.Parse(LblIdClass.Content.ToString());
                        classScheduleReview.Observation = TxtObservacion.Text;
                        classScheduleReview.IdStaff = int.Parse(CmbStaf.SelectedValue.ToString());
                        classScheduleReview.IsTrainer = (LblIdTrainer.Content == CmbTrainer.SelectedValue);
                        //if diferente Trainer then Susititute
                        classScheduleReview.IdTrainer = int.Parse(CmbTrainer.SelectedValue.ToString());
                        classScheduleReview.Members = int.Parse(intMembers.Text);
                        classScheduleReview.End = new TimeSpan(_date.Hour, _date.Minute, _date.Second);
                        classScheduleReview.Estatus = (int)Constants.StatusClass.CLOSE;

                        result = services.Update(classScheduleReview);
                        //result = task.Result;
                        if (result)
                        {
                            GRDialogInformation _msg = new GRDialogInformation();
                            _msg.Message = "Clase Cerrada";
                            _msg.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            GRDialogError _err = new GRDialogError();
                            _err.Message = "Error guardando datos";
                            _err.ShowDialog();
                        }                        
                    }
                    else
                    {
                        GRDialogError _err0 = new GRDialogError();
                        _err0.Message = "Error, Verificando huella";
                        _err0.ShowDialog();
                    }
                    #endregion
                }
                else
                {
                    GRDialogInformation _msg1 = new GRDialogInformation();
                    _msg1.Message = "Debe Ingresar en número de asistentes a la clase";
                    _msg1.ShowDialog();
                    intMembers.Focus();
                }
            }
            else
            {
                GRDialogInformation _msg1 = new GRDialogInformation();
                _msg1.Message = "Debe Ingresar una Observacion";
                _msg1.ShowDialog();
                TxtObservacion.Focus();
            }

        }

        private void CmbTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CmbTrainer.SelectedIndex != 0)
            {
                int id =
                    int.Parse((CmbTrainer.SelectedValue == null)
                        ? LblIdTrainer.Content.ToString()
                        : CmbTrainer.SelectedValue.ToString());
                var result = _trainer.GetByID(id);

                _huella1 = result != null ? result.Huella1 : new byte[2];
                _huella2 = result != null ? result.Huella2 : new byte[2];

                if (_huella1 != null)
                {
                    LblStatusClass.Visibility = Visibility.Hidden;
                    ButtonApertura.IsEnabled = true;
                }
                else
                {
                    LblStatusClass.Visibility = Visibility.Visible;
                    LblStatusClass.Content = "Huella No Registrada";
                    ButtonApertura.IsEnabled = false;
                }

            }
            else
            {
                GRDialogInformation _msg = new GRDialogInformation();
                _msg.Message = "Debe Seleccionar un Trainer";
                _msg.ShowDialog();

                LblStatusClass.Visibility = Visibility.Visible;
                LblStatusClass.Content = "Huella No Registrada";
                ButtonApertura.IsEnabled = false;
            }
        }


        #region FINGERPRINT VERIFY

        //private DPFP.Template Template;
        //private DPFP.Verification.Verification Verificator;

        protected bool VerifyFingerPrint(DPFP.Sample sample)
        {
            bool isOk = false;
            try
            {
                //Utils utils = new Utils();
                //DPFP.FeatureSet features = utils.ExtractFeatures(sample, DPFP.Processing.DataPurpose.Verification);

                //if (features != null)
                //{
                //    //DPFP.Template template = new Template(); // fingerprint.Templates[0];
                //    Template.DeSerialize(_huella);

                //    // Compare the feature set with our template
                //    DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                //    Verificator.Verify(features, Template, ref result);
                //    isOk = (result.Verified) ? true : false;
                //}
            }
            catch (Exception ex)
            {
                isOk = false;
            }
            return isOk;

        }

        private void Button_Salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbStaf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

                if (CmbStaf.SelectedIndex != 0)
                {
                    int id = int.Parse((CmbStaf.SelectedValue == null)
                        ? LblIdStaff.Content.ToString()
                        : CmbStaf.SelectedValue.ToString());

                    var result = _staff.GetById(id);

                    _huellaStaff1 = result != null ? result.Huella1 : new byte[2];
                    _huellaStaff2 = result != null ? result.Huella2 : new byte[2];

                    if (_huella1 != null)
                    {
                        LblStatusClass.Visibility = Visibility.Hidden;
                        ButtonCierre.IsEnabled = true;
                    }
                    else
                    {
                        LblStatusClass.Visibility = Visibility.Visible;
                        LblStatusClass.Content = "Huella No Registrada";
                        ButtonCierre.IsEnabled = false;
                    }

                }
                else
                {
                    GRDialogInformation _msg = new GRDialogInformation();
                    _msg.Message = "Debe Seleccionar un Staff";
                    _msg.ShowDialog();

                    LblStatusClass.Visibility = Visibility.Visible;
                    LblStatusClass.Content = "Huella No Registrada";
                    ButtonCierre.IsEnabled = false;
                }
            
        }


        #endregion


        //private void FingerVerification()
        //{
        //    try
        //    {
        //        FeatureSet FeatureSet = new FeatureSet();
        //        Verification.Result matchResult = new Verification.Result();
        //        Verification verification = new Verification();

        //        var template = new DPFP.Template();
        //        template.DeSerialize(_huella);

        //        verification.Verify(FeatureSet.Size(2), template, ref matchResult);
        //        //var result = verifyControl_OnComplete(template, FeatureSet,  matchResult);

        //        //return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


    }
}
