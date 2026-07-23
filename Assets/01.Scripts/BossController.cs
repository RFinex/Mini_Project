using UnityEngine;

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

    private StateMachine<BossController> stateMachine;
    public BossSleepState sleepState;
    public BossEnterState enterState;
    public BossIdleState idleState;
    public BossAttackState attackState;
    public BossStunState stunState;

    [SerializeField] private BossPatternBase[] patterns;

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

    protected override void Awake()
    {
        base.Awake();
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        animator = GetComponent<Animator>();
        stateMachine = new StateMachine<BossController>(this);
        sleepState = new BossSleepState();
        enterState = new BossEnterState();
        idleState = new BossIdleState();
        attackState = new BossAttackState();
        stunState = new BossStunState();
        
        target = GameObject.Find(ConstString.Player).transform;
    }

    private void Start()
    {
        isAttack = Animator.StringToHash("isAttack");
        isStun = Animator.StringToHash("isStun");
        ChangeState(sleepState);
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
        base.CheckFlip();
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }

    // 페이즈 전환
    public void NextPhase()
    {
        currentPhase++;
        ChangeState(stunState);
    }

    
    public void SetBossHpBar()
    {
        UIManager.instance.SetBossHPSlider(maxHp);
    }

    public void ChangeState(IState<BossController> state)
    {
        stateMachine.ChangeState(state);
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

        if (nowHp <= 200 && currentPhase == 1)
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
        
    }
}
