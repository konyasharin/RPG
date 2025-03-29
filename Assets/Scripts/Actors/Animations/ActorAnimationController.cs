using Core.Enums;
using UnityEngine;

namespace Actors.Animations
{
    public class ActorAnimationController
    {
        private static readonly int VelocityXAnimatorFieldName = Animator.StringToHash("VelocityX");
        private static readonly int VelocityYAnimatorFieldName = Animator.StringToHash("VelocityY");
        private static readonly int LastVelocityXAnimatorFieldName = Animator.StringToHash("LastVelocityX");
        private static readonly int LastVelocityYAnimatorFieldName = Animator.StringToHash("LastVelocityY");
        private static readonly int IsMovingAnimatorFieldName = Animator.StringToHash("IsMoving");

        private readonly bool _withLeftSide;
        private readonly Animator _animator;
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer _spriteRenderer;
        
        public ActorAnimationController(
            Animator animator, 
            Rigidbody2D rb, 
            SpriteRenderer spriteRenderer,
            bool withLeftSide = false
            )
        {
            _withLeftSide = withLeftSide;
           _animator = animator;
           _rb = rb;
           _spriteRenderer = spriteRenderer;
        }

        public void Update()
        {
            float animationVelocityX = _withLeftSide ? _rb.linearVelocity.x : Mathf.Abs(_rb.linearVelocity.x);
            float animationVelocityY = _rb.linearVelocity.y;
            
            _animator.SetFloat(VelocityXAnimatorFieldName, animationVelocityX);
            _animator.SetFloat(VelocityYAnimatorFieldName, animationVelocityY);
            _animator.SetBool(IsMovingAnimatorFieldName, animationVelocityX != 0 || animationVelocityY != 0);
            
            if (animationVelocityX != 0)
            {
                _animator.SetFloat(LastVelocityXAnimatorFieldName, animationVelocityX);
                _spriteRenderer.flipX = _rb.linearVelocity.x < 0;
            }
            if (animationVelocityY != 0)
            {
                _animator.SetFloat(LastVelocityYAnimatorFieldName, animationVelocityY);
            }
        }
    }
}