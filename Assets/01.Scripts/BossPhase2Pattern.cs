using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BossPhase2Pattern : BossPatternBase
{
    [SerializeField] private int pattern_2_BulCount = 5;
    protected override IEnumerator Pattern_1()
    {
        for (int i = 0; i < attackCount; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;
            yield return new WaitForSeconds(0.5f);
        }

        isFinish = true;
    }

    protected override IEnumerator Pattern_2()
    {
        Rect moveRect = StageManager.instance.bossMoveArea[Random.Range(0, StageManager.instance.bossMoveArea.Count)];
        Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));
        
        transform.DOMove(randMove, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.Linear);

        angle = 15f;
        for (int i = 0; i < attackCount2; i++)
        {
            yield return wait;
            SetAngle(boss.GetAttackPosDirection());
            
            // 총알 간격 계산 후 시작 각도 지정
            float startAngle = baseAngle - (angle * (pattern_2_BulCount - 1) / 2f);

            for (int j = 0; j < pattern_2_BulCount; j++)
            {
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, startAngle + (angle * j));
            }

            yield return wait2;

            for (int k = 0; k < attackCount2; k++)
            {
                GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
                warning.transform.position = StageManager.instance.PlayerPos.position;
                yield return new WaitForSeconds(0.5f);
            }
        }

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
