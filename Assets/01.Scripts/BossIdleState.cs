using UnityEngine;

public class BossIdleState : IState<BossController>
{
    private float attackTimer;

    public void Enter(BossController obj)
    {
        attackTimer = 0f;
    }

    public void Exit(BossController obj)
    {
        attackTimer = 0f;
    }

    public void Update(BossController obj)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= obj.IdleTimer)
        {
            obj.ChangeState(obj.attackState);
        }
    }
}
