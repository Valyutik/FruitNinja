using UnityEngine;

namespace _Project.Scripts
{
    public class DifficultyChanger
    {
        private readonly int _difficultyUpScoreStep;
        private readonly int _maxDifficult;
        private int _difficult = 1;
        private int _lastDifficultyUpScore;

        public DifficultyChanger(int difficultyUpScoreStep, int maxDifficult)
        {
            _difficultyUpScoreStep = difficultyUpScoreStep;
            _maxDifficult = maxDifficult;
        }
        
        public float CalculateRandomSpawnDelay(float minDelay, float maxDelay)
        {
            var randomDelay = Random.Range(minDelay, maxDelay);
            var difficultyCoefficient = (float)(_maxDifficult - _difficult) / _maxDifficult;
            var delayDelta = randomDelay - minDelay;

            return minDelay + delayDelta * difficultyCoefficient;
        }
        
        public float CalculateBombChance(float minChance, float maxChance)
        {
            var difficultyCoefficient = (float) _difficult / _maxDifficult;
            var chanceDelta = maxChance - minChance;
            return minChance + chanceDelta * difficultyCoefficient;
        }
        
        public void Restart()
        {
            _difficult = 1;
            _lastDifficultyUpScore = 0;
        }
        
        public void SetDifficultByScore(int score)
        {
            if (score > _lastDifficultyUpScore + _difficultyUpScoreStep && _difficult < _maxDifficult)
            {
                _lastDifficultyUpScore += _difficultyUpScoreStep;
                _difficult++;
            }
        }
    }
}