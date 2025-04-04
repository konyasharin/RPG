using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Core.Animations
{
    public class Animation<T>
    {
        public float CurrentValue { get; private set; }
        public AnimationInfo<T> Info { get; }
        
        public ReadOnlyReactiveProperty<bool> IsPlayingReactive => _isPlayingReactiveInternal;
        public ReadOnlyReactiveProperty<bool> IsPausedReactive => _isPausedReactiveInternal;
        
        private readonly ReactiveProperty<bool> _isPlayingReactiveInternal = new(false);
        private readonly ReactiveProperty<bool> _isPausedReactiveInternal = new(false);
        private readonly ReactiveProperty<CancellationTokenSource> _ctsReactiveInternal = new(new CancellationTokenSource());

        public Animation(AnimationInfo<T> info)
        {
            Info = info;
            SubscribeOnIsPlayingChange();
        }
        
        public async UniTaskVoid Animate(Func<AnimationInfo<T>, float, float> getInterpolation, Action<float> onAnimationStep)
        {
            Stop();
            _ctsReactiveInternal.Value = new CancellationTokenSource();

            float time = 0;
            while (time < Info.Duration && !_ctsReactiveInternal.CurrentValue.IsCancellationRequested)
            {
                if (!_isPausedReactiveInternal.CurrentValue)
                {
                    time += Time.deltaTime;
                    CurrentValue = getInterpolation(Info, time);
                    onAnimationStep(CurrentValue);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _ctsReactiveInternal.CurrentValue.Token);
            }
            Stop();
        }
        
        public void Pause()
        {
            _isPausedReactiveInternal.Value = true;
        }

        public void Unpause()
        {
            _isPausedReactiveInternal.Value = false;
        }

        public void Stop()
        {
            if (!_ctsReactiveInternal.CurrentValue.IsCancellationRequested)
                _ctsReactiveInternal.CurrentValue.Cancel();
        }
        
        private void SubscribeOnIsPlayingChange()
        {
            Observable
                .CombineLatest(
                    Observable.EveryValueChanged(_ctsReactiveInternal, cts => cts.CurrentValue.IsCancellationRequested),
                    _isPausedReactiveInternal
                    )
                .Subscribe(pair =>
                {
                    (bool isRequested, bool isPaused) = (pair[0], pair[1]);
                    _isPlayingReactiveInternal.Value = !isRequested && !isPaused;
                });
        }
    }
}