using System;

namespace AfterDestroy.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int stackSize;

        public InventoryItem(ItemData item)
        {
            itemData = item;
            AddToStack();
        }

        public void AddToStack()
        {
            stackSize++;
        }

        public void RemoveFromStack()
        {
            stackSize--;
        }
    }
}
