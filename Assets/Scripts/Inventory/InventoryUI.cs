using AfterDestroy.Inventory;
using UnityEngine;
using Zenject;

namespace Inventory 
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private GameObject _slotPrefab;
        
        InventorySystem _inventorySystem;
        bool _isInventoryOpened;
        int _emptySlotsCount = 48;

        [Inject]
        public void Construct(InventorySystem inventorySystem) => _inventorySystem = inventorySystem;

        public void ChangeInventoryVisibility()
        {
            if (_isInventoryOpened == false)
            {
                _inventoryPanel.gameObject.SetActive(true);
                UpdateInventory();
                _isInventoryOpened = true;
            }
            else
            {
                _isInventoryOpened = false;
                _inventoryPanel.gameObject.SetActive(false);
            }
        }

        public void DrawInventory()
        {
            var slotsAdded = 0;
            foreach (var item in _inventorySystem.Inventory)
            {
                AddInventorySlot(item);
                slotsAdded++;
            }

            var slotsToAdd = _emptySlotsCount - slotsAdded;
            for (int i = 0; i < slotsToAdd; i++)
            {
                AddEmptySlots();
            }
        }

        public void AddInventorySlot(InventoryItem item)
        {
            var obj = Instantiate(_slotPrefab, _inventoryContent, false);
            var slot = obj.GetComponent<UIInventoryItemSlot>();
            slot.Set(item);
        }

        void UpdateInventory()
        {
            foreach (Transform t in _inventoryContent)
                Destroy(t.gameObject);

            DrawInventory();
        }

        void AddEmptySlots()
        {
            Instantiate(_slotPrefab, _inventoryContent, false);
        }
    }
}