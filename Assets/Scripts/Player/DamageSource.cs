using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();

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
        DamageAbleEnemy damageAbleEnemy = collision.gameObject.GetComponent<DamageAbleEnemy>();
        if (damageAbleEnemy != null)
        {
            damageAbleEnemy.TakeDamage(damageAmount);
        }
    }
}
