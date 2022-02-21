using AfterDestroy.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Core.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private TextMeshProUGUI objectName;  
        [SerializeField] private PointImage pointImage;  

        public override void InstallBindings()
        {
            Container.Bind<TextMeshProUGUI>().FromInstance(objectName).AsSingle().NonLazy();
            Container.Bind<PointImage>().FromInstance(pointImage).AsSingle().NonLazy();
        }
    }
}
