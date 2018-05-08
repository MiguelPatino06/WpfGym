using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfGym.Core
{
    public class Security
    {

        private Index _window;

        public Security() 
        {
            _window = new Index();
        }

        public void Administrator()
        {

        }

        public void Staff()
        {
            _window.btnBranchOffice.IsEnabled = false;
            _window.btnWorkout.IsEnabled = false;
            _window.btnClass.IsEnabled = false;
            _window.btnStaff.IsEnabled = false;
            _window.btnClassScheduleReviewList.IsEnabled = false;
            _window.btnReport.IsEnabled = false;
        }

        public void Operador()
        {
            _window.btnBranchOffice.IsEnabled = false;
            _window.btnWorkout.IsEnabled = false;
            _window.btnClass.IsEnabled = false;
            _window.btnClassSchedule.IsEnabled = false;
            _window.btnStaff.IsEnabled = false;
            _window.btnClassScheduleReviewList.IsEnabled = false;
            _window.btnReport.IsEnabled = false;
            _window.btnTrainer.IsEnabled = false;
        }
    }
}
