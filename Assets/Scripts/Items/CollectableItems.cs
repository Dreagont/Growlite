using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CollectableItems : MonoBehaviour
{
    [SerializeField] PlayerController player;  
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pickupDistance = 1.5f;
    //[SerializeField] private float ttl = 10f;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    private void Update()
    {
        if (player == null) return;  

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > pickupDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (distance < 0.1f)
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                player.inventory.Add("ToolBar", item);
                GameManager.Instance.UIManager.RefreshInventoryUI("ToolBar");
                Destroy(gameObject);  
            }
        }
    }

    // Optional: Handle OnTriggerEnter2D if you want immediate pickup on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player player = collision.GetComponent<Player>();

        //if (player != null)
        //{
        //    Item item = GetComponent<Item>();
        //    if (item != null)
        //    {
        //        player.inventory.Add("ToolBar", item);
        //        Destroy(gameObject);
        //    }
        //}
    }
}
