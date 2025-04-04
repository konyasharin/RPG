using Actors.Stats;
using Core.Animations;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public Stat<StatSettings> Stat { get; private set; }

        public ReadOnlyReactiveProperty<Animation<NumericAnimationType>> AnimationReactive =>
            _animationReactiveInternal;
        private readonly ReactiveProperty<Animation<NumericAnimationType>> _animationReactiveInternal = new();
        
        private const float AnimationDuration = 1.5f;
        
        public void BindTo(Stat<StatSettings> stat)
        {
            Stat = stat;
            Stat.CurrentStatReactive.Subscribe(_ => OnChangeStat());
        }

        private void OnChangeStat()
        {
            var prevAnimation = _animationReactiveInternal.CurrentValue;
            if (
                prevAnimation != null &&
                prevAnimation.IsPlayingReactive.CurrentValue
            )
                prevAnimation.Stop();
            
            _animationReactiveInternal.Value = new Animation<NumericAnimationType>(AnimationFactory.Create(image.fillAmount, Stat.Percentage,
                AnimationDuration, NumericAnimationType.EaseOut));
            _animationReactiveInternal.CurrentValue?
                .Animate(
                    NumericTweener.GetInterpolationByAnimationType(_animationReactiveInternal.CurrentValue.Info.Type),
                    OnAnimationStep
                )
                .Forget();
        }

        private void OnAnimationStep(float value)
        {
            image.fillAmount = value;
        }
    }
}