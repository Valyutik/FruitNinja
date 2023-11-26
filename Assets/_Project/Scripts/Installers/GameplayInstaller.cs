using _Project.Scripts.Bonuses;
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
        
        [Header("Game difficulty")]
        [Range(0,100)]
        [SerializeField] private int difficultyUpScoreStep = 30, maxDifficult = 20;
        
        [Header("Score")]
        [SerializeField] private TMP_Text scoreText;

        [Header("Health")]
        [SerializeField] private TMP_Text healthText;
        [Range(0,10)]
        [SerializeField] private int startHealth;
        
        [Header("Slow Motion Bonus")]
        [Range(0,10)]
        [SerializeField] private float normalTimeScale = 1, slowMotionTimeScale = 0.7f;
        
        [Header("End game")]
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject gameEndScreen;
        [SerializeField] private TMP_Text gameEndScoreText;
        [SerializeField] private TMP_Text bestScoreText;
        
        private FruitSpawnerConfig _fruitSpawnerConfig;
        
        public override void InstallBindings()
        {
            var difficultyChanger = new DifficultyChanger(difficultyUpScoreStep, maxDifficult);
            Container.Bind<DifficultyChanger>().FromInstance(difficultyChanger).AsSingle();
            
            _fruitSpawnerConfig = Resources.Load<FruitSpawnerConfig>("Configs/FruitSpawnerConfig");
            Container.Bind<FruitSpawnerConfig>().FromInstance(_fruitSpawnerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<FruitSpawner>().FromNew().AsSingle().WithArguments(container).NonLazy();

            Container.Bind<Score>().FromNew().AsSingle().WithArguments(scoreText);
            
            var health = new Health(startHealth, healthText);
            Container.Bind<Health>().FromInstance(health).AsSingle();

            Container.Bind<GameEnder>().FromNew().AsSingle().WithArguments(gameScreen, gameEndScreen, gameEndScoreText, bestScoreText);

            var fruitSlicerComboChecker = new FruitSlicerComboChecker(comboMultiplierRootGo, comboMultiplierText,
                comboIncreaseInterval, comboMultiplierIncreaseStep);
            Container.BindInterfacesAndSelfTo<FruitSlicerComboChecker>().FromInstance(fruitSlicerComboChecker).AsSingle();

            Container.BindInterfacesAndSelfTo<SlowMotion>().FromNew().AsSingle()
                .WithArguments(normalTimeScale, slowMotionTimeScale).NonLazy();
        }
    }
}