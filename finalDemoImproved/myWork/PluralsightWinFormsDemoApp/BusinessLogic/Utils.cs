using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class Utils
    {
        public static void AddPodcastToSubscriptionView(ISubscriptionView subscriptionView, Podcast podcast)
        {
            var podcastNode = new TreeNode(podcast.Title) { Tag = podcast, Name = podcast.Id.ToString() };
            foreach (var episode in podcast.Episodes)
            {
                podcastNode.Nodes.Add(new TreeNode(episode.Title) { Tag = episode, Name = episode.Guid });
            }
            subscriptionView.AddNode(podcastNode);
        }

        public static void SelectFirstEpisode(ISubscriptionView subscriptionView, ISubscriptionManager subscriptionManager)
        {
            if (!subscriptionView.IsEmpty())
                subscriptionView.SelectNode(subscriptionManager.Podcasts.SelectMany(p => p.Episodes).First().Guid);
        }
    }
}
