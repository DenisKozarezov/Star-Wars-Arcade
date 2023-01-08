using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        private DisposableAudioClip.Factory _factory;
        private AudioSource _audioSource;
        private AudioSound _music;

        [Inject]
        private void Construct(DisposableAudioClip.Factory audioFactory, AudioSettings settings)
        {
            _factory = audioFactory;
            _music = settings.GameSounds.GameBackground;
        }

        private void Awake()
        {
            if (_instance == null) _instance = this;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
        private void OnClipPlayed(DisposableAudioClip clip)
        {
            clip.Dispose();
            clip.ClipPlayed -= OnClipPlayed;
        }

        private void PlayOneShotInternal(AudioClip clip, float volume = 1f, bool pausable = true)
        {
            if (clip == null) throw new ArgumentNullException("Audio Clip is missing!");

            DisposableAudioClip oneShotClip = _factory.Create
            (
                clip, 
                volume,
                pausable
            );
            oneShotClip.ClipPlayed += OnClipPlayed;
        }
        private void PlayMusicInternal(AudioClip clip, float volume = 1f)
        {
            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.ignoreListenerPause = true;
            _audioSource.Play();
            _audioSource.DOFade(volume, duration: 1f);
        }
        private void StopMusicInternal()
        {
            _audioSource.DOFade(0, duration: 1f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _audioSource.Stop();
                    _audioSource.clip = null;
                });
        }
        public static void PlayOneShot(AudioClip clip, float volume = 1f, bool pausable = true)
        {
            _instance.PlayOneShotInternal(clip, volume, pausable);
        }
        public static void PlayMusic()
        {
            _instance.PlayMusicInternal(_instance._music.Clip, _instance._music.Volume);
        }
        public static void StopMusic()
        {
            _instance.StopMusicInternal();
        }
    }
}