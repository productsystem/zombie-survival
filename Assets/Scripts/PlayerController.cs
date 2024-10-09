using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InventoryObject inventory;
    public float moveSpeed = 5f;
    public float stepIn = 0.424f;
    public float invincibilityTime = 1f;
    public bool canMove;
    private Color ogcolor;
    public Image healthbar;
    public Sprite[] sprites;
    public int weaponNum;
    public int playerHealth;
    private Rigidbody2D rb;
    private float stepTime;

    private Vector2 movement; 
    private Vector2 mousePos;
    private SpriteRenderer spriteRenderer;

    public Camera cam;
    private bool isInvincible = false;

    void Start()
    {
        stepTime = stepIn;
        canMove = true;
        gameObject.SetActive(true);
        playerHealth = 10;
        weaponNum = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        rb = GetComponent<Rigidbody2D>();
        ogcolor = spriteRenderer.color;
        isInvincible = false;
    }

    public void Damage(int damage)
    {
        if(!isInvincible)
        {
            FindObjectOfType<AudioManager>().Play("Attack");
            playerHealth -= damage;
            ActivateInvinci();
        }
    }

    void ActivateInvinci()
    {
        isInvincible = true;
        StartCoroutine(Invinci());
    }

    IEnumerator Invinci()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityTime)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = ogcolor;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }
        spriteRenderer.color = ogcolor;
        isInvincible = false;
    }


    void Update()
    {

        if(playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Scene");
        }
        healthbar.fillAmount = playerHealth/10f;
        switch(weaponNum)
        {
            case 1:
                spriteRenderer.sprite = sprites[1]; //pistol
                break;
            case 2:
                spriteRenderer.sprite = sprites[2]; // machine
                break;
            default:
                spriteRenderer.sprite = sprites[0]; //idle
                break;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        if(movement.magnitude > 0)
        {
            stepTime -= Time.deltaTime;
            if(stepTime <= 0f)
            {
                FindObjectOfType<AudioManager>().Play("Walk");
                stepTime = stepIn;
            }
        }
        else
        {
            stepTime = stepIn;
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    void FixedUpdate()
    {
        if(canMove == true)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            rb.freezeRotation = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            inventory.AddItem(item.item, 1);
            FindObjectOfType<AudioManager>().Play("ItemTake");
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
