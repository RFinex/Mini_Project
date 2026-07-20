using UnityEngine;

public class MiniBossController : EnemyController
{
    private StateMachine<MiniBossController> stateMachine;
    private MiniBossIdleState idleState;
    private MiniBossNormalAttackState normalAttackState;
    private MiniBossHeavyAttackState heavyAttackState;

    private MonsterWeapon mbWeapon;

    private float idleTimer = 0f;
    public float IdleTimer
    {
        get
        {
            return idleTimer;
        }
        private set
        {
            idleTimer = value;
        }
    }

    private Vector2 bulletDir;

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;

        stateMachine = new StateMachine<MiniBossController>();
        idleState = new MiniBossIdleState();
        normalAttackState = new MiniBossNormalAttackState();
        heavyAttackState = new MiniBossHeavyAttackState();

        mbWeapon = GetComponent<MonsterWeapon>();

        target = GameObject.Find(ConstString.Player).transform;
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    protected override void CheckFlip()
    {
        base.CheckFlip();
        mbWeapon.AttackPosFlip(sr.flipX);
    }

    public void ResetIdleTimer()
    {
        idleTimer = 0f;
    }

    public void UpdateIdleTimer()
    {
        idleTimer += Time.deltaTime;
    }

    public void ChangeState(int nextState)
    {
        switch(nextState)
        {
            case 0:
                stateMachine.ChangeState(normalAttackState);
                break;
            case 1:
                stateMachine.ChangeState(heavyAttackState);
                break;
            default:
                break;
        }
    }

    public void ChangeIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    protected override void Update()
    {
        base.Update();
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
}
