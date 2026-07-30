using DG.Tweening;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BossPattern_12 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p12_PatternDelay);
        WaitForSeconds waitDelay2 = new WaitForSeconds(data.p12_PatternDelay2);
        WaitForSeconds waitDelay3 = new WaitForSeconds(data.p12_PatternDelay3);

        if (data.p12_BulCount2 <= 0)
        {
            Debug.Log("12번 패턴 실행 실패. bulCount2가 0 이하");
            isFinish = true;
            yield break;
        }

        foreach (Rect rect in RectArea.instance.bossLaserArea)
        {
            GameObject laser = ObjectPoolManager.instance.GetObject(ConstString.laser);
            laser.transform.position = rect.position;
            laser.transform.rotation = Quaternion.Euler(0f, 0f, data.p12_LaserAngle);

            boss.transform.position = rect.position;

            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);

            for (int j = 0; j < data.p12_BulCount; j++)
            {
                yield return waitDelay;
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, j * data.p12_Angle);
            }
            yield return waitDelay3;
        }

        // 높은곳에 위치한 영역 찾아오기
        Rect top = RectArea.instance.bossMoveArea
            .OrderByDescending(rect => rect.center.y)
            .FirstOrDefault();

        Vector2 topPos = top.center;

        boss.BAnimator.SetBool(boss.IsAttack, false);
        Tween moveTween = boss.transform.DOMove(topPos, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.OutQuart);

        yield return moveTween.WaitForCompletion();

        float currentAngle = 0f;
        float nextAngle = 360f / data.p12_BulCount2;
        boss.BAnimator.SetBool(boss.IsAttack, true);
        for (int i = 0; i < data.p12_AttackCount; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;

            for (int j = 0; j < data.p12_BulCount2; j++)
            {
                SoundManager.instance.PlaySFX(SFXType.Fireball);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, nextAngle * j + currentAngle);
                yield return waitDelay;
            }
            currentAngle += data.p12_AngleOffset;

            yield return waitDelay2;
        }

        isFinish = true;
    }
}
