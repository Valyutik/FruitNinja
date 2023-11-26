using TMPro;

namespace _Project.Scripts
{
    public sealed class Score
    {
        private readonly TMP_Text _scoreText;
        private int _score;

        public Score(TMP_Text scoreText)
        {
            _scoreText = scoreText;
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
        
        public void Restart()
        {
            SetScore(0);
        }
        
        private void SetScore(int value)
        {
            _score = value;
            SetScoreText(value);
        }
        
        private void SetScoreText(int value)
        {
            _scoreText.text = "Очки: " + value;
        }
    }
}
