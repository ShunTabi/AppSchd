namespace ToDo
{
    partial class ToDoInComp
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
            this.b1 = new System.Windows.Forms.Button();
            this.p1 = new System.Windows.Forms.Panel();
            this.cmb2 = new System.Windows.Forms.ComboBox();
            this.cmb1 = new System.Windows.Forms.ComboBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.l4 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l3 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.p4 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.p6 = new System.Windows.Forms.Panel();
            this.l10 = new System.Windows.Forms.Label();
            this.l11 = new System.Windows.Forms.Label();
            this.p5 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
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
            this.l1.Size = new System.Drawing.Size(82, 23);
            this.l1.TabIndex = 0;
            this.l1.Text = "目標名：";
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 280);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 4;
            this.b1.Tag = "";
            this.b1.Text = "確定";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.LightBlue;
            this.p1.Controls.Add(this.cmb2);
            this.p1.Controls.Add(this.cmb1);
            this.p1.Controls.Add(this.tb2);
            this.p1.Controls.Add(this.l4);
            this.p1.Controls.Add(this.tb1);
            this.p1.Controls.Add(this.l3);
            this.p1.Controls.Add(this.l2);
            this.p1.Controls.Add(this.b1);
            this.p1.Controls.Add(this.l1);
            this.p1.Dock = System.Windows.Forms.DockStyle.Left;
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(300, 702);
            this.p1.TabIndex = 21;
            // 
            // cmb2
            // 
            this.cmb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb2.Font = new System.Drawing.Font("Meiryo UI", 8.5F);
            this.cmb2.FormattingEnabled = true;
            this.cmb2.Location = new System.Drawing.Point(18, 108);
            this.cmb2.Name = "cmb2";
            this.cmb2.Size = new System.Drawing.Size(250, 30);
            this.cmb2.TabIndex = 1;
            this.cmb2.Tag = "";
            // 
            // cmb1
            // 
            this.cmb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb1.Font = new System.Drawing.Font("Meiryo UI", 8.5F);
            this.cmb1.FormattingEnabled = true;
            this.cmb1.Location = new System.Drawing.Point(18, 42);
            this.cmb1.Name = "cmb1";
            this.cmb1.Size = new System.Drawing.Size(250, 30);
            this.cmb1.TabIndex = 0;
            this.cmb1.Tag = "";
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(18, 240);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(250, 30);
            this.tb2.TabIndex = 3;
            this.tb2.Tag = "";
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Location = new System.Drawing.Point(14, 214);
            this.l4.Name = "l4";
            this.l4.Size = new System.Drawing.Size(64, 23);
            this.l4.TabIndex = 10;
            this.l4.Text = "完了日";
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 174);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 2;
            this.tb1.Tag = "";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Location = new System.Drawing.Point(14, 148);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(90, 23);
            this.l3.TabIndex = 8;
            this.l3.Text = "ToDo名：";
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(14, 84);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(82, 23);
            this.l2.TabIndex = 6;
            this.l2.Text = "計画名：";
            // 
            // p4
            // 
            this.p4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p4.Controls.Add(this.p5);
            this.p4.Controls.Add(this.l11);
            this.p4.Dock = System.Windows.Forms.DockStyle.Left;
            this.p4.Location = new System.Drawing.Point(1080, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(780, 702);
            this.p4.TabIndex = 20;
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p2.Controls.Add(this.p3);
            this.p2.Controls.Add(this.l10);
            this.p2.Dock = System.Windows.Forms.DockStyle.Left;
            this.p2.Location = new System.Drawing.Point(300, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(780, 702);
            this.p2.TabIndex = 21;
            // 
            // p6
            // 
            this.p6.BackColor = System.Drawing.Color.Gainsboro;
            this.p6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p6.Location = new System.Drawing.Point(1860, 0);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(0, 702);
            this.p6.TabIndex = 21;
            // 
            // l10
            // 
            this.l10.AutoSize = true;
            this.l10.Dock = System.Windows.Forms.DockStyle.Top;
            this.l10.Location = new System.Drawing.Point(0, 0);
            this.l10.Name = "l10";
            this.l10.Size = new System.Drawing.Size(72, 23);
            this.l10.TabIndex = 0;
            this.l10.Text = "▼ToDo";
            // 
            // l11
            // 
            this.l11.AutoSize = true;
            this.l11.Dock = System.Windows.Forms.DockStyle.Top;
            this.l11.Location = new System.Drawing.Point(0, 0);
            this.l11.Name = "l11";
            this.l11.Size = new System.Drawing.Size(73, 23);
            this.l11.TabIndex = 1;
            this.l11.Text = "▼Done";
            // 
            // p5
            // 
            this.p5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p5.Location = new System.Drawing.Point(0, 23);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(780, 679);
            this.p5.TabIndex = 2;
            // 
            // p3
            // 
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(0, 23);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(780, 679);
            this.p3.TabIndex = 3;
            // 
            // ToDoComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p6);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ToDoComp";
            this.Size = new System.Drawing.Size(966, 702);
            this.Load += new System.EventHandler(this.ToDoInComp_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p4.ResumeLayout(false);
            this.p4.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.ComboBox cmb2;
        private System.Windows.Forms.ComboBox cmb1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p6;
        private System.Windows.Forms.Label l10;
        private System.Windows.Forms.Label l11;
        private System.Windows.Forms.Panel p5;
        private System.Windows.Forms.Panel p3;
    }
}
