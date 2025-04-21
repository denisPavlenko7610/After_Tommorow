using AfterDestroy.Inventory;
using Inventory;
using RDDependency;
using RDTools;
using UnityEngine;

namespace AfterDestroy.Interactable
{
    public class InventoryObject : MonoBehaviour, IInteractable
    {
        [SerializeField] InventoryItemData _itemData;
        [SerializeField] Rigidbody _rigidbody;

        ItemInfoPanel _itemInfoPanel;
        InventorySystem _inventorySystem;
        bool _canInteract;
        const string GroundTag = "Ground";

        [Inject]
        void Construct(InventorySystem inventorySystem, ItemInfoPanel itemInfoPanel)
        {
            _inventorySystem = inventorySystem;
            _itemInfoPanel = itemInfoPanel;
        }

        void Update()
        {
            if (_canInteract)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public void OnPickupItem()
        {
            _inventorySystem.Add(_itemData);
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(GroundTag))
                _rigidbody.isKinematic = true;
        }

        public void Interact()
        {
            _canInteract = true;
            _itemInfoPanel.ShowPanel(_itemData.ItemName, _itemData.DescriptionItem, _itemData.Weight);
        }

        public void SetParent(Transform transform) => gameObject.transform.parent = transform;

        public void SetPosition(Transform transform) => gameObject.transform.position = transform.position;

        public void DisableCanvas() => _itemInfoPanel.HidePanel();

        public void ThrowObject() => _rigidbody.isKinematic = false;

        public string GetObjectName() => _itemData.ItemName;

        public void Destroy() => Destroy(gameObject);
    }
}