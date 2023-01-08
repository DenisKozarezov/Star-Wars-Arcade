﻿using Core.Player;
using Core.Units;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Level : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerController _player;

        public Level(PlayerFactory playerFactory, EnemySpawner enemySpawner)
        {
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;

            _player = _playerFactory.Create();
            _player.Died += OnPlayerDead;
            _player.WeaponHit += OnPlayerKilledEnemy;
        }

        private void StartGame()
        {
            _enemySpawner.Enable();
        }
        private void OnPlayerDead()
        {
            _player.Disable();
            _player.Died -= OnPlayerDead;

            _enemySpawner.Disable();
            _enemySpawner.Dispose();

            Logger.Debug("Player died!");
        }
        private void OnPlayerKilledEnemy(IUnit unit)
        {
            Logger.Debug("Enemy killed!");
        }

        void IInitializable.Initialize()
        {
            StartGame();
        }
    }
}
