using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class BranchOfficeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Observation { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<TimeSpan> StarHour { get; set; }
        public Nullable<TimeSpan> EndHour { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateUpdated { get; set; }
        
        //Extend Model
        public string StringStartHour { get; set; }
        public string StringEndHour { get; set; }
        public string IsActive { get; set; }
    }
}
