﻿namespace Record
{
    partial class RecordGenre
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
            this.p1 = new System.Windows.Forms.Panel();
            this.b1 = new System.Windows.Forms.Button();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l1 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.p1.SuspendLayout();
            this.SuspendLayout();
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.LightBlue;
            this.p1.Controls.Add(this.b1);
            this.p1.Controls.Add(this.tb1);
            this.p1.Controls.Add(this.l1);
            this.p1.Dock = System.Windows.Forms.DockStyle.Left;
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(300, 1000);
            this.p1.TabIndex = 8;
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 82);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 1;
            this.b1.Text = "確定";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 42);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 0;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(82, 23);
            this.l1.TabIndex = 0;
            this.l1.Text = "種別名：";
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.Gainsboro;
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.Location = new System.Drawing.Point(841, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(159, 1000);
            this.p3.TabIndex = 9;
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.p2.Dock = System.Windows.Forms.DockStyle.Left;
            this.p2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.p2.Location = new System.Drawing.Point(300, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(541, 1000);
            this.p2.TabIndex = 19;
            // 
            // RecordGenre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RecordGenre";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.Load += new System.EventHandler(this.RecordGenre_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p2;
    }
}
