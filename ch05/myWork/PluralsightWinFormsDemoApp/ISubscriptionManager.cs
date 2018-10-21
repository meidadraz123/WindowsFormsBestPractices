using PluralsightWinFormsDemoApp.Model;
using System.Collections.Generic;

namespace PluralsightWinFormsDemoApp
{
    public interface ISubscriptionManager
    {
        void LoadPodcasts();
        void SavePodcasts();
        void AddPodcast(Podcast podcast);
        void RemovePodcast(Podcast podcast);
        IEnumerable<Podcast> Podcasts { get; }
    }
}