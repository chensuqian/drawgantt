using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGantt.Drawing.Local
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalLanguageEN : LocalLanguageBase
    {
        public LocalLanguageEN()
        {
            InitWeeks();
            InitMonths();
        }

        private void InitWeeks()
        {
            base.weeks.Add(DayOfWeek.Monday, "Mon");
            base.weeks.Add(DayOfWeek.Tuesday, "Tue");
            base.weeks.Add(DayOfWeek.Wednesday, "Wed");
            base.weeks.Add(DayOfWeek.Thursday, "Thur");
            base.weeks.Add(DayOfWeek.Friday, "Fri");
            base.weeks.Add(DayOfWeek.Saturday, "Sat");
            base.weeks.Add(DayOfWeek.Sunday, "Sun");
        }
        private Dictionary<int, String> months = new Dictionary<int, string>();
        private void InitMonths()
        {
            months.Add(1, "January");
            months.Add(2, "February");
            months.Add(3, "March");
            months.Add(4, "April");
            months.Add(5, "May");
            months.Add(6, "June");
            months.Add(7, "July");
            months.Add(8, "August");
            months.Add(9, "September");
            months.Add(10, "October");
            months.Add(11, "November");
            months.Add(12, "December");
        }

        public override string GetDisplayDayString(DateTime dayTime)
        {
            int day = dayTime.Day;
            String week = base.weeks[dayTime.DayOfWeek];
            return day.ToString() +"."+ week;
        }
        public override string GetDisplayMonthString(DateTime dayTime)
        {
            return months[dayTime.Month];
        }

        public override string GetDisplayYearString(DateTime dayTime)
        {
            return dayTime.Year.ToString();
        }
    }
}
