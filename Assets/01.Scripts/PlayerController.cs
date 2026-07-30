using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Normal,
    Die,
    Dash,
    Hold,
    Launch
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator animator;

    private float dir;

    private PlayerState currentState = PlayerState.Normal;

    [Header("Player Stats")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float speed;
    [SerializeField] private float baseGravity;
    [SerializeField] private float jumpPower = 12f;
    [SerializeField] private int jumpCount;
    [SerializeField] private int jumpCountMax = 2;

    [Header("Tempory Stats")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float launchSpeed = 3f;

    // 상태 체크용
    private bool isFlip;
    private bool isGround;
    private bool canDash;
    private bool isAntiGravity;

    [Header("Ground Check Setting")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 boxSize = new Vector2(0.85f, 0.1f);
    [SerializeField] private float boxOffset = -0.08f;
    [SerializeField] private float boxDistance = 0.55f;

    [Header("Criterion")]
    [SerializeField] private float criterionVelocity = 0.1f;

    private PlayerWeapon rangeWeapon;
    private PlayerWeapon meleeWeapon;
    private PlayerWeapon nowWeapon;    

    // 애니메이션 bool
    private int isWalk;
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
        nowWeapon = rangeWeapon;

        speed = baseSpeed;
        isFlip = false;
        canDash = false;
        isAntiGravity = false;
        baseGravity = rb.gravityScale;
        currentState = PlayerState.Normal;
    }

    private void Start()
    {
        meleeWeapon.gameObject.SetActive(false);
        isWalk = Animator.StringToHash("isWalk");
        isJump = Animator.StringToHash("isJump");
        isFall = Animator.StringToHash("isFall");
    }

    private void Update()
    {
        PlayerControll();
    }

    private void PlayerControll()
    {
        if (currentState == PlayerState.Die)
            return;

        switch (currentState)
        {
            case PlayerState.Normal:
                NormalStateHandle();
                break;

            case PlayerState.Hold:
                HoldStateHandle();
                break;

            case PlayerState.Launch:
                LaunchStateHandle();
                break;
        }
        //// 발사 장치에 접촉 시 해당 조작으로 변경
        //if (currentState == PlayerState.Hold)
        //{
        //    Vector2 launchDir = Vector2.zero;

        //    if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        //    {
        //        launchDir.x = -1;
        //    }
        //    if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        //    {
        //        launchDir.x = 1;
        //    }
        //    if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        //    {
        //        launchDir.y = 1;
        //    }
        //    if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        //    {
        //        launchDir.y = -1;
        //    }
            
        //    // 발사 성공 시 해당 방향으로 직선 발사
        //    if (launchDir != Vector2.zero)
        //    {
        //        currentState = PlayerState.Launch;
        //        rb.gravityScale = 0;
        //        rb.linearVelocity = launchDir.normalized * speed * launchSpeed;
        //        dir = 0;
        //    }
        //    return;
        //}

        //// 발사 도중 아무 조작 입력 시 원상태로 복구
        //if (currentState == PlayerState.Launch)
        //{
        //    // 현재 내 속도의 크기 계산
        //    if (Keyboard.current.anyKey.wasPressedThisFrame || rb.linearVelocity.sqrMagnitude <= criterionVelocity)
        //    {
        //        RestoreGravity();
        //        currentState = PlayerState.Normal;
        //    }
        //}

        //dir = 0;

        //if (Keyboard.current.leftArrowKey.isPressed)
        //{
        //    dir += -1;
        //}
        //if (Keyboard.current.rightArrowKey.isPressed)
        //{
        //    dir += 1;
        //}
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    Jump();
        //}
        //if (Keyboard.current.shiftKey.wasPressedThisFrame && canDash)
        //{
        //    StartCoroutine(Dash());
        //}
        ////if (Keyboard.current.tabKey.wasPressedThisFrame)
        ////{
        ////    ChangeWeapon();
        ////}
    }

    private void NormalStateHandle()
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
        if (Keyboard.current.shiftKey.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void HoldStateHandle()
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

        // 발사 성공 시 해당 방향으로 직선 발사
        if (launchDir != Vector2.zero)
        {
            currentState = PlayerState.Launch;
            rb.gravityScale = 0;
            rb.linearVelocity = launchDir.normalized * speed * launchSpeed;
            dir = 0;
        }
    }

    private void LaunchStateHandle()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || rb.linearVelocity.sqrMagnitude <= criterionVelocity)
        {
            RestoreGravity();
            currentState = PlayerState.Normal;
        }
    }

    //private void ChangeWeapon()
    //{
    //    nowWeapon.gameObject.SetActive(false);

    //    if (nowWeapon == rangeWeapon)
    //    {
    //        nowWeapon = meleeWeapon;
    //    }
    //    else
    //    {
    //        nowWeapon = rangeWeapon;
    //    }

    //    nowWeapon.gameObject.SetActive(true);
    //    //if (isRange)
    //    //{
    //    //    rangeWeapon.gameObject.SetActive(false);
    //    //    meleeWeapon.gameObject.SetActive(true);
    //    //    isRange = false;
    //    //    isMelee = true;
    //    //}
    //    //else if (isMelee)
    //    //{
    //    //    rangeWeapon.gameObject.SetActive(true);
    //    //    meleeWeapon.gameObject.SetActive(false);
    //    //    isRange = true;
    //    //    isMelee = false;
    //    //}
    //}

    private void CheckSprite()
    {
        if (dir != 0)
        {
            animator.SetBool(isWalk, true);
            isFlip = dir < 0;
            sr.flipX = isFlip;

            if (nowWeapon != null)
            {
                nowWeapon.AttackPosDirection(isFlip);
                nowWeapon.GetDirection(dir);
            }
            
        }
        else
        {
            animator.SetBool(isWalk, false);
        }
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.Normal)
        {
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        }
        
        CheckSprite();
        GroundCheck();
        FallCheck();
    }

    private void Jump()
    {
        if (jumpCount >= jumpCountMax)
            return;

        float gravityJump = isAntiGravity ? -jumpPower : jumpPower;

        if (currentState != PlayerState.Launch)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, gravityJump);
        }

        if (isGround)
        {
            jumpCount++;
            SoundManager.instance.PlaySFX(SFXType.Jump);
        }
        else
        {
            jumpCount += 2;
            SoundManager.instance.PlaySFX(SFXType.DoubleJump);
        }

        animator.SetBool(isJump, true);
    }

    public void AddJump()
    {
        jumpCount = 1;
    }

    public void LaunchPlayer(Vector3 target)
    {
        transform.position = target;
        currentState = PlayerState.Hold;
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
        currentState = PlayerState.Dash;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(isFlip ? -speed * dashSpeed : speed * dashSpeed, 0);

        yield return new WaitForSeconds(0.2f);

        RestoreGravity();
        rb.linearVelocity = Vector2.zero;
        currentState = PlayerState.Normal;
    }

    public void AntiGravity(bool isAnti)
    {
        isAntiGravity = isAnti;
        RestoreGravity();
        sr.flipY = isAntiGravity;
    }

    private void RestoreGravity()
    {
        rb.gravityScale = isAntiGravity ? -baseGravity : baseGravity;
    }

    private void GroundCheck()
    {
        Vector2 gravity = isAntiGravity ? Vector2.up : Vector2.down;
        Vector2 check = new Vector2(transform.position.x + boxOffset, transform.position.y);

        RaycastHit2D hit = Physics2D.BoxCast(check, boxSize, 0f, gravity, boxDistance, groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround && Mathf.Abs(rb.linearVelocity.y) <= criterionVelocity)
        {
            jumpCount = 0;
            animator.SetBool(isFall, false);
        }
    }

    private void FallCheck()
    {
        bool isFalling = isAntiGravity ? rb.linearVelocity.y > criterionVelocity : rb.linearVelocity.y < -criterionVelocity;

        animator.SetBool(isFall, isFalling);

        if (isFalling)
        {
            animator.SetBool(isJump, false);
        }
    }

    private void OnDrawGizmos()
    {
        float gravityY = isAntiGravity ? -boxDistance : boxDistance;
        Gizmos.color = Color.green;
        Vector3 gizmos = new Vector3(transform.position.x + boxOffset, transform.position.y - gravityY, 0);
        Gizmos.DrawCube(gizmos, boxSize);
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die()
    {
        currentState = PlayerState.Die;

        EffectManager.instance.ShowDeathParticle();
        sr.enabled = false;
        col.enabled = false;

        rangeWeapon.gameObject.SetActive(false);
        meleeWeapon.gameObject.SetActive(false);

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        GameManager.instance.GameOver();
    }

    //private void GetDirection(float dir)
    //{
    //    attackDir = dir;
    //}
}