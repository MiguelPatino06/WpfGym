using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using PowerClub.Bussiness;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;

namespace WpfGym.ViewModels
{
    
    public class ScheduleViewModel : ViewModelBase
    {

        
        ClassScheduleServices _classScheduleServices = new ClassScheduleServices();
        BranchOfficeServices branchOffice = new BranchOfficeServices();
        WeekScheduler weekScheduler = new WeekScheduler();



        public TimeSpan StartJourney
        {
            get { return TimeSpan.FromHours(4); }
        }


        public TimeSpan EndJourney
        {
            get { return TimeSpan.FromHours(19); }

        }

        private ObservableCollection<ClassScheduleModel> _events;
        public ObservableCollection<ClassScheduleModel> WpfScheduleEvents
        {
            get
            {
                if (_events == null)
                    _events = new ObservableCollection<ClassScheduleModel>(GetAllEvents());

                return _events;
            }
            set
            {
                if (_events != value)
                {
                    _events = value;
                    NotifyPropertyChanged("WpfScheduleEvents");
                }
            }
        }


        private List<ClassScheduleModel> GetAllEvents()
        {
            return _classScheduleServices.GetAll();
        }


    }
}
