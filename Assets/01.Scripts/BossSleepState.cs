using System.Collections.Generic;
using UnityEngine;

public class BossSleepState : IState<BossController>
{
    private Transform targetPos;
    private List<Rect> moveArea;

    public void Enter(BossController obj)
    {
        targetPos = StageManager.instance.PlayerPos;
        moveArea = StageManager.instance.bossMoveArea;
    }

    public void Exit(BossController obj)
    {
        
    }

    public void Update(BossController obj)
    {
        if (targetPos == null || moveArea == null)
            return;

        foreach (Rect area in moveArea)
        {
            if (area.Contains(targetPos.position))
            {
                obj.ChangeState(obj.enterState);
            }
        }

    }
}
