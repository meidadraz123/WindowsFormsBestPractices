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
    public partial class EpisodeView : UserControl, IEpisodeView
    {
        public EpisodeView()
        {
            InitializeComponent();
            toolTipEpisodeView.SetToolTip(textBoxMyTags, 
                "Enter tags for this podcast, comma separated");
        }

        public string Title
        {
            set
            {
                labelEpisodeTitle.Text = value;
            }
        }

        public string PubDate
        {
            set
            {
                labelPublicationDate.Text = value;
            }
        }
        public string Description
        {
            set
            {
                labelDescription.Text = value;
            }
        }
        public string Tags
        {
            get
            {
                return textBoxMyTags.Text;
            }
            set
            {
                textBoxMyTags.Text = value;
            }
        }
        public string Notes
        {
            get
            {
                return textBoxMyNotes.Text;
            }
            set
            {
                textBoxMyNotes.Text = value;
            }
        }
        public int Rating
        {
            get
            {
                return (int)numericUpDownMyRating.Value;
            }
            set
            {
                numericUpDownMyRating.Value = value;
            }
        }

        public int PositionInSeconds
        {
            get { return waveformViewer1.PositionInSeconds; }
            set
            {
                if (! waveformViewer1.IsDragging)
                {
                    waveformViewer1.PositionInSeconds = value;
                }
            }
        }

        public void SetPeaks(float[] newPeaks)
        {
            waveformViewer1.SetPeaks(newPeaks);
        }

        private void OnTextBoxTagsHelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(TextResources.TagsHelp);
        }

        public event EventHandler PositionChanged
        {
            add { waveformViewer1.PositionChanged += value; }
            remove { waveformViewer1.PositionChanged -= value; }
        }

        public event EventHandler<NoteArgs> NoteCreated
        {
            add { waveformViewer1.NoteCreated += value; }
            remove { waveformViewer1.NoteCreated -= value; }
        }
    }

    public interface IEpisodeView
    {
        string Title { set; }
        string PubDate { set; }
        string Description { set; }
        string Tags { get; set; }
        string Notes { get; set; }
        int Rating { get; set; }
        void SetPeaks(float[] newPeaks);
        int PositionInSeconds { get; set; }
        event EventHandler PositionChanged;
        event EventHandler<NoteArgs> NoteCreated;
    }

    public class NoteArgs : EventArgs
    {
        public NoteArgs(string note, TimeSpan position)
        {
            Note = note;
            Position = position;
        }
        public string Note { get; private set; }
        public TimeSpan Position { get; private set; }
    }
}
