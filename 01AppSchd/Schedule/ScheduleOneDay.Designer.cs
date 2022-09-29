namespace Schedule
{
    partial class ScheduleOneDay
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleOneDay));
            this.p5 = new System.Windows.Forms.Panel();
            this.p1 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.b1 = new System.Windows.Forms.Button();
            this.p4 = new System.Windows.Forms.Panel();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            this.SuspendLayout();
            // 
            // p5
            // 
            this.p5.BackColor = System.Drawing.Color.Gainsboro;
            this.p5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p5.Location = new System.Drawing.Point(1150, 0);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(0, 1000);
            this.p5.TabIndex = 11;
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.LightBlue;
            this.p1.Controls.Add(this.p3);
            this.p1.Controls.Add(this.p2);
            this.p1.Dock = System.Windows.Forms.DockStyle.Left;
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(300, 1000);
            this.p1.TabIndex = 12;
            // 
            // p3
            // 
            this.p3.AutoScroll = true;
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(0, 60);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(300, 940);
            this.p3.TabIndex = 15;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.b1);
            this.p2.Dock = System.Windows.Forms.DockStyle.Top;
            this.p2.Location = new System.Drawing.Point(0, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(300, 60);
            this.p2.TabIndex = 14;
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.FlatAppearance.BorderSize = 0;
            this.b1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b1.ForeColor = System.Drawing.Color.White;
            this.b1.Image = ((System.Drawing.Image)(resources.GetObject("b1.Image")));
            this.b1.Location = new System.Drawing.Point(14, 16);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(40, 40);
            this.b1.TabIndex = 5;
            this.b1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b1.UseVisualStyleBackColor = false;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // p4
            // 
            this.p4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p4.Dock = System.Windows.Forms.DockStyle.Left;
            this.p4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p4.Location = new System.Drawing.Point(300, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(850, 1000);
            this.p4.TabIndex = 16;
            // 
            // ScheduleOneDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p5);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScheduleOneDay";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.Load += new System.EventHandler(this.ScheduleOneDay_Load);
            this.p1.ResumeLayout(false);
            this.p2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel p5;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p4;
    }
}
