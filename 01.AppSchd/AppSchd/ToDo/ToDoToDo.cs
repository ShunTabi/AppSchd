using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{
    public partial class ToDoToDo : UserControl
    {
        public ToDoToDo()
        {
            InitializeComponent();
        }
        private static Button ToDoActiveButton = null; 
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
                if (FunctionName == "ToDo")
                {
                    uc = new ToDoInComp
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "Done")
                {
                    uc = new ToDoComp
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void ToDoToDo_Load(object sender, EventArgs e)
        {
            ToDoActiveButton = b1;
            ToDoActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "ToDo");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            ToDoActiveButton.Enabled = true;
            ToDoActiveButton = b1;
            ToDoActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "ToDo");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            ToDoActiveButton.Enabled = true;
            ToDoActiveButton = b2;
            ToDoActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "Done");
        }

        private void b3_Click(object sender, EventArgs e)
        {
            if (ToDo.ToDoSearchInstance != null && ToDo.ToDoSearchInstance.IsDisposed)
            {
                ToDo.ToDoSearchInstance.Dispose();
            }
            ToDo.ToDoSearchInstance = new ToDoSearch(l1.Text);
            ToDo.ToDoSearchInstance.Show();
            ToDo.ToDoSearchInstance.Location = new Point(
                int.Parse(string.Format("{0}", SubLocation[0])),
                int.Parse(string.Format("{0}", SubLocation[1]))
                );
        }
        private void b4_Click(object sender, EventArgs e)
        {
            if (l1.Text == "ToDo")
            {
                ToDoInComp.AcessCls.DataLoad();
            }
            else if (l1.Text == "Done")
            {
                ToDoComp.AcessCls.DataLoad();
            }
        }
    }
}
