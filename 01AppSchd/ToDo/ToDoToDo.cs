using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (ToDo.ToDoSearchInstance == null || ToDo.ToDoSearchInstance.IsDisposed)
            {
                ToDo.ToDoSearchInstance = new ToDoSearch(l1.Text);
                ToDo.ToDoSearchInstance.Show();
                ToDo.ToDoSearchInstance.Location = new Point(10, 10);
            }
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
