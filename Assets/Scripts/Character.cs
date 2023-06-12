using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float normalGravityScale;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckRadius;

    private bool isGrounded;


    private void Start() {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 1.1f;
        jumpForce = 17.5f;
    }


        private void Update() {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            rb.AddForce(transform.up * (jumpForce * 10f));
        else if (Input.GetKey(KeyCode.UpArrow) && rb.velocity.y > 0)
            rb.gravityScale = jumpingGravityScale;
        else if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y >= 0)
            rb.velocity = new Vector2(rb.velocity.x,0f);
        else
            rb.gravityScale = normalGravityScale;


       // if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
       //     rb.AddForce(Vector2.up * jumpForce);

        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if(rb.velocity.y == 0) 
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0)
            anim.SetBool("isJumping", true);

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }        
    }
        private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
        rb.velocity = new Vector2(dirX,rb.velocity.y);
    }


        private void LateUpdate()
    {
        if(dirX>0)
            facingRight= true;
        else if(dirX<0)
            facingRight= false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;


        transform.localScale = localScale;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
