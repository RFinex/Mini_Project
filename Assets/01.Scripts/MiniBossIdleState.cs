using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float nowDelay;

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
            // 테스트 용
            int nextState = Random.Range(1, 2);
            //int nextState = Random.Range(0, 2);
            obj.ChangeState(nextState);
        }
    }
}
