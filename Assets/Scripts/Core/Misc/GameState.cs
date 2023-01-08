using Core.UI;

namespace Core
{
    public sealed class GameState
    {
        private readonly ScoreCounter _scoreCounter;

        private ushort Score;

        public GameState(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void AddScore()
        {
            Score++;
            _scoreCounter.SetScore(Score);
        }
        public void Reset()
        {
            Score = 0;
            _scoreCounter.SetScore(0);
        }
    }
}
