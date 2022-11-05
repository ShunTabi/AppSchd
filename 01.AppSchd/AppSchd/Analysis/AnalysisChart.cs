using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Analysis
{
    public partial class AnalysisChart : UserControl
    {
        public AnalysisChart()
        {
            InitializeComponent();
        }
        public static int SearchDuring = 0;
        public static string AnalysisChartKind = "0";
        public static string AnalysisChartData1 = "0";
        public static string AnalysisChartData2 = "0";
        public static string AnalysisChartDay1 = DateTime.Now.AddMonths(-6).ToString("yyyy-MM");
        public static string AnalysisChartDay2 = DateTime.Now.ToString("yyyy-MM");
        private static Chart ch;
        class ThisApplicationSetup
        {
            private static void CreateChart(UserControl uc)
            {
                ch = new Chart();
                ch.Dock = DockStyle.Fill;
                ch.BackColor = Color.LightSteelBlue;
                uc.Controls.Add(ch);
            }
            public static void ThisApplicationSetupExec(UserControl uc)
            {
                CreateChart(uc);
            }
        }
        class ThisApplicationLoad
        {
            private static string[][] output = null;
            private static void GetData()
            {
                if (AnalysisChartData1 == "0")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0003", Analysis.SQLAnalysis0003, new string[] { }, new string[] { });
                }
                else if (AnalysisChartData1 == "1")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0004", Analysis.SQLAnalysis0004, new string[] { }, new string[] { });
                }
                else if (AnalysisChartData1 == "2")
                {
                    output = FunSQL.SQLSELECT("SQLAnalysis0005", Analysis.SQLAnalysis0005, new string[] { }, new string[] { });
                }
            }
            private static void LoadChart()
            {
                ch.ChartAreas.Clear();
                ch.Series.Clear();
                ch.Legends.Clear();
                ch.ChartAreas.Add(new ChartArea("Area"));
                ch.ChartAreas["Area"].BackColor = Color.SkyBlue;
                ch.Palette = ChartColorPalette.BrightPastel;
                string LegendName = "";
                for (int i = 0; i < output.Length; i++)
                {
                    if (i % SearchDuring == 0)
                    {
                        LegendName = output[i][2];
                        ch.Series.Add(LegendName);
                        ch.Series[LegendName].IsVisibleInLegend = false;
                        //ch.Legends.Add(LegendName);
                    }
                    if (AnalysisChartKind == "0")
                    {
                        ch.Series[LegendName].ChartType = SeriesChartType.StackedColumn;
                        //ch.Series[LegendName].ChartType = SeriesChartType.StackedBar;
                    }
                    else if (AnalysisChartKind == "1")
                    {
                        ch.Series[LegendName].ChartType = SeriesChartType.Line;
                        ch.Series[LegendName].ChartType = SeriesChartType.Spline;
                        ch.Series[LegendName].MarkerStyle = MarkerStyle.Circle;
                        ch.Series[LegendName].MarkerSize = 8;
                    }
                    else if (AnalysisChartKind == "2")
                    {
                        ch.Series[LegendName].ChartType = SeriesChartType.StackedArea100;
                    }
                    else if (AnalysisChartKind == "3")
                    {
                        ch.Series[LegendName].ChartType = SeriesChartType.Radar;
                        ch.Series[LegendName].MarkerStyle = MarkerStyle.Circle;
                        ch.Series[LegendName].MarkerSize = 8;
                    }
                    ch.Series[LegendName].ToolTip = LegendName;
                    ch.Series[LegendName].Font = new Font("Meiryo UI", 8, FontStyle.Regular);
                    ch.Series[LegendName].Label = LegendName;
                    ch.Series[LegendName].LabelForeColor = Color.White;
                    ch.Series[LegendName].Points.AddXY(output[i][0], output[i][3]);
                }
            }
            public static void ThisApplicationLoadExec()
            {
                GetData();
                LoadChart();
            }
        }
        private void AnalysisChart_Load(object sender, EventArgs e)
        {
            ThisApplicationSetup.ThisApplicationSetupExec(this);
        }
        public class AcessCls
        {
            public static void DataLoad()
            {
                ThisApplicationLoad.ThisApplicationLoadExec();
            }
        }
    }
}
