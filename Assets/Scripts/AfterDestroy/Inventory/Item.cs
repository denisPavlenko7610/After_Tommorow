using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AfterDestroy.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] string itemName;
        public int Id { get; set; }
        public AssetReferenceSprite icon;
        [SerializeField] int countItem;
        [SerializeField] bool isStackable;
        [SerializeField] string descriptionItem;
        [SerializeField] AssetReference prefab;
    }
}