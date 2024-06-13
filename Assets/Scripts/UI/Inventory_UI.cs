using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public GameObject playerEquipment;

    public GameObject playerStats;

    public GameObject inventoryOuter;

    public Player player;

    PlayerController playerController;

    public List<Slot_UI> slots = new List<Slot_UI>();

    private Slot_UI draggedSlot;

    private Image draggedImage;

    [SerializeField] private Canvas Canvas;

    private bool dropAll = false;

    private void Awake()
    {
        Canvas = FindAnyObjectByType<Canvas>();
  
    }
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

        if (Input.GetKey(KeyCode.Q))
        {
            dropAll = true;
        } else
        {
            dropAll = false;
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            playerController.canAction = false;
            inventoryOuter.SetActive(true);
            playerEquipment.SetActive(true);
            playerStats.SetActive(true);
            Refresh();
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

    public void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
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

    public void Remove()
    {
        Item dropItem = GameManager.Instance.ItemManager.GetItemByName(player.inventory.slots[draggedSlot.slotId].itemName);

        if (dropItem != null)
        {
            if (dropAll)
            {
                player.DropItem(dropItem, player.inventory.slots[draggedSlot.slotId].currentCount);
                player.inventory.RemoveItemInventory(draggedSlot.slotId, player.inventory.slots[draggedSlot.slotId].currentCount);
                draggedSlot = null;
                Refresh();
            } else
            {
                player.DropItem(dropItem);
                player.inventory.RemoveItemInventory(draggedSlot.slotId);
                draggedSlot = null;
                Refresh();
            }

        }
        Debug.Log("hehe");

    }

    public void BeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedImage = Instantiate(draggedSlot.itemIcon);
        draggedImage.raycastTarget = false;
        draggedImage.rectTransform.sizeDelta = new Vector2(50, 50);
        draggedImage.transform.SetParent(Canvas.transform);

        MoveToMousePosition(draggedImage.gameObject);
    }

    public void Drag()
    {
        MoveToMousePosition(draggedImage.gameObject);
    }


    public void EndDrag()
    {
        Destroy(draggedImage.gameObject);
        draggedImage = null;
    }

    public void Drop(Slot_UI slot)
    {
        Debug.Log("Drop" + draggedSlot.name + "on" + slot.name);
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
}
