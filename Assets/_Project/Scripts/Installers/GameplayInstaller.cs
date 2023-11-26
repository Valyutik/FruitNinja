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
        [SerializeField] private GameObject comboMultiplierRootGo;
        [SerializeField] private TextMeshProUGUI comboMultiplierText;
        [Range(0,10)]
        [SerializeField] private float comboIncreaseInterval = 1.1f;
        [Range(0,100)]
        [SerializeField] private int comboMultiplierIncreaseStep = 3;
        
        [Header("Score")]
        [SerializeField] private TMP_Text scoreText;

        [Header("Health")]
        [SerializeField] private TMP_Text healthText;
        [Range(0,10)]
        [SerializeField] private int startHealth;
        
        [Header("End game")]
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject gameEndScreen;
        [SerializeField] private TMP_Text gameEndScoreText;
        [SerializeField] private TMP_Text bestScoreText;
        
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

            Container.Bind<GameEnder>().FromNew().AsSingle().WithArguments(gameScreen, gameEndScreen, gameEndScoreText, bestScoreText);

            var fruitSlicerComboChecker = new FruitSlicerComboChecker(comboMultiplierRootGo, comboMultiplierText,
                comboIncreaseInterval, comboMultiplierIncreaseStep);
            Container.BindInterfacesAndSelfTo<FruitSlicerComboChecker>().FromInstance(fruitSlicerComboChecker).AsSingle();
        }
    }
}