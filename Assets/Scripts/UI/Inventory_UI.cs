using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public PlayerController player;

    public string inventoryName;

    private Inventory inventory;

    public List<Slot_UI> slots = new List<Slot_UI>();


    [SerializeField] private Canvas Canvas;

    private void Awake()
    {
        Canvas = FindAnyObjectByType<Canvas>();
  
    }
    void Start()
    {
        inventory = GameManager.Instance.player.inventory.GetInventoryByName(inventoryName);
        SetupSlots();
        Refresh();
    }

    void Update()
    {
        
    }

    

    public void Refresh()
    {
        if (slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        } 
    }

    public void Remove()
    {
        Item dropItem = GameManager.Instance.ItemManager.GetItemByName(inventory.slots[UIManager.draggedSlot.slotId].itemName);

        if (dropItem != null)
        {
            if (UIManager.dropAll)
            {
                GameManager.Instance.player.DropItem(dropItem, inventory.slots[UIManager.draggedSlot.slotId].currentCount);
                inventory.RemoveItemInventory(UIManager.draggedSlot.slotId, inventory.slots[UIManager.draggedSlot.slotId].currentCount);
                UIManager.draggedSlot = null;
                Refresh();
            } else
            {
                GameManager.Instance.player.DropItem(dropItem);
                inventory.RemoveItemInventory(UIManager.draggedSlot.slotId);
                UIManager.draggedSlot = null;
                Refresh();
            }

        }
        Debug.Log("hehe");

    }

    public void BeginDrag(Slot_UI slot)
    {
        UIManager.draggedSlot = slot;
        UIManager.draggedIcon = Instantiate(UIManager.draggedSlot.itemIcon);
        UIManager.draggedIcon.raycastTarget = false;
        UIManager.draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        UIManager.draggedIcon.transform.SetParent(Canvas.transform);

        MoveToMousePosition(UIManager.draggedIcon.gameObject);
    }

    public void Drag()
    {
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
    }


    public void EndDrag()
    {
        Destroy(UIManager.draggedIcon.gameObject);
        UIManager.draggedIcon = null;
    }

    public void Drop(Slot_UI slot)
    {
        UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotId, slot.slotId, slot.inventory);
        GameManager.Instance.UIManager.RefreshAll();
        Debug.Log("Drop" + UIManager.draggedSlot.name + "on" + slot.name);
    }

    private void MoveToMousePosition(GameObject gameObject)
    {
        if (Canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform
                , Input.mousePosition, null, out position);

            gameObject.transform.position = Canvas.transform.TransformPoint(position);
        }
    }

    private void SetupSlots()
    {
        int counter = 0;

        foreach (Slot_UI slot in slots)
        {
            slot.slotId = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
