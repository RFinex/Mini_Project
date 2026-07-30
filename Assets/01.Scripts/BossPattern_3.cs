using UnityEngine;
using System.Collections;

public class BossPattern_3 : BossPatternBase
{
    [SerializeField] private float randomAngle;
    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < data.p3_AttackCount; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < data.p3_BulCount; j++)
            {
                float baseAngle = GetAngle(boss.GetAttackPosDirection());
                float angle = Random.Range(-data.p3_RandAngle, data.p3_RandAngle);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + angle);
            }
            yield return wait2;
        }

        isFinish = true;
    }
}
