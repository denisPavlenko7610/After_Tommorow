using AfterDestroy.Inventory;
using AfterDestroy.UI;
using AfterTomorrow;
using Inventory;
using RDTools.AutoAttach;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class Installer : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI _objectName;  
        [SerializeField] PointImage _pointImage;
        [SerializeField] InventorySystem _inventorySystem;
        [SerializeField] ItemInfoPanel _itemInfoPanel;
        [SerializeField] InventoryUI _inventoryUI;
        [SerializeField, Attach(Attach.Scene)] Player _player;

        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<TextMeshProUGUI>().FromInstance(_objectName).AsSingle().NonLazy();
            Container.Bind<PointImage>().FromInstance(_pointImage).AsSingle().NonLazy();
            Container.Bind<InventorySystem>().FromInstance(_inventorySystem).AsSingle().NonLazy();
            Container.Bind<ItemInfoPanel>().FromInstance(_itemInfoPanel).AsSingle();
            Container.Bind<InventoryUI>().FromInstance(_inventoryUI).AsSingle();
        }
    }
}
