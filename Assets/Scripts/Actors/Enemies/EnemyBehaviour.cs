using System;
using Actors.Animations;
using Actors.Movement;
using Actors.Stats;
using UnityEngine;

namespace Actors.Enemies
{
    public abstract class EnemyBehaviour<T> : MonoBehaviour, IDamageable, IMoveable where T : EnemyConfig
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        public T Info { get; private set; }
        public HealthSystem HealthSystem { get; private set; }
        public ActorMoveController MoveController { get; private set; }
        
        private ActorMoveAnimationsController _moveAnimationsController;

        protected void Initialize(T info)
        {
            Info = info;
            HealthSystem = new HealthSystem(Info.healthSettings, Info.armorSettings);
            MoveController = new ActorMoveController(this, Info.speed);
            _moveAnimationsController = new ActorMoveAnimationsController(this);
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