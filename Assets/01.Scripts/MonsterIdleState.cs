using UnityEngine;

public class MonsterIdleState : IState<EnemyController>
{
    public void Enter(EnemyController obj)
    {
        Debug.Log("대기 상태");
    }

    public void Exit(EnemyController obj)
    {
        
    }

    public void Update(EnemyController obj)
    {
        float distance = Vector3.Distance(obj.transform.position, obj.Target.position);

        if (distance <= obj.Range)
        {
            obj.ChangeState(MonsterState.Trace);
        }
    }
}
