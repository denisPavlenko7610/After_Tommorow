using AfterDestroy.Inventory;
using AfterDestroy.UI;
using Inventory;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI _objectName;  
        [SerializeField] PointImage _pointImage;
        [SerializeField] InventorySystem _inventorySystem;
        [SerializeField] ItemInfoPanel _itemInfoPanel;
        [SerializeField] private InventoryUI _inventoryUI;

        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>().FromInstance(_objectName).AsSingle().NonLazy();
            Container.Bind<PointImage>().FromInstance(_pointImage).AsSingle().NonLazy();
            Container.Bind<InventorySystem>().FromInstance(_inventorySystem).AsSingle().NonLazy();
            Container.Bind<ItemInfoPanel>().FromInstance(_itemInfoPanel).AsSingle();
            Container.Bind<InventoryUI>().FromInstance(_inventoryUI).AsSingle();
        }
    }
}
