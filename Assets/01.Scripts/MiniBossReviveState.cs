using System.Collections;
using UnityEngine;

public class MiniBossReviveState : IState<MiniBossController>
{
    private float range;
    private float reviveDelay;
    private WaitForSeconds wait;

    private bool isRevive;
    public void Enter(MiniBossController obj)
    {
        range = 5f;
        reviveDelay = (1f + 40f / 60f);
        isRevive = false;
        wait = new WaitForSeconds(reviveDelay);
        obj.Col.enabled = false;
    }

    public void Exit(MiniBossController obj)
    {
        
    }

    public void Update(MiniBossController obj)
    {
        if (!obj.IsStartBoss && !isRevive)
        {
            CheckDistance(obj);
        }
    }

    private void CheckDistance(MiniBossController obj)
    {
        float distance = Vector3.Distance(obj.transform.position, obj.Target.position);
        if (distance < range)
        {
            isRevive = true;
            obj.StartCoroutine(ReviveDelay(obj));
        }            
    }
    public IEnumerator ReviveDelay(MiniBossController obj)
    {
        obj.MiniBossStart();

        yield return wait;

        obj.ChangeIdleState();
    }
}
