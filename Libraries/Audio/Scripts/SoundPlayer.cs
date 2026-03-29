using System.Collections.Generic;
using UnityEngine;



namespace Rune.Audio
{
    public partial class SoundPlayer : MonoPlus
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _isSpatial.OnChange.AddListener(OnChangeIsSpatial);
            _is2D.OnChange.AddListener(OnChangeIs2D);
            _loop.OnChange.AddListener(OnChangeLoop);
        }

        public override void InitObjects()
        {
            base.InitObjects();

            Template.SetActive(false);


            // Audio sources (Minus pitch)
            for (int k = _pitchRange; k >= 1; k--)
            {
                var minus = Utils.Object.InstantiateTemplate(Template).GetComponent<AudioSource>();

                _audioSources.Add(minus);

                minus.name = "Minus " + k;
                minus.pitch = 1.0f - _pitchStap * k;
                minus.gameObject.SetActive(true);
            }


            // Audio sources
            var center = Utils.Object.InstantiateTemplate(Template).GetComponent<AudioSource>();

            _audioSources.Add(center);

            center.name = "Center";
            center.pitch = 1;
            center.gameObject.SetActive(true);


            // Audio sources (Plus pitch)
            for (int k = 1; k <= _pitchRange; k++)
            {
                var plus = Utils.Object.InstantiateTemplate(Template).GetComponent<AudioSource>();

                _audioSources.Add(plus);

                plus.name = "Plus " + k;
                plus.pitch = 1.0f + _pitchStap * k;
                plus.gameObject.SetActive(true);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            _isSpatial.Refresh();
            _loop.Refresh();
        }


        public void Play(SoundPlayData data, float volumeScale = 1.0f)
        {
            if (data == null || !Pools.SoundPool.TryGet(data.Name, out var sound)) return;


            if (IsSpatial && Is2D)
            {
                transform.position = Utils.Math.WithZ(transform.position, 0);
            }


            int index = Mathf.Clamp(Random.Range(data.MinPitch, data.MaxPitch), -_pitchRange, _pitchRange) + _pitchRange;

            var audioSource = _audioSources[index];


            audioSource.clip = sound;
            audioSource.volume = GetVolume(data, volumeScale);
            audioSource.Play();
        }

        public void PlayOneShot(SoundPlayData data, float volumeScale = 1.0f)
        {
            if (data == null || !Pools.SoundPool.TryGet(data.Name, out var sound)) return;


            if (IsSpatial && Is2D)
            {
                transform.position = Utils.Math.WithZ(transform.position, 0);
            }


            int index = Mathf.Clamp(Random.Range(data.MinPitch, data.MaxPitch), -_pitchRange, _pitchRange) + _pitchRange;

            var audioSource = _audioSources[index];


            audioSource.PlayOneShot(sound, GetVolume(data, volumeScale));
        }

        public void StopAll()
        {
            foreach (var audioSource in _audioSources) audioSource.Stop();
        }



        private float GetVolume(SoundPlayData data, float volumeScale = 1.0f)
        {
            if (data == null) return 1.0f;
            
            return
                data.Volume *
                volumeScale *
                AudioManager.masterVolume.Value *
                AudioManager.soundVolume.Value *
                (AudioManager.isMasterMuted.Value ? 0 : 1) *
                (AudioManager.isMasterMutedBySystem.Value ? 0 : 1) *
                (AudioManager.isSoundMuted.Value ? 0 : 1) *
                (AudioManager.isSoundMutedBySystem.Value ? 0 : 1);
        }

        private void OnChangeIsSpatial(bool value)
        {
            foreach (var audioSource in _audioSources) audioSource.spatialBlend = value ? 1 : 0;
        }

        private void OnChangeIs2D(bool value)
        {
            /* Noop */
        }

        private void OnChangeLoop(bool value)
        {
            foreach (var audioSource in _audioSources) audioSource.loop = value;
        }



        private GameObject _template = null;
        private GameObject Template { get => LazyGetGameObjectFromPath(ref _template, "Template"); }



        private readonly Attribute<bool> _isSpatial = new(false);
        public bool IsSpatial { get => _isSpatial.Value; set => _isSpatial.Value = value; }

        private readonly Attribute<bool> _is2D = new(false);
        public bool Is2D { get => _is2D.Value; set => _is2D.Value = value; }

        private readonly Attribute<bool> _loop = new(false);
        public bool Loop { get => _loop.Value; set => _loop.Value = value; }



        private readonly List<AudioSource> _audioSources = new();

        private readonly int _pitchRange = 3;

        private readonly float _pitchStap = 0.04f;
    }



    public class SoundPlayData
    {
        public string Name { get; set; } = string.Empty;

        public float Volume { get; set; } = 1.0f;

        public int MaxPitch { get; set; } = -2;
        public int MinPitch { get; set; } = 2;
    }
}