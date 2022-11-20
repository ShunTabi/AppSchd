using System;
using System.Drawing;
using System.Windows.Forms;

namespace Note
{
    public partial class NoteNote : UserControl
    {
        public NoteNote()
        {
            InitializeComponent();
        }
        private static Button NoteActiveButton = null;
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationLoad
        {
            public static void CreateNote(Label l, Panel p, string FunctionName)
            {
                l.Text = FunctionName;
                UserControl uc = null;
                if (p.Controls.Count > 0) 
                {
                    p.Controls[0].Dispose();
                }
                if (FunctionName == "記録")
                {
                    uc = new NoteRecord
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "リスト")
                {
                    uc = new NoteList
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void NoteNote_Load(object sender, EventArgs e)
        {
            NoteActiveButton = b1;
            NoteActiveButton.Enabled = false;
            ThisApplicationLoad.CreateNote(l1, p2, "記録");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            NoteActiveButton.Enabled = true;
            NoteActiveButton = b1;
            NoteActiveButton.Enabled = false;
            ThisApplicationLoad.CreateNote(l1, p2, "記録");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            NoteActiveButton.Enabled = true;
            NoteActiveButton = b2;
            NoteActiveButton.Enabled = false;
            ThisApplicationLoad.CreateNote(l1, p2, "リスト");
        }
        private void b3_Click(object sender, EventArgs e)
        {
            if (Note.NoteSearchInstance != null && !Note.NoteSearchInstance.IsDisposed)
            {
                Note.NoteSearchInstance.Dispose();
            }
            Note.NoteSearchInstance = new NoteSearch(l1.Text);
            Note.NoteSearchInstance.Show();
            Note.NoteSearchInstance.Location = new Point(
                int.Parse(string.Format("{0}", SubLocation[0])),
                int.Parse(string.Format("{0}", SubLocation[1]))
                );
        }
        private void b4_Click(object sender, EventArgs e)
        {
            if (l1.Text == "記録")
            {
                NoteRecord.AcessCls.DataLoad();
            }
            else if (l1.Text == "リスト")
            {
                NoteList.AcessCls.DataLoad();
            }
        }
    }
}
