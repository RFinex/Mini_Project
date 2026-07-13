using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private float dir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [SerializeField] private bool isGround;
    [SerializeField] private int jumpCount;
    private int jumpCountMax;

    [SerializeField] LayerMask groundLayer;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        speed = 5f;
        jumpPower = 12f;
    }


    private void Update()
    {
        PlayerControll();
        GroundCheck();
    }

    private void PlayerControll()
    {
        dir = 0;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            dir += -1;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            dir += 1;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }    

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        if (isGround)
            jumpCount++;
        else
            jumpCount += 2;
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.1f), 0f, Vector2.down, 0.9f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround && rb.linearVelocity.y <= 0.1f)
        {
            jumpCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - new Vector3(0, 0.9f, 0), new Vector2(1f, 0.1f));
    }
}
