using System;
using System.Drawing;
using System.Windows.Forms;

namespace Schedule
{
    public partial class ScheduleWeek : UserControl
    {
        public ScheduleWeek()
        {
            InitializeComponent();
        }
        public static string ScheduleWeekSearch = DateTime.Now.ToString("yyyy-MM-dd");
        private static Panel p3;
        private static Panel p4;
        private static Panel p5;
        private static Panel p6;
        private static int ScheduleHourLength = int.Parse(string.Format(FunFile.GetString(FunFile.iniAppSchd, "[SCHEDULE]", "ScheduleHourLength")[0]));
        private static int ScheduleDays = int.Parse(string.Format(FunFile.GetString(FunFile.iniAppSchd, "[SCHEDULE]", "ScheduleDays")[0]));
        private static string[] ScheduleDateBox = null;
        private static bool ScheduleStatusControl = FunFile.GetString(FunFile.iniAppSchd, "[SCHEDULE]", "ScheduleStatusControl")[0] == "0";
        class ThisApplicationSetup
        {
            private static void CreatePanel(UserControl uc, Panel p1, Panel p2)
            {
                p3 = new Panel
                {
                    Dock = DockStyle.Fill,
                };
                p4 = new Panel
                {
                    Dock = DockStyle.Fill,
                };
                p1.Controls.Add(p3);
                p2.SendToBack();
                p3.BringToFront();
                uc.Controls.Add(p4);
                p1.SendToBack();
                p4.BringToFront();
            }
            public static void ThisApplicationSetupExec(UserControl uc, Panel p1, Panel p2)
            {
                CreatePanel(uc, p1, p2);
            }
        }
        class ThisApplicationLoad
        {
            private static void CreatePanel()
            {
                if (p3.Contains(p5) && p4.Contains(p6))
                {
                    p5.Dispose();
                    p6.Dispose();
                }
                p5 = new Panel
                {
                    Dock = DockStyle.Fill,
                };
                p6 = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                };
            }
            public static void CreateContextMenuStrip(string SCHEDULEID, Panel p, Label l)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                string STATUSNAME = p.Name;
                if (STATUSNAME != "完了" && STATUSNAME != "保留" || ScheduleStatusControl)
                {
                    contextMenuStrip.Items.Add("更新", Schedule.UpdateImg, (sender, e) =>
                    {
                        if (Schedule.ScheduleFormInstance == null || Schedule.ScheduleFormInstance.IsDisposed)
                        {
                            Schedule.ScheduleFormInstance = new ScheduleForm("更新", "週間", SCHEDULEID);
                            Schedule.ScheduleFormInstance.Show();
                            Schedule.ScheduleFormInstance.Location = new Point(
                                int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[0])),
                                int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[1]))
                                );
                        }
                    });
                }
                if (STATUSNAME != "対応中")
                {
                    contextMenuStrip.Items.Add("進捗更新(→対応中)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "1", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "未")
                {
                    contextMenuStrip.Items.Add("進捗更新(→未)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "完了")
                {
                    contextMenuStrip.Items.Add("進捗更新(→完了)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "3", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("進捗更新(→保留)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "4", SCHEDULEID });
                        DataLoad();
                    });
                }
                contextMenuStrip.Items.Add("収納箱へ", Schedule.ToStorageBinImg, (sender, e) =>
                {
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                    DataLoad();
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Schedule.ToRecycleBinImg, (sender, e) =>
                {
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "0", SCHEDULEID });
                    DataLoad();
                });
                p.ContextMenuStrip = contextMenuStrip;
                l.ContextMenuStrip = contextMenuStrip;
            }
            private static void CreateSchedulePanel(string ScheduleDate, int LocX)
            {
                string[][] output = FunSQL.SQLSELECT("SQLSchedule0004", Schedule.SQLSchedule0004, new string[] { "@SCHEDULEDATE" }, new string[] { $"{ScheduleDate}%" });
                for (int i = 0; i < output.Length; i++)
                {
                    Panel p = new Panel
                    {
                        Name = output[i][5],
                        Location = new Point(LocX + 60, int.Parse(output[i][9]) * ScheduleHourLength / 60+10),
                        Size = new Size(p5.Width / (ScheduleDays + 1), int.Parse(output[i][10]) * ScheduleHourLength / 60),
                        BorderStyle = BorderStyle.Fixed3D,
                    };
                    string STATUSNAME = output[i][5];
                    if (STATUSNAME == "完了")
                    {
                        p.BackColor = Color.DarkGray;
                        p.ForeColor = Color.White;
                    }
                    else if (STATUSNAME == "対応中")
                    {
                        p.BackColor = Color.Firebrick;
                        p.ForeColor = Color.White;
                    }
                    else if (STATUSNAME == "未")
                    {
                        p.BackColor = Color.MediumSeaGreen;
                        p.ForeColor = Color.White;
                    }
                    else if (STATUSNAME == "保留")
                    {
                        p.BackColor = Color.Aquamarine;
                        p.ForeColor = Color.White;
                    }
                    Label l = new Label
                    {
                        Text = $"【{output[i][3]}】\n{output[i][1]}\n{output[i][2]}",
                        Location = new Point(5, 5),
                        AutoSize = true,
                        Font = new Font("Meiryo UI", 7, FontStyle.Regular),
                    };
                    p.Controls.Add(l);
                    p6.Controls.Add(p);
                    string DetailInfo = $"" +
                        $"【目標】：{output[i][1]}" +
                        $"\n【計画】：{output[i][2]}" +
                        $"\n【優先度】：{output[i][4]}({output[i][3]})" +
                        $"\n【進捗】：{output[i][5]}" +
                        $"\n【開始時間】：{output[i][6]}" +
                        $"\n【終了時間】：{output[i][7]}" +
                        $"\n【所要時間】：{output[i][8]}H";
                    ToolTip ttp1 = new ToolTip();
                    ttp1.AutoPopDelay = 10000;
                    ttp1.SetToolTip(p, DetailInfo);
                    ttp1.SetToolTip(l, DetailInfo);
                    CreateContextMenuStrip(output[i][0], p, l);
                }
            }
            private static void CreateDateLabel()
            {
                p3.Controls.Add(p5);
                ScheduleDateBox = new string[] { };
                for (int i = 0; i < ScheduleDays; i++)
                {
                    string ScheduleDate = DateTime.Parse(ScheduleWeekSearch).AddDays(i - (ScheduleDays / 2)).ToString("yyyy-MM-dd");
                    Array.Resize(ref ScheduleDateBox, ScheduleDateBox.Length + 1);
                    ScheduleDateBox[ScheduleDateBox.Length - 1] = ScheduleDate;
                }
                for (int i = 0; i < ScheduleDateBox.Length; i++)
                {
                    string ScheduleDate = ScheduleDateBox[i];
                    Label l = new Label
                    {
                        Text = ScheduleDate,
                        Font = new Font("Meiryo UI", 7, FontStyle.Regular),
                        Location = new Point(i * (p5.Width) / (ScheduleDays + 1), 40),
                        AutoSize = true,
                    };
                    p5.Controls.Add(l);
                    CreateSchedulePanel(ScheduleDate, l.Location.X);
                }
            }
            private static void CreateTimeLabel()
            {
                p4.Controls.Add(p6);
                for (int i = 0; i < 25; i++)
                {
                    Label l = new Label
                    {
                        Text = i.ToString("00") + ":00",
                        Font = new Font("Meiryo UI", 7, FontStyle.Regular),
                        Location = new Point(0, i * ScheduleHourLength),
                        Width = 50,
                    };
                    p6.Controls.Add(l);
                }
            }
            public static void DataLoad()
            {
                CreatePanel();
                CreateDateLabel();
                CreateTimeLabel();
            }
        }
        private void b1_Click(object sender, EventArgs e)
        {
            if (Schedule.ScheduleFormInstance == null || Schedule.ScheduleFormInstance.IsDisposed)
            {
                Schedule.ScheduleFormInstance = new ScheduleForm("確定", "週間", "");
                Schedule.ScheduleFormInstance.Show();
                Schedule.ScheduleFormInstance.Location = new Point(
                    int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[0])),
                    int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[1]))
                    );
            }
        }
        private void ScheduleWeek_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(this, p1, p2);
        }
        private void ScheduleWeek_Resize(object sender, EventArgs e)
        {
            ThisApplicationLoad.DataLoad();
        }
        public class AcessCls
        {
            public static void DataLoad()
            {
                ThisApplicationLoad.DataLoad();
            }
        }
    }
}
