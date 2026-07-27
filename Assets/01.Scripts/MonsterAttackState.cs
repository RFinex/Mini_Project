using UnityEngine;

public class MonsterAttackState : IState<EnemyController>
{
    private float timer;

    public void Enter(EnemyController obj)
    {
        timer = 0f;
        obj.SetAttackAnim(true);
    }

    public void Exit(EnemyController obj)
    {
        timer = 0f;
        obj.SetAttackAnim(false);
    }

    public void Update(EnemyController obj)
    {
        obj.FlipSprite();

        float distance = Vector3.Distance(obj.transform.position, obj.Target.position);

        if (distance > obj.Range)
        {
            obj.ChangeState(MonsterState.Idle);
        }

        timer += Time.deltaTime;
        if (timer >= obj.AttackDelay)
        {
            timer = 0f;
            obj.Attack();
        }
    }
}
