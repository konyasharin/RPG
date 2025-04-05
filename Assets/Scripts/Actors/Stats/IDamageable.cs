using UnityEngine;

namespace Actors.Stats
{
    public interface IDamageable
    {
        public HealthSystem HealthSystem { get; }
        public Rigidbody2D Rb { get; }
    }
}