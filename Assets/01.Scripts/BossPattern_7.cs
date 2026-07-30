using UnityEngine;
using System.Collections;

public class BossPattern_7 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < data.p7_AttackCount; i++)
        {
            Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
            Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = randMove;

            yield return wait;
        }

        isFinish = true;
    }
}
