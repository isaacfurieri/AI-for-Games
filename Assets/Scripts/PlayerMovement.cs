using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool isFacingRight = true;

    Vector2 movement;
    // Update is called once per frame
    void Update()
    {
        //m_ObjectCollider = 
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(!isFacingRight && movement.x > 0 )
        {
            Flip();
            Debug.Log("Enter");
        }
        else if(isFacingRight && movement.x < 0)
        { 
            Flip();        
        }
        Debug.Log(movement.x);

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
