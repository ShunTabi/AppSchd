namespace Analysis
{
    partial class AnalysisSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisSearch));
            this.cmb2 = new System.Windows.Forms.ComboBox();
            this.cmb1 = new System.Windows.Forms.ComboBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.l5 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l4 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.b1 = new System.Windows.Forms.Button();
            this.l1 = new System.Windows.Forms.Label();
            this.cmb3 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(18, 306);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(250, 30);
            this.tb2.TabIndex = 4;
            // 
            // l5
            // 
            this.l5.AutoSize = true;
            this.l5.Location = new System.Drawing.Point(14, 280);
            this.l5.Name = "l5";
            this.l5.Size = new System.Drawing.Size(118, 23);
            this.l5.TabIndex = 34;
            this.l5.Text = "集計終了月：";
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 240);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 3;
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Location = new System.Drawing.Point(14, 214);
            this.l4.Name = "l4";
            this.l4.Size = new System.Drawing.Size(118, 23);
            this.l4.TabIndex = 33;
            this.l4.Text = "集計開始月：";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Location = new System.Drawing.Point(14, 148);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(106, 23);
            this.l3.TabIndex = 32;
            this.l3.Text = "対象データ：";
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(14, 84);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(108, 23);
            this.l2.TabIndex = 31;
            this.l2.Text = "集計レベル：";
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 346);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 5;
            this.b1.Text = "確定";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(84, 23);
            this.l1.TabIndex = 30;
            this.l1.Text = "グラフ種：";
            // 
            // cmb3
            // 
            this.cmb3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb3.Font = new System.Drawing.Font("Meiryo UI", 8.5F);
            this.cmb3.FormattingEnabled = true;
            this.cmb3.ItemHeight = 22;
            this.cmb3.Location = new System.Drawing.Point(18, 174);
            this.cmb3.Name = "cmb3";
            this.cmb3.Size = new System.Drawing.Size(250, 30);
            this.cmb3.TabIndex = 2;
            // 
            // AnalysisSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(378, 404);
            this.Controls.Add(this.cmb3);
            this.Controls.Add(this.cmb2);
            this.Controls.Add(this.cmb1);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.l5);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.l4);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.l1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 460);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 460);
            this.Name = "AnalysisSearch";
            this.Text = "$AnalysisSearch";
            this.Load += new System.EventHandler(this.AnalysisSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmb2;
        private System.Windows.Forms.ComboBox cmb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label l5;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.ComboBox cmb3;
    }
}