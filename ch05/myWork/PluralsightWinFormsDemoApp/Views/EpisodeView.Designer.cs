namespace PluralsightWinFormsDemoApp
{
    public partial class EpisodeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpisodeView));
            this.panelEpisodeEditableInfo = new System.Windows.Forms.Panel();
            this.numericUpDownMyRating = new System.Windows.Forms.NumericUpDown();
            this.labelMyNotes = new System.Windows.Forms.Label();
            this.labelMyRating = new System.Windows.Forms.Label();
            this.labelMyTags = new System.Windows.Forms.Label();
            this.textBoxMyNotes = new System.Windows.Forms.TextBox();
            this.textBoxMyTags = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelEpisodeStaticInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.labelEpisodeTitle = new System.Windows.Forms.Label();
            this.labelPublicationDate = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.toolTipEpisodeView = new System.Windows.Forms.ToolTip(this.components);
            this.panelEpisodeEditableInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMyRating)).BeginInit();
            this.flowLayoutPanelEpisodeStaticInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEpisodeEditableInfo
            // 
            this.panelEpisodeEditableInfo.Controls.Add(this.numericUpDownMyRating);
            this.panelEpisodeEditableInfo.Controls.Add(this.labelMyNotes);
            this.panelEpisodeEditableInfo.Controls.Add(this.labelMyRating);
            this.panelEpisodeEditableInfo.Controls.Add(this.labelMyTags);
            this.panelEpisodeEditableInfo.Controls.Add(this.textBoxMyNotes);
            this.panelEpisodeEditableInfo.Controls.Add(this.textBoxMyTags);
            resources.ApplyResources(this.panelEpisodeEditableInfo, "panelEpisodeEditableInfo");
            this.panelEpisodeEditableInfo.Name = "panelEpisodeEditableInfo";
            // 
            // numericUpDownMyRating
            // 
            resources.ApplyResources(this.numericUpDownMyRating, "numericUpDownMyRating");
            this.numericUpDownMyRating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMyRating.Name = "numericUpDownMyRating";
            // 
            // labelMyNotes
            // 
            resources.ApplyResources(this.labelMyNotes, "labelMyNotes");
            this.labelMyNotes.Name = "labelMyNotes";
            // 
            // labelMyRating
            // 
            resources.ApplyResources(this.labelMyRating, "labelMyRating");
            this.labelMyRating.Name = "labelMyRating";
            // 
            // labelMyTags
            // 
            resources.ApplyResources(this.labelMyTags, "labelMyTags");
            this.labelMyTags.Name = "labelMyTags";
            // 
            // textBoxMyNotes
            // 
            resources.ApplyResources(this.textBoxMyNotes, "textBoxMyNotes");
            this.textBoxMyNotes.Name = "textBoxMyNotes";
            // 
            // textBoxMyTags
            // 
            resources.ApplyResources(this.textBoxMyTags, "textBoxMyTags");
            this.textBoxMyTags.Name = "textBoxMyTags";
            this.textBoxMyTags.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnTextBoxTagsHelpRequested);
            // 
            // flowLayoutPanelEpisodeStaticInfo
            // 
            resources.ApplyResources(this.flowLayoutPanelEpisodeStaticInfo, "flowLayoutPanelEpisodeStaticInfo");
            this.flowLayoutPanelEpisodeStaticInfo.Controls.Add(this.labelEpisodeTitle);
            this.flowLayoutPanelEpisodeStaticInfo.Controls.Add(this.labelPublicationDate);
            this.flowLayoutPanelEpisodeStaticInfo.Controls.Add(this.labelDescription);
            this.flowLayoutPanelEpisodeStaticInfo.Name = "flowLayoutPanelEpisodeStaticInfo";
            // 
            // labelEpisodeTitle
            // 
            resources.ApplyResources(this.labelEpisodeTitle, "labelEpisodeTitle");
            this.labelEpisodeTitle.ForeColor = System.Drawing.Color.DarkGray;
            this.labelEpisodeTitle.Name = "labelEpisodeTitle";
            // 
            // labelPublicationDate
            // 
            resources.ApplyResources(this.labelPublicationDate, "labelPublicationDate");
            this.labelPublicationDate.Name = "labelPublicationDate";
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // EpisodeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEpisodeEditableInfo);
            this.Controls.Add(this.flowLayoutPanelEpisodeStaticInfo);
            this.Name = "EpisodeView";
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnTextBoxTagsHelpRequested);
            this.panelEpisodeEditableInfo.ResumeLayout(false);
            this.panelEpisodeEditableInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMyRating)).EndInit();
            this.flowLayoutPanelEpisodeStaticInfo.ResumeLayout(false);
            this.flowLayoutPanelEpisodeStaticInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelEpisodeEditableInfo;
        public System.Windows.Forms.NumericUpDown numericUpDownMyRating;
        private System.Windows.Forms.Label labelMyNotes;
        private System.Windows.Forms.Label labelMyRating;
        private System.Windows.Forms.Label labelMyTags;
        public System.Windows.Forms.TextBox textBoxMyNotes;
        public System.Windows.Forms.TextBox textBoxMyTags;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEpisodeStaticInfo;
        public System.Windows.Forms.Label labelEpisodeTitle;
        public System.Windows.Forms.Label labelPublicationDate;
        public System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.ToolTip toolTipEpisodeView;
    }
}
