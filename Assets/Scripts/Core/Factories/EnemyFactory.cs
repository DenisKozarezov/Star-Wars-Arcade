using UnityEngine;
using Zenject;
using Core.Models;
using Core.Weapons;
using Core.Models.Units;
using Core.Units;

namespace Core
{
    public interface IEnemyFactory
    {
        public EnemyController Create(Vector2 position);
    }
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly EnemyView.Factory _enemyPool;
        private readonly EnemyConfig _enemyConfig;
        private readonly WeaponsSettings _weaponSettings;

        public EnemyFactory(DiContainer container, EnemyView.Factory enemyPool, EnemyConfig enemySettings, WeaponsSettings weaponSettings)
        {
            _container = container;
            _enemyPool = enemyPool;
            _enemyConfig = enemySettings;
            _weaponSettings = weaponSettings;
        }
        public EnemyController Create(Vector2 position)
        {
            EnemyView view = _enemyPool.Create();
            EnemyModel model = new EnemyModel(_enemyConfig.ReloadTime, _enemyConfig.Velocity, _enemyConfig.RotationSpeed, _enemyConfig.Deacceleration);
            EnemyController controller = new EnemyController(model, view);
            controller.Reset();
            controller.WeaponHit += OnWeaponHit;

            view.SetPosition(position);

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Player);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });

            controller.SetPrimaryWeapon(bulletGun);
            return controller;
        }

        private void OnWeaponHit(IUnit target)
        {
            target.Hit();
        }
    }
}
