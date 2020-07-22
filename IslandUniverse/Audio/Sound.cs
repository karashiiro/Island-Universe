using NAudio.Wave;
using System;

namespace IslandUniverse.Audio
{
    /// <summary>
    ///     An audio-player class for sounds that need to be frequently played, especially
    ///     over other sounds at the same time. If your sound doesn't need to be played
    ///     with other sounds, consider using <see cref="SoundChannel"/>.
    /// </summary>
    public class Sound : IDisposable
    {
        private readonly AudioFileReader audioFile;
        private readonly WaveOutEvent outputDevice;

        public Sound(string audioFilePath)
        {
            this.audioFile = new AudioFileReader(audioFilePath);
            this.outputDevice = new WaveOutEvent();
            this.outputDevice.Init(this.audioFile);
        }

        public void Play()
        {
            this.outputDevice.Play();
        }

        public void Dispose()
        {
            this.audioFile.Dispose();
            this.outputDevice.Dispose();
        }
    }
}
