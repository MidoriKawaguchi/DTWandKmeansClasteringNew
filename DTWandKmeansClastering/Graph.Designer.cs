namespace DTWandKmeansClastering
{
    partial class Graph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.clusteridTex = new System.Windows.Forms.Label();
            this.laShoot = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.udClusterId = new System.Windows.Forms.NumericUpDown();
            this.udShoot = new System.Windows.Forms.NumericUpDown();
            this.laShootNum = new System.Windows.Forms.Label();
            this.laDist = new System.Windows.Forms.Label();
            this.distNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udClusterId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udShoot)).BeginInit();
            this.SuspendLayout();
            // 
            // clusteridTex
            // 
            this.clusteridTex.AutoSize = true;
            this.clusteridTex.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clusteridTex.Location = new System.Drawing.Point(632, 72);
            this.clusteridTex.Name = "clusteridTex";
            this.clusteridTex.Size = new System.Drawing.Size(175, 42);
            this.clusteridTex.TabIndex = 0;
            this.clusteridTex.Text = "Cluster id";
            // 
            // laShoot
            // 
            this.laShoot.AutoSize = true;
            this.laShoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laShoot.Location = new System.Drawing.Point(611, 145);
            this.laShoot.Name = "laShoot";
            this.laShoot.Size = new System.Drawing.Size(116, 42);
            this.laShoot.TabIndex = 1;
            this.laShoot.Text = "Shoot";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(32, 38);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(573, 376);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // udClusterId
            // 
            this.udClusterId.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udClusterId.Location = new System.Drawing.Point(835, 72);
            this.udClusterId.Name = "udClusterId";
            this.udClusterId.Size = new System.Drawing.Size(76, 49);
            this.udClusterId.TabIndex = 3;
            this.udClusterId.ValueChanged += new System.EventHandler(this.UdClusterId_ValueChanged);
            // 
            // udShoot
            // 
            this.udShoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udShoot.Location = new System.Drawing.Point(835, 138);
            this.udShoot.Name = "udShoot";
            this.udShoot.Size = new System.Drawing.Size(76, 49);
            this.udShoot.TabIndex = 4;
            this.udShoot.ValueChanged += new System.EventHandler(this.UdShoot_ValueChanged);
            // 
            // laShootNum
            // 
            this.laShootNum.AutoSize = true;
            this.laShootNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laShootNum.Location = new System.Drawing.Point(728, 145);
            this.laShootNum.Name = "laShootNum";
            this.laShootNum.Size = new System.Drawing.Size(81, 42);
            this.laShootNum.TabIndex = 5;
            this.laShootNum.Text = "123";
            this.laShootNum.UseWaitCursor = true;
            // 
            // laDist
            // 
            this.laDist.AutoSize = true;
            this.laDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laDist.Location = new System.Drawing.Point(626, 219);
            this.laDist.Name = "laDist";
            this.laDist.Size = new System.Drawing.Size(303, 33);
            this.laDist.TabIndex = 7;
            this.laDist.Text = "distance from centroid";
            // 
            // distNum
            // 
            this.distNum.AutoSize = true;
            this.distNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distNum.Location = new System.Drawing.Point(793, 252);
            this.distNum.Name = "distNum";
            this.distNum.Size = new System.Drawing.Size(102, 42);
            this.distNum.TabIndex = 8;
            this.distNum.Text = "1234";
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 504);
            this.Controls.Add(this.distNum);
            this.Controls.Add(this.laDist);
            this.Controls.Add(this.laShootNum);
            this.Controls.Add(this.udShoot);
            this.Controls.Add(this.udClusterId);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.laShoot);
            this.Controls.Add(this.clusteridTex);
            this.Name = "Graph";
            this.Text = "Graph";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udClusterId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udShoot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clusteridTex;
        private System.Windows.Forms.Label laShoot;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.NumericUpDown udClusterId;
        private System.Windows.Forms.NumericUpDown udShoot;
        private System.Windows.Forms.Label laShootNum;
        private System.Windows.Forms.Label laDist;
        private System.Windows.Forms.Label distNum;
    }
}