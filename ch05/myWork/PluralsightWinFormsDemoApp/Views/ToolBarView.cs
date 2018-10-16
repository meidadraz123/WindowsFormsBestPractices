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
    public partial class ToolBarView : UserControl, IToolbarView
    {
        public ToolBarView()
        {
            InitializeComponent();
        }

        public Image FavouriteImage
        {
            set { toolStripButtonFavourite.Image = value; }
        }

        public bool EpisodeIsFavourite
        {
            get { return toolStripButtonFavourite.Checked; }
            set { toolStripButtonFavourite.Checked = value; }
        }

        public event EventHandler StopClicked
        {
            add { toolStripButtonStop.Click += value; }
            remove { toolStripButtonStop.Click -= value; }
        }

        public event EventHandler PauseClicked
        {
            add { toolStripButtonPause.Click += value; }
            remove { toolStripButtonPause.Click -= value; }
        }

        public event EventHandler PlayClicked
        {
            add { toolStripButtonPlay.Click += value; }
            remove { toolStripButtonPlay.Click -= value; }
        }

        public event EventHandler AddPodcastClicked
        {
            add { toolStripButtonAddSubscription.Click += value; }
            remove { toolStripButtonAddSubscription.Click -= value; }
        }

        public event EventHandler RemovePodcastClicked
        {
            add { toolStripButtonRemoveSubscription.Click += value; }
            remove { toolStripButtonRemoveSubscription.Click -= value; }
        }
        public event EventHandler EpisodeFavouriteChanged
        {
            add { toolStripButtonFavourite.CheckStateChanged += value; }
            remove { toolStripButtonFavourite.CheckStateChanged -= value; }
        }

        public event EventHandler SettingsClicked
        {
            add { toolStripButtonSettings.Click += value; }
            remove { toolStripButtonSettings.Click -= value; }
        }
    }

    public interface IToolbarView
    {
        event EventHandler StopClicked;
        event EventHandler PauseClicked;
        event EventHandler PlayClicked;
        event EventHandler AddPodcastClicked;
        event EventHandler RemovePodcastClicked;
        event EventHandler EpisodeFavouriteChanged;
        event EventHandler SettingsClicked;

        Image FavouriteImage { set; }

        bool EpisodeIsFavourite { get; set; }
    }
}
