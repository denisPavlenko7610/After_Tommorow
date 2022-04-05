using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class Item : MonoBehaviour
    {
        [SerializeField] string itemName;
        public int Id { get; set; }
        [SerializeField] int countItem;
        [SerializeField] bool isStackable;
        [SerializeField] string descriptionItem;
        [SerializeField] string iconPath;
        [SerializeField] string prefabPath;
    }
}