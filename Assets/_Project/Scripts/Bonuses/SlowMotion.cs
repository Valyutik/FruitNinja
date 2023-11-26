using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bonuses
{
    public class SlowMotion : ITickable
    {
        private readonly float _normalTimeScale;
        private readonly float _slowMotionTimeScale;
        private float _duration;
        private float _timer;

        public SlowMotion(float normalTimeScale, float slowMotionTimeScale)
        {
            _normalTimeScale = normalTimeScale;
            _slowMotionTimeScale = slowMotionTimeScale;
        }

        public void Tick()
        {
            MoveTimer();
        }
        
        public void StartSlow(float duration)
        {
            _duration = duration;
            _timer = 0;
            SetTimeScale(_slowMotionTimeScale);
        }
        
        public void Restart()
        {
            _duration = 0;
        }

        private void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }
        
        private void MoveTimer()
        {
            if (_timer >= _duration)
            {
                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= _duration)
            {
                SetTimeScale(_normalTimeScale);
            }
        }
    }
}