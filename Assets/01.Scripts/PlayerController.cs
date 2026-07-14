using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private float dir;
    private float attackDir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private bool isFlip;
    [SerializeField] private bool isGround;
    [SerializeField] private int jumpCount;
    private int jumpCountMax;

    [SerializeField] private LayerMask groundLayer;

    private PlayerWeapon rangeWeapon;
    private PlayerWeapon meleeWeapon;

    private bool isRange;
    private bool isMelee;
    [SerializeField] private bool isDead;
    [SerializeField] private bool isHold;
    [SerializeField] private bool isLaunch;

    private float baseGravity;


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
        isRange = true;
        isMelee = false;
        isDead = false;
        isHold = false;
        isLaunch = false;
        rb.gravityScale = 3;
    }

    private void Start()
    {
        meleeWeapon.gameObject.SetActive(false);
    }


    private void Update()
    {
        PlayerControll();
        GroundCheck();
    }

    private void PlayerControll()
    {
        if (isDead)
            return;

        if (isHold)
        {
            Vector2 launchDir = Vector2.zero;

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                launchDir.x = -1;
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                launchDir.x = 1;
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                launchDir.y = 1;
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                launchDir.y = -1;
            }

            if (launchDir != Vector2.zero)
            {
                isHold = false;
                isLaunch = true;
            }
        }


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
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ChangeWeapon();
        }
    }    

    private void ChangeWeapon()
    {
        if (isRange)
        {
            rangeWeapon.gameObject.SetActive(false);
            meleeWeapon.gameObject.SetActive(true);
            isRange = false;
            isMelee = true;
        }
        else if (isMelee)
        {
            rangeWeapon.gameObject.SetActive(true);
            meleeWeapon.gameObject.SetActive(false);
            isRange = true;
            isMelee = false;
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

            if (isRange && !isMelee)
            {
                rangeWeapon.AttackPosDirection(isFlip);
                rangeWeapon.GetDirection(attackDir);
            }
            else if (!isRange && isMelee)
            {
                meleeWeapon.AttackPosDirection(isFlip);
                meleeWeapon.GetDirection(attackDir);
            }
        }
        if (!isDead)
        {
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        }
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

    public void AddJump()
    {
        jumpCount = 1;
    }

    public void LaunchPlayer()
    {

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

    public void CollisionObject()
    {
        Die();
    }

    private void Die()
    {
        isDead = true;

        EffectManager.instance.ShowDeathParticle();
        sr.enabled = false;
        rangeWeapon.gameObject.SetActive(false);
        meleeWeapon.gameObject.SetActive(false);
        rb.linearVelocity = Vector2.zero;
        UIManager.instance.OnGameOverText();
    }

    private void GetDirection(float dir)
    {
        attackDir = dir;
    }
}
