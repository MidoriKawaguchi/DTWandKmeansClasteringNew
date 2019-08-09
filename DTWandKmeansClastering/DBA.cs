using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DTWandKmeansClastering
{
    class DBA
    {
        public struct CostPath
        {
            public double[][] cost;
            public int[][] path;    //  0:左上から、1:左から、2:上から このマスに来るのが一番コストが低い
        }
        //  2つの波形のDTWを算出
        public static CostPath DpMatching(Vec3[] centroid, Vec3[] input)
        {
            int[] Cost_min_j = new int[centroid.Count()];
            double[][] Dist = new double[centroid.Count()][];
            double[][] Dist_diff = new double[centroid.Count()][];
            double[] CostMin = new double[centroid.Count()];
            double[,] distance = new double[centroid.Length, input.Length];
            int[][] path = new int[centroid.Length][];
            double ZurePenalty = 0.05;      //ずれのコスト

            //table output
            //string outputTable = null;

            //string outputfile = "C:\\Users\\MIDORI\\Documents\\Visual Studio 2017\\Projects\\DTW\\" + "CalcurateTableZure_" + A.ToString() + "_" + B.ToString() + ".csv";

            for (int i = 0; i < centroid.Count(); i++)
            {
                for (int j = 0; j < input.Count(); j++)
                {
                    distance[i, j] = Math.Sqrt((centroid[i][0] - input[j][0]) * (centroid[i][0] - input[j][0]) + (centroid[i][1] - input[j][1]) * (centroid[i][1] - input[j][1]) + (centroid[i][2] - input[j][2]) * (centroid[i][2] - input[j][2]));
                }
            }

            //コスト計算
            for (int i = 0; i < Dist.Length; i++)
            {
                Dist[i] = new double[input.Count()];
                path[i] = new int[input.Count()];

                //差分表示用
                Dist_diff[i] = new double[input.Count()];
            }
            for (int i = 0; i < Dist.Length; i++)
            {
                CostMin[i] = double.MaxValue;
            }
            double[] temp = new double[3];

            Dist[0][0] = distance[0, 0];//先頭文字同士のコスト

            for (int i = 1; i < centroid.Count(); i++)
            {
                //distance:distance between (x1,y1,z1)and(x2,y2,z2)
                Dist[i][0] = distance[i, 0] + Dist[i - 1][0] + ZurePenalty;     //たて一列のコストを算出
            }
            for (int j = 1; j < input.Count(); j++)
            {
                Dist[0][j] = distance[0, j] + Dist[0][j - 1] + ZurePenalty;     //横一列のコストを算出
            }

            for (int i = 1; i < centroid.Count(); i++)
            {
                for (int j = 1; j < input.Count(); j++)
                {
                    //  Dist[i][j]を計算
                    temp[0] = Dist[i - 1][j - 1];
                    //  横からの場合
                    temp[1] = Dist[i][j - 1] + ZurePenalty;
                    //  下から上の場合
                    temp[2] = Dist[i - 1][j] + ZurePenalty;
                    //  最もコストが少ないものを選ぶ
                    double min = Double.MaxValue;
                    int minPath = 0;
                    for (int m = 0; m < 3; ++m)
                    {
                        if (temp[m] < min)
                        {
                            min = temp[m];
                            minPath = m;
                        }
                    }
                    Dist[i][j] = distance[i, j] + min;


                    path[i][j] = minPath;

                    //差分表示用
                    Dist_diff[i][j] = distance[i, j];

                    if (Dist[i][j] < CostMin[i] || Dist[i][j] == CostMin[i] && i != j)
                    {
                        CostMin[i] = Dist[i][j];
                        Cost_min_j[i] = j;
                    }
                }
            }
            //CSV格納用
            //if ()
            //{
            //    string DPoutput = " ";
            //    string newfile = "DPmatching.csv";
            //    for (int i = 0; i < centroid.Count(); ++i)
            //    {
            //        for (int j = 0; j < input.Count(); ++j)
            //        {
            //            DPoutput = DPoutput + "," + Dist[i][j].ToString();
            //        }
            //        DPoutput = DPoutput + "/n";
            //    }
            //    File.WriteAllText(newfile, DPoutput);
            //}

            CostPath rv = new CostPath();
            rv.cost = Dist;
            rv.path = path;
            return rv;
        }

        //Shoots で直接渡してしまう

        //DBA(平均の波形)をとる
        public static DBAShoot CalcAverage(Shoots shoots, int numberOfUpdates)
        {
            //仮の平均を決める
            //最初の平均は1番目の時系列データe
            //仮の平均　DBAcentroid[n][lengthOfCentroid][axis]
            //時系列データ　seq[t][axis]
            //centroidを何回計算するか：　numberOfUpdates

            //後のKmeans呼び出しのため、ここで格納
            Cluster cluster = new Cluster();


            //最初のcentroidはshootnumber1番目の時系列データを代入
            //  時系列データの長さが十分にあるとき
            DBAShoot dba = new DBAShoot();
            dba.DBAacc = (Vec3[])shoots[0].acc.Clone();
            dba.DBAtime = (double[])shoots[0].time.Clone();


            List<Vec3>[] assocTab = Enumerable.Range(0, dba.DBAacc.Length).Select(i => new List<Vec3>()).ToArray();
            //時間格納用
            List<double>[] assocTabTime = Enumerable.Range(0, dba.DBAacc.Length).Select(i => new List<double>()).ToArray();




            for (int nu = 0; nu < numberOfUpdates; nu++)
            {
                string output = "centroidNum,DataNum,assocTab[i]x,y,z,assocTabTime[i],\r\n";

                //仮の平均と時系列データとの距離を求める
                foreach (Shoot s in shoots)
                {
                    string file = "assoctab_" + nu + "_"+ s.shootnumber + ".csv";

                    CostPath table = DpMatching(dba.DBAacc, s.acc);
                    int i = table.cost.Length - 1; //時系列データにおけるcentroidの数（固定）
                    int j = table.cost[0].Length - 1;//比較する時系列データの数（固定）（X,Y,Zともに共通）
                    while (i > 0 && j > 0)
                    {
                        //時系列データを対応付け
                        //assocTab[xyz][i] = 関連付けた仮の平均に対する時系列データ

                        assocTab[i].Add(s.acc[j]);
                        assocTabTime[i].Add(s.time[j]);

                        output += i + ", " + j + "," + s.acc[j].x + ", " + s.acc[j].y + ", " + s.acc[j].z + ", " + s.time[j] + "\r\n";

                        if (table.path[i][j] == 0)
                        {
                            //斜め
                            i--; j--;
                        }
                        else if (table.path[i][j] == 1)
                        {
                            //たて
                            j--;
                        }
                        else if (table.path[i][j] == 2)
                        {
                            //よこ
                            i--;
                        }
                    }
                    File.WriteAllText(file, output);
                }

            }


            int n = 0;
            foreach (List<Vec3> assoc in assocTab)
            {
                Vec3 asum = new Vec3();
                // + / はCluster.csでoperaterとして定義済み
                foreach (Vec3 a in assoc)
                {
                    asum += a;
                }
                if (assoc.Count > 0)
                {
                    asum /= assoc.Count;
                    //DBAの値　nはカウントのみ
                    dba.DBAacc[n] = asum;

                    //個別データとDBAcentroidとのコストの差を表示したい
                    //[対応するcentroidの数][何番目のデータ]:単位時間あたりのコストの増減
                    //dba.DBAtime[n] = assocTabTime[assoc.Count][n];  
                    //DBAの時刻は該当するデータの時間の平均
                }
                else if (n > 0)
                {
                    //比較　長さが不均等なときは直前の値を適用
                    dba.DBAacc[n] = dba.DBAacc[n - 1];
                }
                n++;
            }

            int m = 0;
            foreach (List<double> assoc in assocTabTime)
            {
                //DBAtime* 平均の時間を入れたい場合は以下
                //double tsum = 0;
                //foreach (double t in assoc)
                //{
                //    tsum = tsum + t;
                //}
                if (assoc.Count > 0)
                {
                    //DBAの時刻は該当するデータの時間の平均にしたい場合
                    //tsum = tsum / assoc.Count;
                    //dba.DBAtime[m] = tsum;

                    //下記　インデックスエラーを避けるため
                    //if (m < assoc.Count)
                    //{
                    dba.DBAtime[m] = m;
                    //}
                }
                //else if (m > 0)
                //{
                //    //比較　長さが不均等なときは直前の値を適用
                //    dba.DBAtime[m] = assoc[m - 1];
                //}

                m++;
            }
            return dba;

        }


        public struct DBACostPath
        {
            public double dist;
            public double[] cost;    //  0:左上から、1:左から、2:上から このマスに来るのが一番コストが低い
        }
        //他からもさわれるようにコスト計算をこちらに集約
        public static DBACostPath CalcCost(Vec3[] a, Vec3[] b)
        {
            CostPath cp = DpMatching(a, b);
#if false
            for (int i = 0; i < cp.cost.Length; ++i)
            {
                for (int k = 0; k < cp.cost[0].Length; ++k)
                {
                    System.Diagnostics.Debug.Write(cp.cost[i][k]);
                    System.Diagnostics.Debug.Write(" ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            System.Diagnostics.Debug.WriteLine("--");
            for (int i = 0; i < cp.cost.Length; ++i)
            {
                for (int k = 0; k < cp.cost[0].Length; ++k)
                {
                    System.Diagnostics.Debug.Write(cp.path[i][k]);
                    System.Diagnostics.Debug.Write(" ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            System.Diagnostics.Debug.WriteLine("-------------------------------------------");
#endif
            {
                double[] cost = new double[a.Length];
                int i = cp.path.Length - 1;
                int j = cp.path[0].Length - 1;
                double lastCost = cp.cost[cp.path.Length - 1][cp.path[0].Length - 1];
                while (i > 0 && j > 0)
                {
                    //cp.path[i][j] :  0:斜め、1:左、2:上 が前の位置
                    int time = 0;
                    while (cp.path[i][j] == 1 && j > 0)  // 
                    {
                        j--;
                        time++;
                    }
                    if (cp.path[i][j] == 0 && j > 0) //斜め
                    {
                        i--; j--;
                        time++;
                    }                       //下
                    else
                    {
                        i--;
                        time++;
                    }
                    //単位時間あたりのコストの増減
                    cost[i + 1] = (lastCost - cp.cost[i][j]) / time;
                    System.Diagnostics.Debug.WriteLine("cost :" + i + "  " + cost[i + 1]);
                    //直前のコストとの差を算出するため
                    lastCost = cp.cost[i][j];
                }
                cost[0] = 0;

                DBACostPath cc = new DBACostPath();

                cc.dist = cp.cost[cp.path.Length - 1][cp.path[0].Length - 1];
                cc.cost = cost;
                return cc;
            }
        }
    }
}
