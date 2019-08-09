using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTWandKmeansClastering
{
    public partial class Form1 : Form
    {
        //files:入力時系列データ　csvファイル、時系列でxyz 射数分あり
        string[] files;
        //data:射毎のデータ
        //filenumber:入力時系列データfilesのファイル識別番号
        //保存するクラスのインスタンス生成
        Shoots inputshoots = new Shoots();
        DBAShoots dbashoots = new DBAShoots();
        Kmeans kmeans = new Kmeans();
        List<Graph> graphs = new List<Graph>();

        List<Graph2> graphs2 = new List<Graph2>();

        List<GraphCluster> Cgraphs = new List<GraphCluster>(); //クラスタ間差異表示グラフ用
        public Form1()
        {
            InitializeComponent();

            //グラフ用のフォーム呼び出し
            //Kmeansの結果も使用するため引き渡し
            Graph g = new Graph(kmeans);
            graphs.Add(g);
            g.Show();


            Graph2 g2 = new Graph2(kmeans);
            graphs2.Add(g2);
            g2.Show();
        }

        private void DataInput_Click(object sender, EventArgs e)
        {
            //時系列データをcsvファイルから取り込む
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.Filter = "CSV|*.csv";
            if (o.ShowDialog() == DialogResult.OK)
            {
                files = o.FileNames;
                MessageBox.Show("Select" + files.Length + "files");
            }
            else
            {
                MessageBox.Show("Open file Error");
            }
            for (int s = 0; s < files.Length; s++)
            {
                string file = files[s];
                Shoot inputshoot = Shoot.Load(file);
                inputshoots.Add(inputshoot);
            }
        }

        private void Kmean_Click(object sender, EventArgs e)
        {
            kmeans.Kmean(inputshoots,int.Parse(kNum.Text),int.Parse(numberOfUpdatesNum.Text));
            
            //to callback in DBA-DBA distance
            // dbashoots = kmeans.getShoot();

            foreach (Graph g in graphs)
            {
                g.UpdateKmeans(kmeans);
            }
        }

        private void BtCentriudDist_Click(object sender, EventArgs e)
        {
            //他のクラスタとの距離を求める
            int k = int.Parse(kNum.Text);

            for (int i = 0; i < dbashoots.Count(); i++)
            {
                //自身のクラスタ以外
                if (i != k)
                {
                    DBA.CostPath clusterDPtable = DBA.DpMatching(dbashoots[k].DBAacc, dbashoots[i].DBAacc);
                    Console.WriteLine("clusterDPtable : " + i + "    " + clusterDPtable.cost[k][i].ToString());
                }
            }

            GraphCluster gc = new GraphCluster(kmeans);
            Cgraphs.Add(gc);
            gc.Show();

        }
    }
}
