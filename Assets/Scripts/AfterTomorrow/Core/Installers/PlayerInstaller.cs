using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] Player.Player player;
        [SerializeField] Transform playerTransformPosition;

        public override void InstallBindings()
        {
            var playerInstance =
                Container.InstantiatePrefabForComponent<Player.Player>(player.gameObject, playerTransformPosition.transform.position,
                    Quaternion.identity, null);

            Container.Bind<Player.Player>().FromInstance(playerInstance).AsSingle().NonLazy();
            Container.QueueForInject(player);
        }
    }
}