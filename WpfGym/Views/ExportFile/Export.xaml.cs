using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Controls;
using WpfGym.Core;

namespace WpfGym.Views.ExportFile
{
    /// <summary>
    /// Interaction logic for Export.xaml
    /// </summary>
    public partial class Export : Window
    {
        ReadExcel _read = new ReadExcel();

        private List<ExcelFileModel> _excelFile { get; set; }

        public Export()
        {
            InitializeComponent();
        }

        private void Button_Aplicar(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Seleccione un Archivo";
            op.Filter = "Excel files (*.xls or .xlsx)|.xls;*.xlsx";
            if (op.ShowDialog() == true)
            {
                TxtPath.Text = op.FileName;
                _excelFile = _read.Read(TxtPath.Text.Trim());
                DataGridExportFile.ItemsSource = new ObservableCollection<ExcelFileModel>(_excelFile);  
            }
        }

        private void ButtonExportar_Click(object sender, RoutedEventArgs e)
        {
            ClassScheduleServices services = new ClassScheduleServices();

            if (_excelFile != null)
            {
                int _message = 0,_cont =0;
               
                GRDialogConsultation _var = new GRDialogConsultation();
                _var.Message = "Desea crear las Clases?";
                if (_var.ShowDialog() == true)
                {
                    foreach (var item in _excelFile)
                    {
                        var _item = new ClassScheduleModel();

                        _item.IdBranchOffice = int.Parse(item.CodigoSucursal);
                        _item.IdWorkout = int.Parse(item.CodigoDisciplina);
                        _item.WeekDay = item.NumeroDia;
                        _item.StarTime = TimeSpan.Parse(item.HoraInicio);
                        _item.EndTime = TimeSpan.Parse(item.HoraFin);
                        _item.IdTrainer = item.IdTrainer;
                        _item.StartDate = DateTime.Parse(item.FechaInicio);
                        _item.EndDate = DateTime.Parse(item.FechaFin);

                        if (item.MensajeFila == "OK")
                        {
                            _message = services.Add(_item);
                            if (_message > 0)
                                _cont += 1;
                        }
                            
                    }
                    GRDialogInformation _var0 = new GRDialogInformation();
                    _var0.Message = "Total Regisros Exportados : " + _cont.ToString();
                    _var0.ShowDialog();
                  
                    DataGridExportFile.ItemsSource = new ObservableCollection<ExcelFileModel>(new List<ExcelFileModel>());
                    TxtPath.Text = "";

                }
            }
            else
            {
                GRDialogInformation _var = new GRDialogInformation();
                _var.Message = "Debe Seleccionar un Registro";
                _var.ShowDialog();
            }
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
