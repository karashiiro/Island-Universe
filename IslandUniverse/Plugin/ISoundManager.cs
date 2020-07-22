using IslandUniverse.Audio;

namespace IslandUniverse.Plugin
{
    public interface ISoundManager
    {
        /// <summary>
        ///     Creates a single, self-contained sound from a file.
        /// </summary>
        /// <param name="audioFilePath"></param>
        /// <returns></returns>
        Sound CreateSound(string audioFilePath);

        /// <summary>
        ///     Creates a sound channel that can be loaded with any number of sounds, with the caveat
        ///     that the channel can only play one sound file at a time.
        /// </summary>
        /// <returns></returns>
        SoundChannel CreateSoundChannel();
    }
}
