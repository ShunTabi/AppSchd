using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Note
{
    public partial class NoteSearch : Form
    {
        public NoteSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        private string ThisFormKey = null;
        private void b1_Click(object sender, EventArgs e)
        {
            if (ThisFormKey == "記録")
            {
                if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")) { return; }
                if (radio0.Checked) { NoteRecord.NoteRecordSearch0 = 0; }
                else if (radio1.Checked) { NoteRecord.NoteRecordSearch0 = 1; }
                NoteRecord.NoteRecordSearch1 = tb1.Text;
                NoteRecord.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "リスト")
            {
                if (radio0.Checked) { NoteList.NoteListSearch0 = 0; }
                else if (radio1.Checked) { NoteList.NoteListSearch0 = 1; }
                NoteList.NoteListSearch1 = tb1.Text;
                NoteList.AcessCls.DataLoad();
            }
            this.Close();
            this.Dispose();
        }
        private void RecordSeach_Load(object sender, EventArgs e)
        {
            if (ThisFormKey == "記録")
            {
                this.Text = "記録検索";
                l1.Text = "検索レベル："; 
                radio0.Checked = NoteRecord.NoteRecordSearch0 == 0;//全て
                radio1.Checked = NoteRecord.NoteRecordSearch0 == 1;//最新のみ
                l2.Text = "日付：";
                tb1.Text = NoteRecord.NoteRecordSearch1;
            }
            else if (ThisFormKey == "リスト")
            {
                this.Text = "記録検索";
                l1.Text = "検索レベル：";
                radio0.Checked = NoteList.NoteListSearch0 == 0;//全て
                radio1.Checked = NoteList.NoteListSearch0 == 1;//最新のみ
                l2.Text = "ノート内容：";
                tb1.Text = NoteList.NoteListSearch1;
            }
        }
    }
}
