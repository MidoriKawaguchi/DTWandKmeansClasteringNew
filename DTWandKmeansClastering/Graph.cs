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
    public partial class Graph : Form
    {
        Kmeans kmeans;

        public Graph(Kmeans k)
        {
            kmeans = k;
            InitializeComponent();
        }

        public void UpdateKmeans(Kmeans k)
        {
            kmeans = k;
            udClusterId.Maximum = kmeans.clusters.Length - 1;
            udClusterId.Minimum = 0;
            udShoot.Minimum = 0;
            udShoot.Maximum = kmeans.clusters[(int)udClusterId.Value].members.Count;
            SetData((int)udClusterId.Value, (int)udShoot.Value);
        }
        public void SetData(int cid, int sid)
        {

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

            //クラスタ型から拾ってくる
            //Centroid のグラフ出力 sid == 0
            if (sid == 0)
            {
                Cluster c = kmeans.clusters[cid];
                for (int i = 0; i < c.centroid.DBAacc.Length; i++)
                {
                    chart1.Series[legend1].Points.AddY(c.centroid.DBAacc[i].x); //X
                    chart1.Series[legend2].Points.AddY(c.centroid.DBAacc[i].y); //Y
                    chart1.Series[legend3].Points.AddY(c.centroid.DBAacc[i].z); //Z
                }
                laShoot.Text = "Centroid";
                laShootNum.Text = "";
                distNum.Text = " ";
            }
            //各クラスタに属する射の入力データのグラフ出力　Centroidに距離が近い順（Sort済み）
            else
            {
                //クラスタ番号cidのクラスタ
                Cluster c = kmeans.clusters[cid];
                //クラスタ番号cidのメンバー　
                Shoot s = c.members[sid - 1];
                DBAShoot dba = c.centroid;
                laShoot.Text = "Shoot";
                laShootNum.Text = s.shootnumber.ToString();
                distNum.Text = DBA.CalcCost(s.acc, dba.DBAacc).dist.ToString();
                //double dt = 0.1;
                //double[] dtCost = new double[s.acc.Length];
                for (int i = 0; i < s.acc.Length - 1; i++)
                {
                    chart1.Series[legend1].Points.AddXY(s.time[i], s.acc[i].x); //X
                    chart1.Series[legend2].Points.AddXY(s.time[i], s.acc[i].y); //Y
                    chart1.Series[legend3].Points.AddXY(s.time[i], s.acc[i].z); //Z

                }

                //costのグラフも表示
                for (int i = 0; i < s.cost.Length; i++)
                {
                    ////chart1.Series[legend4].Points.AddY(s.path[i]); //cost
                    //dtCost[i] = (s.cost[i] - s.cost[i - 1]) / dt;
                    //dtCost[i] = s.cost[i];
                    //costの軸は2軸目（右側のY軸目盛り）で表示
                    double[] DBAcost = DBA.CalcCost(s.acc, dba.DBAacc).cost;

                    chart1.Series[legend4].YAxisType = AxisType.Secondary;
                    //chart1.Series[legend4].Points.AddXY(dba.DBAtime[i], s.cost[i]); //cost
                    chart1.Series[legend4].Points.AddXY(s.time[i], DBAcost[i]); //cost
                    

                }
            }
        }

        private void UdClusterId_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }

        private void UdShoot_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }
    }
}
