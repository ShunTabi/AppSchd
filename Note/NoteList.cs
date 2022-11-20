using System;
using System.Drawing;
using System.Windows.Forms;

namespace Note
{
    public partial class NoteList : UserControl
    {
        public NoteList()
        {
            InitializeComponent();
        }
        public static DataGridView dg1;
        public static DataGridViewRow ActiveRow1 = null;
        public static string NOTETITLEID = "";
        public static string SOURCEID = "";
        public static int NoteListSearch0 = 1;
        public static string NoteListSearch1 = "";
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationSetup
        {
            private static void CreateDataGridView(Panel p2, TextBox tb1, TextBox tb2, Button b1)
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
                string[] ColumnNames = { "NOTETITLEID", "SOURCEID", "KINDID", "NOTEVERSION", "INFO", "ノート" };
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    dg1.Columns.Add(i.ToString(), ColumnNames[i]);
                    dg1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dg1.CellClick += (sender, e) =>
                {
                    if (dg1.SelectedRows.Count == 0) { return; }
                    if (ActiveRow1 != null) { ActiveRow1.ContextMenuStrip = null; }
                    ActiveRow1 = dg1.SelectedRows[0];
                    ThisApplicationLoad.CreateContextMenuStrip(ActiveRow1, tb1, tb2, b1);
                };
                dg1.Columns[0].Visible = false;
                dg1.Columns[1].Visible = false;
                dg1.Columns[2].Visible = false;
                dg1.Columns[3].Visible = false;
                dg1.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dg1.Columns[5].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                p2.Controls.Add(dg1);
            }
            public static void ThisApplicationExec(Panel p2, TextBox tb1, TextBox tb2, Button b1)
            {
                CreateDataGridView(p2, tb1, tb2, b1);
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
                string NOTEVERSION = ActiveRow.Cells[3].Value.ToString();
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("新規", Note.AddImg, (sender, e) =>
                {
                    ThisApplicationCleaning.FormCleaning(tb1, tb2, b1);
                });
                if (NOTEVERSION == "★最新")
                {
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
                }
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
                string[][] output = null;
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                if (NoteListSearch1 == "")
                {
                    if (NoteListSearch0 == 0)
                    {
                        output = FunSQL.SQLSELECT("SQLNote0004", Note.SQLNote0004, new string[] { }, new string[] { });
                    }
                    else if (NoteListSearch0 == 1)
                    {
                        output = FunSQL.SQLSELECT("SQLNote0002", Note.SQLNote0002, new string[] { }, new string[] { });
                    }
                }
                else
                {
                    if (NoteListSearch0 == 0)
                    {
                        output = FunSQL.SQLSELECT("SQLNote0005", Note.SQLNote0005, new string[] { "@KEYWORD" }, new string[] { $"%{NoteListSearch1}%" });
                    }
                    else if (NoteListSearch0 == 1)
                    {
                        output = FunSQL.SQLSELECT("SQLNote0003", Note.SQLNote0003, new string[] { "@KEYWORD" }, new string[] { $"%{NoteListSearch1}%" });
                    }
                }
                string[] subBox = new string[] { };
                for (int i = 0; i < output.Length; i++)
                {
                    if (output[i][8] == "1")
                    {
                        subBox = output[i];
                    }
                    else if (output[i][7] != "1")
                    {
                        subBox[6] = $"{subBox[6]}{output[i][6]}";
                    }
                    if (output[i][9] == "1")
                    {
                        dg1.Rows.Add(
                                subBox[0],
                                subBox[1],
                                subBox[2],
                                subBox[3],
                                $"Ver：{subBox[3]}\n" +
                                $"タイトル：{subBox[5]}\n" +
                                $"種類：{subBox[4]}\n" +
                                $"日付：" + subBox[7],
                                subBox[6]
                            );
                    }
                }
                dg1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dg1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        public class AcessCls
        {
            public static void DataLoad()
            {
                ThisApplicationLoad.DataLoad();
            }
        }
        private void NoteList_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationExec(p2, tb1, tb2, b1);
            ThisApplicationLoad.DataLoad();
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
                string thisSOURCEID = "";
                if (SOURCEID == "0")
                {
                    thisSOURCEID = NOTETITLEID;
                }
                else
                {
                    thisSOURCEID = SOURCEID;
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