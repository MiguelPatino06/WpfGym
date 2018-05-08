using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class ReportModel
    {

    }

    public class ParametersSustituciones
    {
        public int IdOffice { get; set; }
        public int IdWorkout { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }

    public class ParametersTardanzas
    {
        public int IdOffice { get; set; }
        public int IdTrainer { get; set; }
        public int IdWorkout { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Delay { get; set; }
    }

    public class ParametersClases
    {
        public int IdOffice { get; set; }
        public int IdTrainer { get; set; }
        public int IdWorkout { get; set; }
        public int IdStaff { get; set; }
        public int IdStatus { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Asistencia { get; set; }

    }
}
