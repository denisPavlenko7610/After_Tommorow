using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "Item")]
public class InventoryItem : ScriptableObject
{
    public int Id { get; }
    [SerializeField] string _itemName;
    [SerializeField] [TextArea(4,4)] string _descriptionItem;
    [SerializeField] float _weight;
    [SerializeField] int _count;
    [SerializeField] bool _isStackable = true;
    [SerializeField] AssetReferenceSprite _icon;
    [SerializeField] AssetReference _prefab;

    public string ItemName => _itemName;
    public string DescriptionItem => _descriptionItem;
    public float Weight => _weight;
    public int Count => _count;
    public bool IsStackable => _isStackable;
    public AssetReferenceSprite Icon => _icon;
    public AssetReference Prefab => _prefab;
}