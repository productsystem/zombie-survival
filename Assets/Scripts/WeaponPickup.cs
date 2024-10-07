using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNumber;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().weaponNum = weaponNumber;
            other.GetComponent<PlayerShooting>().weapon = weaponNumber;
            Destroy(gameObject);
        }
    }
}
