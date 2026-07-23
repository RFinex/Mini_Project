using System.Collections;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected int randPattern;
    public bool isFinish;
    protected BossController boss;
    protected float delay;
    protected WaitForSeconds wait;

    private void Awake()
    {
        delay = (45f / 60f);
        wait = new WaitForSeconds(delay);
    }

    public void StartRandomPattern(BossController obj)
    {
        boss = obj;
        isFinish = false;
        //randPattern = Random.Range(1, 5);
        randPattern = Random.Range(1, 2);

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

        obj.BAnimator.SetBool(obj.IsAttack, true);
    }

    protected abstract IEnumerator Pattern_1();
    protected abstract IEnumerator Pattern_2();
    protected abstract IEnumerator Pattern_3();
    protected abstract IEnumerator Pattern_4();
}
