namespace P4G_PC_Save_Importer
{
    partial class AboutForm
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
            this.mainLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGitHub = new System.Windows.Forms.Button();
            this.btnTwitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(71, 37);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(200, 20);
            this.mainLabel.TabIndex = 0;
            this.mainLabel.Text = "Tool Created by RayFirefist";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "For the old players of P4G from PSVita";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "and researchers.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Open Source tool on Github:";
            // 
            // btnGitHub
            // 
            this.btnGitHub.Location = new System.Drawing.Point(38, 183);
            this.btnGitHub.Name = "btnGitHub";
            this.btnGitHub.Size = new System.Drawing.Size(124, 34);
            this.btnGitHub.TabIndex = 4;
            this.btnGitHub.Text = "Github";
            this.btnGitHub.UseVisualStyleBackColor = true;
            this.btnGitHub.Click += new System.EventHandler(this.btnGitHub_Click);
            // 
            // btnTwitter
            // 
            this.btnTwitter.Location = new System.Drawing.Point(192, 183);
            this.btnTwitter.Name = "btnTwitter";
            this.btnTwitter.Size = new System.Drawing.Size(124, 34);
            this.btnTwitter.TabIndex = 5;
            this.btnTwitter.Text = "Twitter";
            this.btnTwitter.UseVisualStyleBackColor = true;
            this.btnTwitter.Click += new System.EventHandler(this.btnTwitter_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 244);
            this.Controls.Add(this.btnTwitter);
            this.Controls.Add(this.btnGitHub);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGitHub;
        private System.Windows.Forms.Button btnTwitter;
    }
}