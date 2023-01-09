using UnityEngine;
using Zenject;
using Core.UI;

namespace Core
{
    public sealed class GameState : ITickable
    {
        private readonly ScoreCounter _scoreCounter;
        private readonly GameTimer _gameTimer;

        private bool _enabled;
        private ushort _score;
        private float _time;

        public GameState(ScoreCounter scoreCounter, GameTimer gameTimer)
        {
            _scoreCounter = scoreCounter;
            _gameTimer = gameTimer;
        }

        public void AddScore()
        {
            _score++;
            _scoreCounter.SetScore(_score);
        }
        public void StartTimer() => _enabled = true;
        public void StopTimer() => _enabled = false;

        void ITickable.Tick()
        {
            if (!_enabled) return;

            _time += Time.deltaTime;
            _gameTimer.SetTime(_time);
        }
    }
}
