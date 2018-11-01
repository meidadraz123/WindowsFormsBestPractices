using System.Linq;
using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;

namespace PluralsightWinFormsDemoApp.Commands
{
    class RemoveSubscriptionCommand : CommandBase
    {
        private readonly ISubscriptionView subscriptionView;
        private readonly ISubscriptionManager subscriptionManager;

        public RemoveSubscriptionCommand(ISubscriptionView subscriptionView, ISubscriptionManager subscriptionManager)
        {
            this.subscriptionView = subscriptionView;
            this.subscriptionManager = subscriptionManager;
            Icon = IconResources.remove_icon_32;
            ToolTip = "Remove Subscription";
        }

        public override void Execute()
        {
            if (subscriptionView.SelectedNode.Tag is Podcast pod)
            {
                subscriptionManager.RemoveSubscription(pod);
                subscriptionView.RemoveNode(pod.Id.ToString());
            }

            else if (subscriptionView.SelectedNode.Tag is Episode episode)
            {
                var relatedPodcast = subscriptionManager.Subscriptions.Where(p => p.Episodes.Where(ep => ep.Guid == episode.Guid).Any()).FirstOrDefault();
                subscriptionManager.RemoveSubscription(relatedPodcast);
                subscriptionView.RemoveNode(relatedPodcast.Id.ToString());
            }
            EventAggregator.Instance.Publish(new PodcastLoadCompleteMessage(subscriptionManager.Subscriptions.ToArray()));
        }
    }
}
