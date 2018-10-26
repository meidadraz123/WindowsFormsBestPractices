using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class PodcastPresenter
    {
        private readonly IPodcastView podcastView;
        public PodcastPresenter(IPodcastView podcastView)
        {
            this.podcastView = podcastView;
            EventAggregator.Instance.Subscribe<PodcastSelectedMessage>(m =>
           {
               podcastView.SetPodcastTitle(m.Podcast.Title);
               podcastView.SetEpisodeCount($"{m.Podcast.Episodes.Count} episodes");
               podcastView.SetPoscastUrl(m.Podcast.Link);
           });
        }
    }
}
