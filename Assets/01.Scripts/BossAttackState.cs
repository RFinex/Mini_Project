using UnityEngine;

public class BossAttackState : IState<BossController>
{
    private int randomPattern;

    public void Enter(BossController obj)
    {
        randomPattern = Random.Range(0, obj.CurrentPhase);
        obj.Patterns[randomPattern].StartRandomPattern(obj);
    }

    public void Exit(BossController obj)
    {
        obj.Patterns[randomPattern].StopAttack();
        obj.BAnimator.SetBool(obj.IsAttack, false);
    }

    public void Update(BossController obj)
    {
        if (obj.Patterns[randomPattern].isFinish)
        {
            obj.ChangeState(obj.idleState);
        }
    }
}
