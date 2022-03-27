using UnityEngine;

namespace AfterDestroy.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/ItemSO", fileName = "Item")]
    public class ItemData : ScriptableObject
    {
        public string displayName;
        public Sprite icon;
    }
}
