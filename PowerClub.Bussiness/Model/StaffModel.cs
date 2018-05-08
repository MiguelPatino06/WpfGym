using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class StaffModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateUpdated { get; set; }
        public byte[] Huella1 { get; set; }
        public byte[] Huella2 { get; set; }
        public bool Huella { get; set; }

        //extend Model
        public string IsHuella { get; set; }
        public string IsActive { get; set; }

    }
}
