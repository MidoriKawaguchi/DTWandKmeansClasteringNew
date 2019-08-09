namespace DTWandKmeansClastering
{
    partial class ViewOverlayVideo
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
            this.label2 = new System.Windows.Forms.Label();
            this.txMovie1time = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txMovie2time = new System.Windows.Forms.Label();
            this.vlcControl = new Vlc.DotNet.Forms.VlcControl();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(388, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Movie1  Time";
            // 
            // txMovie1time
            // 
            this.txMovie1time.AutoSize = true;
            this.txMovie1time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txMovie1time.Location = new System.Drawing.Point(490, 409);
            this.txMovie1time.Name = "txMovie1time";
            this.txMovie1time.Size = new System.Drawing.Size(84, 17);
            this.txMovie1time.TabIndex = 7;
            this.txMovie1time.Text = "Movie1Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(388, 459);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Movie2 Time";
            // 
            // txMovie2time
            // 
            this.txMovie2time.AutoSize = true;
            this.txMovie2time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txMovie2time.Location = new System.Drawing.Point(490, 459);
            this.txMovie2time.Name = "txMovie2time";
            this.txMovie2time.Size = new System.Drawing.Size(84, 17);
            this.txMovie2time.TabIndex = 9;
            this.txMovie2time.Text = "Movie2Time";
            // 
            // vlcControl
            // 
            this.vlcControl.BackColor = System.Drawing.Color.Black;
            this.vlcControl.Location = new System.Drawing.Point(12, 0);
            this.vlcControl.Name = "vlcControl";
            this.vlcControl.Size = new System.Drawing.Size(615, 397);
            this.vlcControl.Spu = -1;
            this.vlcControl.TabIndex = 11;
            this.vlcControl.VlcLibDirectory = null;
            this.vlcControl.VlcMediaplayerOptions = null;
            // 
            // ViewOverlayVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 524);
            this.Controls.Add(this.vlcControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txMovie2time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txMovie1time);
            this.Name = "ViewOverlayVideo";
            this.Text = "ViewOverlayVideo";
            //((System.ComponentModel.ISupportInitialize)(this.vlcControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txMovie1time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txMovie2time;
        private Vlc.DotNet.Forms.VlcControl vlcControl;
    }
}