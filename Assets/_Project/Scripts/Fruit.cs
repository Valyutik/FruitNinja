using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fruit : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }

        public void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }
    }
}