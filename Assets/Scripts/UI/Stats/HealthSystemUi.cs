using System;
using Core.Animations;
using JetBrains.Annotations;
using R3;
using Animation = Core.Animations.Animation;

namespace UI.Stats
{
    public class HealthSystemUi
    {
        private readonly StatBar _healthBar;
        [CanBeNull] private readonly StatBar _armorBar;
        
        public HealthSystemUi(StatBar healthBar, StatBar armorBar = null)
        {
            _healthBar = healthBar;
            _armorBar = armorBar;

            SetupChangeAnimationSubscription();
        }

        private void SetupChangeAnimationSubscription()
        {
            if (_healthBar.AnimationReactive == null)
                throw new Exception("Health Bar animation reactive is null");
            if (_armorBar != null && _armorBar.AnimationReactive == null)
                throw new Exception("Armor Bar animation reactive is null");

            if (_armorBar != null)
                _armorBar.AnimationReactive.Subscribe(SetupArmorBarAnimationSubscription);
            
            
            _healthBar.AnimationReactive.Subscribe(SetupHealthBarAnimationSubscription);
        }

        private void SetupArmorBarAnimationSubscription(Animation newAnimation)
        {
            newAnimation.IsPlayingReactive.Subscribe(isPlaying =>
            {
                var healthBarAnimation = _healthBar.AnimationReactive.CurrentValue;
                if (!GetBothIsDecreasing(healthBarAnimation, newAnimation)) return;
                if (healthBarAnimation.IsPausedReactive.CurrentValue && !isPlaying)
                {
                    healthBarAnimation.ChangeAnimationType(NumericAnimationType.EaseIn);
                    healthBarAnimation.Unpause();   
                }
                else if(!healthBarAnimation.IsPlayingReactive.CurrentValue && isPlaying)
                {
                    healthBarAnimation.Pause();
                }
            });
        }

        private void SetupHealthBarAnimationSubscription(Animation newAnimation)
        {
            newAnimation.IsPlayingReactive.Subscribe(isPlaying =>
            {
                var armorBarAnimation = _armorBar?.AnimationReactive.CurrentValue;
                if (!GetBothIsDecreasing(armorBarAnimation, newAnimation)) return;
                if (_armorBar != null && _armorBar.AnimationReactive.CurrentValue.IsPlayingReactive.CurrentValue && isPlaying) 
                    _healthBar.AnimationReactive.CurrentValue.Pause();
            });
        }

        private static bool GetBothIsDecreasing(Animation animation1, Animation animation2)
        {
            return animation1.Info.From > animation1.Info.To && animation2.Info.From > animation2.Info.To;
        }
    }
}