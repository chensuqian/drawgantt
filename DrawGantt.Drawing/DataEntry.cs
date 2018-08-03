using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGantt.Drawing
{

    /// <summary>
    /// gantt data entity
    /// </summary>
    public class DataEntry
    {
        /// <summary>
        /// identity seed
        /// </summary>
        private static int IncreaseID = 1;
        public DataEntry()
        {
            this.rowID = IncreaseID;
            IncreaseID++;
        }

        private EnumDataType dataType;
        /// <summary>
        /// data type
        /// </summary>
        public EnumDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        private int rowID;
        /// <summary>
        /// ID
        /// </summary>
        public int RowID
        {
            get { return rowID; }
            set { rowID = value; }
        }
        private Guid pID;
        /// <summary>
        /// primary key 
        /// </summary>
        public Guid PID
        {
            get { return pID; }
            set { pID = value; }
        }
        private String name = String.Empty;
        /// <summary>
        /// name
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String adminName = String.Empty;
        /// <summary>
        /// admin name 
        /// </summary>
        public String AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }
        private EnumStatus? status;
        /// <summary>
        /// status
        /// </summary>
        public EnumStatus? Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// status's description
        /// </summary>
        public String StatusString
        {
            get
            {
                if (this.DataType != EnumDataType.Task)
                {
                    return string.Empty;
                }
                if (this.Status.Value == EnumStatus.End)
                {
                    return "End";
                }
                else if (this.Status.Value == EnumStatus.Going)
                {
                    return "Going";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        private DateTime? startTime;
        /// <summary>
        /// start time
        /// </summary>
        public DateTime? StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// start time's text after format
        /// </summary>
        public String GetStartTimeString(string format)
        {
            try
            {
                return this.StartTime.Value.ToString(format);
            }
            catch
            {
                return this.StartTime.Value.ToString("yyyy/MM/dd HH:mm");
            }
        }

        private DateTime? endTime;
        /// <summary>
        /// end time
        /// </summary>
        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        /// <summary>
        /// end time's text after format
        /// </summary>
        public String GetEndTimeString(string format)
        {
            try
            {
                return this.EndTime.Value.ToString(format);
            }
            catch
            {
                return this.EndTime.Value.ToString("yyyy/MM/dd HH:mm");
            }
        }
        private int? progress;
        /// <summary>
        /// progress
        /// </summary>
        public int? Progress
        {
            get { return progress; }
            set { progress = value; }
        }
        /// <summary>
        /// progress's percent text
        /// </summary>
        public String ProgressString
        {
            get
            {
                if (this.Progress.HasValue)
                {
                    return this.Progress.ToString() + "%";
                }
                return string.Empty;
            }
        }

        private List<Guid> frontPIDs = new List<Guid>();
        /// <summary>
        /// front task's PIDs
        /// </summary>
        public List<Guid> FrontPIDs
        {
            get { return frontPIDs; }
            set { frontPIDs = value; }
        }

        private List<DataEntry> subs = new List<DataEntry>();
        /// <summary>
        /// subs
        /// </summary>
        public List<DataEntry> Subs
        {
            get { return subs; }
            set { subs = value; }
        }


        private bool isExpand = true;
        /// <summary>
        /// show subs display status,if value is true,the subs will show,else ths subs hide
        /// </summary>
        public bool IsExpand
        {
            get { return isExpand; }
            set { isExpand = value; }
        }

        private int level = 1;
        /// <summary>
        /// local data's level in tree structure
        /// 1.if this.DataType==EnumDataType.Stage,the value=1; 
        /// 2.if this.DataType==EnumDataType.StageItem,the value=2; 
        /// 3.if this.DataType==EnumDataType.Task,the value = this.Parent.Level+1
        /// </summary>
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private DataEntry parent = null;
        /// <summary>
        /// parent
        /// if this.DataType==EnumDataType.Stage,the parent==null
        /// </summary>
        public DataEntry Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /// <summary>
        /// description after gantt rectangle 
        /// </summary>
        public String GanttDescription
        {
            get
            {
                return "AdminName：" + this.AdminName + " Progress：" + this.ProgressString;
            }
        }

        private Rectangle rect = new Rectangle();
        /// <summary>
        /// gantt rectangle message
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
    }
}
