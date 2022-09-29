using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public partial class ScheduleSchedule : UserControl
    {
        public ScheduleSchedule()
        {
            InitializeComponent();
        }
        private static Button ScheduleActiveButton = null;
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationLoad
        {
            public static void CreateRecord(Label l, Panel p, string FunctionName)
            {
                if (l.Text == FunctionName) { return; }
                l.Text = FunctionName;
                UserControl uc = null;
                if (p.Controls.Count > 0)
                {
                    p.Controls[0].Dispose();
                }
                if (FunctionName == "日別")
                {
                    uc = new ScheduleOneDay
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "週間")
                {
                    uc = new ScheduleWeek
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "リスト")
                {
                    uc = new ScheduleList
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void ScheduleSchedule_Load(object sender, EventArgs e)
        {
            ScheduleActiveButton = b1;
            ScheduleActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "日別");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            ScheduleActiveButton.Enabled = true;
            ScheduleActiveButton = b1;
            ScheduleActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "日別");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            ScheduleActiveButton.Enabled = true;
            ScheduleActiveButton = b2;
            ScheduleActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "週間");
        }
        private void b3_Click(object sender, EventArgs e)
        {
            ScheduleActiveButton.Enabled = true;
            ScheduleActiveButton = b3;
            ScheduleActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "リスト");
        }
        private void b4_Click(object sender, EventArgs e)
        {
            if (Schedule.ScheduleSearchInstance == null || Schedule.ScheduleSearchInstance.IsDisposed)
            {
                Schedule.ScheduleSearchInstance = new ScheduleSearch(l1.Text);
                Schedule.ScheduleSearchInstance.Show();
                Schedule.ScheduleSearchInstance.Location = new Point(
                    int.Parse(string.Format("{0}", SubLocation[0])),
                    int.Parse(string.Format("{0}", SubLocation[1]))
                    );
            }
        }
        private void b5_Click(object sender, EventArgs e)
        {
            if(l1.Text == "日別")
            {
                ScheduleOneDay.AcessCls.DataLoad();
            }
            else if(l1.Text == "週間")
            {
                ScheduleWeek.AcessCls.DataLoad();
            }
            else if(l1.Text == "リスト")
            {
                ScheduleList.AcessCls.DataLoad();
            }
        }
    }
}
