using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using Core.UI;
using Core.Weapons;
using Core.Models.Units;

namespace Core.Infrastructure
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _spawnPoint;
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
            BindInput();
            BindPlayer();
            BindUI();
            BindEnemy();
            BindPools();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<StandaloneInputController>().AsSingle();
        }
        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
        }
        private void BindUI()
        {
            Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
        }
        private void BindEnemy()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();       
        }
        private void BindPools()
        {
            Container.BindFactory<EnemyView, EnemyView.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(_gameSettings.EnemiesLimit)
                .FromComponentInNewPrefab(_enemySettings.Prefab)
                .UnderTransformGroup("Enemies"));

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