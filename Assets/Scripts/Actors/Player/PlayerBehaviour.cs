using Actors.Animations;
using Actors.Stats;
using Input;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Actors.Player
{
    public class PlayerBehaviour: MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public PlayerConfig Config { get; private set; }
        
        [SerializeField] private StatBar healthBar;
        
        private PlayerMoveController _playerMoveController;
        private ActorAnimationsController _animationsController;
        private HealthStat _healthStat;

        [Inject]
        private void Construct(IInput input)
        {
            _playerMoveController = new PlayerMoveController(input, this);
        }

        private void Start()
        {
            _animationsController = new ActorAnimationsController(Animator, Rb, SpriteRenderer);
            _healthStat = new HealthStat(Config.healthSettings);
            healthBar.BindTo(_healthStat);
            
            _healthStat.Reduce(3);
            _healthStat.Reduce(2);
        }

        private void Update()
        {
            _playerMoveController.Update();
            _animationsController.Update();
        }
    }
}