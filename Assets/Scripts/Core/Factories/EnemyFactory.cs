using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Models;
using Core.Weapons;

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
        private readonly EnemySettings _enemySettings;
        private readonly WeaponsSettings _weaponSettings;
        private readonly Queue<EnemyController> _pool;
        public bool Empty => _pool.Count == 0;

        public EnemyFactory(
            DiContainer container, 
            EnemySettings enemySettings,
            WeaponsSettings weaponSettings)
        {
            _container = container;
            _enemySettings = enemySettings;
            _weaponSettings = weaponSettings;
            _pool = new Queue<EnemyController>(enemySettings.EnemiesLimit);

            for (int i = 0; i < enemySettings.EnemiesLimit; i++) Create();
        }
        private EnemyController Create()
        {
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_enemySettings.EnemyConfig.Prefab);
            EnemyModel model = new EnemyModel(
                _enemySettings.EnemyConfig.ReloadTime,
                _enemySettings.EnemyConfig.Velocity,
                _enemySettings.EnemyConfig.RotationSpeed,
                _enemySettings.EnemyConfig.Deacceleration,
                _enemySettings.AggressionRadius,
                _enemySettings.PatrolRadius,
                _enemySettings.EnemyConfig.Health);
            EnemyController controller = new EnemyController(model, view);

            view.SetActive(false);
            view.GetComponent<ContactCollider>().Owner = controller;

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Enemy);
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
