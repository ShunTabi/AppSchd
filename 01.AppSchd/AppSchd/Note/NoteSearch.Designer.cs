namespace Note
{
    partial class NoteSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteSearch));
            this.l1 = new System.Windows.Forms.Label();
            this.b1 = new System.Windows.Forms.Button();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l2 = new System.Windows.Forms.Label();
            this.radio0 = new System.Windows.Forms.RadioButton();
            this.radio1 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(100, 23);
            this.l1.TabIndex = 2;
            this.l1.Text = "$Name1：";
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 149);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 3;
            this.b1.Text = "検索";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 100);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 4;
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(14, 74);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(100, 23);
            this.l2.TabIndex = 5;
            this.l2.Text = "$Name2：";
            // 
            // radio0
            // 
            this.radio0.AutoSize = true;
            this.radio0.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.radio0.Location = new System.Drawing.Point(18, 42);
            this.radio0.Name = "radio0";
            this.radio0.Size = new System.Drawing.Size(63, 24);
            this.radio0.TabIndex = 6;
            this.radio0.TabStop = true;
            this.radio0.Text = "全て";
            this.radio0.UseVisualStyleBackColor = true;
            // 
            // radio1
            // 
            this.radio1.AutoSize = true;
            this.radio1.Font = new System.Drawing.Font("Meiryo UI", 8F);
            this.radio1.Location = new System.Drawing.Point(87, 42);
            this.radio1.Name = "radio1";
            this.radio1.Size = new System.Drawing.Size(93, 24);
            this.radio1.TabIndex = 7;
            this.radio1.TabStop = true;
            this.radio1.Text = "最新のみ";
            this.radio1.UseVisualStyleBackColor = true;
            // 
            // NoteSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(328, 194);
            this.Controls.Add(this.radio1);
            this.Controls.Add(this.radio0);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.l1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 250);
            this.Name = "NoteSearch";
            this.Text = "$RecordSearch";
            this.Load += new System.EventHandler(this.RecordSeach_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.RadioButton radio0;
        private System.Windows.Forms.RadioButton radio1;
    }
}