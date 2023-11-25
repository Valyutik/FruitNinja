using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Collider container;
        private FruitSpawnerConfig _fruitSpawnerConfig;
        
        public override void InstallBindings()
        {
            _fruitSpawnerConfig = Resources.Load<FruitSpawnerConfig>("Configs/FruitSpawnerConfig");
            var fruitSpawner = new FruitSpawner(_fruitSpawnerConfig, container);
            Container.BindInterfacesAndSelfTo<FruitSpawner>().FromInstance(fruitSpawner).AsSingle().NonLazy();
        }
    }
}