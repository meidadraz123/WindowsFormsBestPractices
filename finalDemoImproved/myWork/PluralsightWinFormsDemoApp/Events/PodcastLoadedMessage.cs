using PluralsightWinFormsDemoApp.BusinessLogic;
using PluralsightWinFormsDemoApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Events
{
    class PodcastLoadedMessage : IApplicationEvent
    {
        public PodcastLoadedMessage(Podcast podcast)
        {
            Podcast = podcast;
        }

        public Podcast Podcast { get; private set; }
    }
}
