using System;
using System.Windows.Forms;
using System.Drawing;

namespace PluralsightWinFormsDemoApp.Views
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly WpfEpisodeViewHost episodeView;
        private readonly PodcastView podcastView;
        private readonly SubscriptionView subscriptionView;

        public IEpisodeView EpisodeView { get { return episodeView; } }

        public IPodcastView PodcastView { get { return podcastView; } }

        public ISubscriptionView SubscriptionView { get { return subscriptionView; } }

        public IToolbarView ToolbarView { get { return toolBarView; } }

        public MainForm()
        {
            InitializeComponent();
            episodeView = new WpfEpisodeViewHost() { Dock = DockStyle.Fill };
            podcastView = new PodcastView() { Dock = DockStyle.Fill };
            subscriptionView = new SubscriptionView() { Dock = DockStyle.Fill };
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

    interface IMainFormView
    {
        event EventHandler Load;
        event FormClosedEventHandler FormClosed;
        event HelpEventHandler HelpRequested;
        event KeyEventHandler KeyUp;

        IEpisodeView EpisodeView { get; }
        IPodcastView PodcastView { get; }
        ISubscriptionView SubscriptionView { get; }
        IToolbarView ToolbarView { get; }

        Color BackColor { get; set; }

        void ShowEpisodeView();
        void ShowPodcastView();

    }
}
