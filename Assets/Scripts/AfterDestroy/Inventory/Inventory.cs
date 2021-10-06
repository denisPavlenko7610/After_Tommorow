using System;
using System.Collections.Generic;
using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class Inventory : MonoBehaviour
    {
        private const int SLOTS = 10;

        private List<IInventoryItem> items = new List<IInventoryItem>();

        public event EventHandler<InventoryEventArgs> ItemAdded;

        public void AddItem(IInventoryItem item)
        {
            if (items.Count < SLOTS)
            {
                items.Add(item);
                item.OnPickup();
            }

            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventArgs(item));
            }
        }
    }
}
