using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Commands
{
    class FavouriteCommand : CommandBase
    {
        private Episode currentEpisode;
        private readonly ISubscriptionView subscriptionView;
        public FavouriteCommand(ISubscriptionView subscriptionView)
        {
            this.subscriptionView = subscriptionView;
            ToolTip = "Favourite";
            SetIcon();
            subscriptionView.SelectionChanged += OnSubscriptionViewSelectionChanged;
        }

        private void OnSubscriptionViewSelectionChanged(object sender, EventArgs e)
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
