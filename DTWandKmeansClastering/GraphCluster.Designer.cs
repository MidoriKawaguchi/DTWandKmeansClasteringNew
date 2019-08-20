namespace DTWandKmeansClastering
{
    partial class GraphCluster
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.distNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.laShoot = new System.Windows.Forms.Label();
            this.udClusterB = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.udClusterA = new System.Windows.Forms.NumericUpDown();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.udClusterB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udClusterA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // distNum
            // 
            this.distNum.AutoSize = true;
            this.distNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distNum.Location = new System.Drawing.Point(902, 172);
            this.distNum.Name = "distNum";
            this.distNum.Size = new System.Drawing.Size(102, 42);
            this.distNum.TabIndex = 14;
            this.distNum.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(701, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 33);
            this.label2.TabIndex = 13;
            this.label2.Text = "distance";
            // 
            // laShoot
            // 
            this.laShoot.AutoSize = true;
            this.laShoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laShoot.Location = new System.Drawing.Point(700, 65);
            this.laShoot.Name = "laShoot";
            this.laShoot.Size = new System.Drawing.Size(171, 42);
            this.laShoot.TabIndex = 12;
            this.laShoot.Text = "Cluster B";
            // 
            // udClusterB
            // 
            this.udClusterB.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udClusterB.Location = new System.Drawing.Point(916, 63);
            this.udClusterB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udClusterB.Name = "udClusterB";
            this.udClusterB.Size = new System.Drawing.Size(110, 49);
            this.udClusterB.TabIndex = 11;
            this.udClusterB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udClusterB.ValueChanged += new System.EventHandler(this.UdClusterB_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(700, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 42);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cluster A";
            // 
            // udClusterA
            // 
            this.udClusterA.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udClusterA.Location = new System.Drawing.Point(916, 7);
            this.udClusterA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udClusterA.Name = "udClusterA";
            this.udClusterA.Size = new System.Drawing.Size(110, 49);
            this.udClusterA.TabIndex = 9;
            this.udClusterA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udClusterA.ValueChanged += new System.EventHandler(this.UdClusterA_ValueChanged);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, -5);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(690, 454);
            this.chart1.TabIndex = 8;
            this.chart1.TabStop = false;
            this.chart1.Text = "chart1";
            // 
            // GraphCluster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 461);
            this.Controls.Add(this.distNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.laShoot);
            this.Controls.Add(this.udClusterB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udClusterA);
            this.Controls.Add(this.chart1);
            this.Name = "GraphCluster";
            this.Text = "GraphCluster";
            ((System.ComponentModel.ISupportInitialize)(this.udClusterB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udClusterA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label distNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label laShoot;
        private System.Windows.Forms.NumericUpDown udClusterB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udClusterA;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}