using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PowerClub.Bussiness.Utils
{
    public class Common
    {
        public static string Age(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age)) age--;
           
            return age.ToString();
        }

        public static string GetNameDay(int day)
        {
            if (day > 6) return string.Empty;

            return Enum.GetName(typeof (DayOfWeek), day);
        }

        public static bool ValidateHour(string maskedTextBox)
        {
            try
            {
                string value = maskedTextBox.Replace("_", "0").Replace(":", "").Trim();
                if(value == "0000") return false;

                // get Hour
                int hour = int.Parse(value.Substring(0, 2));
                if ((hour < 0) || (hour > 23)) return false;

                //get minute
                int minute = int.Parse(value.Substring(2, 2));
                if ((minute < 0) || (minute > 59)) return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDate(string inValue)
        {
            bool result  = false;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.Trim());
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }

        //public static bool IsHour(TimeSpan inValue)
        //{
        //    bool result = false;
        //    try
        //    {
        //        TimeSpan myDT = TimeSpan.Parse(inValue.Trim());
        //        result = true;
        //    }
        //    catch (FormatException e)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        public static int IsDay(string inValue)
        {
            int result = -1;
            try
            {
                var x = new DayofWeek();
                string  lower = inValue.Trim().ToLower();

                result = x.GetAll().First(a => a.Name.ToString().ToLower() == lower).Day;
            }
            catch (Exception e)
            {
                result = -1;
            }
            return result;
        }

    }

    public class DayofWeek
    {

        public int Day { get; set; }
        public string Name { get; set; }

        public ObservableCollection<DayofWeek> GetAll()
        {
            var data = new ObservableCollection<DayofWeek>();
            try
            {
                var _days = new List<DayofWeek>();
                _days.Add(new DayofWeek() { Day = 1, Name = "Lunes"});
                _days.Add(new DayofWeek() { Day = 2, Name = "Martes" });
                _days.Add(new DayofWeek() { Day = 3, Name = "Miercoles" });
                _days.Add(new DayofWeek() { Day = 4, Name = "Jueves" });
                _days.Add(new DayofWeek() { Day = 5, Name = "Viernes" });
                _days.Add(new DayofWeek() { Day = 6, Name = "Sabado" });
                _days.Add(new DayofWeek() { Day = 0, Name = "Domingo" });

                return new ObservableCollection<DayofWeek>(_days);

            }
            catch (Exception ex)
            {
                return data;
            }
        }
    }
}
