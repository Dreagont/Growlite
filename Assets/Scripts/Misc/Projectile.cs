using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private bool isEnemyProjectile = false;
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemyProjectile)
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.TakeDamage(10);
                Destroy(gameObject);
            }
        } else
        {
            DamageAbleEnemy enemy = other.GetComponent<DamageAbleEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
            Destroy(gameObject); 
        }
        }

        
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
