using DG.Tweening;
using UnityEngine;

public class BossDieState : IState<BossController>
{
    private float timer;
    private bool isDying;

    public void Enter(BossController obj)
    {
        timer = 0f;
        isDying = false;
    }

    public void Exit(BossController obj)
    {
        
    }

    public void Update(BossController obj)
    {
        timer += Time.deltaTime;
        if (timer >= obj.DieDelay && !isDying)
        {
            isDying = true;

            obj.Sr.DOFade(0f, obj.FadeDelay)
                .SetLink(obj.gameObject, LinkBehaviour.KillOnDisable)
                .OnComplete(() => StageManager.instance.ExitBoss());
        }
    }
}
