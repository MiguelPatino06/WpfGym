using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using PowerClub.Bussiness.Utils;
using Xceed.Wpf.DataGrid;

namespace WpfGym.Core
{
    
    public class ReadExcel
    {
        BranchOfficeServices branch = new BranchOfficeServices();
        WorkoutServices workout = new WorkoutServices();
        TrainerServices trainer = new TrainerServices();

        [STAThread]
        public  List<ExcelFileModel> Read(string filepath)
        {

            List<ExcelFileModel> excel = new List<ExcelFileModel>();         
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                     
            ExcelFile ef = ExcelFile.Load(filepath);
            StringBuilder sb = new StringBuilder();
        

            foreach (ExcelWorksheet sheet in ef.Worksheets)
            {
                int i = 0;
                string _message = string.Empty;
                foreach (ExcelRow row in sheet.Rows)
                {
                    var itemExcel = new ExcelFileModel();
                    try
                    {
                        i += 1;
                        itemExcel.Fila = i-1;

                        var codSuc = branch.GetByCode(row.AllocatedCells[0].StringValue);
                        var codDisc = workout.GetByCode(row.AllocatedCells[2].StringValue);
                        var codTrainer = (row.AllocatedCells[9].Value != null)? trainer.GetByCode(row.AllocatedCells[9].StringValue): new TrainerModel();

                        var validFechaInic = DateTime.Parse((row.AllocatedCells[7]).Value.ToString());
                        var validFechaFin = DateTime.Parse((row.AllocatedCells[8]).Value.ToString());

                        var validHoraInic = TimeSpan.Parse((row.AllocatedCells[5]).StringValue);                         
                        var validHoraFin = TimeSpan.Parse((row.AllocatedCells[6]).Value.ToString());
                           
                        var _day = Common.IsDay(row.AllocatedCells[4].StringValue);

                        itemExcel.IdSucursal = (codSuc != null) ? (int?)codSuc.Id : null;
                        itemExcel.CodigoSucursal = codSuc != null? codSuc.Code : string.Empty;
                        itemExcel.NombreSucursal = row.AllocatedCells[1].StringValue;
                        itemExcel.CodigoWorkout = (codDisc != null) ? codDisc.CodigoWorkout : null;
                        itemExcel.IdWorkout = (codDisc != null) ? (int?)codDisc.Id : null;
                        itemExcel.CodigoDisciplina = codDisc != null ? codDisc.Id.ToString() : string.Empty;
                        itemExcel.NombreDisciplina = row.AllocatedCells[3].StringValue;
                        itemExcel.IdTrainer = (codTrainer != null) ? (int?)codTrainer.Id : null;
                        itemExcel.CodigoTrainer = codTrainer.Code ?? string.Empty;
                        itemExcel.NombreTrainer = (row.AllocatedCells[10].Value == null)? string.Empty: row.AllocatedCells[10].Value.ToString();
                        itemExcel.DiaSemana = row.AllocatedCells[4].StringValue;
                        itemExcel.FechaInicio = validFechaInic.ToString();
                        itemExcel.FechaFin = validFechaFin.ToString();
                        itemExcel.HoraInicio = validHoraInic.ToString();
                        itemExcel.HoraFin = validHoraFin.ToString();
                        itemExcel.Hora = validHoraInic + "-" + validHoraFin;
                        itemExcel.Fecha = validFechaInic.ToString("dd/MM/yyyy") + "-" + validFechaFin.ToString("dd/MM/yyyy");
                        itemExcel.NumeroDia = _day;
                        itemExcel.MensajeFila = "OK";
                    }
                    catch (Exception ex)
                    {
                        itemExcel.MensajeFila = ex.Message;                   
                    }
                    if(i> 1)
                        excel.Add(itemExcel);


                }
            }
            
            return excel;
        }
    }

    
}
