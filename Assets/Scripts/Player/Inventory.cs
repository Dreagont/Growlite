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

        public Slot(int currentCount, int maxCount, int slotIndex, string itemName, Sprite icon)
        {
            this.currentCount = currentCount;
            this.maxCount = maxCount;
            this.slotIndex = slotIndex;
            this.itemName = itemName;
            this.icon = icon;
        }

        public Slot()
        {
            itemName = "";

            currentCount = 0;

            maxCount = 100;
        }

        public bool IsEmpty()
        {
           
                if (itemName == "" && currentCount ==0)
                {
                    return true;
                }
                return false;
            
        }

        public bool AddAble(string itemName)
        {
            if (this.itemName == itemName && currentCount < maxCount)
            {
                return true ;
            }
            return false ;
        }

        public void AddItemSlot(Item item)
        {
            this.itemName = item.itemData.itemName;
            this.icon = item.itemData.icon;
            currentCount++;
        }

        public void AddItemSlot(string itemName, Sprite icon, int maxCount)
        {
            this.itemName = itemName;
            this.icon = icon;
            currentCount++;
            this.maxCount = maxCount;
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

    public Slot selectedSlot = null;
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
            if (slot.itemName == item.itemData.itemName && slot.AddAble(item.itemData.itemName))
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

    public void MoveSlot(int from, int to, Inventory toInventory)
    {
        Slot fromSlot = slots[from];
        Slot toSlot = toInventory.slots[to];

        if (toSlot.IsEmpty() || toSlot.AddAble(fromSlot.itemName)) 
        {
            toSlot.AddItemSlot(fromSlot.itemName, fromSlot.icon, fromSlot.maxCount);
            fromSlot.RemoveItemSlot();
        }
    }

    public void SelectSlot(int index)
    {
        if (slots !=  null && slots.Count > 0)
        {
            selectedSlot = slots[index];
        }
    }
}
