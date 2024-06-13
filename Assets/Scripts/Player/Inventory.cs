using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public int currentCount;
        public int maxCount;
        public int slotIndex;
        public string itemName;

        public Sprite icon;

        public Slot()
        {
            itemName = "";

            currentCount = 0;

            maxCount = 100;
        }

        public bool AddAble()
        {
            return currentCount < maxCount; 
        }

        public void AddItemSlot(Item item)
        {
            this.itemName = item.itemData.itemName;
            this.icon = item.itemData.icon;
            currentCount++;
        }

        public void RemoveItemSlot()
        {
            if (currentCount > 0 )
            {
                currentCount--;
            }

            if (currentCount == 0 )
            {
                this.icon = null;
                this.itemName = "";
            }
        }
    }  

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlot)
    {
        for (int i = 0; i < numSlot; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void AddItemInventory(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == item.itemData.itemName && slot.AddAble())
            {
                slot.AddItemSlot(item);
                return;
            }

            if (slot.itemName == "")
            {
                slot.AddItemSlot(item);
                return;
            }
        }
    }

    public void RemoveItemInventory(int index)
    {
        slots[index].RemoveItemSlot();
    }
    public void RemoveItemInventory(int index, int quantity)
    {
        for (int i = 0; i< quantity; i++)
        {
            RemoveItemInventory(index);
        }
    }
}
