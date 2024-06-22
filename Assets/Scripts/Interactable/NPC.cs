using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string NPCName;
    [SerializeField] private GameObject ShopUI;
    [SerializeField] private GameObject PlayerInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShopUI.SetActive(false);
            PlayerInventory.SetActive(false);
        }
    }

    public void Interact()
    {
        if (NPCName == "Azalea")
        {
            ShopUI.SetActive(true);
            PlayerInventory.SetActive(true);
        }

        Debug.Log("NPC: " + NPCName);
    }
}
