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
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _objectName;  
        [SerializeField] PointImage _pointImage;
        [SerializeField] InventorySystem _inventorySystem;
        [SerializeField] InventoryUI _inventoryUI;
        [SerializeField] ItemInfoPanel _itemInfoPanel;
        [SerializeField] Player _playerPrefab;
        [SerializeField] Transform _playerSpawnPosition;

        public List<MonoBehaviour> _objectsToInject = new List<MonoBehaviour>();
        Provider _provider; 
        IoCContainer _container = new IoCContainer();

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
            _provider.OnAwake();
        }

        protected void Start()
        {
            _provider.OnStart();
        }

        [Button]
        public void FindObjectsNeedInject()
        {
            _objectsToInject = _container.FindObjectsWithInjectAttribute();
        }

        public void Setup()
        {
            _provider = gameObject.AddComponent<Provider>();
            _provider.Init(_container);
            
            RegisterUI();
            
            _container.InjectOnScene(_objectsToInject);
            
            RegisterPlayer();

            
            //show example of register interface to concrete object
            //IoCContainer.Register<IInventorySystem, InventorySystem>();
        }
        
        void RegisterUI()
        {
            _provider.Setup(_objectName);
            _provider.Setup(_pointImage);
            _provider.Setup(_inventorySystem);
            _provider.Setup(_itemInfoPanel);
            _provider.Setup(_inventoryUI);
        }
        void RegisterPlayer()
        {
            Player player = _provider.ProvideFactory(_playerPrefab, _playerSpawnPosition.position);
            PlayerInput playerInput = player.PlayerInput;
            CheckInteractable playerInteractable = playerInput.CheckInteractable;
            
            _provider.Setup(player);
            _provider.Setup(playerInput);
            _provider.Setup(playerInteractable);
        }
    }
}
