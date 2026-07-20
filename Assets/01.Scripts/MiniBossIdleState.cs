using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float nowDelay;

    public void Enter(MiniBossController obj)
    {
        obj.ResetIdleTimer();
        nowDelay = Random.Range(1, 5);
    }

    public void Exit(MiniBossController obj)
    {
        obj.ResetIdleTimer();
    }

    public void Update(MiniBossController obj)
    {
        obj.UpdateIdleTimer();
        if (obj.IdleTimer > nowDelay)
        {
            int nextState = Random.Range(0, 2);
            obj.ChangeState(nextState);
        }
    }
}
