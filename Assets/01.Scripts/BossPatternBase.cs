using System.Collections;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected int randPattern;
    public bool isFinish;
    protected float delay;
    protected float delay2;
    protected WaitForSeconds wait;
    protected WaitForSeconds wait2;
    protected BossController boss;
    protected float baseAngle;
    protected float angle;
    
    protected Vector2 bulDir;

    protected Quaternion rotate;

    private void Awake()
    {
        delay = (45f / 60f);
        delay2 = (20f / 60f);
        wait = new WaitForSeconds(delay);
        wait2 = new WaitForSeconds(delay2);
    }

    public void StartRandomPattern(BossController obj)
    {
        boss = obj;
        isFinish = false;
        //randPattern = Random.Range(1, 5);
        // 테스트 코드
        randPattern = UnityEngine.Random.Range(1, 3);

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

    public void SetAngle(Vector2 dir)
    {
        baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
