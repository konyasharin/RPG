using Actors.Movement;
using Core.Enums;
using Core.Utils;
using UnityEngine;

namespace Actors.Animations
{
    public class ActorMoveAnimationsController
    {
        private Vector2 LinearVelocity => _actor.Rb.linearVelocity;
        private Vector2 AnimationVelocity => new (_withLeftSide ? LinearVelocity.x : Mathf.Abs(LinearVelocity.x), LinearVelocity.y);
        
        private static readonly int VelocityXAnimatorFieldName = Animator.StringToHash("VelocityX");
        private static readonly int VelocityYAnimatorFieldName = Animator.StringToHash("VelocityY");
        private static readonly int LastVelocityXAnimatorFieldName = Animator.StringToHash("LastVelocityX");
        private static readonly int LastVelocityYAnimatorFieldName = Animator.StringToHash("LastVelocityY");
        private static readonly int IsMovingAnimatorFieldName = Animator.StringToHash("IsMoving");

        private readonly bool _withLeftSide;
        private readonly IMoveable _actor;
        
        public ActorMoveAnimationsController(
            IMoveable actor,
            bool withLeftSide = false
            )
        {
            _withLeftSide = withLeftSide;
           _actor = actor;
        }

        public void Update()
        {
            _actor.Animator.SetFloat(VelocityXAnimatorFieldName, AnimationVelocity.x);
            _actor.Animator.SetFloat(VelocityYAnimatorFieldName, AnimationVelocity.y);
            
            _actor.Animator.SetBool(IsMovingAnimatorFieldName, AnimationVelocity.x != 0 || AnimationVelocity.y != 0);
            
            _actor.Animator.SetFloat(LastVelocityXAnimatorFieldName, CalculateAnimationLastVelocity(Axis.Horizontal));
            _actor.Animator.SetFloat(LastVelocityYAnimatorFieldName, CalculateAnimationLastVelocity(Axis.Vertical));
            
            if (AnimationVelocity.x != 0)
            {
                _actor.SpriteRenderer.flipX = LinearVelocity.x < 0;
            }
        }

        private float CalculateAnimationLastVelocity(Axis axis)
        {
            float currentAnimationVelocity = AxisUtils.GetValueFromVectorByAxis(AnimationVelocity, axis); 
            float otherAnimationVelocity = AxisUtils.GetValueFromVectorByAxis(AnimationVelocity, AxisUtils.GetOppositeAxis(axis));

            return currentAnimationVelocity switch
            {
                0 when otherAnimationVelocity != 0 => 0,
                0 => _actor.Animator.GetFloat(axis == Axis.Horizontal
                    ? LastVelocityXAnimatorFieldName
                    : LastVelocityYAnimatorFieldName),
                _ => currentAnimationVelocity
            };
        }
    }
}