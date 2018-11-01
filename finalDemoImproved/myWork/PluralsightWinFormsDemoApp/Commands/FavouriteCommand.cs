using System;
using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;

namespace PluralsightWinFormsDemoApp.Commands
{
    class FavouriteCommand : CommandBase
    {
        private readonly ISubscriptionView subscriptionView;
        private Episode currentEpisode;

        public FavouriteCommand(ISubscriptionView subscriptionView)
        {
            this.subscriptionView = subscriptionView;
            ToolTip = "Favourite";
            SetIcon();
            subscriptionView.SelectionChanged += SubscriptionViewOnSelectionChanged;
        }

        private void SubscriptionViewOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            if (subscriptionView.SelectedNode.Tag is Episode episode)
            {
                currentEpisode = episode;
                SetIcon();
            }
            if (subscriptionView.SelectedNode.Tag is Podcast)
            {
                Icon = IconResources.star_icon_32;
            }


        }

        private void SetIcon()
        {
            Icon = (currentEpisode != null && currentEpisode.IsFavourite) ?
                IconResources.star_icon_fill_32 :
                IconResources.star_icon_32;
        }

        public override void Execute()
        {
            if (currentEpisode != null)
            {
                currentEpisode.IsFavourite = !currentEpisode.IsFavourite;
                SetIcon();
            }
        }
    }
}
