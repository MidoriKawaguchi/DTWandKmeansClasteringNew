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
    public partial class ViewShootingVideo : Form
    {
        public Graph2 parent;
        private int shootNum = 0;
        EventHandler idleHandler;
        public int videoNum;
        //コンストラクタ
        //Graph2の変数さわれるように
        public ViewShootingVideo(Graph2 parent, int num)
        {
            this.parent = parent;
            this.videoNum = num;

            InitializeComponent();

            idleHandler = new EventHandler(IdleMethod);
            Application.Idle += idleHandler;
        }
        ~ViewShootingVideo()
        {
            Application.Idle -= idleHandler;

        }
        public void SetShootNum(int sn)
        {
            if (shootNum != sn)
            {
                shootNum = sn;
                try
                {
                    wmp.URL = "K:/CAM/Aside/!forC#/" + shootNum + ".MP4";
                }
                catch
                {
                }

            }
        }
        public const int fps = 300;
        public const int playtimes = fps / 60;       //300fpsだと再生時間は5倍になる
        //Trackbar動かすと相当する時刻の動画を再生
        public void TrackbarChanged(int frame)
        {
            wmp.Ctlcontrols.currentPosition = frame / (double)fps * playtimes;
        }

        private void IdleMethod(object sender, EventArgs e)
        {
            //curr:動作の再生時刻
            double curr = Math.Round(((wmp.Ctlcontrols.currentPosition) / playtimes), 3);
            label1.Text = curr.ToString();
            //Console.WriteLine("[IdleMethod] - " + curr);
            this.parent.TrackbarValueChange(this.videoNum, curr);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            wmp.Ctlcontrols.currentPosition = (double)numericUpDown1.Value;

            //for check
            // WMPLib.IWMPMedia tmp = wmp.currentMedia;
            //Console.WriteLine(tmp);
            //wmp.currentMedia.getItemInfo("FrameRate");
            //Console.WriteLine(tmp.getItemInfo("FrameRate"));
            //Console.WriteLine(tmp.isReadOnlyItem("FrameRate")); //trueでした→フレームレートは変えられない
            //tmp.setItemInfo("FrameRate", "300000");
        }
    }
}
