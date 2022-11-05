using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bin
{
    public partial class BinStorage : UserControl
    {
        public BinStorage()
        {
            InitializeComponent();
        }
        public static DataGridView dg1;
        public static DataGridView dg2;
        public static DataGridViewRow ActiveRow1 = null;
        public static DataGridViewRow ActiveRow2 = null;
        public static string BinStorageSearch = "";
        public static string BINID = "";
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p1, Panel p2)
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
                dg1 = new Bin.DataGridViewEx
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
                dg2 = new Bin.DataGridViewEx
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
                string[] ColumnNames = { "KEY", "", "項目", "内容", "備考１", "備考２", "更新日" };
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    dg1.Columns.Add(i.ToString(), ColumnNames[i]);
                    dg2.Columns.Add(i.ToString(), ColumnNames[i]);
                    dg1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dg2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dg1.CellClick += (sender, e) =>
                {
                    if (dg1.SelectedRows.Count == 0) { return; }
                    if (ActiveRow1 != null) { ActiveRow1.ContextMenuStrip = null; }
                    ActiveRow1 = dg1.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow1, "収納箱");
                };
                dg2.CellClick += (sender, e) =>
                {
                    if (dg2.SelectedRows.Count == 0) { return; }
                    if (ActiveRow2 != null) { ActiveRow2.ContextMenuStrip = null; }
                    ActiveRow2 = dg2.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow2, "ゴミ箱");
                };
                dg1.Columns[0].Visible = false;
                dg2.Columns[0].Visible = false;
                p1.Controls.Add(dg1);
                p2.Controls.Add(dg2);
            }
            public static void ThisApplicationSetupExec(Panel p1, Panel p2)
            {
                CreateDataGridView(p1, p2);
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, string mode)
            {
                string[] KeyInfo = ActiveRow.Cells[0].Value.ToString().Split('^');
                string KeyName = KeyInfo[0];
                BINID = KeyInfo[1];
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("元に戻す", Bin.StatusImg, (sender, e) =>
                {
                    string sql = "";
                    if (KeyName == "GENRE")
                    {
                        sql = "UPDATE T_GENRE SET GENREVISIBLESTATUS=@VISIBLESTATUS WHERE GENREID=@BINID";
                    }
                    else if (KeyName == "GOAL")
                    {
                        sql = "UPDATE T_GOAL SET GOALVISIBLESTATUS=@VISIBLESTATUS WHERE GOALID=@BINID";
                    }
                    else if (KeyName == "PLAN")
                    {
                        sql = "UPDATE T_PLAN SET PLANVISIBLESTATUS=@VISIBLESTATUS WHERE PLANID=@BINID";
                    }
                    else if (KeyName == "TODO")
                    {
                        sql = "UPDATE T_TODO SET TODOVISIBLESTATUS=@VISIBLESTATUS WHERE TODOID=@BINID";
                    }
                    else if (KeyName == "SCHEDULE")
                    {
                        sql = "UPDATE T_SCHEDULE SET SCHEDULEVISIBLESTATUS=@VISIBLESTATUS WHERE SCHEDULEID=@BINID";
                    }
                    FunSQL.SQLDML($"***BIN-{KeyName}***", sql, new string[] { "@VISIBLESTATUS", "@BINID" }, new string[] { "1", BINID });
                    DataLoad();
                });
                if (mode == "ゴミ箱")
                {
                    contextMenuStrip.Items.Add("完全削除", Bin.StatusImg, (sender, e) =>
                    {
                        DialogResult result = MessageBox.Show("完全削除を実施してもよいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        if (result != DialogResult.OK) { return; }
                        string sql = "";
                        if (KeyName == "GENRE")
                        {
                            sql = "DELETE FROM T_GENRE WHERE GENREID = @BINID";
                        }
                        else if (KeyName == "GOAL")
                        {
                            sql = "DELETE FROM T_GOAL WHERE GOALID = @BINID";
                        }
                        else if (KeyName == "PLAN")
                        {
                            sql = "DELETE FROM T_PLAN WHERE PLANID = @BINID";
                        }
                        else if (KeyName == "TODO")
                        {
                            sql = "DELETE FROM T_REVIEW WHERE REVIEWID = @BINID";
                        }
                        else if (KeyName == "SCHEDULE")
                        {
                            sql = "DELETE FROM T_SCHEDULE WHERE SCHEDULEID = @BINID";
                        }
                        FunSQL.SQLDML($"***BIN-{KeyName}***", sql, new string[] { "@BINID" }, new string[] { BINID });
                        DataLoad();
                    });
                }
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg1.Rows.Clear();
                dg2.Rows.Clear();
                string[][] output1 = null;
                string[][] output2 = null;
                if (BinStorageSearch == "")
                {
                    output1 = FunSQL.SQLSELECT("SQLBin0000", Bin.SQLBin0000, new string[] { "@VISIBLESTATUS" }, new string[] { "2" });
                    output2 = FunSQL.SQLSELECT("SQLBin0000", Bin.SQLBin0000, new string[] { "@VISIBLESTATUS" }, new string[] { "0" });
                }
                else
                {
                    output1 = FunSQL.SQLSELECT("SQLBin0001", Bin.SQLBin0001, new string[] { "@VISIBLESTATUS", "@KEYWORD" }, new string[] { "2", $"%{BinStorageSearch}%" });
                    output2 = FunSQL.SQLSELECT("SQLBin0001", Bin.SQLBin0001, new string[] { "@VISIBLESTATUS", "@KEYWORD" }, new string[] { "0", $"%{BinStorageSearch}%" });
                }
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dg1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dg2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dg2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                for (int i = 0; i < output1.Length; i++)
                {
                    dg1.Rows.Add(
                        output1[i][0],
                        "",
                        output1[i][1],
                        output1[i][2],
                        output1[i][3],
                        output1[i][4],
                        DateTime.Parse(output1[i][5]).ToString("yyyy-MM-dd")
                        );
                }
                for (int i = 0; i < output2.Length; i++)
                {
                    dg2.Rows.Add(
                        output2[i][0],
                        "",
                        output2[i][1],
                        output2[i][2],
                        output2[i][3],
                        output2[i][4],
                        DateTime.Parse(output2[i][5]).ToString("yyyy-MM-dd")
                        );
                }
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dg2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private void BinStorage_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(p2, p3);
            ThisApplicationLoad.DataLoad();
            dg1.BringToFront();
            dg2.BringToFront();
            this.Disposed += (sender1, e1) =>
            {
                if (Bin.BinSearchInstance != null && !Bin.BinSearchInstance.IsDisposed)
                {
                    Bin.BinSearchInstance.Close();
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
        private void BinStorage_Resize(object sender, EventArgs e)
        {
            int MAXSIZE = 720;
            int MINISIZE = 500;
            int thiSize = (this.Width - p1.Width) / 51 * 25;
            if (thiSize > MAXSIZE)
            {
                p2.Width = MAXSIZE;
                p4.Width = MAXSIZE;
            }
            else if (thiSize < MINISIZE)
            {
                p2.Width = MINISIZE;
                p4.Width = MINISIZE;
            }
            else
            {
                p2.Width = thiSize;
                p4.Width = thiSize;
            }
        }
    }
}
