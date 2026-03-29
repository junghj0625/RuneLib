namespace Rune.Audio
{
    public class AudioManager : MonoPlusSingleton<AudioManager>
    {
        public override void OnStart()
        {
            base.OnStart();

            SoundPlayer.IsSpatial = false;
        }


        public static void PlaySoundGlobally(SoundPlayData data)
        {
            SingletonInstance.SoundPlayer.PlayOneShot(data);
        }



        public static Attribute<float> masterVolume = new(0.6f);
        public static Attribute<float> musicVolume = new(0.8f);
        public static Attribute<float> soundVolume = new(1.0f);
        
        public static Attribute<bool> isMasterMuted = new(false);
        public static Attribute<bool> isMasterMutedBySystem = new(false);
        public static Attribute<bool> isMusicMuted = new(false);
        public static Attribute<bool> isMusicMutedBySystem = new(false);
        public static Attribute<bool> isSoundMuted = new(false);
        public static Attribute<bool> isSoundMutedBySystem = new(false);



        private SoundPlayer _soundPlayer = null;
        private SoundPlayer SoundPlayer { get => LazyGetComponentFromTransform(ref _soundPlayer, transform.Find("Sound Player")); }
    }
}