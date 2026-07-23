using UnityEngine;

public class BossStunState : IState<BossController>
{
    private float timer;
    private float maxTimer;
    public void Enter(BossController obj)
    {
        timer = 0f;
        maxTimer = 1f;
        obj.BAnimator.SetBool(obj.IsStun, true);
        obj.Col.enabled = false;
    }

    public void Exit(BossController obj)
    {
        obj.BAnimator.SetBool(obj.IsStun, false);
        obj.Col.enabled = true;
    }

    public void Update(BossController obj)
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            obj.ChangeState(BossState.Idle);
        }
    }
}
