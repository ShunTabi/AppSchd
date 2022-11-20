using System;
using System.Windows.Forms;

namespace Bin
{
    public partial class BinSearch : Form
    {
        public BinSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        private string ThisFormKey = null;
        private void b1_Click(object sender, EventArgs e)
        {
            if (ThisFormKey == "収納箱")
            {
                BinStorage.BinStorageSearch = tb1.Text;
                BinStorage.AcessCls.DataLoad();
            }
            this.Close();
            this.Dispose();
        }
        private void BinSearch_Load(object sender, EventArgs e)
        {
            if (ThisFormKey == "収納箱")
            {
                this.Text = "収納箱検索";
                l1.Text = "内容：";
                tb1.Text = BinStorage.BinStorageSearch;
            }
        }
    }
}
