using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    protected float speed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float timer;

    protected int damage;

    protected float dir;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        timer = 0f;
    }

    protected virtual void OnEnable()
    {
        timer = 0f;
        dir = 1f;
        StopAllCoroutines();
        StartCoroutine(LifeDelay());
    }

    protected void Start()
    {
        StartCoroutine(LifeDelay());
    }

    protected IEnumerator LifeDelay()
    {
        while (true)
        {
            if (timer >= lifeTime)
            {
                ReturnPool();
            }
            yield return null;
        }
    }

    protected void Update()
    {
        timer += Time.deltaTime;
    }

    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public virtual void SetDirection(float dir)
    {
        this.dir = dir;
    }

    public abstract void ReturnPool();
}
