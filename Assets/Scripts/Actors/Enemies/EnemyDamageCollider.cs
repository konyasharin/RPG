using System;
using System.Collections.Generic;
using Actors.Combat;
using Actors.Stats;
using UnityEngine;

namespace Actors.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDamageCollider : MonoBehaviour
    {
        [SerializeField] private Collider2D collision;
        
        private List<IDamageable> _objectsToDamage = new();
        private Attack _attack;
        private const string EnemyTag = "Enemy";

        public void Initialize(AttackConfig collisionAttack)
        {
            _attack = new Attack(collisionAttack);
        }
        
        private void Update()
        {
            if (!_attack.IsCanExecuteReactive.CurrentValue) return;
            _attack.ExecuteMany(_objectsToDamage.ToArray());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(EnemyTag)) return;
            
            var damageable = other.gameObject.GetComponent<IDamageable>();
            _objectsToDamage.Add(damageable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(EnemyTag)) return;
            
            var damageable = other.gameObject.GetComponent<IDamageable>();
            _objectsToDamage.Remove(damageable);
        }
    }
}