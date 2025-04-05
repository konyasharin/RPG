using System;
using Actors.Animations;
using Actors.Movement;
using Actors.Stats;
using UnityEngine;

namespace Actors.Enemies
{
    public abstract class EnemyBehaviour : MonoBehaviour, IDamageable, IMoveable
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [SerializeField] private EnemyDamageCollider damageCollider;
        
        public EnemyConfig Config { get; private set; }
        public HealthSystem HealthSystem { get; private set; }
        public ActorMoveController MoveController { get; private set; }
        
        private ActorMoveAnimationsController _moveAnimationsController;

        protected void Initialize(EnemyConfig info)
        {
            Config = info;
            HealthSystem = new HealthSystem(Config.healthSettings, Config.armorSettings);
            MoveController = new ActorMoveController(this, Config.speed);
            _moveAnimationsController = new ActorMoveAnimationsController(this);
            damageCollider.Initialize(Config.collisionAttack);
        }

        private void Update()
        {
            _moveAnimationsController.Update();
        }

        private void FixedUpdate()
        {
            MoveController.FixedUpdate();
        }
    }
}