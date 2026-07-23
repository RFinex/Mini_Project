using System.Collections;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected int randPattern;
    public bool isFinish;

    public void StartRandomPattern()
    {
        isFinish = false;
        randPattern = Random.Range(1, 5);

        switch (randPattern)
        {
            case 1:
                StartCoroutine(Pattern_1());
                break;
            case 2:
                StartCoroutine(Pattern_2());
                break;
            case 3:
                StartCoroutine(Pattern_3());
                break;
            case 4:
                StartCoroutine(Pattern_4());
                break;
        }
    }

    protected abstract IEnumerator Pattern_1();
    protected abstract IEnumerator Pattern_2();
    protected abstract IEnumerator Pattern_3();
    protected abstract IEnumerator Pattern_4();
}
