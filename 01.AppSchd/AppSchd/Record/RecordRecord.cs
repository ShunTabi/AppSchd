using System;
using System.Drawing;
using System.Windows.Forms;

namespace Record
{
    public partial class RecordRecord : UserControl
    {
        public RecordRecord()
        {
            InitializeComponent();
        }
        private static Button RecordActiveButton = null;
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationLoad
        {
            public static void CreateRecord(Label l, Panel p, string FunctionName)
            {
                l.Text = FunctionName;
                UserControl uc = null;
                if (p.Controls.Count > 0)
                {
                    p.Controls[0].Dispose();
                }
                if (FunctionName == "種別")
                {
                    uc = new RecordGenre
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "目標")
                {
                    uc = new RecordGoal
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "計画")
                {
                    uc = new RecordPlan
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void RecordRecord_Load(object sender, EventArgs e)
        {
            RecordActiveButton = b3;
            RecordActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "計画");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            RecordActiveButton.Enabled = true;
            RecordActiveButton = b1;
            RecordActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "種別");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            RecordActiveButton.Enabled = true;
            RecordActiveButton = b2;
            RecordActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "目標");
        }
        private void b3_Click(object sender, EventArgs e)
        {
            RecordActiveButton.Enabled = true;
            RecordActiveButton = b3;
            RecordActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "計画");
        }
        private void b4_Click(object sender, EventArgs e)
        {
            if (Record.RecordSearchInstance != null && !Record.RecordSearchInstance.IsDisposed)
            {
                Record.RecordSearchInstance.Dispose();
            }
            Record.RecordSearchInstance = new RecordSearch(l1.Text);
            Record.RecordSearchInstance.Show();
            Record.RecordSearchInstance.Location = new Point(
                int.Parse(string.Format("{0}", SubLocation[0])),
                int.Parse(string.Format("{0}", SubLocation[1]))
                );
        }
        private void b5_Click(object sender, EventArgs e)
        {
            if (l1.Text == "種別")
            {
                RecordGenre.AcessCls.DataLoad();
            }
            else if (l1.Text == "目標")
            {
                RecordGoal.AcessCls.DataLoad();
            }
            else if (l1.Text == "計画")
            {
                RecordPlan.AcessCls.DataLoad();
            }
        }
    }
}
