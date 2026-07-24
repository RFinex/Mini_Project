using UnityEngine;
using System.Collections;

public class BossPhase1Pattern : BossPatternBase
{

    protected override IEnumerator Pattern_1()
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

    protected override IEnumerator Pattern_2()
    {
        angle = 30f;
        for (int i = 0; i < attackCount2; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < 12; j++)
            {
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;                
                fire.transform.rotation = Quaternion.Euler(0f, 0f, j * angle);
            }
            yield return wait2;
        }
        
        isFinish = true;
    }

    protected override IEnumerator Pattern_3()
    {
        for (int i = 0; i < attackCount3; i++)
        {
            yield return wait;
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            for (int j = 0; j < 5; j++)
            {
                SetAngle(boss.GetAttackPosDirection());
                angle = Random.Range(-30f, 30f);
                GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
                fire.transform.position = boss.AttackPos.position;
                fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + angle);
            }
            yield return wait2;
        }

        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        yield return wait;
        angle = 10f;
        for (int i = 0; i < attackCount4; i++)
        {
            SetAngle(boss.GetAttackPosDirection());
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
            fire.transform.position = boss.AttackPos.position;
            fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + i * angle);
            yield return new WaitForSeconds(0.05f);
        }

        yield return wait2;
        isFinish = true;
    }
}
