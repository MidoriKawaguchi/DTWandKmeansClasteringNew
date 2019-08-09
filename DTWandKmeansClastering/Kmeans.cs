using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DTWandKmeansClastering
{
    public class Kmeans
    {
        public Shoots allShoots;
        public Cluster[] clusters;

        //to callback in DBA-DBA distance
        //private DBAShoots myDBShoots;
        int numberOfDbaUpdates;

        bool clusteringFinished;
        public void Kmean(Shoots s, int knumber, int numberOfDbaUpdatesIn)
        {
            numberOfDbaUpdates = numberOfDbaUpdatesIn;
            allShoots = s;
            clusters = Enumerable.Range(0, knumber).Select(i => new Cluster()).ToArray();
            MakeCentroid();
            //update : TURE クラスタが更新されなかった場合

            //myDBShoots = new DBAShoots();

            clusteringFinished = false;
            while (!clusteringFinished)
            {
                Update();
            }
            CalcFinalDistance();
            MessageBox.Show("clustering finished");
        }
        public void MakeCentroid()
        {
            Shoots members = new Shoots();
            members.AddRange(allShoots);
            int clsLen = members.Count / clusters.Length;

            //エルボー法評価のため
            double distSumAllShoots = 0;
            string distSumAllShootsOutput = "distSumAllShoots.csv";

            foreach (Cluster c in clusters)
            {
                c.centroid.DBAacc = (Vec3[])members[0].acc.Clone();
                c.members.Add(members[0]);
                for (int i = 1; i < clsLen; ++i)
                {
                    double minDist = double.MaxValue;
                    Shoot minS = null;


                    foreach (Shoot s in members)
                    {
                        DBA.CostPath cp;
                        cp = DBA.DpMatching(s.acc, c.centroid.DBAacc);

                        //to callback in DBA-DBA distance
                        //myDBShoots.Add(new DBAShoot());

                        //最終コストが距離
                        double dist = cp.cost[cp.cost.Length - 1][cp.cost[cp.cost.Length - 1].Length - 1];
                        s.distFromCentroid = dist;

                        //エルボー法評価のため全ての距離の和を求める
                        distSumAllShoots += dist;
                        if (dist < minDist)
                        {
                            minDist = dist;
                            minS = s;
                        }
                    }


                    members.Remove(minS);
                    c.members.Add(minS);
                }
            }
            File.WriteAllText(distSumAllShootsOutput, distSumAllShoots.ToString());
        }
        public void Update()
        {
            UpdateCentroid();
            UpdateMember();
            SortMember();
        }

        //public DBAShoots getShoot()
        //{
        //   return myDBShoots;
        //}

        public void UpdateCentroid()
        {
            //クラスタごとにDBAグラフを出力
            int clustersNumber = 1;
            foreach (Cluster c in clusters)
            {
                string centroidoutput = "centroidoutput" + clustersNumber + ".csv";
                c.centroid = DBA.CalcAverage(c.members, numberOfDbaUpdates);
                //centroid保存用
                string centroid = "";
                for (int i = 0; i < c.centroid.DBAacc.Length; ++i)
                {
                    //グラフの横軸は時刻ではなく何番目のデータか　であることに注意
                    centroid += i + "," + c.centroid.DBAacc[i].x + "," + c.centroid.DBAacc[i].y + "," + c.centroid.DBAacc[i].z + "\r\n";
                }
                File.WriteAllText(centroidoutput, centroid);
                clustersNumber++;
            }
        }
        public void UpdateMember()
        {
            //クラスタの更新
            foreach (Cluster c in clusters)
            {
                c.prevMembers.Clear();
                c.prevMembers.AddRange(c.members);
                c.members.Clear();
            }
            foreach (Shoot s in allShoots)
            {
                double minDist = double.MaxValue;
                Cluster minCluster = null;
                foreach (Cluster c in clusters)
                {
                    double[][] cost = DBA.DpMatching(s.acc, c.centroid.DBAacc).cost;
                    double dist = cost[cost.Length - 1][cost[cost.Length - 1].Length - 1];
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minCluster = c;
                    }
                }
                minCluster.members.Add(s);
            }

            //クラスタが更新されない場合
            clusteringFinished = true;
            foreach (Cluster c in clusters)
            {
                //積集合が一致しないとき　＝＞　クラスタ更新は続ける
                IEnumerable<Shoot> intersect = c.members.Intersect(c.prevMembers);
                if (intersect.Count() != c.members.Count())
                {
                    clusteringFinished = false;
                }
            }

        }
        //distance が短い順にソート
        static int CompareShootByDistanceFromCentroid(Shoot x, Shoot y)
        {
            double diff = x.distFromCentroid - y.distFromCentroid;
            if (diff > 0) return 1;
            if (diff < 0) return -1;
            return 0;
        }
        void SortMember()
        {
            Array.Sort(clusters, (a, b) => b.members.Count - a.members.Count);
            foreach (Cluster c in clusters)
            {
                c.members.Sort(CompareShootByDistanceFromCentroid);
            }

            //表示用
            string clusteroutput = "cluster_result.csv";
            string output = "cluster , shootnumber r\n";
            for (int i = 0; i < clusters.Count(); ++i)
            {
                Console.WriteLine(" cluster : " + i);
                output += i;

                for (int j = 0; j < clusters[i].members.Count(); ++j)
                {
                    Console.WriteLine(" shootnumber : " + clusters[i].members[j].shootnumber);
                    output += "," + clusters[i].members[j].shootnumber + "\r\n";
                }
            }
            File.WriteAllText(clusteroutput, output);
        }
        void CalcFinalDistance()
        {
            foreach (Cluster c in clusters)
            {
                foreach (Shoot s in c.members)
                {
                    //コスト計算
                    s.cost = DBA.CalcCost(s.acc, c.centroid.DBAacc).cost;
                }
            }
        }
    }
}
