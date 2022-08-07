using Dythervin.AutoAttach;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class UIInventoryItemSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _slotName;
        [SerializeField] private TextMeshProUGUI _stackSize;
        [SerializeField] private Image _icon;
        [SerializeField, Attach] private Transform _stackObject;

        public void Set(InventoryItem item)
        {
            _slotName.text = item.Data.name;
            _icon.sprite = item.Data.Icon;

            if (item.StackSize <=1)
            {
                _stackObject.gameObject.SetActive(false);
                return;
            }

            _stackSize.text = item.StackSize.ToString();
        }
    }
}