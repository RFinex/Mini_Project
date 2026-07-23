using System.Collections;
using UnityEngine;

public class BossPhase2Pattern : BossPatternBase
{
    protected override IEnumerator Pattern_1()
    {
        yield return null;
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
