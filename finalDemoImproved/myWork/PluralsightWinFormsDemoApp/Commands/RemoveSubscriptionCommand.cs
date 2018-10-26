using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluralsightWinFormsDemoApp.Views;

namespace PluralsightWinFormsDemoApp.Commands
{
    class RemoveSubscriptionCommand : CommandBase
    {
        private readonly ISubscriptionManager subscriptionManager;
        private readonly ISubscriptionView subscriptionView;

        public RemoveSubscriptionCommand(ISubscriptionView subscriptionView,
            ISubscriptionManager subscriptionManager)
        {
            this.subscriptionManager = subscriptionManager;
            this.subscriptionView = subscriptionView;
            Icon = IconResources.remove_icon_32;
            ToolTip = "Remove Subscription";
        }
        public override void Execute()
        {
            if (subscriptionView.SelectedNode.Tag is Podcast podcast)
            {
                subscriptionManager.RemovePodcast(podcast);
                subscriptionView.RemoveNode(podcast.Id.ToString());
                Utils.SelectFirstEpisode(subscriptionView,subscriptionManager);
            }

            else if (subscriptionView.SelectedNode.Tag is Episode episode)
            {
                var relatedPodcast = subscriptionManager.Podcasts.Where(p => p.Episodes.Where(ep => ep.Guid == episode.Guid).Any()).FirstOrDefault();
                subscriptionManager.RemovePodcast(relatedPodcast);
                subscriptionView.RemoveNode(relatedPodcast.Id.ToString());
                Utils.SelectFirstEpisode(subscriptionView, subscriptionManager);
            }
        }
    }
}
