using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class UIInventoryItemSlot : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _slotName;
        [SerializeField] TextMeshProUGUI _stackSize;
        [SerializeField] TextMeshProUGUI _weight;
        [SerializeField] Image _icon;
        [SerializeField] Transform _stackObject;

        public void Set(InventoryItem item)
        {
            _slotName.text = item.Data.name;
            _icon.enabled = true;
            _icon.sprite = item.Data.Icon;
            _weight.text = $"{item.Weight:f2}kg";

            if (item.StackSize < 1)
            {
                _stackObject.gameObject.SetActive(false);
                return;
            }

            _stackSize.text = item.StackSize.ToString();
        }
    }
}