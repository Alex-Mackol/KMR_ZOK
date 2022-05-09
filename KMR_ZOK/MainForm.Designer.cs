namespace KMR_ZOK
{
    partial class MainForm
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
            this.btnCount = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericM0 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericM0)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCount
            // 
            this.btnCount.Location = new System.Drawing.Point(12, 370);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(151, 47);
            this.btnCount.TabIndex = 0;
            this.btnCount.Text = "Count";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "m0 = ";
            // 
            // numericM0
            // 
            this.numericM0.DecimalPlaces = 2;
            this.numericM0.Location = new System.Drawing.Point(66, 12);
            this.numericM0.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericM0.Name = "numericM0";
            this.numericM0.Size = new System.Drawing.Size(97, 27);
            this.numericM0.TabIndex = 2;
            this.numericM0.Value = new decimal(new int[] {
            4500,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericM0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCount);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericM0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCount;
        private Label label1;
        private NumericUpDown numericM0;
    }
}