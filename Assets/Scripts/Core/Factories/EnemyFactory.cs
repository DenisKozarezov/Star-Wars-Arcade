using UnityEngine;
using Zenject;
using Core.Models;
using Core.Weapons;
using Core.Models.Units;

namespace Core.Units
{
    public interface IEnemyFactory : IFactory<Vector2, EnemyController>
    {
        
    }
    public class EnemyFactory : PlaceholderFactory<Vector2, EnemyController>, IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly EnemyConfig _enemyConfig;
        private readonly WeaponsSettings _weaponSettings;

        public EnemyFactory(DiContainer container, EnemyConfig enemySettings, WeaponsSettings weaponSettings)
        {
            _container = container;
            _enemyConfig = enemySettings;
            _weaponSettings = weaponSettings;
        }

        public override EnemyController Create(Vector2 position)
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

            controller.WeaponHit += OnWeaponHit;
            controller.Disposed += OnEnemyDisposed;

            view.SetPosition(position);
            view.GetComponent<ContactCollider>().Owner = controller;

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Player);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });

            controller.SetPrimaryWeapon(bulletGun);
            return controller;
        }
        private void OnWeaponHit(IUnit target)
        {
            target.Hit();
        }
        private void OnEnemyDisposed(EnemyController enemy)
        {
            enemy.WeaponHit -= OnWeaponHit;
            enemy.Disposed -= OnEnemyDisposed;
        }
    }
}
