using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Models;
using Core.Player;

namespace Core.Units
{
    public class EnemySpawner : ITickable
    {
        private LinkedList<EnemyController> _enemies = new();

        private readonly IEnemyFactory _factory;
        private readonly float _spawnTime;
        private readonly float _aggressionRadius;
        private readonly LazyInject<PlayerController> _player;
        private float _timer;
        private bool _enabled;

        public event Action EnemyKilled;
         
        public EnemySpawner(IEnemyFactory factory, EnemySettings enemySettings, LazyInject<PlayerController> player)
        {
            _factory = factory;
            _spawnTime = enemySettings.EnemiesSpawnTime;
            _player = player;
            _aggressionRadius = enemySettings.AggressionRadius;
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
            EnemyKilled?.Invoke();
        }
        private void SpawnEnemy()
        {
            EnemyController enemy = _factory.Spawn(position: UnityEngine.Random.insideUnitCircle * 5f);
            enemy.WeaponHit += OnWeaponHit;
            enemy.Disposed += OnEnemyDisposed;
            _enemies.AddLast(enemy);
        }
        private bool HasNearbyEnemy(ITransformable patrollingEnemy, float distance)
        {
            return (patrollingEnemy.Position - _player.Value.Transformable.Position).sqrMagnitude <= distance * distance;
        }

        void ITickable.Tick()
        {
            if (!_enabled) return;

            foreach (EnemyController enemy in _enemies)
            {
                enemy.Update();

                if (!enemy.IsTaunted && HasNearbyEnemy(enemy.Transformable, _aggressionRadius))
                {
                    enemy.Taunt(_player.Value);
                }
            }

            if (!_factory.Empty && _timer >= _spawnTime)
            {
                SpawnEnemy();
                _timer = 0f;
            }
            else _timer += Time.deltaTime;
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