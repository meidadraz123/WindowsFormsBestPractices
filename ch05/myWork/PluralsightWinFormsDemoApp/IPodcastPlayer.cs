using PluralsightWinFormsDemoApp.Model;
using System;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp
{
    public interface IPodcastPlayer : IDisposable
    {
        void LoadEpisode(Episode selectedEpisode);
        void UnloadEpisode();
        Task Play();
        void Stop();
        void Pause();
    }
}