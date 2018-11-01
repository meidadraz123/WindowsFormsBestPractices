using PluralsightWinFormsDemoApp.Model;
using System;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    internal interface IPodcastPlayer : IDisposable
    {
        void LoadEpisode(Episode selectedEpisode);
        void UnloadEpisode();
        Task Play();
        void Stop();
        void Pause();
        Task LoadPeaksAsync();
        int PositionInSeconds { get; set; }
        bool IsPlaying { get; }
    }
}
