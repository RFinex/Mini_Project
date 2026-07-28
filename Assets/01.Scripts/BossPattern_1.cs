using UnityEngine;
using System.Collections;

public class BossPattern_1 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < attackCount; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
            fire.transform.position = boss.AttackPos.position;
            SetAngle(boss.GetAttackPosDirection());
            fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle);

            yield return wait2;
        }

        isFinish = true;
    }
}
