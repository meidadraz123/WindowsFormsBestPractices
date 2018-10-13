using PluralsightWinFormsDemoApp.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using NAudio.Wave;

namespace PluralsightWinFormsDemoApp
{
    public partial class MainForm : Form
    {
        private Episode currentEpisode;
        private EpisodeView episodeView;
        private PodcastView podcastView;
        private SubscriptionView subscriptionView;
        private WaveOutEvent player;

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
            List<Podcast> podcasts;
            if (File.Exists("subscriptions.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<Podcast>));
                using (var s = File.OpenRead("subscriptions.xml"))
                {
                    podcasts = (List<Podcast>) serializer.Deserialize(s);
                }
            }
            else
            {
                var defaultFeeds = new[]
                {
                    "http://hwpod.libsyn.com/rss",
                    "http://feeds.feedburner.com/herdingcode",
                    "http://www.pwop.com/feed.aspx?show=dotnetrocks&amp;filetype=master",
                    "http://feeds.feedburner.com/JesseLibertyYapcast",
                    "http://feeds.feedburner.com/HanselminutesCompleteMP3"
                };
                podcasts = defaultFeeds.Select(f => new Podcast() { SubscriptionUrl = f }).ToList();
            }

            foreach (var pod in podcasts)
            {
                var updatePodcastTask = Task.Run(() => UpdatePodcast(pod));
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

        void UpdatePodcast(Podcast podcast)
        {
            var r = new Random();
            Thread.Sleep(r.Next(3000));
            var doc = new XmlDocument();
            doc.Load(podcast.SubscriptionUrl);

            XmlElement channel;
            try
            {
                channel = doc["rss"]["channel"];
            }
            // For example: 
            // 1 - http://feeds.feedburner.com/soundcode is a valid XML document to a Blog site but it's not a Podcast URL
            // 2 - https://www.nasa.gov/rss/dyn/breaking_news.rss is a valid XML document and contain the RSS->Channel node and therefore
            //     it won't cause an exception and will load currently, but when trying to play the podcast an exception will be thrown 
            //     since the "episode.AudioFile" string is not a URL toa podcast audio file. 
            // TODO: need to verify that the episode.AudioFile is indeed playable.
            catch (NullReferenceException ex) 
            {
                throw new XmlRssChannelNodeNotFoundException("Sorry, this is not a Podcast URL. Could not find the rss->channel node inside the XML document",ex);
            }
        
            XmlNodeList items = channel.GetElementsByTagName("item");
            podcast.Title = channel["title"].InnerText;
            podcast.Link = channel["link"].InnerText;
            podcast.Description = channel["description"].InnerText;
            if (podcast.Episodes == null) podcast.Episodes = new List<Episode>();
            foreach (XmlNode item in items)
            {
                var guid = item["guid"].InnerText;
                var episode = podcast.Episodes.FirstOrDefault(e => e.Guid == guid);
                if (episode == null)
                {
                    episode = new Episode() { Guid = guid, IsNew = true};
                    episode.Title = item["title"].InnerText;
                    episode.PubDate = item["pubDate"].InnerText;
                    var xmlElement = item["description"];
                    if (xmlElement != null) episode.Description = xmlElement.InnerText;
                    var element = item["link"];
                    if (element != null) episode.Link = element.InnerText;
                    var enclosureElement = item["enclosure"];
                    if (enclosureElement != null) episode.AudioFile = enclosureElement.Attributes["url"].InnerText;
                    podcast.Episodes.Add(episode);
                }
            }
        }



        private void OnSelectedEpisodeChanged(object sender, TreeViewEventArgs e)
        {
            if (player != null) player.Dispose();
            player = null;

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
            if (player == null)
            {
                if (currentEpisode.AudioFile == null)
                {
                    MessageBox.Show("No audio file download provided");
                    Process.Start(currentEpisode.AudioFile ?? currentEpisode.Link);
                    return;
                }
                try
                {
                    player = new WaveOutEvent();
                    player.Init(new MediaFoundationReader(currentEpisode.AudioFile));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error retrieving podcast audio");
                    player = null;
                }

            }
            if (player != null)
            {
                player.Play();
            }
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
                    UpdatePodcast(pod);
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
            var serializer = new XmlSerializer(typeof(List<Podcast>));
            using (var s = File.Create("subscriptions.xml"))
            {
                var podcasts = subscriptionView.treeViewPodcasts.Nodes
                    .Cast<TreeNode>()
                    .Select( tn => tn.Tag)
                    .OfType<Podcast>()
                    .ToList();
                serializer.Serialize(s, podcasts);
            }
            if (player != null)
            {
                player.Dispose();
            }
        }

        private void OnMainFormHelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show($"Help for {this.Text}");
        }

        private void OnToolStripButtonPauseClick(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Pause();
            }
        }

        private void OnToolStripButtonStopClick(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Stop();
            }
        }

        private void OnToolStripButtonFavouriteCheckStateChanged(object sender, EventArgs e)
        {
            toolStripButtonFavourite.Image = toolStripButtonFavourite.Checked ?
                IconResources.star_icon_fill_32 :
                IconResources.star_icon_32;
        }
    }
}
