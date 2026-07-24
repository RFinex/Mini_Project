using UnityEngine;

public class MiniBossController : EnemyController
{
    private StateMachine<MiniBossController> stateMachine;
    private MiniBossIdleState idleState;
    private MiniBossNormalAttackState normalAttackState;
    private MiniBossHeavyAttackState heavyAttackState;
    private MiniBossMoveState moveState;
    private MiniBossReviveState reviveState;
    private MiniBossDieState dieState;

    private MonsterWeapon mbWeapon;

    protected SpriteRenderer sr;
    public SpriteRenderer Sr
    {
        get
        {
            return sr;
        }
    }

    protected Collider2D col;
    public Collider2D Col
    {
        get
        {
            return col;
        }
    }


    private int isMove;
    public int IsMove
    {
        get
        {
            return isMove;
        }
    }
    private int isHeavy;
    public int IsHeavy
    {
        get
        {
            return isHeavy;
        }
    }
    private int isStart;
    public int IsStart
    {
        get
        {
            return isStart;
        }
    }
    private int isDie;
    public int IsDie
    {
        get
        {
            return isDie;
        }
    }

    private Animator animator;

    public Animator MBAnimator
    {
        get
        {
            return animator;
        }
    }
    

    private bool isPhase2;

    public bool IsPhase2
    {
        get
        {
            return isPhase2;
        }
    }

    private bool isStartBoss;
    public bool IsStartBoss
    {
        get
        {
            return isStartBoss;
        }
    }

    private Vector2 bulletDir;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        stateMachine = new StateMachine<MiniBossController>(this);
        idleState = new MiniBossIdleState();
        normalAttackState = new MiniBossNormalAttackState();
        heavyAttackState = new MiniBossHeavyAttackState();
        moveState = new MiniBossMoveState();
        reviveState = new MiniBossReviveState();
        dieState = new MiniBossDieState();

        animator = GetComponent<Animator>();
        mbWeapon = GetComponent<MonsterWeapon>();

        mbWeapon.dirFunc = GetDirection;

        target = GameObject.Find(ConstString.Player).transform;
    }

    private void Start()
    {
        ChangeState(reviveState);
        isMove = Animator.StringToHash("isMove");
        isHeavy = Animator.StringToHash("isHeavy");
        isStart = Animator.StringToHash("isStart");
        isDie = Animator.StringToHash("isDie");
        nowHp = maxHp;
        isPhase2 = false;
        isStartBoss = false;
    }


    private void OnEnable()
    {
        nowHp = maxHp;
    }

    public void SetBulSpeed()
    {
        if (mbWeapon is MiniBossWeapon mWeapon)
        {
            mWeapon.SetBulletSpeed();
        }
    }

    public void MiniBossStart()
    {
        isStartBoss = true;
        animator.SetBool(isStart, true);
    }

    public void SetMiniBossHpBar()
    {
        UIManager.instance.SetBossHPSlider(maxHp);
    }

    public void FlipBoss()
    {
        CheckFlip();
    }

    protected override void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
        mbWeapon.AttackPosFlip(sr.flipX);
    }

    public void RandomChangeState(int nextState)
    {
        switch(nextState)
        {
            case 0:
                stateMachine.ChangeState(normalAttackState);
                break;
            case 1:
                stateMachine.ChangeState(moveState);
                break;
            case 2:
                stateMachine.ChangeState(heavyAttackState);
                break;
            default:
                break;
        }
    }

    private void ChangeState(IState<MiniBossController> state)
    {
        stateMachine.ChangeState(state);
    }

    public void ChangeIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public Vector2 GetDirection()
    {
        return (target.position - mbWeapon.attackPos.position).normalized;
    }

    public void NormalAttack(int pattern)
    {
        mbWeapon.SetDirection(GetDirection());
        mbWeapon.Attack(pattern);
    }

    public void StopAttack()
    {
        mbWeapon.StopAttack();
    }

    public void HeavyAttack(int pattern)
    {
        mbWeapon.SetAngle(GetDirection());
        if(mbWeapon is MiniBossWeapon mWeapon)
            mWeapon.HeavyAttack(pattern);
    }

    

    public override void TakeDamage()
    {
        nowHp--;

        UIManager.instance.BossHpSlider(nowHp);

        if (nowHp <= 0)
        {
            nowHp = 0;
            Die();
        }
            

        if (nowHp <= maxHp / 2 && !isPhase2)
        {
            isPhase2 = true;
            speed = 2f;
            sr.color = Color.red;
        }
    }

    protected override void Die()
    {
        col.enabled = false;
        UIManager.instance.OffBossHPSlider();
        StopAttack();
        animator.SetBool(isDie, true);
        ChangeState(dieState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
