using UnityEngine;
using Zenject;
using Core.UI;

namespace Core
{
    public sealed class GameState : ITickable
    {
        private readonly ScoreCounter _scoreCounter;
        private readonly GameTimer _gameTimer;

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
        public void Reset()
        {
            _score = 0;
            _time = 0f;
            _scoreCounter.SetScore(0);
            _gameTimer.SetTime(0f);
        }

        void ITickable.Tick()
        {
            _time += Time.deltaTime;
            _gameTimer.SetTime(_time);
        }
    }
}
