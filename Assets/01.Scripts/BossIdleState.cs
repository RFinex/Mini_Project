using UnityEngine;

public class BossIdleState : IState<BossController>
{
    private float attackTimer;
    private float maxTimer;

    public void Enter(BossController obj)
    {
        attackTimer = 0f;
        maxTimer = 3f;
    }

    public void Exit(BossController obj)
    {
        attackTimer = 0f;
    }

    public void Update(BossController obj)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= maxTimer)
        {
            obj.ChangeState(obj.attackState);
        }
    }
}
