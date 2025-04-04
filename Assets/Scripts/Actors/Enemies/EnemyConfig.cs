using Actors.Stats;
using UnityEngine;

namespace Actors.Enemies
{
    public abstract class EnemyConfig : ScriptableObject
    {
        [Min(0.1f)] public float speed = 5;
        [Min(1)] public int damage = 5;
        public StatSettings healthSettings;
        public StatSettings armorSettings;
    }
}