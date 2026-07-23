using UnityEngine;

public class BossEnterState : IState<BossController>
{
    private float moveDis;
    private float enterSpeed;
    private Vector3 enterTarget;

    public void Enter(BossController obj)
    {
        moveDis = 17f;
        enterSpeed = 2f;
        enterTarget = obj.transform.position + Vector3.down * moveDis;        
    }

    public void Exit(BossController obj)
    {
        obj.Col.enabled = true;
    }

    public void Update(BossController obj)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, enterTarget, enterSpeed * Time.deltaTime);
        if (obj.transform.position == enterTarget)
        {
            obj.ChangeState(obj.idleState);
        }
    }
}
