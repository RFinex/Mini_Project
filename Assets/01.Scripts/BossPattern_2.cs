using UnityEngine;
using System.Collections;

public class BossPattern_2 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < data.p2_AttackCount; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < data.p2_BulCount; j++)
            {
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, j * data.p2_Angle);
            }
            yield return wait2;
        }

        isFinish = true;
    }
}
