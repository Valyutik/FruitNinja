using UnityEngine;

namespace _Project.Scripts.Fruits
{
    public sealed class FruitSlicer : MonoBehaviour
    {
        private const float MinSlicingMove = 0.01f;
        
        [SerializeField] private float sliceForce = 65;
        
        private Collider _slicerTrigger;
        private Camera _mainCamera;
        private Vector3 _direction;

        public void Start()
        {
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
            var fruit = other.GetComponent<Fruit>();

            if (fruit == null)
            {
                return;
            }

            fruit.Slice(_direction, transform.position, sliceForce);
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