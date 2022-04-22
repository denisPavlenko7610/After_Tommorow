using System.Collections.Generic;
using AfterDestroy.Interactable;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AfterDestroy.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] GameObject cellContainer;
        [SerializeField] KeyCode showInventory = KeyCode.Tab;
        public List<Item> Items { get; set; } = new List<Item>();

        void Start()
        {
            AddEmptyItems();
            HideInventory();
        }

        void AddEmptyItems()
        {
            for (int i = 0; i < cellContainer.transform.childCount; i++)
            {
                var newItem = gameObject.AddComponent<Item>();
                newItem.Id = i;
                Items.Add(newItem);
            }
        }

        public async UniTaskVoid DisplayItem(Item item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Id == 0)
                {
                    Items[i].item = item.item;
                    var cell = cellContainer.transform.GetChild(i);
                    var icon = cell.GetChild(0);
                    var count = icon.GetChild(0).GetComponent<TextMeshProUGUI>();
                    count.text = item.item.Count.ToString();
                    var image = icon.GetComponent<Image>();
                    image.enabled = true;
                    var sprite = await Items[i].item.icon.LoadAssetAsync<Sprite>();
                    if (sprite == null)
                        continue;

                    image.sprite = sprite;
                    break;
                }
            }
        }

        public void SwitchInventoryStatus()
        {
            if (cellContainer.activeSelf)
            {
                HideInventory();
            }
            else
            {
                ShowInventory();
            }
        }

        void ShowInventory() => cellContainer.gameObject.SetActive(true);

        void HideInventory() => cellContainer.gameObject.SetActive(false);
    }
}