namespace Arachni_API
{
    partial class Main
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
            this.lstScansID = new System.Windows.Forms.ListView();
            this.btnScans = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstScansID
            // 
            this.lstScansID.Location = new System.Drawing.Point(12, 12);
            this.lstScansID.Name = "lstScansID";
            this.lstScansID.Size = new System.Drawing.Size(265, 191);
            this.lstScansID.TabIndex = 0;
            this.lstScansID.UseCompatibleStateImageBehavior = false;
            // 
            // btnScans
            // 
            this.btnScans.Location = new System.Drawing.Point(24, 209);
            this.btnScans.Name = "btnScans";
            this.btnScans.Size = new System.Drawing.Size(90, 44);
            this.btnScans.TabIndex = 1;
            this.btnScans.Text = "Taramaları Getir";
            this.btnScans.UseVisualStyleBackColor = true;
            this.btnScans.Click += new System.EventHandler(this.btnScans_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnScans);
            this.Controls.Add(this.lstScansID);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ListView lstScansID;
        private System.Windows.Forms.Button btnScans;
    }
}