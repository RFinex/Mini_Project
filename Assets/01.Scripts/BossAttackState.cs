using System.Linq;
using UnityEngine;

public class BossAttackState : IState<BossController>
{
    private int randomPattern;
    private int currentPattern = -1;

    public void Enter(BossController obj)
    {
        if (obj.Patterns == null)
        {
            Debug.Log("리스트 없음");
            obj.ChangeState(BossState.Idle);
            return;
        }

        // 중복 실행 방지
        do
        {
            randomPattern = Random.Range(0, obj.CurrentPhase * 4);

        } while (randomPattern == currentPattern);
        currentPattern = randomPattern;
        obj.Patterns[currentPattern].StartPattern(obj);
    }

    public void Exit(BossController obj)
    {
        obj.Patterns[currentPattern].StopAttack();
        obj.BAnimator.SetBool(obj.IsAttack, false);
    }

    public void Update(BossController obj)
    {
        if (obj.Patterns[currentPattern].isFinish)
        {
            obj.ChangeState(BossState.Idle);
        }
    }
}
