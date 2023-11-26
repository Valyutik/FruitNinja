using _Project.Scripts.Fruits;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public sealed class GameEnder
    {
        private readonly Score _score;
        private readonly Health _health;
        private readonly FruitSpawner _fruitSpawner;
        private readonly DifficultyChanger _difficultyChanger;
        private readonly GameObject _gameScreen;
        private readonly GameObject _gameEndScreen;
        private readonly TMP_Text _gameEndScoreText;
        private readonly TMP_Text _bestScoreText;

        public GameEnder(Score score, Health health, FruitSpawner fruitSpawner, DifficultyChanger difficultyChanger, GameObject gameScreen,
            GameObject gameEndScreen, TMP_Text gameEndScoreText, TMP_Text bestScoreText)
        {
            _score = score;
            _health = health;
            _fruitSpawner = fruitSpawner;
            _difficultyChanger = difficultyChanger;
            _gameScreen = gameScreen;
            _gameEndScreen = gameEndScreen;
            _gameEndScoreText = gameEndScoreText;
            _bestScoreText = bestScoreText;
            SwitchScreens(true);
        }
        
        public void EndGame()
        {
            _fruitSpawner.Stop();
            SwitchScreens(false);
            RefreshScores();
        }
        
        public void RestartGame()
        {
            _score.Restart();

            _health.Restart();

            _fruitSpawner.Restart();

            SwitchScreens(true);
            _difficultyChanger.Restart();
        }
        
        private void SwitchScreens(bool isGame)
        {
            _gameScreen.SetActive(isGame);
            _gameEndScreen.SetActive(!isGame);
        }
        
        private void RefreshScores()
        {
            var score = _score.GetScore();
            var oldBestScore = _score.GetBestScore();
            bool isNewBestScore = CheckNewBestScore(score, oldBestScore);
            SetActiveGameEndScoreText(!isNewBestScore);

            if (isNewBestScore)
            {
                _score.SetBestScore(score);
                SetNewBestScoreText(score);
            }
            else
            {
                SetGameEndScoreText(score);
                SetOldBestScoreText(oldBestScore);
            }
        }
        
        private bool CheckNewBestScore(int score, int oldBestScore)
        {
            return score > oldBestScore;
        }
        
        private void SetOldBestScoreText(int value)
        {
            _bestScoreText.text = $"Лучший результат: {value}";
        }

        private void SetNewBestScoreText(int value)
        {
            _bestScoreText.text = $"Новый рекорд: {value}!";
        }

        private void SetActiveGameEndScoreText(bool value)
        {
            _gameEndScoreText.gameObject.SetActive(value);
        }
        
        private void SetGameEndScoreText(int value)
        {
            string scoreText;
            switch (value.ToString()[^1..])
            {
                case "1":
                    scoreText = "очко";
                    break;
                case "2":
                case "3":
                case "4":
                    scoreText = "очка";
                    break;
                default:
                    scoreText = "очков";
                    break;
            }
            
            _gameEndScoreText.text = $"Вы набрали {value} {scoreText}!";
        }
    }
}
