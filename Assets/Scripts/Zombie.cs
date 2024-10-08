using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform player;
    private GameObject playerObj;
    public int enemyHealth = 3;
    public float normalRange = 5f;
    public float chaseRangeMultiplier = 2f;
    public float moveSpeed = 100f;

    private float chaseRange;
    private Rigidbody2D rb;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        chaseRange = normalRange;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            chaseRange = normalRange;
        }
    }

    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChasePlayer()
    {
        if (chaseRange == normalRange)
        {
            chaseRange *= chaseRangeMultiplier;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            playerObj.GetComponent<PlayerController>().Damage(1);
        }
    }
}
