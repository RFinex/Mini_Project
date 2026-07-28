using DG.Tweening;
using System.Linq;
using UnityEngine;
using System.Collections;

public class BossPattern_12 : BossPatternBase
{
    [SerializeField] private float laserAngle;
    [SerializeField] private int bulCount;
    [SerializeField] private int bulCount2;
    [SerializeField] private float addAngle;
    [SerializeField] private float patternDelay;
    [SerializeField] private float patternDelay2;

    private WaitForSeconds waitDelay;
    private WaitForSeconds waitDelay2;

    private void Awake()
    {
        waitDelay = new WaitForSeconds(patternDelay);
        waitDelay2 = new WaitForSeconds(patternDelay2);
    }

    protected override IEnumerator Pattern()
    {
        if (bulCount2 <= 0)
        {
            Debug.Log("12번 패턴 실행 실패. bulCount2가 0 이하");
            isFinish = true;
            yield break;
        }

        foreach (Rect rect in RectArea.instance.bossLaserArea)
        {
            GameObject laser = ObjectPoolManager.instance.GetObject(ConstString.laser);
            laser.transform.position = rect.position;
            laser.transform.rotation = Quaternion.Euler(0f, 0f, laserAngle);

            boss.transform.position = rect.position;

            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < bulCount; j++)
            {
                yield return waitDelay;
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, j * angle);
            }
            yield return wait2;
        }

        // 높은곳에 위치한 영역 찾아오기
        Rect top = RectArea.instance.bossMoveArea
            .OrderByDescending(rect => rect.center.y)
            .FirstOrDefault();

        Vector2 topPos = top.center;

        boss.BAnimator.SetBool(boss.IsAttack, false);
        Tween moveTween = transform.DOMove(topPos, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.OutQuart);

        yield return moveTween.WaitForCompletion();

        float currentAngle = 0f;
        float nextAngle = 360f / bulCount2;
        boss.BAnimator.SetBool(boss.IsAttack, true);
        for (int i = 0; i < attackCount; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;

            for (int j = 0; j < bulCount2; j++)
            {
                SoundManager.instance.PlaySFX(SFXType.Fireball);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, nextAngle * j + currentAngle);
                yield return waitDelay;
            }
            currentAngle += addAngle;

            yield return waitDelay2;
        }

        isFinish = true;
    }
}
