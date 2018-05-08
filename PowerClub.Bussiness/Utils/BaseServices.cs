using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerClub.DataAccess.Domain;
//using PowerClub.DataAccess.Utils;


namespace PowerClub.Bussiness.Utils
{
    public class BaseServices
    {
        //protected IUnitOfWork<GYMEntities> fGymUnitOfWork = new UnitWork<GYMEntities>();
        protected GYMEntities fcontext = new GYMEntities();
    }
}
