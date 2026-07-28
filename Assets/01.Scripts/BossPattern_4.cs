using UnityEngine;
using System.Collections;

public class BossPattern_4 : BossPatternBase
{
    [SerializeField] private float patternDelay;
    private WaitForSeconds waitDelay;
    private void Awake()
    {
        waitDelay = new WaitForSeconds(patternDelay);
    }

    protected override IEnumerator Pattern()
    {
        yield return wait;
        angle = 10f;
        for (int i = 0; i < attackCount; i++)
        {
            SetAngle(boss.GetAttackPosDirection());
            SoundManager.instance.PlaySFX(SFXType.Fireball);
            GameObject fire = ObjectPoolManager.instance.GetObject(ConstString.bossBullet);
            fire.transform.position = boss.AttackPos.position;
            fire.transform.rotation = Quaternion.Euler(0f, 0f, baseAngle + i * angle);
            yield return waitDelay;
        }

        yield return wait2;
        isFinish = true;
    }
}
