using UnityEngine;
using Zenject;
using Core.Models;
using Core.Weapons;
using Core.Models.Units;
using Core.Units;

namespace Core.Player
{
    public class PlayerFactory : ITickable, IFactory<PlayerController>
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _playerSettings;
        private readonly WeaponsSettings _weaponSettings;
        private PlayerController _playerController;

        public PlayerFactory(DiContainer container, PlayerConfig playerSettings, WeaponsSettings weaponSettings)
        {
            _container = container;
            _playerSettings = playerSettings;
            _weaponSettings = weaponSettings;
        }
        public PlayerController Create()
        {
            PlayerView view = _container.InstantiatePrefabForComponent<PlayerView>(_playerSettings.Prefab);
            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _playerSettings.ReloadTime, _playerSettings.Velocity, _playerSettings.RotationSpeed, _playerSettings.Deacceleration, _playerSettings.Health });
            PlayerController controller = new PlayerController(model, view);
            
            controller.WeaponHit += OnWeaponHit;

            view.GetComponent<ContactCollider>().Owner = controller;

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Player);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });

            controller.SetPrimaryWeapon(bulletGun);
            _playerController = controller;
            return controller;
        }

        private void OnWeaponHit(IUnit target)
        {
            target.Hit();
        }

        void ITickable.Tick()
        {
            _playerController?.Update(Time.deltaTime);
        }
    }
}
