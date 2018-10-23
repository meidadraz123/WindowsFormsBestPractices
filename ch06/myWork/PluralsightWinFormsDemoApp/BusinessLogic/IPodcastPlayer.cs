using PluralsightWinFormsDemoApp.Model;
using System;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    public interface IPodcastPlayer : IDisposable
    {
        void LoadEpisode(Episode selectedEpisode);
        void UnloadEpisode();
        Task Play();
        void Stop();
        void Pause();
        Task<float[]> LoadPodcastAsync();
    }
}