using PluralsightWinFormsDemoApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PluralsightWinFormsDemoApp.BusinessLogic;

namespace PluralsightWinFormsDemoApp
{
    public partial class MainForm : Form
    {
        private Episode currentEpisode;
        private EpisodeView episodeView;
        private PodcastView podcastView;
        private SubscriptionView subscriptionView;
        private SubscriptionManager subscriptionManager;
        private PodcastLoader podcastLoader;
        private PodcastPlayer podcastPlayer;

        public MainForm()
        {
            InitializeComponent();
            episodeView = new EpisodeView() { Dock = DockStyle.Fill };
            podcastView = new PodcastView() { Dock = DockStyle.Fill };
            subscriptionView = new SubscriptionView() { Dock = DockStyle.Fill };
            splitContainerMainForm.Panel1.Controls.Add(subscriptionView);
            episodeView.labelDescription.Text = "";
            episodeView.labelEpisodeTitle.Text = "";
            episodeView.labelPublicationDate.Text = "";
            subscriptionView.treeViewPodcasts.AfterSelect += OnSelectedEpisodeChanged;
            if (! SystemInformation.HighContrast)
            {
                this.BackColor = System.Drawing.Color.White;
            }
            subscriptionManager = new SubscriptionManager("subscriptions.xml");
            podcastLoader = new PodcastLoader();
            podcastPlayer = new PodcastPlayer();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Space | Keys.Control) && splitContainerMainForm.Panel2.Controls.Contains(episodeView))
            {
                toolStripButtonPlay.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void OnFormLoad(object sender, EventArgs e)
        {

            var podcasts = subscriptionManager.LoadPodcasts();
            foreach (var pod in podcasts)
            {
                var updatePodcastTask = Task.Run(() => podcastLoader.UpdatedPodcast(pod));
                var firstTask = await Task.WhenAny(updatePodcastTask, Task.Delay(5000)); // timeout for loading the podcast
                if (firstTask == updatePodcastTask)
                {
                    AddPodcastToTreeView(pod);
                }
            }
            SelectFirstEpisode();

            if (Settings.Default.FirstRun)
            {
                MessageBox.Show("Welcome! Get started by clicking Add to subscribe to a podcast");
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
        }

        private void SelectFirstEpisode()
        {
            if (subscriptionView.treeViewPodcasts.GetNodeCount(false) != 0)
            {
                subscriptionView.treeViewPodcasts.SelectedNode =
                    subscriptionView.treeViewPodcasts.Nodes[0].FirstNode;
            }
        }

        private void AddPodcastToTreeView(Podcast pod)
        {
            var podNode = new TreeNode(pod.Title) { Tag = pod};
           subscriptionView.treeViewPodcasts.Nodes.Add(podNode);
            foreach (var episode in pod.Episodes)
            {
                podNode.Nodes.Add(new TreeNode(episode.Title) { Tag = episode });
            }
        }

        private void OnSelectedEpisodeChanged(object sender, TreeViewEventArgs e)
        {
            podcastPlayer.UnloadEpisode();

            var selectedEpisode = subscriptionView.treeViewPodcasts.SelectedNode.Tag as Episode;
            if (selectedEpisode != null)
            {
                splitContainerMainForm.Panel2.Controls.Clear();
                splitContainerMainForm.Panel2.Controls.Add(episodeView);
                SaveEpisode();
                currentEpisode = selectedEpisode;
                episodeView.labelEpisodeTitle.Text = currentEpisode.Title;
                episodeView.labelPublicationDate.Text = currentEpisode.PubDate;
                episodeView.labelDescription.Text = currentEpisode.Description;
                toolStripButtonFavourite.Checked = currentEpisode.IsFavourite;
                currentEpisode.IsNew = false;
                episodeView.numericUpDownMyRating.Value = currentEpisode.Rating;
                episodeView.textBoxMyTags.Text = String.Join(",", currentEpisode.Tags ?? new string[0]);
                episodeView.textBoxMyNotes.Text = currentEpisode.Notes ?? "";
                podcastPlayer.LoadEpisode(selectedEpisode);
            }

            var selectedPodcast = subscriptionView.treeViewPodcasts.SelectedNode.Tag as Podcast;
            if (selectedPodcast != null)
            {
                splitContainerMainForm.Panel2.Controls.Clear();
                splitContainerMainForm.Panel2.Controls.Add(podcastView);
                podcastView.SetPodcast(selectedPodcast);
            }
        }

        private void SaveEpisode()
        {
            if (currentEpisode == null) return;

            currentEpisode.Tags = episodeView.textBoxMyTags.Text.Split(new[] { ',' }).Select(s => s.Trim()).ToArray();
            currentEpisode.Rating = (int)episodeView.numericUpDownMyRating.Value;
            currentEpisode.IsFavourite = toolStripButtonFavourite.Checked;
            currentEpisode.Notes = episodeView.textBoxMyNotes.Text;
        }

        private void OnToolStripButtonPlayClick(object sender, EventArgs e)
        {
            podcastPlayer.Play();
        }

        private void OnToolStripButtonRemovePodcastClick(object sender, EventArgs e)
        {
            var nodeToRemove = subscriptionView.treeViewPodcasts.SelectedNode;
            if (nodeToRemove.Parent != null)
            {
                nodeToRemove = nodeToRemove.Parent;
            }
            subscriptionView.treeViewPodcasts.Nodes.Remove(nodeToRemove);
            SelectFirstEpisode();
        }

        private void OnToolStripButtonAddSubscriptionClick(object sender, EventArgs e)
        {
            var form = new NewPodcastForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var pod = new Podcast() { SubscriptionUrl = form.PodcastUrl };
                try
                {
                    podcastLoader.UpdatedPodcast(pod);
                    AddPodcastToTreeView(pod);
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

        private void OnMainFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveEpisode();
            var podcasts = subscriptionView.treeViewPodcasts.Nodes
                .Cast<TreeNode>()
                .Select( tn => tn.Tag)
                .OfType<Podcast>()
                .ToList();
            subscriptionManager.SavePodcasts(podcasts);

            podcastPlayer.Dispose();
        }

        private void OnMainFormHelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show($"Help for {this.Text}");
        }

        private void OnToolStripButtonPauseClick(object sender, EventArgs e)
        {
            podcastPlayer.Pause();
        }

        private void OnToolStripButtonStopClick(object sender, EventArgs e)
        {
            podcastPlayer.Stop();
        }

        private void OnToolStripButtonFavouriteCheckStateChanged(object sender, EventArgs e)
        {
            toolStripButtonFavourite.Image = toolStripButtonFavourite.Checked ?
                IconResources.star_icon_fill_32 :
                IconResources.star_icon_32;
        }
    }
}
