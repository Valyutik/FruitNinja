using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Fruits
{
    public sealed class FruitSpawner : ITickable
    {
        private readonly FruitSpawnerConfig _config;
        private readonly Collider _container;
        private readonly Transform _containerTransform;
        private readonly DifficultyChanger _difficultyChanger;
        private float _currentDelay;
        private bool _isActive = true;

        public FruitSpawner(FruitSpawnerConfig config, DifficultyChanger difficultyChanger, Collider container)
        {
            _config = config;
            _difficultyChanger = difficultyChanger;
            _container = container;
            _containerTransform = container.transform;
            SetNewDelay();
        }
        
        public void Tick()
        {
            if (!_isActive)
            {
                return;
            }
            MoveDelay();
        }
        
        public void Stop()
        {
            _isActive = false;
        }
        
        public void Restart()
        {
            _isActive = true;
            SetNewDelay();
        }
        
        private void SetNewDelay()
        {
            _currentDelay = _difficultyChanger.CalculateRandomSpawnDelay(_config.minDelay, _config.maxDelay);
        }
        
        private void MoveDelay()
        {
            _currentDelay -= Time.deltaTime;

            if(_currentDelay < 0)
            {
                GameObject prefab = GetPrefabByWeights();

                SpawnObject(prefab);

                SetNewDelay();
            }
        }
        
        private GameObject GetPrefabByWeights()
        {
            float bombWeight = _difficultyChanger.CalculateBombChance(_config.minBombWeight, _config.maxBombWeight);
            var totalWeight = _config.fruitWeight + bombWeight + _config.heartWeight + _config.sandClocksWeight;

            var random = Random.Range(0, totalWeight);

            if (random <= bombWeight)
            {
                return _config.BombsPrefabs[Random.Range(0, _config.BombsPrefabs.Count)].gameObject;
            }
    
            random -= bombWeight;

            if (random <= _config.heartWeight)
            {
                return _config.HeartPrefab.gameObject;
            }
 
            random -= _config.heartWeight;

            if (random <= _config.sandClocksWeight)
            {
                return _config.SandClockPrefab.gameObject;
            }

            return GetRandomFruitPrefab().gameObject;
        }
        
        private void SpawnFruit()
        {
            var fruitPrefab = GetRandomFruitPrefab();
            SpawnObject(fruitPrefab.gameObject);
        }
        
        private void SpawnBomb()
        {
            SpawnObject(_config.BombsPrefabs[Random.Range(0, _config.BombsPrefabs.Count)].gameObject);
        }
        
        private void SpawnObject(GameObject prefab)
        {
            var startPosition = GetRandomSpawnPosition();
            var startRotation = Quaternion.Euler(0f, 0f, Random.Range(-_config.angleRangeZ, _config.angleRangeZ));
            var newObject = Object.Instantiate(prefab, startPosition, startRotation, _containerTransform);

            Object.Destroy(newObject, _config.lifeTime);
            AddForceFruit(newObject);
        }
        
        private Fruit GetRandomFruitPrefab()
        {
            var index = Random.Range(1, 4);
            return _config.FruitPrefabs[index];
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 pos;
            var bounds = _container.bounds;
            pos.x = Random.Range(bounds.min.x, bounds.max.x);
            pos.y = Random.Range(bounds.min.y, bounds.max.y);
            pos.z = Random.Range(bounds.min.z, bounds.max.z);
            return pos;
        }
        
        private void AddForceFruit(GameObject fruit)
        {
            var force = Random.Range(_config.minForce, _config.maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
        }
    }
}
