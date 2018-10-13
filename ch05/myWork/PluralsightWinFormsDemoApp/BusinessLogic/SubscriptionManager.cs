using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class SubscriptionManager
    {
        private readonly string podcastsSubscriptionFilePath;

        public SubscriptionManager(string podcastsSubscriptionFilePath)
        {
            this.podcastsSubscriptionFilePath = podcastsSubscriptionFilePath;
        }

        public List<Podcast> LoadPodcasts()
        {
            List<Podcast> podcasts;
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
            return podcasts;
        }

        public void SavePodcasts(List<Podcast> podcasts)
        {
            var serializer = new XmlSerializer(typeof(List<Podcast>));
            using (var s = File.Create(podcastsSubscriptionFilePath))
            {
                serializer.Serialize(s, podcasts);
            }
        }
    }
}
