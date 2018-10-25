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
            this.toolBarView = new PluralsightWinFormsDemoApp.ToolBarView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainForm)).BeginInit();
            this.splitContainerMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMainForm
            // 
            this.splitContainerMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainForm.Location = new System.Drawing.Point(0, 40);
            this.splitContainerMainForm.Name = "splitContainerMainForm";
            this.splitContainerMainForm.Panel1MinSize = 180;
            this.splitContainerMainForm.Size = new System.Drawing.Size(764, 471);
            this.splitContainerMainForm.SplitterDistance = 355;
            this.splitContainerMainForm.TabIndex = 1;
            this.splitContainerMainForm.TabStop = false;
            // 
            // toolBarView
            // 
            this.toolBarView.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBarView.Location = new System.Drawing.Point(0, 0);
            this.toolBarView.Name = "toolBarView";
            this.toolBarView.Size = new System.Drawing.Size(764, 40);
            this.toolBarView.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(764, 511);
            this.Controls.Add(this.splitContainerMainForm);
            this.Controls.Add(this.toolBarView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(550, 550);
            this.Name = "MainForm";
            this.Text = "My Podcasts";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainForm)).EndInit();
            this.splitContainerMainForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMainForm;
        private ToolBarView toolBarView;
    }
}

