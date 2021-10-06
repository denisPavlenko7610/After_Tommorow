using System;
using UnityEngine;

namespace AfterDestroy.Inventory
{
    public interface IInventoryItem
    {
        string Name { get; }
        Sprite Image { get; }
        void OnPickup();
    }

    public class InventoryEventArgs : EventArgs
    {
        public InventoryEventArgs(IInventoryItem item)
        {
            Item = item;
        }

        public IInventoryItem Item;
    }
}
