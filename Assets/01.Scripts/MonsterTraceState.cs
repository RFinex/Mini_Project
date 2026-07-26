using UnityEngine;

public class MonsterTraceState : IState<EnemyController>
{
    public void Enter(EnemyController obj)
    {
        Debug.Log("추적 상태");
    }

    public void Exit(EnemyController obj)
    {
        
    }

    public void Update(EnemyController obj)
    {
        obj.FlipSprite();

        obj.transform.position = Vector3.MoveTowards(obj.transform.position, obj.Target.position, obj.Speed * Time.deltaTime);

        float distance = Vector3.Distance(obj.transform.position, obj.Target.position);

        if (distance > obj.Range)
        {
            obj.ChangeState(MonsterState.Idle);
        }
    }
}
