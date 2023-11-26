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
        private float _currentDelay;

        public FruitSpawner(FruitSpawnerConfig config, Collider container)
        {
            _config = config;
            _container = container;
            SetNewDelay();
        }
        
        public void Tick()
        {
            MoveDelay();
        }
        
        private void SetNewDelay()
        {
            _currentDelay = Random.Range(_config.minDelay, _config.maxDelay);
        }
        
        private void MoveDelay()
        {
            _currentDelay -= Time.deltaTime;

            if(_currentDelay < 0)
            {
                var random = Random.value;

                if (random < _config.bombChance)
                {
                    SpawnBomb();
                }
                else
                {
                    SpawnFruit();
                }
                SetNewDelay();
            }
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
            var newObject = Object.Instantiate(prefab, startPosition, startRotation);

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
