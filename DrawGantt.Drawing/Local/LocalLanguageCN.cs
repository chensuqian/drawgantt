using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGantt.Drawing.Local
{
    /// <summary>
    /// chinese
    /// </summary>
    public class LocalLanguageCN : LocalLanguageBase
    {
        public LocalLanguageCN()
        {
            base.weeks.Add(DayOfWeek.Monday, "周一");
            base.weeks.Add(DayOfWeek.Tuesday, "周二");
            base.weeks.Add(DayOfWeek.Wednesday, "周三");
            base.weeks.Add(DayOfWeek.Thursday, "周四");
            base.weeks.Add(DayOfWeek.Friday, "周五");
            base.weeks.Add(DayOfWeek.Saturday, "周六");
            base.weeks.Add(DayOfWeek.Sunday, "周日");
        }

        public override string GetDisplayDayString(DateTime dayTime)
        {
            int day = dayTime.Day;
            String week = base.weeks[dayTime.DayOfWeek];
            return day.ToString() +"号."+ week;
        }

        public override string GetDisplayMonthString(DateTime dayTime)
        {
            return dayTime.Month.ToString() + "月";
        }

        public override string GetDisplayYearString(DateTime dayTime)
        {
            return dayTime.Year.ToString()+"年";
        }
    }
}
