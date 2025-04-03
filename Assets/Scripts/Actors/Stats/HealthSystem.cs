using JetBrains.Annotations;
using UI.Stats;

namespace Actors.Stats
{
    public class HealthSystem
    {
        public int Health => _healthStat.CurrentStat.CurrentValue;
        public int? Armor => _armorStat?.CurrentStat.CurrentValue;
        
        private readonly HealthStat _healthStat;
        [CanBeNull] private readonly ArmorStat _armorStat;
        
        public HealthSystem(StatSettings healthSettings, StatSettings armorSettings = null)
        {
            _healthStat = new HealthStat(healthSettings);
            _armorStat = armorSettings != null ? new ArmorStat(armorSettings) : null;
        }

        public void BindTo(StatBar healthBar, StatBar armorBar = null)
        {
            healthBar.BindTo(_healthStat);
            armorBar?.BindTo(_armorStat);
        }

        public void TakeDamage(int damage)
        {
            if (_armorStat != null)
            {
                int remainingDamage = damage - _armorStat.CurrentStat.CurrentValue;
                _armorStat.Reduce(damage);

                if (remainingDamage > 0) _healthStat.Reduce(remainingDamage);
            }
            else
            {
                _healthStat.Reduce(damage);
            }
        }
    }
}