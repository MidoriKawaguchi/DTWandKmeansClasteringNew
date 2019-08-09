using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace DTWandKmeansClastering
{
    public class Vec3
    {
        public double x, y, z;
        public double this[int i]
        {
            get
            {
                if (i == 0) return x;
                else if (i == 1) return y;
                else return z;
            }
            set
            {
                if (i == 0) x = value;
                else if (i == 1) y = value;
                else z = value;
            }
        }
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            Vec3 rv = new Vec3();
            rv.x = a.x + b.x;
            rv.y = a.y + b.y;
            rv.z = a.z + b.z;
            return rv;
        }
        public static Vec3 operator /(Vec3 a, double b)
        {
            Vec3 rv = new Vec3();
            rv.x = a.x / b;
            rv.y = a.y / b;
            rv.z = a.z / b;
            return rv;
        }
        public Vec3 Clone()
        {
            Vec3 rv = new Vec3();
            rv.x = x;
            rv.y = y;
            rv.z = z;
            return rv;
        }
    }
    public class Cluster
    {
        public Shoots members;
        public Shoots prevMembers;
        public DBAShoot centroid;
        public double distFromCentroid; //クラスタの中心からの距離　クラスタークラスタ間用
        public Cluster()
        {
            members = new Shoots();
            prevMembers = new Shoots();
            centroid = new DBAShoot();
        }
        public Cluster(Cluster c)
        {
            members = new Shoots();
            prevMembers = new Shoots();
            centroid = new DBAShoot();
            foreach (Shoot s in c.members)
            {
                members.Add(s);
            }
            centroid.DBAacc = new Vec3[c.centroid.DBAacc.Length];
            for (int i = 0; i < c.centroid.DBAacc.Length; ++i)
            {
                centroid.DBAacc[i] = c.centroid.DBAacc[i].Clone();
            }
        }
    }

    public class Shoot : ICloneable
    {
        //double acc[t][axis]
        public Vec3[] acc;
        public int shootnumber;
        public double distFromCentroid; //クラスタの中心からの距離
        public int[] path;//cost
        public double[] cost;
        public double[] time; //acc time

        //Cloneの関数です　浅いクローンなので注意
        public Object Clone()
        {
            Shoot s = new Shoot();
            s.acc = (Vec3[])acc.Clone();
            s.shootnumber = shootnumber;
            s.distFromCentroid = distFromCentroid;
            s.path = (int[])path.Clone();
            s.cost = (double[])cost.Clone();
            s.time = (double[])time.Clone();
            return s;
        }
        public static Shoot Load(string file)
        {
            string[] line;
            // ファイル毎に行を読み込む
            string[] data = File.ReadAllLines(file);
            //  data.Length: 射の総数　＝　labels.Count

            Shoot inputshoot = new Shoot();
            //ファイル番号（shoot number）は保持
            inputshoot.shootnumber = int.Parse(Regex.Replace(Path.GetFileNameWithoutExtension(file), @"[^0-9]", ""));


            //コンストラクタ　便利な記述
            inputshoot.acc = Enumerable.Range(0, data.Length - 1).Select(i => new Vec3()).ToArray();
            inputshoot.time = Enumerable.Range(0, data.Length - 1).Select(i => double.MinValue).ToArray();

            for (int i = 0; i < data.Length - 1; i++)
            {
                line = data[i + 1].Split(',');
                //2次元配列であれば配列毎にサイズを定義する必要があるため、ここではListを使用
                //データ格納用のためのList

                // line.Length : ラベル系列の長さ　射によって異なる
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        //time
                        inputshoot.time[i] = double.Parse(line[j]);
                    }

                    else if (j == 1)
                    {
                        //X
                        inputshoot.acc[i].x = double.Parse(line[j]);
                        //Console.WriteLine("Xcurr" + line[j]);
                    }
                    else if (j == 2)
                    {
                        //Y
                        inputshoot.acc[i].y = double.Parse(line[j]);
                        //Console.WriteLine("Ycurr" + line[j]);
                    }
                    else if (j == 3)
                    {
                        //Z
                        inputshoot.acc[i].z = double.Parse(line[j]);
                        //Console.WriteLine("Zcurr" + line[j]);

                    }
                }

                //label[i]:i射目のラベル系列　label[i][j]はi射目、j番目のラベル系列 

            }
            return inputshoot;
        }
    }
    public class Shoots : List<Shoot>
    {
    }
    public class DBAShoot
    {
        //to callback in DBA-DBA distance
        //public DBAShoot(Vec3[] dbaacc, double[] DBAtime)
        //{

        //    //clone
        //    this.DBAacc = dbaacc;
        //    this.DBAtime = DBAtime;
        //}

        public Vec3[] DBAacc;
        public double[] DBAtime; //acc time
    }
    public class DBAShoots : List<DBAShoot>
    {
    }
}
