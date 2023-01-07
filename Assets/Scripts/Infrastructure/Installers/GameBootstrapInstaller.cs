using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using Core.UI;
using Core.Weapons;
using Core.Models.Units;
using Core.Player;

namespace Core.Infrastructure
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private ScoreCounter _scoreCounter;
        [Inject]
        private GameSettings _gameSettings;
        [Inject]
        private EnemyConfig _enemySettings;
        [Inject]
        private WeaponsSettings _weaponsSettings;
        [SerializeField]
        private GameObject _explosionPrefab;

        public override void InstallBindings()
        {
            BindPools();
            BindPlayer();
            BindUI();
            BindEnemy();
            BindGame();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<StandaloneInputController>().AsSingle();
        }
        private void BindUI()
        {
            Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
        }
        private void BindEnemy()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindFactoryCustomInterface<Vector2, EnemyController, EnemyFactory, IEnemyFactory>();
        }
        private void BindGame()
        {
            Container.Bind<IInitializable>().To<Level>().AsSingle();
        }
        private void BindPools()
        {
            Container.BindFactory<Vector2, EnemyController, EnemyFactory>().AsCached();

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