using UnityEngine;
using System.Collections;

public class BossPhase1Pattern : BossPatternBase
{
    protected override IEnumerator Pattern_1()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return wait;
            GameObject fire = ObjectPoolManager.instance.GetObject("bossBullet");
            fire.transform.position = boss.AttackPos.position;
            fire.GetComponent<BossBullet>().SetDirection(boss.GetAttackPosDirection());

            yield return new WaitForSeconds(20f / 60f);
        }
        isFinish = true;
    }

    protected override IEnumerator Pattern_2()
    {
        yield return null;
        isFinish = true;
    }

    protected override IEnumerator Pattern_3()
    {
        yield return null;
        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        yield return null;
        isFinish = true;
    }
}
