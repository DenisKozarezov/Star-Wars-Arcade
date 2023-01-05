using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Units;
using Core.Models;

namespace Core
{
    public class EnemySpawner : IInitializable, ITickable
    {
        private LinkedList<EnemyController> _enemies = new();

        private readonly IEnemyFactory _factory;
        private readonly byte _enemiesLimit;
        private readonly float _spawnTime;
        private float _timer;
        private bool _enabled;
         
        public EnemySpawner(IEnemyFactory factory, GameSettings settings)
        {
            _factory = factory;
            _enemiesLimit = settings.EnemiesLimit;
            _spawnTime = settings.EnemiesSpawnTime;
        }
        void IInitializable.Initialize()
        {
            _timer = Time.realtimeSinceStartup;
        }
        void ITickable.Tick()
        {
            foreach (EnemyController enemy in _enemies)
            {
                enemy.Update();
            }

            if (!_enabled || _enemies.Count > _enemiesLimit) return;

            if (Time.realtimeSinceStartup - _timer >= _spawnTime)
            {
                EnemyController enemy = _factory.Create(position: Random.insideUnitCircle * 5f);
                enemy.Disposed += OnEnemyDisposed;
                _enemies.AddLast(enemy);
                _timer = Time.realtimeSinceStartup;
            }
        }

        private void OnEnemyDisposed(EnemyController enemy)
        {
            _enemies.Remove(enemy);
            enemy.Disposed -= OnEnemyDisposed;
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
            foreach (EnemyController enemy in _enemies)
            {
                enemy.Dispose();
            }
        }
    }
}