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
        private readonly GameObject _gameScreen;
        private readonly GameObject _gameEndScreen;
        private readonly TMP_Text _gameEndScoreText;

        public GameEnder(Score score, Health health, FruitSpawner fruitSpawner, GameObject gameScreen,
            GameObject gameEndScreen, TMP_Text gameEndScoreText)
        {
            _score = score;
            _health = health;
            _fruitSpawner = fruitSpawner;
            _gameScreen = gameScreen;
            _gameEndScreen = gameEndScreen;
            _gameEndScoreText = gameEndScoreText;
            SwitchScreens(true);
        }
        
        public void EndGame()
        {
            _fruitSpawner.Stop();
            SetGameEndScoreText(_score.GetScore());
            SwitchScreens(false);
        }
        
        public void RestartGame()
        {
            _score.Restart();

            _health.Restart();

            _fruitSpawner.Restart();

            SwitchScreens(true);
        }
        
        private void SwitchScreens(bool isGame)
        {
            _gameScreen.SetActive(isGame);
            _gameEndScreen.SetActive(!isGame);
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
