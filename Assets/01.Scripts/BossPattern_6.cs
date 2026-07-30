using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossPattern_6 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p6_PatternDelay);

        Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
        Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

        boss.transform.DOMove(randMove, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.Linear);

        for (int i = 0; i < data.p6_AttackCount; i++)
        {
            yield return wait;

            float baseAngle = GetAngle(boss.GetAttackPosDirection());

            // 총알 간격 계산 후 시작 각도 지정
            float startAngle = baseAngle - (data.p6_Angle * (data.p6_BulCount - 1) / 2f);

            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < data.p6_BulCount; j++)
            {
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, startAngle + (data.p6_Angle * j));
            }

            yield return wait2;

            for (int k = 0; k < data.p6_AttackCount; k++)
            {
                GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
                warning.transform.position = StageManager.instance.PlayerPos.position;
                yield return waitDelay;
            }
        }

        isFinish = true;
    }
}
