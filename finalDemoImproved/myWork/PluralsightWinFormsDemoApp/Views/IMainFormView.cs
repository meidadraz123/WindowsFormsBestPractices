using System;
using System.Drawing;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Views
{
    interface IMainFormView
    {
        event EventHandler Load;
        event FormClosedEventHandler FormClosed;
        event HelpEventHandler HelpRequested;
        event KeyEventHandler KeyUp;

        IPodcastView PodcastView { get; }
        ISubscriptionView SubscriptionView { get; }

        Color BackColor { get; set; }

        void ShowEpisodeView();
        void ShowPodcastView();

    }
}
