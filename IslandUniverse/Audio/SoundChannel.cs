using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace IslandUniverse.Audio
{
    /// <summary>
    ///     An audio-player class for sounds that don't need to be frequently played, especially
    ///     without other sounds at the same time. If your sound does need to be played
    ///     with other sounds, consider using <see cref="Sound"/>.
    /// </summary>
    public class SoundChannel : IDisposable
    {
        private readonly IDictionary<string, AudioFileReader> audioFiles;
        private readonly WaveOutEvent outputDevice;

        public SoundChannel()
        {
            this.audioFiles = new Dictionary<string, AudioFileReader>();
            this.outputDevice = new WaveOutEvent();
        }

        public void Play(string audioFilePath)
        {
            if (!this.audioFiles.TryGetValue(audioFilePath, out var audioFile))
            {
                audioFile = new AudioFileReader(audioFilePath);
                this.audioFiles.Add(audioFilePath, audioFile);
            }
            this.outputDevice.Init(audioFile);
            this.outputDevice.Play();
        }

        public void Dispose()
        {
            foreach (var (_, audioFile) in this.audioFiles)
            {
                audioFile.Dispose();
            }
            this.outputDevice.Dispose();
        }
    }
}
