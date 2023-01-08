using System;
using UnityEngine;
using Zenject;
using Core.Models;
using Core.Models.Units;

namespace Core.Models
{
    [Serializable]
    public sealed class GameSettings
    {
        [Min(0f)]
        public float GameTime;
        public byte EnemiesLimit;
        [Min(0f)]
        public float EnemiesSpawnTime;
    }

    [Serializable]
    public sealed class WeaponsSettings
    {
        public BulletGunConfig BulletGunConfig;
    }
}

namespace Core.Infrastructure.Installers
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("Game Configuration")]
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private PlayerConfig _playerSettings;
        [SerializeField]
        private EnemyConfig _enemySettings;
        [SerializeField]
        private WeaponsSettings _weaponsSettings;

        public override void InstallBindings()
        {
            BindAllSettings();
        }

        private void BindAllSettings()
        {
            Container.BindInstance(_gameSettings).AsSingle().IfNotBound();
            Container.BindInstance(_playerSettings).AsSingle().IfNotBound();
            Container.BindInstance(_enemySettings).AsSingle().IfNotBound();
            Container.BindInstance(_weaponsSettings).AsSingle().IfNotBound();
        }     
    }
}