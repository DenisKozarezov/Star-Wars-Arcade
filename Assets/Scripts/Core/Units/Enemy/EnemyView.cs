using System;
using UnityEngine;
using Zenject;
using Core.Infrastructure.Signals;

namespace Core.Units
{
    public interface IEnemyFactory : IFactory<EnemyView> { }
    public interface IEnemy : IUnit
    {

    }

    public class EnemyView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable, IEnemy
    {
        private IMemoryPool _pool;
        private SignalBus _signalBus;
        private EnemyRegistry _registry;
        private Explosion.Factory _explosionFactory;

        [Inject]
        private void Construct(SignalBus signalBus, EnemyRegistry registry, Explosion.Factory explosionFactory)
        {
            _signalBus = signalBus;
            _registry = registry;
            _explosionFactory = explosionFactory;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet bullet))
            {
                Kill();
                bullet.Dispose();
            }
        }

        public void Kill()
        {
            Explosion explosion = _explosionFactory.Create();
            explosion.transform.position = transform.position;
            _signalBus.Fire<EnemyDestroyedSignal>();
            Dispose();
        }
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        void IPoolable<IMemoryPool>.OnDespawned()
        {
            _pool = null;
            _registry.Unregister(this);
        }
        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _registry.Register(this);
        }

        public class Factory : PlaceholderFactory<EnemyView>, IEnemyFactory { }
    }
}