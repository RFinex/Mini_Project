using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossPattern_6 : BossPatternBase
{
    [SerializeField] private int bulCount;
    [SerializeField] private float patternDelay;
    private WaitForSeconds waitDelay;

    private void Awake()
    {
        waitDelay = new WaitForSeconds(patternDelay);
    }

    protected override IEnumerator Pattern()
    {
        Rect moveRect = RectArea.instance.bossMoveArea[Random.Range(0, RectArea.instance.bossMoveArea.Count)];
        Vector2 randMove = new Vector2(Random.Range(moveRect.xMin, moveRect.xMax), Random.Range(moveRect.yMin, moveRect.yMax));

        boss.transform.DOMove(randMove, boss.Speed)
            .SetLink(gameObject)
            .SetEase(Ease.Linear);

        for (int i = 0; i < attackCount; i++)
        {
            yield return wait;
            SetAngle(boss.GetAttackPosDirection());

            // 총알 간격 계산 후 시작 각도 지정
            float startAngle = baseAngle - (angle * (bulCount - 1) / 2f);

            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < bulCount; j++)
            {
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, startAngle + (angle * j));
            }

            yield return wait2;

            for (int k = 0; k < attackCount; k++)
            {
                GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
                warning.transform.position = StageManager.instance.PlayerPos.position;
                yield return waitDelay;
            }
        }

        isFinish = true;
    }
}
