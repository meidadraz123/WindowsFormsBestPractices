using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluralsightWinFormsDemoApp.Commands;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class MainFormPresenter
    {
        private readonly IMainFormView mainFormView;
        private Episode currentEpisode;
        private readonly IPodcastPlayer podcastPlayer;
        private readonly ISubscriptionManager subscriptionManager;
        private readonly IPodcastLoader podcastLoader;

        private readonly ISubscriptionView subscriptionView;
        private readonly IEpisodeView episodeView;
        private readonly IPodcastView podcastView;
        private readonly IMessageBoxDisplayService messageBoxDisplayService;
        private readonly ISettingsService settingsService;
        private readonly IToolbarCommand[] commands;
        private readonly Timer timer;

        public MainFormPresenter( IMainFormView mainFormView,
            IPodcastLoader podcastLoader,
            ISubscriptionManager subscriptionManager,
            IPodcastPlayer podcastPlayer,
            IMessageBoxDisplayService messageBoxDisplayService,
            ISettingsService settingsService,
            ISystemInformationService systemInformationService,
            IToolbarCommand[] commands)
        {
            this.podcastPlayer = podcastPlayer;
            this.subscriptionManager = subscriptionManager;
            this.podcastLoader = podcastLoader;
            this.messageBoxDisplayService = messageBoxDisplayService;
            this.settingsService = settingsService;
            this.commands = commands;


            subscriptionView = mainFormView.SubscriptionView;
            episodeView = mainFormView.EpisodeView;
            podcastView = mainFormView.PodcastView;

            this.mainFormView = mainFormView;
            mainFormView.Load += MainFormViewOnLoad;
            mainFormView.FormClosed += MainFormViewOnFormClosed;
            mainFormView.HelpRequested += OnHelpRequested;
            mainFormView.KeyUp += MainFormViewOnKeyUp;
            mainFormView.ToolbarView.SetCommands(commands);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerOnTick;
            timer.Start();

            episodeView.Title = "";
            episodeView.Description = "";
            episodeView.PubDate= "";
            subscriptionView.SelectionChanged += OnSelectedEpisodeChanged;
            if (! systemInformationService.IsHighContrastColourScheme)
            {
                mainFormView.BackColor = System.Drawing.Color.White;
            }
            subscriptionManager.LoadPodcasts();

            episodeView.PositionChanged += (s, a) => podcastPlayer.PositionInSeconds = episodeView.PositionInSeconds;
        }



        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (podcastPlayer.IsPlaying)
            {
                episodeView.PositionInSeconds = podcastPlayer.PositionInSeconds;
            }
        }

        private void MainFormViewOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs keyEventArgs)
        {
            if (subscriptionView.SelectedNode != null && subscriptionView.SelectedNode.Tag is Episode)
            {
                var command = commands.FirstOrDefault(c => keyEventArgs.KeyData == c.ShortcutKey);
                if (command != null)
                {
                    command.Execute();
                    keyEventArgs.Handled = true;
                }
            }
        }

        private async void MainFormViewOnLoad(object sender, EventArgs eventArgs)
        {
            foreach (var podcast in subscriptionManager.Podcasts)
            {
                var podcastLoadTask = Task.Run( () => podcastLoader.UpdatePodcast(podcast));
                var firstFinished = await Task.WhenAny(podcastLoadTask, Task.Delay(5000));
                if (firstFinished == podcastLoadTask)
                {
                    Utils.AddPodcastToSubscriptionView(subscriptionView, podcast);
                    if (subscriptionView.SelectedNode == null && !subscriptionView.IsEmpty())
                    {
                        Utils.SelectFirstEpisode( subscriptionView, subscriptionManager);
                    } 
                }
            }

            if (settingsService.FirstRun)
            {
                messageBoxDisplayService.Show("Welcome! Get started by clicking Add to subscribe to a podcast");
                settingsService.FirstRun = false;
                settingsService.Save();
            }
        }

        private void OnHelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
        {
            messageBoxDisplayService.Show($"Help for {sender.ToString()}");
        }

        private async void OnSelectedEpisodeChanged(object sender, EventArgs e)
        {
            podcastPlayer.UnloadEpisode();
            if (subscriptionView.SelectedNode == null)
            {
                return;
            }
            if (subscriptionView.SelectedNode.Tag is Episode selectedEpisode)
            {
                EventAggregator.Instance.Publish(new EpisodeSelectedMessage(selectedEpisode));
                mainFormView.ShowEpisodeView();
                SaveEpisode();
                currentEpisode = selectedEpisode;
                episodeView.Title = currentEpisode.Title;
                episodeView.PubDate = currentEpisode.PubDate;
                episodeView.Description = currentEpisode.Description;
                currentEpisode.IsNew = false;
                episodeView.Rating= currentEpisode.Rating;
                episodeView.Tags= String.Join(",", currentEpisode.Tags ?? new string[0]);
                episodeView.Notes = currentEpisode.Notes ?? "";
                podcastPlayer.LoadEpisode(currentEpisode);
                if (currentEpisode.Peaks == null || currentEpisode.Peaks.Length == 0)
                {
                    episodeView.SetPeaks(null);
                    currentEpisode.Peaks = await podcastPlayer.LoadPodcastAsync();
                }
                episodeView.SetPeaks(currentEpisode.Peaks);
            }

            if (subscriptionView.SelectedNode.Tag is Podcast selectedPodcast)
            {
                EventAggregator.Instance.Publish(new PodcastSelectedMessage(selectedPodcast));
                mainFormView.ShowPodcastView();
                podcastView.SetPodcastTitle(selectedPodcast.Title);
                podcastView.SetEpisodeCount($"{selectedPodcast.Episodes.Count} episodes");
            }
        }

        private void MainFormViewOnFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs formClosedEventArgs)
        {
            SaveEpisode();
            subscriptionManager.SavePodcasts();
            podcastPlayer.Dispose();
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
