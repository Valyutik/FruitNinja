using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Fruits
{
    [CreateAssetMenu(fileName = "FruitSpawnerConfig", menuName = "Configs/FruitSpawnerConfig")]
    public sealed class FruitSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public List<Fruit> FruitPrefabs { get; private set; }
        [field: SerializeField] public List<Bomb> BombsPrefabs { get; private set; }

        [field: Range(0, 100)] [field: SerializeField]
        public float minDelay = 0.2f,
            maxDelay = 0.9f,
            angleRangeZ = 20,
            lifeTime = 7f,
            minForce = 15f,
            maxForce = 25f,
            bombChance = 0.1f;
    }
}