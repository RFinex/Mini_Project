using UnityEngine;

public class MiniBossHeavyAttackState : IState<MiniBossController>
{
    private float timer;

    public void Enter(MiniBossController obj)
    {
        timer = 0f;
        int nextPattern = Random.Range(0, 3);
        obj.MBAnimator.SetBool(obj.IsHeavy, true);
        obj.HeavyAttack(nextPattern);
    }

    public void Exit(MiniBossController obj)
    {
        timer = 0f;
        obj.MBAnimator.SetBool(obj.IsHeavy, false);
    }

    public void Update(MiniBossController obj)
    {
        timer += Time.deltaTime;
        if (timer >= 7f)
        {
            obj.ChangeIdleState();
        }
    }
}
