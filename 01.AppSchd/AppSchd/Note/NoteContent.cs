using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note
{
    public partial class NoteContent : Form
    {
        public NoteContent(string Key1,string Key2,string Key3)
        {
            InitializeComponent();
            NoteTitle = Key1;
            NoteDate = Key2;
            NoteValue = Key3;
        }
        private string NoteTitle = null;
        private string NoteDate = null;
        private string NoteValue = null;
        private void NoteContent_Load(object sender, EventArgs e)
        {
            this.Text = $"【{NoteTitle}】{NoteDate}";
            tb1.Text = $"{this.Text}{Environment.NewLine}{NoteValue}";
        }
    }
}
