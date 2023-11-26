using System.Collections.Generic;
using _Project.Scripts.Bonuses;
using UnityEngine;

namespace _Project.Scripts.Fruits
{
    [CreateAssetMenu(fileName = "FruitSpawnerConfig", menuName = "Configs/FruitSpawnerConfig")]
    public sealed class FruitSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public List<Fruit> FruitPrefabs { get; private set; }
        [field: SerializeField] public List<Bomb> BombsPrefabs { get; private set; }
        [field: SerializeField] public Heart HeartPrefab { get; private set; }
        [field: SerializeField] public SandClocks SandClockPrefab { get; private set; }

        [field: Range(0, 100)] [field: SerializeField]
        public float minDelay = 0.2f,
            maxDelay = 0.9f,
            angleRangeZ = 20,
            lifeTime = 7f,
            minForce = 15f,
            maxForce = 25f,
            fruitWeight = 1f,
            minBombWeight = 0.1f,
            maxBombWeight = 0.25f,
            heartWeight = 0.02f,
            sandClocksWeight = 0.04f;
    }
}