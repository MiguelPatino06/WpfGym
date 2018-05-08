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
using PowerClub.Bussiness.Utils;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;

namespace WpfGym.Views.ClassSchedule
{
    /// <summary>
    /// Interaction logic for ClassScheduleImput.xaml
    /// </summary>
    public partial class ClassScheduleImput : Window
    {

        #region Variables

        private readonly BranchOfficeServices _branchOfficeServices = new BranchOfficeServices();
        private readonly WorkoutServices _workoutServices = new WorkoutServices();
        private readonly DayofWeek _days = new DayofWeek();
        private readonly TrainerServices _trainer = new TrainerServices();
        private readonly ClassScheduleServices _services = new ClassScheduleServices();
      

        #endregion


        public Nullable<TimeSpan> HourStart { get; set; }
        public Nullable<TimeSpan> HourEnd { get; set; }


        public ClassScheduleImput(int? Id)
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
                FillCombos(null, null, null, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var x = Brushes.MediumPurple;
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            bool hourStart = Common.ValidateHour(mkbHoraInicio.Text);
            bool hourEnd = Common.ValidateHour(mkbHoraFin.Text);


            if (!((CmbBranchOffice.SelectedIndex == 0) || (CmbWorkout.SelectedIndex == 0)))
            {
                if ((hourStart) && (hourEnd))
                {
                    if (TimeSpan.Parse(mkbHoraInicio.Text) < TimeSpan.Parse(mkbHoraFin.Text))
                    {
                        if (((TimeSpan.Parse(mkbHoraInicio.Text) >= HourStart) && (TimeSpan.Parse(mkbHoraFin.Text) <= HourEnd)))
                        {                                                     
                            int result = 0;
                            ClassScheduleModel _class = new ClassScheduleModel();
                            _class.IdBranchOffice = int.Parse(CmbBranchOffice.SelectedValue.ToString());
                            _class.IdWorkout = int.Parse(CmbWorkout.SelectedValue.ToString());
                            _class.WeekDay = int.Parse(CmbDia.SelectedValue.ToString());
                            _class.StarTime = TimeSpan.Parse(mkbHoraInicio.Text);
                            _class.EndTime = TimeSpan.Parse(mkbHoraFin.Text);
                            _class.IdTrainer = int.Parse(CmbTrainer.SelectedValue.ToString());
                            _class.Active = CmbActive.Text == "Activo";

                            if (LblId.Content == "0") //ADD
                            {
                                result = _services.Add(_class);
                            }
                            else //EDIT
                            {
                                int _id = int.Parse(LblId.Content.ToString());
                                _class.Id = _id;
                                var task = _services.Update(_class);
                                result = task ? _id : 0;
                            }


                            if (result > 0)
                            {
                                GRDialogConsultation _var = new GRDialogConsultation();
                                _var.Message = "Registro Guardado, desea crear otro Registro?";
                                if (_var.ShowDialog() == true)
                                {
                                    CleanControls();
                                }
                                else
                                {
                                    var window = new ClassScheduleList();
                                    this.Close();
                                    window.ShowDialog();
                                }
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
                            var h0 = HourStart.ToString().Remove(HourStart.ToString().Length - 3);
                            var h1 = HourEnd.ToString().Remove(HourEnd.ToString().Length - 3);

                            GRDialogInformation _var0 = new GRDialogInformation();
                            _var0.Message = "Hora Sucursal fuera de Rango , Hora Apertura (" + h0 + ") Hora Cierre (" + h1 + ")";
                            _var0.ShowDialog();                        
                        }
                    }
                    else
                    {
                        GRDialogInformation _var1 = new GRDialogInformation();
                        _var1.Message = "La Hora de Inicio no puede ser mayor a la Hora Fin";
                        _var1.ShowDialog();                        
                    }
                }
                else
                {
                    GRDialogInformation _var2 = new GRDialogInformation();
                    _var2.Message = "Verificar campos Obligatorios / Formato de Horas";
                    _var2.ShowDialog();                   
                }

            }
            else
            {
                GRDialogInformation _var3 = new GRDialogInformation();
                _var3.Message = "Validar campos obligatorios";
                _var3.ShowDialog();               
            }         
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitializedWindow(int Id)
        {
            var result = _services.GetById(Id);
            LblId.Content = Id.ToString();
            mkbHoraInicio.Text = result.StartHour;
            mkbHoraFin.Text = result.EndHour;

            HourStart = result.StarBranchOffice;
            HourEnd = result.EndBranchOffice;

            if (!(bool) result.Active)
                CmbActive.SelectedIndex = 1;
            else
                CmbActive.SelectedIndex = 0;

            FillCombos(result.IdBranchOffice, result.IdWorkout, result.WeekDay, result.IdTrainer);
        }

        private void FillCombos(int? IdBranch, int? Idworkout, int? IdDay, int? IdTrainer)
        {
            //BranchOffice
            var result0 = _branchOfficeServices.GetAll().Where(a => a.Active == true).OrderBy(a=> a.Name).ToList();
            result0.Insert(0, new BranchOfficeModel { Id = 0, Name = null });
            CmbBranchOffice.ItemsSource = result0;
            CmbBranchOffice.SelectedIndex = 0;
            if(IdBranch != null) CmbBranchOffice.SelectedValue = (int)IdBranch;

            //Workout
            var result1 = _workoutServices.GetList().Where(a => a.Active == true).OrderBy(r => r.Name).ToList();
            result1.Insert(0, new WorkoutModel { Id = 0, Name = null });
            CmbWorkout.ItemsSource = result1;
            CmbWorkout.SelectedIndex = 0;
            if (Idworkout != null) CmbWorkout.SelectedValue = (int) Idworkout;

            //Day
            CmbDia.ItemsSource = _days.GetAll().ToList();
            CmbDia.SelectedIndex = 0;
            if (IdDay != null) CmbDia.SelectedValue = (int)IdDay;

            //Trainer
            var result = _trainer.GetAll().Where(a => a.Active == true).ToList();
            result.Insert(0, new TrainerModel { Id = 0, Name = null });
            CmbTrainer.ItemsSource = result;
            CmbTrainer.SelectedIndex = 0;
            if (IdTrainer != null) CmbTrainer.SelectedValue = (int)IdTrainer;

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
            CmbBranchOffice.SelectedIndex = 0;
            CmbDia.SelectedIndex = 0;
            CmbTrainer.SelectedIndex = 0;
            CmbActive.SelectedIndex = 0;
            CmbWorkout.SelectedIndex = 0;
            mkbHoraInicio.Text = "";
            mkbHoraFin.Text = "";
        }

        private void CmbBranchOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int val = int.Parse(CmbBranchOffice.SelectedValue.ToString());

            if (val > 0)
            {
                BranchOfficeModel branchOffice = _branchOfficeServices.GetByID(val);
                HourStart = branchOffice.StarHour;
                HourEnd = branchOffice.EndHour;
            }       
        }

    }
}
