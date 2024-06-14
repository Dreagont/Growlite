using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("ToolBar")]
    public Inventory toolBar;
    public int toolBarSlotCount;

    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    private void Awake()
    {
        backpack = new Inventory(backpackSlotCount);
        toolBar = new Inventory(toolBarSlotCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("ToolBar", toolBar);
    }

    public Inventory GetInventoryByName(string name)
    {
        if (inventoryByName.ContainsKey(name)) return inventoryByName[name];
        return null;
    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].AddItemInventory(item);
        }
    }
}
