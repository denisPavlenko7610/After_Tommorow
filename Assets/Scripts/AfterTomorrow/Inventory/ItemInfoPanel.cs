using System;
using TMPro;
using UnityEngine;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] GameObject _itemInfoPanel;
    [SerializeField] TextMeshProUGUI _objectName;
    [SerializeField] TextMeshProUGUI _description;
    [SerializeField] TextMeshProUGUI _weight;

    private void Start()
    {
        HidePanel();
    }

    public void ShowPanel(string objectName, string description, float weight)
    {
        _itemInfoPanel.SetActive(true);
        _objectName.text = objectName;
        _description.text = description;
        _weight.text = $"{weight:f2}";
    }

    public void HidePanel()
    {
        _itemInfoPanel.SetActive(false);
    }
}