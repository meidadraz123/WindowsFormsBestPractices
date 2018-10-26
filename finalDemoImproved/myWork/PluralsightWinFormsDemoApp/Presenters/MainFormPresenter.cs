using PluralsightWinFormsDemoApp.Model;
using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Commands;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluralsightWinFormsDemoApp.Events;

namespace PluralsightWinFormsDemoApp.Presenters
{
    class MainFormPresenter
    {
        private readonly IMainFormView mainFormView;
        private readonly ISubscriptionManager subscriptionManager;
        private readonly IPodcastLoader podcastLoader;

        private readonly IPodcastView podcastView;
        private readonly IMessageBoxDisplayService messageBoxDisplayService;
        private readonly ISettingsService settingsService;
        private readonly IToolbarCommand[] commands;

        public MainFormPresenter( IMainFormView mainFormView,
            IPodcastLoader podcastLoader,
            ISubscriptionManager subscriptionManager,
            IMessageBoxDisplayService messageBoxDisplayService,
            ISettingsService settingsService,
            ISystemInformationService systemInformationService,
            IToolbarCommand[] commands)
        {
            this.subscriptionManager = subscriptionManager;
            this.podcastLoader = podcastLoader;
            this.messageBoxDisplayService = messageBoxDisplayService;
            this.settingsService = settingsService;
            this.commands = commands;

            podcastView = mainFormView.PodcastView;

            this.mainFormView = mainFormView;
            mainFormView.Load += MainFormViewOnLoad;
            mainFormView.FormClosed += MainFormViewOnFormClosed;
            mainFormView.HelpRequested += OnHelpRequested;
            mainFormView.KeyUp += MainFormViewOnKeyUp;

            if (! systemInformationService.IsHighContrastColourScheme)
            {
                mainFormView.BackColor = System.Drawing.Color.White;
            }
            this.subscriptionManager.LoadPodcasts();

            EventAggregator.Instance.Subscribe<EpisodeSelectedMessage>(m => mainFormView.ShowEpisodeView());
            EventAggregator.Instance.Subscribe<PodcastSelectedMessage>(m =>
            {
                mainFormView.ShowPodcastView();
                podcastView.SetPodcastTitle(m.Podcast.Title);
                podcastView.SetEpisodeCount($"{m.Podcast.Episodes.Count} episodes");
                podcastView.SetPoscastUrl(m.Podcast.Link);
            } );
        }

        private void MainFormViewOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs keyEventArgs)
        {
            var command = commands.FirstOrDefault(c => keyEventArgs.KeyData == c.ShortcutKey);
            if (command != null)
            {
                command.Execute();
                keyEventArgs.Handled = true;
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
                    EventAggregator.Instance.Publish(new PodcastLoadedMessage(podcast));
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

        private void MainFormViewOnFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs formClosedEventArgs)
        {
            EventAggregator.Instance.Publish(new ApplicationClosingMessage());
            subscriptionManager.SavePodcasts();
        }
    }
}
