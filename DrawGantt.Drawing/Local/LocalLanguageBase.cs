using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGantt.Drawing.Local
{
    /// <summary>
    /// define some avariable
    /// </summary>
    public class LocalLanguageBase
    {
        /// <summary>
        /// Week to string,display in date column
        /// </summary>
        public Dictionary<DayOfWeek, String> weeks = new Dictionary<DayOfWeek, string>();

        public LocalLanguageBase()
        {
        }

        /// <summary>
        /// display date column's Day text
        /// </summary>
        /// <param name="dayTime"></param>
        /// <returns></returns>
        public virtual String GetDisplayDayString(DateTime dayTime)
        {
            throw new Exception("must be override");
        }
        /// <summary>
        /// display date column's Month text
        /// </summary>
        /// <param name="dayTime"></param>
        /// <returns></returns>
        public virtual String GetDisplayMonthString(DateTime dayTime)
        {
            throw new Exception("must be override");
        }
        /// <summary>
        /// display date column's Year text
        /// </summary>
        /// <param name="dayTime"></param>
        /// <returns></returns>
        public virtual String GetDisplayYearString(DateTime dayTime)
        {
            throw new Exception("must be override");
        }
    }
}
