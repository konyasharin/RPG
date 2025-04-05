using Actors.Combat;
using Actors.Configs;
using UnityEngine;

namespace Actors.Enemies
{
    public abstract class EnemyConfig : ActorConfig
    {
        public AttackConfig collisionAttack;
    }
}