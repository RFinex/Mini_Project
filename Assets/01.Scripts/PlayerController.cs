using System.Collections;
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
    private float baseSpeed;

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
    private bool canDash;
    private bool isDash;
    private bool isAntiGravity;

    private float baseGravity;

    // 애니메이션
    private Animator animator;
    private int isMove;
    private int isJump;
    private int isFall;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        canDash = false;
        isDash = false;
        isAntiGravity = false;
        rb.gravityScale = 3f;
        baseGravity = rb.gravityScale;
        baseSpeed = speed;
    }

    private void Start()
    {
        meleeWeapon.gameObject.SetActive(false);
        isMove = Animator.StringToHash("isMove");
        isJump = Animator.StringToHash("isJump");
        isFall = Animator.StringToHash("isFall");
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
        // 발사 장치에 접촉 시 해당 조작으로 변경
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
                rb.gravityScale = 0;
                rb.linearVelocity = launchDir.normalized * speed * 3;
                dir = 0;
            }
            return;
        }

        // 발사 도중 아무 조작 입력 시 원상태로 복구
        if (isLaunch)
        {
            float nowSpeed = rb.linearVelocity.sqrMagnitude;
            if (Keyboard.current.anyKey.wasPressedThisFrame || nowSpeed <= 0.1f)
            {
                rb.gravityScale = baseGravity;
                isLaunch = false;
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
        if (Keyboard.current.shiftKey.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }

        //if (!isLaunch)
        //{
        //    dir = 0;

        //    if (Keyboard.current.leftArrowKey.isPressed)
        //    {
        //        dir += -1;
        //    }
        //    if (Keyboard.current.rightArrowKey.isPressed)
        //    {
        //        dir += 1;
        //    }
        //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //    {
        //        Jump();
        //    }
        //    if (Keyboard.current.tabKey.wasPressedThisFrame)
        //    {
        //        ChangeWeapon();
        //    }
        //    if (Keyboard.current.shiftKey.wasPressedThisFrame && canDash)
        //    {
        //        StartCoroutine(Dash());
        //    }
        //}
        

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

        if (!isDead && !isHold && !isLaunch && !isDash)
        {
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        if (jumpCount >= jumpCountMax)
            return;

        float gravityJump = isAntiGravity ? -jumpPower : jumpPower;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, gravityJump);
        if (isGround)
            jumpCount++;
        else
            jumpCount += 2;
    }

    public void AddJump()
    {
        jumpCount = 1;
    }

    public void LaunchPlayer(Vector3 target)
    {
        transform.position = target;
        isHold = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    public void DashOn()
    {
        canDash = true;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(isFlip ? -speed * 5 : speed * 5, 0);

        yield return new WaitForSeconds(0.2f);

        rb.gravityScale = baseGravity;
        rb.linearVelocity = Vector2.zero;
        isDash = false;
    }

    public void AntiGravity(bool isAnti)
    {
        isAntiGravity = isAnti;
        rb.gravityScale = isAntiGravity ? -baseGravity : baseGravity;
        sr.flipY = isAntiGravity;
    }

    private void GroundCheck()
    {
        Vector2 gravity = isAntiGravity ? Vector2.up : Vector2.down;

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.2f), 0f, gravity, 0.5f, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround && rb.linearVelocity.y <= 0.1f)
        {
            jumpCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        float gravityY = isAntiGravity ? -0.5f : 0.5f;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - new Vector3(0, gravityY, 0), new Vector2(1f, 0.2f));
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
        rb.gravityScale = 0f;
        UIManager.instance.OnGameOverText();
    }

    private void GetDirection(float dir)
    {
        attackDir = dir;
    }
}
