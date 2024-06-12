using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public GameObject toolBar;

    public GameObject playerEquipment;

    public GameObject playerStats;


    public Player player;

    PlayerController playerController;

    public List<Slot_UI> slots = new List<Slot_UI>();
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            playerController.canAttack = false;
            toolBar.SetActive(false);
            playerEquipment.SetActive(true);
            playerStats.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
            playerController.canAttack = true;
            playerEquipment.SetActive(false);
            playerStats.SetActive(false);
            toolBar.SetActive(true);
        }
    }

    public void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count) {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].collectableType != CollectableType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int index)
    {
        CollectableItems dropItem = GameManager.Instance.ItemManager.GetItemByType(player.inventory.slots[index].collectableType);

        if ( dropItem != null)
        {
            player.DropItem(dropItem);
            player.inventory.RemoveItemInventory(index);
            Refresh();
        }
        
    }
}
