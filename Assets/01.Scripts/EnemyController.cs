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
    protected Collider2D col;
    public Collider2D Col
    {
        get
        {
            return col;
        }
    }
    protected Transform target;
    public Transform Target
    {
        get
        {
            return target;
        }
    }

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    protected virtual void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }

    public abstract void TakeDamage();

    protected abstract void Die();
}
