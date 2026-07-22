using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float nowDelay = 4f;
    private int randPattern = 2;
    private int currentPattern = -1;

    public void Enter(MiniBossController obj)
    {
        obj.ResetIdleTimer();
        nowDelay = obj.IsPhase2 ? 2f : 4f;
        randPattern = obj.IsPhase2 ? 3 : 2;
        if (obj.IsPhase2)
        {
            obj.SetBulSpeed();
        }
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
            if (randPattern > 2)
            {
                while (nextState == currentPattern)
                {
                    nextState = Random.Range(0, randPattern);
                }
            }
            currentPattern = nextState;
            obj.RandomChangeState(currentPattern);
        }
    }
}