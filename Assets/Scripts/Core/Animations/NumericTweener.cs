using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Animations
{
    public class NumericTweener
    {
        public float CurrentValue { get; private set; }
        private CancellationTokenSource _cts;
        
        public async UniTaskVoid Animate(float from, float to, float duration, Action onAnimationStep,
            NumericAnimationType animation = NumericAnimationType.Linear)
        {
            _cts = new CancellationTokenSource();

            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                CurrentValue = GetInterpolationByAnimationType(from, to, duration, time, animation);
                onAnimationStep();
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
        }

        private static float GetInterpolationByAnimationType(float from, float to, float duration, float time, NumericAnimationType animation)
        {
            return animation switch
            {
                NumericAnimationType.Linear => GetLinearInterpolation(from, to, duration, time),
                NumericAnimationType.EaseOut => GetEaseOutInterpolation(from, to, duration, time),
                _ => throw new ArgumentOutOfRangeException(nameof(animation), animation, null)
            };
        }

        private static float GetLinearInterpolation(float from, float to, float duration, float time)
        {
            return Mathf.Lerp(from, to, time / duration);
        }

        private static float GetEaseOutInterpolation(float from, float to, float duration, float time)
        {
            return Mathf.Lerp(from, to, 1 - Mathf.Pow(1 - (time / duration), 2));
        }
    }
}