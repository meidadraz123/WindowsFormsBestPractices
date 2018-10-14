using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class PodcastPlayer : IDisposable
    {
        private WaveOutEvent player;
        private Episode currentEpisode;

        public PodcastPlayer()
        {

        }

        public void Dispose()
        {
            if (player != null)
            {
                player.Dispose();
            }
        }

        public void LoadEpisode(Episode selectedEpisode)
        {
            currentEpisode = selectedEpisode;
        }

        public void UnloadEpisode()
        {
            Dispose();
            currentEpisode = null;
            player = null;
        }

        public async Task Play()
        {
            if (currentEpisode == null)
            {
                MessageBox.Show("No podcast episode was loaded");
                return;
            }
            if (player == null)
            {
                if (currentEpisode.AudioFile == null)
                {
                    MessageBox.Show("No audio file download provided");
                    Process.Start(currentEpisode.Link ?? "");
                    return;
                }
                try
                {
                    player = new WaveOutEvent();
                    await Task.Run( () => player.Init(new MediaFoundationReader(currentEpisode.AudioFile)));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error retrieving podcast audio");
                    player = null;
                }

            }
            if (player != null)
            {
                player.Play();
            }
        }

        public void Stop()
        {
            if (player != null)
            {
                player.Stop();
            }
        }

        public void Pause()
        {
            if (player != null)
            {
                player.Pause();
            }
        }
    }
}
