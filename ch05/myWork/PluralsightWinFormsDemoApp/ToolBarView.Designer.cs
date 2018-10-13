namespace PluralsightWinFormsDemoApp
{
    partial class ToolBarView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMainForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddSubscription = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveSubscription = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFavourite = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripMainForm.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripMainForm.Size = new System.Drawing.Size(286, 39);
            this.toolStripMainForm.TabIndex = 1;
            this.toolStripMainForm.TabStop = true;
            this.toolStripMainForm.Text = "toolStripMainForm";
            // 
            // toolStripButtonAddSubscription
            // 
            this.toolStripButtonAddSubscription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddSubscription.Image = global::PluralsightWinFormsDemoApp.IconResources.add_icon_32;
            this.toolStripButtonAddSubscription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddSubscription.Name = "toolStripButtonAddSubscription";
            this.toolStripButtonAddSubscription.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonAddSubscription.Text = "Play";
            // 
            // toolStripButtonRemoveSubscription
            // 
            this.toolStripButtonRemoveSubscription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveSubscription.Image = global::PluralsightWinFormsDemoApp.IconResources.remove_icon_32;
            this.toolStripButtonRemoveSubscription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveSubscription.Name = "toolStripButtonRemoveSubscription";
            this.toolStripButtonRemoveSubscription.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonRemoveSubscription.Text = "Remove podcast subscription";
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
            this.toolStripButtonPlay.Text = "Play";
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = global::PluralsightWinFormsDemoApp.IconResources.pause_icon_32;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPause.Text = "Pause";
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = global::PluralsightWinFormsDemoApp.IconResources.stop_icon_32;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonStop.Text = "Stop";
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
            // ToolBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripMainForm);
            this.Name = "ToolBarView";
            this.Size = new System.Drawing.Size(286, 40);
            this.toolStripMainForm.ResumeLayout(false);
            this.toolStripMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
