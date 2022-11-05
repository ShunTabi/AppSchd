using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Analysis
{
    public partial class AnalysisSearch : Form
    {
        public AnalysisSearch(string Key)
        {
            InitializeComponent();
            ThisFormKey = Key;
        }
        public static string ThisFormKey;
        private static string[][] output = new string[][] { };
        class ThisApplicationSetup
        {
            private static void CreateComboBox(ComboBox cmb1, ComboBox cmb2)
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add("ValueMember");
                dt1.Columns.Add("DisplayMember");
                DataTable dt2 = new DataTable();
                DataRow dr2;
                dt2.Columns.Add("ValueMember");
                dt2.Columns.Add("DisplayMember");
                string[] ChartNames = { "積立棒", "放物線", "100%面", "レーダー" };
                for (int i = 0; i < ChartNames.Length; i++)
                {
                    dr1 = dt1.NewRow();
                    dr1["ValueMember"] = i;
                    dr1["DisplayMember"] = ChartNames[i];
                    dt1.Rows.Add(dr1);
                }
                cmb1.DataSource = dt1;
                cmb1.ValueMember = "ValueMember";
                cmb1.DisplayMember = "DisplayMember";
                string[] Data1Names = { "GENRE", "GOAL", "PLAN" };
                for (int i = 0; i < Data1Names.Length; i++)
                {
                    dr2 = dt2.NewRow();
                    dr2["ValueMember"] = i;
                    dr2["DisplayMember"] = Data1Names[i];
                    dt2.Rows.Add(dr2);
                }
                cmb2.DataSource = dt2;
                cmb2.ValueMember = "ValueMember";
                cmb2.DisplayMember = "DisplayMember";
            }
            private static void InsertValue(Form fm, ComboBox cmb1, ComboBox cmb2, ComboBox cmb3, TextBox tb1, TextBox tb2)
            {
                if (ThisFormKey == "Chart")
                {
                    fm.Text = "Chart作成";
                    cmb1.SelectedValue = AnalysisChart.AnalysisChartKind.ToString();
                    cmb2.SelectedValue = AnalysisChart.AnalysisChartData1.ToString();
                    tb1.Text = AnalysisChart.AnalysisChartDay1;
                    tb2.Text = AnalysisChart.AnalysisChartDay2;
                }
            }
            public static void ThisApplicationSetupExec(Form fm, ComboBox cmb1, ComboBox cmb2, ComboBox cmb3, TextBox tb1, TextBox tb2)
            {
                CreateComboBox(cmb1, cmb2);
                InsertValue(fm, cmb1, cmb2, cmb3, tb1, tb2);
            }
        }
        class ThisApplicationLoad
        {
            public static void ComboBoxLoad(ComboBox cmb2, ComboBox cmb3, TextBox tb1, TextBox tb2)
            {
                DataTable dt13 = new DataTable();
                DataRow dr13;
                dt13.Columns.Add("ValueMember");
                dt13.Columns.Add("DisplayMember");
                dr13 = dt13.NewRow();
                dr13["ValueMember"] = "0";
                dr13["DisplayMember"] = "ALL";
                dt13.Rows.Add(dr13);
                string NextMonth = DateTime.Parse(tb2.Text).AddMonths(1).ToString();
                if (cmb2.SelectedValue.ToString() == "0")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0000", Analysis.SQLAnalysis0000, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, NextMonth });
                }
                else if (cmb2.SelectedValue.ToString() == "1")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0001", Analysis.SQLAnalysis0001, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, NextMonth });
                }
                else if (cmb2.SelectedValue.ToString() == "2")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0002", Analysis.SQLAnalysis0002, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, NextMonth });
                }
                for (int i = 0; i < output.Length; i++)
                {
                    dr13 = dt13.NewRow();
                    dr13["ValueMember"] = output[i][0];
                    dr13["DisplayMember"] = output[i][1];
                    dt13.Rows.Add(dr13);
                }
                cmb3.DataSource = dt13;
                cmb3.ValueMember = "ValueMember";
                cmb3.DisplayMember = "DisplayMember";
            }
        }
        private void AnalysisSearch_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(this, cmb1, cmb2, cmb3, tb1, tb2);
            ThisApplicationLoad.ComboBoxLoad(cmb2, cmb3, tb1, tb2);
            cmb2.SelectedIndexChanged += (sender1, e1) =>
            {
                ThisApplicationLoad.ComboBoxLoad(cmb2, cmb3, tb1, tb2);
            };
            tb1.TextChanged += (sender1, e1) =>
            {
                if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { return; }
                ThisApplicationLoad.ComboBoxLoad(cmb2, cmb3, tb1, tb2);
            };
            tb2.TextChanged += (sender1, e1) =>
            {
                if (!Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { return; }
                ThisApplicationLoad.ComboBoxLoad(cmb2, cmb3, tb1, tb2);
            };
            cmb3.SelectedValue = AnalysisChart.AnalysisChartData2.ToString();
        }
        private void b1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$") || !Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$")) { return; }
            AnalysisChart.AnalysisChartKind = cmb1.SelectedValue.ToString();
            AnalysisChart.AnalysisChartData1 = cmb2.SelectedValue.ToString();
            AnalysisChart.AnalysisChartData2 = cmb3.SelectedValue.ToString();
            AnalysisChart.AnalysisChartDay1 = tb1.Text;
            AnalysisChart.AnalysisChartDay2 = tb2.Text;
            FunSQL.SQLDML("SQLAnalysis0030", Analysis.SQLAnalysis0030, new string[] { }, new string[] { });
            AnalysisChart.SearchDuring = 0;
            if (cmb3.SelectedValue.ToString() == "0")
            {
                for (int i1 = 0; DateTime.Parse(tb1.Text).AddMonths(i1) < DateTime.Parse(tb2.Text).AddMonths(1); i1++)
                {
                    for (int i2 = 0; i2 < output.Length; i2++)
                    {
                        FunSQL.SQLDML("SQLAnalysis0010", Analysis.SQLAnalysis0010, new string[] { "@ANALYSISDATE", "@ANALYSISITEMID", "@ANALYSISITEMNAME", "@ANALYSISREMARKS" }, new string[] { DateTime.Parse(tb1.Text).AddMonths(i1).ToString("yyyy-MM-01"), output[i2][0], output[i2][1], $"{cmb2.Text}|{cmb3.Text}" });
                    }
                    AnalysisChart.SearchDuring += 1;
                }
            }
            else
            {
                for (int i = 0; DateTime.Parse(tb1.Text).AddMonths(i) < DateTime.Parse(tb2.Text).AddMonths(1); i++)
                {
                    FunSQL.SQLDML("SQLAnalysis0010", Analysis.SQLAnalysis0010, new string[] { "@ANALYSISDATE", "@ANALYSISITEMID", "@ANALYSISITEMNAME", "@ANALYSISREMARKS" }, new string[] { DateTime.Parse(tb1.Text).AddMonths(i).ToString("yyyy-MM-01"), cmb3.SelectedValue.ToString(), cmb3.Text, $"{cmb2.Text}" });
                    AnalysisChart.SearchDuring += 1;
                }
            }
            AnalysisChart.AcessCls.DataLoad();
            this.Close();
        }
    }
}
