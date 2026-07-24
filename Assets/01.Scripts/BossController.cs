using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Sleep,
    Enter,
    Idle,
    Attack,
    Stun,
    Die
}

public class BossController : EnemyController
{   
    [SerializeField] private Transform attackPos;
    public Transform AttackPos
    {
        get
        {
            return attackPos;
        }
    }
    private Vector2 baseAttackPos;

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

    Dictionary<BossState, IState<BossController>> states = new Dictionary<BossState, IState<BossController>>()
    {
        { BossState.Sleep, new BossSleepState() },
        { BossState.Enter, new BossEnterState() },
        { BossState.Idle, new BossIdleState() },
        { BossState.Attack, new BossAttackState() },
        { BossState.Stun, new BossStunState() },
        { BossState.Die, new BossDieState() }
    };

    private StateMachine<BossController> stateMachine;

    [SerializeField] private BossPatternBase[] patterns;

    [SerializeField] private float dieDelay = (2f + 50f / 60f);
    public float DieDelay
    {
        get
        {
            return dieDelay;
        }
    }

    [SerializeField] private float fadeDelay = 5f;
    public float FadeDelay
    {
        get
        {
            return fadeDelay;
        }
    }

    private Animator animator;
    public Animator BAnimator
    {
        get
        {
            return animator;
        }
    }

    private int isAttack;
    public int IsAttack
    {
        get
        {
            return isAttack;
        }
    }

    private int isStun;
    public int IsStun
    {
        get
        {
            return isStun;
        }
    }

    private int isDie;

    public BossPatternBase[] Patterns
    {
        get
        {
            return patterns;
        }
    }

    private int currentPhase = 1;
    public int CurrentPhase
    {
        get
        {
            return currentPhase;
        }
        private set
        {
            currentPhase = value;
        }
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        animator = GetComponent<Animator>();
        stateMachine = new StateMachine<BossController>(this);

        idleTimer = baseIdleTimer;
        target = GameObject.Find(ConstString.Player).transform;
    }

    private void Start()
    {
        isAttack = Animator.StringToHash("isAttack");
        isStun = Animator.StringToHash("isStun");
        isDie = Animator.StringToHash("isDie");
        ChangeState(states[BossState.Sleep]);
    }

    private void OnEnable()
    {
        nowHp = maxHp;
    }

    private void Update()
    {
        CheckFlip();
        stateMachine.Update();
    }

    protected override void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }


    // 페이즈 전환
    public void NextPhase()
    {
        currentPhase++;
        ChangeState(states[BossState.Stun]);
        if (currentPhase > 2)
            idleTimer = baseIdleTimer * 0.5f;
    }

    
    public void SetBossHpBar()
    {
        UIManager.instance.SetBossHPSlider(maxHp);
    }

    private void ChangeState(IState<BossController> state)
    {
        stateMachine.ChangeState(state);
    }

    public void ChangeState(BossState state)
    {
        ChangeState(states[state]);
    }

    public override void TakeDamage()
    {
        nowHp--;

        UIManager.instance.BossHpSlider(nowHp);

        if (nowHp <= 0)
        {
            nowHp = 0;
            Die();
            return;
        }

        if (currentPhase == 1 && nowHp <= maxHp * (2f / 3f))
        {
            NextPhase();
        }
        else if (currentPhase == 2 && nowHp <= maxHp * (1f / 3f)) 
        {
            NextPhase();
        }
    }

    public Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }

    public Vector2 GetAttackPosDirection()
    {
        return (target.position - attackPos.position).normalized;
    }

    protected override void Die()
    {
        foreach (var state in states.Values)
        {
            state.Exit(this);
        }
        animator.SetBool(isDie, true);
        ChangeState(BossState.Die);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
