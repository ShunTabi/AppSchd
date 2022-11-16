using System;
using System.Drawing;
using System.Windows.Forms;

namespace Note
{
    public partial class NoteRecord : UserControl
    {
        public NoteRecord()
        {
            InitializeComponent();
        }
        public static Label l10;
        public static Label l11;
        public static DataGridView dg1;
        public static DataGridView dg2;
        public static DataGridViewRow ActiveRow1 = null;
        public static DataGridViewRow ActiveRow2 = null;
        public static int NoteRecordSearch0 = 1;
        public static string NoteRecordSearch1 = DateTime.Now.ToString("yyyy-MM-dd");
        public static string NOTETITLEID = "";
        public static string SOURCEID = "";
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p3, Panel p5, TextBox tb1, TextBox tb2, Button b1)
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
                dg1 = new Note.DataGridViewEx
                {
                    ColumnHeadersDefaultCellStyle = dgCellStyle1,
                    DefaultCellStyle = dgCellStyle2,
                    Dock = DockStyle.Fill,
                    EnableHeadersVisualStyles = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    ColumnHeadersVisible = false,
                    ScrollBars = ScrollBars.Vertical,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.LightSteelBlue,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                    BorderStyle = BorderStyle.None,
                    MultiSelect = false,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                };
                dg2 = new Note.DataGridViewEx
                {
                    ColumnHeadersDefaultCellStyle = dgCellStyle1,
                    DefaultCellStyle = dgCellStyle2,
                    Dock = DockStyle.Fill,
                    EnableHeadersVisualStyles = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    ColumnHeadersVisible = false,
                    ScrollBars = ScrollBars.Vertical,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.LightSteelBlue,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeColumns = false,
                    AllowUserToResizeRows = false,
                    AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                    BorderStyle = BorderStyle.None,
                    MultiSelect = false,
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                };
                string[] ColumnNames = { "NOTETITLEID", "SOURCEID", "KINDID", "NOTEVERSION", "タイトル", "ノート", "日付" };
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
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow1, tb1, tb2, b1);
                };
                dg2.CellClick += (sender, e) =>
                {
                    if (dg2.SelectedRows.Count == 0) { return; }
                    if (ActiveRow2 != null) { ActiveRow2.ContextMenuStrip = null; }
                    ActiveRow2 = dg2.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow2, tb1, tb2, b1);
                };
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    if (i == 5)
                    {
                        continue;
                    }
                    dg1.Columns[i].Visible = false;
                    dg2.Columns[i].Visible = false;
                }
                dg1.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dg2.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                p3.Controls.Add(dg1);
                p5.Controls.Add(dg2);
            }
            public static void ThisApplicationExec(Panel p3, Panel p5, TextBox tb1, TextBox tb2, Button b1)
            {
                CreateDataGridView(p3, p5, tb1, tb2, b1);
            }
        }
        class ThisApplicationCleaning
        {
            public static void FormCleaning(TextBox tb1, TextBox tb2, Button b1)
            {
                tb1.Text = "";
                tb2.Text = "";
                b1.Text = "確定";
                NOTETITLEID = "";
                SOURCEID = "";
            }
        }
        class ThisApplicationLoad
        {
            public static void CreateContextMenuStrip(DataGridViewRow ActiveRow, TextBox tb1, TextBox tb2, Button b1)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Note.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                contextMenuStrip.Items.Add("更新", Note.UpdateImg, (sender, e) =>
                {
                    NOTETITLEID = ActiveRow.Cells[0].Value.ToString();
                    SOURCEID = ActiveRow.Cells[1].Value.ToString();
                    string[][] output = FunSQL.SQLSELECT("SQLNote0007", Note.SQLNote0007, new string[] { "@NOTETITLEID" }, new string[] { NOTETITLEID });
                    string[] subBox = new string[] { };
                    for (int i = 0; i < output.Length; i++)
                    {
                        if (i == 0)
                        {
                            subBox = output[i];
                        }
                        else
                        {
                            subBox[2] = $"{subBox[2]}{output[i][2]}";
                        }
                    }
                    tb1.Text = subBox[1];
                    tb2.Text = subBox[2];
                    b1.Text = "更新";
                });
                contextMenuStrip.Items.Add("ノート表示", Note.NoteContentImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                    NOTETITLEID = ActiveRow.Cells[0].Value.ToString();
                    SOURCEID = ActiveRow.Cells[1].Value.ToString();
                    string[][] output = FunSQL.SQLSELECT("SQLNote0007", Note.SQLNote0007, new string[] { "@NOTETITLEID" }, new string[] { NOTETITLEID });
                    string[] subBox = new string[] { };
                    for (int i = 0; i < output.Length; i++)
                    {
                        if (i == 0)
                        {
                            subBox = output[i];
                        }
                        else
                        {
                            subBox[2] = $"{subBox[2]}{output[i][2]}";
                        }
                    }
                    Note.NoteContentInstance = new NoteContent(subBox[1], subBox[0], subBox[2]);
                    Note.NoteContentInstance.Show();
                    Note.NoteContentInstance.Location = new Point(
                        int.Parse(string.Format("{0}", SubLocation[0])),
                        int.Parse(string.Format("{0}", SubLocation[1]))
                        );
                });
                ActiveRow.ContextMenuStrip = contextMenuStrip;
            }
            public static void DataLoad()
            {
                dg1.Rows.Clear();
                dg2.Rows.Clear();
                string[][] output = null;
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dg2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                string date1 = "";
                string date2 = "";
                l10.Text = "";
                l11.Text = "";
                if(NoteRecordSearch0 == 0)
                {
                    output = FunSQL.SQLSELECT("SQLNote0001", Note.SQLNote0001, new string[] { "@NOTEDATE" }, new string[] { DateTime.Parse(NoteRecordSearch1).ToString("yyyy-MM-dd") + " 23:59:59" });
                }
                else if (NoteRecordSearch0 == 1)
                {
                    output = FunSQL.SQLSELECT("SQLNote0000", Note.SQLNote0000, new string[] { "@NOTEDATE" }, new string[] { DateTime.Parse(NoteRecordSearch1).ToString("yyyy-MM-dd") + " 23:59:59" });
                }
                string[] subBox = new string[] { };
                for (int i = 0; i < output.Length; i++)
                {
                    if (i == 0)
                    {
                        date1 = output[i][6];
                        l10.Text = "▼" + date1;
                    }
                    else if (date1 != output[i][6])
                    {
                        if (date2 == "")
                        {
                            date2 = output[i][6];
                        }
                        l11.Text = "▼" + date2;
                    }
                    if (output[i][7] == "1")
                    {
                        subBox = output[i];
                        subBox[5] = $"【{output[i][4]}】\n{output[i][5]}";
                    }
                    else if (output[i][7] != "1")
                    {
                        subBox[5] = $"{subBox[5]}{output[i][5]}";
                    }
                    string thisDate = output[i][6];
                    if (output[i][8] == "1")
                    {
                        if (thisDate == date1)
                        {
                            dg1.Rows.Add(
                                    subBox[0],
                                    subBox[1],
                                    subBox[2],
                                    subBox[3],
                                    subBox[4],
                                    subBox[5],
                                    subBox[6]
                                );
                        }
                        else if (thisDate == date2)
                        {
                            dg2.Rows.Add(
                                    subBox[0],
                                    subBox[1],
                                    subBox[2],
                                    subBox[3],
                                    subBox[4],
                                    subBox[5],
                                    subBox[6]
                                );
                        }
                        subBox = new string[] { };
                    }
                }
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dg2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
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
            string NOTETEXT = tb2.Text;
            int stringLength = 70;
            if (tb1.Text == "" || tb2.Text == "") { return; }
            if (b1.Text == "確定" && NOTETITLEID == "" && SOURCEID == "")
            {
                if (FunSQL.SQLSELECT("SQLNote0006", Note.SQLNote0006, new string[] { "@NOTETITLE", "@NOTEDATE" }, new string[] { tb1.Text, DateTime.Now.ToString("yyyy-MM-dd") + "%" }).Length > 0) { return; }
                string[] sqlcode = new string[] { "SQLNote0010" };
                string[] sql = new string[] { Note.SQLNote0010 };
                string[][] parameters = new string[][] {
                    new string[] { "@SOURCEID", "@KINDID", "@NOTETITLE", "@NOTETYPE", "@NOTEVERSION", "@NOTEDATE" },
                };
                string[][] values = new string[][] {
                    new string[] { "0", "--", tb1.Text, "0", "0",DateTime.Now.ToString("yyyy-MM-dd 00:00:00") },
                };
                for (int i = 0; NOTETEXT.Length > 0; i++)
                {
                    if (NOTETEXT.Length > stringLength)
                    {
                        Array.Resize(ref sqlcode, sqlcode.Length + 1);
                        sqlcode[sqlcode.Length - 1] = "SQLNote0011";
                        Array.Resize(ref sql, sql.Length + 1);
                        sql[sql.Length - 1] = Note.SQLNote0011;
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = new string[] { "@NOTETITLE", "@NOTEDATE", "@ROWID", "@NOTETEXT", "@FIN" };
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = new string[] { tb1.Text, DateTime.Now.ToString("yyyy-MM-dd") + "%", (i + 1).ToString(), NOTETEXT.Substring(0, stringLength), "0" };
                        NOTETEXT = NOTETEXT.Substring(stringLength, NOTETEXT.Length - stringLength);
                    }
                    else if (NOTETEXT.Length <= stringLength)
                    {
                        Array.Resize(ref sqlcode, sqlcode.Length + 1);
                        sqlcode[sqlcode.Length - 1] = "SQLNote0011";
                        Array.Resize(ref sql, sql.Length + 1);
                        sql[sql.Length - 1] = Note.SQLNote0011;
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = new string[] { "@NOTETITLE", "@NOTEDATE", "@ROWID", "@NOTETEXT", "@FIN" };
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = new string[] { tb1.Text, DateTime.Now.ToString("yyyy-MM-dd") + "%", (i + 1).ToString(), NOTETEXT, "1" };
                        NOTETEXT = "";
                    }
                }
                FunSQL.SQLDML2(sqlcode, sql, parameters, values);
            }
            else if (b1.Text == "更新" && NOTETITLEID != "" && SOURCEID != "")
            {
                string thisSOURCEID = SOURCEID;
                if (thisSOURCEID == "0")
                {
                    thisSOURCEID = NOTETITLEID;
                }
                string[] sqlcode = null;
                string[] sql = null;
                string[][] parameters = null;
                string[][] values = null;
                if (FunSQL.SQLSELECT("SQLNote0008", Note.SQLNote0008, new string[] { "@SOURCEID", "@NOTEDATE" }, new string[] { thisSOURCEID, DateTime.Now.ToString("yyyy-MM-dd") + "%" }).Length > 0)
                {
                    sqlcode = new string[] { "SQLNote0021", "SQLNote0030" };
                    sql = new string[] { Note.SQLNote0021, Note.SQLNote0030 };
                    parameters = new string[][] {
                        new string[] { "@NOTETITLE", "@NOTETITLEID"},
                        new string[] { "@NOTETITLEID" },
                    };
                    values = new string[][] {
                        new string[] { tb1.Text,NOTETITLEID },
                        new string[] { NOTETITLEID },
                    };
                }
                else
                {
                    sqlcode = new string[] { "SQLNote0020", "SQLNote0010" };
                    sql = new string[] { Note.SQLNote0020, Note.SQLNote0010 };
                    parameters = new string[][] {
                        new string[] { "@SOURCEID", "@NOTETITLEID","@NOTETITLE" },
                        new string[] { "@SOURCEID", "@KINDID", "@NOTETITLE", "@NOTETYPE", "@NOTEVERSION", "@NOTEDATE" },
                    };
                    values = new string[][] {
                        new string[] { thisSOURCEID,NOTETITLEID,tb1.Text },
                        new string[] { thisSOURCEID, "--", tb1.Text, "0", "0",DateTime.Now.ToString("yyyy-MM-dd 00:00:00") },
                    };
                }
                for (int i = 0; NOTETEXT.Length > 0; i++)
                {
                    if (NOTETEXT.Length > stringLength)
                    {
                        Array.Resize(ref sqlcode, sqlcode.Length + 1);
                        sqlcode[sqlcode.Length - 1] = "SQLNote0011";
                        Array.Resize(ref sql, sql.Length + 1);
                        sql[sql.Length - 1] = Note.SQLNote0011;
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = new string[] { "@NOTETITLE", "@NOTEDATE", "@ROWID", "@NOTETEXT", "@FIN" };
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = new string[] { tb1.Text, DateTime.Now.ToString("yyyy-MM-dd") + "%", (i + 1).ToString(), NOTETEXT.Substring(0, stringLength), "0" };
                        NOTETEXT = NOTETEXT.Substring(stringLength, NOTETEXT.Length - stringLength);
                    }
                    else if (NOTETEXT.Length <= stringLength)
                    {
                        Array.Resize(ref sqlcode, sqlcode.Length + 1);
                        sqlcode[sqlcode.Length - 1] = "SQLNote0011";
                        Array.Resize(ref sql, sql.Length + 1);
                        sql[sql.Length - 1] = Note.SQLNote0011;
                        Array.Resize(ref parameters, parameters.Length + 1);
                        parameters[parameters.Length - 1] = new string[] { "@NOTETITLE", "@NOTEDATE", "@ROWID", "@NOTETEXT", "@FIN" };
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = new string[] { tb1.Text, DateTime.Now.ToString("yyyy-MM-dd") + "%", (i + 1).ToString(), NOTETEXT, "1" };
                        NOTETEXT = "";
                    }
                }
                FunSQL.SQLDML2(sqlcode, sql, parameters, values);
            }
            ThisApplicationLoad.DataLoad();
            ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
        }
        private void NoteOneDay_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationExec(p3, p5, tb1, tb2, b1);
            l10 = new Label();
            l11 = new Label();
            l10.Text = "";
            l11.Text = "";
            l10.Dock = DockStyle.Top;
            l11.Dock = DockStyle.Top;
            p3.Controls.Add(l10);
            p5.Controls.Add(l11);
            ThisApplicationLoad.DataLoad();
        }
        private void NoteOneDay_Resize(object sender, EventArgs e)
        {
            int MAXSIZE = 720;
            int MINISIZE = 200;
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
        private void tb2_Enter(object sender, EventArgs e)
        {
            p1.Width = 760;
            tb2.Width = 720;
        }
        private void tb2_Leave(object sender, EventArgs e)
        {
            p1.Width = 300;
            tb2.Width = 250;
        }
    }
}
