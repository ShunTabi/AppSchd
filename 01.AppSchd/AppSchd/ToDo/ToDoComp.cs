using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class ToDoComp : UserControl
    {
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow1 = null;
        public static DataGridViewRow ActiveRow2 = null;
        public static string ToDoCompSearch = "";
        public static string TODOID = "";
        public ToDoComp()
        {
            InitializeComponent();
        }
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p2, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                DataGridViewCellStyle dgCellStyle1 = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Meiryo UI", 8F),
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White,
                    SelectionBackColor = Color.SteelBlue,
                    SelectionForeColor = Color.White,
                    WrapMode = DataGridViewTriState.True,
                };
                DataGridViewCellStyle dgCellStyle2 = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.TopLeft,
                    Font = new Font("Meiryo UI", 8F),
                    BackColor = Color.LightSteelBlue,
                    ForeColor = SystemColors.ControlText,
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = SystemColors.HighlightText,
                    WrapMode = DataGridViewTriState.False,
                };
                dg = new ToDo.DataGridViewEx
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
                    Font = new Font("Meiryo UI", 8, FontStyle.Regular),
                };
                string[] ColumnNames = { "ID", "", "目標名", "計画名", "ToDo名", "進捗", "完了日", "更新日" };
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    dg.Columns.Add(i.ToString(), ColumnNames[i]);
                    dg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dg.CellClick += (sender, e) =>
                {
                    if (dg.SelectedRows.Count == 0) { return; }
                    if (ActiveRow1 != null) { ActiveRow1.ContextMenuStrip = null; }
                    ActiveRow1 = dg.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow1, cmb1, cmb2, tb1, tb2, b1);
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
                string sql1 = "SELECT GOALID,GOALNAME FROM T_GOAL WHERE GOALVISIBLESTATUS=1 ORDER BY GOALNAME,GOALUPDATEDATE DESC";
                string[][] output1 = FunSQL.SQLSELECT("***ToDo***", sql1, new string[] { }, new string[] { });
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
            public static void ThisApplicationExec(Panel p2, ComboBox cmb1, TextBox tb1, ComboBox cmb2, TextBox tb2, TextBox tb3, Button b1)
            {
                CreateDataGridView(p2, cmb1, tb1, cmb2, tb2, tb3, b1);
                CreateCmbItems(cmb1, cmb2);
                if (cmb1.Text == "") { return; }
                ThisApplicationLoad.LoadCmbData(cmb2, cmb1.SelectedValue.ToString());
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, TextBox tb2, Button b1)
            {
                tb1.Text = "";
                tb2.Text = DateTime.Now.ToString("yyyy-MM-dd");
                b1.Text = "確定";
                TODOID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, ComboBox cmb1, ComboBox cmb2, TextBox tb1, TextBox tb2, Button b1)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", ToDo.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                contextMenuStrip.Items.Add("更新", ToDo.UpdateImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    string[][] output = FunSQL.SQLSELECT("SQLToDo0004", ToDo.SQLToDo0004, new string[] { "@TODOID" }, new string[] { TODOID });
                    if (cmb1.FindStringExact(output[0][0]) < 0)
                    {
                        Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                    }
                    cmb1.Text = output[0][0];
                    if (cmb2.FindStringExact(output[0][1]) < 0)
                    {
                        Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                        ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                    }
                    else
                    {
                        cmb2.Text = output[0][1];
                        tb1.Text = output[0][2];
                        tb2.Text = DateTime.Parse(output[0][3]).ToString("yyyy-MM-dd");
                        b1.Text = "更新";
                    }
                });
                contextMenuStrip.Items.Add("ステータス変更(→対応中)", ToDo.StatusImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0022", ToDo.SQLToDo0022, new string[] { "@STATUSID", "@TODOID" }, new string[] { "1", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                contextMenuStrip.Items.Add("ステータス変更(→未)", ToDo.StatusImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0022", ToDo.SQLToDo0022, new string[] { "@STATUSID", "@TODOID" }, new string[] { "2", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                /*
                contextMenuStrip.Items.Add("ステータス変更(→完了)", ToDo.StatusImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0022", ToDo.SQLToDo0022, new string[] { "@STATUSID", "@TODOID" }, new string[] { "3", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                */
                contextMenuStrip.Items.Add("ステータス変更(→保留)", ToDo.StatusImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0022", ToDo.SQLToDo0022, new string[] { "@STATUSID", "@TODOID" }, new string[] { "4", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                contextMenuStrip.Items.Add("収納箱へ", ToDo.ToStorageBinImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0021", ToDo.SQLToDo0021, new string[] { "@VISIBLESTATUS", "@TODOID" }, new string[] { "2", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", ToDo.ToRecycleBinImg, (sender, e) =>
                {
                    TODOID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLToDo0021", ToDo.SQLToDo0021, new string[] { "@VISIBLESTATUS", "@TODOID" }, new string[] { "0", TODOID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                string[][] output = null;
                if (ToDoCompSearch == "")
                {
                    output = FunSQL.SQLSELECT("SQLToDo0002", ToDo.SQLToDo0002, new string[] { }, new string[] { });
                }
                else
                {
                    output = FunSQL.SQLSELECT("SQLToDo0003", ToDo.SQLToDo0003, new string[] { "@KEYWORD" }, new string[] { $"%{ToDoCompSearch}%" });
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                for (int i = 0; i < output.Length; i++)
                {
                    dg.Rows.Add(
                        output[i][0],
                        "",
                        output[i][1],
                        output[i][2],
                        output[i][3],
                        output[i][4],
                        DateTime.Parse(output[i][5]).ToString("yyyy-MM-dd"),
                        DateTime.Parse(output[i][6]).ToString("yyyy-MM-dd")
                        /*
                         
                         */
                        );
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
        private void ToDoComp_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationExec(p2, cmb1, tb1, cmb2, tb2, tb1, b1);
            ThisApplicationLoad.DataLoad();
            tb2.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            if (!Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
            if (b1.Text == "確定" && TODOID == "")
            {
                FunSQL.SQLDML("SQLToDo0010", ToDo.SQLToDo0010, new string[] { "@PLANID", "@TODONAME", "@TODOENDDATE" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb2.Text} 00:00:00" });
            }
            else if (b1.Text == "更新" && TODOID != "")
            {
                FunSQL.SQLDML("SQLToDo0020", ToDo.SQLToDo0020, new string[] { "@PLANID", "@TODONAME", "@TODOENDDATE", "@TODOID" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb2.Text} 00:00:00", TODOID });
            }
            ThisApplicationLoad.DataLoad();
            ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
        }
        private void b1_Click_1(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
            if (b1.Text == "確定" && TODOID == "")
            {
                FunSQL.SQLDML("SQLToDo0010", ToDo.SQLToDo0010, new string[] { "@PLANID", "@TODONAME", "@STATUSID", "@TODOENDDATE" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, "3", tb2.Text });
            }
            else if (b1.Text == "更新" && TODOID != "")
            {
                FunSQL.SQLDML("SQLToDo0020", ToDo.SQLToDo0020, new string[] { "@PLANID", "@TODONAME", "@TODOENDDATE", "@TODOID" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, tb2.Text, TODOID });
            }
            ThisApplicationLoad.DataLoad();
            ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
        }
    }
}
