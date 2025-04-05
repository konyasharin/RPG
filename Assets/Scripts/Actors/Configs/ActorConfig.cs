using Actors.Combat;
using Actors.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors.Configs
{
    public abstract class ActorConfig : ScriptableObject
    {
        [Min(0.1f)] public float speed = 5;

        public AttackConfig baseAttack;
        
        public StatSettings healthSettings;
        public StatSettings armorSettings;
    }
}