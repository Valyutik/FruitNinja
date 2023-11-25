using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class FruitSpawner : ITickable
    {
        private readonly FruitSpawnerConfig _config;
        private readonly Collider _container;
        private readonly Transform _transformContainer;
        private float _currentDelay;

        public FruitSpawner(FruitSpawnerConfig config, Collider container)
        {
            _config = config;
            _container = container;
            _transformContainer = _container.transform;
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
            if (_currentDelay < 0)
            {
                SpawnFruit();
                SetNewDelay();
            }
        }
        
        private void SpawnFruit()
        {
            var startPosition = GetRandomSpawnPosition();
            var startRotation = Quaternion.Euler(0f, 0f, Random.Range(-_config.angleRangeZ, _config.angleRangeZ));
            var newFruit = Object.Instantiate(GetRandomFruitPrefab(), startPosition, startRotation, _transformContainer);
            newFruit.Initialize();
            Object.Destroy(newFruit, _config.lifeTime);
            AddForceFruit(newFruit);
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
        
        private void AddForceFruit(Fruit fruit)
        {
            var force = Random.Range(_config.minForce, _config.maxForce);
            fruit.Rigidbody.AddForce(fruit.Transform.up * force, ForceMode.Impulse);
        }
    }
}
