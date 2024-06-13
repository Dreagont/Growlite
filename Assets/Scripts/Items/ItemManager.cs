using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    private Dictionary<string, Item> ItemDict= new Dictionary<string, Item>();

    private void Awake()
    {
        foreach (Item item in items)
        {
            AddItem(item);
        }
    }
    private void AddItem(Item item)
    {
        if (!ItemDict.ContainsKey(item.itemData.itemName))
        {
            ItemDict.Add(item.itemData.itemName, item);
        }
    }

    public Item GetItemByName(string name)
    {
        if (ItemDict.ContainsKey(name)) {
            return ItemDict[name];
        }
        return null;
    }
}
