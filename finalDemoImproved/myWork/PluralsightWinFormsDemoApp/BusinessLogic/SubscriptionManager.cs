using PluralsightWinFormsDemoApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class SubscriptionManager : ISubscriptionManager
    {
        private readonly string podcastsSubscriptionFilePath;
        private List<Podcast> podcasts;

        public SubscriptionManager(string podcastsSubscriptionFilePath)
        {
            this.podcastsSubscriptionFilePath = podcastsSubscriptionFilePath;
            LoadPodcasts();
        }

        public void LoadPodcasts()
        {
            if (File.Exists(podcastsSubscriptionFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Podcast>));
                using (var s = File.OpenRead(podcastsSubscriptionFilePath))
                {
                    podcasts = (List<Podcast>)serializer.Deserialize(s);
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
                podcasts = defaultFeeds.Select(f => new Podcast() { SubscriptionUrl = f , Id = Guid.NewGuid()}).ToList();
            }
        }

        public void SavePodcasts()
        {
            var serializer = new XmlSerializer(typeof(List<Podcast>));
            using (var s = File.Create(podcastsSubscriptionFilePath))
            {
                serializer.Serialize(s, Podcasts);
            }
        }

        public void AddPodcast(Podcast podcast)
        {
            podcasts.Add(podcast);
            SavePodcasts();
        }

        public void RemovePodcast(Podcast podcast)
        {
            podcasts.Remove(podcast);
            SavePodcasts();
        }

        public IEnumerable<Podcast> Podcasts { get { return podcasts; } }
    }
}
