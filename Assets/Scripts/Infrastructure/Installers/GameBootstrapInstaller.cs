using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;
using Core.Units;
using Core.UI;

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
        private EnemySettings _enemySettings;
        [Inject]
        private PlayerSettings _playerSettings;

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
            Container.BindInterfacesAndSelfTo<PlayerBulletFactory>().AsSingle();
            Container.Bind<PlayerView>()
                .FromComponentInNewPrefabResource(_playerSettings.PlayerModel.PrefabPath)
                .AsSingle()
                .OnInstantiated<PlayerView>((ctx, obj) => obj.transform.position = _spawnPoint.position)
                .NonLazy();
        }
        private void BindUI()
        {
            Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
        }
        private void BindEnemy()
        {
            Container.Bind<EnemyBulletFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRegistry>().AsSingle();       
        }
        private void BindPools()
        {
            Container.BindFactoryCustomInterface<EnemyView, EnemyView.Factory, IEnemyFactory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(_enemySettings.EnemiesLimit)     
                .FromSubContainerResolve()
                .ByNewPrefabResourceInstaller<EnemyInstaller>(_enemySettings.EnemyModel.PrefabPath)
                .UnderTransformGroup("Enemies"));

            Container.BindFactory<float, BulletType, Bullet, Bullet.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_gameSettings.BulletPrefab)
                .UnderTransformGroup("Bullets"));

            Container.BindFactory<Explosion, Explosion.Factory>().FromMonoPoolableMemoryPool(x => x
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_gameSettings.ExplosionPrefab)
                .UnderTransformGroup("VFX"));
        }
    }
}