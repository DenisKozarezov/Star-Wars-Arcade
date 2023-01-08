using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Models;

namespace Core.Units
{
    public class EnemySpawner : IInitializable, ITickable
    {
        private LinkedList<EnemyController> _enemies = new();

        private readonly IEnemyFactory _factory;
        private readonly float _spawnTime;
        private float _timer;
        private bool _enabled;
         
        public EnemySpawner(IEnemyFactory factory, GameSettings settings)
        {
            _factory = factory;
            _spawnTime = settings.EnemiesSpawnTime;
        }

        private void OnWeaponHit(IUnit target)
        {
            target.Hit();
        }
        private void OnEnemyDisposed(EnemyController enemy)
        {
            enemy.WeaponHit -= OnWeaponHit;
            enemy.Disposed -= OnEnemyDisposed;
            _factory.Despawn(enemy);
            _enemies.Remove(enemy);
        }
        private void SpawnEnemy()
        {
            EnemyController enemy = _factory.Spawn(position: Random.insideUnitCircle * 5f);
            enemy.WeaponHit += OnWeaponHit;
            enemy.Disposed += OnEnemyDisposed;
            _enemies.AddLast(enemy);
        }

        void IInitializable.Initialize()
        {
            _timer = Time.realtimeSinceStartup;
        }
        void ITickable.Tick()
        {
            if (!_enabled) return;

            foreach (EnemyController enemy in _enemies)
            {
                enemy.Update();
            }

            if (!_factory.Empty && Time.realtimeSinceStartup - _timer >= _spawnTime)
            {
                SpawnEnemy();
                _timer = Time.realtimeSinceStartup;
            }
        }

        public void Enable()
        {
            _enabled = true;
        }
        public void Disable()
        {
            _enabled = false;
        }
        public void Dispose()
        {
            while (_enemies.Count > 0) _enemies.First.Value.Dispose();
        }
    }
}