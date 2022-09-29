using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record
{
    public partial class RecordSearch : Form
    {
        public RecordSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        private string ThisFormKey = null;
        private void b1_Click(object sender, EventArgs e)
        {
            if (ThisFormKey == "種別")
            {
                RecordGenre.RecordGenreSearch = tb1.Text;
                RecordGenre.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "目標")
            {
                RecordGoal.RecordGoalSearch = tb1.Text;
                RecordGoal.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "計画")
            {
                RecordPlan.RecordPlanSearch = tb1.Text;
                RecordPlan.AcessCls.DataLoad();
            }
            this.Close();
            this.Dispose();
        }
        private void RecordSeach_Load(object sender, EventArgs e)
        {
            if (ThisFormKey == "種別")
            {
                this.Text = "種別検索";
                l1.Text = "種別名：";
                tb1.Text = RecordGenre.RecordGenreSearch;
            }
            else if (ThisFormKey == "目標")
            {
                this.Text = "目標検索";
                l1.Text = "目標名：";
                tb1.Text = RecordGoal.RecordGoalSearch;
            }
            else if (ThisFormKey == "計画")
            {
                this.Text = "計画検索";
                l1.Text = "目標名/計画名：";
                tb1.Text = RecordPlan.RecordPlanSearch;
            }
        }
    }
}
