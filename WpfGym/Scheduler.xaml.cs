using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Services;
using WpfGym.Views.ClassSchedule;

namespace WpfGym
{

    public enum Mode
    {
        Day,
        Week,
        Month
    }

    public class CustomEventArgsTimeSpan : EventArgs
    {
        public CustomEventArgsTimeSpan(TimeSpan s)
        {
            message = s;
        }
        private TimeSpan message;

        public TimeSpan Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    /// <summary>
    /// Interaction logic for Scheduler.xaml
    /// </summary>
    public partial class Scheduler : UserControl
    {
        BranchOfficeServices branchOffice = new BranchOfficeServices();

        public event EventHandler<CustomEventArgsClassSchedule> OnEventDoubleClick;
        public event EventHandler<CustomEventArgsDateTime> OnScheduleDoubleClick;

        //internal event EventHandler<Event> OnEventAdded;
        //internal event EventHandler<Event> OnEventDeleted;
        internal event EventHandler OnEventsModified;

        internal event EventHandler<CustomEventArgsTimeSpan> OnStartJourneyChanged;
        internal event EventHandler<CustomEventArgsTimeSpan> OnEndJourneyChanged;

        #region SelectedDate
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(SelectedDateChanged)));

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static int? _idbranch { get; set; }
        public static int? _idworkout { get; set; }
        public static int? _idlivingroom { get; set; }
        //public static TimeSpan? _datestart { get; set; }
        //public static TimeSpan? _dateend { get; set; }

        private static void SelectedDateChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DateTime SelectedDate = (DateTime)e.NewValue;
            Scheduler sc = source as Scheduler;
            //sc.DayScheduler.CurrentDay = SelectedDate;
            sc.WeekScheduler.IDBranch = _idbranch;
            sc.WeekScheduler.IDLivingroom = _idlivingroom;
            sc.WeekScheduler.IDWorkout = _idworkout;
            sc.WeekScheduler.FirstDay = SelectedDate;
            //sc.WeekScheduler.DateStart = _datestart;
            //sc.WeekScheduler.DateEnd = _dateend;
            // sc.MonthScheduler.CurrentMonth = SelectedDate;
        }
        #endregion

        #region Events
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(ObservableCollection<ClassScheduleModel>), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(AdjustEvents)));

        public ObservableCollection<ClassScheduleModel> Events
        {
            get { return (ObservableCollection<ClassScheduleModel>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        private static void AdjustEvents(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((source as Scheduler).OnEventsModified != null)
                (source as Scheduler).OnEventsModified(source, null);
        }
        #endregion

        #region StartJourney
        public static readonly DependencyProperty StartJourneyProperty =
            DependencyProperty.Register("StartJourney", typeof(TimeSpan), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(StartJourneyChanged)));

        public TimeSpan StartJourney
        {
            get { return (TimeSpan)GetValue(StartJourneyProperty); }
            set { SetValue(StartJourneyProperty, value); }
        }

        private static void StartJourneyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((source as Scheduler).OnStartJourneyChanged != null)
                (source as Scheduler).OnStartJourneyChanged(source, new CustomEventArgsTimeSpan((TimeSpan)e.NewValue));
        }
        #endregion

        #region EndJourney
        public static readonly DependencyProperty EndJourneyProperty =
            DependencyProperty.Register("EndJourney", typeof(TimeSpan), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(EndJourneyChanged)));

        public TimeSpan EndJourney
        {
            get { return (TimeSpan)GetValue(EndJourneyProperty); }
            set { SetValue(EndJourneyProperty, value); }
        }


        private static void EndJourneyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((source as Scheduler).OnEndJourneyChanged != null)
                (source as Scheduler).OnEndJourneyChanged(source, new CustomEventArgsTimeSpan((TimeSpan)e.NewValue));
        }
        #endregion

        #region WeekScheduler
        internal static readonly DependencyProperty WeekSchedulerProperty =
            DependencyProperty.Register("WeekScheduler", typeof(WeekScheduler), typeof(Scheduler),
            new FrameworkPropertyMetadata());

        internal WeekScheduler WeekScheduler
        {
            get { return ucWeekScheduler; }
        }
        #endregion

        #region Mode
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(Mode), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(ModeChanged)));

        public Mode Mode
        {
            get { return (Mode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        private static void ModeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Mode mode = (Mode)e.NewValue;
            Scheduler sc = source as Scheduler;
            sc.WeekScheduler.Visibility = (mode == Mode.Week ? Visibility.Visible : Visibility.Hidden);


            switch (mode)
            {
                //case WpfGym.Mode.Day:
                //    sc.DayScheduler.CurrentDay = sc.WeekScheduler.FirstDay;
                //    break;
                case WpfGym.Mode.Week:
                    sc.WeekScheduler.FirstDay = Convert.ToDateTime("01/01/0001 0:00:00"); //sc.DayScheduler.CurrentDay;
                    break;
                //case WpfGym.Mode.Month:
                //    sc.MonthScheduler.CurrentMonth = sc.DayScheduler.CurrentDay;
                //    break;
            }
        }
        #endregion


        public Scheduler()
        {
            InitializeComponent();
            Mode = WpfGym.Mode.Week;
            Events = new ObservableCollection<ClassScheduleModel>();
            SelectedDate = DateTime.Now;

            WeekScheduler.OnEventDoubleClick += InnerScheduler_OnEventDoubleClick;
            //WeekScheduler.OnScheduleDoubleClick += InnerScheduler_OnScheduleDoubleClick;       
        }

        void InnerScheduler_OnScheduleDoubleClick(object sender, DateTime e)
        {
            if (OnScheduleDoubleClick != null) OnScheduleDoubleClick(sender, new CustomEventArgsDateTime(e));
        }

        void InnerScheduler_OnEventDoubleClick(object sender, CustomEventArgsClassSchedule e)
        {
            if (e != null)
            {
                int _id = e.Message.Id;
                var _classScheduleReview = new ClassScheduleReview(_id);
                _classScheduleReview.Show();
            } 
            //if (OnEventDoubleClick != null) OnEventDoubleClick(sender, e);
        }

        public void NextPage(int? idbranch, int? livingroom, int? idworkout)
        {
            _idbranch = idbranch;
            _idlivingroom = livingroom;
            _idworkout = idworkout;
            //var branc = branchOffice.GetByID((int)idbranch);
            //_datestart = branc.StarHour;
            //_dateend = branc.EndHour;

            SelectedDate = SelectedDate.AddMilliseconds(50);
            //switch (Mode)
            //{
            //    case WpfScheduler.Mode.Day:
            //        SelectedDate = SelectedDate.AddDays(1);
            //        break;
            //    case WpfScheduler.Mode.Week:
            //        SelectedDate = SelectedDate.AddDays(7);
            //        break;
            //    case WpfScheduler.Mode.Month:
            //        SelectedDate = SelectedDate.AddMonths(1);
            //        break;
            //}
        }

    }
}
