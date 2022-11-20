using System;
using System.Drawing;
using System.Windows.Forms;

namespace Record
{
    public partial class RecordGenre : UserControl
    {
        public RecordGenre()
        {
            InitializeComponent();
        }
        public static DataGridView dg;
        public static DataGridViewRow ActiveRow = null;
        public static string RecordGenreSearch = "";
        public static string GENREID = "";
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p2, TextBox tb1, Button b1)
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
                string[] ColumnNames = { "ID", "", "種別名", "更新日" };
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
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow, tb1, b1);
                };
                dg.Columns[0].Visible = false;
                p2.Controls.Add(dg);
            }
            public static void ThisApplicationSetupExec(Panel p2, TextBox tb1, Button b1)
            {
                CreateDataGridView(p2, tb1, b1);
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, Button b1)
            {
                tb1.Text = "";
                b1.Text = "確定";
                GENREID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, TextBox tb1, Button b1)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Record.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                contextMenuStrip.Items.Add("更新", Record.UpdateImg, (sender, e) =>
                {
                    GENREID = ActiveRow.Cells[0].Value.ToString();
                    string[][] output = FunSQL.SQLSELECT("SQLRecordGenre0003", Record.SQLRecordGenre0003, new string[] { "@GENREID" }, new string[] { GENREID });
                    tb1.Text = output[0][0];
                    b1.Text = "更新";
                });
                contextMenuStrip.Items.Add("収納箱へ", Record.ToStorageBinImg, (sender, e) =>
                {
                    GENREID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordGenre0021", Record.SQLRecordGenre0021, new string[] { "@VISIBLESTATUS", "@GENREID" }, new string[] { "2", GENREID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                contextMenuStrip.Items.Add("ゴミ箱へ", Record.ToRecycleBinImg, (sender, e) =>
                {
                    GENREID = ActiveRow.Cells[0].Value.ToString();
                    FunSQL.SQLDML("SQLRecordGenre0021", Record.SQLRecordGenre0021, new string[] { "@VISIBLESTATUS", "@GENREID" }, new string[] { "0", GENREID });
                    DataLoad();
                    ThisApplicationCleaning.FormCleaning(tb1, b1);
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg.Rows.Clear();
                string[][] output = null;
                if (RecordGenreSearch == "")
                {
                    output = FunSQL.SQLSELECT("SQLRecordGenre0000", Record.SQLRecordGenre0000, new string[] { }, new string[] { });
                }
                else
                {
                    output = FunSQL.SQLSELECT("SQLRecordGenre0001", Record.SQLRecordGenre0001, new string[] { "@GENRENAME" }, new string[] { $"%{RecordGenreSearch}%" });
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                for (int i = 0; i < output.Length; i++)
                {
                    dg.Rows.Add(
                        output[i][0],
                        "",
                        output[i][1],
                        output[i][2]
                        );
                }
                dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private void RecordGenre_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(p2, tb1, b1);
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
            if (b1.Text == "確定" && GENREID == "")
            {
                FunSQL.SQLDML("SQLRecordGenre0010", Record.SQLRecordGenre0010, new string[] { "@GENRENAME" }, new string[] { tb1.Text });
            }
            else if (b1.Text == "更新" && GENREID != "")
            {
                FunSQL.SQLDML("SQLRecordGenre0020", Record.SQLRecordGenre0020, new string[] { "@GENRENAME", "@GENREID" }, new string[] { tb1.Text, GENREID });
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
