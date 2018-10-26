using PluralsightWinFormsDemoApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Events
{
    class PodcastLoadCompletedMessage : IApplicationEvent
    {
        public PodcastLoadCompletedMessage(Podcast[] podcasts)
        {
            Podcasts = podcasts;
        }

        public Podcast[] Podcasts { get; private set; }
    }
}
