using System;

namespace Actors.Stats
{
    public class HealthStat : Stat<StatSettings>
    {
        private event Action OnDeath;
        
        public HealthStat(StatSettings settings) : base(settings) { }

        protected override void OnCheckValue()
        {
            base.OnCheckValue();
            if (CurrentValue <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}