using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        private Inventory.Inventory inventory;
        public override void InstallBindings()
        {
            Container.Bind<Inventory.Inventory>().FromInstance(inventory).AsSingle().NonLazy();
        }
    }
}
