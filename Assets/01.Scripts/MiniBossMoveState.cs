using UnityEngine;
using DG.Tweening;

public class MiniBossMoveState : IState<MiniBossController>
{
    private Rect moveRect;
    private Vector2 movePos;
    private Tween moveTween;
    public void Enter(MiniBossController obj)
    {
        moveRect = obj.moveArea;
        movePos = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));
        Move(obj);
    }

    public void Exit(MiniBossController obj)
    {
        moveTween?.Kill();
    }

    public void Update(MiniBossController obj)
    {
        
    }

    private void Move(MiniBossController obj)
    {
        moveTween = obj.transform.DOMove(movePos, 1f)
            .SetLink(obj.gameObject)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => obj.FlipBoss());
    }
}
