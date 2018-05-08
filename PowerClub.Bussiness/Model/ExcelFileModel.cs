using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Resources;

namespace PowerClub.Bussiness.Model
{
    public class ExcelFileModel
    {
        public int Fila { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string CodigoWorkout { get; set; }
        public Nullable<int> IdWorkout { get; set; }
        public string CodigoDisciplina { get; set; }
        public string NombreDisciplina { get; set; }
        public Nullable<int> IdTrainer { get; set; }
        public string CodigoTrainer { get; set; }
        public string NombreTrainer { get; set; }
        public string DiaSemana { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int NumeroDia { get; set; }
        public string MensajeFila { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }

    }
}
