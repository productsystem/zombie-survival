using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Bullet : MonoBehaviour
{
    public float life = 2f;

    void Start()
    {
        Destroy(gameObject,life);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Zombie>().enemyHealth--;
        }
        Destroy(gameObject);
    }
}
