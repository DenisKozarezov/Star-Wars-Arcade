using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using Core.Weapons;
using Core.Player;
using Core.UI;

namespace Core.Infrastructure.Installers
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private ScoreCounter _scoreCounter;
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
            Container.Bind<GameState>().AsSingle();
            Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
        }
        private void BindPools()
        {
            Container.BindFactory<Vector2, Quaternion, float, float, BulletType, Bullet, Bullet.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_weaponsSettings.BulletGunConfig.Prefab)
                .UnderTransformGroup("Bullets"));

            Container.BindFactory<Explosion, Explosion.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_explosionPrefab)
                .UnderTransformGroup("VFX"));
        }
    }
}