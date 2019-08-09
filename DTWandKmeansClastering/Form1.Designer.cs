namespace DTWandKmeansClastering
{
    partial class Form1
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
            this.dataInput = new System.Windows.Forms.Button();
            this.kmean = new System.Windows.Forms.Button();
            this.numberOfUpdate = new System.Windows.Forms.Label();
            this.knumber = new System.Windows.Forms.Label();
            this.numberOfUpdatesNum = new System.Windows.Forms.TextBox();
            this.kNum = new System.Windows.Forms.TextBox();
            this.btCentriudDist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataInput
            // 
            this.dataInput.Location = new System.Drawing.Point(35, 27);
            this.dataInput.Name = "dataInput";
            this.dataInput.Size = new System.Drawing.Size(140, 29);
            this.dataInput.TabIndex = 0;
            this.dataInput.Text = "shootdata input";
            this.dataInput.UseVisualStyleBackColor = true;
            this.dataInput.Click += new System.EventHandler(this.DataInput_Click);
            // 
            // kmean
            // 
            this.kmean.Location = new System.Drawing.Point(35, 156);
            this.kmean.Name = "kmean";
            this.kmean.Size = new System.Drawing.Size(140, 30);
            this.kmean.TabIndex = 1;
            this.kmean.Text = "Kmeans";
            this.kmean.UseVisualStyleBackColor = true;
            this.kmean.Click += new System.EventHandler(this.Kmean_Click);
            // 
            // numberOfUpdate
            // 
            this.numberOfUpdate.AutoSize = true;
            this.numberOfUpdate.Location = new System.Drawing.Point(35, 92);
            this.numberOfUpdate.Name = "numberOfUpdate";
            this.numberOfUpdate.Size = new System.Drawing.Size(97, 13);
            this.numberOfUpdate.TabIndex = 2;
            this.numberOfUpdate.Text = "number of Updates";
            // 
            // knumber
            // 
            this.knumber.AutoSize = true;
            this.knumber.Location = new System.Drawing.Point(35, 125);
            this.knumber.Name = "knumber";
            this.knumber.Size = new System.Drawing.Size(13, 13);
            this.knumber.TabIndex = 3;
            this.knumber.Text = "k";
            // 
            // numberOfUpdatesNum
            // 
            this.numberOfUpdatesNum.Location = new System.Drawing.Point(148, 89);
            this.numberOfUpdatesNum.Name = "numberOfUpdatesNum";
            this.numberOfUpdatesNum.Size = new System.Drawing.Size(100, 20);
            this.numberOfUpdatesNum.TabIndex = 4;
            // 
            // kNum
            // 
            this.kNum.Location = new System.Drawing.Point(148, 122);
            this.kNum.Name = "kNum";
            this.kNum.Size = new System.Drawing.Size(100, 20);
            this.kNum.TabIndex = 5;
            // 
            // btCentriudDist
            // 
            this.btCentriudDist.Location = new System.Drawing.Point(35, 222);
            this.btCentriudDist.Name = "btCentriudDist";
            this.btCentriudDist.Size = new System.Drawing.Size(140, 24);
            this.btCentriudDist.TabIndex = 6;
            this.btCentriudDist.Text = "view Distance ";
            this.btCentriudDist.UseVisualStyleBackColor = true;
            this.btCentriudDist.Click += new System.EventHandler(this.BtCentriudDist_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 393);
            this.Controls.Add(this.btCentriudDist);
            this.Controls.Add(this.kNum);
            this.Controls.Add(this.numberOfUpdatesNum);
            this.Controls.Add(this.knumber);
            this.Controls.Add(this.numberOfUpdate);
            this.Controls.Add(this.kmean);
            this.Controls.Add(this.dataInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dataInput;
        private System.Windows.Forms.Button kmean;
        private System.Windows.Forms.Label numberOfUpdate;
        private System.Windows.Forms.Label knumber;
        private System.Windows.Forms.TextBox numberOfUpdatesNum;
        private System.Windows.Forms.TextBox kNum;
        private System.Windows.Forms.Button btCentriudDist;
    }
}

