using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bin
{
    public partial class BinBin : UserControl
    {
        public BinBin()
        {
            InitializeComponent();
        }
        private static Button BinActiveButton = null;
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
                if (FunctionName == "収納箱")
                {
                    uc = new BinStorage
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                else if (FunctionName == "ゴミ箱")
                {
                    uc = new BinRecycle
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void BinBin_Load(object sender, EventArgs e)
        {
            BinActiveButton = b1;
            BinActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "収納箱");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            BinActiveButton.Enabled = true;
            BinActiveButton = b1;
            BinActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "収納箱");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            BinActiveButton.Enabled = true;
            BinActiveButton = b2;
            BinActiveButton.Enabled = false;
            ThisApplicationLoad.CreateRecord(l1, p2, "ゴミ箱");
        }
        private void b3_Click(object sender, EventArgs e)
        {
            if (Bin.BinSearchInstance != null && Bin.BinSearchInstance.IsDisposed)
            {
                Bin.BinSearchInstance.Dispose();
            }
            Bin.BinSearchInstance = new BinSearch(l1.Text);
            Bin.BinSearchInstance.Show();
            Bin.BinSearchInstance.Location = new Point(
                int.Parse(string.Format("{0}", SubLocation[0])),
                int.Parse(string.Format("{0}", SubLocation[1]))
                );
        }
        private void b4_Click(object sender, EventArgs e)
        {
            if (l1.Text == "収納箱")
            {
                BinStorage.AcessCls.DataLoad();
            }
            else if (l1.Text == "ゴミ箱")
            {
                //BinRecycle.AcessCls.DataLoad();
            }
        }
    }
}
