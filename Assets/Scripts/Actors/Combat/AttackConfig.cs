using System;
using UnityEngine;

namespace Actors.Combat
{
    [Serializable]
    public class AttackConfig
    {
        [Min(1)] public int damage;
        [Min(0.1f)] public float cooldown;
    }
}