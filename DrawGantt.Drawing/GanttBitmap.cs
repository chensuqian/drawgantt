using DrawGantt.Drawing.Local;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace DrawGantt.Drawing
{
    /// <summary>
    /// Draw Gantt
    /// use memory = screenWidth * screenHeight * 4
    /// a lot of data will throw exception OOM
    /// </summary>
    public class GanttBitmap
    {
        /// <summary>
        /// default use cn language
        /// </summary>
        LocalLanguageBase language=LocalLanguageFactory.CreateInstance("cn");

        #region global settings
        /// <summary>
        /// bitmap  width
        /// </summary>
        int screenWidth = 1024;
        /// <summary>
        /// bitmap  height
        /// </summary>
        int screenHeight = 1024;

        /// <summary>
        /// bitmap backgroundColor
        /// </summary>
        int backgroundColor = Color.FromArgb(252, 252, 252).ToArgb();
        /// <summary>
        /// bitmap background Brush
        /// </summary>
        Brush backgroupBrush;
        /// <summary>
        /// draw name space
        /// </summary>
        int marginTop = 100;

        /// <summary>
        /// draw description space
        /// </summary>
        int marginBottom = 60;
        /// <summary>
        /// Bitmap
        /// </summary>
        Bitmap bitmap = null;
        /// <summary>
        /// Graphics
        /// </summary>
        Graphics graphics = null;
        /// <summary>
        /// date column type
        /// </summary>
        EnumDateType dateType = EnumDateType.Day;
        /// <summary>
        /// memory size limit 2G
        /// </summary>
        long maxCache = 2L * 1024 * 1024 * 1024;
        #endregion

        #region title
        /// <summary>
        /// title name
        /// </summary>
        String title = string.Empty;
        /// <summary>
        /// title Color
        /// </summary>
        int titleColor = Color.FromArgb(41, 41, 41).ToArgb();
        /// <summary>
        /// title Size
        /// </summary>
        int titleSize = 36;
        /// <summary>
        /// title Brush
        /// </summary>
        Brush titleBrush;
        /// <summary>
        /// title Font
        /// </summary>
        Font titleFont;
        #endregion

        #region description
        /// <summary>
        /// description text
        /// </summary>
        String description = string.Empty;
        /// <summary>
        /// description Color
        /// </summary>
        int descriptionColor = Color.FromArgb(27, 47, 77).ToArgb();
        /// <summary>
        /// description Size
        /// </summary>
        int descriptionSize = 18;
        /// <summary>
        /// description Brush
        /// </summary>
        Brush descriptionBrush;
        /// <summary>
        /// description Font
        /// </summary>
        Font descriptionFont;
        #endregion

        #region date column
        /// <summary>
        /// Date Height
        /// </summary>
        int topDateHeight = 30;
        /// <summary>
        /// Date Color
        /// </summary>
        int topDateTitleColor = Color.FromArgb(110, 110, 110).ToArgb();
        /// <summary>
        /// Date Size
        /// </summary>
        int topDateTitleSize = 16;
        /// <summary>
        /// Date Brush
        /// </summary>
        Brush topDateTitleBrush;
        /// <summary>
        /// Date Font
        /// </summary>
        Font topDateTitleFont;
        #endregion

        #region data grid
        /// <summary>
        /// data grid height
        /// </summary>
        int dataGridHeight = 50;
        /// <summary>
        /// data grid width
        /// </summary>
        int dataGridWidth = 120;
        /// <summary>
        /// data grid line color
        /// </summary>
        int dataGridLineColor = Color.FromArgb(210, 210, 210).ToArgb();
        /// <summary>
        /// data grid line width
        /// </summary>
        int dataGridLineWidth = 1;
        /// <summary>
        /// data grid line pen
        /// </summary>
        Pen dataGridLinePen;
        #endregion

        #region left message block
        /// <summary>
        /// no.1 column,RowNumber width
        /// </summary>
        int numberWidth = 80;
        /// <summary>
        /// no.2 column,DataEntry.Name width
        /// </summary>
        int nameWidth = 500;
        /// <summary>
        /// no.3 column,DataEntry.AdminName width
        /// </summary>
        int adminNameWidth = 200;
        /// <summary>
        /// no.4 column,DataEntry.Status width
        /// </summary>
        int statusWidth = 100;
        /// <summary>
        /// no.5 column,DataEntry.StartTime width
        /// </summary>
        int startTimeWidth = 240;
        /// <summary>
        /// no.6 column,DataEntry.EndTime width
        /// </summary>
        int endTimeWidth = 240;
        /// <summary>
        ///  no.7 column,DataEntry.Progress width
        /// </summary>
        int progressWidth = 120;
        #endregion

        #region left message block -- LabelTitle
        /// <summary>
        /// LabelTitle Color
        /// </summary>
        int leftLabelTitleColor = Color.FromArgb(75, 75, 75).ToArgb();
        /// <summary>
        /// LabelTitle Size
        /// </summary>
        int leftLabelTitleSize = 20;
        /// <summary>
        /// LabelTitle Brush
        /// </summary>
        Brush leftLabelTitleBrush;
        /// <summary>
        /// LabelTitle Font
        /// </summary>
        Font leftLabelTitleFont;
        #endregion

        #region main grid
        /// <summary>
        /// mainGrid Color
        /// </summary>
        int mainGridColor = Color.FromArgb(125, 125, 125).ToArgb();
        /// <summary>
        /// mainGridLine Width
        /// </summary>
        int mainGridLineWidth = 1;
        /// <summary>
        /// mainGrid Pen
        /// </summary>
        Pen mainGridPen;
        #endregion

        #region left message block -- DataLabel
        /// <summary>
        /// dataLabel Color
        /// </summary>
        int dataLabelColor = Color.FromArgb(20, 20, 20).ToArgb();
        /// <summary>
        /// dataLabel Size
        /// </summary>
        int dataLabelSize = 16;
        /// <summary>
        /// dataLabel Brush
        /// </summary>
        Brush dataLabelBrush;
        /// <summary>
        /// dataLabel Font
        /// </summary>
        Font dataLabelFont;
        #endregion

        #region gantt rectangle text
        /// <summary>
        ///  gantt rectangle text Color 
        /// </summary>
        int ganttDataTextColor = Color.FromArgb(255, 255, 255).ToArgb();
        /// <summary>
        /// gantt rectangle text
        /// </summary>
        int ganttDataTextSize = 16;
        /// <summary>
        /// gantt rectangle text Font
        /// </summary>
        Font ganttDataTextFont;
        /// <summary>
        /// gantt rectangle text Brush
        /// </summary>
        Brush ganttDataTextBrush;
        #endregion

        #region  gantt description
        /// <summary>
        ///  gantt description text Color
        /// </summary>
        int ganttDataDescriptionColor = Color.FromArgb(27, 47, 77).ToArgb();
        /// <summary>
        /// gantt description text Size
        /// </summary>
        int ganttDataDescriptionSize = 16;
        /// <summary>
        /// gantt description text Font
        /// </summary>
        Font ganttDataDescriptionFont;
        /// <summary>
        /// gantt description text Brush
        /// </summary>
        Brush ganttDataDescriptionBrush;
        #endregion

        #region gantt rectangle
        /// <summary>
        /// gantt rectangle Color
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        int ganttDataGoingCompleteProgressColor = Color.FromArgb(42, 172, 251).ToArgb();
        /// <summary>
        /// gantt rectangle Brush,fill rectangle
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        Brush ganttDataGoingCompleteProgressSolidBrush;
        /// <summary>
        /// gantt rectangle Pen,draw rectangle border
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        Pen ganttDataGoingCompleteProgressPen;

        /// <summary>
        /// gantt rectangle Color
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        int ganttDataGoingUnCompleteProgressColor = Color.FromArgb(133, 207, 254).ToArgb();
        /// <summary>
        /// gantt rectangle Brush
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        Brush ganttDataGoingUnCompleteProgressSolidBrush;
        /// <summary>
        /// gantt rectangle Pen
        /// 1.DataEntry.Status value is EnumStatus.Going
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        Pen ganttDataGoingUnCompleteProgressPen;

        /// <summary>
        /// gantt rectangle Color
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        int ganttDataEndCompleteProgressColor = Color.FromArgb(131, 136, 151).ToArgb();
        /// <summary>
        /// gantt rectangle Brush
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        Brush ganttDataEndCompleteProgressSolidBrush;
        /// <summary>
        /// gantt rectangle Pen
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress has Completed
        /// </summary>
        Pen ganttDataEndCompleteProgressPen;

        /// <summary>
        /// gantt rectangle Color
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        int ganttDataEndUnCompleteProgressColor = Color.FromArgb(148, 149, 180).ToArgb();
        /// <summary>
        /// gantt rectangle Brush
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        Brush ganttDataEndUnCompleteProgressSolidBrush;
        /// <summary>
        /// gantt rectangle Pen
        /// 1.DataEntry.Status value is EnumStatus.End
        /// 2.DataEntry.Progress remaining value
        /// </summary>
        Pen ganttDataEndUnCompleteProgressPen;
        #endregion

        #region Task's Relation
        /// <summary>
        /// Task's Relation Line Color
        /// </summary>
        int relationLineColor = Color.FromArgb(223, 142, 75).ToArgb();
        /// <summary>
        /// Task's Relation Line Width
        /// </summary>
        int relationLineWidth = 3;
        /// <summary>
        /// Task's Relation Line Pen
        /// </summary>
        Pen relationLinePen;
        /// <summary>
        /// Task's Relation Line Brush
        /// </summary>
        Brush relationLineBrush;
        /// <summary>
        /// Task's Relation broken line width (at left)
        /// </summary>
        int relationLineMarginLeft = 20;
        /// <summary>
        /// Task's Relation broken line width (at right)
        /// </summary>
        int relationLineMarginRight = 10;
        #endregion

        /// <summary>
        /// gantt rectangle's margin in data grid, top and bottom
        /// </summary>
        int marginGanttData = 7;

        /// <summary>
        /// gantt rectangle's heigth = dataGridHeight - 2 * marginGanttData
        /// </summary>
        int ganttDataHeight = 0;

        /// <summary>
        /// temp variable：left messagle block's width
        /// the value is Calculate result
        /// </summary>
        int leftLabelWidth;

        /// <summary>
        /// y start offset = marginTop + dateTotalHeight
        /// </summary>
        int yStartOffset;

        /// <summary>
        /// gantt data (data structure:tree)
        /// Parameters passed externally
        /// </summary>
        List<DataEntry> entryListTree = new List<DataEntry>();

        /// <summary>
        ///  gantt data (data structure:list)
        ///  convert entryListTree to List,because the DataEntry maybe do not show subs,
        ///  so the entryList  count <= entryListTree  total count
        /// </summary>
        List<DataEntry> entryList = new List<DataEntry>();

        /// <summary>
        /// gantt starttime，the value is min date of entryList previous day
        /// </summary>
        DateTime startTime = DateTime.Now;
        /// <summary>
        /// gantt starttime，the value is max date of entryList add 3 days
        /// </summary>
        DateTime endTime = DateTime.Now;
        /// <summary>
        /// temp variable：record xStart's offset
        /// </summary>
        int leftOffset = 0;
        /// <summary>
        ///left margin of draw text in left message block 
        /// </summary>
        int dataLabelMarginLeft = 20;
        /// <summary>
        /// icon width in left message block 
        /// </summary>
        int iconWidth = 24;
        /// <summary>
        /// margin between in icons in left message block 
        /// </summary>
        int iconMargin = 6;
        /// <summary>
        /// gantt rectangle width
        /// </summary>
        int pieHeight = 6;
        /// <summary>
        /// DataEntry.StartTime DataEntry.EndTime format string
        /// </summary>
        String dateFormat = "yyyy/MM/dd HH:mm";
        /// <summary>
        ///  date column total height，call CalculateTopDateHeight()
        /// </summary>
        int dateTotalHeight = 0;

        /// <summary>
        /// temp variable: x start point
        /// </summary>
        int xStart = 0;
        /// <summary>
        /// temp variable: y start point
        /// </summary>
        int yStart = 0;
        /// <summary>
        /// temp variable: x end point
        /// </summary>
        int xEnd = 0;
        /// <summary>
        /// temp variable: y end point
        /// </summary>
        int yEnd = 0;

        /// <summary>
        /// constructor 
        /// init weeks key value pair
        /// </summary>
        public GanttBitmap()
        {
        }
        /// <summary>
        /// on dateType,Calculate date column total height
        /// </summary>
        private void CalculateTopDateTotalHeight()
        {
            switch (this.dateType)
            {
                case EnumDateType.Day:
                    this.dateTotalHeight = 4 * mainGridLineWidth + 3 * topDateHeight;
                    break;
                case EnumDateType.Hour:
                    this.dateTotalHeight = 5 * mainGridLineWidth + 4 * topDateHeight;
                    break;
                case EnumDateType.Minute:
                    this.dateTotalHeight = 6 * mainGridLineWidth + 5 * topDateHeight;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// set the gantt data
        /// init all variable
        /// </summary>
        /// <param name="entryListTree">gantt data,tree structure</param>
        public void SetData(List<DataEntry> entryListTree)
        {
            if (entryListTree == null || entryListTree.Count == 0)
            {
                throw new ArgumentNullException("the entryListTree is null or empty");
            }
            InitStartEndDate();
            this.entryListTree = entryListTree;
            AddToList(entryListTree);
            FindStartEnd();
            CalculateTopDateTotalHeight();
            CalculateWidthHeght();
            InitBitmap();
            InitPens();
            initBrushs();
            Draw();
        }
        /// <summary>
        /// init starttime and endtime
        /// starttime default value:first day of datetime.now month 
        /// endtime default value:last day of datetime.now month 
        /// </summary>
        private void InitStartEndDate()
        {
            DateTime now = DateTime.Now;
            startTime = new DateTime(now.Year, now.Month, 1);
            endTime = startTime.AddMonths(1).AddDays(-1);
        }
        /// <summary>
        /// create Bitmap
        /// </summary>
        private void InitBitmap()
        {
            bitmap = new Bitmap(screenWidth, screenHeight);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
        }
        /// <summary>
        /// init all Pen
        /// </summary>
        private void InitPens()
        {
            mainGridPen = new Pen(Color.FromArgb(mainGridColor));
            mainGridPen.Width = mainGridLineWidth;

            dataGridLinePen = new Pen(Color.FromArgb(dataGridLineColor));
            dataGridLinePen.Width = dataGridLineWidth;

            relationLinePen = new Pen(Color.FromArgb(relationLineColor));
            relationLinePen.Width = relationLineWidth;


            ganttDataEndCompleteProgressPen = new Pen(Color.FromArgb(ganttDataEndCompleteProgressColor), 2);
            ganttDataEndUnCompleteProgressPen = new Pen(Color.FromArgb(ganttDataEndUnCompleteProgressColor), 2);

            ganttDataGoingCompleteProgressPen = new Pen(Color.FromArgb(ganttDataGoingCompleteProgressColor), 2);
            ganttDataGoingUnCompleteProgressPen = new Pen(Color.FromArgb(ganttDataGoingUnCompleteProgressColor), 2);


        }
        /// <summary>
        /// init all Brush
        /// </summary>
        private void initBrushs()
        {
            titleFont = new Font("Times New Roman", titleSize);
            titleBrush = new SolidBrush(Color.FromArgb(titleColor));

            descriptionFont = new Font("Times New Roman", descriptionSize);
            descriptionBrush = new SolidBrush(Color.FromArgb(descriptionColor));

            leftLabelTitleFont = new Font("Times New Roman", leftLabelTitleSize);
            leftLabelTitleBrush = new SolidBrush(Color.FromArgb(leftLabelTitleColor));

            topDateTitleFont = new Font("Times New Roman", topDateTitleSize);
            topDateTitleBrush = new SolidBrush(Color.FromArgb(topDateTitleColor));

            dataLabelFont = new Font("Times New Roman", dataLabelSize);
            dataLabelBrush = new SolidBrush(Color.FromArgb(dataLabelColor));

            backgroupBrush = new SolidBrush(Color.FromArgb(backgroundColor));

            ganttDataDescriptionFont = new Font("Times New Roman", ganttDataDescriptionSize);
            ganttDataDescriptionBrush = new SolidBrush(Color.FromArgb(ganttDataDescriptionColor));


            ganttDataTextFont = new Font("Times New Roman", ganttDataTextSize);
            ganttDataTextBrush = new SolidBrush(Color.FromArgb(ganttDataTextColor));

            ganttDataEndCompleteProgressSolidBrush = new SolidBrush(Color.FromArgb(ganttDataEndCompleteProgressColor));
            ganttDataEndUnCompleteProgressSolidBrush = new SolidBrush(Color.FromArgb(ganttDataEndUnCompleteProgressColor));

            ganttDataGoingCompleteProgressSolidBrush = new SolidBrush(Color.FromArgb(ganttDataGoingCompleteProgressColor));
            ganttDataGoingUnCompleteProgressSolidBrush = new SolidBrush(Color.FromArgb(ganttDataGoingUnCompleteProgressColor));

            relationLineBrush = new SolidBrush(Color.FromArgb(relationLineColor));
        }
        /// <summary>
        /// draw bitmap
        /// </summary>
        private void Draw()
        {
            DrawBackground();
            DrawTitle();
            DrawLandscapeLine();
            DrawVerticalLine();
            DrawDateDividerLine();
            DrawDataLabel();
            DrawGantt();
            DrawDescription();
        }
        /// <summary>
        /// draw bitmap background
        /// </summary>
        private void DrawBackground()
        {
            graphics.FillRectangle(backgroupBrush, 0, 0, screenWidth, screenHeight);
        }
        private void DrawTitle()
        {
            graphics.DrawString(title, titleFont, titleBrush, new PointF(30, 30));
        }
        private void DrawDescription()
        {
            SizeF sf = graphics.MeasureString(description, descriptionFont);
            graphics.DrawString(description, descriptionFont, descriptionBrush, new PointF(30, screenHeight - marginBottom + (marginBottom - sf.Height) / 2 + 2));
        }
        private void DrawLandscapeLine()
        {
            int dateCount = GetDateCount();
            //first landscape line,divider title area and date column
            xStart = 0;
            yStart = marginTop;
            xEnd = screenWidth;
            yEnd = marginTop;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));

            //date column landscape line
            for (int i = 1; i < dateCount; i++)
            {
                xStart = leftLabelWidth;
                yStart = marginTop + i * (topDateHeight + mainGridLineWidth);
                xEnd = screenWidth;
                yEnd = yStart;
                graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            }

            //date column last landscape line,divider date column and data grid
            xStart = 0;
            yStart = marginTop + dateCount * (topDateHeight + mainGridLineWidth);
            xEnd = screenWidth;
            yEnd = yStart;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));

            //data grid landscape line
            for (int i = 1; i < entryList.Count; i++)
            {
                xStart = 0;
                yStart = marginTop + dateTotalHeight + dataGridHeight * i + dataGridLineWidth * (i - 1);
                xEnd = screenWidth;
                yEnd = yStart;
                graphics.DrawLine(dataGridLinePen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            }

            //the last data grid landscape line,divider data grid and description
            xStart = 0;
            yStart = screenHeight - marginBottom;
            xEnd = screenWidth;
            yEnd = yStart;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
        }
        /// <summary>
        /// date column landscape count，according to the date precision
        /// </summary>
        /// <returns></returns>
        private int GetDateCount()
        {
            switch (this.dateType)
            {
                case EnumDateType.Day:
                    return 3;
                case EnumDateType.Hour:
                    return 4;
                case EnumDateType.Minute:
                    return 5;
                default:
                    throw new NotSupportedException("dateType's value not Supported");
            }
        }
        private void DrawVerticalLine()
        {
            leftOffset = 0;
            #region left message block

            //first vertical line
            xStart = numberWidth;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));

            String label = "ID";
            SizeF sf = graphics.MeasureString(label, leftLabelTitleFont);
            int dataLabelPointY = marginTop + ((int)((mainGridLineWidth + topDateHeight) * 3 - sf.Height)) / 2 + 2;

            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));

            //second vertical line
            xStart = numberWidth + nameWidth + mainGridLineWidth * 1;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "Name";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));
            leftOffset = xStart;
            //third vertical line
            xStart = numberWidth + nameWidth + adminNameWidth + mainGridLineWidth * 2;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "AdminName";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));
            leftOffset = xStart;
            //fourth vertical line
            xStart = numberWidth + nameWidth + adminNameWidth + statusWidth + mainGridLineWidth * 3;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "Status";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));
            leftOffset = xStart;
            //fifth vertical line
            xStart = numberWidth + nameWidth + adminNameWidth + statusWidth + startTimeWidth + mainGridLineWidth * 4;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "StartTime";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));
            leftOffset = xStart;
            //sixth vertical line
            xStart = numberWidth + nameWidth + adminNameWidth + statusWidth + startTimeWidth + endTimeWidth + mainGridLineWidth * 5;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = marginTop + (mainGridLineWidth + topDateHeight) * 3;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "EndTime";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));
            leftOffset = xStart;
            //seventh vertical line
            xStart = numberWidth + nameWidth + adminNameWidth + statusWidth + startTimeWidth + endTimeWidth + progressWidth + mainGridLineWidth * 6;
            yStart = marginTop;
            xEnd = xStart;
            yEnd = screenHeight - marginBottom;
            graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));
            label = "Progress";
            sf = graphics.MeasureString(label, leftLabelTitleFont);
            graphics.DrawString(label, leftLabelTitleFont, leftLabelTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, dataLabelPointY));

            #endregion

            #region date column
            //this only draw day's vertical divider
            leftOffset = leftLabelWidth;
            //total days
            int days = (int)(endTime.Date - startTime.Date).TotalDays;
            DateTime dayTime = startTime;
            xStart = leftOffset;
            label = GetDayString(dayTime);
            sf = graphics.MeasureString(label, topDateTitleFont);
            yStart = marginTop + topDateHeight * 2 + mainGridLineWidth * 3;
            graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(xStart + (dataGridWidth - sf.Width) / 2, yStart + (int)(topDateHeight - sf.Height) / 2 + 3));
            leftOffset = leftOffset + dataGridWidth + dataGridLineWidth;
            for (int i = 1; i <= days; i++)
            {
                dayTime = dayTime.AddDays(1);
                xStart = leftLabelWidth + dataGridWidth * i + dataGridLineWidth * (i - 1);
                xEnd = xStart;
                yEnd = screenHeight - marginBottom - mainGridLineWidth;
                graphics.DrawLine(dataGridLinePen, new Point(xStart, yStart), new Point(xEnd, yEnd));

                label = GetDayString(dayTime);
                sf = graphics.MeasureString(label, topDateTitleFont);
                graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(xStart + (dataGridWidth - sf.Width) / 2, yStart + (int)(topDateHeight - sf.Height) / 2 + 3));
                leftOffset = xStart;
            }
            #endregion
        }
        /// <summary>
        /// draw date column divider of year and month
        /// </summary>
        private void DrawDateDividerLine()
        {
            //draw year's vertical divider
            SizeF sf;
            leftOffset = leftLabelWidth;
            int startYear = startTime.Year;
            int endYear = endTime.Year;
            String label = string.Empty;
            if (startYear < endYear)
            {
                //if year of starttime not equal year of endtime,calculate x point on time percent
                for (int year = (startYear + 1); year <= endYear; year++)
                {
                    //get first day of next year
                    DateTime nextYear = new DateTime(year, 1, 1);
                    int tempDays = (int)(nextYear - startTime).TotalDays;
                    xStart = leftLabelWidth + dataGridWidth * tempDays + (tempDays - 1) * dataGridLineWidth;
                    yStart = marginTop + mainGridLineWidth;
                    xEnd = xStart;
                    yEnd = yStart + topDateHeight;
                    graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));

                    label = GetYearString(nextYear.AddYears(-1));
                    //draw year text
                    sf = graphics.MeasureString(label, topDateTitleFont);
                    graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(leftLabelWidth + (xEnd - leftLabelWidth - sf.Width) / 2, yStart + (topDateHeight - sf.Height) / 2));
                    leftOffset = xStart;
                }
                //draw last year text
                label = GetYearString(endTime);
                sf = graphics.MeasureString(label, topDateTitleFont);
                graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(xStart + (screenWidth - xStart - sf.Width) / 2, yStart + (topDateHeight - sf.Height) / 2));
            }
            else
            {
                yStart = marginTop + mainGridLineWidth;
                //year of starttime equals year of endtime,no year divider,draw year text
                label = GetYearString(startTime);
                sf = graphics.MeasureString(label, topDateTitleFont);
                graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(leftLabelWidth + (screenWidth - leftLabelWidth - sf.Width) / 2, yStart + (topDateHeight - sf.Height) / 2));
            }

            //draw month's vertical divider
            int startMonth = startTime.Month;
            int endMonth = endTime.Month;

            DateTime tempStartTime = new DateTime(startTime.Year, startTime.Month, 1);
            leftOffset = leftLabelWidth;
            while (tempStartTime.AddMonths(1) < endTime)
            {
                tempStartTime = tempStartTime.AddMonths(1);
                startMonth = tempStartTime.Month;
                int tempDays = (int)(tempStartTime - startTime).TotalDays;

                xStart = leftLabelWidth + dataGridWidth * tempDays + (tempDays - 1) * dataGridLineWidth;
                yStart = marginTop + mainGridLineWidth * 2 + topDateHeight;
                xEnd = xStart;
                yEnd = yStart + topDateHeight;
                graphics.DrawLine(mainGridPen, new Point(xStart, yStart), new Point(xEnd, yEnd));

                label = GetMonthString(tempStartTime.AddMonths(-1));
                sf = graphics.MeasureString(label, topDateTitleFont);

                graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(leftOffset + (xEnd - leftOffset - sf.Width) / 2, yStart + (topDateHeight - sf.Height) / 2 + 2));

                leftOffset = xStart;
            }
            label = GetMonthString(endTime);
            sf = graphics.MeasureString(label, topDateTitleFont);
            graphics.DrawString(label, topDateTitleFont, topDateTitleBrush, new PointF(leftOffset + (screenWidth - leftOffset - sf.Width) / 2, yStart + (topDateHeight - sf.Height) / 2 + 2));

        }
        private String GetYearString(DateTime datetime)
        {
            return language.GetDisplayYearString(datetime);
        }
        private String GetMonthString(DateTime datetime)
        {
            return language.GetDisplayMonthString(datetime);
        }

        /// <summary>
        /// draw left message block，this will reset the DataEntry.RowID
        /// </summary>
        private void DrawDataLabel()
        {
            leftOffset = 0;
            for (int i = 0; i < entryList.Count; i++)
            {
                DataEntry entry = entryList[i];
                xStart = leftOffset;
                yStart = yStartOffset + (dataGridHeight + dataGridLineWidth) * i;

                SizeF sf = graphics.MeasureString("ID", dataLabelFont);

                int dataLabelMarginTop = (int)((dataGridHeight - sf.Height) / 2) + 3;
                int pointY = yStart + dataLabelMarginTop;

                //ID,from 1
                entry.RowID = i + 1;
                graphics.DrawString(entry.RowID.ToString(), dataLabelFont, dataLabelBrush, new PointF(xStart + dataLabelMarginLeft, pointY));

                //icon
                xStart = numberWidth + mainGridLineWidth + dataLabelMarginLeft / 2;
                int iconOffset = iconWidth + iconMargin;
                if (entry.DataType == EnumDataType.Stage)
                {
                    DrawStageIcon(entry);
                }
                else if (entry.DataType == EnumDataType.StageItem)
                {
                    DrawStageItemIcon(entry, iconOffset);
                }
                else
                {
                    DrawTaskIcon(entry, iconOffset);
                }
                //name
                xStart = xStart + iconOffset;
                int maxXStart = numberWidth + nameWidth + mainGridLineWidth;

                sf = graphics.MeasureString(entry.Name, dataLabelFont);
                int nameSpaceLength = maxXStart - xStart;
                if (nameSpaceLength > (sf.Width + dataLabelMarginLeft))
                {
                    graphics.DrawString(entry.Name, dataLabelFont, dataLabelBrush, new PointF(xStart + dataLabelMarginLeft, pointY));
                }
                else
                {
                    graphics.DrawString(GetSubTaskName(entry, nameSpaceLength - dataLabelMarginLeft), dataLabelFont, dataLabelBrush, new PointF(xStart + dataLabelMarginLeft, pointY));
                }

                if (entry.DataType == EnumDataType.Task)
                {
                    //AdminName
                    xStart = numberWidth + mainGridLineWidth;
                    xStart = xStart + nameWidth + mainGridLineWidth;
                    graphics.DrawString(entry.AdminName, dataLabelFont, dataLabelBrush, new PointF(xStart + dataLabelMarginLeft, pointY));

                    //Status
                    xStart = xStart + adminNameWidth + mainGridLineWidth;
                    sf = graphics.MeasureString(entry.StatusString, dataLabelFont);
                    graphics.DrawString(entry.StatusString, dataLabelFont, dataLabelBrush, new PointF(xStart + (statusWidth - sf.Width) / 2, pointY));

                    //StartTime
                    xStart = xStart + statusWidth + mainGridLineWidth;
                    sf = graphics.MeasureString(entry.GetStartTimeString(this.dateFormat), dataLabelFont);
                    graphics.DrawString(entry.GetStartTimeString(this.dateFormat), dataLabelFont, dataLabelBrush, new PointF(xStart + (startTimeWidth - sf.Width) / 2, pointY));

                    //EndTime
                    xStart = xStart + startTimeWidth + mainGridLineWidth;
                    sf = graphics.MeasureString(entry.GetEndTimeString(this.dateFormat), dataLabelFont);
                    graphics.DrawString(entry.GetEndTimeString(this.dateFormat), dataLabelFont, dataLabelBrush, new PointF(xStart + (endTimeWidth - sf.Width) / 2, pointY));

                    //Progress
                    xStart = xStart + endTimeWidth + mainGridLineWidth;
                    graphics.DrawString(entry.ProgressString, dataLabelFont, dataLabelBrush, new PointF(xStart + dataLabelMarginLeft, pointY));
                }
            }
        }
        private void DrawStageIcon(DataEntry entry)
        {
            if (entry.Subs.Count == 0)
            {
                DrawNoSubsIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                return;
            }
            if (entry.IsExpand)
            {
                DrawExpandIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + (iconWidth + iconMargin);
                DrawHasSubsAndExpandedIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
            else
            {
                DrawRetractIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + (iconWidth + iconMargin);
                DrawHasSubsButUnExpandIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
        }
        private void DrawStageItemIcon(DataEntry entry, int iconOffset)
        {
            xStart = xStart + iconOffset;
            if (entry.Subs.Count == 0)
            {
                DrawNoSubsIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                return;
            }
            if (entry.IsExpand)
            {
                DrawExpandIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + iconOffset;
                DrawHasSubsAndExpandedIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
            else
            {
                DrawRetractIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + iconOffset;
                DrawHasSubsButUnExpandIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
        }
        private void DrawTaskIcon(DataEntry entry, int iconOffset)
        {
            xStart = xStart + (entry.Level - 1) * iconOffset;
            if (entry.Subs.Count == 0)
            {
                DrawNoSubsIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                return;
            }
            if (entry.IsExpand)
            {
                DrawExpandIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + iconOffset;
                DrawHasSubsAndExpandedIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
            else
            {
                DrawRetractIcon(xStart, yStart + (dataGridHeight - 24) / 2);
                xStart = xStart + iconOffset;
                DrawHasSubsButUnExpandIcon(xStart, yStart + (dataGridHeight - 30) / 2);
            }
        }
        /// <summary>
        /// if DataEntry.IsExpand==true,draw retract icon
        /// </summary>
        private void DrawExpandIcon(int xStart, int yStart)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons", "icon_retract.png");
            graphics.DrawImage(Image.FromFile(path), new Point(xStart, yStart));
        }
        /// <summary>
        /// if DataEntry.IsExpand==false,draw expand icon 
        /// </summary>
        private void DrawRetractIcon(int xStart, int yStart)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"icons", "icon_expand.png");
            graphics.DrawImage(Image.FromFile(path), new Point(xStart, yStart));
        }
        /// <summary>
        /// has subs and DataEntry.IsExpand = true
        /// </summary>
        private void DrawHasSubsAndExpandedIcon(int xStart, int yStart)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons", "icon_stage.png");
            graphics.DrawImage(Image.FromFile(path), new Point(xStart, yStart));
        }
        /// <summary>
        ///  has subs but DataEntry.IsExpand = false
        /// </summary>
        private void DrawHasSubsButUnExpandIcon(int xStart, int yStart)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons", "icon_stageitem.png");
            graphics.DrawImage(Image.FromFile(path), new Point(xStart, yStart));
        }
        /// <summary>
        /// no subs
        /// </summary>
        private void DrawNoSubsIcon(int xStart, int yStart)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons", "icon_task.png");
            graphics.DrawImage(Image.FromFile(path), new Point(xStart, yStart));
        }
        private void DrawGantt()
        {
            //draw stage and stageitem total timeline
            DrawGanttTotalTimeLine();

            for (int i = 0; i < entryList.Count; i++)
            {
                DataEntry entry = entryList[i];
                if (entry.DataType != EnumDataType.Task)
                {
                    continue;
                }
                CalculateGanttRectanglePoint(entry);
            }
            //first draw relation line,then draw ractangle,in order the relation line under the ractangle
            DrawGanttRelationLine();

            for (int i = 0; i < entryList.Count; i++)
            {
                DataEntry entry = entryList[i];
                if (entry.DataType != EnumDataType.Task)
                {
                    continue;
                }
                DrawGanttRectangle(entry);
                DrawGanttTextInRectangle(entry);
                DrawGanttDescriptionAfterRectangle(entry);
            }
        }
        /// <summary>
        /// draw stage and stageitem total timeline
        /// </summary>
        private void DrawGanttTotalTimeLine()
        {
            for (int i = 0; i < entryList.Count; i++)
            {
                DataEntry e = entryList[i];
                if (e.DataType == EnumDataType.Stage && e.IsExpand)
                {
                    FindStartEndOfStage(e);
                    DrawGanttTotalTimeLine(e);
                }
                else if (e.DataType == EnumDataType.StageItem && e.IsExpand)
                {
                    DrawGanttTotalTimeLine(e);
                }
                else
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// draw total timeline
        /// </summary>
        private void DrawGanttTotalTimeLine(DataEntry e)
        {
            CalculateGanttRectanglePoint(e);
            //draw left triangle
            Point[] points = new Point[3];
            points[0] = new Point(e.Rect.Left, e.Rect.Top);
            points[1] = new Point(e.Rect.Left, e.Rect.Top + dataGridHeight / 2);
            points[2] = new Point(e.Rect.Left + dataGridHeight / 2, e.Rect.Top);
            graphics.DrawPolygon(ganttDataGoingCompleteProgressPen, points);
            graphics.FillPolygon(ganttDataGoingCompleteProgressSolidBrush, points);
            //draw center rectangle
            graphics.DrawRectangle(ganttDataGoingCompleteProgressPen, e.Rect.Left, e.Rect.Top, e.Rect.Width, dataGridHeight / 4);
            graphics.FillRectangle(ganttDataGoingCompleteProgressSolidBrush, e.Rect.Left, e.Rect.Top, e.Rect.Width, dataGridHeight / 4);
            //draw right triangle
            points = new Point[3];
            points[0] = new Point(e.Rect.Right, e.Rect.Top);
            points[1] = new Point(e.Rect.Right, e.Rect.Top + dataGridHeight / 2);
            points[2] = new Point(e.Rect.Right - dataGridHeight / 2, e.Rect.Top);
            graphics.DrawPolygon(ganttDataGoingCompleteProgressPen, points);
            graphics.FillPolygon(ganttDataGoingCompleteProgressSolidBrush, points);
        }
        private void CalculateGanttRectanglePoint(DataEntry entry)
        {
            xStart = leftLabelWidth;
            DateTime localStartDate = entry.StartTime.Value.Date;
            DateTime localEndDate = entry.EndTime.Value.Date;

            int offsetStartTimeDays = (int)(localStartDate - startTime.Date).TotalDays;
            int offsetStartEndDays = (int)(localEndDate.AddDays(1) - localStartDate.Date).TotalDays;

            int startOffsetMinutes = (int)(entry.StartTime.Value - localStartDate).TotalMinutes;
            int startOffsetMinutesWidth = GetMinuteWidth(startOffsetMinutes);
            int endOffsetMinutes = (int)(localEndDate.AddDays(1) - entry.EndTime.Value).TotalMinutes;
            int endOffsetMinutesWidth = GetMinuteWidth(endOffsetMinutes);

            xStart = xStart + offsetStartTimeDays * (dataGridWidth + dataGridLineWidth) + startOffsetMinutesWidth;
            yStart = yStartOffset + (dataGridHeight + dataGridLineWidth) * (entry.RowID - 1) + marginGanttData;

            xEnd = xStart + offsetStartEndDays * (dataGridWidth + dataGridLineWidth) - endOffsetMinutesWidth - startOffsetMinutesWidth;
            yEnd = yStart + ganttDataHeight;

            entry.Rect = new Rectangle(xStart, yStart, xEnd - xStart, ganttDataHeight);
        }
        /// <summary>
        /// calculate minutes width
        /// </summary>
        private int GetMinuteWidth(int minutes)
        {
            return (minutes * dataGridWidth) / (24 * 60);
        }
        private void DrawGanttRectangle(DataEntry entry)
        {
            switch (entry.Status.Value)
            {
                case EnumStatus.Going:
                    DrawGanttRectangleOfGoing(entry);
                    break;
                case EnumStatus.End:
                    DrawGanttRectangleOfEnd(entry);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// draw gantt rectangle of DataEntry.Status=EnumStatus.Going
        /// </summary>
        private void DrawGanttRectangleOfGoing(DataEntry entry)
        {
            if (entry.Progress.Value < 100)
            {
                int tempXEnd = entry.Rect.Left + (entry.Rect.Width) * entry.Progress.Value / 100;
                if (tempXEnd < entry.Rect.Left + pieHeight / 2)
                {
                    tempXEnd = entry.Rect.Left + pieHeight / 2;
                }
                if (tempXEnd > (entry.Rect.Right - pieHeight / 2))
                {
                    tempXEnd = entry.Rect.Right - pieHeight / 2;
                }
                //need split two part
                //1.draw complated progress rectangle
                DrawArcRectangle(new Rectangle(entry.Rect.Left, entry.Rect.Top, (tempXEnd - entry.Rect.Left), ganttDataHeight), ganttDataGoingCompleteProgressSolidBrush, ganttDataGoingCompleteProgressPen, true, true, false, false);
                //2.draw uncomplate progress rectangle
                DrawArcRectangle(new Rectangle(tempXEnd, entry.Rect.Top, (entry.Rect.Right - tempXEnd), ganttDataHeight), ganttDataGoingUnCompleteProgressSolidBrush, ganttDataGoingUnCompleteProgressPen, false, false, true, true);
            }
            else
            {
                //only complate progress rectangle
                DrawArcRectangle(new Rectangle(entry.Rect.Left, entry.Rect.Top, entry.Rect.Width, ganttDataHeight), ganttDataGoingUnCompleteProgressSolidBrush, ganttDataGoingUnCompleteProgressPen, true, true, true, true);
            }
        }
        /// <summary>
        /// draw gantt rectangle of DataEntry.Status=EnumStatus.End 
        /// </summary>
        private void DrawGanttRectangleOfEnd(DataEntry entry)
        {
            if (entry.Progress.Value < 100)
            {
                int tempXEnd = entry.Rect.Left + entry.Rect.Width * entry.Progress.Value / 100;

                if (tempXEnd < entry.Rect.Left + pieHeight / 2)
                {
                    tempXEnd = entry.Rect.Left + pieHeight / 2;
                }
                if (tempXEnd > (entry.Rect.Right - pieHeight / 2))
                {
                    tempXEnd = entry.Rect.Right - pieHeight / 2;
                }
                //need split two part
                //1.draw complated progress rectangle
                DrawArcRectangle(new Rectangle(entry.Rect.Left, entry.Rect.Top, tempXEnd - entry.Rect.Left, ganttDataHeight), ganttDataEndCompleteProgressSolidBrush, ganttDataEndCompleteProgressPen, true, true, false, false);
                //only complate progress rectangle
                DrawArcRectangle(new Rectangle(tempXEnd, entry.Rect.Top, entry.Rect.Right - tempXEnd, ganttDataHeight), ganttDataEndUnCompleteProgressSolidBrush, ganttDataEndUnCompleteProgressPen, false, false, true, true);
            }
            else
            {
                //only complate progress rectangle
                DrawArcRectangle(new Rectangle(entry.Rect.Left, entry.Rect.Top, entry.Rect.Width, ganttDataHeight), ganttDataEndUnCompleteProgressSolidBrush, ganttDataEndUnCompleteProgressPen, true, true, true, true);
            }
        }
        /// <summary>
        /// draw task name in gantt rectangle
        /// </summary>
        private void DrawGanttTextInRectangle(DataEntry entry)
        {
            String taskName = entry.Name;
            int taskNameSpaceMaxLength = entry.Rect.Width;

            int sumLength = 0;
            StringBuilder sbName = new StringBuilder();
            SizeF sf;
            //substring
            for (int j = 0; j < taskName.Length; j++)
            {
                if (sumLength > taskNameSpaceMaxLength)
                {
                    sbName.Append("...");
                    sf = graphics.MeasureString(sbName.ToString(), ganttDataTextFont);
                    //back string
                    if (sf.Width > entry.Rect.Width)
                    {
                        sbName = sbName.Remove(sbName.Length - 3, 3);
                    }
                    break;
                }

                char c = taskName[j];
                sf = graphics.MeasureString(c.ToString(), ganttDataTextFont);
                sumLength = sumLength + (int)sf.Width;
                sbName.Append(c);
            }
            sf = graphics.MeasureString(sbName.ToString(), ganttDataTextFont);
            int tempX = entry.Rect.Left + taskNameSpaceMaxLength / 2 - (int)sf.Width / 2;
            graphics.DrawString(sbName.ToString(), ganttDataTextFont, ganttDataTextBrush, new PointF(tempX, entry.Rect.Top + 10));
        }
        /// <summary>
        /// draw task description after gantt rectangle 
        /// </summary>
        private void DrawGanttDescriptionAfterRectangle(DataEntry entry)
        {
            graphics.DrawString(entry.GanttDescription, ganttDataDescriptionFont, ganttDataDescriptionBrush, new PointF(entry.Rect.Right + 10, entry.Rect.Top + 10));
        }
        private void DrawGanttRelationLine()
        {
            for (int i = 0; i < entryList.Count; i++)
            {
                DataEntry entry = entryList[i];
                for (int j = 0; j < entry.FrontPIDs.Count; j++)
                {
                    DataEntry frontEntry = FindFront(entry.FrontPIDs[j]);
                    if (frontEntry == null)
                    {
                        continue;
                    }
                    DateTime frontEndDate = frontEntry.EndTime.Value.Date;
                    int offsetFrontEndDays = (int)(frontEndDate.AddDays(1) - startTime.Date).TotalDays;

                    // start line,after the front task,landscape line

                    int frontXStart = frontEntry.Rect.Right;
                    int frontYStart = frontEntry.Rect.Top + ganttDataHeight / 2;

                    int frontXEnd = frontXStart + relationLineMarginRight;
                    int frontYEnd = frontYStart;
                    graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));

                    //second line ,vertical line
                    if (frontEntry.RowID > entry.RowID)
                    {
                        //front task after the local task
                        frontXStart = frontXEnd;
                        frontYStart = frontYEnd;
                        frontXEnd = frontXStart;
                        frontYEnd = frontYStart - ganttDataHeight / 2 - marginGanttData;
                        graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));
                    }
                    else
                    {
                        //front task before the local task,
                        frontXStart = frontXEnd;
                        frontYStart = frontYEnd;
                        frontXEnd = frontXStart;
                        frontYEnd = frontYStart + (entry.RowID - frontEntry.RowID) * (dataGridHeight + dataGridLineWidth);

                        if (frontXEnd > entry.Rect.Left - relationLineMarginLeft)
                        {
                            frontYEnd = frontYEnd - dataGridHeight / 2;
                        }
                        graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));

                    }

                    //third line,to the local task rectangle's front,landscape line
                    frontXStart = frontXEnd;
                    frontYStart = frontYEnd;
                    frontXEnd = entry.Rect.Left - relationLineMarginLeft;
                    frontYEnd = frontYStart;
                    graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));

                    //fourth line,to local task rectangle center,vertical line
                    frontXStart = frontXEnd;
                    frontYStart = frontYEnd;
                    frontXEnd = frontXStart;
                    frontYEnd = entry.Rect.Top + ganttDataHeight / 2;
                    graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));

                    //fifth line,to local task rectangle border,landscape line
                    frontXStart = frontXEnd;
                    frontYStart = frontYEnd;
                    frontXEnd = frontXStart + relationLineMarginLeft;
                    frontYEnd = frontYStart;
                    graphics.DrawLine(relationLinePen, new Point(frontXStart, frontYStart), new Point(frontXEnd, frontYEnd));

                    //draw end point's triangle
                    Point firstPoint = new Point(frontXEnd - 8, frontYEnd - 8);
                    Point secondPoint = new Point(frontXEnd - 8, frontYEnd + 8);
                    Point thirdPoint = new Point(frontXEnd, frontYEnd);

                    graphics.FillPolygon(relationLineBrush, new Point[] { firstPoint, secondPoint, thirdPoint });
                }
            }
        }
        /// <summary>
        /// if task name length greater name space length, sub task name,and end with '...'
        /// </summary>
        private String GetSubTaskName(DataEntry entry, int nameSpaceLength)
        {
            String name = entry.Name;
            SizeF sf = graphics.MeasureString(name, dataLabelFont);
            if (sf.Width < nameSpaceLength)
            {
                return name;
            }
            while (true)
            {
                if (name.Length == 0)
                {
                    break;
                }
                name = name.Remove(name.Length - 1);
                sf = graphics.MeasureString(name, dataLabelFont);
                if (sf.Width > nameSpaceLength)
                {
                    continue;
                }
                if (name.Length > 3)
                {
                    name = name.Substring(0, name.Length - 3);
                    name = name + "...";
                }
                break;
            }
            return name;
        }
        /// <summary>
        /// get day label string in date column 
        /// </summary>
        private String GetDayString(DateTime dayTime)
        {
            return language.GetDisplayDayString(dayTime);
        }
        /// <summary>
        /// find the starttime and endtime from the gantt's datas
        /// </summary>
        private void FindStartEnd()
        {
            DataEntry firstEntry = null;
            int startIndex = 0;
            for (int i = 0; i < entryList.Count; i++)
            {
                if (entryList[i].DataType != EnumDataType.Task)
                {
                    continue;
                }
                firstEntry = entryList[i];
                startIndex = i;
                break;
            }
            if (firstEntry == null)
            {
                return;
            }
            startTime = firstEntry.StartTime.Value;
            endTime = firstEntry.EndTime.Value;
            for (int i = startIndex; i < entryList.Count; i++)
            {
                DataEntry entry = entryList[i];
                if (entry.DataType != EnumDataType.Task)
                {
                    continue;
                }
                if (startTime > entry.StartTime.Value)
                {
                    startTime = entry.StartTime.Value;
                }
                if (endTime < entry.EndTime.Value)
                {
                    endTime = entry.EndTime.Value;
                }
            }
            startTime = startTime.Date.AddDays(-1);
            endTime = endTime.Date.AddDays(5);
        }
        /// <summary>
        /// find starttime and endtime from stage
        /// </summary>
        private StartEndPair FindStartEndOfStage(DataEntry entry)
        {
            DateTime now = DateTime.Now.Date;
            StartEndPair stageStartEndPair = new StartEndPair(now, now);
            if (!entry.IsExpand|| entry.Subs.Count == 0)
            {
                return stageStartEndPair;
            }

            foreach (DataEntry e in entry.Subs)
            {
                StartEndPair stageItemStartEndPair = new StartEndPair(now, now);
                FindStartEndOfStageItem(e, ref stageItemStartEndPair);
                e.StartTime = stageItemStartEndPair.StartTime;
                e.EndTime = stageItemStartEndPair.EndTime;
                if (stageStartEndPair.StartTime == stageStartEndPair.EndTime)
                {
                    stageStartEndPair.StartTime = stageItemStartEndPair.StartTime;
                    stageStartEndPair.EndTime = stageItemStartEndPair.EndTime;
                }
                if (stageStartEndPair.StartTime > stageItemStartEndPair.StartTime)
                {
                    stageStartEndPair.StartTime = stageItemStartEndPair.StartTime;
                }
                if (stageStartEndPair.EndTime < stageItemStartEndPair.EndTime)
                {
                    stageStartEndPair.EndTime = stageItemStartEndPair.EndTime;
                }
            }
            entry.StartTime = stageStartEndPair.StartTime;
            entry.EndTime = stageStartEndPair.EndTime;
            return stageStartEndPair;
        }
        /// <summary>
        /// find starttime and endtime from stageitem,recursion all subs
        /// </summary>
        private void FindStartEndOfStageItem(DataEntry entry, ref StartEndPair stageItemStartEndPair)
        {
            if (!entry.IsExpand)
            {
                return;
            }
            foreach (DataEntry item in entry.Subs)
            {
                if (stageItemStartEndPair.StartTime == stageItemStartEndPair.EndTime)
                {
                    stageItemStartEndPair.StartTime = item.StartTime.Value;
                    stageItemStartEndPair.EndTime = item.EndTime.Value;
                }
                if (stageItemStartEndPair.StartTime > item.StartTime.Value)
                {
                    stageItemStartEndPair.StartTime = item.StartTime.Value;
                }
                if (stageItemStartEndPair.EndTime < item.EndTime.Value)
                {
                    stageItemStartEndPair.EndTime = item.EndTime.Value;
                }
                if (item.IsExpand)
                {
                    for (int i = 0; i < item.Subs.Count; i++)
                    {
                        FindStartEndOfStageItem(item.Subs[i], ref stageItemStartEndPair);
                    }
                }
            }
        }
        /// <summary>
        /// 1.calculate left message block width
        /// 2.calculate y start point
        /// 3.calculate gantt rectangle height
        /// 4.calculate bitmap width and height
        /// if the bitmap's width and height use memory greater than limit memory,shrink the data grid width,till to under the limit memory 
        /// </summary>
        private void CalculateWidthHeght()
        {
            leftLabelWidth = numberWidth + nameWidth + adminNameWidth + statusWidth + startTimeWidth + endTimeWidth + progressWidth + mainGridLineWidth * 7;

            yStartOffset = marginTop + dateTotalHeight;

            ganttDataHeight = dataGridHeight - marginGanttData * 2;

            int days = (int)(endTime.Date - startTime.Date).TotalDays;
            screenWidth = leftLabelWidth + dataGridWidth * days + (days - 1) * dataGridLineWidth;
            screenHeight = marginBottom + //
                marginTop + //
                mainGridLineWidth * 4 + //
                topDateHeight * 3 + //
                dataGridHeight * entryList.Count + //
                dataGridLineWidth * (entryList.Count - 1) + //
                mainGridLineWidth;

            long totolCache = screenHeight * screenWidth * 4L;
            //收缩数据格宽度,以保证每个甘特图使用内存在2G以内
            while (totolCache > maxCache)
            {
                dataGridWidth = dataGridWidth - 1;
                screenWidth = leftLabelWidth + dataGridWidth * days + (days - 1) * dataGridLineWidth;
                screenHeight = marginBottom + //
                    marginTop + //
                    mainGridLineWidth * 4 + //
                    topDateHeight * 3 + //
                    dataGridHeight * entryList.Count + //
                    dataGridLineWidth * (entryList.Count - 1) + //
                    mainGridLineWidth;
                totolCache = screenHeight * screenWidth * 4L;
            }
        }
        /// <summary>
        /// convert gantt data tree to list,if local DataEntry.IsExpand==false,and do not add
        /// </summary>
        /// <param name="entries"></param>
        private void AddToList(List<DataEntry> entries)
        {
            foreach (DataEntry entry in entries)
            {
                if (entry.Parent == null)
                {
                    entryList.Add(entry);
                }
                else
                {
                    if (entry.Parent.IsExpand)
                    {
                        entryList.Add(entry);
                    }
                    else
                    {
                        return;
                    }
                }
                AddToList(entry.Subs);
            }
        }
        /// <summary>
        /// save to image
        /// </summary>
        public void SaveImage(String path, System.Drawing.Imaging.ImageFormat format)
        {
            if (File.Exists(path))
            {
                throw new Exception("the path file has exist");
            }
            bitmap.Save(path, format);
            Dispose();
        }
        /// <summary>
        /// dispose all resources
        /// </summary>
        private void Dispose()
        {
            mainGridPen.Dispose();
            dataGridLinePen.Dispose();
            relationLinePen.Dispose();

            titleFont.Dispose();
            titleBrush.Dispose();

            descriptionFont.Dispose();
            descriptionBrush.Dispose();

            leftLabelTitleFont.Dispose();
            leftLabelTitleBrush.Dispose();

            topDateTitleFont.Dispose();
            topDateTitleBrush.Dispose();

            dataLabelFont.Dispose();
            dataLabelBrush.Dispose();

            backgroupBrush.Dispose();

            ganttDataDescriptionFont.Dispose();
            ganttDataDescriptionBrush.Dispose();

            ganttDataTextFont.Dispose();
            ganttDataTextBrush.Dispose();

            ganttDataEndCompleteProgressSolidBrush.Dispose();
            ganttDataEndUnCompleteProgressSolidBrush.Dispose();
            ganttDataGoingCompleteProgressSolidBrush.Dispose();
            ganttDataGoingUnCompleteProgressSolidBrush.Dispose();

            ganttDataEndCompleteProgressPen.Dispose();
            ganttDataEndUnCompleteProgressPen.Dispose();

            ganttDataGoingCompleteProgressPen.Dispose();
            ganttDataGoingUnCompleteProgressPen.Dispose();

            relationLineBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();
        }
        /// <summary>
        /// draw rounded rectangle,need define round point
        /// <param name="rc">rectangle</param>
        /// <param name="isLeftTopArc">define is or not draw leftTopArc,true-yes,false-no</param>
        /// <param name="isLeftBottomArc">define is or not draw leftBottomArc,true-yes,false-no</param>
        /// <param name="isRightTopArc">define is or not draw RightTopArc,true-yes,false-no</param>
        /// <param name="isRightBottomArc">define is or not draw rightBottompArc,true-yes,false-no</param>
        /// </summary>
        private void DrawArcRectangle(Rectangle rc, Brush b, Pen p, bool isLeftTopArc, bool isLeftBottomArc, bool isRightTopArc, bool isRightBottomArc)
        {
            if (isLeftTopArc)
            {
                //draw leftTopArc
                graphics.DrawPie(p, rc.Left, rc.Top, pieHeight, pieHeight, 180, 90);
                //fill leftTopArc
                graphics.FillPie(b, rc.Left, rc.Top, pieHeight, pieHeight, 180, 90);
            }
            if (isLeftBottomArc)
            {
                //draw leftBottomArc
                graphics.DrawPie(p, rc.Left, rc.Bottom - pieHeight, pieHeight, pieHeight, 180, -90);
                //fill leftBottomArc
                graphics.FillPie(b, rc.Left, rc.Bottom - pieHeight, pieHeight, pieHeight, 180, -90);
            }
            if (isRightTopArc)
            {
                //draw rightTopArc
                graphics.DrawPie(p, rc.Right - pieHeight, rc.Top, pieHeight, pieHeight, 0, -90);
                //fill rightTopArc
                graphics.FillPie(b, rc.Right - pieHeight, rc.Top, pieHeight, pieHeight, 0, -90);
            }
            if (isRightBottomArc)
            {
                //draw rightBottomArc
                graphics.DrawPie(p, rc.Right - pieHeight, rc.Bottom - pieHeight, pieHeight, pieHeight, 0, 90);
                //fill rightBottomArc
                graphics.FillPie(b, rc.Right - pieHeight, rc.Bottom - pieHeight, pieHeight, pieHeight, 0, 90);
            }

            //draw rectangle don't contain left and right Arc
            int x = rc.Left + ((isLeftTopArc || isLeftBottomArc) ? pieHeight / 2 : 0);
            int y = rc.Top;
            int width = rc.Right - rc.Left - ((isLeftTopArc || isLeftBottomArc) ? pieHeight / 2 : 0) - ((isRightTopArc || isRightBottomArc) ? pieHeight / 2 : 0);
            int height = rc.Bottom - rc.Top;
            graphics.DrawRectangle(p, x, y, width, height);
            graphics.FillRectangle(b, x, y, width, height);

            //draw rectangle don't contain top and bottom Arc
            x = rc.Left;
            y = rc.Top + ((isLeftTopArc || isRightTopArc) ? pieHeight / 2 : 0);
            width = rc.Right - rc.Left;
            height = rc.Bottom - rc.Top - ((isLeftTopArc || isRightTopArc) ? pieHeight / 2 : 0) - ((isLeftBottomArc || isRightBottomArc) ? pieHeight / 2 : 0);

            graphics.DrawRectangle(p, x, y, width, height);
            graphics.FillRectangle(b, x, y, width, height);

        }
        /// <summary>
        /// find the front task,if front task not in display,return null
        /// </summary>
        private DataEntry FindFront(Guid frontID)
        {
            if (frontID == Guid.Empty)
            {
                return null;
            }
            for (int i = 0; i < entryList.Count; i++)
            {
                if (entryList[i].PID.Equals(frontID))
                {
                    return entryList[i];
                }
            }
            return null;
        }
        public void SetBackgroundColor(int backgroundColor)
        {
            this.backgroundColor = backgroundColor;
        }
        public void SetTopDateHeight(int topDateHeight)
        {
            this.topDateHeight = topDateHeight;
        }
        public void SetDataGridHeight(int dataGridHeight)
        {
            this.dataGridHeight = dataGridHeight;
        }
        private void SetDataGridWidth(int dataGridWidth)
        {
            this.dataGridWidth = dataGridWidth;
        }
        public void SetNumberWidth(int numberWidth)
        {
            this.numberWidth = numberWidth;
        }
        public void SetNameWidth(int nameWidth)
        {
            this.nameWidth = nameWidth;
        }
        public void SetAdminNameWidth(int adminNameWidth)
        {
            this.adminNameWidth = adminNameWidth;
        }
        public void SetStatusWidth(int statusWidth)
        {
            this.statusWidth = statusWidth;
        }
        public void SetStartTimeWidth(int startTimeWidth)
        {
            this.startTimeWidth = startTimeWidth;
        }
        public void SetEndTimeWidth(int endTimeWidth)
        {
            this.endTimeWidth = endTimeWidth;
        }
        public void SetProgressWidth(int progressWidth)
        {
            this.progressWidth = progressWidth;
        }
        public void SetMarginTop(int marginTop)
        {
            this.marginTop = marginTop;
        }
        public void SetMarginBottom(int marginBottom)
        {
            this.marginBottom = marginBottom;
        }
        public void SetLeftLabelTitleColor(int leftLabelTitleColor)
        {
            this.leftLabelTitleColor = leftLabelTitleColor;
        }
        public void SetLeftLabelTitleSize(int leftLabelTitleSize)
        {
            this.leftLabelTitleSize = leftLabelTitleSize;
        }
        public void SetTopDateTitleColor(int topDateTitleColor)
        {
            this.topDateTitleColor = topDateTitleColor;
        }
        public void SetTopDateTitleSize(int topDateTitleSize)
        {
            this.topDateTitleSize = topDateTitleSize;
        }
        public void SetMainGridColor(int mainGridColor)
        {
            this.mainGridColor = mainGridColor;
        }
        public void SetMainGridLineWidth(int mainGridLineWidth)
        {
            this.mainGridLineWidth = mainGridLineWidth;
        }
        public void SetDataLabelColor(int dataLabelColor)
        {
            this.dataLabelColor = dataLabelColor;
        }
        public void SetDataLabelSize(int dataLabelSize)
        {
            this.dataLabelSize = dataLabelSize;
        }
        public void SetDataGridLineColor(int dataGridLineColor)
        {
            this.dataGridLineColor = dataGridLineColor;
        }
        public void SetDataGridLineWidth(int dataGridLineWidth)
        {
            this.dataGridLineWidth = dataGridLineWidth;
        }
        public void SetGanttDataTextColor(int ganttDataTextColor)
        {
            this.ganttDataTextColor = ganttDataTextColor;
        }
        public void SetganttDataTextSize(int ganttDataTextSize)
        {
            this.ganttDataTextSize = ganttDataTextSize;
        }
        public void SetGanttDataDescriptionColor(int ganttDataDescriptionColor)
        {
            this.ganttDataDescriptionColor = ganttDataDescriptionColor;
        }
        public void SetGanttDataDescriptionSize(int ganttDataDescriptionSize)
        {
            this.ganttDataDescriptionSize = ganttDataDescriptionSize;
        }
        public void SetGanttDataGoingCompleteProgressColor(int ganttDataGoingCompleteProgressColor)
        {
            this.ganttDataGoingCompleteProgressColor = ganttDataGoingCompleteProgressColor;
        }
        public void SetGanttDataGoingUnCompleteProgressColor(int ganttDataGoingUnCompleteProgressColor)
        {
            this.ganttDataGoingUnCompleteProgressColor = ganttDataGoingUnCompleteProgressColor;
        }
        public void SetGanttDataEndCompleteProgressColor(int ganttDataEndCompleteProgressColor)
        {
            this.ganttDataEndCompleteProgressColor = ganttDataEndCompleteProgressColor;
        }
        public void SetGanttDataEndUnCompleteProgressColor(int ganttDataEndUnCompleteProgressColor)
        {
            this.ganttDataEndUnCompleteProgressColor = ganttDataEndUnCompleteProgressColor;
        }
        public void SetRelationLineColor(int relationLineColor)
        {
            this.relationLineColor = relationLineColor;
        }
        public void SetRelationLineWidth(int relationLineWidth)
        {
            this.relationLineWidth = relationLineWidth;
        }
        public void SetRelationLineMarginLeft(int relationLineMarginLeft)
        {
            this.relationLineMarginLeft = relationLineMarginLeft;
        }
        public void SetRelationLineMarginRight(int relationLineMarginRight)
        {
            this.relationLineMarginRight = relationLineMarginRight;
        }
        public void SetMarginGanttData(int marginGanttData)
        {
            this.marginGanttData = marginGanttData;
        }
        public void SetDataLabelMarginLeft(int dataLabelMarginLeft)
        {
            this.dataLabelMarginLeft = dataLabelMarginLeft;
        }
        public void SetIconWidth(int iconWidth)
        {
            this.iconWidth = iconWidth;
        }
        public void SetIconMargin(int iconMargin)
        {
            this.iconMargin = iconMargin;
        }
        public void SetPieHeight(int pieHeight)
        {
            this.pieHeight = pieHeight;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public void SetTitleColor(int titleColor)
        {
            this.titleColor = titleColor;
        }
        public void SetTitleSize(int titleSize)
        {
            this.titleSize = titleSize;
        }
        public void SetDateFormat(string dateFormat)
        {
            this.dateFormat = dateFormat;
        }
        public void SetDescription(string description)
        {
            this.description = description;
        }
        public void SetDescriptionColor(int descriptionColor)
        {
            this.descriptionColor = descriptionColor;
        }
        public void SetDescriptionSize(int descriptionSize)
        {
            this.descriptionSize = descriptionSize;
        }
        public void SetDateType(EnumDateType dateType)
        {
            this.dateType = dateType;
        }
        /// <summary>
        /// set the local language,at present only support en and cn
        /// if the code unsupport,will throw an exception
        /// </summary>
        public void SetLocalCode(String localCode)
        {
            this.language = LocalLanguageFactory.CreateInstance(localCode);
        }
    }
}
