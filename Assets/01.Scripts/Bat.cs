using System.Collections.Generic;
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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine<EnemyController>(this);

        nowHp = maxHp;
    }

    private void Start()
    {
        target = StageManager.instance.PlayerPos;
        ChangeState(MonsterState.Idle);
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
        
    }

    protected override void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }

    protected override void Die()
    {
              
    }

    public override Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }
}