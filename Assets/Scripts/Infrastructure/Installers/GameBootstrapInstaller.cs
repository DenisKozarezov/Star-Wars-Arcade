using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using Core.UI;
using Core.Weapons;
using Core.Player;

namespace Core.Infrastructure.Installers
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [Inject]
        private WeaponsSettings _weaponsSettings;
        [SerializeField]
        private GameObject _explosionPrefab;

        public override void InstallBindings()
        {
            BindPools();
            BindPlayer();
            BindEnemy();
            BindGame();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<StandaloneInputController>().AsSingle();
        }
        private void BindEnemy()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        }
        private void BindGame()
        {
            Container.Bind<IInitializable>().To<Level>().AsSingle();
        }
        private void BindPools()
        {
            Container.BindFactory<Vector2, Quaternion, float, float, BulletType, Bullet, Bullet.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_weaponsSettings.BulletGunConfig.Prefab)
                .UnderTransformGroup("Bullets"));

            Container.BindFactory<Explosion, Explosion.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_explosionPrefab)
                .UnderTransformGroup("VFX"));
        }
    }
}