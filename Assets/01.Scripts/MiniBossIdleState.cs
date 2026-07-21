using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float nowDelay;
    private int randPattern = 2;

    public void Enter(MiniBossController obj)
    {
        obj.ResetIdleTimer();
        nowDelay = 5f;
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
            int nextState = Random.Range(0, randPattern);
            obj.ChangeState(nextState);
        }
    }
}
