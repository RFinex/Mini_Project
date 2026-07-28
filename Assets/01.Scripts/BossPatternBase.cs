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
    [SerializeField] protected float baseAngle;
    [SerializeField] protected float angle;

    [SerializeField] protected int attackCount;
    
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

        StartCoroutine(Pattern());
        
    }

    protected abstract IEnumerator Pattern();

    public void SetAngle(Vector2 dir)
    {
        baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }
}
