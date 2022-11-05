using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record
{
    public class Record
    {
        private static string SQLLimit = FunFile.GetString(FunFile.iniAppSchd, "[DB]", "SQLLimit")[0];
        private static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
        public static string SQLRecordGenre0000 = $"SELECT GENREID,GENRENAME,GENREUPDATEDATE FROM T_GENRE WHERE GENREVISIBLESTATUS=1 ORDER BY GENRENAME,GENREUPDATEDATE DESC LIMIT {SQLLimit}";
        public static string SQLRecordGenre0001 = $"SELECT GENREID,GENRENAME,GENREUPDATEDATE FROM T_GENRE WHERE GENREVISIBLESTATUS=1 AND GENRENAME LIKE @GENRENAME ORDER BY GENRENAME,GENREUPDATEDATE DESC LIMIT {SQLLimit}";
        public const string SQLRecordGenre0002 = "SELECT GENREID,GENRENAME FROM T_GENRE WHERE GENREVISIBLESTATUS=1 ORDER BY GENRENAME,GENREUPDATEDATE DESC";
        public static string SQLRecordGenre0003 = "SELECT GENRENAME FROM T_GENRE WHERE GENREID=@GENREID";
        public static string SQLRecordGenre0010 = $"INSERT INTO T_GENRE(GENRENAME,GENREUPDATEDATE) VALUES(@GENRENAME,{now})";
        public static string SQLRecordGenre0020 = $"UPDATE T_GENRE SET GENRENAME=@GENRENAME,GENREUPDATEDATE={now} WHERE GENREID=@GENREID";
        public const string SQLRecordGenre0021 = "UPDATE T_GENRE SET GENREVISIBLESTATUS=@VISIBLESTATUS WHERE GENREID=@GENREID";
        //public const string SQLRecordGenre0030 = "DELETE FROM T_GENRE WHERE GENREID = @GENREID";
        //
        public static string SQLRecordGoal0000 = $"SELECT GOALID,GENRENAME,GOALNAME,GOALUPDATEDATE FROM T_GOAL " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 ORDER BY GOALNAME,GOALUPDATEDATE DESC LIMIT {SQLLimit}";
        public static string SQLRecordGoal0001 = $"SELECT GOALID,GENRENAME,GOALNAME,GOALUPDATEDATE FROM T_GOAL " +
            $"INNER JOIN T_GENRE USING(GENREID) " +
            $"WHERE GENREVISIBLESTATUS=1 AND GOALVISIBLESTATUS=1 AND GOALNAME LIKE @GOALNAME ORDER BY GOALNAME,GOALUPDATEDATE DESC LIMIT {SQLLimit}";
        public const string SQLRecordGoal0002 = "SELECT GOALID,GOALNAME FROM T_GOAL WHERE GOALVISIBLESTATUS=1 ORDER BY GOALNAME,GOALUPDATEDATE DESC";
        public static string SQLRecordGoal0003 = "SELECT GENRENAME,GOALNAME FROM T_GOAL INNER JOIN T_GENRE USING(GENREID) WHERE GOALID=@GOALID";
        public static string SQLRecordGoal0010 = $"INSERT INTO T_GOAL(GENREID,GOALNAME,GOALUPDATEDATE) VALUES(@GENREID,@GOALNAME,{now})";
        public static string SQLRecordGoal0020 = $"UPDATE T_GOAL SET GENREID=@GENREID,GOALNAME=@GOALNAME,GOALUPDATEDATE={now} WHERE GOALID=@GOALID";
        public const string SQLRecordGoal0021 = "UPDATE T_GOAL SET GOALVISIBLESTATUS=@VISIBLESTATUS WHERE GOALID=@GOALID";
        //public const string SQLRecordGoal0030 = "DELETE FROM T_GOAL WHERE GOALID = @GOALID";
        //
        public static string SQLRecordPlan0000 = $"SELECT PLANID,GOALNAME,PLANNAME,PRIORSUBNAME,STATUSNAME,PLANSTARTDATE,PLANENDDATE,PLANUPDATEDATE,ALERTSTATUS FROM T_PLAN " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_PRIOR ON T_PLAN.PRIORID = T_PRIOR.PRIORID " +
            $"INNER JOIN T_STATUS ON T_PLAN.STATUSID = T_STATUS.STATUSID " +
            $"LEFT OUTER JOIN (SELECT PLANID,'！' AS ALERTSTATUS FROM T_PLAN WHERE STATUSID in (1,2) AND PLANENDDATE<{now}) USING(PLANID) " +
            $"WHERE GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 " +
            $"ORDER BY T_PLAN.PRIORID ASC,T_PLAN.STATUSID ASC,PLANENDDATE ASC,GOALNAME,PLANNAME,PLANUPDATEDATE DESC LIMIT {SQLLimit}";
        public static string SQLRecordPlan0001 = $"SELECT PLANID,GOALNAME,PLANNAME,PRIORSUBNAME,STATUSNAME,PLANSTARTDATE,PLANENDDATE,PLANUPDATEDATE,ALERTSTATUS FROM T_PLAN " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_PRIOR ON T_PLAN.PRIORID = T_PRIOR.PRIORID " +
            $"INNER JOIN T_STATUS ON T_PLAN.STATUSID = T_STATUS.STATUSID " +
            $"LEFT OUTER JOIN (SELECT PLANID,'！' AS ALERTSTATUS FROM T_PLAN WHERE STATUSID in (1,2) AND PLANENDDATE<{now}) USING(PLANID) " +
            $"WHERE GOALVISIBLESTATUS=1 AND PLANVISIBLESTATUS=1 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) " +
            $"ORDER BY T_PLAN.PRIORID ASC,T_PLAN.STATUSID ASC,PLANENDDATE ASC,GOALNAME,PLANNAME,PLANUPDATEDATE DESC LIMIT {SQLLimit}";
        public static string SQLRecordPlan0002 = "SELECT GOALNAME,PLANNAME,PRIORNAME,PLANSTARTDATE,PLANENDDATE FROM T_PLAN " +
            "INNER JOIN T_GOAL USING(GOALID) " +
            "INNER JOIN T_PRIOR USING(PRIORID) WHERE PLANID=@PLANID";
        public static string SQLRecordPlan0010 = $"INSERT INTO T_PLAN(GOALID, PLANNAME,PRIORID,PLANSTARTDATE,PLANENDDATE,PLANUPDATEDATE) VALUES(@GOALID, @PLANNAME,@PRIORID,@PLANSTARTDATE,@PLANENDDATE,{now})";
        public static string SQLRecordPlan0020 = $"UPDATE T_PLAN SET GOALID=@GOALID,PLANNAME=@PLANNAME,PRIORID=@PRIORID,PLANSTARTDATE=@PLANSTARTDATE,PLANENDDATE=@PLANENDDATE,PLANUPDATEDATE={now} WHERE PLANID=@PLANID";
        public const string SQLRecordPlan0021 = "UPDATE T_PLAN SET PLANVISIBLESTATUS=@VISIBLESTATUS WHERE PLANID=@PLANID";
        public const string SQLRecordPlan0022 = "UPDATE T_PLAN SET STATUSID=@STATUSID WHERE PLANID=@PLANID";
        public const string SQLRecordPlan0023 = "UPDATE T_PLAN SET PRIORID=@PRIORID WHERE PLANID=@PLANID";

        /*
            public const string AppSchedule000230 = "DELETE FROM T_PLAN WHERE PLANID = @PLANID";
         */
        public static Image UpdateImg = Image.FromFile(@".\resources\png\Update.png");
        public static Image ToRecycleBinImg = Image.FromFile(@".\resources\png\ToRecycleBin.png");
        public static Image ToStorageBinImg = Image.FromFile(@".\resources\png\ToStorageBin.png");
        public static Image AddImg = Image.FromFile(@".\resources\png\Add.png");
        public static Image StatusImg = Image.FromFile(@".\resources\png\Status.png");
        public static RecordSearch RecordSearchInstance = null;
        public class DataGridViewEx : DataGridView
        {
            public DataGridViewEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("Record Loaded!", 1);
            UserControl RecordInstance = new RecordRecord
            {
                Dock = DockStyle.Fill,
            };
            RecordInstance.Disposed += (sender, e) =>
            {
                if (RecordSearchInstance != null && !RecordSearchInstance.IsDisposed)
                {
                    RecordSearchInstance.Close();
                }
            };
            MainPanel.Controls.Add(RecordInstance);
        }
    }
}
