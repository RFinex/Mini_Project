using UnityEngine;

public class MiniBossMoveState : IState<MiniBossController>
{
    private Rect moveRect;
    private Vector2 movePos;
    public void Enter(MiniBossController obj)
    {
        moveRect = obj.moveArea;
        movePos = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));
    }

    public void Exit(MiniBossController obj)
    {
        
    }

    public void Update(MiniBossController obj)
    {
        
    }
}
