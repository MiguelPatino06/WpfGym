using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;



namespace PowerClub.Bussiness.Model
{
    public class ClassScheduleModel
    {
        public int Id { get; set; }

        public int IdBranchOffice { get; set; }

        public int IdWorkout { get; set; }

        public Nullable<int> WeekDay { get; set; }

        public Nullable<TimeSpan> StarTime { get; set; }

        public Nullable<TimeSpan> EndTime { get; set; }

        public Nullable<int> IdTrainer { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }

        public Nullable<DateTime> DateUpdated { get; set; }

        public string Color { get; set; }

        public Nullable<DateTime> StartDate { get; set; }

        public Nullable<DateTime> EndDate { get; set; }

        //Extend Model
        public byte[] HuellaTrainer1 { get; set; }
        public byte[] HuellaTrainer2 { get; set; }
        public string Day { get; set; }
        public string NameBranchOffice { get; set; }
        public string NameWorkout { get; set; }
        public string Subject { get; set; }
        public Brush BrushColor { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string TrainerName { get; set; }
        public int IdParent { get; set; }
        public string IsActive { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public Nullable<TimeSpan> StarBranchOffice { get; set; }
        public Nullable<TimeSpan> EndBranchOffice { get; set; }
    }
}
