using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossPattern_11 : BossPatternBase
{
    [SerializeField] private float bulSpeed;
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
            for (int j = 0; j < attackCount; j++)
            {
                Vector3 attackPos = boss.AttackPos.position;
                attackPos.y = Random.Range(moveRect.yMin, moveRect.yMax);

                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = attackPos;
                if (fire.TryGetComponent<BossBullet>(out BossBullet bul))
                {
                    bul.SetSpeed(bulSpeed);
                }

                yield return waitDelay2;
            }

            yield return wait2;
        }

        isFinish = true;
    }
}
