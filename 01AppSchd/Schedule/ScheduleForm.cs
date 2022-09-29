using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Schedule
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm(string TypeName,string Key,string ID)
        {
            InitializeComponent();
            ThisFormKey = Key;
            SCHEDULEID = ID;
            ButtonName = TypeName;
        }
        private static string ThisFormKey;
        private static string SCHEDULEID;
        private static string ButtonName;
        class ThisApplicationSetup
        {
            private static void CreateCmbItems(ComboBox cmb1, ComboBox cmb2)
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add("ValueMember");
                dt1.Columns.Add("DisplayMember");
                string sql1 = "SELECT GOALID,GOALNAME FROM T_GOAL WHERE GOALVISIBLESTATUS=1 ORDER BY GOALNAME,GOALUPDATEDATE DESC";
                string[][] output1 = FunSQL.SQLSELECT("***Schedule***", sql1, new string[] { }, new string[] { });
                for (int i = 0; i < output1.Length; i++)
                {
                    dr1 = dt1.NewRow();
                    dr1["ValueMember"] = output1[i][0];
                    dr1["DisplayMember"] = output1[i][1];
                    dt1.Rows.Add(dr1);
                }
                cmb1.DataSource = dt1;
                cmb1.ValueMember = "ValueMember";
                cmb1.DisplayMember = "DisplayMember";
                cmb1.SelectedIndexChanged += (sender1, e1) =>
                {
                    ThisApplicationLoad.LoadCmbData(cmb2, cmb1.SelectedValue.ToString());
                };
            }
            private static void InsertValue(Form fm,ComboBox cmb1, ComboBox cmb2,TextBox tb1, TextBox tb2, TextBox tb3, Button b1)
            {
                b1.Text = ButtonName;
                if (ButtonName == "確定" && SCHEDULEID == "")
                {
                    if(ThisFormKey == "日別")
                    {
                        tb1.Text = ScheduleOneDay.ScheduleOneDaySearch;
                    }
                    else if(ThisFormKey == "週間")
                    {
                        tb1.Text = ScheduleWeek.ScheduleWeekSearch;
                    }
                    tb2.Text = DateTime.Now.ToString("HH:mm");
                    tb3.Text = DateTime.Now.ToString("HH:mm");
                }
                else if (ButtonName == "更新" && SCHEDULEID != "")
                {
                    string[][] output1 = FunSQL.SQLSELECT("SQLSchedule0003", Schedule.SQLSchedule0003, new string[] { "@SCHEDULEID" }, new string[] { SCHEDULEID });
                    if (cmb1.FindStringExact(output1[0][0]) < 0 )
                    {
                        fm.Close();
                        Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                        return;
                    }
                    cmb1.Text = output1[0][0];
                    if (cmb2.FindStringExact(output1[0][1]) < 0)
                    {
                        fm.Close();
                        Task ActiveTask = FunFile.ErrEvtProc("更新できません", 0);
                    }
                    else
                    {
                        cmb2.Text = output1[0][1];
                        tb1.Text = DateTime.Parse(output1[0][2]).ToString("yyyy-MM-dd");
                        tb2.Text = DateTime.Parse(output1[0][3]).ToString("HH:mm");
                        tb3.Text = DateTime.Parse(output1[0][4]).ToString("HH:mm");
                    }
                }
            }
            public static void ThisApplicationSetupExec(Form fm,ComboBox cmb1,ComboBox cmb2, TextBox tb1, TextBox tb2, TextBox tb3, Button b1)
            {
                CreateCmbItems(cmb1, cmb2);
                ThisApplicationLoad.LoadCmbData(cmb2, cmb1.SelectedValue.ToString());
                InsertValue(fm,cmb1, cmb2, tb1, tb2, tb3, b1);
            }
        }
        class ThisApplicationLoad
        {
            public static void LoadCmbData(ComboBox cmb2,string SelectValue)
            {
                DataTable dt2 = new DataTable();
                DataRow dr2;
                dt2.Columns.Add("ValueMember");
                dt2.Columns.Add("DisplayMember");
                string sql2 = "SELECT PLANID,PLANNAME FROM T_PLAN WHERE GOALID=@GOALID AND PLANVISIBLESTATUS=1 ORDER BY PRIORID ASC,STATUSID ASC,PLANENDDATE ASC,PLANNAME,PLANUPDATEDATE DESC";
                string[][] output2 = FunSQL.SQLSELECT("***Schedule***", sql2, new string[] { "@GOALID" }, new string[] { SelectValue });
                for (int i = 0; i < output2.Length; i++)
                {
                    dr2 = dt2.NewRow();
                    dr2["ValueMember"] = output2[i][0];
                    dr2["DisplayMember"] = output2[i][1];
                    dt2.Rows.Add(dr2);
                }
                cmb2.DataSource = dt2;
                cmb2.ValueMember = "ValueMember";
                cmb2.DisplayMember = "DisplayMember";
            }
        }
        private void b1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
            if (!Regex.IsMatch(tb2.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$") || !Regex.IsMatch(tb3.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$")) { return; }
            string[] STARTTIMEHM = DateTime.Parse(tb2.Text).ToString("HH:mm").Split(':');
            int SCHEDULELOCATION = int.Parse(STARTTIMEHM[0]) * 60 + int.Parse(STARTTIMEHM[1]);
            string[] ENDTIMEHM = DateTime.Parse(tb3.Text).ToString("HH:mm").Split(':');
            int SCHEDULEHEIGHT = int.Parse(ENDTIMEHM[0]) * 60 + int.Parse(ENDTIMEHM[1]) - SCHEDULELOCATION;
            string[][] CheckSchedule = FunSQL.SQLSELECT("SQLSchedule0005", Schedule.SQLSchedule0005, new string[] { "@SCHEDULEID", "@SCHEDULEDATE" }, new string[] { SCHEDULEID,tb1.Text });
            DateTime TGScheduleStartTime = DateTime.Parse($"{tb1.Text} {tb2.Text}:00");
            DateTime TGScheduleEndTime = DateTime.Parse($"{tb1.Text} {tb3.Text}:00"); 
            bool CheckResult = TGScheduleStartTime < TGScheduleEndTime;
            for (int i=0;i< CheckSchedule.Length; i++)
            {
                DateTime ScheduledScheduleStartTime = DateTime.Parse(CheckSchedule[i][0]);
                DateTime ScheduledScheduleEndTime = DateTime.Parse(CheckSchedule[i][1]);
                CheckResult = CheckResult && ((TGScheduleEndTime < ScheduledScheduleEndTime && TGScheduleEndTime < ScheduledScheduleStartTime) || (ScheduledScheduleStartTime < TGScheduleStartTime && ScheduledScheduleEndTime < TGScheduleStartTime));
            }
            if (CheckResult)
            {
                if (b1.Text == "確定" && SCHEDULEID == "")
                {
                    FunSQL.SQLDML("SQLSchedule0010", Schedule.SQLSchedule0010, new string[] { "@PLANID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULELOCATION", "@SCHEDULEHEIGHT" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb1.Text} {tb2.Text}:00", $"{tb1.Text} {tb3.Text}:00", SCHEDULELOCATION.ToString(), SCHEDULEHEIGHT.ToString() });
                }
                else if (b1.Text == "更新" && SCHEDULEID != "")
                {
                    FunSQL.SQLDML("SQLSchedule0020", Schedule.SQLSchedule0020, new string[] { "@PLANID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULELOCATION", "@SCHEDULEHEIGHT", "@SCHEDULEID" }, new string[] { cmb2.SelectedValue.ToString(), tb1.Text, $"{tb1.Text} {tb2.Text}:00", $"{tb1.Text} {tb3.Text}:00", SCHEDULELOCATION.ToString(), SCHEDULEHEIGHT.ToString(), SCHEDULEID });
                }
                if (ThisFormKey == "日別")
                {
                    ScheduleOneDay.AcessCls.DataLoad();
                }
                else if (ThisFormKey == "週間")
                {
                    ScheduleWeek.AcessCls.DataLoad();
                }
                this.Close();
            }
        }
        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(this,cmb1, cmb2,tb1,tb2,tb3,b1);
        }
    }
}
