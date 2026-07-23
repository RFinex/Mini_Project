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

    public BossPatternBase[] Patterns
    {
        get
        {
            return patterns;
        }
    }

    private int currentPhase;
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
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;
        currentPhase = 1;

        animator = GetComponent<Animator>();
        stateMachine = new StateMachine<BossController>(this);
        sleepState = new BossSleepState();
        enterState = new BossEnterState();
        idleState = new BossIdleState();
        attackState = new BossAttackState();

        target = GameObject.Find(ConstString.Player).transform;
    }

    private void Start()
    {
        isAttack = Animator.StringToHash("isAttack");
        ChangeState(sleepState);
    }

    private void OnEnable()
    {
        maxHp = 300;
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

    public void NextPhase()
    {
        currentPhase++;
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
