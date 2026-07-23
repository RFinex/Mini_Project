using UnityEngine;
using System.Collections;

public class BossPhase1Pattern : BossPatternBase
{

    protected override IEnumerator Pattern_1()
    {
        for (int i = 0; i < 10; i++)
        {

        }
        isFinish = true;
    }

    protected override IEnumerator Pattern_2()
    {
        isFinish = true;
    }

    protected override IEnumerator Pattern_3()
    {
        isFinish = true;
    }

    protected override IEnumerator Pattern_4()
    {
        isFinish = true;
    }
}
