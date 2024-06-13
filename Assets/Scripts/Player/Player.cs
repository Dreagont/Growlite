using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(10);
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
