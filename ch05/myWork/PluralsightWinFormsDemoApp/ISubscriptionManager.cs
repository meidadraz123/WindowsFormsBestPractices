using PluralsightWinFormsDemoApp.Model;
using System.Collections.Generic;

namespace PluralsightWinFormsDemoApp
{
    public interface ISubscriptionManager
    {
        List<Podcast> LoadPodcasts();

        void SavePodcasts(List<Podcast> podcasts);
    }
}