using Actors.Animations;
using Actors.Movement;
using Actors.Stats;
using Input;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Actors.Player
{
    public class PlayerBehaviour: MonoBehaviour, IDamageable, IMoveable
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public PlayerConfig Config { get; private set; }
        
        [SerializeField] private StatBar healthBar;
        [SerializeField] private StatBar armorBar;
        
        public HealthSystem HealthSystem { get; private set; }
        
        private PlayerMoveController _playerMoveController;
        private ActorMoveAnimationsController _moveAnimationsController;
        private HealthSystemUi _healthSystemUi;

        [Inject]
        private void Construct(IInput input)
        {
            _playerMoveController = new PlayerMoveController(input, this);
        }

        private void Start()
        {
            _moveAnimationsController = new ActorMoveAnimationsController(this);
            HealthSystem = new HealthSystem(Config.healthSettings, Config.armorSettings);
            HealthSystem.BindTo(healthBar, armorBar);
            _healthSystemUi = new HealthSystemUi(healthBar, armorBar);
            
            HealthSystem.TakeDamage(60);
        }

        private void Update()
        {
            _playerMoveController.Update();
            _moveAnimationsController.Update();
        }
    }
}