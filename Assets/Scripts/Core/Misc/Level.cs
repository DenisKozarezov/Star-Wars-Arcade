using Core.Audio;
using Core.Player;
using Core.Units;
using Zenject;

namespace Core
{
    public class Level : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerController _player;
        private readonly AudioSound _music;

        public Level(PlayerFactory playerFactory, EnemySpawner enemySpawner, AudioSettings audio)
        {
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
            _music = audio.GameSounds.GameBackground;

            _player = _playerFactory.Create();
            _player.Died += OnPlayerDead;
            _player.WeaponHit += OnPlayerKilledEnemy;
        }

        private void StartGame()
        {
            _enemySpawner.Enable();
            _player.Enable();
            SoundManager.PlayMusic(_music.Clip, _music.Volume);
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
