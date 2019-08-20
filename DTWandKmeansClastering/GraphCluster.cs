using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DTWandKmeansClastering
{
    public partial class GraphCluster : Form
    {
        Kmeans kmeans;

        public GraphCluster(Kmeans k)
        {
            kmeans = k;
            InitializeComponent();
        }
        public void UpdateCluster(Kmeans k)
        {
            kmeans = k;
            udClusterA.Maximum = kmeans.clusters.Length - 1;
            udClusterA.Minimum = 0;
            udClusterB.Maximum = kmeans.clusters.Length - 1;
            udClusterB.Minimum = 0;
            SetData((int)udClusterA.Value, (int)udClusterB.Value);
        }
        public void SetData(int cidA, int cidB)
        {
            //クラスタ同士のコスト　格納
            double[] cost = DBA.CalcCost(kmeans.clusters[cidA].centroid.DBAacc, kmeans.clusters[cidB].centroid.DBAacc).cost;
            double dist = DBA.CalcCost(kmeans.clusters[cidA].centroid.DBAacc, kmeans.clusters[cidB].centroid.DBAacc).dist;

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // ChartにSeriesを追加します
            string legend1 = "X";
            string legend2 = "Y";
            string legend3 = "Z";
            string legend4 = "cost";
            chart1.Series.Add(legend1);
            chart1.Series.Add(legend2);
            chart1.Series.Add(legend3);
            chart1.Series.Add(legend4);

            chart1.Series[legend1].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart1.Series[legend2].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart1.Series[legend3].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart1.Series[legend4].ChartType = SeriesChartType.Line; // 折れ線グラフ

            chart1.ChartAreas.Add(new ChartArea("Area1"));
            chart1.ChartAreas["Area1"].AxisX.Title = "time";
            chart1.ChartAreas["Area1"].AxisY.Title = "degree";


            {
                //クラスタ１（基準）
                Cluster ca = kmeans.clusters[cidA];
                //クラスタ２（比較したいもの）
                Cluster cb = kmeans.clusters[cidB];

                //クラスタ間の距離（総コスト）
                distNum.Text = dist.ToString();
                //double dt = 0.1;
                //double[] dtCost = new double[s.acc.Length];
                for (int i = 0; i < ca.centroid.DBAacc.Length - 1; i++)
                {
                    chart1.Series[legend1].Points.AddXY(ca.centroid.DBAtime[i], ca.centroid.DBAacc[i].x); //X
                    chart1.Series[legend2].Points.AddXY(ca.centroid.DBAtime[i], ca.centroid.DBAacc[i].y); //Y
                    chart1.Series[legend3].Points.AddXY(ca.centroid.DBAtime[i], ca.centroid.DBAacc[i].z); //Z

                }

                for (int i = 0; i < ca.centroid.DBAtime.Length; i++)
                {
                    //costの軸は2軸目（右側のY軸目盛り）で表示
                    chart1.Series[legend4].YAxisType = AxisType.Secondary;
                    //caを基準にグラフを表示
                    chart1.Series[legend4].Points.AddXY(ca.centroid.DBAtime[i], cost[i]); //cost

                }
            }
        }

        private void UdClusterA_ValueChanged(object sender, EventArgs e)
        {
            UpdateCluster(kmeans);
        }

        private void UdClusterB_ValueChanged(object sender, EventArgs e)
        {
            UpdateCluster(kmeans);
        }
    }
}
