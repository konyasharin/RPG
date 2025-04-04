using JetBrains.Annotations;
using UnityEngine;

namespace Actors.Movement
{
    public class ActorMoveController
    {
        public bool IsMoving => _targetTransform != null || _targetPoint != null;
        
        [CanBeNull] private Transform _targetTransform;
        [CanBeNull] private Vector2? _targetPoint;
        private readonly float _speed;
        private readonly IMoveable _actor;
        private float _speedFactor;
        private float _currentMovingTime;
        
        private const float TimeToMaxSpeedFactor = 0.5f;
        private const float StopDistance = 0.5f;
        private const float DistanceDelta = 0.1f;
        
        public ActorMoveController(IMoveable actor, float speed)
        {
            _actor = actor;
            _speed = speed;
        }
        
        public void FixedUpdate()
        {
            var target = _targetPoint ?? _targetTransform?.position;
            if (target == null || !IsMoving) return;

            var distance = Vector2.Distance(_actor.Rb.position, (Vector2)target);
            _currentMovingTime += Time.fixedDeltaTime;
            
            if (_currentMovingTime >= TimeToMaxSpeedFactor)
                _speedFactor = 1;
            else
                _speedFactor = Mathf.Lerp(
                    0, 1, 
                    Mathf.Min(
                        _currentMovingTime / TimeToMaxSpeedFactor, 
                        distance / StopDistance
                    )
                );
            
            _actor.Rb.linearVelocity = ((Vector2)target - _actor.Rb.position).normalized * Mathf.Lerp(0, _speed, _speedFactor);
            if (distance <= DistanceDelta) StopMove();
        }
        
        public virtual void Follow(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _targetPoint = null;
        }

        public virtual void MoveToPoint(Vector2 targetPoint)
        {
            _targetPoint = targetPoint;
            _targetTransform = null;
        }

        public virtual void StopMove()
        {
            _targetTransform = null;
            _targetPoint = null;
            _currentMovingTime = 0;
            _actor.Rb.linearVelocity = Vector2.zero;
        }
    }
}