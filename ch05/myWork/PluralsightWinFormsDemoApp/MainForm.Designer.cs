namespace PluralsightWinFormsDemoApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerMainForm = new System.Windows.Forms.SplitContainer();
            this.toolStripMainForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddSubscription = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveSubscription = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFavourite = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainForm)).BeginInit();
            this.splitContainerMainForm.SuspendLayout();
            this.toolStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMainForm
            // 
            this.splitContainerMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainForm.Location = new System.Drawing.Point(0, 39);
            this.splitContainerMainForm.Name = "splitContainerMainForm";
            this.splitContainerMainForm.Panel1MinSize = 180;
            this.splitContainerMainForm.Size = new System.Drawing.Size(764, 405);
            this.splitContainerMainForm.SplitterDistance = 355;
            this.splitContainerMainForm.TabIndex = 1;
            this.splitContainerMainForm.TabStop = false;
            // 
            // toolStripMainForm
            // 
            this.toolStripMainForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddSubscription,
            this.toolStripButtonRemoveSubscription,
            this.toolStripSeparator1,
            this.toolStripButtonPlay,
            this.toolStripButtonPause,
            this.toolStripButtonStop,
            this.toolStripButtonFavourite,
            this.toolStripButtonSettings});
            this.toolStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripMainForm.Name = "toolStripMainForm";
            this.toolStripMainForm.Size = new System.Drawing.Size(764, 39);
            this.toolStripMainForm.TabIndex = 0;
            this.toolStripMainForm.TabStop = true;
            this.toolStripMainForm.Text = "toolStripMainForm";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAddSubscription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddSubscription.Image = global::PluralsightWinFormsDemoApp.IconResources.add_icon_32;
            this.toolStripButtonAddSubscription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddSubscription.Name = "toolStripButtonAdd";
            this.toolStripButtonAddSubscription.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonAddSubscription.Text = "Play";
            this.toolStripButtonAddSubscription.Click += new System.EventHandler(this.OnToolStripButtonAddSubscriptionClick);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemoveSubscription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveSubscription.Image = global::PluralsightWinFormsDemoApp.IconResources.remove_icon_32;
            this.toolStripButtonRemoveSubscription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveSubscription.Name = "toolStripButtonRemove";
            this.toolStripButtonRemoveSubscription.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonRemoveSubscription.Text = "Remove podcast subscription";
            this.toolStripButtonRemoveSubscription.Click += new System.EventHandler(this.OnToolStripButtonRemovePodcastClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonPlay
            // 
            this.toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlay.Image = global::PluralsightWinFormsDemoApp.IconResources.play_icon_32;
            this.toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlay.Name = "toolStripButtonPlay";
            this.toolStripButtonPlay.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPlay.Text = "toolStripButton3";
            this.toolStripButtonPlay.Click += new System.EventHandler(this.OnToolStripButtonPlayClick);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = global::PluralsightWinFormsDemoApp.IconResources.pause_icon_32;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPause.Text = "Pause";
            this.toolStripButtonPause.Click += new System.EventHandler(this.OnToolStripButtonPauseClick);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = global::PluralsightWinFormsDemoApp.IconResources.stop_icon_32;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.OnToolStripButtonStopClick);
            // 
            // toolStripButtonFavourite
            // 
            this.toolStripButtonFavourite.CheckOnClick = true;
            this.toolStripButtonFavourite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFavourite.Image = global::PluralsightWinFormsDemoApp.IconResources.star_icon_32;
            this.toolStripButtonFavourite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFavourite.Name = "toolStripButtonFavourite";
            this.toolStripButtonFavourite.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonFavourite.Text = "Favourite";
            this.toolStripButtonFavourite.CheckStateChanged += new System.EventHandler(this.OnToolStripButtonFavouriteCheckStateChanged);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = global::PluralsightWinFormsDemoApp.IconResources.settings_icon_32;
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonSettings.Text = "Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(764, 444);
            this.Controls.Add(this.splitContainerMainForm);
            this.Controls.Add(this.toolStripMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 415);
            this.Name = "MainForm";
            this.Text = "My Podcasts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnMainFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnMainFormHelpRequested);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainForm)).EndInit();
            this.splitContainerMainForm.ResumeLayout(false);
            this.toolStripMainForm.ResumeLayout(false);
            this.toolStripMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMainForm;
        private System.Windows.Forms.ToolStrip toolStripMainForm;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddSubscription;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveSubscription;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlay;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonFavourite;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
    }
}

