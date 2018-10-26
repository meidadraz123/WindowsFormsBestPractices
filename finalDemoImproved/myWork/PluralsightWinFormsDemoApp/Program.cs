using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Commands;
using PluralsightWinFormsDemoApp.Presenters;
using PluralsightWinFormsDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            var culture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var subscriptionManager = new SubscriptionManager("subscriptions.xml");
            var podcastLoader = new PodcastLoader();
            var podcastPlayer = new PodcastPlayer();
            var messageBoxDisplayService = new MessageBoxDisplayService();
            var settingsService = new SettingsService();
            var systemInformationService = new SystemInformationService();
            var newSubscriptionService = new NewSubscriptionService();
            var toolbarView = new ToolBarView();
            var episodeView = new WpfEpisodeViewHost();
            var subscriptionView = new SubscriptionView();
            var mainFormView = new MainForm(episodeView, subscriptionView, toolbarView);

            var commands = new IToolbarCommand[]
                {
                    new AddSubscriptionCommand(subscriptionView, 
                        messageBoxDisplayService,
                        newSubscriptionService,
                        podcastLoader,
                        subscriptionManager),
                    new RemoveSubscriptionCommand(subscriptionView,subscriptionManager),
                    new PlayCommand(podcastPlayer),
                    new PauseCommand(podcastPlayer),
                    new StopCommand(podcastPlayer),
                    new FavouriteCommand(subscriptionView),
                    new SettingsCommand()
            };

            // for now, keep presenters alive with Tags
            toolbarView.Tag = new ToolbarPresenter(toolbarView, commands);
            episodeView.Tag = new EpisodePresenter(episodeView, podcastPlayer);
            subscriptionView.Tag = new SubscriptionPresenter(subscriptionView);


            var mainFormPresenter = new MainFormPresenter(mainFormView,
                podcastLoader, 
                subscriptionManager,
                messageBoxDisplayService,
                settingsService,
                systemInformationService,
                commands);

            Application.Run(mainFormView);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
                $"{((Exception)e.ExceptionObject).Message}\r\n" +
                "Please contact support.");
            Console.WriteLine($"ERROR {DateTimeOffset.Now}:{(Exception)e.ExceptionObject}");

            MessageBox.Show(message, "Unexpected Error");
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
                $"{e.Exception.Message}\r\n" +
                "Please contact support.");
            Console.WriteLine($"ERROR {DateTimeOffset.Now}:{e.Exception}");

            MessageBox.Show(message, "Unexpected Error");
        }
    }
}
