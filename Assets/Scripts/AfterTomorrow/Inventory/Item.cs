using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class Item : MonoBehaviour
    {
        public InventoryItem item;
        [HideInInspector]
        public int Id;
    }
}