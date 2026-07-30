using UnityEngine;
using System.Collections;

public class BossPattern_5 : BossPatternBase
{
    protected override IEnumerator Pattern()
    {
        WaitForSeconds waitDelay = new WaitForSeconds(data.p5_PatternDelay);

        for (int i = 0; i < data.p5_AttackCount; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;
            yield return waitDelay;
        }

        isFinish = true;
    }
}
