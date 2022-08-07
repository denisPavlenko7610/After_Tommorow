using System;
using AfterDestroy.Inventory;
using UnityEngine;

namespace Inventory 
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] KeyCode _showInventory = KeyCode.Tab;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private GameObject slotPrefab;
        private InventorySystem _inventorySystem;
        private bool _isInventoryOpened;

        public void ChangeInventoryVisibility()
        {
            if (_isInventoryOpened == false)
            {
                _inventoryPanel.gameObject.SetActive(true);
                OnUpdateInventory();
                _isInventoryOpened = true;
            }
            else
            {
                _inventoryPanel.gameObject.SetActive(false);
            }
        }

        private void OnUpdateInventory()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            DrawInventory();
        }

        public void DrawInventory()
        {
            foreach (var item in _inventorySystem.Inventory)
            {
                AddInventorySlot(item);
            }
        }

        public void AddInventorySlot(InventoryItem item)
        {
            var obj = Instantiate(slotPrefab);
            obj.transform.SetParent(transform,false);
            UIInventoryItemSlot slot = obj.GetComponent<UIInventoryItemSlot>();
            slot.Set(item);
        }
    }
}