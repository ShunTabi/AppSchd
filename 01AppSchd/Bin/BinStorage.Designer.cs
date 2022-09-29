namespace Bin
{
    partial class BinStorage
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
            this.p2 = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Panel();
            this.l2 = new System.Windows.Forms.Label();
            this.p4 = new System.Windows.Forms.Panel();
            this.P1 = new System.Windows.Forms.Panel();
            this.p2.SuspendLayout();
            this.p3.SuspendLayout();
            this.SuspendLayout();
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p2.Controls.Add(this.l1);
            this.p2.Dock = System.Windows.Forms.DockStyle.Left;
            this.p2.Location = new System.Drawing.Point(30, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(720, 1000);
            this.p2.TabIndex = 0;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Dock = System.Windows.Forms.DockStyle.Top;
            this.l1.Location = new System.Drawing.Point(0, 0);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(82, 23);
            this.l1.TabIndex = 0;
            this.l1.Text = "▼収納箱";
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p3.Controls.Add(this.l2);
            this.p3.Dock = System.Windows.Forms.DockStyle.Left;
            this.p3.Location = new System.Drawing.Point(750, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(720, 1000);
            this.p3.TabIndex = 1;
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Dock = System.Windows.Forms.DockStyle.Top;
            this.l2.Location = new System.Drawing.Point(0, 0);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(71, 23);
            this.l2.TabIndex = 1;
            this.l2.Text = "▼ゴミ箱";
            // 
            // p4
            // 
            this.p4.BackColor = System.Drawing.Color.Gainsboro;
            this.p4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p4.Location = new System.Drawing.Point(1470, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(0, 1000);
            this.p4.TabIndex = 2;
            // 
            // P1
            // 
            this.P1.BackColor = System.Drawing.Color.LightBlue;
            this.P1.Dock = System.Windows.Forms.DockStyle.Left;
            this.P1.Location = new System.Drawing.Point(0, 0);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(30, 1000);
            this.P1.TabIndex = 1;
            // 
            // BinStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.P1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BinStorage";
            this.Size = new System.Drawing.Size(1014, 1000);
            this.Load += new System.EventHandler(this.BinStorage_Load);
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.p3.ResumeLayout(false);
            this.p3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Panel P1;
    }
}
