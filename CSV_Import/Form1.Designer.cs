
namespace CSV_Import
{
    partial class Form1
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
            this.test_btn = new System.Windows.Forms.Button();
            this.insert_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // test_btn
            // 
            this.test_btn.BackColor = System.Drawing.Color.Green;
            this.test_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.test_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test_btn.ForeColor = System.Drawing.Color.White;
            this.test_btn.Location = new System.Drawing.Point(132, 70);
            this.test_btn.Name = "test_btn";
            this.test_btn.Size = new System.Drawing.Size(163, 41);
            this.test_btn.TabIndex = 0;
            this.test_btn.Text = "Test Connection";
            this.test_btn.UseVisualStyleBackColor = false;
            this.test_btn.Click += new System.EventHandler(this.test_btn_Click);
            // 
            // insert_btn
            // 
            this.insert_btn.BackColor = System.Drawing.Color.Maroon;
            this.insert_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.insert_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insert_btn.ForeColor = System.Drawing.Color.White;
            this.insert_btn.Location = new System.Drawing.Point(366, 70);
            this.insert_btn.Name = "insert_btn";
            this.insert_btn.Size = new System.Drawing.Size(163, 41);
            this.insert_btn.TabIndex = 1;
            this.insert_btn.Text = "Insert into DB";
            this.insert_btn.UseVisualStyleBackColor = false;
            this.insert_btn.Click += new System.EventHandler(this.insert_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(551, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Created by Mina Dawood";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(687, 244);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.insert_btn);
            this.Controls.Add(this.test_btn);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSV Data Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button test_btn;
        private System.Windows.Forms.Button insert_btn;
        private System.Windows.Forms.Label label1;
    }
}

