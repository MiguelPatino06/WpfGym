using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class WorkoutModel
    {
        public int Id { get; set; }
        public string CodigoWorkout { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<int> Parent { get; set; }

        public Nullable<DateTime> DateCreated { get; set; }

        public Nullable<DateTime> DateUpdated { get; set; }

        public List<WorkoutModel> Parents { get; set; }

        public string Color { get; set; }

        //Extend Model
        public string IsActive { get; set; }
        public string NameParent { get; set; }
    }

}
