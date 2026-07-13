using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private float dir;
    private float bulletDir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private bool isFlip;
    [SerializeField] private bool isGround;
    [SerializeField] private int jumpCount;
    private int jumpCountMax;

    [SerializeField] LayerMask groundLayer;

    PlayerWeapon rangeWeapon;
    PlayerWeapon meleeWeapon;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        rangeWeapon = GetComponentInChildren<PlayerRangeWeapon>(true);
        meleeWeapon = GetComponentInChildren<PlayerMeleeWeapon>(true);
        speed = 5f;
        jumpPower = 12f;
        jumpCount = 0;
        jumpCountMax = 2;
        isFlip = false;
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
        if (dir != 0)
        {
            if (dir > 0)
            {
                isFlip = false;
                sr.flipX = isFlip;
                GetDirection(dir);
            }
            else
            {
                isFlip = true;
                sr.flipX = isFlip;
                GetDirection(dir);
            }
            rangeWeapon.AttackPosDirection(isFlip);
            meleeWeapon.AttackPosDirection(isFlip);
            rangeWeapon.GetDirection(bulletDir);
            meleeWeapon.GetDirection(bulletDir);
        }
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (jumpCount >= jumpCountMax)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        if (isGround)
            jumpCount++;
        else
            jumpCount += 2;
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.1f), 0f, Vector2.down, 0.5f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround && rb.linearVelocity.y <= 0.1f)
        {
            jumpCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - new Vector3(0, 0.5f, 0), new Vector2(1f, 0.1f));
    }

    private void GetDirection(float dir)
    {
        bulletDir = dir;
    }
}
