using System;
using R3;

namespace Actors.Stats
{
    public class HealthStat : Stat<StatSettings>
    {
        private event Action OnDeath;

        public HealthStat(StatSettings settings) : base(settings)
        {
            CurrentStatReactive.Subscribe(OnChangeHealth);
        }

        private void OnChangeHealth(int health)
        {
            if (health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}