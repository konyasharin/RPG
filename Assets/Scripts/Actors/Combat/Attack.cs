using System.Threading;
using Actors.Stats;
using Core.Utils;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Actors.Combat
{
    public class Attack
    {
        public ReadOnlyReactiveProperty<bool> IsCanExecuteReactive => _isCanExecuteReactiveInternal; 
        private readonly ReactiveProperty<bool> _isCanExecuteReactiveInternal = new(true);

        private CancellationTokenSource _cts;
        private readonly AttackConfig _config;
        
        
        public Attack(AttackConfig config)
        {
            _config = config;
        }
        
        public bool Execute(IDamageable target)
        {
            if (!_isCanExecuteReactiveInternal.CurrentValue) return false;
            
            target.HealthSystem.TakeDamage(_config.damage);
            _isCanExecuteReactiveInternal.Value = false;
            WaitCooldownFinish().Forget();
            
            return true;
        }

        public bool ExecuteMany(IDamageable[] targets)
        {
            if (!_isCanExecuteReactiveInternal.CurrentValue) return false;
            
            foreach (var target in targets)
            {
                target.HealthSystem.TakeDamage(_config.damage);
            }
            
            _isCanExecuteReactiveInternal.Value = false;
            WaitCooldownFinish().Forget();
            
            return true;
        }

        private async UniTaskVoid WaitCooldownFinish()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            
            float time = 0;
            while (time < _config.cooldown)
            {
                time += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _isCanExecuteReactiveInternal.Value = true;
        }
    }
}