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
using System.IO;

namespace DTWandKmeansClastering
{
    public partial class Graph2 : Form
    {
        Kmeans kmeans;
        public Graph2(Kmeans k)
        {
            kmeans = k;
            InitializeComponent();
        }
        public void UpdateKmeans(Kmeans k)
        {
            kmeans = k;
            udClusterId1.Maximum = kmeans.clusters.Length - 1;
            udClusterId1.Minimum = 0;
            udShoot1.Minimum = 0;
            udShoot1.Maximum = kmeans.clusters[(int)udClusterId1.Value].members.Count;
            SetData((int)udClusterId1.Value, (int)udShoot1.Value);

            udClusterId2.Maximum = kmeans.clusters.Length - 1;
            udClusterId2.Minimum = 0;
            udShoot2.Minimum = 0;
            udShoot2.Maximum = kmeans.clusters[(int)udClusterId2.Value].members.Count;
            SetData2((int)udClusterId2.Value, (int)udShoot2.Value);
            SetCost((int)udClusterId1.Value, (int)udShoot1.Value, (int)udClusterId2.Value, (int)udShoot2.Value);
        }

        public void SetCost(int c1, int s1, int c2, int s2)
        {
            if (s1 == 0 || s2 == 0) return;
            Shoot[] shoots = new Shoot[2];
            shoots[0] = kmeans.clusters[c1].members[s1 - 1];
            shoots[1] = kmeans.clusters[c2].members[s2 - 1];

            //DBA.CostPath DPtable = DBA.DpMatching(inputshoots[shoot1].acc, inputshoots[shoot2].acc);
            // DBA.CalcCost(a,b) aからみたｂのコスト　長さはa
            double[][] costs = new double[2][];
            Chart[] charts = { chart1, chart2 };

            for (int c = 0; c < 2; ++c)
            {
                //shoot[0] and shoot[1] compare 
                //cost[0]=(0,1) cost[1]=(1,0)
                costs[c] = DBA.CalcCost(shoots[c].acc, shoots[(c + 1) % 2].acc).cost;
                string legend4 = "cost";
                charts[c].Series.Add(legend4);
                charts[c].Series[legend4].ChartType = SeriesChartType.Line; // 折れ線グラフ

                for (int i = 0; i < shoots[c].acc.Length; i++)
                {
                    //shoot1における、shoot2からみたコスト
                    //shoot2における、shoot1からみたコスト
                    charts[c].Series[legend4].YAxisType = AxisType.Secondary;
                    charts[c].Series[legend4].Points.AddXY(shoots[c].time[i], costs[c][i]); //cost
                    Console.WriteLine("cost i :" + i + "  time:" + shoots[c].time[i]);
                }
            }
        }
        public void SetData(int cid, int sid)
        {

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // ChartにSeriesを追加します
            string legend1 = "X";
            string legend2 = "Y";
            string legend3 = "Z";

            chart1.Series.Add(legend1);
            chart1.Series.Add(legend2);
            chart1.Series.Add(legend3);


            chart1.Series[legend1].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart1.Series[legend2].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart1.Series[legend3].ChartType = SeriesChartType.Line; // 折れ線グラフ

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
                laShoot1.Text = "Centroid";
                laShootNum1.Text = "";
            }
            //各クラスタに属する射の入力データのグラフ出力　Centroidに距離が近い順（Sort済み）
            else
            {
                //  //クラスタ番号cidのクラスタ
                Cluster c = kmeans.clusters[cid];
                //クラスタ番号cidのメンバー　
                Shoot s = c.members[sid - 1];
                DBAShoot dba = c.centroid;
                laShoot1.Text = "Shoot";
                laShootNum1.Text = s.shootnumber.ToString();
                if (video[0] != null) video[0].SetShootNum(s.shootnumber);

                for (int i = 0; i < s.acc.Length - 1; i++)
                {
                    chart1.Series[legend1].Points.AddXY(s.time[i], s.acc[i].x); //X
                    chart1.Series[legend2].Points.AddXY(s.time[i], s.acc[i].y); //Y
                    chart1.Series[legend3].Points.AddXY(s.time[i], s.acc[i].z); //Z
                }

                //Setupのとき
                trackBar1.Minimum = (int)s.time[0] * ViewShootingVideo.fps;
                trackBar1.Maximum = (int)s.time[s.acc.Length - 1] * ViewShootingVideo.fps;
                System.Diagnostics.Debug.WriteLine("video1 t0=" + s.time[0] + " tn=" + s.time[s.acc.Length - 1]);
            }

        }
        public void SetData2(int cid, int sid)
        {
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();

            // ChartにSeriesを追加します
            string legend1 = "X";
            string legend2 = "Y";
            string legend3 = "Z";

            chart2.Series.Add(legend1);
            chart2.Series.Add(legend2);
            chart2.Series.Add(legend3);


            chart2.Series[legend1].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart2.Series[legend2].ChartType = SeriesChartType.Line; // 折れ線グラフ
            chart2.Series[legend3].ChartType = SeriesChartType.Line; // 折れ線グラフ

            chart2.ChartAreas.Add(new ChartArea("Area2"));
            chart2.ChartAreas["Area2"].AxisX.Title = "time";
            chart2.ChartAreas["Area2"].AxisY.Title = "degree";

            //クラスタ型から拾ってくる
            //Centroid のグラフ出力 sid == 0
            if (sid == 0)
            {
                Cluster c = kmeans.clusters[cid];
                for (int i = 0; i < c.centroid.DBAacc.Length; i++)
                {
                    chart2.Series[legend1].Points.AddY(c.centroid.DBAacc[i].x); //X
                    chart2.Series[legend2].Points.AddY(c.centroid.DBAacc[i].y); //Y
                    chart2.Series[legend3].Points.AddY(c.centroid.DBAacc[i].z); //Z
                }
                laShoot2.Text = "Centroid";
                laShootNum2.Text = "";
            }
            //各クラスタに属する射の入力データのグラフ出力　Centroidに距離が近い順（Sort済み）
            else
            {
                //クラスタ番号cidのクラスタ
                Cluster c = kmeans.clusters[cid];
                //クラスタ番号cidのメンバー　
                Shoot s = c.members[sid - 1];
                DBAShoot dba = c.centroid;
                laShoot2.Text = "Shoot";
                laShootNum2.Text = s.shootnumber.ToString();

                for (int i = 0; i < s.acc.Length - 1; i++)
                {
                    chart2.Series[legend1].Points.AddXY(s.time[i], s.acc[i].x); //X
                    chart2.Series[legend2].Points.AddXY(s.time[i], s.acc[i].y); //Y
                    chart2.Series[legend3].Points.AddXY(s.time[i], s.acc[i].z); //Z

                }
                trackBar2.Minimum = (int)s.time[0] * ViewShootingVideo.fps;
                trackBar2.Maximum = (int)s.time[s.acc.Length - 1] * ViewShootingVideo.fps;
                System.Diagnostics.Debug.WriteLine("video2 t0=" + s.time[0] + " tn=" + s.time[s.acc.Length - 1]);
            }
        }
        private void UdClusterId1_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }

        private void UdShoot1_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }

        private void UdClusterId2_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }

        private void UdShoot2_ValueChanged(object sender, EventArgs e)
        {
            UpdateKmeans(kmeans);
        }

        ViewShootingVideo[] video = { null, null };
        private void BtShowVideo1_Click(object sender, EventArgs e)
        {
            //shooting videoの表示（比較する２射同時）
            ShowVideo(0);
            ShowVideo(1);
        }

        private void ShowVideo(int i)
        {
            if (video[i] == null)
            {
                //インスタンス化
                //Graph2のインスタンスの中に存在
                video[i] = new ViewShootingVideo(this, i);
            }
            video[i].Show();
            video[i].Activate();    //上層
            int[] sn = { int.Parse(laShootNum1.Text), int.Parse(laShootNum2.Text) };
            video[i].SetShootNum(sn[i]);
            video[i].Text = "Shoot" + sn[i];
        }

        ViewOverlayVideo overlayVideo;
        private void ShowOverlayVideo(int a,int b)
        {
            overlayVideo = new ViewOverlayVideo();
            overlayVideo.Show();
            overlayVideo.Activate();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //Console.WriteLine("Current frame No.: " + trackBar1.Value);
            if (video[0] != null) video[0].TrackbarChanged(trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (video[1] != null) video[1].TrackbarChanged(trackBar2.Value);
        }

        public void TrackbarValueChange(int num, double currtime)
        {
            int value = (int)(currtime * ViewShootingVideo.fps);
            //Console.WriteLine("[TrackbarValueChange] - " + (int)currtime * ViewShootingVideo.fps);

            //トラックバーの時間のminmun maximumはSetup/Eiming/Releaseの始まりと終わりの時間
            //該当する時間以外は　トラックバーは始まり及び終わりの隅に留まる
            //shoot1
            if (num == 0)
            {
                value = value <= trackBar1.Minimum ? trackBar1.Minimum : value;
                value = value >= trackBar1.Maximum ? trackBar1.Maximum : value;
                trackBar1.Value = value;
            }
            //shoot2
            else if (num == 1)
            {
                value = value <= trackBar2.Minimum ? trackBar2.Minimum : value;
                value = value >= trackBar2.Maximum ? trackBar2.Maximum : value;
                trackBar2.Value = value;
            }
        }

        public void BtOverlayVideo_Click(object sender, EventArgs e)
        {
            //該当フェーズ部分をオーバーレイして表示する

            //System.Diagnostics.Process p = new System.Diagnostics.Process();

            //とりあえずパスを通した
            string directoryName1 = "K:\\CAM\\Aside\\!forC#\\" + laShootNum1.Text + ".MP4";
            string directoryName2 = "K:\\CAM\\Aside\\!forC#\\" + laShootNum2.Text + ".MP4";

            double assocTimeAStart = -1;
            double assocTimeAEnd = -1;
            double assocTimeBStart = -1;
            double assocTimeBEnd = -1;

            //2つの射の重ね合わせ動画は、対応するAssocTabTimeで合わせる
            StreamReader sr1 = new StreamReader("assoctab_3_" + laShootNum1.Text + ".csv");
            int assocTimeALength = (File.ReadAllLines("assoctab_3_" + laShootNum1.Text + ".csv")).Length;

            int lineACount = 0;
            while (!sr1.EndOfStream)
            {
                string line = sr1.ReadLine(); //CSVの1行読み込み
                string[] values = line.Split(',');//カンマに分け配列に格納
                //はじめの時刻を取りに行く
                lineACount++;

                if (lineACount == 2)
                {
                    assocTimeAEnd = Double.Parse(values[5]);
                }
                else if (lineACount == assocTimeALength)
                {
                    assocTimeAStart = Double.Parse(values[5]);
                }
            }

            StreamReader sr2 = new StreamReader("assoctab_3_" + laShootNum2.Text + ".csv");
            int assocTimeBLength = (File.ReadAllLines("assoctab_3_" + laShootNum2.Text + ".csv")).Length;
            int lineBCount = 0;
            while (!sr2.EndOfStream)
            {
                string line = sr2.ReadLine(); //CSVの1行読み込み
                string[] values = line.Split(',');//カンマに分け配列に格納
                lineBCount++;
                //はじめの時刻を取りに行く
                if (lineBCount == 2)
                {
                    assocTimeBEnd = Double.Parse(values[5]);
                }
                else if (lineBCount == assocTimeBLength)
                {
                    assocTimeBStart = Double.Parse(values[5]);
                }
            }


            //再生時間が300fpsなら5倍になっている　/300*60 をする

            if(assocTimeAStart > 0 && assocTimeAEnd > 0 && assocTimeBStart >0 && assocTimeBEnd > 0)
            {
                string video1starttime = (assocTimeAStart * ViewShootingVideo.playtimes).ToString();
                string video1cutLength = ((assocTimeAEnd - assocTimeAStart) * ViewShootingVideo.playtimes).ToString();
                string video2strattime = (assocTimeBStart * ViewShootingVideo.playtimes).ToString();
                string video2cutLength = ((assocTimeBEnd - assocTimeBStart) * ViewShootingVideo.playtimes).ToString();

                //p.StartInfo.FileName = "ffmpeg";

                //-y :強制上書きオプション
                // [ " を書く必要があるため　\" で　”　を表しています]
                // -ss:切り出しはじめの時刻 -t:切り出す範囲の時刻 -i:入力 
                //blend filter takes two input streams and outputs one stream, the first input is the "top" layer and second input is "bottom" layer
                //overlayフィルタだけでは透過にならず、blendを使う必要がある
                //[0:0]1つ目の動画   [1:0]2つ目の動画
                //blend=all_mode=difference : 差分を表示　blend=c0_mode=average：普通に重畳

                string[] paras0 =
                {
                    //ffmpeg.exeのある場所
                    //出力ファイルもこちらにされる
                    " cd C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug"
                };

                //string[] paras = { "-y -ss", video1starttime, " -t", video1cutLength, " -i" ,directoryName1, " -ss",
                //     video2strattime, " -t", video2cutLength, "-i", directoryName2 ,
                //    "-filter_complex \"[0:0]split[shoot1][shoot2]; [shoot1] [1:0] overlay[overlay]; [overlay] [shoot2] blend=all_mode=difference \" -map 1:1 overlay"+ laShootNum1.Text + "_" + laShootNum2.Text  +".MP4"
                //    };

                //string[] paras = { "ffmpeg -y -i " + directoryName1 +  " -filter_complex  \"[0:v]colorchannelmixer=0:0:0:0.9:1:0:0:0.9:1:0:0:0.9[colorchannelmixed];[colorchannelmixed]eq=1:0.3:1:1:1:1:1:1[color_effect]\" -map [color_effect] -c:v libx264 -c:a copy output_video1.MP4"
                //      + " && ffmpeg -y -i " + directoryName2 + " -filter_complex \"[0:v]colorchannelmixer=1:0:0:0:0:0:0:0:0:0:0:0[colorchannelmixed];[colorchannelmixed]eq=1:1:1:1:1:1:1:1[color_effect]\" -map [color_effect] -c:v libx264 -c:a copy output_video2.MP4"
                //         + " && ffmpeg -y -i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video1.MP4 - i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video2.MP4 - filter_complex \"blend=all_opacity=0.7\" " + laShootNum1.Text + "_" + laShootNum2.Text + ".MP4"
                //};
                // "/C ffmpeg

                //https://nico-lab.net/edgedetect_with_ffmpeg/

                string[] paras1 = { "-y -ss " + video1starttime + " -t " + video1cutLength + " -i " + directoryName1 +  " -filter_complex \"[0:v] edgedetect=10/255:50/255:wires[edge],[edge]colorchannelmixer=0:0:0:0.9:1:0:0:0.9:1:0:0:0.9[colorchannelmixed];[colorchannelmixed]eq=1:0.3:1:1:1:1:1:1[color_effect]\" -map [color_effect] -c:v libx264 -c:a copy output_video" + laShootNum1.Text +".MP4"
                };

                //add" -ignore_chapter 1" if you have output trable 
                string[] paras2 = { "-y -ss " + video2strattime + " -t " + video2cutLength + " -i " + directoryName2 +  " -filter_complex \"[0:v] edgedetect=10/255:50/255:wires[edge],[edge]colorchannelmixer=1:0:0:0.9:0:0:0:0.9:0:0:0:0.9[colorchannelmixed];[colorchannelmixed]eq=1:0.3:1:1:1:1:1:1[color_effect]\" -map [color_effect] -c:v libx264 -c:a copy output_video"+ laShootNum2.Text +".MP4"
                };

                string[] paras3 =
                {
                    "-y -i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video" + laShootNum2.Text +".MP4 -i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video"+ laShootNum1.Text +".MP4 -filter_complex \"blend=all_opacity=0.7\" " + "blue"+ laShootNum1.Text + "_" +"red"+ laShootNum2.Text + ".MP4"
                    //"-i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video" + laShootNum2.Text +".MP4 -i C:\\Users\\MIDORI\\Source\\Repos\\DTWandKmeansClastering\\DTWandKmeansClastering\\bin\\Debug\\output_video"+ laShootNum1.Text +".MP4 -filter_complex \"[0:0]split[shoot1][shoot2]; [shoot1] [1:0] overlay[overlay]; [overlay] [shoot2] blend=all_mode=difference \" -map 1:1 overlay"+ laShootNum1.Text + "_" + laShootNum2.Text  +".MP4"
                };


                //https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.synchronizingobject?view=netframework-4.8#System_Diagnostics_Process_SynchronizingObject
                //for (int cn = 1; cn < 2; cn++)
                //{
#if false
                   System.Diagnostics.Process p = new System.Diagnostics.Process();
                //string para = "paras" + cn;
                    p.StartInfo.FileName = "cmd.exe";
                    //cmd// p.StartInfo.FileName = "cmd.exe";
;                   //cmd// p.StartInfo.FileName = "cmd.exe";
                    //Console.WriteLine(string.Join(" ", paras1));
                    //p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    //cmd//p.StartInfo.Arguments = string.Join(" ", paras1);
                    //cmd//p.StartInfo.Arguments = string.Join(p.StartInfo.Arguments, paras2);
                    //cmd//p.StartInfo.Arguments = string.Join(p.StartInfo.Arguments, paras3);
                    p.StartInfo.Arguments = string.Join("  /K", paras1);
                    p.StartInfo.UseShellExecute = false;        //don't use the operating system shell
                    p.StartInfo.RedirectStandardOutput = true;  //put output directly in Process.StandardOutput
                    p.StartInfo.RedirectStandardInput = true;   //put input directly in Process.StandardOutput
                    p.StartInfo.CreateNoWindow = true;          //don't make new Window


                    try
                    {
                        p.Start();
                        //p.Exited += new EventHandler(MyProcessExited);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
#else


#endif
                //batファイルと文字列の組み合わせで実行できなかったため、中身を文字列にして実行することに
                System.Diagnostics.Process p = System.Diagnostics.Process.Start("cmd.exe", paras0[0]);
                //System.Diagnostics.Process p = System.Diagnostics.Process.Start("ff.bat");
                //p.WaitForExit();

                //    p.Dispose();
                //}

                System.Diagnostics.Process p2 = new System.Diagnostics.Process();
                p2.StartInfo.FileName = "ffmpeg";
                p2.StartInfo.Arguments = string.Join("", paras1[0]);
                p2.StartInfo.UseShellExecute = false;        //don't use the operating system shell
                p2.StartInfo.RedirectStandardOutput = true;  //put output directly in Process.StandardOutput
                p2.StartInfo.RedirectStandardInput = true;   //put input directly in Process.StandardOutput
                p2.StartInfo.CreateNoWindow = true;          //don't make new Window


                try
                {
                    p2.Start();
                    p2.WaitForExit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                p2.Dispose();


                System.Diagnostics.Process p3 = new System.Diagnostics.Process();
                p3.StartInfo.FileName = "ffmpeg";
                p3.StartInfo.Arguments = string.Join("", paras2[0]);
                p3.StartInfo.UseShellExecute = false;        //don't use the operating system shell
                p3.StartInfo.RedirectStandardOutput = true;  //put output directly in Process.StandardOutput
                p3.StartInfo.RedirectStandardInput = true;   //put input directly in Process.StandardOutput
                p3.StartInfo.CreateNoWindow = true;          //don't make new Window


                try
                {
                    p3.Start();
                    p3.WaitForExit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                p3.Dispose();

                System.Diagnostics.Process p4 = new System.Diagnostics.Process();
                p4.StartInfo.FileName = "ffmpeg";
                p4.StartInfo.Arguments = string.Join("", paras3[0]);
                p4.StartInfo.UseShellExecute = false;        //don't use the operating system shell
                p4.StartInfo.RedirectStandardOutput = true;  //put output directly in Process.StandardOutput
                p4.StartInfo.RedirectStandardInput = true;   //put input directly in Process.StandardOutput
                p4.StartInfo.CreateNoWindow = true;          //don't make new Window


                try
                {
                    p4.Start();
                    p4.WaitForExit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                p4.Dispose();

                //cmd上での動作が終るまでプロセスpは閉じない
                //p.WaitForExit();
                p.Dispose();

                ShowOverlayVideo(int.Parse(laShootNum1.Text), int.Parse(laShootNum2.Text));

                MessageBox.Show("Output video finished");



            }
            else
            {
                MessageBox.Show("assocTime Read Error");
            }


           
        }
        private void MyProcessExited(Object source, EventArgs e)
        {
            MessageBox.Show("The process has exited.");
        }

        private void BtSavePng_Click(object sender, EventArgs e)
        {
            chart1.SaveImage( laShootNum1 + ".png", ChartImageFormat.Png);
            chart2.SaveImage( laShootNum2 + ".png", ChartImageFormat.Png);
        }
    }
}
