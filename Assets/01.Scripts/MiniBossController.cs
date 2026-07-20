using System.Collections;
using UnityEngine;

public class MiniBossController : EnemyController
{
    [SerializeField] private Transform attackPos;
    public Transform AttackPos
    {
        get
        {
            return attackPos;
        }
        private set
        {
            attackPos = value;
        }
    }
    private Vector2 baseAttackPos;

    private StateMachine<MiniBossController> stateMachine;
    private MiniBossIdleState idleState;
    private MiniBossNormalAttackState normalAttackState;
    private MiniBossHeavyAttackState heavyAttackState;

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

    public bool canAttack;

    private Vector2 bulletDir;
    public Vector2 BulletDir
    {
        get
        {
            return bulletDir;
        }
        private set
        {
            bulletDir = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        stateMachine = new StateMachine<MiniBossController>();
        idleState = new MiniBossIdleState();
        normalAttackState = new MiniBossNormalAttackState();
        heavyAttackState = new MiniBossHeavyAttackState();
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    protected override void CheckFlip()
    {
        base.CheckFlip();
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX ? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
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
        return (target.position - attackPos.position).normalized;
    }

    private IEnumerator normalAttackCool()
    {
        yield return new WaitForSeconds(0.5f);

        canAttack = true;
    }
}
