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
    public partial class ToDoSearch : Form
    {
        public ToDoSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        private string ThisFormKey = null;
        private void b1_Click(object sender, EventArgs e)
        {
            if (ThisFormKey == "ToDo")
            {
                ToDoInComp.ToDoInCompSearch = tb1.Text;
                ToDoInComp.AcessCls.DataLoad();
            }
            else if (ThisFormKey == "Done")
            {
                ToDoComp.ToDoCompSearch = tb1.Text;
                ToDoComp.AcessCls.DataLoad();
            }
            this.Close();
            this.Dispose();
        }
        private void ToDoSearch_Load(object sender, EventArgs e)
        {
            if (ThisFormKey == "ToDo")
            {
                this.Text = "ToDo検索";
                l1.Text = "目標名/計画名：";
                tb1.Text = ToDoInComp.ToDoInCompSearch;
            }
            else if (ThisFormKey == "Done")
            {
                this.Text = "Done検索";
                l1.Text = "目標名/計画名：";
                tb1.Text = ToDoComp.ToDoCompSearch;
            }
        }
    }
}
