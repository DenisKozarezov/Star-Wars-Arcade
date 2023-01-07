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

        private EnemyController CreateEnemy()
        {
            EnemyController enemy = _factory.Create(Random.insideUnitCircle * 5f);
            _enemies.AddLast(enemy);
            return enemy;
        }

        void IInitializable.Initialize()
        {
            _timer = Time.realtimeSinceStartup;

            for (int i = 0; i < _enemiesLimit; i++) CreateEnemy();
        }
        void ITickable.Tick()
        {
            if (!_enabled) return;

            foreach (EnemyController enemy in _enemies)
            {
                enemy.Update();
            }

            if (_enemies.Count > _enemiesLimit) return;

            //if (Time.realtimeSinceStartup - _timer >= _spawnTime)
            //{
            //    SpawnEnemy();
            //    _timer = Time.realtimeSinceStartup;
            //}
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
                _enemies.Remove(enemy);
                enemy.Dispose();
            }
        }
    }
}