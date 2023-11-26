using _Project.Scripts.Bonuses;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Fruits
{
    [RequireComponent(typeof(Collider))]
    public sealed class FruitSlicer : MonoBehaviour
    {
        private const float MinSlicingMove = 0.01f;
        
        [SerializeField] private float sliceForce = 65;
        
        private GameEnder _gameEnder;
        private FruitSlicerComboChecker _comboChecker;
        private SlowMotion _slowMotion;
        private Score _score;
        private Health _health;
        private Collider _slicerTrigger;
        private Camera _mainCamera;
        private Vector3 _direction;

        [Inject]
        public void Construct(GameEnder gameEnder, FruitSlicerComboChecker comboChecker, SlowMotion slowMotion,
            Score score, Health health)
        {
            _gameEnder = gameEnder;
            _comboChecker = comboChecker;
            _slowMotion = slowMotion;
            _score = score;
            _health = health;
            _slicerTrigger = GetComponent<Collider>();
            _mainCamera = Camera.main;
            SetSlicing(false);
        }
        
        public void Update()
        {
            Slicing();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            CheckFruit(other);
            CheckBomb(other);
            CheckHeart(other);
            CheckSandClocks(other);
        }
        
        private void CheckHeart(Component other)
        {
            var heart = other.GetComponentInParent<Heart>();

            if (heart == null)
            {
                return;
            }

            var healthForHeart = heart.HealthForHeart;
            Destroy(heart.gameObject);
            _comboChecker.IncreaseComboStep();
            _health.AddHealth(healthForHeart);
        }
        
        private void CheckSandClocks(Component other)
        {
            var sandClocks = other.GetComponent<SandClocks>();
            if (sandClocks == null)
            {
                return;
            }

            var slowDuration = sandClocks.SlowDuration; 
            
            Destroy(sandClocks.gameObject);
            _comboChecker.IncreaseComboStep();
            _slowMotion.StartSlow(slowDuration);
        }
        
        private void CheckFruit(Component other)
        {
            var fruit = other.GetComponent<Fruit>();
            if (fruit == null)
            {
                return;
            }

            fruit.Slice(_direction, transform.position, sliceForce);
            
            _comboChecker.IncreaseComboStep();
            var scoreByFruit = 1 * _comboChecker.GetComboMultiplier();
            _score.AddScore(scoreByFruit);
        }
        
        private void CheckBomb(Component other) 
        {
            var bomb = other.GetComponent<Bomb>();
            if (bomb == null)
            {
                return;
            }

            Destroy(bomb.gameObject);
            _health.RemoveHealth();
            CheckHealthEnd(_health.GetCurrentHealth());

            _comboChecker.StopCombo();
        }
        
        private void CheckHealthEnd(int health)
        {
            if (health > 0)
            {
                return;
            }
    
            StopGame();
        }

        private void StopGame()
        {
            _gameEnder.EndGame();
        }
        
        private void Slicing()
        {
            if (Input.GetMouseButton(0))
            {
                RefreshSlicing();
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetSlicing(false);
            }
        }
        
        private void SetSlicing(bool value)
        {
            _slicerTrigger.enabled = value;
        }
        
        private void RefreshSlicing()
        {
            var targetPosition = GetTargetPosition();
            RefreshDirection(targetPosition);
            MoveSlicer(targetPosition);
            var isSlicing = CheckMoreThenMinMove(_direction);
            SetSlicing(isSlicing);
        }
        
        private Vector3 GetTargetPosition()
        {
            var targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            return targetPosition;
        }
        
        private void RefreshDirection(Vector3 targetPosition)
        {
            _direction = targetPosition - transform.position;
        }
        
        private void MoveSlicer(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }
        
        private bool CheckMoreThenMinMove(Vector3 direction)
        {
            var slicingSpeed = direction.magnitude / Time.deltaTime;
            return slicingSpeed >= MinSlicingMove;
        }
    }
}