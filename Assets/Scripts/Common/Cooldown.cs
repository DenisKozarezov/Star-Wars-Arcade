using UnityEngine;

namespace Core
{
    public class Cooldown
    {
        private float _duration;
        private float _startTime;

        public float CurrentTime => Time.time - _startTime;
        public float RemainingTime => _duration - CurrentTime;
        public bool IsOver => CurrentTime > _duration;

        public void Run(float duration)
        {
            _duration = duration;
            _startTime = Time.time;
        }
    }
}
