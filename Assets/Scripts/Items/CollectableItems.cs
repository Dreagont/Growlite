using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CollectableItems : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null )
        {
            Item item = GetComponent<Item>();   
            if (item != null)
            {
                player.inventory.AddItemInventory(item);
                Destroy(gameObject);
            }
            
        }
    }
}
