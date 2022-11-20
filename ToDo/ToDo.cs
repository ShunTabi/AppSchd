using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public class ToDo
    {
        private static string SQLLimit = FunFile.GetString(FunFile.iniAppSchd, "[DB]", "SQLLimit")[0];
        private static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
        public static string SQLToDo0000 = $"" +
            $"SELECT TODOID,ALERTSTATUS,GOALNAME,PLANNAME,TODONAME,STATUSNAME,strftime('%Y-%m-%d',TODOENDDATE) AS TODOENDDATE,strftime('%Y-%m-%d',TODOUPDATEDATE) AS TODOUPDATEDATE FROM T_TODO " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"LEFT OUTER JOIN(SELECT TODOID,'！' AS ALERTSTATUS FROM T_TODO WHERE STATUSID in (1,2) AND TODOENDDATE<{now}) USING(TODOID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 AND TODOVISIBLESTATUS=1 " +
            $"ORDER BY T_TODO.STATUSID ASC,TODOENDDATE,GOALNAME,PLANNAME,TODOUPDATEDATE LIMIT {SQLLimit}";
        public static string SQLToDo0001 = $"" +
            $"SELECT TODOID,ALERTSTATUS,GOALNAME,PLANNAME,TODONAME,STATUSNAME,strftime('%Y-%m-%d',TODOENDDATE) AS TODOENDDATE,strftime('%Y-%m-%d',TODOUPDATEDATE) AS TODOUPDATEDATE FROM T_TODO " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"LEFT OUTER JOIN(SELECT TODOID,'！' AS ALERTSTATUS FROM T_TODO WHERE STATUSID in (1,2) AND TODOENDDATE<{now}) USING(TODOID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 AND TODOVISIBLESTATUS=1 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) " +
            $"ORDER BY T_TODO.STATUSID,TODOENDDATE,GOALNAME,PLANNAME LIMIT {SQLLimit}";
        public static string SQLToDo0002 = $"" +
            $"SELECT TODOID,GOALNAME,PLANNAME,TODONAME,STATUSNAME,strftime('%Y-%m-%d',TODOENDDATE) AS TODOENDDATE,strftime('%Y-%m-%d',TODOUPDATEDATE) AS TODOUPDATEDATE FROM T_TODO " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 AND TODOVISIBLESTATUS=1 AND T_TODO.STATUSID=3 " +
            $"ORDER BY TODOENDDATE,GOALNAME,PLANNAME LIMIT {SQLLimit}";
        public static string SQLToDo0003 = $"" +
            $"SELECT TODOID,GOALNAME,PLANNAME,TODONAME,STATUSNAME,strftime('%Y-%m-%d',TODOENDDATE) AS TODOENDDATE,strftime('%Y-%m-%d',TODOUPDATEDATE) AS TODOUPDATEDATE FROM T_TODO " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 AND TODOVISIBLESTATUS=1 AND T_TODO.STATUSID =3 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) " +
            $"ORDER BY TODOENDDATE,GOALNAME,PLANNAME LIMIT {SQLLimit}";
        public static string SQLToDo0004 = $"" +
            $"SELECT GOALNAME,PLANNAME,TODONAME,strftime('%Y-%m-%d',TODOENDDATE) AS TODOENDDATE FROM T_TODO " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) WHERE TODOID = @TODOID ";
        public static string SQLToDo0010 = $"" +
            $"INSERT INTO T_TODO(PLANID,TODONAME,STATUSID,TODOENDDATE,TODOUPDATEDATE) VALUES(@PLANID,@TODONAME,@STATUSID,@TODOENDDATE,{now})";
        public static string SQLToDo0020 = $"" +
            $"UPDATE T_TODO SET PLANID=@PLANID,TODONAME=@TODONAME,TODOENDDATE=@TODOENDDATE,TODOUPDATEDATE={now} WHERE TODOID=@TODOID";
        public static string SQLToDo0021 = "" +
            $"UPDATE T_TODO SET TODOVISIBLESTATUS=@VISIBLESTATUS,TODOUPDATEDATE={now} WHERE TODOID=@TODOID";
        public static string SQLToDo0022 = "" +
            $"UPDATE T_TODO SET STATUSID=@STATUSID,TODOUPDATEDATE={now} WHERE TODOID=@TODOID";
        public static Image UpdateImg = Image.FromFile(@".\resources\png\Update.png");
        public static Image ToRecycleBinImg = Image.FromFile(@".\resources\png\ToRecycleBin.png");
        public static Image ToStorageBinImg = Image.FromFile(@".\resources\png\ToStorageBin.png");
        public static Image AddImg = Image.FromFile(@".\resources\png\Add.png");
        public static Image StatusImg = Image.FromFile(@".\resources\png\Status.png");
        public static ToDoSearch ToDoSearchInstance = null;
        public class DataGridViewEx : DataGridView
        {
            public DataGridViewEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("ToDo Loaded!", 1);
            UserControl ToDoToDo = new ToDoToDo
            {
                Dock = DockStyle.Fill,
            };
            ToDoToDo.Disposed += (sender, e) =>
            {
                if (ToDoSearchInstance == null || ToDoSearchInstance.IsDisposed) { return; }
                ToDoSearchInstance.Close();
            };
            MainPanel.Controls.Add(ToDoToDo);
        }
    }
}
