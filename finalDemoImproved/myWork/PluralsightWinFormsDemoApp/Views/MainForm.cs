using System;
using System.Windows.Forms;
using System.Drawing;

namespace PluralsightWinFormsDemoApp.Views
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly Control episodeView;
        private readonly PodcastView podcastView;

        public IPodcastView PodcastView { get { return podcastView; } }

        public MainForm(Control episodeView, Control subscriptionView, Control toolbarView)
        {
            InitializeComponent();
            toolbarView.Dock = DockStyle.Top;
            Controls.Add(toolbarView);
            this.episodeView = episodeView;
            episodeView.Dock = DockStyle.Fill;
            podcastView = new PodcastView() { Dock = DockStyle.Fill };
            subscriptionView.Dock = DockStyle.Fill;
            splitContainerMainForm.Panel1.Controls.Add(subscriptionView);
        }

        public void ShowEpisodeView()
        {
            splitContainerMainForm.Panel2.Controls.Clear();
            splitContainerMainForm.Panel2.Controls.Add(episodeView);
        }

        public void ShowPodcastView()
        {
            splitContainerMainForm.Panel2.Controls.Clear();
            splitContainerMainForm.Panel2.Controls.Add(podcastView);
        }
    }
}
