using Zenject;
using Core.Models;
using Core.Player;
using Core.Weapons;
using Core.Models.Units;
using Core.Units;

namespace Core
{
    public class PlayerFactory : IFactory<PlayerController>
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _playerSettings;
        private readonly WeaponsSettings _weaponSettings;

        public PlayerFactory(DiContainer container, PlayerConfig playerSettings, WeaponsSettings weaponSettings)
        {
            _container = container;
            _playerSettings = playerSettings;
            _weaponSettings = weaponSettings;
        }
        public PlayerController Create()
        {
            PlayerView view = _container.InstantiateComponent<PlayerView>(_playerSettings.Prefab);
            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _playerSettings.ReloadTime, _playerSettings.Velocity });

            PlayerController playerController = new PlayerController(model, view);
            playerController.WeaponHit += OnWeaponHit;

            BulletGunModel bulletGunModel = new BulletGunModel(model.ReloadTime, _weaponSettings.BulletGunConfig, view.FirePoint, BulletType.Player);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });

            playerController.SetPrimaryWeapon(bulletGun);
            return playerController;
        }

        private void OnWeaponHit(IUnit target)
        {
            
        }
    }
}
