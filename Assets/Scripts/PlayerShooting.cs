using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerShooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Light2D flashMuz;
    private Transform cameraPos;
    public float shakeDuration = 0.2f;
    public float shakeMag = 0.1f;
    public GameObject bullet;
    private Transform ogCamPos;
    public Transform firePoint;
    public float fireForce = 10f;
    public float fireRate = 0.5f;
    public int maxAmmo = 10;
    public int currentAmmo;
    public int weapon;
    public bool canFire = true;
    private float fireCooldown;

    void Start()
    {
        flashMuz.enabled = false;
        cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
        currentAmmo = maxAmmo;
        canFire = true;
        fireCooldown = 0f;
    }

    void Update()
    {
        switch(weapon)
        {
            case 1:
                fireRate = 0.5f;//pistol
                canFire = true;
                break;
            case 2:
                fireRate = 0.15f;//machine
                canFire = true;
                break;
            default://idle
                canFire = false;
                break;
        }
        
        fireCooldown -= Time.deltaTime;

        if(Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0 && canFire && !FindFirstObjectByType<DisplayInventory>().alreadyOn)
        {
            Shoot();    
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = (mousePos - (Vector2)firePoint.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        muzzleFlash.Play();
        StartCoroutine(Flash());
        
        FindObjectOfType<AudioManager>().Play("Shoot");
        Shake();
        /*
        GameObject temp = Instantiate(bullet,firePoint.position, firePoint.rotation);
        temp.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse); */
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePos - firePoint.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, shootDirection);
        if(hit.collider != null)
        {
        if(hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<Zombie>().enemyHealth--;
            hit.collider.GetComponent<Zombie>().ChasePlayer();
        }
        }
        currentAmmo--;
        fireCooldown = fireRate;
    }

    IEnumerator Flash()
    {
        flashMuz.enabled = true;
        yield return new WaitForSeconds(0.1f);
        flashMuz.enabled = false;
    }

    void Shake()
    {
        ogCamPos = cameraPos.transform;
        StartCoroutine(Shakity());
    }

    IEnumerator Shakity()
    {
        float elapsed = 0f;
        while(elapsed <= shakeDuration)
        {
            float offX = Random.Range(-1f,1f) * shakeMag;
            float offY = Random.Range(-1f,1f) * shakeMag;
            cameraPos.localPosition = new Vector3(cameraPos.position.x + offX, cameraPos.position.y + offY, -10);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraPos = ogCamPos;
    }
}
