namespace PluralsightWinFormsDemoApp.Views
{
    partial class NoteForm
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
            this.labelEnterNote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelEnterNote
            // 
            this.labelEnterNote.AutoSize = true;
            this.labelEnterNote.Location = new System.Drawing.Point(13, 13);
            this.labelEnterNote.Name = "labelEnterNote";
            this.labelEnterNote.Size = new System.Drawing.Size(61, 13);
            this.labelEnterNote.TabIndex = 0;
            this.labelEnterNote.Text = "Enter Note:";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(16, 39);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(183, 20);
            this.textBoxNote.TabIndex = 1;
            // 
            // NoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(216, 76);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.labelEnterNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NoteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEnterNote;
        private System.Windows.Forms.TextBox textBoxNote;
    }
}