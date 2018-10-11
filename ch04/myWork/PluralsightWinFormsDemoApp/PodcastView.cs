using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp
{
    public partial class PodcastView : UserControl
    {
        public PodcastView()
        {
            InitializeComponent();
        }

        internal void SetPodcast(Podcast selectedPodcast)
        {
            labelTitle.Text = selectedPodcast.Title;
            labelEpisodeCount.Text = String.Format($"{selectedPodcast.Episodes.Count} episodes");
        }
    }
}
