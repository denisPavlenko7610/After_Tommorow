using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        public Inventory.InventorySystem inventorySystem;
        public override void InstallBindings()
        {
            Container.Bind<Inventory.InventorySystem>().FromInstance(inventorySystem).AsSingle().NonLazy();
        }
    }
}
