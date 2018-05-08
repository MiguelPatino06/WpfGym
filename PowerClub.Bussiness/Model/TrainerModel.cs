using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PowerClub.Bussiness.Model
{
    public class TrainerModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }
        public Nullable<DateTime> DateUpdated { get; set; }
        public byte[] Huella1 { get; set; }
        public byte[] Huella2 { get; set; }
        public double? Price { get; set; }
        public string PhotoLink { get; set; }


        //Extend Model
        public string IsActive { get; set; }
        public string NameType { get; set; }
        public string IsHuella { get; set; }
    }
}
