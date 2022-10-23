using System;
using UnityEngine;
using Zenject;
using Core.Models;

namespace Core
{
    public enum BulletType : byte
    {
        Player = 0x00,
        Enemy = 0x01
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public class Bullet : MonoBehaviour, IPoolable<float, BulletType, IMemoryPool>, IDisposable
    {
        [SerializeField]
        private AudioClip[] _sounds;

        private float _velocity;
        private SpriteRenderer _renderer;
        private AudioSource _audioSource;
        private GameSettings _settings;
        private IMemoryPool _pool;

        [Inject]
        private void Construct(GameSettings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (_velocity == 0f) return;

            transform.position += transform.up * _velocity * Time.deltaTime;
        }
        public void Dispose()
        {
            _pool?.Despawn(this);
        }
        void IPoolable<float, BulletType, IMemoryPool>.OnDespawned()
        {
            _velocity = 0f;
            _pool = null;
        }
        void IPoolable<float, BulletType, IMemoryPool>.OnSpawned(float velocity, BulletType type, IMemoryPool pool)
        {
            if (_sounds.Length > 0)
            {
                _audioSource.PlayOneShot(_sounds[UnityEngine.Random.Range(0, _sounds.Length - 1)]);
            }

            if (_settings.Bullets.TryGetValue(type, out Sprite sprite))
            {
                _renderer.sprite = sprite;
            }
            _velocity = velocity;
            gameObject.layer = type == BulletType.Player ? Constants.PlayerLayer : Constants.EnemyLayer;
            _pool = pool;
        }    

        public class Factory : PlaceholderFactory<float, BulletType, Bullet> { }
    }
}