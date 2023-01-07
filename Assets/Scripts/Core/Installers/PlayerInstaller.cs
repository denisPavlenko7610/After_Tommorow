using AfterTomorrow.Player;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] Player player;
        [SerializeField] Transform playerTransformPosition;

        public override void InstallBindings()
        {
            var playerInstance =
                Container.InstantiatePrefabForComponent<IPlayer>(player.gameObject, playerTransformPosition.transform.position,
                    Quaternion.identity, null);

            Container.Bind<IPlayer>().FromInstance(playerInstance).AsSingle().NonLazy();
            Container.QueueForInject(player);
        }
    }
}