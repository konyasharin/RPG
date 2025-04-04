using System;
using UnityEngine;

namespace Actors.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyDamageCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            
        }
    }
}