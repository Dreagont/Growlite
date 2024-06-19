using System.Collections;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float damageAmount;
    private Player playerController;


    private void Start()
    {
        playerController = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (playerController != null)
        {
            damageAmount = playerController.Damage;
        }
        else
        {
            Debug.LogError("PlayerController not found in parent objects.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered by: " + collision.name);
        if (collision.GetComponent<DamageAbleEnemy>() != null )
        {
            DamageAbleEnemy enemy = collision.GetComponent<DamageAbleEnemy>();
            enemy.TakeDamage(damageAmount);
        }
        if (collision.GetComponent<Minerals>() != null )
        {
            Minerals minerals = collision.GetComponent<Minerals>();
            minerals.Hit(1);
        }
    }

    public void SetFacingDirection(int direction)
    {
        switch (direction)
        {
            case 1: // Right
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2: // Left
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3: // Up
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 4: // Down
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }
}
