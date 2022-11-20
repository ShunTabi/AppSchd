using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bin
{
    public class Bin
    {
        public static string SQLBin0000 = $"SELECT 'GENRE^'||GENREID AS COL_KEY,'種別項目' AS COL_ITEM,GENRENAME AS COL_NAME,'' AS COL_REMARKS1,'' AS COL_REMARKS2,strftime('%Y-%m-%d', GENREUPDATEDATE) AS COL_UPDATEDATE FROM T_GENRE WHERE GENREVISIBLESTATUS = @VISIBLESTATUS " +
            $"UNION " +
            $"SELECT 'GOAL^'||GOALID,'目標項目',GOALNAME,'','',strftime('%Y-%m-%d', GOALUPDATEDATE) FROM T_GOAL WHERE GOALVISIBLESTATUS = @VISIBLESTATUS " +
            $"UNION " +
            $"SELECT 'PLAN^'||PLANID,'計画項目',PLANNAME,GOALNAME||'-'||PLANNAME,strftime('%Y%m%d', PLANSTARTDATE)||'-'||strftime('%Y%m%d', PLANENDDATE),strftime('%Y-%m-%d', PLANUPDATEDATE) FROM T_PLAN INNER JOIN T_GOAL USING(GOALID) WHERE PLANVISIBLESTATUS = @VISIBLESTATUS " +
            $"UNION " +
            $"SELECT 'SCHEDULE^'||SCHEDULEID,'予定項目',strftime('%Y%m%d', SCHEDULEDATE),strftime('%H%M', SCHEDULESTARTTIME)||'-'||strftime('%H%M', SCHEDULEENDTIME),GOALNAME||'-'||PLANNAME,strftime('%Y-%m-%d', SCHEDULEUPDATEDATE) FROM T_SCHEDULE INNER JOIN T_PLAN USING(PLANID) INNER JOIN T_GOAL USING(GOALID) WHERE SCHEDULEVISIBLESTATUS = @VISIBLESTATUS " +
            $"UNION " +
            $"SELECT 'TODO^'||TODOID,'TODO項目',TODONAME,GOALNAME||'-'||PLANNAME,'',strftime('%Y-%m-%d', TODOUPDATEDATE) FROM T_TODO INNER JOIN T_PLAN USING(PLANID) INNER JOIN T_GOAL USING(GOALID) WHERE TODOVISIBLESTATUS = @VISIBLESTATUS";
        public static string SQLBin0001 = $"SELECT 'GENRE^'||GENREID AS COL_KEY,'種別項目' AS COL_ITEM,GENRENAME AS COL_NAME,'' AS COL_REMARKS1,'' AS COL_REMARKS2,strftime('%Y-%m-%d', GENREUPDATEDATE) AS COL_UPDATEDATE FROM T_GENRE WHERE GENREVISIBLESTATUS = @VISIBLESTATUS AND GENRENAME LIKE @KEYWORD " +
            $"UNION " +
            $"SELECT 'GOAL^'||GOALID,'目標項目',GOALNAME,'','',strftime('%Y-%m-%d', GOALUPDATEDATE) FROM T_GOAL WHERE GOALVISIBLESTATUS = @VISIBLESTATUS AND GOALNAME LIKE @KEYWORD " +
            $"UNION " +
            $"SELECT 'PLAN^'||PLANID,'計画項目',PLANNAME,GOALNAME||'-'||PLANNAME,strftime('%Y%m%d', PLANSTARTDATE)||'-'||strftime('%Y%m%d', PLANENDDATE),strftime('%Y-%m-%d', PLANUPDATEDATE) FROM T_PLAN INNER JOIN T_GOAL USING(GOALID) WHERE PLANVISIBLESTATUS = @VISIBLESTATUS AND PLANNAME LIKE @KEYWORD " +
            $"UNION " +
            $"SELECT 'SCHEDULE^'||SCHEDULEID,'予定項目',strftime('%Y%m%d', SCHEDULEDATE),strftime('%H%M', SCHEDULESTARTTIME)||'-'||strftime('%H%M', SCHEDULEENDTIME),GOALNAME||'-'||PLANNAME,strftime('%Y-%m-%d', SCHEDULEUPDATEDATE) FROM T_SCHEDULE INNER JOIN T_PLAN USING(PLANID) INNER JOIN T_GOAL USING(GOALID) WHERE SCHEDULEVISIBLESTATUS = @VISIBLESTATUS AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) " +
            $"UNION " +
            $"SELECT 'TODO^'||TODOID,'TODO項目',TODONAME,GOALNAME||'-'||PLANNAME,'',strftime('%Y-%m-%d', TODOUPDATEDATE) FROM T_TODO INNER JOIN T_PLAN USING(PLANID) INNER JOIN T_GOAL USING(GOALID) WHERE TODOVISIBLESTATUS = @VISIBLESTATUS AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD OR TODONAME LIKE @KEYWORD)";
        public static Image UpdateImg = Image.FromFile(@".\resources\png\Update.png");
        public static Image ToRecycleBinImg = Image.FromFile(@".\resources\png\ToRecycleBin.png");
        public static Image ToStorageBinImg = Image.FromFile(@".\resources\png\ToStorageBin.png");
        public static Image AddImg = Image.FromFile(@".\resources\png\Add.png");
        public static Image StatusImg = Image.FromFile(@".\resources\png\Status.png");
        public static BinSearch BinSearchInstance = null;
        public class DataGridViewEx : DataGridView
        {
            public DataGridViewEx()
            {
                this.DoubleBuffered = true;
            }
        }
        public void MainClassExec(Panel MainPanel)
        {
            Task ActiveTask = FunFile.EvtProc("Bin Loaded!", 1);
            UserControl BinInstance = new BinBin
            {
                Dock = DockStyle.Fill,
            };
            MainPanel.Controls.Add(BinInstance);
        }
    }
}
