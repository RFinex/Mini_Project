using UnityEngine;

public class MiniBossNormalAttackState : IState<MiniBossController>
{
    private float timer;

    public void Enter(MiniBossController obj)
    {
        timer = 0f;
        int nextPattern = Random.Range(0, 3);
        obj.NormalAttack(nextPattern);
    }

    public void Exit(MiniBossController obj)
    {
        timer = 0f;
        obj.StopAttack();
    }

    public void Update(MiniBossController obj)
    {
        timer += Time.deltaTime;
        if (timer >= 6f)
        {
            obj.ChangeIdleState();
        }
    }
}
