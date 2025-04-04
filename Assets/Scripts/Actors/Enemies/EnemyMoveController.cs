using Actors.Player;

namespace Actors.Enemies
{
    public class EnemyMoveController
    {
        public bool IsMoving { get; private set; }
        
        private readonly EnemyBehaviour<EnemyConfig> _enemy;
        private readonly PlayerBehaviour _player;
        
        public EnemyMoveController(EnemyBehaviour<EnemyConfig> enemy, PlayerBehaviour player)
        {
            _enemy = enemy;
            _player = player;
        }

        public void Update()
        {
            
        }
        
        public virtual void StartMoveToPlayer()
        {
            IsMoving = true;
        }

        public virtual void StopMove()
        {
            
        }
    }
}