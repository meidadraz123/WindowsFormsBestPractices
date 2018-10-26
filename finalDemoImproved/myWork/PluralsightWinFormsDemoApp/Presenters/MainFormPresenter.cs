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

        private readonly ISubscriptionView subscriptionView;
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


            subscriptionView = mainFormView.SubscriptionView;
            podcastView = mainFormView.PodcastView;

            this.mainFormView = mainFormView;
            mainFormView.Load += MainFormViewOnLoad;
            mainFormView.FormClosed += MainFormViewOnFormClosed;
            mainFormView.HelpRequested += OnHelpRequested;
            mainFormView.KeyUp += MainFormViewOnKeyUp;

            subscriptionView.SelectionChanged += OnSelectedEpisodeChanged;
            if (! systemInformationService.IsHighContrastColourScheme)
            {
                mainFormView.BackColor = System.Drawing.Color.White;
            }
            subscriptionManager.LoadPodcasts();
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

        private void OnSelectedEpisodeChanged(object sender, EventArgs e)
        {
            if (subscriptionView.SelectedNode == null)
            {
                return;
            }
            if (subscriptionView.SelectedNode.Tag is Episode selectedEpisode)
            {
                EventAggregator.Instance.Publish(new EpisodeSelectedMessage(selectedEpisode));
                mainFormView.ShowEpisodeView();
            }

            if (subscriptionView.SelectedNode.Tag is Podcast selectedPodcast)
            {
                EventAggregator.Instance.Publish(new PodcastSelectedMessage(selectedPodcast));
                mainFormView.ShowPodcastView();
                podcastView.SetPodcastTitle(selectedPodcast.Title);
                podcastView.SetEpisodeCount($"{selectedPodcast.Episodes.Count} episodes");
                podcastView.SetPoscastUrl(selectedPodcast.Link);
            }
        }

        private void MainFormViewOnFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs formClosedEventArgs)
        {
            EventAggregator.Instance.Publish(new ApplicationClosingMessage());
            subscriptionManager.SavePodcasts();
        }
    }
}
