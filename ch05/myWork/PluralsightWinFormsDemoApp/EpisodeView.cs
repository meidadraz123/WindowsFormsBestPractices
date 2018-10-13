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
            toolTipEpisodeView.SetToolTip(textBoxMyTags, 
                "Enter tags for this podcast, comma separated");
        }

        private void OnTextBoxTagsHelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(TextResources.TagsHelp);
        }
    }
}
