using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MiniBossDieState : IState<MiniBossController>
{
    private float timer;
    private float dieDelay;
    private bool isDying;
    public void Enter(MiniBossController obj)
    {
        timer = 0f;
        dieDelay = (2f + 10f / 60f);
        isDying = false;
    }

    public void Exit(MiniBossController obj)
    {
        
    }

    public void Update(MiniBossController obj)
    {
        timer += Time.deltaTime;
        if (timer >= dieDelay && !isDying)
        {
            isDying = true;

            obj.Sr.DOFade(0f, 5f)
                .SetLink(obj.gameObject, LinkBehaviour.KillOnDisable)
                .OnComplete(() => )
        }
    }    
}
