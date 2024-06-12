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
        public CollectableType collectableType;

        public Sprite icon;

        public Slot()
        {
            collectableType = CollectableType.NONE;

            currentCount = 0;

            maxCount = 100;
        }

        public bool AddAble()
        {
            return currentCount < maxCount; 
        }

        public void AddItemSlot(CollectableItems item)
        {
            this.collectableType = item.type;
            this.icon = item.icon;
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
                this.collectableType = CollectableType.NONE;
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

    public void AddItemInventory(CollectableItems item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.collectableType == item.type && slot.AddAble())
            {
                slot.AddItemSlot(item);
                return;
            }

            if (slot.collectableType == CollectableType.NONE)
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

}
