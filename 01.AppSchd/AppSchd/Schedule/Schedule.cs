using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public class Schedule
    {
        private static string SQLLimit = FunFile.GetString(FunFile.iniAppSchd, "[DB]", "SQLLimit")[0];
        private static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
        public static string SQLSchedule0000 = $"" +
            $"SELECT SCHEDULEID,ALERTSTATUS,GOALNAME,PLANNAME,STATUSNAME,strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME,SCHEDULEHOURS,strftime('%Y-%m-%d',SCHEDULEUPDATEDATE) AS SCHEDULEUPDATEDATE FROM T_SCHEDULE " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_PRIOR USING(PRIORID) " +
            $"LEFT OUTER JOIN(SELECT SCHEDULEID,'！' AS ALERTSTATUS FROM T_SCHEDULE WHERE STATUSID in (1,2) AND SCHEDULEENDTIME<{now}) USING(SCHEDULEID) " +
            $"WHERE SCHEDULEVISIBLESTATUS=1 AND SCHEDULEDATE LIKE @SCHEDULEDATE ORDER BY SCHEDULESTARTTIME LIMIT {SQLLimit}";
        public static string SQLSchedule0001 = $"" +
            $"SELECT SCHEDULEID,ALERTSTATUS,GOALNAME,PLANNAME,STATUSNAME,strftime('%Y-%m-%d',SCHEDULEDATE) AS SCHEDULEDATE,strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME,SCHEDULEHOURS,strftime('%Y-%m-%d',SCHEDULEUPDATEDATE) AS SCHEDULEUPDATEDATE FROM T_SCHEDULE " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_PRIOR USING(PRIORID) " +
            $"LEFT OUTER JOIN(SELECT SCHEDULEID,'！' AS ALERTSTATUS FROM T_SCHEDULE WHERE STATUSID in (1,2) AND SCHEDULEENDTIME<{now}) USING(SCHEDULEID) " +
            $"WHERE SCHEDULEVISIBLESTATUS=1 ORDER BY SCHEDULEDATE DESC,SCHEDULESTARTTIME LIMIT {SQLLimit}";
        public static string SQLSchedule0002 = $"" +
            $"SELECT SCHEDULEID,ALERTSTATUS,GOALNAME,PLANNAME,STATUSNAME,strftime('%Y-%m-%d',SCHEDULEDATE) AS SCHEDULEDATE,strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME,SCHEDULEHOURS,strftime('%Y-%m-%d',SCHEDULEUPDATEDATE) AS SCHEDULEUPDATEDATE FROM T_SCHEDULE " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_PRIOR USING(PRIORID) " +
            $"LEFT OUTER JOIN(SELECT SCHEDULEID,'！' AS ALERTSTATUS FROM T_SCHEDULE WHERE STATUSID in (1,2) AND SCHEDULEENDTIME<{now}) USING(SCHEDULEID) " +
            $"WHERE SCHEDULEVISIBLESTATUS=1 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) " +
            $"ORDER BY SCHEDULEDATE DESC,SCHEDULESTARTTIME LIMIT {SQLLimit}";
        public static string SQLSchedule0003 = "" +
            "SELECT GOALNAME,PLANNAME,strftime('%Y-%m-%d',SCHEDULEDATE) AS SCHEDULEDATE,strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME FROM T_SCHEDULE INNER JOIN T_PLAN USING(PLANID) INNER JOIN T_GOAL USING(GOALID) WHERE SCHEDULEID=@SCHEDULEID";
        public static string SQLSchedule0004 = $"" +
            $"SELECT SCHEDULEID,GOALNAME,PLANNAME,PRIORSUBNAME,PRIORNAME,STATUSNAME,strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME,SCHEDULEHOURS,SCHEDULELOCATION,SCHEDULEHEIGHT FROM T_SCHEDULE " +
            $"INNER JOIN T_PLAN USING(PLANID) " +
            $"INNER JOIN T_GOAL USING(GOALID) " +
            $"INNER JOIN T_STATUS USING(STATUSID) " +
            $"INNER JOIN T_PRIOR USING(PRIORID) " +
            $"LEFT OUTER JOIN(SELECT SCHEDULEID,'！' AS ALERTSTATUS FROM T_SCHEDULE WHERE STATUSID in (1,2) AND SCHEDULEENDTIME<{now}) USING(SCHEDULEID) " +
            $"WHERE SCHEDULEVISIBLESTATUS=1 AND SCHEDULEDATE LIKE @SCHEDULEDATE " +
            $"ORDER BY SCHEDULEDATE DESC,SCHEDULESTARTTIME";
        public static string SQLSchedule0005 = "" +
            "SELECT strftime('%H:%M',SCHEDULESTARTTIME) AS SCHEDULESTARTTIME,strftime('%H:%M',SCHEDULEENDTIME) AS SCHEDULEENDTIME FROM T_SCHEDULE WHERE SCHEDULEID != @SCHEDULEID AND SCHEDULEDATE=@SCHEDULEDATE";
        public static string SQLSchedule0010 = $"" +
            $"INSERT INTO T_SCHEDULE(PLANID,SCHEDULEDATE,SCHEDULESTARTTIME,SCHEDULEENDTIME,SCHEDULEHOURS,SCHEDULELOCATION,SCHEDULEHEIGHT,SCHEDULEUPDATEDATE) VALUES(@PLANID,@SCHEDULEDATE,@SCHEDULESTARTTIME,@SCHEDULEENDTIME,round(cast((strftime('%s', @SCHEDULEENDTIME) - strftime('%s', @SCHEDULESTARTTIME)) as REAL)/ 3600,2),@SCHEDULELOCATION,@SCHEDULEHEIGHT,{now})";
        public static string SQLSchedule0020 = $"" +
            $"UPDATE T_SCHEDULE SET PLANID=@PLANID,SCHEDULEDATE=@SCHEDULEDATE,SCHEDULESTARTTIME=@SCHEDULESTARTTIME,SCHEDULEENDTIME=@SCHEDULEENDTIME,SCHEDULEHOURS=round(cast((strftime('%s', @SCHEDULEENDTIME) - strftime('%s', @SCHEDULESTARTTIME)) as REAL)/ 3600,2),SCHEDULELOCATION=@SCHEDULELOCATION,SCHEDULEHEIGHT=@SCHEDULEHEIGHT,SCHEDULEUPDATEDATE={now} WHERE SCHEDULEID=@SCHEDULEID";
        public static string SQLSchedule0021 = $"" +
            $"UPDATE T_SCHEDULE SET SCHEDULEVISIBLESTATUS=@VISIBLESTATUS,SCHEDULEUPDATEDATE={now} WHERE SCHEDULEID=@SCHEDULEID";
        public static string SQLSchedule0022 = $"" +
            $"UPDATE T_SCHEDULE SET STATUSID=@STATUSID,SCHEDULEUPDATEDATE={now} WHERE SCHEDULEID=@SCHEDULEID";
        public static Image UpdateImg = Image.FromFile(@".\resources\png\Update.png");
        public static Image ToRecycleBinImg = Image.FromFile(@".\resources\png\ToRecycleBin.png");
        public static Image ToStorageBinImg = Image.FromFile(@".\resources\png\ToStorageBin.png");
        public static Image AddImg = Image.FromFile(@".\resources\png\Add.png");
        public static Image StatusImg = Image.FromFile(@".\resources\png\Status.png");
        public static ScheduleSearch ScheduleSearchInstance = null;
        public static ScheduleForm ScheduleFormInstance = null;

        public class DataGridViewEx : DataGridView
        {
            public DataGridViewEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("Schedule Loaded!", 1);
            UserControl ScheduleInstance = new ScheduleSchedule
            {
                Dock = DockStyle.Fill,
            };
            ScheduleInstance.Disposed += (sender, e) =>
            {
                if (ScheduleSearchInstance != null && !ScheduleSearchInstance.IsDisposed)
                {
                    ScheduleSearchInstance.Close();
                }
            };
            MainPanel.Controls.Add(ScheduleInstance);
        }
    }
}
