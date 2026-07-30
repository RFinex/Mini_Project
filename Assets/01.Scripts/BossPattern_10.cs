using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossPattern_10 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p10_PatternDelay);

        if (data.p10_BulCount <= 0)
        {
            Debug.Log("10번 패턴 실행 실패. bulCount가 0 이하");
            isFinish = true;
            yield break;
        }

        boss.BAnimator.SetBool(boss.IsAttack, false);
        float currentAngle = 0f;
        float nextAngle = 360f / data.p10_BulCount;

        Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
        Vector2 centerPos = moveRect.center;
        yield return boss.transform.DOMove(centerPos, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.InOutCubic)
            .WaitForCompletion();

        boss.BAnimator.SetBool(boss.IsAttack, true);
        yield return wait;
        
        for (int i = 0; i < data.p10_AttackCount; i++)
        {
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < data.p10_BulCount; j++)
            {
                float angle = j * nextAngle;
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle + angle);

                GameObject fire2 = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire2.transform.position = boss.AttackPos.position;
                fire2.transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle + angle);
            }

            currentAngle += data.p10_AngleOffset;

            yield return waitDelay;
        }

        isFinish = true;
    }
}
