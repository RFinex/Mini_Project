using UnityEngine;
using DG.Tweening;

public class MiniBossMoveState : IState<MiniBossController>
{
    private Rect moveRect;
    private Vector2 movePos;
    private Tween moveTween;
    private float moveDelay;
    public void Enter(MiniBossController obj)
    {
        moveDelay = obj.Speed;
        moveRect = obj.moveArea;
        movePos = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));
        obj.MBAnimator.SetBool(obj.IsMove, true);
        Move(obj);
    }

    public void Exit(MiniBossController obj)
    {
        moveTween?.Kill();
        obj.FlipBoss();
        obj.MBAnimator.SetBool(obj.IsMove, false);
    }

    public void Update(MiniBossController obj)
    {
        
    }

    private void Move(MiniBossController obj)
    {
        moveTween = obj.transform.DOMove(movePos, moveDelay)
            .SetLink(obj.gameObject)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => obj.ChangeIdleState());
    }
}
