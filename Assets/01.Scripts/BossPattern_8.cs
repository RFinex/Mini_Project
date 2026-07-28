using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class BossPattern_8 : BossPatternBase
{
    [SerializeField] private float addAngle;
    [SerializeField] private int bulCount;
    protected override IEnumerator Pattern()
    {
        if (bulCount <= 0)
        {
            Debug.Log("8번 패턴 실행 실패. bulCount가 0 이하");
            isFinish = true;
            yield break;
        }

        float currentAngle = 0f;
        float nextAngle = 360f / bulCount;
        for (int i = 0; i < attackCount; i++)
        {
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < bulCount; j++)
            {
                angle = currentAngle + (j * nextAngle);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            currentAngle += addAngle;
            yield return wait2;
        }

        isFinish = true;
    }
}
