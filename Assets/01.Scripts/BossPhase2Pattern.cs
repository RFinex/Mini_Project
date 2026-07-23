using System.Collections;
using UnityEngine;

public class BossPhase2Pattern : BossPatternBase
{
    protected override IEnumerator Pattern_1()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;
            yield return new WaitForSeconds(0.5f);
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
