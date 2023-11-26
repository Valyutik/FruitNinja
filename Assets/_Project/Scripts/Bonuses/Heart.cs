using UnityEngine;

namespace _Project.Scripts.Bonuses
{
    public sealed class Heart : MonoBehaviour
    {
        [field: SerializeField] public int HealthForHeart { get; private set; } = 1;
    }
}