using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class FruitSpawner : ITickable
    {
        private readonly FruitSpawnerConfig _config;
        private readonly Transform _container;
        private float _currentDelay;

        public FruitSpawner(FruitSpawnerConfig config, Transform container)
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
            if (_currentDelay < 0)
            {
                SpawnFruit();
                SetNewDelay();
            }
        }
        
        private void SpawnFruit()
        {
            var startRotation = Quaternion.Euler(0f, 0f, Random.Range(-_config.angleRangeZ, _config.angleRangeZ));
            var newFruit = Object.Instantiate(GetRandomFruitPrefab(), _container.position, startRotation, _container);
            newFruit.Initialize();
            Object.Destroy(newFruit, _config.lifeTime);
            AddForceFruit(newFruit);
        }
        
        private Fruit GetRandomFruitPrefab()
        {
            var index = Random.Range(1, 4);
            return _config.FruitPrefabs[index];
        }
        
        private void AddForceFruit(Fruit fruit)
        {
            var force = Random.Range(_config.minForce, _config.maxForce);
            Debug.Log(fruit.Rigidbody);
            fruit.Rigidbody.AddForce(fruit.Transform.up * force, ForceMode.Impulse);
        }
    }
}
