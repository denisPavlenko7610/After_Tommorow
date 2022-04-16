using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "Item")]
public class InventoryItem : ScriptableObject
{
    public int Id;
    public string ItemName;
    [TextArea] public string descriptionItem;
    public float Weight;
    public int Count;
    public bool IsStackable = true;
    public AssetReferenceSprite icon;
    [SerializeField] AssetReference prefab;
}