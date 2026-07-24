using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BossPhase3Pattern : BossPatternBase
{
    [SerializeField] private float pattern_2_Angle = 15f;
    [SerializeField] private float pattern_3_bulSpeed = 16f;

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
        float currentAngle = 0f;
        Rect moveRect = StageManager.instance.bossMoveArea[Random.Range(0, StageManager.instance.bossMoveArea.Count)];
        Vector2 centerPos = moveRect.center;
        transform.DOMove(centerPos, boss.Speed / 2)
            .SetLink(gameObject)
            .SetEase(Ease.InOutCubic);

        yield return wait;
        for (int i = 0; i < attackCount2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                angle = j * 90f;
                SoundManager.instance.PlaySFX(SFXType.Fireball);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle + angle);

                GameObject fire2 = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire2.transform.position = boss.AttackPos.position;
                fire2.transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle + angle);
            }

            currentAngle += pattern_2_Angle;

            yield return new WaitForSeconds(0.2f);
        }
        
        isFinish = true;
    }

    protected override IEnumerator Pattern_3()
    {
        for (int i = 0; i < StageManager.instance.bossMoveArea.Count; i++)
        {
            boss.BAnimator.SetBool(boss.IsAttack, false);
            Rect moveRect = StageManager.instance.bossMoveArea[i];
            Vector2 leftPos = new Vector2(moveRect.xMin, moveRect.center.y);

            Tween moveTween = transform.DOMove(leftPos, boss.Speed)
                .SetLink(gameObject)
                .SetEase(Ease.InOutCubic);

            yield return moveTween.WaitForCompletion();

            yield return new WaitForSeconds(2f);

            boss.BAnimator.SetBool(boss.IsAttack, true);
            for (int j = 0; j < attackCount3; j++)
            {
                Vector3 attackPos = boss.AttackPos.position;
                attackPos.y = Random.Range(moveRect.yMin, moveRect.yMax);

                SoundManager.instance.PlaySFX(SFXType.Fireball);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = attackPos;
                if (fire.TryGetComponent<BossBullet>(out BossBullet bul))
                {
                    bul.SetSpeed(pattern_3_bulSpeed);
                }

                yield return new WaitForSeconds(0.05f);
            }

            yield return wait2;
        }
        
        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        yield return null;
        isFinish = true;
    }
}
