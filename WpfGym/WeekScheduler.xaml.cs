using System;
using System.Collections.Generic;
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

namespace WpfGym
{
    
    public class CustomEventArgsClassSchedule : EventArgs
    {
       
        public CustomEventArgsClassSchedule(ClassScheduleModel s)
        {
            message = s;
        }
        private ClassScheduleModel message;

        public ClassScheduleModel Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    public class CustomEventArgsDateTime: EventArgs
    {
        public CustomEventArgsDateTime(DateTime s)
        {
            message = s;
        }
        private DateTime message;

        public DateTime Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    /// <summary>
    /// Interaction logic for WeekScheduler.xaml
    /// </summary>
    public partial class WeekScheduler : UserControl
    {

        private Scheduler _scheduler;
        BranchOfficeServices  branchOffice = new BranchOfficeServices();

        internal event EventHandler<CustomEventArgsClassSchedule> OnEventDoubleClick;
        internal event EventHandler<CustomEventArgsDateTime> OnScheduleDoubleClick;

        //the type must convertible to 'System.EvenArgs' in order to use it as parameter 'TEvenArgs'
        

        #region FirstDay
        private DateTime _firstDay;

        internal int? IDBranch { get; set; }
        internal int? IDWorkout { get; set; }
        internal int? IDLivingroom { get; set; }

        internal DateTime FirstDay
        {
            get { return _firstDay; }
            set
            {
                while (value.DayOfWeek != DayOfWeek.Monday)
                    value = value.AddDays(-1);
                _firstDay = value;
                AdjustFirstDay(value);
            }
        }

        private void AdjustFirstDay(DateTime firstDay)
        {
            dayLabel1.Content = firstDay.ToString("dddd dd/MM");
            dayLabel2.Content = firstDay.AddDays(1).ToString("dddd dd/MM");
            dayLabel3.Content = firstDay.AddDays(2).ToString("dddd dd/MM");
            dayLabel4.Content = firstDay.AddDays(3).ToString("dddd dd/MM");
            dayLabel5.Content = firstDay.AddDays(4).ToString("dddd dd/MM");
            dayLabel6.Content = firstDay.AddDays(5).ToString("dddd dd/MM");
            dayLabel7.Content = firstDay.AddDays(6).ToString("dddd dd/MM");

         
            PaintAllEvents(null, IDBranch, IDLivingroom, IDWorkout);
            //PaintAllDayEvents();
        }
        #endregion

        public WeekScheduler()
        {
            InitializeComponent();
            column1.MouseDown += Canvas_MouseDown;
            column1.Background = Brushes.Transparent;
            column2.MouseDown += Canvas_MouseDown;
            column2.Background = Brushes.Transparent;
            column3.MouseDown += Canvas_MouseDown;
            column3.Background = Brushes.Transparent;
            column4.MouseDown += Canvas_MouseDown;
            column4.Background = Brushes.Transparent;
            column5.MouseDown += Canvas_MouseDown;
            column5.Background = Brushes.Transparent;
            column6.MouseDown += Canvas_MouseDown;
            column6.Background = Brushes.Transparent;
            column7.MouseDown += Canvas_MouseDown;
            column7.Background = Brushes.Transparent;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DependencyObject ucParent = (sender as WeekScheduler).Parent;
                while (!(ucParent is Scheduler))
                {
                    ucParent = LogicalTreeHelper.GetParent(ucParent);
                }
                _scheduler = ucParent as Scheduler;

                var ed = new ClassScheduleModel();


                ResizeGrids(new Size(this.ActualWidth, this.ActualHeight));
                PaintAllEvents(null, IDBranch, IDLivingroom, IDWorkout);
              
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        private void WeekScheduler_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var ed = new ClassScheduleModel();
                ResizeGrids(e.NewSize);
                PaintAllEvents(null, IDBranch, IDLivingroom, IDWorkout);
                //PaintAllDayEvents();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        
            if (e.ClickCount >= 2)
            {
                int dayoffset = Convert.ToInt32(((Canvas)sender).Name.Replace("column", "")) - 1;
                if (OnScheduleDoubleClick != null)
                    OnScheduleDoubleClick(sender, new CustomEventArgsDateTime(new DateTime(FirstDay.Year, FirstDay.Month, FirstDay.Day).AddDays(dayoffset)));
            }
        }

        private void PaintAllEvents(DateTime? date, int? idBranch, int? idLivingroom, int? idWorkout)
        {
            try
            {
                if (_scheduler == null || _scheduler.Events == null) return;

                int idW = idLivingroom ?? 0;

                IEnumerable<ClassScheduleModel> eventList;

                if (idWorkout != null)
                {
                    eventList = _scheduler.Events.Where(ev =>
                                                        ev.Start.Date == ev.End.Date &&
                                                        ev.IdBranchOffice == idBranch &&
                                                        ev.IdParent == idW &&
                                                        ev.IdWorkout == (int)idWorkout).OrderBy(ev => ev.Start);
                }
                else
                {
                    eventList = _scheduler.Events.Where(ev =>
                                                        ev.Start.Date == ev.End.Date &&
                                                        ev.IdBranchOffice == idBranch &&
                                                        ev.IdParent == idW).OrderBy(ev => ev.Start);
                }



                if (date == null)
                {
                    column1.Children.Clear();
                    column2.Children.Clear();
                    column3.Children.Clear();
                    column4.Children.Clear();
                    column5.Children.Clear();
                    column6.Children.Clear();
                    column7.Children.Clear();
                }
                else
                {
                    int numColumn = (int)date.Value.Date.Subtract(FirstDay.Date).TotalDays + 1;
                    ((Canvas)this.FindName("column" + numColumn)).Children.Clear();

                    eventList = idWorkout != null ? eventList.Where(ev => ev.Start.Date == date.Value.Date && ev.IdBranchOffice == idBranch && ev.IdParent == idW && ev.IdWorkout == (int)idWorkout).OrderBy(ev => ev.Start) : eventList.Where(ev => ev.Start.Date == date.Value.Date && ev.IdBranchOffice == idBranch && ev.IdParent == idW).OrderBy(ev => ev.Start);
                }

                double columnWidth = EventsGrid.ColumnDefinitions[1].Width.Value;

               // PaintRowsHour((TimeSpan) dS, (TimeSpan) dE);

                PaintRowsHour((int)idBranch);

                foreach (ClassScheduleModel e in eventList)
                {
                    int numColumn = (int)e.Start.Date.Subtract(FirstDay.Date).TotalDays + 1;
                    if (numColumn > 0 && numColumn <= 7)
                    {
                        Canvas sp = (Canvas)this.FindName("column" + numColumn);
                        sp.Width = columnWidth;

                        double oneHourHeight = sp.ActualHeight / 22;


                        var concurrentEvents = new List<ClassScheduleModel>();

                        if (idWorkout != null)
                            concurrentEvents = _scheduler.Events.Where(e1 => ((e1.Start <= e.Start && e1.End > e.Start) ||
                                                                        (e1.Start > e.Start && e1.Start < e.End)) &&
                                                                       e1.End.Date == e1.Start.Date && e1.IdBranchOffice == idBranch && e1.IdParent == idW && e1.IdWorkout == (int)idWorkout).OrderBy(ev => ev.Start).ToList();
                        else
                            concurrentEvents = _scheduler.Events.Where(e1 => ((e1.Start <= e.Start && e1.End > e.Start) ||
                                                                        (e1.Start > e.Start && e1.Start < e.End)) &&
                                                                       e1.End.Date == e1.Start.Date && e1.IdBranchOffice == idBranch && e1.IdParent == idW).OrderBy(ev => ev.Start).ToList();

                        double marginTop = oneHourHeight * (e.Start.Hour + (e.Start.Minute / 60.0));
                        double width = columnWidth / (concurrentEvents.Count());
                        double marginLeft = width * getIndex(e, concurrentEvents);

                        EventUserControl wEvent = new EventUserControl(e, true);
                        wEvent.Width = width;
                        wEvent.Height = e.End.Subtract(e.Start).TotalHours * oneHourHeight;
                        wEvent.Margin = new Thickness(marginLeft, marginTop, 0, 0);
                        wEvent.MouseDoubleClick += ((object sender, MouseButtonEventArgs ea) =>
                        {
                            ea.Handled = true;
                            OnEventDoubleClick(sender, new CustomEventArgsClassSchedule(wEvent.Event));
                        });

                        sp.Children.Add(wEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private int getIndex(ClassScheduleModel e, List<ClassScheduleModel> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (e.Id == list[i].Id) return i;
            }
            return -1;
        }

        private void PaintAllDayEvents()
        {
            try
            {
                if (_scheduler == null || _scheduler.Events == null) return;

                allDayEvents.Children.Clear();

                double columnWidth = EventsGrid.ColumnDefinitions[1].Width.Value;

                foreach (ClassScheduleModel e in _scheduler.Events.Where(ev => ev.End.Date > ev.Start.Date))
                {
                    int numColumn = (int)e.Start.Date.Subtract(FirstDay.Date).TotalDays;
                    int numEndColumn = (int)e.End.Date.Subtract(FirstDay.Date).TotalDays + 1;

                    if (numColumn > 7 || numEndColumn <= 0) continue;

                    if ((numColumn > 0 && numColumn <= 7) || (numEndColumn > 0 && numEndColumn <= 7))
                    {
                        double marginLeft = (numColumn) * columnWidth;

                        EventUserControl wEvent = new EventUserControl(e, false);
                        wEvent.Width = columnWidth * (numEndColumn - numColumn);
                        wEvent.Margin = new Thickness(marginLeft, 0, 0, 0);
                        wEvent.MouseDoubleClick += ((object sender, MouseButtonEventArgs ea) =>
                        {
                            ea.Handled = true;
                            OnEventDoubleClick(sender, new CustomEventArgsClassSchedule(wEvent.Event));
                        });
                        allDayEvents.Children.Add(wEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ResizeGrids(Size newSize)
        {
            EventsGrid.Width = newSize.Width;
            EventsHeaderGrid.Width = newSize.Width;

            double columnWidth = (this.ActualWidth - EventsGrid.ColumnDefinitions[0].ActualWidth) / 7;
            for (int i = 1; i < EventsGrid.ColumnDefinitions.Count; i++)
            {
                EventsGrid.ColumnDefinitions[i].Width = new GridLength(columnWidth);
                EventsHeaderGrid.ColumnDefinitions[i].Width = new GridLength(columnWidth);
            }
        }

        //private void PaintRowsHour(TimeSpan di, TimeSpan de)
        private void PaintRowsHour(int IdB)
        {
            TimeSpan di, de;

            var result = branchOffice.GetByID(IdB);

            di = (TimeSpan)result.StarHour;
            de = (TimeSpan)result.EndHour;

            _scheduler.OnStartJourneyChanged += ((object s, CustomEventArgsTimeSpan t) =>
            {
                if (di.Hours == 0)
                    startJourney.Visibility = System.Windows.Visibility.Hidden;
                else
                    Grid.SetRowSpan(startJourney, di.Hours);
            });

            _scheduler.OnEndJourneyChanged += ((object s, CustomEventArgsTimeSpan t) =>
            {
                if (de.Hours == 0)
                    endJourney.Visibility = System.Windows.Visibility.Hidden;
                else
                {
                    Grid.SetRow(endJourney, de.Hours);
                    Grid.SetRowSpan(endJourney, 24 - de.Hours);
                }
            });

           // this.SizeChanged += WeekScheduler_SizeChanged;


            if (di.Hours != 0)
            {
                double hourHeight = EventsGrid.ActualHeight / 22;
                ScrollEventsViewer.ScrollToVerticalOffset(hourHeight * (di.Hours - 1));
            }

            if (di.Hours == 0)
                startJourney.Visibility = System.Windows.Visibility.Hidden;
            else
                Grid.SetRowSpan(startJourney, di.Hours);

            if (de.Hours == 0)
                endJourney.Visibility = System.Windows.Visibility.Hidden;
            else
            {
                Grid.SetRow(endJourney, de.Hours);
                Grid.SetRowSpan(endJourney, 24 - de.Hours);
            }
        }

    }
}
