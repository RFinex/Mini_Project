using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BossPhase2Pattern : BossPatternBase
{
    [SerializeField] private int pattern_2_BulCount = 5;
    [SerializeField] private float pattern_4_Angle = 10f;
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
        Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
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
                SoundManager.instance.PlaySFX(SFXType.Fireball);
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
        for (int i = 0; i < attackCount3; i++)
        {
            Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
            Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = randMove;

            yield return wait;
        }

        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        float currentAngle = 0f;
        for (int i = 0; i < attackCount4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                angle = currentAngle + (j * 90f);
                SoundManager.instance.PlaySFX(SFXType.Fireball);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            currentAngle += pattern_4_Angle;
            yield return wait2;
        }

        isFinish = true;
    }
}
