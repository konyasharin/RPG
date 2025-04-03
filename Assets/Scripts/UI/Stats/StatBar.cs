using Actors.Stats;
using Core.Animations;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Image image;

        public bool IsAnimationPlaying => Tweener.IsPlaying.CurrentValue;
        public NumericTweener Tweener { get; } = new();
        public Stat<StatSettings> Stat { get; private set; }
        
        private const float AnimationDuration = 1.5f;

        public void BindTo(Stat<StatSettings> stat)
        {
            Stat = stat;
            Stat.CurrentStat.Subscribe(_ => OnChangeStat());
        }

        private void OnChangeStat()
        {
            Tweener.Animate(image.fillAmount, Stat.Percentage, AnimationDuration, OnAnimationStep, NumericAnimationType.EaseOut).Forget();
        }

        private void OnAnimationStep(float value)
        {
            image.fillAmount = value;
        }
    }
}