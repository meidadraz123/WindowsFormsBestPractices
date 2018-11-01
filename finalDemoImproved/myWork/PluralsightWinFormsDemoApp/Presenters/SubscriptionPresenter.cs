using System;
using System.Linq;
using System.Windows.Forms;
using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class SubscriptionPresenter
    {
        private readonly ISubscriptionView subscriptionView;
        public SubscriptionPresenter(ISubscriptionView subscriptionView)
        {
            this.subscriptionView = subscriptionView;
            subscriptionView.SelectionChanged += OnSelectedEpisodeChanged;
            EventAggregator.Instance.Subscribe<PodcastLoadedMessage>(m => AddPodcastToTreeView(m.Podcast));
            EventAggregator.Instance.Subscribe<PodcastLoadCompleteMessage>(m => SelectFirstEpisode(m.Subscriptions));
        }

        private void OnSelectedEpisodeChanged(object sender, EventArgs e)
        {
            if (subscriptionView.SelectedNode == null) return;

            if (subscriptionView.SelectedNode.Tag is Episode selectedEpisode)
            {
                EventAggregator.Instance.Publish(new EpisodeSelectedMessage(selectedEpisode));
            }

            if (subscriptionView.SelectedNode.Tag is Podcast selectedPodcast)
            {
                EventAggregator.Instance.Publish(new PodcastSelectedMessage(selectedPodcast));
            }
        }

        public void AddPodcastToTreeView(Podcast podcast)
        {
            var podNode = new TreeNode(podcast.Title) { Tag = podcast, Name = podcast.Id.ToString() };
            foreach (var episode in podcast.Episodes)
            {
                podNode.Nodes.Add(new TreeNode(episode.Title) { Tag = episode, Name = episode.Guid });
            }

            subscriptionView.AddNode(podNode);
            if (subscriptionView.SelectedNode == null)
            {
                subscriptionView.SelectNode(podNode.FirstNode.Name);
            }
        }

        private void SelectFirstEpisode( Podcast[] podcasts)
        {
            subscriptionView.SelectNode(podcasts.SelectMany(p => p.Episodes).First().Guid);
        }
    }
}
