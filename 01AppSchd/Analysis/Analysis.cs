using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;

namespace Analysis
{
    public class Analysis
    {
        private static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
        public static string SQLAnalysis0000 = "SELECT DISTINCT GENREID,GENRENAME FROM T_SCHEDULE " +
            "INNER JOIN T_PLAN USING(PLANID) " +
            "INNER JOIN T_GOAL USING(GOALID) " +
            "INNER JOIN T_GENRE USING (GENREID) WHERE SCHEDULEDATE BETWEEN @MONTH1 AND @MONTH2";
        public static string SQLAnalysis0001 = "SELECT DISTINCT GOALID,GOALNAME FROM T_SCHEDULE " +
            "INNER JOIN T_PLAN USING(PLANID) " +
            "INNER JOIN T_GOAL USING(GOALID) WHERE SCHEDULEDATE BETWEEN @MONTH1 AND @MONTH2";
        public static string SQLAnalysis0002 = "SELECT DISTINCT PLANID,GOALNAME||'|'||PLANNAME FROM T_SCHEDULE " +
            "INNER JOIN T_PLAN USING(PLANID) " +
            "INNER JOIN T_GOAL USING(GOALID) WHERE SCHEDULEDATE BETWEEN @MONTH1 AND @MONTH2";
        public static string SQLAnalysis0003 = "SELECT strftime('%Y-%m',ANALYSISDATE),ANALYSISITEMID,ANALYSISITEMNAME,ROUND(SUM(IFNULL(SCHEDULEHOURS,0)), 6) AS HOURS FROM T_ANALYSISDATA " +
            "INNER JOIN T_GOAL ON GENREID = ANALYSISITEMID " +
            "INNER JOIN T_PLAN USING(GOALID) " +
            "LEFT OUTER JOIN (SELECT PLANID,SCHEDULEHOURS,SCHEDULEDATE FROM T_SCHEDULE WHERE STATUSID = 3) AS A ON strftime('%Y-%m-01', SCHEDULEDATE) = ANALYSISDATE " +
            "AND A.PLANID = T_PLAN.PLANID " +
            "GROUP BY ANALYSISDATE, ANALYSISITEMID ORDER BY ANALYSISITEMNAME ASC,ANALYSISITEMID ASC,ANALYSISDATE ASC";
        public static string SQLAnalysis0004 = "SELECT strftime('%Y-%m',ANALYSISDATE),ANALYSISITEMID,ANALYSISITEMNAME,ROUND(SUM(IFNULL(SCHEDULEHOURS,0)), 6) AS HOURS FROM T_ANALYSISDATA " +
            "INNER JOIN T_PLAN ON GOALID = ANALYSISITEMID " +
            "LEFT OUTER JOIN (SELECT PLANID,SCHEDULEHOURS,SCHEDULEDATE FROM T_SCHEDULE WHERE STATUSID = 3) AS A ON strftime('%Y-%m-01', SCHEDULEDATE) = ANALYSISDATE " +
            "AND A.PLANID = T_PLAN.PLANID " +
            "GROUP BY ANALYSISDATE, ANALYSISITEMID ORDER BY ANALYSISITEMNAME ASC,ANALYSISITEMID ASC,ANALYSISDATE ASC";
        public static string SQLAnalysis0005 = "SELECT strftime('%Y-%m',ANALYSISDATE),ANALYSISITEMID,ANALYSISITEMNAME,ROUND(SUM(IFNULL(SCHEDULEHOURS,0)), 6) AS HOURS FROM T_ANALYSISDATA " +
            "LEFT OUTER JOIN (SELECT PLANID,SCHEDULEHOURS,SCHEDULEDATE FROM T_SCHEDULE WHERE STATUSID = 3) AS A ON strftime('%Y-%m-01', SCHEDULEDATE) = ANALYSISDATE " +
            "AND A.PLANID = T_ANALYSISDATA.ANALYSISITEMID " +
            "GROUP BY ANALYSISDATE, ANALYSISITEMID ORDER BY ANALYSISITEMNAME ASC,ANALYSISITEMID ASC,ANALYSISDATE ASC";
        public static string SQLAnalysis0010 = $"INSERT INTO T_ANALYSISDATA(ANALYSISDATE,ANALYSISITEMID,ANALYSISITEMNAME,ANALYSISREMARKS,ANALYSISUPDATEDATE) VALUES(@ANALYSISDATE,@ANALYSISITEMID,@ANALYSISITEMNAME,@ANALYSISREMARKS,{now})";
        public static string SQLAnalysis0030 = "DELETE FROM T_ANALYSISDATA";
        public static Image ChartImg = Image.FromFile(@".\resources\png\Chart.png");
        public static AnalysisSearch AnalysisSearchInstance = null;
        public class ChartEx : Chart
        {
            public ChartEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("Analysis Loaded!", 1);
            UserControl AnalysisInstance = new AnalysisAnalysis
            {
                Dock = DockStyle.Fill,
            };
            MainPanel.Controls.Add(AnalysisInstance);
        }
    }
}
