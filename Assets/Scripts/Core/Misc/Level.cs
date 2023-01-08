﻿using Core.Audio;
using Core.Player;
using Core.Units;
using Zenject;

namespace Core
{
    public class Level : IInitializable, ILateDisposable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemySpawner _enemySpawner;
        private readonly GameState _gameState;
        private readonly PlayerController _player;

        public Level(PlayerFactory playerFactory, EnemySpawner enemySpawner, GameState gameState)
        {
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
            _gameState = gameState;
            _player = _playerFactory.Create();
        }

        private void StartGame()
        {
            _enemySpawner.Enable();

            _playerFactory.EnemyKilled += _gameState.AddScore;
            _player.Died += OnPlayerDead;

            SoundManager.PlayMusic();
        }
        private void OnPlayerDead()
        {
            _player.Disable();
            _enemySpawner.Disable();
            _enemySpawner.Dispose();

            SoundManager.StopMusic();

            Logger.Debug("Player <b><color=yellow>died</color></b>!");
        }

        void IInitializable.Initialize()
        {
            StartGame();
        }
        void ILateDisposable.LateDispose()
        {
            _playerFactory.EnemyKilled -= _gameState.AddScore;
            _player.Died -= OnPlayerDead;
        }
    }
}
