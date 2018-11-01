using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using PluralsightWinFormsDemoApp.Events;
using PluralsightWinFormsDemoApp.Model;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    class PodcastPlayer : IPodcastPlayer
    {
        private WaveOutEvent player;
        private Episode currentEpisode;
        private MediaFoundationReader currentReader;

        public PodcastPlayer()
        {

        }

        public void Dispose()
        {
            UnloadEpisode();
        }

        public void LoadEpisode(Episode selectedEpisode)
        {
            currentEpisode = selectedEpisode;
        }

        public void UnloadEpisode()
        {
            if (player != null)
            {
                player.Dispose();
            }
            if (currentReader != null)
            {
                currentReader.Dispose();
            }
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
                    currentReader = new MediaFoundationReader(currentEpisode.AudioFile);
                    await Task.Run(() => player.Init(currentReader));

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

        public async Task LoadPeaksAsync()
        {
            var episode = currentEpisode;
            await Task.Run(() =>
            {
                if (episode == null)
                {
                    return;
                }
                var peaks = new List<float>();
                using (var reader = new MediaFoundationReader(episode.AudioFile))
                {
                    var sampleProvider = reader.ToSampleProvider();
                    var sampleBuffer = new float[reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
                    int read;
                    do
                    {
                        read = sampleProvider.Read(sampleBuffer, 0, sampleBuffer.Length);
                        if (read > 0)
                        {
                            var max = sampleBuffer.Take(read).Select(Math.Abs).Max();
                            peaks.Add(max);
                        }
                    } while (read > 0);
                    episode.Peaks = peaks.ToArray();
                }
            });
            EventAggregator.Instance.Publish(new PeaksAvailableMessage(episode));
        }

        private int positionInSeconds;

        public int PositionInSeconds
        {
            get
            {
                if (currentReader != null)
                {
                    positionInSeconds = (int)currentReader.CurrentTime.TotalSeconds;
                }
                return positionInSeconds;
            }
            set
            {
                positionInSeconds = value;
                if (currentReader != null)
                {
                    currentReader.CurrentTime = TimeSpan.FromSeconds(value);
                }
            }
        }

        public bool IsPlaying { get { return player != null && player.PlaybackState == PlaybackState.Playing; } }
    }
}
