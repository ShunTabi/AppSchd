using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note
{
    public class Note
    {
        private static string SQLLimit = "100";//FunFile.GetString(FunFile.iniAppSchd, "[DB]", "SQLLimit")[0];
        private static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
        public static string SQLNote0000 = "" +
            "SELECT NOTETITLEID,SOURCEID,KINDID,T_NOTETITLE.NOTEVERSION,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            "INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE T_NOTETITLE.NOTEVERSION = '0' AND NOTEDATE IN (SELECT NOTEDATE FROM T_NOTETITLE WHERE NOTEVERSION='0' " +
            "GROUP BY strftime('%Y-%m-%d',NOTEDATE) HAVING NOTEDATE<=@NOTEDATE ORDER BY NOTEDATE DESC LIMIT 2) ORDER BY NOTEDATE ASC,NOTETITLEID,ROWID";
        public static string SQLNote0001 = "" +
            "SELECT NOTETITLEID,SOURCEID,KINDID,T_NOTETITLE.NOTEVERSION,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            "INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE NOTEDATE IN (SELECT NOTEDATE FROM T_NOTETITLE GROUP BY strftime('%Y-%m-%d',NOTEDATE) HAVING NOTEDATE<=@NOTEDATE ORDER BY NOTEDATE DESC LIMIT 2) " +
            "ORDER BY NOTEDATE ASC,NOTETITLEID,ROWID";
        public static string SQLNote0002 = $"" +
            $"SELECT NOTETITLEID,SOURCEID,KINDID,'★最新' AS NOTEVERSION,CASE WHEN NOTETYPE='0' THEN '【ノート】' WHEN NOTETYPE='1' THEN '【履歴】' END,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            $"INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE T_NOTETITLE.NOTEVERSION = '0' ORDER BY NOTEDATE DESC,NOTETITLEID,ROWID LIMIT {SQLLimit}";
        public static string SQLNote0003 = $"" +
            $"SELECT NOTETITLEID,SOURCEID,KINDID,'★最新' AS NOTEVERSION,CASE WHEN NOTETYPE='0' THEN '【ノート】' WHEN NOTETYPE='1' THEN '【履歴】' END,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            $"INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE T_NOTETITLE.NOTEVERSION = '0' AND NOTETITLEID IN (SELECT DISTINCT NOTETITLEID FROM T_NOTETEXT WHERE NOTETEXT LIKE @KEYWORD) ORDER BY NOTEDATE DESC,NOTETITLEID,ROWID LIMIT {SQLLimit}";
        public static string SQLNote0004 = $"" +
            $"SELECT NOTETITLEID,SOURCEID,KINDID,CASE WHEN T_NOTETITLE.NOTEVERSION='0' THEN '★最新' ELSE T_NOTETITLE.NOTEVERSION END AS NOTEVERSION,CASE WHEN NOTETYPE='0' THEN '【ノート】' WHEN NOTETYPE='1' THEN '【履歴】' END AS NOTETYPE,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            $"INNER JOIN T_NOTETEXT USING (NOTETITLEID) ORDER BY NOTEDATE DESC,NOTETITLEID,ROWID LIMIT {SQLLimit}";
        public static string SQLNote0005 = $"" +
            $"SELECT NOTETITLEID,SOURCEID,KINDID,CASE WHEN T_NOTETITLE.NOTEVERSION='0' THEN '★最新' ELSE T_NOTETITLE.NOTEVERSION END AS NOTEVERSION,CASE WHEN NOTETYPE='0' THEN '【ノート】' WHEN NOTETYPE='1' THEN '【履歴】' END AS NOTETYPE,NOTETITLE,NOTETEXT,strftime('%Y-%m-%d',NOTEDATE) AS NOTEDATE,ROWID,FIN FROM T_NOTETITLE " +
            $"INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE NOTETITLEID IN (SELECT DISTINCT NOTETITLEID FROM T_NOTETEXT WHERE NOTETEXT LIKE @KEYWORD) ORDER BY NOTEDATE DESC,NOTETITLEID,ROWID LIMIT {SQLLimit}";
        public static string SQLNote0006 = "" +
            "SELECT * FROM T_NOTETITLE INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE NOTETITLE=@NOTETITLE AND NOTEDATE LIKE @NOTEDATE";
        public static string SQLNote0007 = "" +
            "SELECT NOTEDATE,NOTETITLE,NOTETEXT,ROWID,FIN FROM T_NOTETITLE INNER JOIN T_NOTETEXT USING (NOTETITLEID) WHERE T_NOTETITLE.NOTEVERSION = 0 AND (SOURCEID=@NOTETITLEID OR NOTETITLEID=@NOTETITLEID) ORDER BY ROWID";
        public static string SQLNote0008 = "" +
            "SELECT * FROM T_NOTETITLE WHERE (SOURCEID=@SOURCEID OR NOTETITLEID=@SOURCEID) AND NOTEDATE LIKE @NOTEDATE";
        public static string SQLNote0010 = $"" +
            $"INSERT INTO T_NOTETITLE(SOURCEID,KINDID,NOTETITLE,NOTETYPE,NOTEVERSION,NOTEDATE,NOTEFIRSTDATE,NOTEUPDATEDATE) VALUES(@SOURCEID,@KINDID,@NOTETITLE,@NOTETYPE,@NOTEVERSION,@NOTEDATE,{now},{now})";
        public static string SQLNote0011 = "" +
            "INSERT INTO T_NOTETEXT(NOTETITLEID,ROWID,NOTETEXT,FIN) VALUES((SELECT NOTETITLEID FROM T_NOTETITLE WHERE NOTETITLE=@NOTETITLE AND NOTEDATE LIKE @NOTEDATE),@ROWID,@NOTETEXT,@FIN)";
        public static string SQLNote0020 = $"" +
            $"UPDATE T_NOTETITLE SET NOTEVERSION=(SELECT COUNT(NOTEVERSION) FROM T_NOTETITLE WHERE NOTETITLEID=@SOURCEID OR SOURCEID=@SOURCEID),SOURCEID=@SOURCEID,NOTETITLE=@NOTETITLE,NOTEUPDATEDATE={now} WHERE NOTETITLEID=@NOTETITLEID";
        public static string SQLNote0021 = $"" +
            $"UPDATE T_NOTETITLE SET NOTETITLE=@NOTETITLE,NOTEUPDATEDATE={now} WHERE NOTETITLEID=@NOTETITLEID";
        public static string SQLNote0030 = $"" +
            $"DELETE FROM T_NOTETEXT WHERE NOTETITLEID=@NOTETITLEID";
        public static Image UpdateImg = Image.FromFile(@".\resources\png\Update.png");
        public static Image ToRecycleBinImg = Image.FromFile(@".\resources\png\ToRecycleBin.png");
        public static Image ToStorageBinImg = Image.FromFile(@".\resources\png\ToStorageBin.png");
        public static Image AddImg = Image.FromFile(@".\resources\png\Add.png");
        public static Image StatusImg = Image.FromFile(@".\resources\png\Status.png");
        public static Image NoteContentImg = Image.FromFile(@".\resources\png\NoteContent.png");
        public static NoteSearch NoteSearchInstance = null;
        public static NoteContent NoteContentInstance = null;
        public class DataGridViewEx : DataGridView
        {
            public DataGridViewEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("Note Loaded!", 1);
            UserControl NoteInstance = new NoteNote()
            {
                Dock = DockStyle.Fill,
            };
            NoteInstance.Disposed += (sender, e) =>
            {
                if (NoteSearchInstance != null && !NoteSearchInstance.IsDisposed)
                {
                    NoteSearchInstance.Close();
                }
                if (NoteContentInstance != null && !NoteContentInstance.IsDisposed)
                {
                    NoteContentInstance.Close();
                }
            };
            MainPanel.Controls.Add(NoteInstance);
        }
    }
}
