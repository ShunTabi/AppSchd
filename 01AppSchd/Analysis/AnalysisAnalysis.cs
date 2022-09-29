using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analysis
{
    public partial class AnalysisAnalysis : UserControl
    {
        public AnalysisAnalysis()
        {
            InitializeComponent();
        }
        private static Button AnalysisActiveButton = null;
        public static string[] SubLocation = FunFile.GetString(FunFile.iniDefault, "[SUB]", "SubLocation");
        class ThisApplicationLoad
        {
            public static void CreateAnalysis(Label l,Panel p,string FunctionName)
            {
                if(l.Text == FunctionName) { return; }
                l.Text = FunctionName;
                UserControl uc = null;
                if(p.Controls.Count > 0)
                {
                    p.Controls[0].Dispose();
                }
                if(FunctionName == "Chart")
                {
                    uc = new AnalysisChart()
                    {
                        Dock = DockStyle.Fill,
                    };
                }
                p.Controls.Add(uc);
            }
        }
        private void AnalysisAnalysis_Load(object sender, EventArgs e)
        {
            AnalysisActiveButton = b1;
            AnalysisActiveButton.Enabled = false;
            ThisApplicationLoad.CreateAnalysis(l1, p2, "Chart");
        }
        private void b1_Click(object sender, EventArgs e)
        {
            AnalysisActiveButton.Enabled = true;
            AnalysisActiveButton = b1;
            AnalysisActiveButton.Enabled = false;
            ThisApplicationLoad.CreateAnalysis(l1, p2, "Chart");
        }
        private void b2_Click(object sender, EventArgs e)
        {
            if (Analysis.AnalysisSearchInstance == null || Analysis.AnalysisSearchInstance.IsDisposed)
            {
                Analysis.AnalysisSearchInstance = new AnalysisSearch(l1.Text);
                Analysis.AnalysisSearchInstance.Show();
                Analysis.AnalysisSearchInstance.Location = new Point(
                    int.Parse(string.Format("{0}", SubLocation[0])),
                    int.Parse(string.Format("{0}", SubLocation[1]))
                    );
            }
        }
        private void b3_Click(object sender, EventArgs e)
        {
            if (l1.Text == "Chart")
            {
                Task ActiveTask = FunFile.ErrEvtProc("Chartは最新化をできません。", 0);
            }
        }
    }
}
