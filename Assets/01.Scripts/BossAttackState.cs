using UnityEngine;

public class BossAttackState : IState<BossController>
{
    private int randomPattern;    

    public void Enter(BossController obj)
    {
        if (obj.Patterns == null || obj.Patterns.Count == 0)
        {
            Debug.Log("리스트 없음");
            obj.ChangeState(BossState.Idle);
            return;
        }

        int patterns = obj.Patterns.Count;

        // 중복 실행 방지
        if (patterns == 1)
        {
            obj.CurrentPattern = 0;
        }
        else
        {
            do
            {
                randomPattern = Random.Range(0, patterns);

            } while (randomPattern == obj.CurrentPattern);

            obj.CurrentPattern = randomPattern;
        }
        
        obj.Patterns[obj.CurrentPattern].StartPattern(obj);
    }

    public void Exit(BossController obj)
    {
        if (obj.Patterns != null && obj.CurrentPattern >= 0)
        {
            obj.Patterns[obj.CurrentPattern].StopAttack();
        }
        
        obj.BAnimator.SetBool(obj.IsAttack, false);
    }

    public void Update(BossController obj)
    {
        if (obj.Patterns[obj.CurrentPattern].isFinish)
        {
            obj.ChangeState(BossState.Idle);
        }
    }
}
