using UnityEngine;
using System.Collections;

public class BossPhase3Pattern : BossPatternBase
{
    protected override IEnumerator Pattern_1()
    {
        Rect moveRect = StageManager.instance.bossMoveArea[Random.Range(0, StageManager.instance.bossMoveArea.Count)];

        for (int i = 0; i < attackCount; i++)
        {
            Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = randMove;
            yield return new WaitForSeconds(0.1f);
        }

        isFinish = true;
    }

    protected override IEnumerator Pattern_2()
    {
        yield return null;
        isFinish = true;
    }

    protected override IEnumerator Pattern_3()
    {
        yield return null;
        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        yield return null;
        isFinish = true;
    }
}
