using Actors.Stats;
using Core.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Stat<StatSettings> _stat;
        private readonly NumericTweener _tweener = new ();

        public void BindTo(Stat<StatSettings> stat)
        {
            _stat = stat;
            _stat.OnChange += OnChangeStat;
        }

        private void OnChangeStat()
        {
            _tweener.Animate(image.fillAmount, _stat.Percentage, 0.7f, OnAnimationStep, NumericAnimationType.EaseOut).Forget();
        }

        private void OnAnimationStep()
        {
            image.fillAmount = _tweener.CurrentValue;
        }
    }
}