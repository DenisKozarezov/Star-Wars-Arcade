using UnityEngine;
using Zenject;
using Core.Models;
using Core.Units;

namespace Core
{
    public class EnemySpawner : IInitializable, ITickable
    {
        private readonly IEnemyFactory _factory;
        private readonly EnemyRegistry _registry;
        private readonly byte _enemiesLimit;
        private readonly float _spawnTime;
        private float _timer;

        public EnemySpawner(IEnemyFactory factory, EnemySettings settings, EnemyRegistry registry)
        {
            _factory = factory;
            _registry = registry;
            _enemiesLimit = settings.EnemiesLimit;
            _spawnTime = settings.SpawnTime;
        }
        void IInitializable.Initialize()
        {
            _timer = Time.realtimeSinceStartup;
        }
        void ITickable.Tick()
        {
            if (_registry.Count > _enemiesLimit) return;

            if (Time.realtimeSinceStartup - _timer >= _spawnTime)
            {
                EnemyView enemy = _factory.Create();
                enemy.transform.position = Random.insideUnitCircle * 5f;
                _timer = Time.realtimeSinceStartup;
            }
        }
    }
}