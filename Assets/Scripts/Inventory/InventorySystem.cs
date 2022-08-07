using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class InventorySystem : MonoBehaviour
    {

        public List<InventoryItem> Inventory { get; private set; } = new();
        private Dictionary<InventoryItemData, InventoryItem> inventoryDictionary = new();

        public void Add(InventoryItemData referenceData)
        {
            if (inventoryDictionary.TryGetValue(referenceData, out InventoryItem value))
            {
                value.AddToStack();
            }
            else
            {
                InventoryItem newHelper = new InventoryItem(referenceData);
                Inventory.Add(newHelper);
                inventoryDictionary.Add(referenceData, newHelper);
            }
        }

        public void Remove(InventoryItemData referenceData)
        {
            if (inventoryDictionary.TryGetValue(referenceData, out InventoryItem value))
            {
                value.RemoveFromStack();

                if (value.StackSize == 0)
                {
                    Inventory.Remove(value);
                    inventoryDictionary.Remove(referenceData);
                }
            }
        }

        public InventoryItem Get(InventoryItemData referenceData)
        {
            if (inventoryDictionary.TryGetValue(referenceData, out InventoryItem value))
            {
                return value;
            }

            return null;
        }
    }
}