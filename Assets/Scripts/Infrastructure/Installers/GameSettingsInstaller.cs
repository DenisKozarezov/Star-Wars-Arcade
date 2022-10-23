using System;
using UnityEngine;
using Zenject;
using RotaryHeart.Lib.SerializableDictionary;
using Core.Models;
using Core.Models.Units;

namespace Core.Models
{
    [Serializable]
    public class GameSettings
    {
        [Min(0f)]
        public float GameTime;
        public GameObject ExplosionPrefab;
        public GameObject BulletPrefab;
        public SerializableDictionaryBase<BulletType, Sprite> Bullets;
    }
    [Serializable]
    public class PlayerSettings
    {
        public bool Immortality;
        public UnitModel PlayerModel;
    }
    [Serializable]
    public class EnemySettings
    {
        public byte EnemiesLimit;
        public EnemyModel EnemyModel;
        [Range(0f, 3f)]
        public float SpawnTime;
    }
}

namespace Core.Infrastructure
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("Game Configuration")]
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private PlayerSettings _playerSettings;
        [SerializeField]
        private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            BindAllSettings();
        }

        private void BindAllSettings()
        {
            Container.BindInstances(_gameSettings, _playerSettings, _enemySettings);
        }     
    }
}