using UnityEngine;
using System.Collections;

public class BossPattern_3 : BossPatternBase
{
    [SerializeField] private float randomAngle;
    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < attackCount; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < 5; j++)
            {
                SetAngle(boss.GetAttackPosDirection());
                angle = Random.Range(-randomAngle, randomAngle);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + angle);
            }
            yield return wait2;
        }

        isFinish = true;
    }
}
