using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Core.Animations
{
    public class NumericTweener
    {
        public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;
        public ReadOnlyReactiveProperty<bool> IsPaused => _isPaused;
        
        private readonly ReactiveProperty<bool> _isPlaying = new(false);
        private readonly ReactiveProperty<bool> _isPaused = new(false);
        private readonly ReactiveProperty<CancellationTokenSource> _cts = new(new CancellationTokenSource());
        private float _currentValue;

        public NumericTweener()
        {
            SubscribeOnIsPlayingChange();
        }

        public async UniTaskVoid Animate(float from, float to, float duration, Action<float> onAnimationStep,
            NumericAnimationType animation = NumericAnimationType.Linear)
        {
            _cts.CurrentValue.Cancel();
            _cts.Value = new CancellationTokenSource();

            float time = 0;
            while (time < duration)
            {
                if (!_isPaused.CurrentValue)
                {
                    time += Time.deltaTime;
                    _currentValue = GetInterpolationByAnimationType(from, to, duration, time, animation);
                    onAnimationStep(_currentValue);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.CurrentValue.Token);
            }
        }

        public void Pause()
        {
            _isPaused.Value = true;
        }

        public void Unpause()
        {
            _isPaused.Value = false;
        }

        private void SubscribeOnIsPlayingChange()
        {
            Observable
                .CombineLatest(_cts.Select(value => value.IsCancellationRequested), _isPaused)
                .Subscribe(pair =>
                {
                    (bool isRequested, bool isPaused) = (pair[0], pair[1]);
                    _isPlaying.Value = !isRequested && !isPaused;
                });
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