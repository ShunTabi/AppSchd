namespace Record
{
    partial class RecordSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordSearch));
            this.tb1 = new System.Windows.Forms.TextBox();
            this.l1 = new System.Windows.Forms.Label();
            this.b1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(18, 42);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(250, 30);
            this.tb1.TabIndex = 1;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(14, 16);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(89, 23);
            this.l1.TabIndex = 2;
            this.l1.Text = "$Name：";
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(18, 82);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(60, 35);
            this.b1.TabIndex = 3;
            this.b1.Text = "検索";
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // RecordSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(328, 144);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.l1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "RecordSearch";
            this.Text = "$RecordSearch";
            this.Load += new System.EventHandler(this.RecordSeach_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Button b1;
    }
}