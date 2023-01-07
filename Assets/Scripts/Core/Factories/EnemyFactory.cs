using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Models;
using Core.Weapons;
using Core.Models.Units;

namespace Core.Units
{
    public interface IEnemyFactory
    {
        bool Empty { get; }
        EnemyController Spawn(Vector2 position);
        void Despawn(EnemyController enemy);
    }
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly EnemyConfig _enemyConfig;
        private readonly WeaponsSettings _weaponSettings;
        private readonly Queue<EnemyController> _pool;

        public bool Empty => _pool.Count == 0;

        public EnemyFactory(DiContainer container, EnemyConfig enemySettings, WeaponsSettings weaponSettings, GameSettings gameSettings)
        {
            _container = container;
            _enemyConfig = enemySettings;
            _weaponSettings = weaponSettings;
            _pool = new Queue<EnemyController>(gameSettings.EnemiesLimit);
            Init(gameSettings.EnemiesLimit);
        }
        private void Init(int capacity)
        {
            for (int i = 0; i < capacity; i++) Create();
        }
        private EnemyController Create()
        {
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_enemyConfig.Prefab);
            EnemyModel model = new EnemyModel(
                _enemyConfig.ReloadTime,
                _enemyConfig.Velocity,
                _enemyConfig.RotationSpeed,
                _enemyConfig.Deacceleration,
                _enemyConfig.AggressionRadius,
                _enemyConfig.PatrolRadius,
                _enemyConfig.Health);
            EnemyController controller = new EnemyController(model, view);

            view.SetActive(false);
            view.GetComponent<ContactCollider>().Owner = controller;

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Player);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });

            controller.SetPrimaryWeapon(bulletGun);
            _pool.Enqueue(controller);
            return controller;
        }
        public EnemyController Spawn(Vector2 position)
        {
            if (_pool.TryDequeue(out EnemyController enemy))
            {
                (enemy as IPoolable<Vector2>).OnSpawned(position);
                return enemy;
            }
            else 
                return Create();
        }
        public void Despawn(EnemyController enemy)
        {
            (enemy as IPoolable<Vector2>).OnDespawned();
            _pool.Enqueue(enemy);
        }
    }
}
