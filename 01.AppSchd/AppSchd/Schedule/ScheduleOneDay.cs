using System;
using System.Drawing;
using System.Windows.Forms;

namespace Schedule
{
    public partial class ScheduleOneDay : UserControl
    {
        public ScheduleOneDay()
        {
            InitializeComponent();
        }
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow = null;
        private static Panel p6;
        private static Panel p7;
        private static int ScheduleHourLength = int.Parse(string.Format(FunFile.GetString(FunFile.iniAppSchd, "[SCHEDULE]", "ScheduleHourLength")[0]));
        public static string ScheduleOneDaySearch = DateTime.Now.ToString("yyyy-MM-dd");
        public static string SCHEDULEID = "";
        class ThisApplicationSetup
        {
            private static void CreatePanel(Panel p3)
            {
                p6 = new Panel
                {
                    Dock = DockStyle.Fill,
                };
                p3.Controls.Add(p6);
            }
            private static void CreateDataGridView(Panel p4)
            {
                DataGridViewCellStyle dgCellStyle1 = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Meiryo UI", 9F),
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White,
                    SelectionBackColor = Color.SteelBlue,
                    SelectionForeColor = Color.White,
                    WrapMode = DataGridViewTriState.True,
                };
                DataGridViewCellStyle dgCellStyle2 = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.TopLeft,
                    Font = new Font("Meiryo UI", 9F),
                    BackColor = Color.LightSteelBlue,
                    ForeColor = SystemColors.ControlText,
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = SystemColors.HighlightText,
                    WrapMode = DataGridViewTriState.False,
                };
                dg = new DataGridView
                {
                    ColumnHeadersDefaultCellStyle = dgCellStyle1,
                    DefaultCellStyle = dgCellStyle2,
                    Dock = DockStyle.Fill,
                    EnableHeadersVisualStyles = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    ScrollBars = ScrollBars.Vertical,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.LightSteelBlue,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    BorderStyle = BorderStyle.None,
                    MultiSelect = false,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                };
                string[] ColumnNames = { "ID", "", "目標名", "計画名", "進捗", "開始時間", "終了時間", "工数(H)", "更新日" };
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    dg.Columns.Add(i.ToString(), ColumnNames[i]);
                    dg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dg.CellClick += (sender, e) =>
                {
                    if (dg.SelectedRows.Count == 0) { return; }
                    if (ActiveRow != null) { ActiveRow.ContextMenuStrip = null; }
                    ActiveRow = dg.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip1(ActiveRow);
                };
                /*
                dg.RowsAdded += (sender, e) =>
                {
                    ThisApplicationLoad.CreateContextMenuStrip(dg.RowCount - 1);
                };
                 */
                dg.Columns[0].Visible = false;
                p4.Controls.Add(dg);
            }
            public static void ThisApplicaionSetupExec(Panel p3, Panel p4)
            {
                CreatePanel(p3);
                CreateDataGridView(p4);
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip1(DataGridViewRow ActiveRow)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("更新", Schedule.UpdateImg, (sender, e) =>
                {
                    string SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                    if (Schedule.ScheduleFormInstance == null || Schedule.ScheduleFormInstance.IsDisposed)
                    {
                        Schedule.ScheduleFormInstance = new ScheduleForm("更新", "日別", SCHEDULEID);
                        Schedule.ScheduleFormInstance.Show();
                        Schedule.ScheduleFormInstance.Location = new Point(
                            int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[0])),
                            int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[1]))
                            );
                    }
                });
                string STATUSNAME = ActiveRow.Cells[4].Value.ToString();
                if (STATUSNAME != "対応中")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→対応中)", Schedule.StatusImg, (sender, e) =>
                    {
                        string SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "1", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "未")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→未)", Schedule.StatusImg, (sender, e) =>
                    {
                        string SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "完了")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→完了)", Schedule.StatusImg, (sender, e) =>
                    {
                        string SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "3", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→保留)", Schedule.StatusImg, (sender, e) =>
                    {
                        string SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "4", SCHEDULEID });
                        DataLoad();
                    });
                }
                contextMenuStrip.Items.Add("収納箱へ", Schedule.ToStorageBinImg, (sender, e) =>
                {
                    SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                    DataLoad();
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Schedule.ToRecycleBinImg, (sender, e) =>
                {
                    SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "0", SCHEDULEID });
                    DataLoad();
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void CreateContextMenuStrip2(string SCHEDULEID, Panel p, Label l)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("更新", Schedule.UpdateImg, (sender, e) =>
                {
                    if (Schedule.ScheduleFormInstance == null || Schedule.ScheduleFormInstance.IsDisposed)
                    {
                        Schedule.ScheduleFormInstance = new ScheduleForm("更新", "日別", SCHEDULEID);
                        Schedule.ScheduleFormInstance = new ScheduleForm("更新", "日別", SCHEDULEID);
                        Schedule.ScheduleFormInstance.Show();
                        Schedule.ScheduleFormInstance.Location = new Point(
                            int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[0])),
                            int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[1]))
                            );
                    }
                });
                string STATUSNAME = p.Name;
                if (STATUSNAME != "対応中")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→対応中)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "1", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "未")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→未)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "完了")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→完了)", Schedule.StatusImg, (sender, e) =>
                    {
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "3", SCHEDULEID });
                        DataLoad();
                    });
                }
                if (STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→保留)", Schedule.StatusImg, (sender, e) =>
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
            private static void CreateTimeLabel()
            {
                if (p6.Contains(p7))
                {
                    p7.Dispose();
                }
                p7 = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                };
                p6.Controls.Add(p7);
                for (int i = 0; i < 25; i++)
                {
                    Label l = new Label
                    {
                        Text = i.ToString("00") + ":00",
                        Font = new Font("Meiryo UI", 7, FontStyle.Regular),
                        Location = new Point(0, i * ScheduleHourLength),
                        Width = 50,
                    };
                    p7.Controls.Add(l);
                }
            }
            private static void CreateSchedulePanel()
            {
                string[][] output = FunSQL.SQLSELECT("SQLSchedule0004", Schedule.SQLSchedule0004, new string[] { "@SCHEDULEDATE" }, new string[] { $"{ScheduleOneDaySearch}%" });
                for (int i = 0; i < output.Length; i++)
                {
                    Panel p = new Panel
                    {
                        Name = output[i][5],
                        Location = new Point(55, int.Parse(output[i][9]) * ScheduleHourLength / 60 + 10),
                        Size = new Size(p7.Width - 85, int.Parse(output[i][10]) * ScheduleHourLength / 60),
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
                    p7.Controls.Add(p);
                    string DetailInfo = $"" +
                        $"【目標】：{output[i][1]}" +
                        $"\n【計画】：{output[i][2]}" +
                        $"\n【優先度】：{output[i][3]}{output[i][4]}" +
                        $"\n【進捗】：{output[i][5]}" +
                        $"\n【開始時間】：{DateTime.Parse(output[i][6]).ToString("yyyy-MM-dd HH:mm")}" +
                        $"\n【終了時間】：{DateTime.Parse(output[i][7]).ToString("yyyy-MM-dd HH:mm")}" +
                        $"\n【所要時間】：{output[i][8]}H";
                    ToolTip ttp1 = new ToolTip();
                    ttp1.AutoPopDelay = 10000;
                    ttp1.SetToolTip(p, DetailInfo);
                    ttp1.SetToolTip(l, DetailInfo);
                    CreateContextMenuStrip2(output[i][0], p, l);
                }
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                CreateTimeLabel();
                CreateSchedulePanel();
                string[][] output = FunSQL.SQLSELECT("SQLSchedule0000", Schedule.SQLSchedule0000, new string[] { "@SCHEDULEDATE" }, new string[] { $"{ScheduleOneDaySearch}%" });
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                for (int i = 0; i < output.Length; i++)
                {
                    dg.Rows.Add(
                        output[i][0],
                        output[i][8],
                        output[i][1],
                        output[i][2],
                        output[i][3],
                        DateTime.Parse(output[i][4]).ToString("HH:mm"),
                        DateTime.Parse(output[i][5]).ToString("HH:mm"),
                        output[i][6],
                        DateTime.Parse(output[i][7]).ToString("yyyy-MM-dd")
                        );
                    if (output[i][8] != "")
                    {
                        int count = dg.Rows.Count - 1;
                        dg.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                        dg.Rows[count].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private void ScheduleOneDay_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicaionSetupExec(p3, p4);
            ThisApplicationLoad.DataLoad();
            dg.BringToFront();
            this.Disposed += (sender1, e1) =>
            {
                if (Schedule.ScheduleSearchInstance != null && !Schedule.ScheduleSearchInstance.IsDisposed)
                {
                    Schedule.ScheduleSearchInstance.Close();
                }
                if (Schedule.ScheduleFormInstance != null && !Schedule.ScheduleFormInstance.IsDisposed)
                {
                    Schedule.ScheduleFormInstance.Close();
                }
            };
        }
        private void b1_Click(object sender, EventArgs e)
        {
            if (Schedule.ScheduleFormInstance == null || Schedule.ScheduleFormInstance.IsDisposed)
            {
                Schedule.ScheduleFormInstance = new ScheduleForm("確定", "日別", "");
                Schedule.ScheduleFormInstance.Show();
                Schedule.ScheduleFormInstance.Location = new Point(
                    int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[0])),
                    int.Parse(string.Format("{0}", ScheduleSchedule.SubLocation[1]))
                    );
            }
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
