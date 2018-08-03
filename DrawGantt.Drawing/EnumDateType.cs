using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGantt.Drawing
{
    /// <summary>
    /// date column min unit type
    /// </summary>
    public enum EnumDateType
    {
        /// <summary>
        /// day,date column split year,month,day
        /// </summary>
        Day,
        /// <summary>
        /// hour,date column split year,month,day,hour
        /// </summary>
        Hour,
        /// <summary>
        /// minute,date column split year,month,day,hour,minute
        /// </summary>
        Minute
    }
}
