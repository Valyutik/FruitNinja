using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public sealed class Score
    {
        private const string BestScoreKey = "BestScore";
        private readonly TMP_Text _scoreText;
        private readonly DifficultyChanger _difficultyChanger;
        private int _score;
        private int _bestScore;
        private bool _isNewBestScore;

        public Score(DifficultyChanger difficultyChanger,TMP_Text scoreText)
        {
            _difficultyChanger = difficultyChanger;
            _scoreText = scoreText;
            LoadBestScore();
            SetScore(0);
        }
        
        public void AddScore(int value)
        {
            SetScore(_score + value);
        }
        
        public int GetScore()
        {
            return _score;
        }
        
        public void SetBestScore(int value)
        {
            _bestScore = value;
            SaveBestScore(value);
        }
        
        public int GetBestScore()
        {
            return _bestScore;
        }
        
        public void Restart()
        {
            SetScore(0);
        }
        
        private void SetScore(int value)
        {
            _score = value;
            _difficultyChanger.SetDifficultByScore(value);
            SetScoreText(value);
        }
        
        private void SetScoreText(int value)
        {
            _scoreText.text = "Очки: " + value;
        }
        
        private void LoadBestScore()
        {
            _bestScore = PlayerPrefs.GetInt(BestScoreKey);
        }
        
        private void SaveBestScore(int value)
        {
            PlayerPrefs.SetInt(BestScoreKey, value);
        }
    }
}
