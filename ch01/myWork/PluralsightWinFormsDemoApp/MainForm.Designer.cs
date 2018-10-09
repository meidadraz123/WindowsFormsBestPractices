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
            this.listBoxPodcasts = new System.Windows.Forms.ListBox();
            this.listBoxEpisodes = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.textBoxEpisodeTitle = new System.Windows.Forms.TextBox();
            this.textBoxPublicationDate = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxTags = new System.Windows.Forms.TextBox();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelTags = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.numericUpDownRating = new System.Windows.Forms.NumericUpDown();
            this.labelRating = new System.Windows.Forms.Label();
            this.checkBoxIsFavorite = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRating)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxPodcasts
            // 
            this.listBoxPodcasts.FormattingEnabled = true;
            this.listBoxPodcasts.Location = new System.Drawing.Point(13, 13);
            this.listBoxPodcasts.Name = "listBoxPodcasts";
            this.listBoxPodcasts.Size = new System.Drawing.Size(191, 316);
            this.listBoxPodcasts.TabIndex = 0;
            this.listBoxPodcasts.SelectedIndexChanged += new System.EventHandler(this.OnSelectedPodcastChanged);
            // 
            // listBoxEpisodes
            // 
            this.listBoxEpisodes.FormattingEnabled = true;
            this.listBoxEpisodes.Location = new System.Drawing.Point(211, 13);
            this.listBoxEpisodes.Name = "listBoxEpisodes";
            this.listBoxEpisodes.Size = new System.Drawing.Size(216, 316);
            this.listBoxEpisodes.TabIndex = 1;
            this.listBoxEpisodes.SelectedIndexChanged += new System.EventHandler(this.OnSelectedEpisodeChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(13, 336);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.OnButtonAddSubscriptionClick);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(94, 335);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.OnButtonRemovePodcastClick);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(462, 306);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.OnButtonPlayClick);
            // 
            // textBoxEpisodeTitle
            // 
            this.textBoxEpisodeTitle.Location = new System.Drawing.Point(459, 13);
            this.textBoxEpisodeTitle.Name = "textBoxEpisodeTitle";
            this.textBoxEpisodeTitle.Size = new System.Drawing.Size(314, 20);
            this.textBoxEpisodeTitle.TabIndex = 4;
            // 
            // textBoxPublicationDate
            // 
            this.textBoxPublicationDate.Location = new System.Drawing.Point(459, 40);
            this.textBoxPublicationDate.Name = "textBoxPublicationDate";
            this.textBoxPublicationDate.Size = new System.Drawing.Size(314, 20);
            this.textBoxPublicationDate.TabIndex = 5;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(459, 66);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(314, 20);
            this.textBoxDescription.TabIndex = 6;
            // 
            // textBoxTags
            // 
            this.textBoxTags.Location = new System.Drawing.Point(577, 94);
            this.textBoxTags.Name = "textBoxTags";
            this.textBoxTags.Size = new System.Drawing.Size(196, 20);
            this.textBoxTags.TabIndex = 4;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(462, 182);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(311, 118);
            this.textBoxNotes.TabIndex = 6;
            // 
            // labelTags
            // 
            this.labelTags.AutoSize = true;
            this.labelTags.Location = new System.Drawing.Point(459, 94);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(51, 13);
            this.labelTags.TabIndex = 7;
            this.labelTags.Text = "My Tags:";
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(462, 166);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(55, 13);
            this.labelNotes.TabIndex = 7;
            this.labelNotes.Text = "My Notes:";
            // 
            // numericUpDownRating
            // 
            this.numericUpDownRating.Location = new System.Drawing.Point(577, 121);
            this.numericUpDownRating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownRating.Name = "numericUpDownRating";
            this.numericUpDownRating.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRating.TabIndex = 8;
            // 
            // labelRating
            // 
            this.labelRating.AutoSize = true;
            this.labelRating.Location = new System.Drawing.Point(459, 123);
            this.labelRating.Name = "labelRating";
            this.labelRating.Size = new System.Drawing.Size(58, 13);
            this.labelRating.TabIndex = 7;
            this.labelRating.Text = "My Rating:";
            // 
            // checkBoxIsFavorite
            // 
            this.checkBoxIsFavorite.AutoSize = true;
            this.checkBoxIsFavorite.Location = new System.Drawing.Point(577, 148);
            this.checkBoxIsFavorite.Name = "checkBoxIsFavorite";
            this.checkBoxIsFavorite.Size = new System.Drawing.Size(64, 17);
            this.checkBoxIsFavorite.TabIndex = 9;
            this.checkBoxIsFavorite.Text = "Favorite";
            this.checkBoxIsFavorite.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 381);
            this.Controls.Add(this.checkBoxIsFavorite);
            this.Controls.Add(this.numericUpDownRating);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.labelRating);
            this.Controls.Add(this.labelTags);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxTags);
            this.Controls.Add(this.textBoxPublicationDate);
            this.Controls.Add(this.textBoxEpisodeTitle);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxEpisodes);
            this.Controls.Add(this.listBoxPodcasts);
            this.Name = "MainForm";
            this.Text = "My Podcasts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnMainFormClosed);
            this.Load += new System.EventHandler(this.OnMainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPodcasts;
        private System.Windows.Forms.ListBox listBoxEpisodes;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.TextBox textBoxEpisodeTitle;
        private System.Windows.Forms.TextBox textBoxPublicationDate;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelTags;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownRating;
        private System.Windows.Forms.Label labelRating;
        private System.Windows.Forms.CheckBox checkBoxIsFavorite;
    }
}

