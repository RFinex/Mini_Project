using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController
{
    private StateMachine<EnemyController> stateMachine;

    private SpriteRenderer sr;
    private Collider2D col;

    Dictionary<MonsterState, IState<EnemyController>> states = new Dictionary<MonsterState, IState<EnemyController>>()
    {
        { MonsterState.Idle, new MonsterIdleState() }
    };

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        stateMachine = new StateMachine<EnemyController>(this);

        nowHp = maxHp;
    }

    public override void TakeDamage()
    {
        
    }

    protected override void CheckFlip()
    {
        
    }

    protected override void Die()
    {
        
    }
}
