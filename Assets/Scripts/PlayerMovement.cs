using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5.0f;
    public static PlayerMovement Instance;
    public Rigidbody2D rb;
    public WeaponBow weapon;

    public float fireDelay = 5.0f;
    public float dmgDelay = 1.0f;
    public float timer = 0.0f;
    public float lavaTimer = 0.0f;
    public float maxHealth = 100.0f;
    public float currentHealth = 1.0f;
    public HealthBar healthBar;

    private bool isFacingRight = true;
    private bool lava = true;

    Vector2 movement;
    Vector2 mousePosition;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called when game starts
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lavaTimer += Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (!isFacingRight && movement.x > 0)
        {
            Flip();
            Debug.Log("Enter");
        }
        else if (isFacingRight && movement.x < 0)
        {
            Flip();
        }

        if (Input.GetMouseButton(0) & fireDelay < timer)
        {
            //weapon.Attack(rb.position, mousePosition);
            animator.SetBool("isAttacking", true);
            moveSpeed = 0;
            timer = 0.0f;
        }
        else if (!Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isAttacking", false);
            moveSpeed = 5.0f;
        }

        if(dmgDelay < lavaTimer & lava == false)
        {
            lava = true;
            lavaTimer = 0.0f;
        }

        //Player animation with RPC -- RPC goes on player will fire numbers 
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
    }

    public void Flip() 
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //currentHealth = Mathf.Lerp(currentHealth, damage, 2.0f);

        healthBar.SetHealth(currentHealth);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Debug.Log("TAKEN SHOT");
            //AddReward(-5);
            //EndEpisode();
            TakeDamage(20.0f);
        }
        if (collision.gameObject.CompareTag("Lava"))
        {
            //Debug.Log("TAKEN SHOT");
            //AddReward(-5);
            //EndEpisode();
            if (lava)
            {
                TakeDamage(1.0f);
                lava = false;
            }
        }
        Debug.Log("Collision with lava");
        
    }

}
