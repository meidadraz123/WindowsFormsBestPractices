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
    public partial class EpisodeView : UserControl
    {
        public EpisodeView()
        {
            InitializeComponent();
            toolTip1.SetToolTip(textBoxTags, 
                "Enter tag for this podcast, comma separated");
            toolTip1.SetToolTip(buttonPlay, 
                "Launch Windows Media Player to play this episode");
        }

        private void textBoxTags_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(TextResources.TagsHelp);
        }
    }
}
