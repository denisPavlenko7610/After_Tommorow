using AfterDestroy.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] TextMeshProUGUI objectName;  
        [SerializeField] PointImage pointImage;
        [SerializeField] Inventory.Inventory inventory;

        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>().FromInstance(objectName).AsSingle().NonLazy();
            Container.Bind<PointImage>().FromInstance(pointImage).AsSingle().NonLazy();
            Container.Bind<Inventory.Inventory>().FromInstance(inventory).AsSingle().NonLazy();
        }
    }
}
