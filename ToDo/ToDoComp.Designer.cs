namespace ToDo
{
    partial class ToDoComp
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
            this.tb2 = new System.Windows.Forms.TextBox();
            this.l4 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l3 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.b1 = new System.Windows.Forms.Button();
            this.p3 = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Label();
            this.p1 = new System.Windows.Forms.Panel();
            this.cmb2 = new System.Windows.Forms.ComboBox();
            this.cmb1 = new System.Windows.Forms.ComboBox();
            this.p2 = new System.Windows.Forms.Panel();
            this.p1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(18, 240);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(250, 30);
            this.tb2.TabIndex = 3;
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
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 280);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 4;
            this.b1.Text = "確定";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click_1);
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.Gainsboro;
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(1150, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(0, 702);
            this.p3.TabIndex = 17;
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
            this.p1.TabIndex = 18;
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
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p2.Dock = System.Windows.Forms.DockStyle.Left;
            this.p2.Location = new System.Drawing.Point(300, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(850, 702);
            this.p2.TabIndex = 18;
            // 
            // ToDoComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ToDoComp";
            this.Size = new System.Drawing.Size(966, 702);
            this.Load += new System.EventHandler(this.ToDoComp_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.ComboBox cmb2;
        private System.Windows.Forms.ComboBox cmb1;
        private System.Windows.Forms.Panel p2;
    }
}
