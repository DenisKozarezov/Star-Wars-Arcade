using System;
using UnityEngine;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private float _lifetime;
        private float _timer;
        private bool _disposed;

        private void Awake()
        {
            _lifetime = GetComponent<ParticleSystem>().main.duration;
        }
        private void Update()
        {
            if (_disposed) return;

            if (_timer <= 0.0f) Dispose();
            else _timer -= Time.deltaTime;
        }
        public void Dispose()
        {
            _disposed = true;
            _pool.Despawn(this);
        }
        void IPoolable<IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }
        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _timer = _lifetime;
            _disposed = false;
            _pool = pool;
        }

        public class Factory : PlaceholderFactory<Explosion> { }
    }
}