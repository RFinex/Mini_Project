using UnityEngine;
using System.Collections;

public class BossPattern_8 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        if (data.p8_BulCount <= 0)
        {
            Debug.Log("8번 패턴 실행 실패. bulCount가 0 이하");
            isFinish = true;
            yield break;
        }

        float currentAngle = 0f;
        float nextAngle = 360f / data.p8_BulCount;
        for (int i = 0; i < data.p8_AttackCount; i++)
        {
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < data.p8_BulCount; j++)
            {
                float angle = currentAngle + (j * nextAngle);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            currentAngle += data.p8_AngleOffset;
            yield return wait2;
        }

        isFinish = true;
    }
}
