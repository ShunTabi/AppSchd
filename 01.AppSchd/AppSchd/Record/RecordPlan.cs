using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record
{
    public partial class RecordPlan : UserControl
    {
        public RecordPlan()
        {
            InitializeComponent();
        }
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow = null;
        public static string RecordPlanSearch = "";
        public static string PLANID = "";
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p2, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
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
                dg = new Record.DataGridViewEx
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
                string[] ColumnNames = { "ID", "", "目標名", "計画名", "優先度", "進捗", "開始日", "終了日", "更新日" };
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
                p2.Controls.Add(dg);
            }
            private static void CreateCmbItems(ComboBox cmb1, ComboBox cmb2)
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add("ValueMember");
                dt1.Columns.Add("DisplayMember");
                string[][] output1 = FunSQL.SQLSELECT("SQLRecordGoal0002", Record.SQLRecordGoal0002, new string[] { }, new string[] { });
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
                DataTable dt2 = new DataTable();
                DataRow dr2;
                dt2.Columns.Add("ValueMember");
                dt2.Columns.Add("DisplayMember");
                string sql = "SELECT PRIORID,PRIORNAME FROM T_PRIOR WHERE PRIORVISIBLESTATUS=1 ORDER BY PRIORID ASC";
                string[][] output2 = FunSQL.SQLSELECT("PriorStatusSQL", sql, new string[] { }, new string[] { });
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
            public static void ThisApplicaionSetupExec(Panel p2, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                CreateDataGridView(p2, cmb1, tb1, cmb2, tb2, tb3, b1);
                CreateCmbItems(cmb1, cmb2);
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, TextBox tb2, TextBox tb3, Button b1)
            {
                tb1.Text = "";
                b1.Text = "確定";
                tb2.Text = DateTime.Now.ToString("yyyy-MM-dd");
                tb3.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                PLANID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                string STATUSNAME = ActiveRow.Cells[5].Value.ToString();
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Record.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                if (STATUSNAME != "完了" && STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("更新", Record.UpdateImg, (sender, e) =>
                    {
                        PLANID = ActiveRow.Cells[0].Value.ToString();
                        string[][] output = FunSQL.SQLSELECT("SQLRecordPlan0002", Record.SQLRecordPlan0002, new string[] { "@PLANID" }, new string[] { PLANID });
                        if (cmb1.FindStringExact(output[0][0]) < 0 || cmb2.FindStringExact(output[0][2]) < 0)
                        {
                            Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                            ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                        }
                        else
                        {
                            cmb1.Text = output[0][0];
                            tb1.Text = output[0][1];
                            cmb2.Text = output[0][2];
                            tb2.Text = DateTime.Parse(output[0][3]).ToString("yyyy-MM-dd");
                            tb3.Text = DateTime.Parse(output[0][4]).ToString("yyyy-MM-dd");
                            b1.Text = "更新";
                        }
                    });
                }
                if (STATUSNAME != "対応中")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→対応中)", Record.StatusImg, (sender, e) =>
                    {
                        PLANID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLRecordPlan0022", Record.SQLRecordPlan0022, new string[] { "@STATUSID", "@PLANID" }, new string[] { "1", PLANID });
                        if (dg.SelectedRows[0].Cells[4].Value.ToString() == "-")
                        {
                            FunSQL.SQLDML("SQLRecordPlan0023", Record.SQLRecordPlan0023, new string[] { "@PRIORID", "@PLANID" }, new string[] { "1", PLANID });
                        }
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "未")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→未)", Record.StatusImg, (sender, e) =>
                    {
                        PLANID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLRecordPlan0022", Record.SQLRecordPlan0022, new string[] { "@STATUSID", "@PLANID" }, new string[] { "2", PLANID });
                        if (dg.SelectedRows[0].Cells[4].Value.ToString() == "-")
                        {
                            FunSQL.SQLDML("SQLRecordPlan0023", Record.SQLRecordPlan0023, new string[] { "@PRIORID", "@PLANID" }, new string[] { "1", PLANID });
                        }
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "完了")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→完了)", Record.StatusImg, (sender, e) =>
                    {
                        PLANID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLRecordPlan0022", Record.SQLRecordPlan0022, new string[] { "@STATUSID", "@PLANID" }, new string[] { "3", PLANID });
                        FunSQL.SQLDML("SQLRecordPlan0023", Record.SQLRecordPlan0023, new string[] { "@PRIORID", "@PLANID" }, new string[] { "4", PLANID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }
                if (STATUSNAME != "保留")
                {
                    contextMenuStrip.Items.Add("ステータス変更(→保留)", Record.StatusImg, (sender, e) =>
                    {
                        PLANID = ActiveRow.Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQLRecordPlan0022", Record.SQLRecordPlan0022, new string[] { "@STATUSID", "@PLANID" }, new string[] { "4", PLANID });
                        FunSQL.SQLDML("SQLRecordPlan0023", Record.SQLRecordPlan0023, new string[] { "@PRIORID", "@PLANID" }, new string[] { "4", PLANID });
                        DataLoad();
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                    });
                }

                contextMenuStrip.Items.Add("収納箱へ", Record.ToStorageBinImg, (sender, e) =>
                {
                    PLANID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordPlan0021", Record.SQLRecordPlan0021, new string[] { "@VISIBLESTATUS", "@PLANID" }, new string[] { "2", PLANID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Record.ToRecycleBinImg, (sender, e) =>
                {
                    PLANID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordPlan0021", Record.SQLRecordPlan0021, new string[] { "@VISIBLESTATUS", "@PLANID" }, new string[] { "0", PLANID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                string[][] output = null;
                if (RecordPlanSearch == "")
                {
                    output = FunSQL.SQLSELECT("SQLRecordPlan0000", Record.SQLRecordPlan0000, new string[] { }, new string[] { });
                }
                else
                {
                    output = FunSQL.SQLSELECT("SQLRecordPlan0001", Record.SQLRecordPlan0001, new string[] { "@KEYWORD" }, new string[] { $"%{RecordPlanSearch}%" });
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                for (int i = 0; i < output.Length; i++)
                {
                    dg.Rows.Add(
                        output[i][0],
                        output[i][8],
                        output[i][1],
                        output[i][2],
                        output[i][3],
                        output[i][4],
                        DateTime.Parse(output[i][5]).ToString("yyyy-MM-dd"),
                        DateTime.Parse(output[i][6]).ToString("yyyy-MM-dd"),
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
        private void RecordPlan_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicaionSetupExec(p2, cmb1, tb1, cmb2, tb2, tb3, b1);
            tb2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tb3.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            ThisApplicationLoad.DataLoad();
            dg.BringToFront();
            this.Disposed += (sender1, e1) =>
            {
                if (Record.RecordSearchInstance != null && !Record.RecordSearchInstance.IsDisposed)
                {
                    Record.RecordSearchInstance.Close();
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
            if (tb1.Text == "") { return; }
            if (!Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$") || !Regex.IsMatch(tb3.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
            if (b1.Text == "確定" && PLANID == "")
            {
                FunSQL.SQLDML("SQLRecordPlan0010", Record.SQLRecordPlan0010, new string[] { "@GOALID", "@PLANNAME", "@PRIORID", "@PLANSTARTDATE", "@PLANENDDATE" }, new string[] { cmb1.SelectedValue.ToString(), tb1.Text, cmb2.SelectedValue.ToString(), $"{tb2.Text} 00:00:00", $"{tb3.Text} 00:00:00" });
            }
            else if (b1.Text == "更新" && PLANID != "")
            {
                FunSQL.SQLDML("SQLRecordPlan0020", Record.SQLRecordPlan0020, new string[] { "@GOALID", "@PLANNAME", "@PRIORID", "@PLANSTARTDATE", "@PLANENDDATE", "@PLANID" }, new string[] { cmb1.SelectedValue.ToString(), tb1.Text, cmb2.SelectedValue.ToString(), $"{tb2.Text} 00:00:00", $"{tb3.Text} 00:00:00", PLANID });
            }
            ThisApplicationLoad.DataLoad();
            ThisApplicationCleaning.FormCleaning(tb1, tb2, tb3, b1);
        }
    }
}
