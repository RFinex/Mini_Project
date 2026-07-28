using UnityEngine;
using System.Collections;

public class BossPattern_9 : BossPatternBase
{
    [SerializeField] private float patternDelay;
    protected override IEnumerator Pattern()
    {
        Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];

        for (int i = 0; i < attackCount; i++)
        {
            Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = randMove;
            yield return new WaitForSeconds(patternDelay);
        }

        isFinish = true;
    }
}
