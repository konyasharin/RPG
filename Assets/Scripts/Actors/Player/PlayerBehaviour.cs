using Actors.Animations;
using Input;
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
        
        private PlayerMoveController _playerMoveController;
        private ActorAnimationController _animationController;

        [Inject]
        private void Construct(IInput input)
        {
            _playerMoveController = new PlayerMoveController(input, this);
            _animationController = new ActorAnimationController(Animator, Rb, SpriteRenderer);
        }

        private void Update()
        {
            _playerMoveController.Update();
            _animationController.Update();
        }
    }
}