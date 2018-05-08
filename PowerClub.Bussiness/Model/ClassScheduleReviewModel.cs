using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class ClassScheduleReviewModel
    {
        public int Id { get; set; }
        public int IdClassSchedule { get; set; }
        public string Observation { get; set; }
        public int IdStaff { get; set; }
        public bool IsTrainer { get; set; }
        public int IdTrainer { get; set; }
        public Nullable<int> Members { get; set; }
        public Nullable<TimeSpan> Start { get; set; }
        public Nullable<TimeSpan> End { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ClassScheduleModel Schedule { get; set; }
        public Nullable<int> Estatus { get; set; }

        //Extend Model
        public string EstatusName { get; set; }
        public string StaffName { get; set; }
        public string StartString { get; set; }
        public string EndString { get; set; }
        public string Flag { get; set; } //this field id used for delay time 
        public string Day { get; set; }

    }
}
