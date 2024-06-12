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

    public void DropItem(CollectableItems item)
    {
        Vector2 dropLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 1.5f;

        Instantiate(item, dropLocation + spawnOffset, Quaternion.identity);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
