using Actors.Player;
using Input;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerBehaviour player;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBehaviour>().FromInstance(player).AsSingle();
        }
    }
}
