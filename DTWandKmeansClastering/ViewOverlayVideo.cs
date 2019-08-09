using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DTWandKmeansClastering
{
    public partial class ViewOverlayVideo : Form
    {
        EventHandler idleHandler;
        public ViewOverlayVideo()
        {
            InitializeComponent();

            idleHandler = new EventHandler(IdleMethod);
            Application.Idle += idleHandler;
        }
        ~ViewOverlayVideo()
        {
            Application.Idle -= idleHandler;

        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            e.VlcLibDirectory = new DirectoryInfo(@"C:\Program Files\VideoLAN\VLC");
        }

        public void SetVideo(int a,int b)
        {
            if (a !=  b)
            {
                
                try
                {
                    vlcControl.Play(new Uri("C:/Users/MIDORI/Source/Repos/DTWandKmeansClastering/DTWandKmeansClastering/bin/Debug/overlay" + a + "_" + b + ".MP4"));
                }
                catch
                {
                }

            }
        }
        private void IdleMethod(object sender, EventArgs e)
        {
            //curr:動作の再生時刻
            //double time1 = Math.Round((() / ViewShootingVideo.playtimes), 3);
            //double time2 = Math.Round((() / ViewShootingVideo.playtimes), 3);
            //txMovie1time.Text = time1.ToString();
            //txMovie2time.Text = time2.ToString();

        }
    }
}
