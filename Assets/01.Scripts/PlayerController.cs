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
    [Header("Player Data")]
    [SerializeField] private PlayerDataSO data;

    #region Component
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Animator animator;
    #endregion

    #region Player Stats
    private PlayerState currentState = PlayerState.Normal;
    private float dir;
    private float speed;
    private float baseGravity;
    private int jumpCount;
    #endregion

    private WaitForSeconds dashWait;

    // 상태 체크용
    private bool isFlip;
    private bool isGround;
    private bool canDash;
    private bool isAntiGravity;

    private PlayerWeapon rangeWeapon;
    private PlayerWeapon meleeWeapon;
    private PlayerWeapon nowWeapon;

    #region Animation Hash
    private int isWalk;
    private int isJump;
    private int isFall;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rangeWeapon = GetComponentInChildren<PlayerRangeWeapon>(true);
        meleeWeapon = GetComponentInChildren<PlayerMeleeWeapon>(true);
        nowWeapon = rangeWeapon;

        speed = data.baseSpeed;
        isFlip = false;
        canDash = false;
        isAntiGravity = false;
        baseGravity = rb.gravityScale;
        currentState = PlayerState.Normal;
        dashWait = new WaitForSeconds(data.dashDelay);
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
            rb.linearVelocity = launchDir.normalized * speed * data.launchSpeed;
            dir = 0;
        }
    }

    private void LaunchStateHandle()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RestoreGravity();
            currentState = PlayerState.Normal;
            Jump();
        }
        if (Keyboard.current.anyKey.wasPressedThisFrame || rb.linearVelocity.sqrMagnitude <= data.criterionVelocity)
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
        if (jumpCount >= data.jumpCountMax)
            return;

        float gravityJump = isAntiGravity ? -data.jumpPower : data.jumpPower;

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
        rb.linearVelocity = new Vector2(isFlip ? -speed * data.dashSpeed : speed * data.dashSpeed, 0);

        yield return dashWait;

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
        Vector2 check = new Vector2(transform.position.x + data.boxOffset, transform.position.y);

        RaycastHit2D hit = Physics2D.BoxCast(check, data.boxSize, 0f, gravity, data.boxDistance, data.groundLayer);

        isGround = hit.collider == null ? false : true;

        if (isGround && Mathf.Abs(rb.linearVelocity.y) <= data.criterionVelocity)
        {
            jumpCount = 0;
            animator.SetBool(isFall, false);
        }
    }

    private void FallCheck()
    {
        bool isFalling = isAntiGravity ? rb.linearVelocity.y > data.criterionVelocity : rb.linearVelocity.y < -data.criterionVelocity;

        animator.SetBool(isFall, isFalling);

        if (isFalling)
        {
            animator.SetBool(isJump, false);
        }
    }

    private void OnDrawGizmos()
    {
        float gravityY = isAntiGravity ? -data.boxDistance : data.boxDistance;
        Gizmos.color = Color.green;
        Vector3 gizmos = new Vector3(transform.position.x + data.boxOffset, transform.position.y - gravityY, 0);
        Gizmos.DrawCube(gizmos, data.boxSize);
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