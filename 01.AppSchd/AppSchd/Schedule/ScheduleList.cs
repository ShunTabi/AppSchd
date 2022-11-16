using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public partial class ScheduleList : UserControl
    {
        public ScheduleList()
        {
            InitializeComponent();
        }
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow = null;
        public static string ScheduleListSearch = "";
        public static string SCHEDULEID = "";
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p6, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
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
                dg = new Schedule.DataGridViewEx
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
                string[] ColumnNames = { "ID", "", "目標名", "計画名", "進捗", "日付", "開始時間", "終了時間", "工数(H)", "更新日" };
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
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow, cmb1, tb1, cmb2, tb2, tb3, b1);
                };
                dg.Columns[0].Visible = false;
                p6.Controls.Add(dg);
            }
            private static void CreateCmbItems(ComboBox cmb1, ComboBox cmb2)
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add("ValueMember");
                dt1.Columns.Add("DisplayMember");
                string sql1 = "SELECT GOALID,GOALNAME FROM T_GOAL WHERE GOALVISIBLESTATUS=1 ORDER BY GOALNAME,GOALUPDATEDATE DESC";
                string[][] output1 = FunSQL.SQLSELECT("***Schedule***", sql1, new string[] { }, new string[] { });
                for (int i = 0; i < output1.Length; i++)
                {
                    dr1 = dt1.NewRow();
                    dr1["ValueMember"] = output1[i][0];
                    dr1["DisplayMember"] = output1[i][1];
                    dt1.Rows.Add(dr1);
                }
                cmb1.DataSource = dt1;
                cmb1.ValueMember = "ValueMember";
                cmb1.DisplayMember = "DisplayMember";
                cmb1.SelectedIndexChanged += (sender, e) =>
                {
                    ThisApplicationLoad.LoadCmbData(cmb2, cmb1.SelectedValue.ToString());
                };
            }
            public static void ThisApplicaionSetupExec(Panel p6, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                CreateDataGridView(p6, cmb1, tb1, cmb2, tb2, tb3, b1);
                CreateCmbItems(cmb1, cmb2);
                if (cmb1.Text == "") { return; }
                ThisApplicationLoad.LoadCmbData(cmb2, cmb1.SelectedValue.ToString());
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, TextBox tb2, TextBox tb3, Button b1)
            {
                tb1.Text = DateTime.Now.ToString("yyyy-MM-dd");
                tb2.Text = DateTime.Now.ToString("HH:mm");
                tb3.Text = DateTime.Now.ToString("HH:mm");
                b1.Text = "確定";
                SCHEDULEID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Schedule.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                string STATUSNAME = ActiveRow.Cells[4].Value.ToString();
                if (STATUSNAME != "完了" && STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("更新", Schedule.UpdateImg, (sender, e) =>
                    {
                        SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        string[][] output = FunSQL.SQLSELECT("SQLSchedule0003", Schedule.SQLSchedule0003, new string[] { "@SCHEDULEID" }, new string[] { SCHEDULEID });
                        if (cmb1.FindStringExact(output[0][0]) < 0)
                        {
                            Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                            ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                            return;
                        }
                        cmb1.Text = output[0][0];
                        if (cmb2.FindStringExact(output[0][1]) < 0)
                        {
                            Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                            ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                        }
                        else
                        {
                            cmb2.Text = output[0][1];
                            tb1.Text = output[0][2];
                            tb2.Text = output[0][3];
                            tb3.Text = output[0][4];
                            b1.Text = "更新";
                        }
                    });
                }
                if (STATUSNAME != "対応中")
                {
                    contextMenuStrip.Items.Add("進捗更新(→対応中)", Schedule.StatusImg, (sender, e) =>
                    {
                        SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "1", SCHEDULEID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "未")
                {
                    contextMenuStrip.Items.Add("進捗更新(→未)", Schedule.StatusImg, (sender, e) =>
                    {
                        SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "完了")
                {
                    contextMenuStrip.Items.Add("進捗更新(→完了)", Schedule.StatusImg, (sender, e) =>
                    {
                        SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "3", SCHEDULEID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("進捗更新(→保留)", Schedule.StatusImg, (sender, e) =>
                    {
                        SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLSchedule0022", Schedule.SQLSchedule0022, new string[] { "@STATUSID", "@SCHEDULEID" }, new string[] { "4", SCHEDULEID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                contextMenuStrip.Items.Add("収納箱へ", Schedule.ToStorageBinImg, (sender, e) =>
                {
                    SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "2", SCHEDULEID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Schedule.ToRecycleBinImg, (sender, e) =>
                {
                    SCHEDULEID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLSchedule0021", Schedule.SQLSchedule0021, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "0", SCHEDULEID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                string[][] output = null;
                if (ScheduleListSearch == "")
                {
                    output = FunSQL.SQLSELECT("SQLSchedule0001", Schedule.SQLSchedule0001, new string[] { }, new string[] { });
                }
                else
                {
                    output = FunSQL.SQLSELECT("SQLSchedule0002", Schedule.SQLSchedule0002, new string[] { "@KEYWORD" }, new string[] { $"%{ScheduleListSearch}%" });
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                for (int i = 0; i < output.Length; i++)
                {
                    dg.Rows.Add(
                        output[i][0],
                        output[i][1],
                        output[i][2],
                        output[i][3],
                        output[i][4],
                        output[i][5],
                        output[i][6],
                        output[i][7],
                        output[i][8],
                        output[i][9]
                        );
                    if (output[i][1] != "")
                    {
                        int count = dg.Rows.Count - 1;
                        dg.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                        dg.Rows[count].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            public static void LoadCmbData(ComboBox cmb2, string SelectValue)
            {
                DataTable dt2 = new DataTable();
                DataRow dr2;
                dt2.Columns.Add("ValueMember");
                dt2.Columns.Add("DisplayMember");
                string sql2 = "SELECT PLANID,PLANNAME FROM T_PLAN WHERE GOALID=@GOALID AND PLANVISIBLESTATUS=1 ORDER BY PRIORID ASC,STATUSID ASC,PLANENDDATE ASC,PLANNAME,PLANUPDATEDATE DESC";
                string[][] output2 = FunSQL.SQLSELECT("***Schedule***", sql2, new string[] { "@GOALID" }, new string[] { SelectValue });
                for (int i = 0; i < output2.Length; i++)
                {
                    dr2 = dt2.NewRow();
                    dr2["ValueMember"] = output2[i][0];
                    dr2["DisplayMember"] = output2[i][1];
                    dt2.Rows.Add(dr2);
                }
                cmb2.DataSource = dt2;
                cmb2.ValueMember = "ValueMember";
                cmb2.DisplayMember = "DisplayMember";
            }
        }
        private void ScheduleList_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicaionSetupExec(p2, cmb1, tb1, cmb2, tb2, tb3, b1);
            ThisApplicationLoad.DataLoad();
            tb1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb2.Text = DateTime.Now.ToString("HH:mm");
            tb3.Text = DateTime.Now.ToString("HH:mm");
            dg.BringToFront();
            this.Disposed += (sender1, e1) =>
            {
                if (Schedule.ScheduleSearchInstance != null && !Schedule.ScheduleSearchInstance.IsDisposed)
                {
                    Schedule.ScheduleSearchInstance.Close();
                }
            };
        }
        public class AcessCls
        {
            public static void DataLoad()
            {
                ThisApplicationLoad.DataLoad();
            }
        }
        private void b1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
            if (!Regex.IsMatch(tb2.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$") || !Regex.IsMatch(tb3.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$")) { return; }
            string[] STARTTIMEHM = DateTime.Parse(tb2.Text).ToString("HH:mm").Split(':');
            int SCHEDULELOCATION = int.Parse(STARTTIMEHM[0]) * 60 + int.Parse(STARTTIMEHM[1]);
            string[] ENDTIMEHM = DateTime.Parse(tb3.Text).ToString("HH:mm").Split(':');
            int SCHEDULEHEIGHT = int.Parse(ENDTIMEHM[0]) * 60 + int.Parse(ENDTIMEHM[1]) - SCHEDULELOCATION;
            string[][] CheckSchedule = FunSQL.SQLSELECT("SQLSchedule0005", Schedule.SQLSchedule0005, new string[] { "@SCHEDULEID", "@SCHEDULEDATE" }, new string[] { SCHEDULEID, tb1.Text });
            DateTime TGScheduleStartTime = DateTime.Parse($"{tb1.Text} {tb2.Text}:00");
            DateTime TGScheduleEndTime = DateTime.Parse($"{tb1.Text} {tb3.Text}:00");
            bool CheckResult = TGScheduleStartTime < TGScheduleEndTime;
            for (int i = 0; i < CheckSchedule.Length; i++)
            {
                DateTime ScheduledScheduleStartTime = DateTime.Parse(CheckSchedule[i][0]);
                DateTime ScheduledScheduleEndTime = DateTime.Parse(CheckSchedule[i][1]);
                CheckResult = CheckResult && ((TGScheduleEndTime < ScheduledScheduleEndTime && TGScheduleEndTime < ScheduledScheduleStartTime) || (ScheduledScheduleStartTime < TGScheduleStartTime && ScheduledScheduleEndTime < TGScheduleStartTime));
            }
            if (CheckResult)
            {
                if (b1.Text == "確定" && SCHEDULEID == "")
                {
                    FunSQL.SQLDML("SQLSchedule0010", Schedule.SQLSchedule0010, new string[] { "@PLANID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULELOCATION", "@SCHEDULEHEIGHT" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb1.Text} {tb2.Text}:00", $"{tb1.Text} {tb3.Text}:00", SCHEDULELOCATION.ToString(), SCHEDULEHEIGHT.ToString() });
                }
                else if (b1.Text == "更新" && SCHEDULEID != "")
                {
                    FunSQL.SQLDML("SQLSchedule0020", Schedule.SQLSchedule0020, new string[] { "@PLANID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULELOCATION", "@SCHEDULEHEIGHT", "@SCHEDULEID" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb1.Text} {tb2.Text}:00", $"{tb1.Text} {tb3.Text}:00", SCHEDULELOCATION.ToString(), SCHEDULEHEIGHT.ToString(), SCHEDULEID });
                }
                ThisApplicationLoad.DataLoad();
                ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
            }
        }
    }
}
