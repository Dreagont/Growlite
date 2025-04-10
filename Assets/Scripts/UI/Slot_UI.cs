using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    public Image itemIcon;

    public TextMeshProUGUI itemQuantityText;

    public int slotId;

    public Inventory inventory;

    [SerializeField] private GameObject highLightItem;
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            itemQuantityText.text = slot.currentCount.ToString();
        }
    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        itemQuantityText.text = "";
    }

    public void SetHightLight(bool hightLight)
    {
        highLightItem.SetActive(hightLight); 
    }
}
