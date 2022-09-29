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
    public partial class ScheduleSearch : Form
    {
        public ScheduleSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        private string ThisFormKey = null;
        private void b1_Click(object sender, EventArgs e)
        {
            if (ThisFormKey == "日別")
            {
                ScheduleOneDay.ScheduleOneDaySearch = tb1.Text;
                ScheduleOneDay.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "週間")
            {
                ScheduleWeek.ScheduleWeekSearch = tb1.Text;
                ScheduleWeek.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "リスト")
            {
                ScheduleList.ScheduleListSearch = tb1.Text;
                ScheduleList.AcessCls.DataLoad();
            }
            this.Close();
            this.Dispose();
        }
        private void ScheduleSearch_Load(object sender, EventArgs e)
        {
            if (ThisFormKey == "日別")
            {
                this.Text = "日別スケジュール検索";
                l1.Text = "日付：";
                tb1.Text = ScheduleOneDay.ScheduleOneDaySearch;
            }
            else if (ThisFormKey == "週間")
            {
                this.Text = "週間スケジュール検索";
                l1.Text = "日付：";
                tb1.Text = ScheduleWeek.ScheduleWeekSearch;
            }
            else if (ThisFormKey == "リスト")
            {
                this.Text = "スケジュールリスト検索";
                l1.Text = "目標名/計画名：";
                tb1.Text = ScheduleList.ScheduleListSearch;
            }
        }
    }
}
