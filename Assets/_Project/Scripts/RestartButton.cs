using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Button))]
    public sealed class RestartButton : MonoBehaviour
    {
        private Button _button;
        private GameEnder _gameEnder;
        
        [Inject]
        private void Construct(GameEnder gameEnder)
        {
            _gameEnder = gameEnder;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_gameEnder.RestartGame);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_gameEnder.RestartGame);
        }
    }
}