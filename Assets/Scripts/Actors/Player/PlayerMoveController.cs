using Input;

namespace Actors.Player
{
    public class PlayerMoveController
    {
        private readonly PlayerBehaviour _player;
        private readonly IInput _input;
        
        public PlayerMoveController(IInput input, PlayerBehaviour player)
        {
            _input = input;
            _player = player;
        }

        public void Update()
        {
            _player.Rb.linearVelocity = _input.InputMove.normalized * _player.Config.speed;
        }
    }
}