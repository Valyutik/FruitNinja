using UnityEngine;

namespace _Project.Scripts.Fruits
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Fruit : MonoBehaviour
    {
        private Rigidbody Rigidbody { get; set; }
        public Transform Transform { get; private set; }
        
        [SerializeField] private GameObject wholeFruit;
        [SerializeField] private GameObject slicedFruit;
        [SerializeField] private Rigidbody topPartRigidbody;
        [SerializeField] private Rigidbody bottomPartRigidbody;

        private Collider _sliceTrigger;

        public void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _sliceTrigger = GetComponent<Collider>();
            Transform = transform;
        }
        
        public void Slice(Vector3 direction, Vector3 position, float force)
        {
            SetSliced();

            RotateBySliceDirection(direction);

            AddForce(topPartRigidbody, direction, position, force);
            AddForce(bottomPartRigidbody, direction, position, force);
        }
        
        private void SetSliced()
        {
            wholeFruit.SetActive(false);

            slicedFruit.SetActive(true);

            _sliceTrigger.enabled = false;
        }
        
        private void RotateBySliceDirection(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            slicedFruit.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        
        private void AddForce(Rigidbody sliceRigidbody, Vector3 direction, Vector3 position, float force)
        {
            sliceRigidbody.velocity = Rigidbody.velocity;
            sliceRigidbody.angularVelocity = Rigidbody.angularVelocity;

            sliceRigidbody.AddForceAtPosition(direction * force, position);
        }
    }
}