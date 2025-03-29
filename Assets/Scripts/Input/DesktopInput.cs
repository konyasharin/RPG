using UnityEngine;
using Zenject;

namespace Input
{
    public class DesktopInput : IInput, ITickable
    {
        public Vector2 InputMove { get; private set; }

        public void Tick()
        {
            InputMove = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }
    }
}