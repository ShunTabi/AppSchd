namespace Note
{
    partial class NoteRecord
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
            this.l1 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.b1 = new System.Windows.Forms.Button();
            this.l2 = new System.Windows.Forms.Label();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.p1 = new System.Windows.Forms.Panel();
            this.p4 = new System.Windows.Forms.Panel();
            this.p5 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
            this.p6 = new System.Windows.Forms.Panel();
            this.p1.SuspendLayout();
            this.p4.SuspendLayout();
            this.p2.SuspendLayout();
            this.SuspendLayout();
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(99, 23);
            this.l1.TabIndex = 0;
            this.l1.Text = "タイトル名：";
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 42);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 0;
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(16, 468);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 3;
            this.b1.Text = "確定";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(14, 84);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(68, 23);
            this.l2.TabIndex = 3;
            this.l2.Text = "ノート：";
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(18, 108);
            this.tb2.Multiline = true;
            this.tb2.Name = "tb2";
            this.tb2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb2.Size = new System.Drawing.Size(250, 350);
            this.tb2.TabIndex = 1;
            this.tb2.Enter += new System.EventHandler(this.tb2_Enter);
            this.tb2.Leave += new System.EventHandler(this.tb2_Leave);
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.LightBlue;
            this.p1.Controls.Add(this.tb2);
            this.p1.Controls.Add(this.l2);
            this.p1.Controls.Add(this.b1);
            this.p1.Controls.Add(this.tb1);
            this.p1.Controls.Add(this.l1);
            this.p1.Dock = System.Windows.Forms.DockStyle.Left;
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(300, 1000);
            this.p1.TabIndex = 20;
            // 
            // p4
            // 
            this.p4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p4.Controls.Add(this.p5);
            this.p4.Dock = System.Windows.Forms.DockStyle.Left;
            this.p4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p4.Location = new System.Drawing.Point(940, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(640, 1000);
            this.p4.TabIndex = 25;
            // 
            // p5
            // 
            this.p5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p5.Location = new System.Drawing.Point(0, 0);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(640, 1000);
            this.p5.TabIndex = 26;
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p2.Controls.Add(this.p3);
            this.p2.Dock = System.Windows.Forms.DockStyle.Left;
            this.p2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p2.Location = new System.Drawing.Point(300, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(640, 1000);
            this.p2.TabIndex = 24;
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p3.Location = new System.Drawing.Point(0, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(640, 1000);
            this.p3.TabIndex = 25;
            // 
            // p6
            // 
            this.p6.BackColor = System.Drawing.Color.Gainsboro;
            this.p6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p6.Location = new System.Drawing.Point(1580, 0);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(0, 1000);
            this.p6.TabIndex = 26;
            // 
            // NoteRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p6);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NoteRecord";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.Load += new System.EventHandler(this.NoteOneDay_Load);
            this.Resize += new System.EventHandler(this.NoteOneDay_Resize);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p4.ResumeLayout(false);
            this.p2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p6;
        private System.Windows.Forms.Panel p5;
        private System.Windows.Forms.Panel p3;
    }
}
