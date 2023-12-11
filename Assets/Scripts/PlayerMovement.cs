using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public static PlayerMovement Instance;
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool isFacingRight = true;

    Vector2 movement;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called when game starts
    void Start()
    {
        if (IsOwner)
        {
            Debug.Log("Is Host :" + IsHost + " IsClient : " + IsClient);
            ConnectionManager.Instance.UpdateFlags(IsHost, IsClient);
            Camera.main.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (!isFacingRight && movement.x > 0)
        {
            Flip();
            Debug.Log("Enter");
        }
        else if (isFacingRight && movement.x < 0)
        {
            Flip();
        }
        if (Input.GetMouseButton(0))
        {
            Attack();
            animator.SetBool("isAttacking", true);
            moveSpeed = 0;
        }
        else if (!Input.GetMouseButton(0))
        {
            animator.SetBool("isAttacking", false);
            moveSpeed = 5.0f;
        }
        //Player animation with RPC -- RPC goes on player will fire numbers 
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Flip() 
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    public void Attack() 
    {   
        
    }
}
