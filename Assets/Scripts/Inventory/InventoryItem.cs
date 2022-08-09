using System;

namespace Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItemData Data { get; private set; }
        public int StackSize { get; private set; }
        public float Weight { get; private set; }

        public InventoryItem(InventoryItemData data)
        {
            Data = data;
            AddToStack(data);
        }

        public void AddToStack(InventoryItemData data)
        {
            Weight += data.Weight;
            StackSize++;
        }

        public void RemoveFromStack(InventoryItemData data)
        {
            Weight -= data.Weight;
            StackSize--;
        }
    }
}