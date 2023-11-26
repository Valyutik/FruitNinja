using UnityEngine;
using Zenject;

namespace _Project.Scripts.Fruits
{
    public sealed class FruitSlicer : MonoBehaviour
    {
        private const float MinSlicingMove = 0.01f;
        
        [SerializeField] private float sliceForce = 65;
        
        private GameEnder _gameEnder;
        private Score _score;
        private Health _health;
        private Collider _slicerTrigger;
        private Camera _mainCamera;
        private Vector3 _direction;

        [Inject]
        public void Construct(GameEnder gameEnder, Score score, Health health)
        {
            _gameEnder = gameEnder;
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
        }
        
        private void CheckFruit(Component other)
        {
            var fruit = other.GetComponent<Fruit>();

            if (fruit == null)
            {
                return;
            }

            fruit.Slice(_direction, transform.position, sliceForce);
            _score.AddScore(1);
        }
        
        private void CheckBomb(Collider other)
        {
            var bomb = other.GetComponent<Bomb>();
            if (bomb == null)
            {
                return;
            }

            Destroy(bomb.gameObject);
            _health.RemoveHealth();
            CheckHealthEnd(_health.GetCurrentHealth());
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
            Vector3 targetPosition = GetTargetPosition();
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