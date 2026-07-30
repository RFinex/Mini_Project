using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossPattern_11 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p11_PatternDelay);
        WaitForSeconds waitDelay2 = new WaitForSeconds(data.p11_PatternDelay2);

        for (int i = 0; i < RectArea.instance.bossMoveArea.Count; i++)
        {
            boss.BAnimator.SetBool(boss.IsAttack, false);
            Rect moveRect = RectArea.instance.bossMoveArea[i];
            Vector2 leftPos = new Vector2(moveRect.xMin, moveRect.center.y);

            Tween moveTween = boss.transform.DOMove(leftPos, boss.Speed)
                .SetLink(gameObject)
                .SetEase(Ease.InOutCubic);

            yield return moveTween.WaitForCompletion();

            yield return waitDelay;

            boss.BAnimator.SetBool(boss.IsAttack, true);

            SoundManager.instance.PlaySFX(SFXType.Fireball);

            for (int j = 0; j < data.p11_AttackCount; j++)
            {
                Vector3 attackPos = boss.AttackPos.position;
                attackPos.y = Random.Range(moveRect.yMin, moveRect.yMax);

                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = attackPos;
                if (fire.TryGetComponent<BossBullet>(out BossBullet bul))
                {
                    bul.SetSpeed(data.p11_BulSpeed);
                }

                yield return waitDelay2;
            }

            yield return wait2;
        }

        isFinish = true;
    }
}
