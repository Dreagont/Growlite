using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();

    public List<Inventory_UI> inventory_UIs = new List<Inventory_UI>();

    public static Slot_UI draggedSlot;

    public static Image draggedIcon;

    public static bool dropAll;

    public GameObject inventoryPanel;
    public GameObject inventoryOuter;
    public GameObject playerEquipment;
    public GameObject playerStats;
    PlayerController playerController;


    private void Awake()
    {
        Initialize();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            dropAll = true;
        }
        else
        {
            dropAll = false;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null && inventoryOuter != null && playerEquipment != null && playerStats != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                playerController.canAction = false;
                inventoryOuter.SetActive(true);
                playerEquipment.SetActive(true);
                playerStats.SetActive(true);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
                playerController.canAction = true;
                inventoryOuter.SetActive(false);
                playerEquipment.SetActive(false);
                playerStats.SetActive(false);
            }
        }

    }

    public void RefreshAll()
    {
        foreach(KeyValuePair<string,Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        } 
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }
    public Inventory_UI GetInventory_UI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName)) return inventoryUIByName[inventoryName];
        Debug.Log("no");
        return null;
    }

    void Initialize()
    {
        foreach(Inventory_UI ui in inventory_UIs)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            inventoryUIByName.Add(ui.inventoryName, ui);
        }
    }
}
