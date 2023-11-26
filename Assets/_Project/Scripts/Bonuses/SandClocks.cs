using UnityEngine;

namespace _Project.Scripts.Bonuses
{
    public sealed class SandClocks : MonoBehaviour
    {
        [field:SerializeField] public float SlowDuration { get; private set; } = 3f;
        [SerializeField] private GameObject sandClockParticle;
        
        public void ShowSliceParticles()
        {
            var tr = transform;
            Instantiate(sandClockParticle, tr.position, tr.rotation);
        }
    }
}