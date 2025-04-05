using Actors.Player;
using UnityEngine;
using Zenject;

namespace Actors.Enemies.Slime
{
    public class SlimeBehaviour : EnemyBehaviour
    {
        [SerializeField] private SlimeConfig config;
        private PlayerBehaviour _player;
        
        [Inject]
        private void Construct(PlayerBehaviour player)
        {
            _player = player;
        }
        
        private void Start()
        {
            Initialize(config);
            MoveController.Follow(_player.transform);
        }
    }
}