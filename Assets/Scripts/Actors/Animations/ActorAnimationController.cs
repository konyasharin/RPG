using Core.Enums;
using Core.Utils;
using UnityEngine;

namespace Actors.Animations
{
    public class ActorAnimationController
    {
        private Vector2 LinearVelocity => _rb.linearVelocity;
        private Vector2 AnimationVelocity => new (_withLeftSide ? LinearVelocity.x : Mathf.Abs(LinearVelocity.x), LinearVelocity.y);
        
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
            _animator.SetFloat(VelocityXAnimatorFieldName, AnimationVelocity.x);
            _animator.SetFloat(VelocityYAnimatorFieldName, AnimationVelocity.y);
            
            _animator.SetBool(IsMovingAnimatorFieldName, AnimationVelocity.x != 0 || AnimationVelocity.y != 0);
            
            _animator.SetFloat(LastVelocityXAnimatorFieldName, CalculateAnimationLastVelocity(Axis.Horizontal));
            _animator.SetFloat(LastVelocityYAnimatorFieldName, CalculateAnimationLastVelocity(Axis.Vertical));
            
            if (AnimationVelocity.x != 0)
            {
                _spriteRenderer.flipX = LinearVelocity.x < 0;
            }
        }

        private float CalculateAnimationLastVelocity(Axis axis)
        {
            float currentAnimationVelocity = AxisUtils.GetValueFromVectorByAxis(AnimationVelocity, axis); 
            float otherAnimationVelocity = AxisUtils.GetValueFromVectorByAxis(AnimationVelocity, AxisUtils.GetOppositeAxis(axis));

            return currentAnimationVelocity switch
            {
                0 when otherAnimationVelocity != 0 => 0,
                0 => _animator.GetFloat(axis == Axis.Horizontal
                    ? LastVelocityXAnimatorFieldName
                    : LastVelocityYAnimatorFieldName),
                _ => currentAnimationVelocity
            };
        }
    }
}