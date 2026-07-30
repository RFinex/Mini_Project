using UnityEngine;
using System.Collections;

public class BossPattern_4 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p4_PatternDelay);

        yield return wait;
        for (int i = 0; i < data.p4_AttackCount; i++)
        {
            float baseAngle = GetAngle(boss.GetAttackPosDirection());
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
            fire.transform.position = boss.AttackPos.position;
            fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + i * data.p4_Angle);
            yield return waitDelay;
        }

        yield return wait2;
        isFinish = true;
    }
}
