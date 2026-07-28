using System.Collections;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected int randPattern;
    protected int currentPattern = 0;
    public bool isFinish;
    [SerializeField] protected float delayFrame = (45f / 60f);
    [SerializeField] protected float delay2Frame = (20f / 60f);
    protected WaitForSeconds wait;
    protected WaitForSeconds wait2;
    protected BossController boss;
    protected float baseAngle;
    protected float angle;

    [SerializeField] protected int attackCount = 1;
    [SerializeField] protected int attackCount2 = 2;
    [SerializeField] protected int attackCount3 = 3;
    [SerializeField] protected int attackCount4 = 4;
    
    protected Vector2 bulDir;

    protected Quaternion rotate;

    private void Awake()
    {
        wait = new WaitForSeconds(delayFrame);
        wait2 = new WaitForSeconds(delay2Frame);
    }

    public void StartPattern(BossController obj)
    {
        boss = obj;
        isFinish = false;
                
        obj.BAnimator.SetBool(obj.IsAttack, true);

        // 나중에 패턴마다 클래스 분리할 것
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

    public void SetAngle(Vector2 dir)
    {
        baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
