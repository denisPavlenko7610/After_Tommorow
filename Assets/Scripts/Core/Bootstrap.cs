using AfterDestroy;
using AfterDestroy.Inventory;
using AfterDestroy.UI;
using AfterTomorrow.Core.RDDependency;
using Inventory;
using RDDependency;
using RDTools;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AfterTomorrow.Core.Installers
{
    [DefaultExecutionOrder(-17000)]
    public class Bootstrap : MonoBehaviour, ISetup
    {
        [SerializeField] TextMeshProUGUI _objectName;  
        [SerializeField] PointImage _pointImage;
        [SerializeField] InventorySystem _inventorySystem;
        [SerializeField] InventoryUI _inventoryUI;
        [SerializeField] ItemInfoPanel _itemInfoPanel;
        [SerializeField] Player _playerPrefab;
        [SerializeField] Transform _playerSpawnPosition;

        public List<MonoBehaviour> _objectsToInject = new List<MonoBehaviour>();

        void OnValidate()
        {
            if (_objectsToInject.Count == 0)
            {
                FindObjectsNeedInject();
            }
        }

        protected void Awake()
        {
            Setup();
        }
        
        [Button]
        public void FindObjectsNeedInject()
        {
            _objectsToInject = IoCContainer.FindObjectsWithInjectAttribute();
        }

        public void Setup()
        {
            Provider provider = gameObject.AddComponent<Provider>();
            
            RegisterUI();
            
            IoCContainer.InjectOnScene(_objectsToInject);
            
            RegisterPlayer(provider);

            //show example of register interface to concrete object
            //IoCContainer.Register<IInventorySystem, InventorySystem>();
        }
        
        void RegisterUI()
        {
            IoCContainer.Register<TextMeshProUGUI>(_objectName);
            IoCContainer.Register<PointImage>(_pointImage);
            IoCContainer.Register<InventorySystem>(_inventorySystem);
            IoCContainer.Register<ItemInfoPanel>(_itemInfoPanel);
            IoCContainer.Register<InventoryUI>(_inventoryUI);
        }
        void RegisterPlayer(Provider provider)
        {
            Player player = provider.ProvideFactory(_playerPrefab, _playerSpawnPosition.position);
            PlayerInput playerInput = player.PlayerInput;
            CheckInteractable playerInteractable = playerInput.CheckInteractable;
            
            IoCContainer.Register<Player>(player);
            IoCContainer.Register<PlayerInput>(playerInput);
            IoCContainer.Register<CheckInteractable>(playerInteractable);
        }
    }
}
