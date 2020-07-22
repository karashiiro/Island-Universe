using IslandUniverse.Audio;

namespace IslandUniverse.Plugin
{
    public class SoundManager : ISoundManager
    {
        public Sound CreateSound(string audioFilePath)
        {
            return new Sound(audioFilePath);
        }

        public SoundChannel CreateSoundChannel()
        {
            return new SoundChannel();
        }
    }
}
