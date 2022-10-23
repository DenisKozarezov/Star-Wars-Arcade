using Zenject;
using Core.Infrastructure.Signals;
using Core.Models;
using Core.UI;

namespace Core
{
    public class GameState : IInitializable, ILateDisposable
    {
        private ushort _score;
        private readonly SignalBus _signalBus;
        private readonly ScoreCounter _scoreCounter;

        public GameState(SignalBus signalBus, GameSettings settings, ScoreCounter scoreCounter)
        {
            _signalBus = signalBus;
            _scoreCounter = scoreCounter;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyDestroyedSignal>(OnEnemyDestroyed);
        }
        void ILateDisposable.LateDispose()
        {
            _signalBus.TryUnsubscribe<EnemyDestroyedSignal>(OnEnemyDestroyed);
        }
        private void OnEnemyDestroyed()
        {
            _score++;
            _scoreCounter.SetScore(_score);
        }
    }
}