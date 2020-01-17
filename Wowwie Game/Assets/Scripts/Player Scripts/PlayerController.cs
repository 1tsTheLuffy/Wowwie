using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xAxis;
    private bool isGrounded;
    [SerializeField] private bool isOnWall;
    private bool isRight;
    private int i = 1;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [Range(0f, 5f)]
    [SerializeField] float jumpPointRadius;
    [Range(0f,2f)]
    [SerializeField] float wallPointRadius;
    [SerializeField] float wallSlidingForce = 3;
    [SerializeField] float wallJumpForce;

    [SerializeField] Vector2 wallJumpDirection;

    [SerializeField] Transform jumpPoint;
    [SerializeField] Transform wallPoint;

    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Wall;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, jumpPointRadius, Ground);
        isOnWall = Physics2D.OverlapCircle(wallPoint.position, wallPointRadius, Wall);

        if (xAxis < 0 && !isRight)
        {
            Flip();
        }
        else if (xAxis > 0 && isRight)
        {
            Flip();
        }

        //For Sliding..

        if(isOnWall)
        {
            if (rb.velocity.y < -wallSlidingForce)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingForce);
            }
        }

        //if(isOnWall)
        //{
        //    if(Input.GetKey(KeyCode.UpArrow))
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, wallSlidingForce * 1);
        //    }
        //}

        
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(xAxis * movementSpeed * Time.deltaTime, rb.velocity.y);

        if (isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }

        if (isOnWall && Input.GetKeyDown(KeyCode.Z))
        {
            WallJump();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce * Time.deltaTime;
    }

    void Flip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void WallJump()
    {
        if (isOnWall && isRight)
        {
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * 1 * Time.deltaTime,
                wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }
        else if (isOnWall && !isRight)
        {
            Vector2 addForce = new Vector2(wallJumpDirection.x * wallJumpForce * -1 * Time.deltaTime,
                wallJumpDirection.y * wallJumpForce * Time.fixedDeltaTime);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(jumpPoint.position, jumpPointRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(wallPoint.position, wallPointRadius);
    }
}
