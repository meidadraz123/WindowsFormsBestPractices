using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class SubscriptionPresenter
    {
        private readonly ISubscriptionView subscriptionView;
        public SubscriptionPresenter(ISubscriptionView subscriptionView)
        {
            this.subscriptionView = subscriptionView;
            subscriptionView.SelectionChanged += OnSelectedEpisodeChanged;
            EventAggregator.Instance.Subscribe<PodcastLoadedMessage>(m => AddPodcastToSubscriptionView(m.Podcast));
            EventAggregator.Instance.Subscribe<PodcastLoadCompletedMessage>(m => SelectFirstEpisode(m.Podcasts));
        }

        private void OnSelectedEpisodeChanged(object sender, EventArgs e)
        {
            if (subscriptionView.SelectedNode == null)
            {
                return;
            }
            if (subscriptionView.SelectedNode.Tag is Episode selectedEpisode)
            {
                EventAggregator.Instance.Publish(new EpisodeSelectedMessage(selectedEpisode));
            }

            if (subscriptionView.SelectedNode.Tag is Podcast selectedPodcast)
            {
                EventAggregator.Instance.Publish(new PodcastSelectedMessage(selectedPodcast));
            }
        }

        private void AddPodcastToSubscriptionView( Podcast podcast)
        {
            var podcastNode = new TreeNode(podcast.Title) { Tag = podcast, Name = podcast.Id.ToString() };
            foreach (var episode in podcast.Episodes)
            {
                podcastNode.Nodes.Add(new TreeNode(episode.Title) { Tag = episode, Name = episode.Guid });
            }
            subscriptionView.AddNode(podcastNode);
            if (subscriptionView.SelectedNode == null)
            {
                subscriptionView.SelectNode(podcastNode.FirstNode.Name);
            }
        }

        private void SelectFirstEpisode( Podcast[] podcasts)
        {
            subscriptionView.SelectNode(podcasts.SelectMany(p => p.Episodes).First().Guid);
        }
    }
}
