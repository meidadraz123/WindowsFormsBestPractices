namespace PluralsightWinFormsDemoApp
{
    partial class NewPodcastForm
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxFeedUrl = new System.Windows.Forms.TextBox();
            this.labelFeedUrl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(197, 63);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.OnButtonOkClick);
            // 
            // textBoxFeedUrl
            // 
            this.textBoxFeedUrl.Location = new System.Drawing.Point(78, 25);
            this.textBoxFeedUrl.Name = "textBoxFeedUrl";
            this.textBoxFeedUrl.Size = new System.Drawing.Size(194, 20);
            this.textBoxFeedUrl.TabIndex = 1;
            // 
            // labelFeedUrl
            // 
            this.labelFeedUrl.AutoSize = true;
            this.labelFeedUrl.Location = new System.Drawing.Point(13, 28);
            this.labelFeedUrl.Name = "labelFeedUrl";
            this.labelFeedUrl.Size = new System.Drawing.Size(59, 13);
            this.labelFeedUrl.TabIndex = 2;
            this.labelFeedUrl.Text = "Feed URL:";
            // 
            // NewPodcastForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 110);
            this.Controls.Add(this.labelFeedUrl);
            this.Controls.Add(this.textBoxFeedUrl);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPodcastForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a New Podcast";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxFeedUrl;
        private System.Windows.Forms.Label labelFeedUrl;
    }
}