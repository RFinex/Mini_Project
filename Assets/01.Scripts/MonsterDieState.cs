using UnityEngine;

public class MonsterDieState : IState<EnemyController>
{
    private float timer;
    private bool isDying;

    public void Enter(EnemyController obj)
    {
        timer = 0f;
        isDying = false;
    }

    public void Exit(EnemyController obj)
    {
        timer = 0f;
        isDying = false;
    }

    public void Update(EnemyController obj)
    {
        timer += Time.deltaTime;
        if (!isDying && timer >= obj.DieDelay)
        {
            isDying = true;
            obj.ReturnPool();
        }
    }
}
