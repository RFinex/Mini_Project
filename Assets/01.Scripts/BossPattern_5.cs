using UnityEngine;
using System.Collections;

public class BossPattern_5 : BossPatternBase
{
    [SerializeField] private float patternDelay;
    private WaitForSeconds waitDelay;

    private void Awake()
    {
        waitDelay = new WaitForSeconds(patternDelay);
    }

    protected override IEnumerator Pattern()
    {
        for (int i = 0; i < attackCount; i++)
        {
            GameObject warning = ObjectPoolManager.instance.GetObject(ConstString.warningSign);
            warning.transform.position = StageManager.instance.PlayerPos.position;
            yield return waitDelay;
        }

        isFinish = true;
    }
}
