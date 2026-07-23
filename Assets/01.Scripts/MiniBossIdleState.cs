using UnityEngine;

public class MiniBossIdleState : IState<MiniBossController>
{
    private float timer = 0f;
    private int randPattern = 2;
    private int currentPattern = -1;

    public void Enter(MiniBossController obj)
    {
        timer = 0f;
        randPattern = obj.IsPhase2 ? 3 : 2;
        if (obj.IsPhase2)
        {
            obj.SetBulSpeed();
        }
    }

    public void Exit(MiniBossController obj)
    {
        timer = 0f;
    }

    public void Update(MiniBossController obj)
    {
        timer += Time.deltaTime;

        if (timer > obj.IdleTimer)
        {
            int nextState = Random.Range(0, randPattern);
            if (randPattern > 2)
            {
                do
                {
                    nextState = Random.Range(0, randPattern);
                } while (nextState == currentPattern);
            }

            currentPattern = nextState;
            obj.RandomChangeState(currentPattern);
        }
    }
}