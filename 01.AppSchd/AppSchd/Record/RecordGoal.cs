using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record
{
    public partial class RecordGoal : UserControl
    {
        public RecordGoal()
        {
            InitializeComponent();
        }
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow = null;
        public static string RecordGoalSearch = "";
        public static string GOALID = "";
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p2, ComboBox cmb1, TextBox tb1, Button b1)
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
                string[] ColumnNames = { "ID", "", "種別名", "目標名", "更新日" };
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
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow, cmb1, tb1, b1);
                };
                dg.Columns[0].Visible = false;
                p2.Controls.Add(dg);
            }
            private static void CreateCmbItems(ComboBox cmb1)
            {

                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add("ValueMember");
                dt.Columns.Add("DisplayMember");
                string[][] output = FunSQL.SQLSELECT("SQLRecordGenre0002", Record.SQLRecordGenre0002, new string[] { }, new string[] { });
                for (int i = 0; i < output.Length; i++)
                {
                    dr = dt.NewRow();
                    dr["ValueMember"] = output[i][0];
                    dr["DisplayMember"] = output[i][1];
                    dt.Rows.Add(dr);
                }
                cmb1.DataSource = dt;
                cmb1.ValueMember = "ValueMember";
                cmb1.DisplayMember = "DisplayMember";
            }
            public static void ThisApplicationSetupExec(Panel p2, ComboBox cmb1, TextBox tb1, Button b1)
            {
                CreateDataGridView(p2, cmb1, tb1, b1);
                CreateCmbItems(cmb1);
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, Button b1)
            {
                tb1.Text = "";
                b1.Text = "確定";
                GOALID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, ComboBox cmb1, TextBox tb1, Button b1)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Record.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                contextMenuStrip.Items.Add("更新", Record.UpdateImg, (sender, e) =>
                {
                    GOALID = ActiveRow.Cells[0].Value.ToString();
                    string[][] output = FunSQL.SQLSELECT("SQLRecordGoal0003", Record.SQLRecordGoal0003, new string[] { "@GOALID" }, new string[] { GOALID });
                    if (cmb1.FindStringExact(output[0][0]) < 0)
                    {
                        Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                        ThisApplicationCleaning.FormCleaning(tb1, b1);
                    }
                    else
                    {
                        cmb1.Text = output[0][0];
                        tb1.Text = output[0][1];
                        b1.Text = "更新";
                    }
                });
                contextMenuStrip.Items.Add("収納箱へ", Record.ToStorageBinImg, (sender, e) =>
                {
                    GOALID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordGoal0021", Record.SQLRecordGoal0021, new string[] { "@VISIBLESTATUS", "@GOALID" }, new string[] { "2", GOALID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Record.ToRecycleBinImg, (sender, e) =>
                {
                    GOALID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordGoal0021", Record.SQLRecordGoal0021, new string[] { "@VISIBLESTATUS", "@GOALID" }, new string[] { "0", GOALID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                string[][] output = null;
                if (RecordGoalSearch == "")
                {
                    output = FunSQL.SQLSELECT("SQLRecordGoal0000", Record.SQLRecordGoal0000, new string[] { }, new string[] { });
                }
                else
                {
                    output = FunSQL.SQLSELECT("SQLRecordGoal0001", Record.SQLRecordGoal0001, new string[] { "@GOALNAME" }, new string[] { $"%{RecordGoalSearch}%" });
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
                        DateTime.Parse(output[i][3]).ToString("yyyy-MM-dd")
                        );
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private void RecordGoal_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(p2, cmb1, tb1, b1);
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
        private void b1_Click(object sender, EventArgs e)
        {
            if (tb1.Text == "") { return; }
            if (b1.Text == "確定" && GOALID == "")
            {
                FunSQL.SQLDML("SQLRecordGoal0010", Record.SQLRecordGoal0010, new string[] { "@GENREID", "@GOALNAME" }, new string[] { cmb1.SelectedValue.ToString(), tb1.Text });
            }
            else if (b1.Text == "更新" && GOALID != "")
            {
                FunSQL.SQLDML("SQLRecordGoal0020", Record.SQLRecordGoal0020, new string[] { "@GENREID", "@GOALNAME", "@GOALID" }, new string[] { cmb1.SelectedValue.ToString(), tb1.Text, GOALID });
            }
            ThisApplicationLoad.DataLoad();
            ThisApplicationCleaning.FormCleaning(tb1, b1);
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
