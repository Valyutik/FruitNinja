using _Project.Scripts.Fruits;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        [Header("Fruits")]
        [SerializeField] private Collider container;
        
        [Header("Score")]
        [SerializeField] private TMP_Text scoreText;

        [Header("Health")]
        [SerializeField] private TMP_Text healthText;
        [Range(0,10)]
        [SerializeField] private int startHealth;
        
        private FruitSpawnerConfig _fruitSpawnerConfig;
        
        public override void InstallBindings()
        {
            _fruitSpawnerConfig = Resources.Load<FruitSpawnerConfig>("Configs/FruitSpawnerConfig");
            var fruitSpawner = new FruitSpawner(_fruitSpawnerConfig, container);
            Container.BindInterfacesAndSelfTo<FruitSpawner>().FromInstance(fruitSpawner).AsSingle().NonLazy();

            var score = new Score(scoreText);
            Container.Bind<Score>().FromInstance(score).AsSingle();
            
            var health = new Health(startHealth, healthText);
            Container.Bind<Health>().FromInstance(health).AsSingle();
        }
    }
}