using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PluralsightWinFormsDemoApp
{
    class MainFormPresenter
    {
        private readonly IMainFormView mainFormView;
        private Episode currentEpisode;
        private readonly PodcastPlayer podcastPlayer;
        private readonly SubscriptionManager subscriptionManager;
        private readonly PodcastLoader podcastLoader;
        private readonly List<Podcast> podcasts;

        private readonly ISubscriptionView subscriptionView;
        private readonly IEpisodeView episodeView;
        private readonly IToolbarView toolbarView;
        private readonly IPodcastView podcastView;

        public MainFormPresenter( IMainFormView mainFormView)
        {
            subscriptionView = mainFormView.SubscriptionView;
            episodeView = mainFormView.EpisodeView;
            toolbarView = mainFormView.ToolbarView;
            podcastView = mainFormView.PodcastView;

            this.mainFormView = mainFormView;
            mainFormView.Load += MainFormViewOnLoad;
            mainFormView.FormClosed += MainFormViewOnFormClosed;
            mainFormView.HelpRequested += OnHelpRequested;
            mainFormView.KeyUp += MainFormViewOnKeyUp;

            toolbarView.PlayClicked += OnButtonPlayClick;
            toolbarView.StopClicked += OnButtonStopClick;
            toolbarView.PauseClicked += OnButtonPauseClick;
            toolbarView.AddPodcastClicked += OnButtonAddSubscriptionClick;
            toolbarView.RemovePodcastClicked += OnButtonRemovePodcastClick;
            toolbarView.EpisodeFavouriteChanged += OnButtonEpisodeFavouriteChanged;
            toolbarView.SettingsClicked += OnButtonSettingsClick;

            episodeView.Title = "";
            episodeView.Description = "";
            episodeView.PubDate= "";
            subscriptionView.SelectionChanged += OnSelectedEpisodeChanged;
            if (!SystemInformation.HighContrast)
            {
                mainFormView.BackColor = System.Drawing.Color.White;
            }
            subscriptionManager = new SubscriptionManager("subscriptions.xml");
            podcastLoader = new PodcastLoader();
            podcastPlayer = new PodcastPlayer();
            podcasts = subscriptionManager.LoadPodcasts();
        }

        private void MainFormViewOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs keyEventArgs)
        {
            if (subscriptionView.SelectedNode == null)
            {
                return;
            }
            if (keyEventArgs.KeyData == (Keys.Control | Keys.Space) && subscriptionView.SelectedNode.Tag is Episode )
            {
                OnButtonPlayClick(this, EventArgs.Empty);
                keyEventArgs.Handled = true;
            }
        }

        private async void MainFormViewOnLoad(object sender, EventArgs eventArgs)
        {
            foreach (var podcast in podcasts)
            {
                var podcastLoadTask = Task.Run( () => podcastLoader.LoadPodcast(podcast));
                var firstFinished = await Task.WhenAny(podcastLoadTask, Task.Delay(5000));
                if (firstFinished == podcastLoadTask)
                {
                    AddPodcastToTreeView(podcast);
                    if (subscriptionView.SelectedNode == null)
                    {
                        SelectFirstEpisode();
                    } 
                }
            }

            if (Settings.Default.FirstRun)
            {
                MessageBox.Show("Welcome! Get started by clicking Add to subscribe to a podcast");
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
        }

        private void OnHelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
        {
            MessageBox.Show($"Help for {sender.ToString()}");
        }

        private void OnButtonEpisodeFavouriteChanged(object sender, EventArgs e)
        {
            toolbarView.FavouriteImage = toolbarView.EpisodeIsFavourite ?
                IconResources.star_icon_fill_32 :
                IconResources.star_icon_32;
        }

        private void OnSelectedEpisodeChanged(object sender, EventArgs e)
        {
            podcastPlayer.UnloadEpisode();
            if (subscriptionView.SelectedNode == null)
            {
                return;
            }
            if (subscriptionView.SelectedNode.Tag is Episode selectedEdepisode)
            {
                mainFormView.ShowEpisodeView();
                SaveEpisode();
                currentEpisode = selectedEdepisode;
                episodeView.Title = currentEpisode.Title;
                episodeView.PubDate = currentEpisode.PubDate;
                episodeView.Description = currentEpisode.Description;
                toolbarView.EpisodeIsFavourite = currentEpisode.IsFavourite;
                currentEpisode.IsNew = false;
                episodeView.Rating= currentEpisode.Rating;
                episodeView.Tags= string.Join(",", currentEpisode.Tags ?? new string[0]);
                episodeView.Notes = currentEpisode.Notes ?? "";
                podcastPlayer.LoadEpisode(selectedEdepisode);
            }

            if (subscriptionView.SelectedNode.Tag is Podcast selectedPodcast)
            {
                mainFormView.ShowPodcastView();
                podcastView.SetPodcastTitle(selectedPodcast.Title);
                podcastView.SetEpisodeCount($"{selectedPodcast.Episodes.Count} episodes");
            }
        }

        private void MainFormViewOnFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs formClosedEventArgs)
        {
            SaveEpisode();
            subscriptionManager.SavePodcasts(podcasts);
            podcastPlayer.Dispose();
        }

        private void SaveEpisode()
        {
            if (currentEpisode == null) return;

            currentEpisode.Tags = episodeView.Tags.Split(new[] { ',' }).Select(s => s.Trim()).ToArray();
            currentEpisode.Rating = episodeView.Rating;
            currentEpisode.IsFavourite = toolbarView.EpisodeIsFavourite;
            currentEpisode.Notes = episodeView.Notes;
        }

        private async void OnButtonPlayClick(object sender, EventArgs e)
        {
            await podcastPlayer.Play();
        }

        private void OnButtonRemovePodcastClick(object sender, EventArgs e)
        {
            if (subscriptionView.SelectedNode.Tag is Podcast podcast)
            {
                podcasts.Remove(podcast);
                subscriptionView.RemoveNode(podcast.Id.ToString());
                SelectFirstEpisode();
            }

            else if (subscriptionView.SelectedNode.Tag is Episode episode)
            {
                var relatedPodcast = podcasts.Where(p => p.Episodes.Where(ep => ep.Guid == episode.Guid).Any()).FirstOrDefault();
                podcasts.Remove(relatedPodcast);
                subscriptionView.RemoveNode(relatedPodcast.Id.ToString());
                SelectFirstEpisode();
            }
        }

        private async void OnButtonAddSubscriptionClick(object sender, EventArgs e)
        {
            var form = new NewPodcastForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var podcast = new Podcast() { SubscriptionUrl = form.PodcastUrl };
                try
                {
                    await podcastLoader.LoadPodcast(podcast);
                    podcasts.Add(podcast);
                    AddPodcastToTreeView(podcast);
                }
                catch (WebException)
                {
                    MessageBox.Show("Sorry, that podcast could not be found. Please check the URL");
                }
                catch (XmlException)
                {
                    MessageBox.Show("Sorry, the URL is not a podcast feed");
                }
                catch (XmlRssChannelNodeNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SelectFirstEpisode()
        {
            if (! subscriptionView.IsEmpty())
                subscriptionView.SelectNode(podcasts.SelectMany(p => p.Episodes).First().Guid);
        }

        private void AddPodcastToTreeView(Podcast podcast)
        {
            var podcastNode = new TreeNode(podcast.Title) { Tag = podcast, Name = podcast.Id.ToString() };
            foreach (var episode in podcast.Episodes)
            {
                podcastNode.Nodes.Add(new TreeNode(episode.Title) { Tag = episode, Name = episode.Guid });
            }
            subscriptionView.AddNode(podcastNode);
        }

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            podcastPlayer.Stop();
        }

        private void OnButtonPauseClick(object sender, EventArgs e)
        {
            podcastPlayer.Pause();
        }

        private void OnButtonSettingsClick(object sender, EventArgs e)
        {
        }
    }
}
