using DrawGantt.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DrawGantt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //AutoCreateTestData();
            //return;
            List<DataEntry> entries = GenerateTestData();

            GanttBitmap gantt = new GanttBitmap();

            #region 设置各种值
            gantt.SetAdminNameWidth(200);
            gantt.SetBackgroundColor(Color.FromArgb(252, 252, 252).ToArgb());
            gantt.SetDataGridLineColor(Color.FromArgb(210, 210, 210).ToArgb());
            gantt.SetDataGridLineWidth(1);
            gantt.SetDataGridHeight(50);
            gantt.SetDataLabelColor(Color.FromArgb(20, 20, 20).ToArgb());
            gantt.SetDataLabelMarginLeft(20);
            gantt.SetDataLabelSize(16);
            //gantt.SetDataGridWidth(120);
            gantt.SetDateFormat("yyyy/MM/dd HH:mm");
            gantt.SetDescription("QQ：343778189   Email：chensuqian@163.com");
            gantt.SetDescriptionColor(Color.FromArgb(27, 47, 77).ToArgb());
            gantt.SetDescriptionSize(18);
            gantt.SetEndTimeWidth(240);
            gantt.SetGanttDataDescriptionColor(Color.FromArgb(27, 47, 77).ToArgb());
            gantt.SetGanttDataDescriptionSize(16);
            gantt.SetGanttDataEndCompleteProgressColor(Color.FromArgb(131, 136, 151).ToArgb());
            gantt.SetGanttDataEndUnCompleteProgressColor(Color.FromArgb(148, 149, 180).ToArgb());
            gantt.SetGanttDataGoingCompleteProgressColor(Color.FromArgb(42, 172, 251).ToArgb());
            gantt.SetGanttDataGoingUnCompleteProgressColor(Color.FromArgb(133, 207, 254).ToArgb());
            gantt.SetGanttDataTextColor(Color.FromArgb(255, 255, 255).ToArgb());
            gantt.SetganttDataTextSize(16);
            gantt.SetIconMargin(6);
            gantt.SetIconWidth(24);
            gantt.SetLeftLabelTitleColor(Color.FromArgb(75, 75, 75).ToArgb());
            gantt.SetLeftLabelTitleSize(20);
            gantt.SetMainGridColor(Color.FromArgb(125, 125, 125).ToArgb());
            gantt.SetMainGridLineWidth(1);
            gantt.SetMarginBottom(60);
            gantt.SetMarginGanttData(7);
            gantt.SetMarginTop(100);
            gantt.SetNameWidth(500);
            gantt.SetNumberWidth(80);
            gantt.SetPieHeight(6);
            gantt.SetProgressWidth(120);
            gantt.SetRelationLineColor(Color.FromArgb(223, 142, 75).ToArgb());
            gantt.SetRelationLineMarginLeft(20);
            gantt.SetRelationLineMarginRight(10);
            gantt.SetRelationLineWidth(3);
            gantt.SetStartTimeWidth(240);
            gantt.SetStatusWidth(100);
            gantt.SetTitle("Project Name");
            gantt.SetTitleColor(Color.FromArgb(41, 41, 41).ToArgb());
            gantt.SetTitleSize(36);
            gantt.SetTopDateTitleColor(Color.FromArgb(110, 110, 110).ToArgb());
            gantt.SetTopDateTitleSize(16);
            gantt.SetTopDateHeight(30);
            gantt.SetDateType(EnumDateType.Day);
            gantt.SetLocalCode("en");
            #endregion

            gantt.SetData(entries);
            String ganttPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"gantt.jpg");
            if (File.Exists(ganttPath))
            {
                File.Delete(ganttPath);
            }
            gantt.SaveImage(ganttPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //ganttPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gantt.png");
            //gantt.SaveImage(ganttPath, System.Drawing.Imaging.ImageFormat.Png);

        }
        
        public static void AutoCreateTestData()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            int stageCount = 3;
            int stageItemCount = 3;
            int masterTaskCount = 6;
            int firstSubTaskCount = 3;
            int secondSubTaskCount = 3;
            DateTime start = Convert.ToDateTime("2018/01/01 08:00:00");
            DateTime end = Convert.ToDateTime("2018/01/03 18:00:00");
            
            WriteTestData("List<DataEntry> entries = new List<DataEntry>();");
            Random r = new Random();
          
            for (int a = 1; a <= stageCount; a++)
            {
                WriteTestData("DataEntry stage" + a + " = new DataEntry()");
                WriteTestData("{");
                if (a == 2)
                {
                    WriteTestData("IsExpand = false,");
                }
                else
                {
                    WriteTestData("IsExpand = true,");
                }
                WriteTestData(" Level=1,");
                WriteTestData(" DataType = EnumDataType.Stage,");
                WriteTestData(" PID = Guid.NewGuid(),");
                WriteTestData(" Name = \"Stage" + a + "\",");
                WriteTestData("};");
                WriteTestData("entries.Add(stage" + a + ");");
            
                for (int b = 1; b <= stageItemCount; b++)
                {
                    WriteTestData("DataEntry stageItem" + a + b + " = new DataEntry()");
                    WriteTestData("{");
                    if (b == 1)
                    {
                        WriteTestData("IsExpand = false,");
                    }
                    else
                    {
                        WriteTestData("IsExpand = true,");
                    }
                    WriteTestData(" Level = 2,");
                    WriteTestData("DataType = EnumDataType.StageItem,");
                    WriteTestData("PID = Guid.NewGuid(),");
                    WriteTestData("Name = \"StageItem" + b + "\",");
                    WriteTestData("Parent = stage" + a + "");
                    WriteTestData("};");
                    WriteTestData("stage" + a + ".Subs.Add(stageItem" + a + b + ");");
                 
                    for (int c = 1; c <= masterTaskCount; c++)
                    {
                        WriteTestData("DataEntry masterTask" + a + b + c + " = new DataEntry()");
                        WriteTestData(" {");
                        if (c == 3 || c == 5)
                        {
                            WriteTestData(" IsExpand = false,");
                        }
                        else
                        {
                            WriteTestData(" IsExpand = true,");
                        }
                        WriteTestData(" Level = 3,");
                        WriteTestData("  DataType = EnumDataType.Task,");
                        WriteTestData(" PID = Guid.NewGuid(),");
                        WriteTestData("Name = \"MasterTask" + c + "\",");
                        WriteTestData(" AdminName = \"XXXXXXXX\",");
                        WriteTestData(" Progress = " + (r.Next(101)) + ",");
                        if ((a + b + c) % 3 == 0)
                        {
                            WriteTestData(" Status = EnumStatus.End,");
                        }
                        else
                        {
                            WriteTestData(" Status = EnumStatus.Going,");
                        }
                        WriteTestData("StartTime = Convert.ToDateTime(\"" + start.ToString() + "\"),");
                        WriteTestData("EndTime = Convert.ToDateTime(\"" + end.ToString() + "\"),");
                        WriteTestData("Parent = stageItem" + a + b + "");
                        WriteTestData("};");
                        WriteTestData("stageItem" + a + b + ".Subs.Add(masterTask" + a + b + c + ");");

                        if (c == 6)
                        {
                            WriteTestData("List<Guid> frontIDs" + a + b + "4 = new List<Guid>();");
                            WriteTestData("frontIDs" + a + b + "4.Add(masterTask" + a + b + "1.PID);");
                            WriteTestData("frontIDs" + a + b + "4.Add(masterTask" + a + b + "2.PID);");
                            WriteTestData("frontIDs" + a + b + "4.Add(masterTask" + a + b + "5.PID);");
                            WriteTestData("frontIDs" + a + b + "4.Add(masterTask" + a + b + "6.PID);");
                            WriteTestData("masterTask" + a + b + "4.FrontPIDs = frontIDs" + a + b + "4;");
                        }
                        start = AddStart(start);
                        end = AddEnd(end);
                    
                        //for (int d = 1; d <= firstSubTaskCount; d++)
                        //{
                        //    WriteTestData("DataEntry subTask" + a + b + c + d + " = new DataEntry()");
                        //    WriteTestData(" {");
                        //    if (d == 1)
                        //    {
                        //        WriteTestData(" IsExpand = false,");
                        //    }
                        //    else
                        //    {
                        //        WriteTestData(" IsExpand = true,");
                        //    }
                        //    WriteTestData(" Level = 4,");
                        //    WriteTestData("  DataType = EnumDataType.Task,");
                        //    WriteTestData(" PID = Guid.NewGuid(),");
                        //    WriteTestData("  Name = \"FirstSubs" + c + d + "\",");
                        //    WriteTestData("AdminName = \"YYYYYYYY\",");
                        //    WriteTestData(" Progress = " + (r.Next(101)) + ",");
                        //    if ((a + b + c+d) % 3 == 0)
                        //    {
                        //        WriteTestData(" Status = EnumStatus.End,");
                        //    }
                        //    else
                        //    {
                        //        WriteTestData(" Status = EnumStatus.Going,");
                        //    }
                        //    WriteTestData("  StartTime = Convert.ToDateTime(\"" + start.ToString() + "\"),");
                        //    WriteTestData(" EndTime = Convert.ToDateTime(\"" + end.ToString() + "\"),");
                        //    WriteTestData("Parent = masterTask" + a + b + c + "");
                        //    WriteTestData(" };");
                        //    WriteTestData(" masterTask" + a + b + c + ".Subs.Add(subTask" + a + b + c + d + ");");

                        //    start = AddStart(start);
                        //    end = AddEnd(end);
                          
                        //    for (int e = 1; e <= secondSubTaskCount; e++)
                        //    {
                        //        WriteTestData("DataEntry subTask" + a + b + c + d + e + " = new DataEntry()");
                        //        WriteTestData("{");
                        //        if (e == 1 || e == 3)
                        //        {
                        //            WriteTestData("IsExpand = false,");
                        //        }
                        //        else
                        //        {
                        //            WriteTestData("IsExpand = true,");
                        //        }
                        //        WriteTestData(" Level = 5,");
                        //        WriteTestData(" DataType = EnumDataType.Task,");
                        //        WriteTestData(" PID = Guid.NewGuid(),");
                        //        WriteTestData(" Name = \"SecondSubs" + c + d + e + "\",");
                        //        WriteTestData(" AdminName = \"ZZZZZZZZ\",");
                        //        WriteTestData(" Progress = " + (r.Next(101)) + ",");
                        //        if ((a + b + c + d+e) % 3 == 0)
                        //        {
                        //            WriteTestData(" Status = EnumStatus.End,");
                        //        }
                        //        else
                        //        {
                        //            WriteTestData(" Status = EnumStatus.Going,");
                        //        }
                        //        WriteTestData("StartTime = Convert.ToDateTime(\"" + start.ToString() + "\"),");
                        //        WriteTestData("EndTime = Convert.ToDateTime(\"" + end.ToString() + "\"),");
                        //        WriteTestData("Parent = subTask" + a + b + c + d + "");
                        //        WriteTestData("};");
                        //        WriteTestData("subTask" + a + b + c + d + ".Subs.Add(subTask" + a + b + c + d + e + ");");

                        //        start = AddStart(start);
                        //        end = AddEnd(end);
                        //    }
                        //}
                    }
                }
            }
            WriteTestData("return entries;");
        }
        private static DateTime AddStart(DateTime start)
        {
            Random r = new Random();
            return start.AddHours(r.Next(10,24));
            }
        private static DateTime AddEnd(DateTime end)
        {
            Random r = new Random();
            return end.AddHours(r.Next(10,24));
        }
        private static void WriteTestData(string line)
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata.txt");
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(line);
            }
        }
        /// <summary>
        /// copy content of the AutoCreateTestData file in this function
        /// </summary>
        /// <returns></returns>
        public static List<DataEntry> GenerateTestData()
        {
            List<DataEntry> entries = new List<DataEntry>();
            DataEntry stage1 = new DataEntry()
            {
                IsExpand = true,
                Level = 1,
                DataType = EnumDataType.Stage,
                PID = Guid.NewGuid(),
                Name = "Stage1",
            };
            entries.Add(stage1);
            DataEntry stageItem11 = new DataEntry()
            {
                IsExpand = false,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem1",
                Parent = stage1
            };
            stage1.Subs.Add(stageItem11);
            DataEntry masterTask111 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 28,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/1 8:00:00"),
                EndTime = Convert.ToDateTime("2018/1/3 18:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask111);
            DataEntry masterTask112 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 48,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/2 0:00:00"),
                EndTime = Convert.ToDateTime("2018/1/4 10:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask112);
            DataEntry masterTask113 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 67,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/2 16:00:00"),
                EndTime = Convert.ToDateTime("2018/1/5 2:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask113);
            DataEntry masterTask114 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 38,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/3 13:00:00"),
                EndTime = Convert.ToDateTime("2018/1/5 23:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask114);
            DataEntry masterTask115 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 46,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/4 8:00:00"),
                EndTime = Convert.ToDateTime("2018/1/6 18:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask115);
            DataEntry masterTask116 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 21,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/4 18:00:00"),
                EndTime = Convert.ToDateTime("2018/1/7 4:00:00"),
                Parent = stageItem11
            };
            stageItem11.Subs.Add(masterTask116);
            List<Guid> frontIDs114 = new List<Guid>();
            frontIDs114.Add(masterTask111.PID);
            frontIDs114.Add(masterTask112.PID);
            frontIDs114.Add(masterTask115.PID);
            frontIDs114.Add(masterTask116.PID);
            masterTask114.FrontPIDs = frontIDs114;
            DataEntry stageItem12 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem2",
                Parent = stage1
            };
            stage1.Subs.Add(stageItem12);
            DataEntry masterTask121 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 46,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/5 9:00:00"),
                EndTime = Convert.ToDateTime("2018/1/7 19:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask121);
            DataEntry masterTask122 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 39,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/6 3:00:00"),
                EndTime = Convert.ToDateTime("2018/1/8 13:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask122);
            DataEntry masterTask123 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 59,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/6 18:00:00"),
                EndTime = Convert.ToDateTime("2018/1/9 4:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask123);
            DataEntry masterTask124 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 94,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/7 14:00:00"),
                EndTime = Convert.ToDateTime("2018/1/10 0:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask124);
            DataEntry masterTask125 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 24,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/8 10:00:00"),
                EndTime = Convert.ToDateTime("2018/1/10 20:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask125);
            DataEntry masterTask126 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 65,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/8 21:00:00"),
                EndTime = Convert.ToDateTime("2018/1/11 7:00:00"),
                Parent = stageItem12
            };
            stageItem12.Subs.Add(masterTask126);
            List<Guid> frontIDs124 = new List<Guid>();
            frontIDs124.Add(masterTask121.PID);
            frontIDs124.Add(masterTask122.PID);
            frontIDs124.Add(masterTask125.PID);
            frontIDs124.Add(masterTask126.PID);
            masterTask124.FrontPIDs = frontIDs124;
            DataEntry stageItem13 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem3",
                Parent = stage1
            };
            stage1.Subs.Add(stageItem13);
            DataEntry masterTask131 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 64,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/9 20:00:00"),
                EndTime = Convert.ToDateTime("2018/1/12 6:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask131);
            DataEntry masterTask132 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 40,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/10 10:00:00"),
                EndTime = Convert.ToDateTime("2018/1/12 20:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask132);
            DataEntry masterTask133 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 22,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/10 22:00:00"),
                EndTime = Convert.ToDateTime("2018/1/13 8:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask133);
            DataEntry masterTask134 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 71,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/11 15:00:00"),
                EndTime = Convert.ToDateTime("2018/1/14 1:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask134);
            DataEntry masterTask135 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 76,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/12 8:00:00"),
                EndTime = Convert.ToDateTime("2018/1/14 18:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask135);
            DataEntry masterTask136 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 91,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/13 6:00:00"),
                EndTime = Convert.ToDateTime("2018/1/15 16:00:00"),
                Parent = stageItem13
            };
            stageItem13.Subs.Add(masterTask136);
            List<Guid> frontIDs134 = new List<Guid>();
            frontIDs134.Add(masterTask131.PID);
            frontIDs134.Add(masterTask132.PID);
            frontIDs134.Add(masterTask135.PID);
            frontIDs134.Add(masterTask136.PID);
            masterTask134.FrontPIDs = frontIDs134;
            DataEntry stage2 = new DataEntry()
            {
                IsExpand = false,
                Level = 1,
                DataType = EnumDataType.Stage,
                PID = Guid.NewGuid(),
                Name = "Stage2",
            };
            entries.Add(stage2);
            DataEntry stageItem21 = new DataEntry()
            {
                IsExpand = false,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem1",
                Parent = stage2
            };
            stage2.Subs.Add(stageItem21);
            DataEntry masterTask211 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 20,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/14 2:00:00"),
                EndTime = Convert.ToDateTime("2018/1/16 12:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask211);
            DataEntry masterTask212 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 87,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/14 13:00:00"),
                EndTime = Convert.ToDateTime("2018/1/16 23:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask212);
            DataEntry masterTask213 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 56,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/15 11:00:00"),
                EndTime = Convert.ToDateTime("2018/1/17 21:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask213);
            DataEntry masterTask214 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 1,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/16 0:00:00"),
                EndTime = Convert.ToDateTime("2018/1/18 10:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask214);
            DataEntry masterTask215 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 2,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/16 13:00:00"),
                EndTime = Convert.ToDateTime("2018/1/18 23:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask215);
            DataEntry masterTask216 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 16,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/17 7:00:00"),
                EndTime = Convert.ToDateTime("2018/1/19 17:00:00"),
                Parent = stageItem21
            };
            stageItem21.Subs.Add(masterTask216);
            List<Guid> frontIDs214 = new List<Guid>();
            frontIDs214.Add(masterTask211.PID);
            frontIDs214.Add(masterTask212.PID);
            frontIDs214.Add(masterTask215.PID);
            frontIDs214.Add(masterTask216.PID);
            masterTask214.FrontPIDs = frontIDs214;
            DataEntry stageItem22 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem2",
                Parent = stage2
            };
            stage2.Subs.Add(stageItem22);
            DataEntry masterTask221 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 42,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/17 23:00:00"),
                EndTime = Convert.ToDateTime("2018/1/20 9:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask221);
            DataEntry masterTask222 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 55,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/18 20:00:00"),
                EndTime = Convert.ToDateTime("2018/1/21 6:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask222);
            DataEntry masterTask223 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 44,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/19 15:00:00"),
                EndTime = Convert.ToDateTime("2018/1/22 1:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask223);
            DataEntry masterTask224 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 82,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/20 1:00:00"),
                EndTime = Convert.ToDateTime("2018/1/22 11:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask224);
            DataEntry masterTask225 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 92,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/20 11:00:00"),
                EndTime = Convert.ToDateTime("2018/1/22 21:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask225);
            DataEntry masterTask226 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 30,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/21 2:00:00"),
                EndTime = Convert.ToDateTime("2018/1/23 12:00:00"),
                Parent = stageItem22
            };
            stageItem22.Subs.Add(masterTask226);
            List<Guid> frontIDs224 = new List<Guid>();
            frontIDs224.Add(masterTask221.PID);
            frontIDs224.Add(masterTask222.PID);
            frontIDs224.Add(masterTask225.PID);
            frontIDs224.Add(masterTask226.PID);
            masterTask224.FrontPIDs = frontIDs224;
            DataEntry stageItem23 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem3",
                Parent = stage2
            };
            stage2.Subs.Add(stageItem23);
            DataEntry masterTask231 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 84,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/21 15:00:00"),
                EndTime = Convert.ToDateTime("2018/1/24 1:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask231);
            DataEntry masterTask232 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 30,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/22 9:00:00"),
                EndTime = Convert.ToDateTime("2018/1/24 19:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask232);
            DataEntry masterTask233 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 37,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/23 0:00:00"),
                EndTime = Convert.ToDateTime("2018/1/25 10:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask233);
            DataEntry masterTask234 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 48,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/23 20:00:00"),
                EndTime = Convert.ToDateTime("2018/1/26 6:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask234);
            DataEntry masterTask235 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 18,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/24 7:00:00"),
                EndTime = Convert.ToDateTime("2018/1/26 17:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask235);
            DataEntry masterTask236 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 40,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/25 6:00:00"),
                EndTime = Convert.ToDateTime("2018/1/27 16:00:00"),
                Parent = stageItem23
            };
            stageItem23.Subs.Add(masterTask236);
            List<Guid> frontIDs234 = new List<Guid>();
            frontIDs234.Add(masterTask231.PID);
            frontIDs234.Add(masterTask232.PID);
            frontIDs234.Add(masterTask235.PID);
            frontIDs234.Add(masterTask236.PID);
            masterTask234.FrontPIDs = frontIDs234;
            DataEntry stage3 = new DataEntry()
            {
                IsExpand = true,
                Level = 1,
                DataType = EnumDataType.Stage,
                PID = Guid.NewGuid(),
                Name = "Stage3",
            };
            entries.Add(stage3);
            DataEntry stageItem31 = new DataEntry()
            {
                IsExpand = false,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem1",
                Parent = stage3
            };
            stage3.Subs.Add(stageItem31);
            DataEntry masterTask311 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 80,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/25 20:00:00"),
                EndTime = Convert.ToDateTime("2018/1/28 6:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask311);
            DataEntry masterTask312 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 93,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/26 13:00:00"),
                EndTime = Convert.ToDateTime("2018/1/28 23:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask312);
            DataEntry masterTask313 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 54,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/27 11:00:00"),
                EndTime = Convert.ToDateTime("2018/1/29 21:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask313);
            DataEntry masterTask314 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 75,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/28 7:00:00"),
                EndTime = Convert.ToDateTime("2018/1/30 17:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask314);
            DataEntry masterTask315 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 83,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/29 3:00:00"),
                EndTime = Convert.ToDateTime("2018/1/31 13:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask315);
            DataEntry masterTask316 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 66,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/29 14:00:00"),
                EndTime = Convert.ToDateTime("2018/2/1 0:00:00"),
                Parent = stageItem31
            };
            stageItem31.Subs.Add(masterTask316);
            List<Guid> frontIDs314 = new List<Guid>();
            frontIDs314.Add(masterTask311.PID);
            frontIDs314.Add(masterTask312.PID);
            frontIDs314.Add(masterTask315.PID);
            frontIDs314.Add(masterTask316.PID);
            masterTask314.FrontPIDs = frontIDs314;
            DataEntry stageItem32 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem2",
                Parent = stage3
            };
            stage3.Subs.Add(stageItem32);
            DataEntry masterTask321 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 65,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/1/30 3:00:00"),
                EndTime = Convert.ToDateTime("2018/2/1 13:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask321);
            DataEntry masterTask322 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 58,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/30 21:00:00"),
                EndTime = Convert.ToDateTime("2018/2/2 7:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask322);
            DataEntry masterTask323 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 67,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/1/31 13:00:00"),
                EndTime = Convert.ToDateTime("2018/2/2 23:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask323);
            DataEntry masterTask324 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 15,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/2/1 5:00:00"),
                EndTime = Convert.ToDateTime("2018/2/3 15:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask324);
            DataEntry masterTask325 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 50,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/2 2:00:00"),
                EndTime = Convert.ToDateTime("2018/2/4 12:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask325);
            DataEntry masterTask326 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 73,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/2 23:00:00"),
                EndTime = Convert.ToDateTime("2018/2/5 9:00:00"),
                Parent = stageItem32
            };
            stageItem32.Subs.Add(masterTask326);
            List<Guid> frontIDs324 = new List<Guid>();
            frontIDs324.Add(masterTask321.PID);
            frontIDs324.Add(masterTask322.PID);
            frontIDs324.Add(masterTask325.PID);
            frontIDs324.Add(masterTask326.PID);
            masterTask324.FrontPIDs = frontIDs324;
            DataEntry stageItem33 = new DataEntry()
            {
                IsExpand = true,
                Level = 2,
                DataType = EnumDataType.StageItem,
                PID = Guid.NewGuid(),
                Name = "StageItem3",
                Parent = stage3
            };
            stage3.Subs.Add(stageItem33);
            DataEntry masterTask331 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask1",
                AdminName = "XXXXXXXX",
                Progress = 77,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/3 18:00:00"),
                EndTime = Convert.ToDateTime("2018/2/6 4:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask331);
            DataEntry masterTask332 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask2",
                AdminName = "XXXXXXXX",
                Progress = 46,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/4 9:00:00"),
                EndTime = Convert.ToDateTime("2018/2/6 19:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask332);
            DataEntry masterTask333 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask3",
                AdminName = "XXXXXXXX",
                Progress = 25,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/2/5 0:00:00"),
                EndTime = Convert.ToDateTime("2018/2/7 10:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask333);
            DataEntry masterTask334 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask4",
                AdminName = "XXXXXXXX",
                Progress = 7,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/5 13:00:00"),
                EndTime = Convert.ToDateTime("2018/2/7 23:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask334);
            DataEntry masterTask335 = new DataEntry()
            {
                IsExpand = false,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask5",
                AdminName = "XXXXXXXX",
                Progress = 61,
                Status = EnumStatus.Going,
                StartTime = Convert.ToDateTime("2018/2/6 7:00:00"),
                EndTime = Convert.ToDateTime("2018/2/8 17:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask335);
            DataEntry masterTask336 = new DataEntry()
            {
                IsExpand = true,
                Level = 3,
                DataType = EnumDataType.Task,
                PID = Guid.NewGuid(),
                Name = "MasterTask6",
                AdminName = "XXXXXXXX",
                Progress = 52,
                Status = EnumStatus.End,
                StartTime = Convert.ToDateTime("2018/2/7 1:00:00"),
                EndTime = Convert.ToDateTime("2018/2/9 11:00:00"),
                Parent = stageItem33
            };
            stageItem33.Subs.Add(masterTask336);
            List<Guid> frontIDs334 = new List<Guid>();
            frontIDs334.Add(masterTask331.PID);
            frontIDs334.Add(masterTask332.PID);
            frontIDs334.Add(masterTask335.PID);
            frontIDs334.Add(masterTask336.PID);
            masterTask334.FrontPIDs = frontIDs334;
            return entries;


        }
    }
}
