using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    public CollectableType type;

    public int QUANTITY = 1;

    public Sprite icon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null )
        {
            player.inventory.AddItemInventory(this);
            Destroy(gameObject);
        }
    }
}

public enum CollectableType
{
    NONE, CARROT_SEED, CORN_SEED
}
