using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    private GameObject playerObj;
    public int enemyHealth = 3;
    public float normalRange = 5f;
    public float chaseRangeMultiplier = 2f;
    public float moveSpeed = 100f;

    private bool isChasing;

    private float chaseRange;
    private Rigidbody2D rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        playerObj = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        chaseRange = normalRange;
        isChasing = false;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseRange)
        {
            if(!isChasing)
            {
                if(Random.Range(0f,1f) <= 0.5f)
                FindObjectOfType<AudioManager>().Play("Moan1");
                else
                FindObjectOfType<AudioManager>().Play("Moan2");
                isChasing = true;
            }
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            chaseRange = normalRange;
        }
    }

    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (player != null)
        {
            Vector3 direction = agent.velocity;

            if (direction.sqrMagnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
        /*
        if (chaseRange == normalRange)
        {
            chaseRange *= chaseRangeMultiplier;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle; */
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            playerObj.GetComponent<PlayerController>().Damage(1);
        }
    }
}
