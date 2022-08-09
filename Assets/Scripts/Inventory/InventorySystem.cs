using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class InventorySystem : MonoBehaviour
    {

        public List<InventoryItem> Inventory { get; } = new();
        Dictionary<InventoryItemData, InventoryItem> inventoryDictionary = new();

        public void Add(InventoryItemData referenceData)
        {
            if (inventoryDictionary.TryGetValue(referenceData, out InventoryItem value))
            {
                value.AddToStack(referenceData);
            }
            else
            {
                InventoryItem newItem = new InventoryItem(referenceData);
                Inventory.Add(newItem);
                inventoryDictionary.Add(referenceData, newItem);
            }
        }

        public void Remove(InventoryItemData referenceData)
        {
            if (inventoryDictionary.TryGetValue(referenceData, out InventoryItem value))
            {
                value.RemoveFromStack(referenceData);

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