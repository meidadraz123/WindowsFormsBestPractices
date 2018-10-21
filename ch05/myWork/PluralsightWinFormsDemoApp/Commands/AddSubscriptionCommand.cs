using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.BusinessLogic;
using System.Net;
using System.Xml;

namespace PluralsightWinFormsDemoApp.Commands
{
    class AddSubscriptionCommand : CommandBase
    {
        private readonly ISubscriptionView subscriptionView;
        private readonly IMessageBoxDisplayService messageBoxDisplayService;
        private readonly INewSubscriptionService newSubscriptionService;
        private readonly IPodcastLoader podcastLoader;
        private readonly ISubscriptionManager subscriptionManager;

        public AddSubscriptionCommand(ISubscriptionView subscriptionView,
            IMessageBoxDisplayService messageBoxDisplayService,
            INewSubscriptionService newSubscriptionService,
            IPodcastLoader podcastLoader,
            ISubscriptionManager subscriptionManager)
        {
            this.subscriptionView = subscriptionView;
            this.messageBoxDisplayService = messageBoxDisplayService;
            this.newSubscriptionService = newSubscriptionService;
            this.podcastLoader = podcastLoader;
            this.subscriptionManager = subscriptionManager;

            Icon = IconResources.add_icon_32;
            ToolTip = "Add Subscription";
        }
        public override async void Execute()
        {
            var newPodcastSubscription = newSubscriptionService.GetSubscriptionUrl();
            if (newPodcastSubscription != null)
            {
                var newPodcast = new Podcast() { SubscriptionUrl = newPodcastSubscription };
                try
                {
                    await podcastLoader.UpdatePodcast(newPodcast);
                    subscriptionManager.AddPodcast(newPodcast);
                    Utils.AddPodcastToSubscriptionView(subscriptionView, newPodcast);
                }
                catch (WebException)
                {
                    messageBoxDisplayService.Show("Sorry, that podcast could not be found. Please check the URL");
                }
                catch (XmlException)
                {
                    messageBoxDisplayService.Show("Sorry, the URL is not a podcast feed");
                }
                catch (XmlRssChannelNodeNotFoundException ex)
                {
                    messageBoxDisplayService.Show(ex.Message);
                }
            }
        }
    }
}
