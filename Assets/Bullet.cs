using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float life = 2f;

    void Start()
    {
        Destroy(gameObject,life);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
