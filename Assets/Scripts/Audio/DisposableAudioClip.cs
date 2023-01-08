using System;
using UnityEngine;
using Zenject;

namespace Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class DisposableAudioClip : MonoBehaviour, IPoolable<AudioClip, float, bool, IMemoryPool>, IDisposable
    {
        private AudioSource _source;
        private IMemoryPool _pool;

        public event Action<DisposableAudioClip> ClipPlayed;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void OnClipPlayed()
        {
            ClipPlayed?.Invoke(this);
        }

        public void PlaySound(AudioClip clip, float volume, bool pausable)
        {
            _source.clip = clip;
            _source.volume = volume;
            _source.ignoreListenerPause = !pausable;
            _source.Play();
            Invoke(nameof(OnClipPlayed), _source.clip.length);
        }
        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        void IPoolable<AudioClip, float, bool, IMemoryPool>.OnDespawned()
        {
            _pool = null;
            _source.clip = null;
        }
        void IPoolable<AudioClip, float, bool, IMemoryPool>.OnSpawned(AudioClip clip, float volume, bool pausable, IMemoryPool pool)
        {
            _pool = pool;

            if (clip == null) throw new ArgumentNullException("Can't play an empty audio clip!");

            PlaySound(clip, volume, pausable);
        }

        public class Factory : PlaceholderFactory<AudioClip, float, bool, DisposableAudioClip> { }
    }
}
