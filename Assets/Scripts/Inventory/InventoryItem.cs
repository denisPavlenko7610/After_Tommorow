using System;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItemData Data { get; private set; }
        public int StackSize { get; private set; }

        public InventoryItem(InventoryItemData data)
        {
            Data = data;
            AddToStack();
        }

        public void AddToStack()
        {
            StackSize++;
        }

        public void RemoveFromStack()
        {
            StackSize--;
        }
    }
}