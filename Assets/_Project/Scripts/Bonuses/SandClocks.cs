using UnityEngine;

namespace _Project.Scripts.Bonuses
{
    public sealed class SandClocks : MonoBehaviour
    {
        [field:SerializeField] public float SlowDuration { get; private set; } = 3f;
    }
}