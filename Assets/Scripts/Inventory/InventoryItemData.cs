using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item", fileName = "Item")]
    public class InventoryItemData : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string ItemName { get; private set; }

        [field: SerializeField, TextArea(4, 4)]
        public string DescriptionItem { get; private set; }

        [field: SerializeField] public float Weight { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; } = true;
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}