using UnityEngine;

namespace Actors.Movement
{
    public interface IMoveable
    {
        public Rigidbody2D Rb { get; }
        public Animator Animator { get; }
        public SpriteRenderer SpriteRenderer { get; }
    }
}