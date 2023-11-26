using UnityEngine;

namespace _Project.Scripts.Bonuses
{
    public sealed class Heart : MonoBehaviour
    {
        [field: SerializeField] public int HealthForHeart { get; private set; } = 1;
        [SerializeField] private GameObject heartParticle;
        
        public void ShowSliceParticles()
        {
            var tr = transform;
            Instantiate(heartParticle, tr.position, tr.rotation);
        }
    }
}