using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bat : EnemyController
{
    private StateMachine<EnemyController> stateMachine;

    private SpriteRenderer sr;
    private Collider2D col;
    private Animator animator;



    Dictionary<MonsterState, IState<EnemyController>> states = new Dictionary<MonsterState, IState<EnemyController>>()
    {
        { MonsterState.Idle, new MonsterIdleState() },
        { MonsterState.Trace, new MonsterTraceState() }
    };

    private int isDie;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine<EnemyController>(this);

        isDie = Animator.StringToHash("isDie");

        nowHp = maxHp;
    }

    private void Start()
    {
        target = StageManager.instance.PlayerPos;
        ChangeState(MonsterState.Idle);
    }

    private void OnEnable()
    {
        col.enabled = true;
        nowHp = maxHp;
        ChangeState(MonsterState.Idle);
        if (animator != null)
        {
            animator.SetBool(isDie, false);
        }        
    }

    private void Update()
    {
        stateMachine.Update();
    }

    protected override void ChangeState(IState<EnemyController> state)
    {
        stateMachine.ChangeState(state);
    }

    public override void ChangeState(MonsterState state)
    {
        ChangeState(states[state]);
    }

    public override void TakeDamage()
    {
        nowHp--;

        if (nowHp <= 0)
        {
            nowHp = 0;
            Die();
            return;
        }
    }

    protected override void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }

    protected override void Die()
    {
        foreach (var state in states.Values)
        {
            state.Exit(this);
        }
        col.enabled = false;
        animator.SetBool(isDie, true);
    }

    public override Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Monster_bat", this.gameObject);
    }
}