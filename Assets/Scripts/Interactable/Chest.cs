using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private bool isOpen;

    public void Interact()
    {
        if (isOpen == false)
        {
            isOpen = true;
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
        }
        else
        {
            isOpen = false;
            chestClosed.SetActive(true);
            chestOpen.SetActive(false);
        }

        Debug.Log("Chest opened!");
    }
}
