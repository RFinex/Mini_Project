using System.Collections;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected BossPatternDataSO data;

    protected int randPattern;
    protected int currentPattern = 0;
    public bool isFinish;

    protected WaitForSeconds wait;
    protected WaitForSeconds wait2;
    protected BossController boss;
    
    protected Vector2 bulDir;
    protected Quaternion rotate;

    public void SetPatternData(BossPatternDataSO data)
    {
        this.data = data;

        if (this.data != null)
        {
            wait = new WaitForSeconds(data.delayFrame);
            wait2 = new WaitForSeconds(data.delay2Frame);
        }
    }

    public void StartPattern(BossController obj)
    {
        boss = obj;
        isFinish = false;
                
        obj.BAnimator.SetBool(obj.IsAttack, true);

        StartCoroutine(Pattern());
        
    }

    protected abstract IEnumerator Pattern();

    public float GetAngle(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }    
}
