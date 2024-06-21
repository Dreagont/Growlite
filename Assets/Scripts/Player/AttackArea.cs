using System.Collections;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float damageAmount;
    private Player player;
    private PlayerController playerController;


    private void Start()
    {
        player = GetComponentInParent<Player>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (player != null)
        {
            damageAmount = player.Damage;
        }
        else
        {
            Debug.LogError("PlayerController not found in parent objects.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerController.isShootting)
        {
            Debug.Log("Trigger Entered by: " + collision.name);
            if (collision.GetComponent<DamageAbleEnemy>() != null)
            {
                DamageAbleEnemy enemy = collision.GetComponent<DamageAbleEnemy>();
                enemy.TakeDamage(damageAmount);
            }
            if (collision.GetComponent<Minerals>() != null)
            {
                Minerals minerals = collision.GetComponent<Minerals>();
                minerals.Hit(1);
            }
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

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    public void ShootArrow()
    {
        Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation);
    }
}
