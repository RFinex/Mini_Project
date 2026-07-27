using UnityEngine;

public class MonsterIdleState : IState<EnemyController>
{
    public void Enter(EnemyController obj)
    {
        
    }

    public void Exit(EnemyController obj)
    {
        
    }

    public void Update(EnemyController obj)
    {
        float distance = Vector3.Distance(obj.transform.position, obj.Target.position);

        if (distance <= obj.Range)
        {
            if (obj.CanMove)
            {
                obj.ChangeState(MonsterState.Trace);
            }
            else
            {
                obj.ChangeState(MonsterState.Attack);
            }
        }
    }
}
