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

namespace WpfGym
{
    /// <summary>
    /// Interaction logic for EventUserControl.xaml
    /// </summary>
    public partial class EventUserControl : UserControl
    {
        ClassScheduleModel _e;
        public EventUserControl(ClassScheduleModel e, bool showTime)
        {
            InitializeComponent();

            _e = e;

            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.Subject = e.Subject;
            this.BorderElement.Background = e.BrushColor;   
            if (!showTime)
            {
                this.DisplayDateText.Visibility = System.Windows.Visibility.Hidden;
                this.DisplayDateText.Height = 0;
                this.DisplayDateText.Text = String.Format("{0} - {1}", e.Start.ToShortDateString(), e.End.ToShortDateString());
            }
            else
            {
                this.DisplayDateText.Text = String.Format("{0} - {1}", e.Start.ToString("HH:mm"), e.End.ToString("HH:mm"));
            }
            DisplayTextTrainer.Text = e.TrainerName;
            this.BorderElement.ToolTip = this.DisplayDateText.Text + System.Environment.NewLine + this.DisplayText.Text + System.Environment.NewLine + DisplayTextTrainer.Text;
            
        }


        public ClassScheduleModel Event
        {
            get { return _e; }
        }

        #region Subject
        public static readonly DependencyProperty SubjectProperty =
            DependencyProperty.Register("Subject", typeof(string), typeof(EventUserControl),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(AdjustSubject)));

        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }

        private static void AdjustSubject(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as EventUserControl).DisplayText.Text = (string)e.NewValue;
        }
        #endregion

    }
}
