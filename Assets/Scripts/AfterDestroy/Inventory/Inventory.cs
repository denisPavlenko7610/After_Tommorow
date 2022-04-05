using System;
using System.Collections.Generic;
using UnityEngine;

namespace AfterDestroy.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] GameObject cellContainer;
        [SerializeField] private KeyCode showInventory = KeyCode.Tab;
        public List<Item> Items { get; set; } = new List<Item>();

        private void Start()
        {
            for (int i = 0; i < cellContainer.transform.childCount; i++)
            {
                Items.Add(new Item());
            }

            HideInventory();
        }

        private void Update()
        {
            SwitchInventoryStatus();
        }

        private void SwitchInventoryStatus()
        {
            if (Input.GetKeyDown(showInventory))
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
        }

        private void ShowInventory()
        {
            cellContainer.gameObject.SetActive(true);
        }

        private void HideInventory()
        {
            cellContainer.gameObject.SetActive(false);
        }
    }
}