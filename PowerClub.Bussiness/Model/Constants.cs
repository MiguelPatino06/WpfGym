using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerClub.Bussiness.Model
{
    public class Constants
    {
        [Serializable]
        public enum enumTypeTrainer
        {
           PRINCIPAL = 1,
           SUBSTITUTE = 2 
        }

        [Serializable]
        public enum enumWeekDay
        {
            SUNDAY = 0,
            MONDAY = 1,
            TUESDAY = 2,
            WEDNESDAY = 3,
            THURSDAY = 4,
            FRIDAY = 5,
            SATURDAY = 6
        }

        [Serializable]
        public enum StatusClass
        {
            OPEN = 1,
            CLOSE = 2,
            CANCELED = 3
        }

    }
}
