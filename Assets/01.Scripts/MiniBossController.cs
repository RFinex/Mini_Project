using UnityEngine;
using System.Collections.Generic;

public class MiniBossController : EnemyController
{
    private StateMachine<MiniBossController> stateMachine;
    private MiniBossIdleState idleState;
    private MiniBossNormalAttackState normalAttackState;
    private MiniBossHeavyAttackState heavyAttackState;
    private MiniBossMoveState moveState;

    private MonsterWeapon mbWeapon;


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

    private Animator animator;

    public Animator MBAnimator
    {
        get
        {
            return animator;
        }
    }


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

    private bool isPhase2;

    public bool IsPhase2
    {
        get
        {
            return isPhase2;
        }
    }

    private Vector2 bulletDir;

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;
        isPhase2 = false;

        stateMachine = new StateMachine<MiniBossController>(this);
        idleState = new MiniBossIdleState();
        normalAttackState = new MiniBossNormalAttackState();
        heavyAttackState = new MiniBossHeavyAttackState();
        moveState = new MiniBossMoveState();

        animator = GetComponent<Animator>();
        mbWeapon = GetComponent<MonsterWeapon>();
        stateMachine.ChangeState(idleState);

        mbWeapon.dirFunc = GetDirection;

        target = GameObject.Find(ConstString.Player).transform;
    }

    private void Start()
    {
        isMove = Animator.StringToHash("isMove");
        isHeavy = Animator.StringToHash("isHeavy");
    }


    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    public void FlipBoss()
    {
        CheckFlip();
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
                stateMachine.ChangeState(moveState);
                break;
            case 2:
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

    public void StopNormalAttack()
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
        base.TakeDamage();
        if (nowHp <= maxHp / 2)
        {
            isPhase2 = true;
            speed = 2f;
            sr.color = Color.red;
        }
    }

    protected override void Die()
    {
        
    }
}
