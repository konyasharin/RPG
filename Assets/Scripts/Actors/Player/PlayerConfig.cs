﻿using Actors.Stats;
using UnityEngine;

namespace Actors.Player
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig: ScriptableObject
    {
        [Min(0.1f)] public float speed = 5;
        public StatSettings healthSettings;
        public StatSettings armorSettings;
    }
}