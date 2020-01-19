using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float xAxis;
    public int j;
    [SerializeField] int randomJumpValue;
    [SerializeField] int randomMovementValue;
    [SerializeField] private bool isGrounded;
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
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;
    public float time = 10f;
    [SerializeField] float hogHitForce;
    public int health;
    public int Diamond;

    [SerializeField] Vector2 wallJumpDirection;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject timeUpPanel;
    [SerializeField] GameObject destoryParticle;

    [SerializeField] Transform[] transportPoints;
    [SerializeField] Transform jumpPoint;
    [SerializeField] Transform wallPoint;
    [SerializeField] Transform shootPoint;

    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Wall;

    Spider spider;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        spider = GameObject.FindGameObjectWithTag("Spider").GetComponent<Spider>();

        randomJumpValue = Random.Range(1, 15);
        randomMovementValue = Random.Range(1, 5);

        timer = timeBtwShoot;

        timerText.text = time.ToString();
        healthText.text = health.ToString();

        timeUpPanel.SetActive(false);
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(jumpPoint.position, jumpPointRadius, Ground);
        isOnWall = Physics2D.OverlapCircle(wallPoint.position, wallPointRadius, Wall);

        if(randomMovementValue >= 1 && randomMovementValue <= 3)
        {
            movementSpeed = 250;
        }else if(randomMovementValue >= 4 && randomMovementValue <= 5)
        {
            movementSpeed = 450;
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            randomMovementValue = Random.Range(1, 5);
        }

        if(xAxis == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }

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
            animator.SetBool("isSliding", true);
            if (rb.velocity.y < -wallSlidingForce)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingForce);
            }
        }
        else
        {
            animator.SetBool("isSliding", false);
        }

        //For Shooting..

        if(Input.GetKeyDown(KeyCode.X) && timer <= 0)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            FindObjectOfType<AudioManager>().Play("Shoot");
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(time <= 0f)
        {
            time = 0f;
            timeUpPanel.SetActive(true);
            Destroy(gameObject);
        }

        healthText.text = health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //if(transform.position.y <= -45f)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(xAxis * movementSpeed * Time.deltaTime, rb.velocity.y);

        if(randomJumpValue >= 1 && randomJumpValue <= 6)
        {
            jumpForce = 800f;
        }
        else if(randomJumpValue >= 7 && randomJumpValue <= 10)
        {
            jumpForce = 1100;
        }else if(randomJumpValue >= 11 && randomJumpValue <= 15)
        {
            jumpForce = 500;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            Jump();
            animator.SetBool("isJumping", true);
            randomJumpValue = Random.Range(1, 15);
        }else
        {
            animator.SetBool("isJumping", false);
        }

        if (isOnWall && Input.GetKeyDown(KeyCode.Z))
        {
            WallJump();
        }

        timerText.text = time.ToString("0");
        time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hog"))
        {
            if (isRight)
            {
                Vector2 addForce = new Vector2(2 * hogHitForce, 2 * hogHitForce);
                rb.AddForce(addForce);
            }
            else if (!isRight)
            {
                Vector2 addForce = new Vector2(-2 * hogHitForce, 2 * hogHitForce);
                rb.AddForce(addForce);
            }
            health--;
        }

        if(collision.transform.CompareTag("Pighead"))
        {
            health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FireBall"))
        {
            health -= 1;
        }

        if(collision.CompareTag("Spider") && spider.i == 1)
        {
            health -= 5;
            Destroy(collision.transform.gameObject);
        }else if(collision.CompareTag("Spider") && spider.i == 2)
        {
            j = Random.Range(0, transportPoints.Length);
            transform.position = transportPoints[j].position;
        }else if(collision.CompareTag("Spider") && spider.i == 3)
        {
            health -= 5;
            j = Random.Range(0, transportPoints.Length);
            transform.position = transportPoints[j].position;
        }

        if(collision.CompareTag("Lava"))
        {
            health -= 1;
        }

        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Lava"))
        {
            StartCoroutine(damage());
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

    IEnumerator damage()
    {
        yield return new WaitForSeconds(1f);
        health -= 1;
        yield return new WaitForSeconds(1f);
    }

    private void OnDestroy()
    {
        Instantiate(destoryParticle, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(jumpPoint.position, jumpPointRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(wallPoint.position, wallPointRadius);
    }
}
