using Core.Player;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Level : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemySpawner _enemySpawner;
        private PlayerController _player;

        public Level(PlayerFactory playerFactory, EnemySpawner enemySpawner)
        {
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
        }

        private void StartGame()
        {
            _player = _playerFactory.Create();
            _player.Enable();
            _player.Died += OnPlayerDead;

            _enemySpawner.Enable();
        }
        private void OnPlayerDead()
        {
            _player.Disable();
            _enemySpawner.Disable();
            _enemySpawner.Dispose();

#if UNITY_EDITOR
            Debug.Log("Player died!");
#endif
        }

        void IInitializable.Initialize()
        {
            StartGame();
        }
    }
}
