namespace Schedule
{
    partial class ScheduleSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleSchedule));
            this.p2 = new System.Windows.Forms.Panel();
            this.b1 = new System.Windows.Forms.Button();
            this.l1 = new System.Windows.Forms.Label();
            this.b4 = new System.Windows.Forms.Button();
            this.b3 = new System.Windows.Forms.Button();
            this.b2 = new System.Windows.Forms.Button();
            this.p1 = new System.Windows.Forms.Panel();
            this.b5 = new System.Windows.Forms.Button();
            this.p1.SuspendLayout();
            this.SuspendLayout();
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.Transparent;
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.Location = new System.Drawing.Point(0, 51);
            this.p2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(1000, 949);
            this.p2.TabIndex = 7;
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.FlatAppearance.BorderSize = 0;
            this.b1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b1.ForeColor = System.Drawing.Color.White;
            this.b1.Image = ((System.Drawing.Image)(resources.GetObject("b1.Image")));
            this.b1.Location = new System.Drawing.Point(120, 7);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(90, 40);
            this.b1.TabIndex = 5;
            this.b1.Text = "日別";
            this.b1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b1.UseVisualStyleBackColor = false;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(82, 23);
            this.l1.TabIndex = 1;
            this.l1.Text = "$Name2";
            // 
            // b4
            // 
            this.b4.BackColor = System.Drawing.Color.Transparent;
            this.b4.FlatAppearance.BorderSize = 0;
            this.b4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b4.ForeColor = System.Drawing.Color.White;
            this.b4.Image = ((System.Drawing.Image)(resources.GetObject("b4.Image")));
            this.b4.Location = new System.Drawing.Point(408, 7);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(90, 40);
            this.b4.TabIndex = 4;
            this.b4.Text = "検索";
            this.b4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b4.UseVisualStyleBackColor = false;
            this.b4.Click += new System.EventHandler(this.b4_Click);
            // 
            // b3
            // 
            this.b3.BackColor = System.Drawing.Color.Transparent;
            this.b3.FlatAppearance.BorderSize = 0;
            this.b3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b3.ForeColor = System.Drawing.Color.White;
            this.b3.Image = ((System.Drawing.Image)(resources.GetObject("b3.Image")));
            this.b3.Location = new System.Drawing.Point(312, 7);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(90, 40);
            this.b3.TabIndex = 2;
            this.b3.Text = "リスト";
            this.b3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b3.UseVisualStyleBackColor = false;
            this.b3.Click += new System.EventHandler(this.b3_Click);
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.FlatAppearance.BorderSize = 0;
            this.b2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b2.ForeColor = System.Drawing.Color.White;
            this.b2.Image = ((System.Drawing.Image)(resources.GetObject("b2.Image")));
            this.b2.Location = new System.Drawing.Point(216, 7);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(90, 40);
            this.b2.TabIndex = 3;
            this.b2.Text = "週間";
            this.b2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b2.UseVisualStyleBackColor = false;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // p1
            // 
            this.p1.AutoScroll = true;
            this.p1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.p1.Controls.Add(this.b5);
            this.p1.Controls.Add(this.b1);
            this.p1.Controls.Add(this.l1);
            this.p1.Controls.Add(this.b4);
            this.p1.Controls.Add(this.b3);
            this.p1.Controls.Add(this.b2);
            this.p1.Dock = System.Windows.Forms.DockStyle.Top;
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(1000, 51);
            this.p1.TabIndex = 6;
            // 
            // b5
            // 
            this.b5.BackColor = System.Drawing.Color.Transparent;
            this.b5.FlatAppearance.BorderSize = 0;
            this.b5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b5.ForeColor = System.Drawing.Color.White;
            this.b5.Image = ((System.Drawing.Image)(resources.GetObject("b5.Image")));
            this.b5.Location = new System.Drawing.Point(504, 7);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(90, 40);
            this.b5.TabIndex = 7;
            this.b5.Text = "読込";
            this.b5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b5.UseVisualStyleBackColor = false;
            this.b5.Click += new System.EventHandler(this.b5_Click);
            // 
            // ScheduleSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScheduleSchedule";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.Load += new System.EventHandler(this.ScheduleSchedule_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Button b4;
        private System.Windows.Forms.Button b3;
        private System.Windows.Forms.Button b2;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Button b5;
    }
}
