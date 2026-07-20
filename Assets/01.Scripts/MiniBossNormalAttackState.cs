using UnityEngine;

public class MiniBossNormalAttackState : IState<MiniBossController>
{
    public void Enter(MiniBossController obj)
    {
        int nextPattern = Random.Range(0, 2);
    }

    public void Exit(MiniBossController obj)
    {
        
    }

    public void Update(MiniBossController obj)
    {
        if (obj.canAttack)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject("minibossBullet");

        }
    }
}
