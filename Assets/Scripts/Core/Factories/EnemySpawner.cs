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
            if (_enemies.Count > _enemiesLimit) return;

            if (Time.realtimeSinceStartup - _timer >= _spawnTime)
            {
                EnemyController enemy = _factory.Create(position : Random.insideUnitCircle * 5f);
                enemy.WeaponHit += OnEnemyHit;
                enemy.Disposed += OnEnemyDisposed;
                _enemies.AddLast(enemy);
                _timer = Time.realtimeSinceStartup;
            }

            foreach (EnemyController enemy in _enemies)
            {
                enemy.Update();
            }
        }

        private void OnEnemyHit(IUnit target)
        {
  
        }
        private void OnEnemyDisposed(EnemyController enemy)
        {
            _enemies.Remove(enemy);
            enemy.WeaponHit -= OnEnemyHit;
            enemy.Disposed -= OnEnemyDisposed;
        }
    }
}