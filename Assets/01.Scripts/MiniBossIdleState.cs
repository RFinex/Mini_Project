using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float nowDelay = 5f;
    private int randPattern = 2;

    public void Enter(MiniBossController obj)
    {
        obj.ResetIdleTimer();
        nowDelay = obj.IsPhase2 ? 2.5f : 5f;
        randPattern = obj.IsPhase2 ? 3 : 2;
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
