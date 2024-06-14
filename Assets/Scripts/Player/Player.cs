using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public InventoryManager inventory;

    private TileManager tileManager;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    public void DropItem(Item item)
    {
        Vector2 dropLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 1.5f;

        Instantiate(item, dropLocation + spawnOffset, Quaternion.identity);
    }

    public void DropItem(Item item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            DropItem(item);
        }
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
