using UnityEngine;

public class BossController : EnemyController
{   
    [SerializeField] private Transform attackPos;
    private Vector2 baseAttackPos;

    private StateMachine<BossController> stateMachine;
    public BossSleepState sleepState;
    public BossEnterState enterState;
    public BossIdleState idleState;
    public BossAttackState attackState;

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        stateMachine = new StateMachine<BossController>(this);

        target = GameObject.Find(ConstString.Player).transform;
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    private void Update()
    {
        CheckFlip();
    }

    protected override void CheckFlip()
    {
        base.CheckFlip();
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
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

    protected override void Die()
    {
        
    }
}
