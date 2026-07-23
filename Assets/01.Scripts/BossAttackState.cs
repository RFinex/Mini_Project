using UnityEngine;

public class BossAttackState : IState<BossController>
{
    private int randomPattern;

    public void Enter(BossController obj)
    {
        randomPattern = Random.Range(0, obj.CurrentPhase);
        obj.Patterns[randomPattern].StartRandomPattern();
    }

    public void Exit(BossController obj)
    {
        
    }

    public void Update(BossController obj)
    {
        if (obj.Patterns[randomPattern].isFinish)
        {
            obj.ChangeState(obj.idleState);
        }
    }
}
