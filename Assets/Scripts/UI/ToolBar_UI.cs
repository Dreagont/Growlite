using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolBarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private void Start()
    {
        selectSlot(0);
    }

    private void Update()
    {
        CheckNumKey();
    }

    public void selectSlot(int index)
    {
        if (toolBarSlots.Count == 10)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHightLight(false);
            }
            selectedSlot = toolBarSlots[index];
            selectedSlot.SetHightLight(true);

            GameManager.Instance.player.inventory.toolBar.SelectSlot(index);
        }
    }
    private void CheckNumKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectSlot(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { selectSlot(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { selectSlot(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { selectSlot(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { selectSlot(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { selectSlot(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { selectSlot(6); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { selectSlot(7); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { selectSlot(8); }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { selectSlot(9); }
    }
}
