using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class PodcastLoader
    {
        public PodcastLoader()
        {

        }

        public async Task LoadPodcast(Podcast podcast)
        {
            var r = new Random();
            await Task.Delay(r.Next(3000));
            var doc = new XmlDocument();
            await Task.Run( () => doc.Load(podcast.SubscriptionUrl));

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
                throw new XmlRssChannelNodeNotFoundException("Sorry, this is not a Podcast URL. Could not find the rss->channel node inside the XML document", ex);
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
                    episode = new Episode() { Guid = guid, IsNew = true };
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
    }
}
