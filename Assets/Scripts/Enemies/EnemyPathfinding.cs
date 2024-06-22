using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float avoidObstacleDistance = 1f; // Distance to move away from obstacle

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void MoveToPlayer(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        moveDir = direction;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Calculate new roaming position to avoid the obstacle
            Vector2 newRoamPosition = GetAvoidObstaclePosition(collision.contacts[0].normal);
            MoveTo(newRoamPosition);
        }
    }

    private Vector2 GetAvoidObstaclePosition(Vector2 obstacleNormal)
    {
        // Calculate a new roaming position to avoid the obstacle
        Vector2 avoidDirection = Vector2.Reflect(moveDir, obstacleNormal).normalized;
        Vector2 newRoamPosition = (Vector2)transform.position + avoidDirection * avoidObstacleDistance;
        return newRoamPosition;
    }
}
