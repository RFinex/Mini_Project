using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected int maxHp;
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
    }
    protected int nowHp;

    public int NowHp
    {
        get
        {
            return nowHp;
        }
        private set
        {
            nowHp = value;
        }
    }
    protected float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        private set
        {
            speed = value;
        }
    }

    protected SpriteRenderer sr;
    protected Transform target;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }

    public virtual void TakeDamage()
    {
        nowHp--;

        if (nowHp <= 0)
            Die();
    }

    protected abstract void Die();
}
