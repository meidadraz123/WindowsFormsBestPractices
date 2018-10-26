using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class EpisodePresenter
    {
        private Episode currentEpisode;
        private readonly Timer timer;
        private readonly IEpisodeView episodeView;
        private readonly IPodcastPlayer podcastPlayer;

        public EpisodePresenter(IEpisodeView episodeView, IPodcastPlayer podcastPlayer)
        {
            this.episodeView = episodeView;
            this.podcastPlayer = podcastPlayer;
            episodeView.Title = "";
            episodeView.PublicationDate = "";
            episodeView.PositionChanged += (s, a) => podcastPlayer.PositionInSeconds = episodeView.PositionInSeconds;
            episodeView.NoteCreated += EpisodeViewOnNoteCreated;

            timer = new Timer { Interval = 100 };
            timer.Tick += TimerOnTick;
            timer.Start();

            EventAggregator.Instance.Subscribe<EpisodeSelectedMessage>(OnSelectedEpisodeChanged);
            EventAggregator.Instance.Subscribe<ApplicationClosingMessage>(m => 
            {
                SaveEpisode();
                podcastPlayer.Dispose();
            });
        }

        private void EpisodeViewOnNoteCreated(object sender, NoteArgs noteArgs)
        {
            currentEpisode.Notes = String.Format(episodeView.Notes + $"{noteArgs.Position:hh\\:mm\\:ss}: {noteArgs.Note}\r\n");
            episodeView.Notes = currentEpisode.Notes;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (podcastPlayer != null && podcastPlayer.IsPlaying)
            {
                episodeView.PositionInSeconds = podcastPlayer.PositionInSeconds;
            }
        }

        private async void OnSelectedEpisodeChanged(EpisodeSelectedMessage episodeSelectedMessage)
        {
            podcastPlayer.UnloadEpisode();
            SaveEpisode();
            currentEpisode = episodeSelectedMessage.Episode;
            episodeView.Title = currentEpisode.Title;
            episodeView.PublicationDate = currentEpisode.PubDate;
            episodeView.Description = currentEpisode.Description;
            currentEpisode.IsNew = false;
            episodeView.Rating = currentEpisode.Rating;
            episodeView.Tags = String.Join(",", currentEpisode.Tags ?? new string[0]);
            episodeView.Notes = currentEpisode.Notes ?? "";
            podcastPlayer.LoadEpisode(currentEpisode);
            if (currentEpisode.Peaks == null || currentEpisode.Peaks.Length == 0)
            {
                episodeView.SetPeaks(null);
                currentEpisode.Peaks = await podcastPlayer.LoadPodcastAsync();
            }
            episodeView.SetPeaks(currentEpisode.Peaks);
        }

        private void SaveEpisode()
        {
            if (currentEpisode == null) return;

            currentEpisode.Tags = episodeView.Tags.Split(new[] { ',' }).Select(s => s.Trim()).ToArray();
            currentEpisode.Rating = episodeView.Rating;
            currentEpisode.Notes = episodeView.Notes;
        }
    }
}
